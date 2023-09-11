Public Class TxtMstSyubetsu
  Inherits TxtMstBase

  ' 種別マスタ入力用テキストボックス

#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("種別コード", "種別名")
    MyBase.MaxLength = 4
    MyBase.lcCallBackCreateGridSrcSql = AddressOf CreateGridSrc
  End Sub
#End Region


#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' 一覧抽出用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGridSrc() As String
    Dim sql As String = String.Empty

    sql &= " SELECT SBCODE AS ItemCode "
    sql &= "      , SBNAME as ItemName "
    sql &= " FROM SHUB "

    Return sql

  End Function
#End Region

#End Region

End Class
