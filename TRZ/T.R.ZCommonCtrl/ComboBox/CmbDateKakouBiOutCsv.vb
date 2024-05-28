Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbDateKakouBiOutCsv
  Inherits CmbDateKakouBi

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
    MyBase.SetMsgLabelText("加工日を選択してください。ENTERを押すと更新されます。。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"
  ' コンボボックスソース抽出用
  Public Overloads Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    ' データベースより現在日付を文字列で取得し、DateTime値に変換する
    Dim dt As DateTime = DateTime.Parse(ComGetProcDate())

    sql &= " SELECT DISTINCT KAKOUBI AS ItemCode "
    sql &= " FROM ( "
    sql &= " SELECT Format(KAKOUBI,'" & CODE_FORMAT & "') AS KAKOUBI "
    sql &= " FROM CUTJ "
    sql &= " INNER JOIN MST_CUSTOMER_GROUP "
    sql &= " ON MST_CUSTOMER_GROUP.TKCODE = CUTJ.TKCODE "
    sql &= " WHERE KUBUN = 1 "
    sql &= "   AND ( NSZFLG <= 3 OR NSZFLG = 99 ) "
    sql &= "   AND KIKAINO <> 999 "
    sql &= "   AND TDATE > '" & DateAdd(DateInterval.Month, -13, dt).ToString("yyyy/MM/dd") & "'"

    sql &= " GROUP BY Format(KAKOUBI,'" & CODE_FORMAT & "') "
    sql &= " UNION "
    sql &= " SELECT CONVERT(nvarchar,GETDATE(),111) AS KAKOUBI "
    sql &= " ) KakouBiDate "
    sql &= " ORDER BY KAKOUBI DESC "

    Console.WriteLine(sql)

    Return sql
  End Function
#End Region

#End Region

End Class
