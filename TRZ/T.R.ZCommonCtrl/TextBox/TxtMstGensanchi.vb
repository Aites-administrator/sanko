Public Class TxtMstGensanchi
  Inherits TxtMstBase

  ' 原産地マスタ入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("原産地コード", "原産地名")
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

    sql &= " SELECT GNCODE AS ItemCode "
    sql &= "      , GNNAME as ItemName "
    sql &= " FROM GENSN "

    Return sql

  End Function
#End Region

#End Region

End Class
