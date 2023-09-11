Public Class TxtMstCustomer
  Inherits TxtMstBase

  ' 得意先入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("得意先コード", "名称")
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

    sql &= " SELECT TKCODE AS ItemCode "
    sql &= "      , LTKNAME as ItemName "
    sql &= " FROM TOKUISAKI "

    Return sql

  End Function
#End Region

#End Region

End Class
