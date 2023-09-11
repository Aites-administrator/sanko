Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstCattle
  Inherits CmbMstBase

  '----------------------------------------------
  '          畜種入力コンボボックス
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
    MyBase.SetMsgLabelText("畜種（種別）を選択して下さい。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT SBCODE as ItemCode "
      sql &= "      , CONCAT(FORMAT(SBCODE,'" & CODE_FORMAT & "') , ':' , SBNAME) as ItemName "
      sql &= " FROM SHUB "

      sql &= " WHERE KUBUN <> 9 "
      If prmCode <> "" Then
        sql &= " AND SBCODE = " & prmCode
      End If

      sql &= " ORDER BY SBCODE"
    End If

    Return sql
  End Function

#End Region

#End Region


End Class
