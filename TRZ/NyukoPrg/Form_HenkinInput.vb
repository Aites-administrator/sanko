Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_HenkinInput

#Region "メンバ"
#Region "プライベート"
  'メインフォームオブジェクト
  Private mfrmMain As Form_NYUKO

  Private _targetData As Dictionary(Of String, String)
#End Region
#End Region

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()

    ' この呼び出しはデザイナーで必要です。
    InitializeComponent()

    ' InitializeComponent() 呼び出しの後で初期化を追加します。

    ' 画面初期化処理追加
    MyBase.lcCallBackInitForm = AddressOf DspData

  End Sub

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  ''' <param name="afrm">メインフォームオブジェクト</param>
  Public Sub New(ByVal afrm As Form_NYUKO)

    ' この呼び出しはデザイナーで必要です。
    InitializeComponent()

    ' InitializeComponent() 呼び出しの後で初期化を追加します。

    ' 画面初期化処理追加
    MyBase.lcCallBackInitForm = AddressOf DspData

    'メインフォームオブジェクトの退避
    Me.mfrmMain = afrm

  End Sub

#End Region


#Region "メソッド"
#Region "プライベート"

  ''' <summary>
  ''' パラメータ画面表示
  ''' </summary>
  ''' <remarks>
  ''' 起動時処理
  ''' </remarks>
  Private Sub DspData(prmTargetData As Dictionary(Of String, String))

    _targetData = prmTargetData

    ' 返金金額デフォルト値に原価を表示
    Me.txtHenkingaku.Text = ComBlank2ZeroText(_targetData("GENKA"))

  End Sub


  ''' <summary>
  ''' 返品処理
  ''' </summary>
  Private Sub ReturnItem()

    Dim tmpDb As New clsSqlServer
    Dim tmpProcTime As String = ComGetProcTime()

    Try

      tmpDb.TrnStart()

      ' 在庫データの作成（単価＝返品単価で作成）
      If 1 <> tmpDb.Execute(SqlInsReturnItem(tmpProcTime, Integer.Parse(Me.txtHenkingaku.Text))) Then
        Throw New Exception("返品処理に失敗しました。他のユーザーによって修正された可能性があります。")
      End If

      ' 元データの更新  （返品日を設定）
      If 1 <> tmpDb.Execute(SqlUpdReturn(tmpProcTime, Integer.Parse(Me.txtHenkingaku.Text))) Then
        Throw New Exception("返品処理に失敗しました。他のユーザーによって修正された可能性があります。")
      End If

      tmpDb.TrnCommit()

    Catch ex As Exception
      Call ComWriteErrLog(ex)

      tmpDb.TrnRollBack()

      Throw New Exception("返品処理に失敗しました。")
    Finally
      tmpDb.DbDisconnect()
    End Try

  End Sub

#End Region
#End Region

