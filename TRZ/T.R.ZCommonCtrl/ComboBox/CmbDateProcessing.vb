Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbDateProcessing
  Inherits CmbDateBase

  Private Const CODE_FORMAT As String = "yyyy/MM/dd"

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()
    ' 選択項目抽出SQL文設定
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    ' 初期化
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("出荷日を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"
  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty


    sql &= " SELECT Format(SYUKKABI,'" & CODE_FORMAT & "') AS ItemCode  "
    sql &= " FROM ("
    sql &= " SELECT SYUKKABI "
    sql &= " FROM CUTJ "
    sql &= " WHERE (NSZFLG = 2 AND DKUBUN = 0) "
    sql &= "   AND SYUKKABI >= dateadd(month,-7,getdate()) "
    sql &= " GROUP BY SYUKKABI "
    sql &= " ) SRC "
    sql &= " ORDER BY SYUKKABI DESC"


    Return sql
  End Function
#End Region

#End Region

End Class
