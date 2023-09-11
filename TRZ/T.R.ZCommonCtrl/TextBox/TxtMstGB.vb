Public Class TxtMstGB
  Inherits TxtMstBase

  ' 牛豚フラグ入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("牛豚コード", "名称")
    MyBase.MaxLength = 1
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

    sql &= " SELECT GBCODE AS ItemCode "
    sql &= "      , GBNAME as ItemName "
    sql &= " FROM GBFLG_TBL "

    Return sql

  End Function
#End Region

#End Region


End Class
