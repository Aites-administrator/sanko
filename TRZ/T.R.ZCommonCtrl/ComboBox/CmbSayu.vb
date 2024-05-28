Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData

Public Class CmbSayu
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "00"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("左右を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    If (ComChkNumeric(prmCode)) Then
      sql &= " SELECT ItemCode,ItemName "
      sql &= " FROM ( "
      sql &= " SELECT "
      sql &= PARTS_SIDE_BOTH & " AS ItemCode"
      sql &= ", '1頭' AS ItemName"
      sql &= " UNION "
      sql &= " SELECT "
      sql &= PARTS_SIDE_LEFT & " AS ItemCode"
      sql &= ", '左' AS ItemName"
      sql &= " UNION "
      sql &= " SELECT "
      sql &= PARTS_SIDE_RIGHT & " AS ItemCode"
      sql &= ", '右' AS ItemName"
      sql &= " ) SAYU "

    End If

    Return sql
  End Function

#End Region

#End Region

End Class
