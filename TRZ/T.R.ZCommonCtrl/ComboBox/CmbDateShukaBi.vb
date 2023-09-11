Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbDateShukaBi
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
    MyBase.SetMsgLabelText("出荷日付を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"
  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    ' データベースより現在日付を文字列で取得し、DateTime値に変換する
    Dim dt As DateTime = DateTime.Parse(ComGetProcDate())

    sql &= " SELECT Format(SYUKKABI,'" & CODE_FORMAT & "') AS ItemCode  "
    sql &= " FROM CUTJ "
    sql &= " WHERE NSZFLG = 2 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND SYUKKABI >= '" & DateAdd(DateInterval.Month, -7, dt).ToString("yyyy/MM/dd") & "'"

    sql &= " GROUP BY Format(SYUKKABI,'" & CODE_FORMAT & "') "
    sql &= " ORDER BY Format(SYUKKABI,'" & CODE_FORMAT & "') DESC"

    Console.WriteLine(sql)

    Return sql
  End Function
#End Region

#End Region

End Class
