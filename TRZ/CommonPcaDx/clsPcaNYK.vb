Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 仕入伝票操作クラス
''' </summary>
Public Class clsPcaNYK
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PcaApp As New clsCommonPCA()
  Private _PostData As Dictionary(Of String, String)
  Private _NykHeader As New List(Of clsPcaNYKH)
  Private _NykDetailList As New Dictionary(Of clsPcaNYKH, List(Of clsPcaNYKD))
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

      tmpXml &= "<ArrayOfBEInputNYK>"

      For Each tmpNykh As clsPcaNYKH In _NykHeader
        ' 伝票ヘッダー追加
        tmpXml &= "<BEInputNYK>"
        tmpXml &= ComDic2XmlText(_PostData)
        tmpXml &= "<InputNYKH>" & tmpNykh.XML & "</InputNYKH>"

        ' 伝票明細追加
        tmpXml &= "<InputNYKDList>"
        For Each tmpNykd As clsPcaNYKD In _NykDetailList(tmpNykh)
          tmpXml &= "<BEInputNYKD>" & tmpNykd.XML & "</BEInputNYKD>"
        Next
        tmpXml &= "</InputNYKDList>"
        tmpXml &= "</BEInputNYK>"
      Next

      tmpXml &= "</ArrayOfBEInputNYK>"

      Return tmpXml
    End Get
  End Property
#End Region

#Region "パブリック"
  Public ReadOnly Property EntryCount As Long
    Get
      Return _NykHeader.Count
    End Get
  End Property
#End Region
#End Region

#Region "メソッド"
#Region "パブリック"

  ''' <summary>
  ''' 仕入伝票作成
  ''' </summary>
  Public Sub Create()
    Try
      _PcaApp.Create("InputNYK?CalcTotal=True&CalcTax=True&TransactionScope=Whole", Me.XML)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("仕入伝票の作成に失敗しました。")
    Finally
      For Each tmpNYkH As clsPcaNYKH In _NykHeader
        For Each tmpSykD As clsPcaNYKD In _NykDetailList(tmpNYkH)
          tmpSykD.Dispose()
        Next
        tmpNYkH.Dispose()
      Next
      _NykDetailList.Clear()
      _NykHeader.Clear()
    End Try
  End Sub

  ''' <summary>
  ''' 仕入伝票明細行追加
  ''' </summary>
  ''' <param name="prmNykd">仕入伝票明細</param>
  Public Sub AddDetail(prmNykd As clsPcaNYKD)
    _NykDetailList(_NykHeader.Last()).Add(prmNykd)
  End Sub

  ''' <summary>
  ''' 仕入伝票ヘッダー追加
  ''' </summary>
  ''' <param name="prmNykH"></param>
  Public Sub AddHeader(prmNykH As clsPcaNYKH)
    Me._NykHeader.Add(prmNykH)
    Me._NykDetailList.Add(prmNykH, New List(Of clsPcaNYKD))
  End Sub

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

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
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
