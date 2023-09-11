Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstShiresaki
  Inherits CmbMstBase

  '----------------------------------------------
  '          仕入先入力コンボボックス
  '
  '
  '----------------------------------------------

  Private Const CODE_FORMAT As String = "0000"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("仕入先名を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT SRCODE as ItemCode "
      sql &= "      , CONCAT(FORMAT(SRCODE,'" & CODE_FORMAT & "') , ':' , LSRNAME) as ItemName "
      sql &= " FROM CUTSR "

      sql &= " WHERE KUBUN <> 9 "
      If prmCode <> "" Then
        sql &= " AND SRCODE = " & prmCode
      End If

      sql &= " ORDER BY SRCODE"
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
