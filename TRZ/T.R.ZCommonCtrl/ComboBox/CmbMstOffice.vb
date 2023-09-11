Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbMstOffice
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "000"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("事業所を選択して下さい。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT SBCODE as ItemCode "
      sql &= "      , CONCAT(FORMAT(事業所コード,'" & CODE_FORMAT & "') , ':' , 事業所名) as ItemName "
      sql &= " FROM MST_OFFICE "

      sql &= " WHERE KUBUN <> 9 "
      If prmCode <> "" Then
        sql &= " AND 事業所コード = " & prmCode
      End If

      sql &= " ORDER BY 事業所コード"
    End If

    Return sql
  End Function

#End Region

#End Region

End Class
