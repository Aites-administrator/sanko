Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 仕入伝票ヘッダークラス
''' </summary>
Public Class clsPcaNYKH
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PostData As New Dictionary(Of String, String)
#End Region
#End Region

#Region "プロパティー"
#Region "パブリック"

  ''' <summary>
  ''' 設定データをXML形式で出力
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property XML As String
    Get
      Return ComDic2XmlText(_PostData)
    End Get
  End Property

  ''' <summary>
  ''' 伝票番号
  ''' </summary>
  Public WriteOnly Property 伝票番号 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "DenpyoNo", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入科目
  ''' </summary>
  Public WriteOnly Property 仕入科目 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireKamoku", value)
    End Set
  End Property

  ''' <summary>
  ''' 伝区
  ''' </summary>
  Public WriteOnly Property 伝区 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Denku", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入日
  ''' </summary>
  Public WriteOnly Property 仕入日 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Shirebi", value)
    End Set
  End Property

  ''' <summary>
  ''' 精算日
  ''' </summary>
  Public WriteOnly Property 精算日 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Seisanbi", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入先コード 
  ''' </summary>
  Public WriteOnly Property 仕入先コード As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShiresakiCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 部門コード 
  ''' </summary>
  Public WriteOnly Property 部門コード As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "BumonCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 担当者コード 
  ''' </summary>
  Public WriteOnly Property 担当者コード As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "TantosyaCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 摘要コード 
  ''' </summary>
  Public WriteOnly Property 摘要コード As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "TekiyoCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 摘要名 
  ''' </summary>
  Public WriteOnly Property 摘要名 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Tekiyo", value)
    End Set
  End Property

#End Region

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
