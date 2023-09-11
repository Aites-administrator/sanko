Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbDateTanaorosi
  Inherits CmbDateBase

  Private Const CODE_FORMAT As String = "yyyy/MM/dd"

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()
    ' 選択項目抽出SQL文設定
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    ' 初期化
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("棚卸日を選択してください。ENTERを押すと更新されます。。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"
  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT Format(OROSIBI,'" & CODE_FORMAT & "') AS ItemCode  "
    sql &= " FROM TANAOROSI "
    sql &= " GROUP BY OROSIBI "
    sql &= " ORDER BY OROSIBI DESC; "

    Return sql
  End Function
#End Region

#End Region

End Class
