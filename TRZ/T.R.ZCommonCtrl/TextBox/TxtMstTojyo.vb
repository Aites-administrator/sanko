Public Class TxtMstTojyo
  Inherits TxtMstBase

  ' 屠場マスタ入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("屠場コード", "屠場名")
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

    sql &= " SELECT TJCODE AS ItemCode "
    sql &= "      , TJNAME as ItemName "
    sql &= " FROM TOJM "

    Return sql

  End Function
#End Region

#End Region

End Class
