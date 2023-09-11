Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstStaff
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "0000"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("担当者名を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT TANTOC  as ItemCode "
      sql &= "      , CONCAT(FORMAT(TANTOC,'" & CODE_FORMAT & "') , ':', TANTOMEI)  as ItemName"
      sql &= " FROM TANTO_TBL "

      If prmCode <> "" Then
        sql &= "  WHERE TANTOC = " & prmCode
      End If

      sql &= " ORDER BY TANTOC "
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
