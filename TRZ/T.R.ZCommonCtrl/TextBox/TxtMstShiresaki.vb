Public Class TxtMstShiresaki
  Inherits TxtMstBase

  ' 仕入先マスタ入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("仕入先コード", "仕入先名")
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

    sql &= " SELECT SRCODE AS ItemCode "
    sql &= "      , LSRNAME as ItemName "
    sql &= " FROM CUTSR "

    Return sql

  End Function
#End Region

#End Region

End Class
