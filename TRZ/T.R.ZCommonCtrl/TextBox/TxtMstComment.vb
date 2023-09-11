Public Class TxtMstComment
  Inherits TxtMstBase

  ' コメントコード入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("コメントコード", "名称")
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

    sql &= " SELECT CMCODE AS ItemCode "
    sql &= "      , CMNAME as ItemName "
    sql &= " FROM COMNT "

    Return sql

  End Function
#End Region

#End Region

End Class
