Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 仕入伝票明細操作クラス
''' </summary>
Public Class clsPcaNYKD
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
  ''' 商品コード
  ''' </summary>
  Public WriteOnly Property 商品コード As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinCode", value)
    End Set
  End Property

  ''' <summary>
  ''' マスター区分
  ''' </summary>
  Public WriteOnly Property マスター区分 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "MasterKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 税区分
  ''' </summary>
  Public WriteOnly Property 税区分 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ZeiKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 税込区分
  ''' </summary>
  Public WriteOnly Property 税込区分 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ZeikomiKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 数量小数桁
  ''' </summary>
  Public WriteOnly Property 数量小数桁 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SuryoKeta", value)
    End Set
  End Property

  ''' <summary>
  ''' 品名
  ''' </summary>
  Public WriteOnly Property 品名 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinMei", value)
    End Set
  End Property

  ''' <summary>
  ''' 規格型番
  ''' </summary>
  Public WriteOnly Property 規格型番 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "KikakuKataban", value)
    End Set
  End Property

  ''' <summary>
  ''' 色
  ''' </summary>
  Public WriteOnly Property 色 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Color", value)
    End Set
  End Property

  ''' <summary>
  ''' サイズ
  ''' </summary>
  Public WriteOnly Property サイズ As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Size", value)
    End Set
  End Property

  ''' <summary>
  ''' 倉庫
  ''' </summary>
  Public WriteOnly Property 倉庫 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SokoCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 区
  ''' </summary>
  Public WriteOnly Property 区 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Ku", value)
    End Set
  End Property

  ''' <summary>
  ''' 入数
  ''' </summary>
  Public WriteOnly Property 入数 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Irisu", value)
    End Set
  End Property

  ''' <summary>
  ''' 箱数
  ''' </summary>
  Public WriteOnly Property 箱数 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Hakosu", value)
    End Set
  End Property

  ''' <summary>
  ''' 数量
  ''' </summary>
  Public WriteOnly Property 数量 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Suryo", value)
    End Set
  End Property

  ''' <summary>
  ''' 単位
  ''' </summary>
  Public WriteOnly Property 単位 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Tani", value)
    End Set
  End Property

  ''' <summary>
  ''' 単価
  ''' </summary>
  Public WriteOnly Property 単価 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Tanka", value)
    End Set
  End Property

  ''' <summary>
  ''' 金額
  ''' </summary>
  Public WriteOnly Property 金額 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Kingaku", value)
    End Set
  End Property

  ''' <summary>
  ''' 備考
  ''' </summary>
  Public WriteOnly Property 備考 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Biko", value)
    End Set
  End Property

  ''' <summary>
  ''' 税率
  ''' </summary>
  Public WriteOnly Property 税率 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ZeiRitsu", value)
    End Set
  End Property

  ''' <summary>
  ''' 外税額
  ''' </summary>
  Public WriteOnly Property 外税額 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SotoZeigaku", value)
    End Set
  End Property

  ''' <summary>
  ''' 内税額
  ''' </summary>
  Public WriteOnly Property 内税額 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "UchiZeigaku", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品項目1 
  ''' </summary>
  Public WriteOnly Property 商品項目1 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKomoku1", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品項目2
  ''' </summary>
  Public WriteOnly Property 商品項目2 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKomoku2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品項目3
  ''' </summary>
  Public WriteOnly Property 商品項目3 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKomoku3", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入項目1
  ''' </summary>
  Public WriteOnly Property 仕入項目1 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireKomoku1", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入項目2
  ''' </summary>
  Public WriteOnly Property 仕入項目2 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireKomoku2", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入項目3
  ''' </summary>
  Public WriteOnly Property 仕入項目3 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireKomoku3", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入税種別
  ''' </summary>
  Public WriteOnly Property 仕入税種別 As String
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireZeiSyubetsu", value)
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
