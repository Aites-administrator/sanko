Public Class TxtMstStaff
  Inherits TxtMstBase

  ' 担当者マスタ入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("担当者コード", "担当者名")
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

    sql &= " SELECT TANTOC  as ItemCode "
    sql &= "      , TANTOMEI  as ItemName "
    sql &= " FROM TANTO_TBL "

    Return sql

  End Function
#End Region

#End Region

End Class
