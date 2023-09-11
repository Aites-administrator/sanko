Public Class TxtMstItem
  Inherits TxtMstBase

  ' 部位マスタ入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("部位コード", "部位名")
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

    sql &= " SELECT BICODE AS ItemCode "
    sql &= "      , BINAME as ItemName "
    sql &= " FROM BUIM "

    Return sql

  End Function
#End Region

#End Region

End Class