#Region "SQL文作成関連"

  ''' <summary>
  ''' 返品データ作成用SQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">更新日付</param>
  ''' <param name="prmHenpinKingaku">返品金額</param>
  ''' <returns></returns>
  ''' <remarks>
  ''' 返品金額を単価とした在庫データを作成
  ''' </remarks>
  Private Function SqlInsReturnItem(prmProcTime As String _
                                  , prmHenpinKingaku As Integer) As String
    Dim sql As String = String.Empty
    Dim tmpKeyVal As Dictionary(Of String, String) = TargetValExtractionCutJVal()
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    ' 更新項目を修正
    tmpKeyVal("KDATE") = "'" & prmProcTime & "'"
    tmpKeyVal("NSZFLG") = "8"
    tmpKeyVal("HONSU") = ComBlank2ZeroText(Me.mfrmMain.TxtSyukoCount.Text)
    tmpKeyVal("JYURYO") = (Decimal.Parse(ComBlank2ZeroText(Me.mfrmMain.TxtWeitghtKg1.Text)) * 1000).ToString
    tmpKeyVal("GENKA") = ComBlank2ZeroText(Me.mfrmMain.TxtUnitPrice.Text)
    tmpKeyVal("HTANKA") = prmHenpinKingaku.ToString
    tmpKeyVal("GYONO") = "9999"

    tmpKeyVal("BAIKA") = "0"

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst = tmpDst & tmpKey & " ,"
      tmpVal = tmpVal & tmpKeyVal(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= "INSERT INTO CUTJ(" & tmpDst & ")"
    sql &= "          VALUES(" & tmpVal & ")"

    Return sql

  End Function

  ''' <summary>
  ''' 返品データ作成用SQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">処理日時</param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 出庫データを返品として登録
  ''' </remarks>
  Private Function SqlUpdReturn(prmProcTime As String _
                              , prmHenpinKingaku As Integer) As String
    Dim sql As String = String.Empty


    sql &= " UPDATE CUTJ "
    sql &= " SET HENPINBI = '" & Date.Parse(prmProcTime).ToString("yyyy/MM/dd") & "' "
    sql &= "    ,NSZFLG = 8"
    sql &= "    ,SIRIALNO = " & (ComBlank2Zero(_targetData("SIRIALNO")) + 1).ToString()
    sql &= "    ,HONSU =  " & (ComBlank2Zero(Me.mfrmMain.TxtSyukoCount.Text) * -1).ToString
    sql &= "    ,JYURYO = " & (Decimal.Parse(ComBlank2ZeroText(Me.mfrmMain.TxtWeitghtKg1.Text)) * 1000 * -1).ToString
    sql &= "    ,GENKA = " & prmHenpinKingaku.ToString
    sql &= "    ,HTANKA = " & prmHenpinKingaku.ToString
    sql &= "    ,SYUKKABI = NULL "
    sql &= "    ,NGYONO = 0"
    sql &= "    ,NKUBUN = 1"
    sql &= "    ,NDENNO = 0"
    sql &= "    ,GYONO = 9999"
    sql &= "    ,TDATE = '" & prmProcTime & "'"
    sql &= "    ,KDATE = '" & prmProcTime & "'"
    sql &= "    ,TANTO = " & ComNothing2ZeroText(Me.mfrmMain.CmbMstStaff1.SelectedValue)

    sql &= " WHERE TDATE = '" & _targetData("TDATE") & "'"
    sql &= "   AND KUBUN = " & _targetData("KUBUN")
    sql &= "   AND KIKAINO = " & _targetData("KIKAINO")
    sql &= "   AND SIRIALNO = " & _targetData("SIRIALNO")
    sql &= "   AND KYOKUFLG = " & _targetData("KYOKUFLG")
    sql &= "   AND BICODE = " & _targetData("BICODE")
    sql &= "   AND TOOSINO =" & _targetData("TOOSINO")
    sql &= "   AND EBCODE =" & _targetData("EBCODE")
    sql &= "   AND NSZFLG =" & _targetData("NSZFLG")
    sql &= "   AND KDATE ='" & _targetData("KDATE") & "'"

    Return sql

  End Function

  ''' <summary>
  ''' 親フォームより渡された操作対象データからCUTJ項目抽出
  ''' </summary>
  ''' <returns>CUTJ項目のみ設定した連想配列</returns>
  Private Function TargetValExtractionCutJVal() As Dictionary(Of String, String)

    Dim ret As New Dictionary(Of String, String)

    With ret

      ' 日付形式データ
      ' 空白は"NULL"に置き換え
      ' 空白以外はシングルコード(')で囲む
      Dim tmpKeyName As String = String.Empty

      tmpKeyName = "KAKOUBI"
      If _targetData(tmpKeyName) = "" Then
        .Add(tmpKeyName, ComBlank2NullText(_targetData(tmpKeyName)))
      Else
        .Add(tmpKeyName, "'" & _targetData(tmpKeyName) & "'")
      End If

      tmpKeyName = "KEIRYOBI"
      If _targetData(tmpKeyName) = "" Then
        .Add(tmpKeyName, ComBlank2NullText(_targetData(tmpKeyName)))
      Else
        .Add(tmpKeyName, "'" & _targetData(tmpKeyName) & "'")
      End If

      tmpKeyName = "SYUKKABI"
      If _targetData(tmpKeyName) = "" Then
        .Add(tmpKeyName, ComBlank2NullText(_targetData(tmpKeyName)))
      Else
        .Add(tmpKeyName, "'" & _targetData(tmpKeyName) & "'")
      End If

      tmpKeyName = "HENPINBI"
      If _targetData(tmpKeyName) = "" Then
        .Add(tmpKeyName, ComBlank2NullText(_targetData(tmpKeyName)))
      Else
        .Add(tmpKeyName, "'" & _targetData(tmpKeyName) & "'")
      End If

      tmpKeyName = "TDATE"
      If _targetData(tmpKeyName) = "" Then
        .Add(tmpKeyName, ComBlank2NullText(_targetData(tmpKeyName)))
      Else
        .Add(tmpKeyName, "'" & _targetData(tmpKeyName) & "'")
      End If

      tmpKeyName = "KDATE"
      If _targetData(tmpKeyName) = "" Then
        .Add(tmpKeyName, ComBlank2NullText(_targetData(tmpKeyName)))
      Else
        .Add(tmpKeyName, "'" & _targetData(tmpKeyName) & "'")
      End If


      ' 数値形式データ
      '  空白は"NULL"に置き換え
      .Add("KUBUN", ComBlank2NullText(_targetData("KUBUN")))
      .Add("KIKAINO", ComBlank2NullText(_targetData("KIKAINO")))
      .Add("SIRIALNO", ComBlank2NullText(_targetData("SIRIALNO")))
      .Add("KYOKUFLG", ComBlank2NullText(_targetData("KYOKUFLG")))
      .Add("TKCODE", ComBlank2NullText(_targetData("TKCODE")))
      .Add("KAKUC", ComBlank2NullText(_targetData("KAKUC")))
      .Add("EBCODE", ComBlank2NullText(_targetData("EBCODE")))
      .Add("HONSU", ComBlank2NullText(_targetData("HONSU")))
      .Add("SAYUKUBUN", ComBlank2NullText(_targetData("SAYUKUBUN")))
      .Add("SPKUBUN", ComBlank2NullText(_targetData("SPKUBUN")))
      .Add("KIKAKUC", ComBlank2NullText(_targetData("KIKAKUC")))
      .Add("BICODE", ComBlank2NullText(_targetData("BICODE")))
      .Add("JYURYO", ComBlank2NullText(_targetData("JYURYO")))
      .Add("GBFLG", ComBlank2NullText(_targetData("GBFLG")))
      .Add("TOOSINO", ComBlank2NullText(_targetData("TOOSINO")))
      .Add("BLOCKCODE", ComBlank2NullText(_targetData("BLOCKCODE")))
      .Add("KINGAKU", ComBlank2NullText(_targetData("KINGAKU")))
      .Add("KIGENBI", ComBlank2NullText(_targetData("KIGENBI")))
      .Add("SYUBETUC", ComBlank2NullText(_targetData("SYUBETUC")))
      .Add("KOTAINO", ComBlank2NullText(_targetData("KOTAINO")))
      .Add("YOBI", ComBlank2NullText(_targetData("YOBI")))
      .Add("SHOHINC", ComBlank2NullText(_targetData("SHOHINC")))
      .Add("LABELJI", ComBlank2NullText(_targetData("LABELJI")))
      .Add("GENSANCHIC", ComBlank2NullText(_targetData("GENSANCHIC")))
      .Add("COMMENTC", ComBlank2NullText(_targetData("COMMENTC")))
      .Add("SRCODE", ComBlank2NullText(_targetData("SRCODE")))
      .Add("TJCODE", ComBlank2NullText(_targetData("TJCODE")))
      .Add("KFLG", ComBlank2NullText(_targetData("KFLG")))
      .Add("BUDOMARI", ComBlank2NullText(_targetData("BUDOMARI")))
      .Add("OLDTKC", ComBlank2NullText(_targetData("OLDTKC")))
      .Add("UTKCODE", ComBlank2NullText(_targetData("UTKCODE")))
      .Add("TANKA", ComBlank2NullText(_targetData("TANKA")))
      .Add("BAIKA", ComBlank2NullText(_targetData("BAIKA")))
      .Add("DENNO", ComBlank2NullText(_targetData("DENNO")))
      .Add("GYONO", ComBlank2NullText(_targetData("GYONO")))
      .Add("TANTO", ComBlank2NullText(_targetData("TANTO")))
      .Add("NSZFLG", ComBlank2NullText(_targetData("NSZFLG")))
      .Add("SETCD", ComBlank2NullText(_targetData("SETCD")))
      .Add("GENKA", ComBlank2NullText(_targetData("GENKA")))
      .Add("DKUBUN", ComBlank2NullText(_targetData("DKUBUN")))
      .Add("NDENNO", ComBlank2NullText(_targetData("NDENNO")))
      .Add("NGYONO", ComBlank2NullText(_targetData("NGYONO")))
      .Add("NKUBUN", ComBlank2NullText(_targetData("NKUBUN")))
      .Add("HTANKA", ComBlank2NullText(_targetData("HTANKA")))
    End With

    Return ret
  End Function

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' OKボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click

    Try
      ' 返品処理
      Call ReturnItem()
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("返品処理に失敗しました")
    Finally
      Me.Close()
    End Try

  End Sub

  ''' <summary>
  ''' キャンセルボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
    Me.Close()
  End Sub

  ''' <summary>
  ''' 画面起動時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_HenkinInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = "返品単価入力"

    ' キャンセルボタンにフォーカス設定
    Me.ActiveControl = Me.btnCancel

  End Sub

#End Region

End Class
