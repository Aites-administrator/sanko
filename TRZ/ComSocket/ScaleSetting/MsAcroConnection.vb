Imports System.Net.Sockets
Imports System.Text.Encoding
Imports T.R.ZCommonClass.clsCommonFnc

Public Class MsAcroConnection
  Inherits clsClientHandler

#Region "メンバ"

#Region "プライベート"
  ''' <summary>
  ''' 受信処理完了後処理コールバック(外部通知)
  ''' </summary>
  Private _lcReceived As CallBackReadComp

  ''' <summary>
  ''' 受信処理完了後処理コールバック(内部通知)
  ''' </summary>
  Private _lcReadComp As CallBackReadComp = AddressOf PreReceive

  Private _ReceiveBuff() As Byte
  Private _bReceived As Boolean
#End Region

#End Region

#Region "プロパティー"
#Region "パブリック"
  Public Property ReceivedData As New MsAcroData
#End Region
#End Region

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  ''' <param name="prmSocket">Socket通信オブジェクト</param>
  ''' <param name="prmCbReadComp">読込完了時処理（外部通知）</param>
  Public Sub New(prmSocket As Socket, prmCbReadComp As CallBackReadComp)

    ' オブジェクト作成
    MyBase.New(prmSocket)

    ' 受信中フラグOFF
    _bReceived = False

    ' 読込完了時処理設定（計量器固有の設定）
    MyBase.SetCallBackRoadComp(_lcReadComp)

    ' 読込完了時処理設定（外部通知）
    _lcReceived = prmCbReadComp
  End Sub

#End Region

#Region "メソッド"
#Region "プライベート"

  ''' <summary>
  ''' 読込完了時処理
  ''' </summary>
  ''' <param name="sender">イベント発生オブジェクト</param>
  ''' <param name="prmBuff">受信データ</param>
  ''' <remarks>
  ''' 継承元クラスでの受信完了後に計量器固有の処理を行い、呼出し元にデータを返す
  ''' </remarks>
  Private Sub PreReceive(sender As clsClientHandler, prmBuff() As Byte)
    Dim tmpBuff() As Byte

    If _lcReceived IsNot Nothing Then
      If prmBuff Is Nothing Then
        Call _lcReceived(sender, Nothing)
      Else
        If _bReceived = False Then
          ' 初回受信
          ReDim _ReceiveBuff(-1)
        End If

        _bReceived = True

        tmpBuff = _ReceiveBuff.Concat(prmBuff).ToArray()
        _ReceiveBuff = tmpBuff

        With ReceivedData
          ' 可変部分を除いた全てが受信できたらヘッダー情報保持
          If _ReceiveBuff.Length >= (.HeaderLength) Then

            ' ヘッダー情報保持
            .SetHeader(_ReceiveBuff)

            ' 設定総バイト数分のデータを受信したら受信完了通知発行
            If (_ReceiveBuff.Length - .MainHeaderLength) >= .GetHeaderData(MsAcroData.DataName.総バイト数) Then

              _bReceived = False

              ' 計量実績受信時は実績データを変数に保持
              If .GetHeaderData(MsAcroData.DataName.処理識別番号) = "9501" Then
                ' モリタ屋特別仕様（ハンディ原料出荷）
                Dim tmpWeightResult = GetEncoding("UTF-8").GetString(_ReceiveBuff)
                tmpWeightResult = tmpWeightResult.Substring(0, 64) & MsAcroData.SCALE_HEADER_TITLE & vbCrLf & tmpWeightResult.Substring(64)

                .SetWeighingResults(GetEncoding("UTF-8").GetBytes(tmpWeightResult))
              Else
                Select Case .GetHeaderData(MsAcroData.DataName.処理識別番号)
                  Case "1501"
                    .SetWeighingResults(_ReceiveBuff)
                  Case Is >= "1502"
                    .SetMstRequestStatus(_ReceiveBuff)
                End Select
              End If


              ' 受信ログ出力
              Call ComWriteLog(Now().ToString() & " [RECEIVE]" & GetEncoding("UTF-8").GetString(_ReceiveBuff) _
                               , System.AppDomain.CurrentDomain.BaseDirectory & "\connection.log")

              Call _lcReceived(sender, _ReceiveBuff)

            End If
          End If

        End With

      End If

    End If
  End Sub

#End Region
#End Region

End Class
