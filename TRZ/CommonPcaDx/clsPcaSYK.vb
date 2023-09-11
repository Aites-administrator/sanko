Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 売上伝票操作クラス
''' </summary>
Public Class clsPcaSYK
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PcaApp As New clsCommonPCA()
  Private _PostData As Dictionary(Of String, String)
  Private _SykHeader As New List(Of clsPcaSYKH)
  Private _SykDetailList As New Dictionary(Of clsPcaSYKH, List(Of clsPcaSYKD))
#End Region
#End Region

#Region "プロパティー"

#Region "プライベート"

  ''' <summary>
  ''' 登録内容をXML形式で出力
  ''' </summary>
  ''' <returns></returns>
  Private ReadOnly Property XML As String
    Get
      Dim tmpXml As String = String.Empty

      tmpXml &= "<ArrayOfBEInputSYK>"

      For Each tmpSykh As clsPcaSYKH In _SykHeader
        ' 伝票ヘッダー追加
        tmpXml &= "<BEInputSYK>"
        tmpXml &= ComDic2XmlText(_PostData)
        tmpXml &= "<InputSYKH>" & tmpSykh.XML & "</InputSYKH>"

        ' 伝票明細追加
        tmpXml &= "<InputSYKDList>"
        For Each tmpSykd As clsPcaSYKD In _SykDetailList(tmpSykh)
          tmpXml &= "<BEInputSYKD>" & tmpSykd.XML & "</BEInputSYKD>"
        Next
        tmpXml &= "</InputSYKDList>"
        tmpXml &= "</BEInputSYK>"
      Next

      tmpXml &= "</ArrayOfBEInputSYK>"

      Return tmpXml
    End Get
  End Property
#End Region


#End Region

#Region "コンストラクタ"
  Public Sub New(prmUserId As String _
                 , prmPassWord As String _
                 , prmProgramId As String _
                 , prmProgramName As String _
                 , prmDataAreaName As String)
    MyClass.New

    With _PcaApp
      .UserID = prmUserId
      .PassWord = prmPassWord
      .ProgramId = prmProgramId
      .ProgramName = prmProgramName
      .DataAreaName = prmDataAreaName
    End With

  End Sub

  Public Sub New()
    Call ComSetDictionaryVal(_PostData, "BEVersion", "800")
  End Sub
#End Region

#Region "メソッド"
#Region "パブリック"

  ''' <summary>
  ''' 売上伝票作成
  ''' </summary>
  Public Sub Create()
    Try
      _PcaApp.Create("InputSYK?CalcTotal=True&CalcTax=True&TransactionScope=Whole", Me.XML)
    Catch ex As Exception
      Dim msg As String = ex.Message

      ' 改行コードより後ろを取得
      If msg.IndexOf(vbCrLf) > 0 Then
        msg = msg.Substring(msg.IndexOf(vbCrLf) + Len(vbCrLf))
      End If

      ' 改行コードより前を取得
      If msg.IndexOf(vbCrLf) > 0 Then
        msg = msg.Substring(0, msg.IndexOf(vbCrLf) - Len(vbCrLf) + 1)
      End If

      Call ComWriteErrLog(ex)
      Throw New Exception(msg)
    Finally
      For Each tmpSYkH As clsPcaSYKH In _SykHeader
        For Each tmpSykD As clsPcaSYKD In _SykDetailList(tmpSYkH)
          tmpSykD.Dispose()
        Next
        tmpSYkH.Dispose()
      Next
      _SykDetailList.Clear()
      _SykHeader.Clear()
    End Try
  End Sub

  ''' <summary>
  ''' 売上伝票明細行追加
  ''' </summary>
  ''' <param name="prmSykd">売上伝票明細</param>
  Public Sub AddDetail(prmSykd As clsPcaSYKD)
    _SykDetailList(_SykHeader.Last()).Add(prmSykd)
  End Sub

  ''' <summary>
  ''' 売上伝票ヘッダー追加
  ''' </summary>
  ''' <param name="prmSykH"></param>
  Public Sub AddHeader(prmSykH As clsPcaSYKH)
    Me._SykHeader.Add(prmSykH)
    Me._SykDetailList.Add(prmSykH, New List(Of clsPcaSYKD))
  End Sub

#End Region
#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
        If _PcaApp IsNot Nothing Then
          _PcaApp.Disconnect()
          _PcaApp.Dispose()
        End If

        If _SykHeader IsNot Nothing Then
          For Each tmpSYkH As clsPcaSYKH In _SykHeader
            For Each tmpSykD As clsPcaSYKD In _SykDetailList(tmpSYkH)
              tmpSykD.Dispose()
            Next
            tmpSYkH.Dispose()
          Next
          _SykHeader = Nothing
          _SykDetailList = Nothing
        End If

      End If

      ' TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下の Finalize() をオーバーライドします。
      ' TODO: 大きなフィールドを null に設定します。
    End If
    disposedValue = True
  End Sub

  ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
  'Protected Overrides Sub Finalize()
  '    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
  '    Dispose(False)
  '    MyBase.Finalize()
  'End Sub

  ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
  Public Sub Dispose() Implements IDisposable.Dispose
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(True)
    ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
    ' GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class
