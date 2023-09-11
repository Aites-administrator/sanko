Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstKikaku
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "0000"
  Private Const CODE_FORMAT_2KETA As String = "00"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("規格を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      If (ComChkNumeric(prmCode)) Then

        sql &= " SELECT KICODE AS ItemCode "
        sql &= "      , CONCAT(FORMAT(KICODE,'" & CODE_FORMAT_2KETA & "') , ':', KKNAME) as ItemName "
        sql &= " FROM KIKA "
        If prmCode <> "" Then
          sql &= "  WHERE KICODE = " & prmCode
        End If
        sql &= " ORDER BY KICODE "
      End If
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
