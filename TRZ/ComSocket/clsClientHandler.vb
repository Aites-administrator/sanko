Imports System.Net.Sockets
Imports T.R.ZCommonClass.clsCommonFnc
Imports System.Text.Encoding

Public Class clsClientHandler
  Implements IDisposable
  '----------------------------------
  '     クライアントハンドルクラス
  '----------------------------------
  ' クライアント個別の送受信処理を行う

#Region "定数定義"
  ''' <summary>
  ''' 受信バッファサイズ
  ''' </summary>
  Private Const BUFFER_SIZE = 1024
#End Region

#Region "メンバ"

#Region "プライベート"

  ''' <summary>
  ''' 受信バッファ
  ''' </summary>
  Private _ReadBuff() As Byte

  ''' <summary>
  ''' Socketオブジェクト
  ''' </summary>
  Private _Socket As Socket

  ''' <summary>
  ''' ネットワーク通信オブジェクト
  ''' </summary>
  Private _NetStream As NetworkStream

  ''' <summary>
  ''' 受信完了後処理コールバック（内部通知）
  ''' </summary>
  Private _acReadComp As AsyncCallback

  ''' <summary>
  ''' 送信処理完了後処理コールバック(内部通知)
  ''' </summary>
  Private _acSendComp As AsyncCallback

#End Region

#Region "パブリック"

  ''' <summary>
  ''' 読込終了時処理（コールバック）
  ''' </summary>
  ''' <param name="prmBuff"></param>
  Public Delegate Sub CallBackReadComp(sender As Object, prmBuff() As Byte)

  ''' <summary>
  ''' 受信処理完了後処理コールバック(外部通知)
  ''' </summary>
  Private _lcReadComp As CallBackReadComp

#End Region

#End Region

#Region "コンストラクタ"
  Public Sub New(prmSocket As Socket, Optional prmCbReadComp As CallBackReadComp = Nothing)

    '--------------------------------
    '   通信関連オブジェクト初期化
    '--------------------------------
    _Socket = prmSocket
    _NetStream = New NetworkStream(prmSocket)

    '--------------------------------
    '   コールバック処理設定
    '--------------------------------
    _acReadComp = New AsyncCallback(AddressOf ReadComp)
    _acSendComp = New AsyncCallback(AddressOf SendComp)

    If prmCbReadComp IsNot Nothing Then
      _lcReadComp = prmCbReadComp
    End If

    '--------------------------------
    '   受信バッファクリア
    '--------------------------------
    ReDim _ReadBuff(BUFFER_SIZE)

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' 受信処理開始
  ''' </summary>
  ''' <remarks>
  ''' 受信処理完了後は[NetworkStream]の[BeginRead]に設定したコールバック関数が実行される
  ''' </remarks>
  Public Sub StarRead()
    ReDim _ReadBuff(BUFFER_SIZE)
    _NetStream.BeginRead(_ReadBuff, 0, _ReadBuff.Length, _acReadComp, Nothing)
  End Sub

  ''' <summary>
  ''' 受信処理終了時処理
  ''' </summary>
  ''' <param name="prmAa"></param>
  ''' <remarks>
  ''' 受信処理終了時に実行される
  ''' 受信データが存在しない場合は、接続が切断されたと見做す
  ''' </remarks>
  Public Sub ReadComp(prmAa As IAsyncResult)

    Try
      If _Socket.Connected = False _
        OrElse _Socket.EndReceive(prmAa) <= 0 Then
        _Socket.Close()
        If _lcReadComp IsNot Nothing Then
          Call _lcReadComp(Me, Nothing)
        End If
      Else
        If _lcReadComp IsNot Nothing Then
          Call _lcReadComp(Me, _ReadBuff)
        End If
        Call StarRead()
      End If

    Catch ex As Exception

    End Try

  End Sub

  ''' <summary>
  ''' 送信処理開始
  ''' </summary>
  ''' <param name="tmpBuffer">送信データ</param>
  Public Sub StartSend(tmpBuffer() As Byte)
    _NetStream.BeginWrite(tmpBuffer, 0, tmpBuffer.Length, _acSendComp, Nothing)

    ' 送信ログ出力
    Call ComWriteLog(Now().ToString() & " [SEND]" & GetEncoding("UTF-8").GetString(tmpBuffer) _
                               , System.AppDomain.CurrentDomain.BaseDirectory & "\connection.log")
  End Sub

  ''' <summary>
  ''' 送信処理終了時処理
  ''' </summary>
  ''' <param name="prmAa"></param>
  Public Sub SendComp(prmAa As IAsyncResult)
    _NetStream.EndWrite(prmAa)
  End Sub

  Public Sub SetCallBackRoadComp(prmCbReadComp As CallBackReadComp)
    _lcReadComp = prmCbReadComp
  End Sub
#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
      End If

      If _Socket IsNot Nothing Then
        _Socket.Close()
        _Socket = Nothing
      End If

      If _acReadComp IsNot Nothing Then
        _acReadComp = Nothing
      End If

      If _NetStream IsNot Nothing Then
        _NetStream.Close()
        _NetStream = Nothing
      End If

      If _lcReadComp IsNot Nothing Then
        _lcReadComp = Nothing
      End If
    End If
    disposedValue = True
  End Sub

  ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
  Protected Overrides Sub Finalize()
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(False)
    MyBase.Finalize()
  End Sub

  ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
  Public Sub Dispose() Implements IDisposable.Dispose
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(True)
    ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
    GC.SuppressFinalize(Me)
  End Sub
#End Region

#End Region

End Class
