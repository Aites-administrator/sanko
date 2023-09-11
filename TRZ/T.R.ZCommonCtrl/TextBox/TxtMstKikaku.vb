Public Class TxtMstKikaku
  Inherits TxtMstBase

  ' 規格マスタ入力用テキストボックス

#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("規格コード", "規格名")
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

    sql &= " SELECT KICODE AS ItemCode "
    sql &= "      , KKNAME as ItemName "
    sql &= " FROM KIKA "

    Return sql

  End Function
#End Region

#End Region

End Class
