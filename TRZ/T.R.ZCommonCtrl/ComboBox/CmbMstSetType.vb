Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstSetType
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "0000"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("セット区分を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT SHCODE as ItemCode "
      sql &= "      , CONCAT(FORMAT(SHCODE,'" & CODE_FORMAT & "') , ':' , HINMEI) as ItemName "
      sql &= " FROM SHOHIN "
      sql &= " WHERE 1=1 "
      If prmCode <> "" Then
        sql &= "  AND SHCODE = " & prmCode
      End If
      sql &= " ORDER BY SHCODE "
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
