Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstHenpin
  Inherits CmbMstBase

  '----------------------------------------------
  '          返品事由入力コンボボックス
  '
  '
  '----------------------------------------------

  Private Const CODE_FORMAT As String = "00"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("返品事由を選択入力して下さい。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT RETURN_CODE as ItemCode "
      sql &= "      , CONCAT(FORMAT(RETURN_CODE,'" & CODE_FORMAT & "') , ':' , RETURN_REASON) as ItemName "
      sql &= " FROM MST_RETURN_TYPE "

      sql &= " WHERE KUBUN <> 9 "
      If prmCode <> "" Then
        sql &= " AND RETURN_CODE = " & prmCode
      End If

      sql &= " ORDER BY RETURN_CODE"
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
