Public Class TxtMstBlockCode
  Inherits TxtMstBase

  ' ブロックコード入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("ブロックコード", "名称")
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

    sql &= " SELECT BLOCKCODE AS ItemCode "
    sql &= "      , BLNAME as ItemName "
    sql &= " FROM BLOCK_TBL "

    Return sql

  End Function
#End Region

#End Region

End Class
