Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstCustomer
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "0000"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("得意先名を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT TKCODE as ItemCode "
      sql &= "      , CONCAT(FORMAT(TKCODE,'" & CODE_FORMAT & "') , ':' , LTKNAME) as ItemName "
      sql &= " FROM TOKUISAKI "
      sql &= " WHERE KUBUN <> 9 "

      If prmCode <> "" Then
        sql &= " AND TKCODE = " & prmCode
      End If

      sql &= " ORDER BY TKCODE"
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
