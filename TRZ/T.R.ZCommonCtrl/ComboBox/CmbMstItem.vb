Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstItem
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "0000"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("商品名を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      If (ComChkNumeric(prmCode)) Then

        sql &= " SELECT BICODE AS ItemCode "
        sql &= "      , CONCAT(FORMAT(BICODE,'" & CODE_FORMAT & "') , ':', BINAME) as ItemName "
        sql &= " FROM BUIM "
        If prmCode <> "" Then
          sql &= "  WHERE BICODE = " & prmCode
        End If
        sql &= " ORDER BY BICODE "
      End If
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
