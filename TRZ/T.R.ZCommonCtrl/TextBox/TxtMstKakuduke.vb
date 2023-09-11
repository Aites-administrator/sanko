Public Class TxtMstKakuduke
  Inherits TxtMstBase

  ' 格付マスタ入力用テキストボックス


#Region "コンストラクタ"

  Public Sub New()
    MyBase.New("格付コード", "格付名")
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

    sql &= " SELECT KKCODE AS ItemCode "
    sql &= "      , KZNAME as ItemName "
    sql &= " FROM KAKU "

    Return sql

  End Function
#End Region

#End Region

End Class
