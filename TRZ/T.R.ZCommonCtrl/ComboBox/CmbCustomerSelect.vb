Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbCustomerSelect
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "0000"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("得意先グループを選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT DISTINCT "
      sql &= "        GROUP_ID as ItemCode "
      sql &= "      , CONCAT(FORMAT(GROUP_ID,'" & CODE_FORMAT & "') , ':' , GROUP_CUSTOMER_NAME)  as ItemName "
      sql &= " FROM MST_CUSTOMER_GROUP "
      sql &= " ORDER BY GROUP_ID "

    End If

    Return sql
  End Function

#End Region

#End Region

End Class
