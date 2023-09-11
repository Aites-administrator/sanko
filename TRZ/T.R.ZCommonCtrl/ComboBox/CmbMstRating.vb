Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstRating
  Inherits CmbMstBase

  '----------------------------------------------
  '          原産地入力コンボボックス
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
    MyBase.SetMsgLabelText("格付を選択入力して下さい。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT KKCODE as ItemCode "
      sql &= "      , CONCAT(FORMAT(KKCODE,'" & CODE_FORMAT & "') , ':' , KZNAME) as ItemName "
      sql &= " FROM KAKU "

      sql &= " WHERE KUBUN <> 9 "
      If prmCode <> "" Then
        sql &= " AND KKCODE = " & prmCode
      End If

      sql &= " ORDER BY KKCODE"
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
