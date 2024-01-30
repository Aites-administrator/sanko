Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports T.R.ZCommonClass.clsDataGridSearchControl

Public Class Form_NYUKO
  Implements IDgvForm02

  '----------------------------------------------
  '          入庫入力画面
  '
  '
  '----------------------------------------------
#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "nyuko"
  Private Const PRG_TITLE As String = "入庫処理"
  Private _targetData As Dictionary(Of String, String)
#End Region
#End Region

#Region "メンバ"

#Region "プライベート"

  ''' <summary>
  ''' Gridより選択されたデータ
  ''' </summary>
  ''' <remarks>
  ''' ダブルクリック・Enter入力時に編集対象として保持する
  ''' </remarks>
  Private _SelectedData As New Dictionary(Of String, String)
  ' データグリッドのフォーカス喪失時の位置
  Private rowIndexDataGrid As Integer
  ' データグリッドの選択件数
  Private dataGridCount As Long
  ' データグリッドの検索日付
  Private dataGridDate As String
  ' 個体識別番号入力チェックキャンセル判定
  Private TxtKotaiNo1CancelFlg As Boolean
#End Region

#End Region

#Region "データグリッドビュー２つの画面用操作関連共通"
  '１つ目のDataGridViewオブジェクト格納先
  Private DG2V1 As DataGridView

  ''' <summary>
  ''' 初期化処理
  ''' </summary>
  ''' <remarks>
  ''' コントロールの初期化（Form_Loadで実行して下さい）
  ''' </remarks>
  Private Sub InitForm02() Implements IDgvForm02.InitForm02

    ' データグリッドのフォーカス喪失時の位置初期化
    rowIndexDataGrid = -1

    '入庫明細オブジェクトの設定
    DG2V1 = Me.DataGridView1

    ' グリッド初期化
    Call InitGrid(DG2V1, CreateGrid2Src1(), CreateGrid2Layout1())

    ' 入庫明細設定
    With DG2V1

      '表示する
      .Visible = True

      ' グリッド動作設定
      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 4

        ' 検索コントロール設定
        .AddSearchControl(Me.TxtNyukoDate, "CUTJ.KEIRYOBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.CmbMstCustomer1, "CUTJ.SRCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtKotaiNo1, "CUTJ.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtEdaban1, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' 編集可能列設定
        .EditColumnList = CreateGrid2EditCol1()

      End With
    End With

  End Sub

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   入庫明細抽出用
  ''' </remarks>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1

    Dim sql As String = String.Empty

    sql &= CreateSqlBase()
    sql &= " WHERE CUTJ.KYOKUFLG = 0 "
    sql &= "   AND CUTJ.KUBUN <> 9  "
    sql &= "   AND CUTJ.DKUBUN = 0 "
    sql &= "   AND ISDATE(CUTJ.HENPINBI) <> 1 "

    sql &= " ORDER BY CUTJ.SRCODE"
    sql &= "         ,CUTJ.KOTAINO"
    sql &= "         ,CUTJ.KAKOUBI"
    sql &= "         ,CUTJ.SAYUKUBUN"
    sql &= "         ,CUTJ.BICODE"
    sql &= "         ,CUTJ.TOOSINO"
    sql &= "         ,CUTJ.SIRIALNO DESC"

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' 入庫明細DataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1

    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("状態", "JYOUTAI", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("個体識別", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("枝番", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("仕入先", "DLSRNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("加工日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("ｶｰﾄﾝNo", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("部位", "DBINAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("左右", "LR2", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argFormat:="###0.0", argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("格付", "DKZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("規格", "DKKNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("原単価", "GENKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("入庫日", "KEIRYOBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("売単価", "BAIKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("種別", "DSBNAME", argTextAlignment:=typAlignment.MiddleLeft))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret

  End Function

  ''' <summary>
  ''' 入庫明細のDataGridViewオブジェクトの一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  ''' <remarks>編集列はありません</remarks>
  Public Function CreateGrid2EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol1
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
      .Add(New clsDataGridEditTextBox("原単価", prmUpdateFnc:=AddressOf UpDateDbFromList, prmValueType:=VT_NUMBER, prmMaxChar:=6))
      .Add(New clsDataGridEditTextBox("売単価", prmUpdateFnc:=AddressOf UpDateDbFromList, prmValueType:=VT_NUMBER, prmMaxChar:=6))

    End With

    Return ret
  End Function

  Public Function CreateGrid2Src2() As String Implements IDgvForm02.CreateGrid2Src2
    Throw New NotImplementedException()
  End Function

  Public Function CreateGrid2layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Throw New NotImplementedException()
  End Function

  Public Function CreateGrid2EditCol2() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol2
    Throw New NotImplementedException()
  End Function

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_NYUKO, AddressOf ComRedisplay)
  End Sub
#End Region

#Region "メソッド"

#Region "プライベート"

#Region "SQL文作成"

  ''' <summary>
  ''' 一覧表示用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 抽出条件、並び順を変更し[入庫明細][在庫一覧]で共通で使用
  ''' </remarks>
  Private Function CreateSqlBase() As String
    Dim sql As String = String.Empty

    sql &= " SELECT CUTJ.* "
    sql &= "      , CONCAT(FORMAT(CUTJ.UTKCODE,'0000') , ':' , UTK.LTKNAME)  AS DLTKNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.KAKUC,'00') , ':' , KZNAME)  AS DKZNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.KIKAKUC,'00') , ':' , KKNAME)  AS DKKNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.BICODE,'0000') , ':' , BINAME)  AS DBINAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.BLOCKCODE,'00') , ':' , BLNAME)  AS DBLNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.GBFLG,'00') , ':' , GBNAME)  AS DGBNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.GENSANCHIC,'00') , ':' , GNNAME)  AS DGNNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.SYUBETUC,'00') , ':' , SBNAME)  AS DSBNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.COMMENTC,'00') , ':' , CMNAME)  AS DCMNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.SRCODE,'0000') , ':' , LSRNAME)  AS DLSRNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.TJCODE,'00') , ':' , TJNAME)  AS DTJNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.TANTO,'0000') , ':' , TANTOMEI)  AS DTANTOMEI "
    sql &= "      , (ROUND((CUTJ.JYURYO / 100), 0, 1) / 10) AS JYURYOK"
    sql &= "      , CASE NSZFLG "
    sql &= "         WHEN 0 THEN '在庫' "
    sql &= "         WHEN 1 THEN '返品' "
    sql &= "         WHEN 2 THEN '出庫' "
    sql &= "         WHEN 3 THEN '廃棄' "
    sql &= "         WHEN 4 THEN '不明' "
    sql &= "         WHEN 5 THEN '取消' "
    sql &= "         WHEN 8 THEN 'Ｍ返品' "
    sql &= "         ELSE '枝入庫'"
    sql &= "        END AS JYOUTAI "
    sql &= "      , CASE SAYUKUBUN "
    sql &= "         WHEN 1 THEN '左' "
    sql &= "         WHEN 2 THEN '右' "
    sql &= "         ELSE ' '"
    sql &= "        END AS LR2 "
    sql &= "  FROM (((((((((((CUTJ LEFT JOIN TOKUISAKI AS UTK ON CUTJ.UTKCODE = UTK.TKCODE) "
    sql &= "                       LEFT JOIN KAKU ON CUTJ.KAKUC = KAKU.KKCODE) "
    sql &= "                       LEFT JOIN KIKA ON CUTJ.KIKAKUC = KIKA.KICODE) "
    sql &= "                       LEFT JOIN BUIM ON CUTJ.BICODE = BUIM.BICODE) "
    sql &= "                       LEFT JOIN BLOCK_TBL ON CUTJ.BLOCKCODE = BLOCK_TBL.BLOCKCODE) "
    sql &= "                       LEFT JOIN GBFLG_TBL ON CUTJ.GBFLG = GBFLG_TBL.GBCODE) "
    sql &= "                       LEFT JOIN GENSN ON CUTJ.GENSANCHIC = GENSN.GNCODE) "
    sql &= "                       LEFT JOIN SHUB ON CUTJ.SYUBETUC = SHUB.SBCODE) "
    sql &= "                       LEFT JOIN COMNT ON CUTJ.COMMENTC = COMNT.CMCODE) "
    sql &= "                       LEFT JOIN CUTSR ON CUTJ.SRCODE = CUTSR.SRCODE) "
    sql &= "                       LEFT JOIN TOJM ON CUTJ.TJCODE = TOJM.TJCODE) "
    sql &= "                       LEFT JOIN TANTO_TBL ON CUTJ.TANTO = TANTO_TBL.TANTOC "

    Return sql

  End Function


  ''' <summary>
  ''' 部位マスタ読込用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 部位コードを指定した部位マスタ情報を取得する
  ''' </remarks>
  Private Function SqlReadBuim(buiCode As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT GBFLG, BLOCKCODE, SHOHINC, COMMENTC, BUDOMARI, SHOMINISU FROM BUIM"
    sql &= " WHERE KUBUN <> 9 "
    sql &= "   AND BICODE =" & buiCode

    Return sql

  End Function

  ''' <summary>
  ''' カット肉加工実績データ更新テーブル挿入SQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">処理日時</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdData(prmProcTime As String) As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET SRCODE = " & ComNothing2ZeroText(CmbMstCustomer1.SelectedValue)
    sql &= "    ,SYUBETUC = " & ComNothing2ZeroText(CmbMstCattle1.SelectedValue)
    sql &= "    ,GENSANCHIC = " & ComNothing2ZeroText(CmbMstOriginPlace1.SelectedValue)
    sql &= "    ,KAKUC =  " & ComNothing2ZeroText(CmbMstRating1.SelectedValue)
    sql &= "    ,KIKAKUC = " & ComNothing2ZeroText(CmbMstKikaku1.SelectedValue)
    sql &= "    ,EBCODE = " & ComBlank2ZeroText(TxtEdaban1.Text)
    sql &= "    ,KOTAINO = " & ComBlank2ZeroText(TxtKotaiNo1.Text)

    If IsDate(TxtKakouDate.Text) Then
      sql &= "    ,KAKOUBI = " & "'" & TxtKakouDate.Text & "'"
    Else
      sql &= "    ,KAKOUBI = NULL "
    End If

    Dim limitDays As Long = 0

    ' 部位コードが変更された場合
    If Me.TxtLblMstItem1.CodeTxt.Equals(TxtBuiCode1.Text) = False Then

      sql &= "    ,BICODE = " & ComBlank2ZeroText(Me.TxtLblMstItem1.CodeTxt)

      Dim tmpDb As New clsSqlServer
      Dim sqlBuim As String = String.Empty

      ' SQL文の作成
      sqlBuim = SqlReadBuim(Me.TxtLblMstItem1.CodeTxt)

      Dim tmpDt As New DataTable
      Call tmpDb.GetResult(tmpDt, sqlBuim)

      If (0 >= tmpDt.Rows.Count) Then
        ' 部位マスタが存在しない場合は期限日は30日
        limitDays = 30
      Else

        Dim dtRow As DataRow
        dtRow = tmpDt.Rows(0)

        sql &= "    ,GBFLG = " & ComNothing2ZeroText(dtRow("GBFLG").ToString)
        sql &= "    ,BLOCKCODE = " & ComNothing2ZeroText(dtRow("BLOCKCODE").ToString)
        sql &= "    ,SHOHINC = " & ComNothing2ZeroText(dtRow("SHOHINC").ToString)
        sql &= "    ,COMMENTC = " & ComNothing2ZeroText(dtRow("COMMENTC").ToString)
        sql &= "    ,BUDOMARI = " & ComNothing2ZeroText(dtRow("BUDOMARI").ToString)
        limitDays = ComBlank2Zero(dtRow("SHOMINISU").ToString)
      End If

    End If

    Dim dt As DateTime
    dt = DateTime.Parse(TxtNyukoDate.Text)
    sql &= "    ,KIGENBI = " & DateAdd(DateInterval.Day, limitDays, dt).ToString("yyMMdd")
    sql &= "    ,TOOSINO = " & ComBlank2ZeroText(TxtCartonNumber1.Text)

    sql &= "    ,HONSU = " & ComBlank2ZeroText(TxtSyukoCount.Text)
    sql &= "    ,JYURYO = " & (Decimal.Parse(ComBlank2ZeroText(TxtWeitghtKg1.Text)) * 1000).ToString()
    sql &= "    ,GENKA = " & ComBlank2ZeroText(TxtUnitPrice.Text)

    sql &= "    ,TANTO = " & ComNothing2ZeroText(CmbMstStaff1.SelectedValue)

    sql &= "    ,KDATE = '" & prmProcTime & "'"

    sql &= " WHERE TDATE = '" & _SelectedData("TDATE") & "'"
    sql &= "   AND KUBUN = " & _SelectedData("KUBUN")
    sql &= "   AND KIKAINO = " & _SelectedData("KIKAINO")
    sql &= "   AND SIRIALNO = " & _SelectedData("SIRIALNO")
    sql &= "   AND KYOKUFLG = " & _SelectedData("KYOKUFLG")
    sql &= "   AND BICODE = " & _SelectedData("BICODE")
    sql &= "   AND TOOSINO =" & _SelectedData("TOOSINO")
    sql &= "   AND EBCODE =" & _SelectedData("EBCODE")
    sql &= "   AND NSZFLG =" & _SelectedData("NSZFLG")
    sql &= "   AND KDATE ='" & _SelectedData("KDATE") & "'"

    Return sql

  End Function

  ''' <summary>
  ''' カット肉加工実績データ更新テーブル更新SQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">処理日時</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsData(prmProcTime As String) As String

    Dim sql As String = String.Empty
    Dim tmpKeyVal As Dictionary(Of String, String) = TargetValNewCutJVal()
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    ' 更新項目を修正
    tmpKeyVal("TDATE") = "'" & prmProcTime & "'"
    tmpKeyVal("KUBUN") = "0"
    tmpKeyVal("KIKAINO") = "999"
    tmpKeyVal("SIRIALNO") = ComBlank2ZeroText(TxtCartonNumber1.Text)
    tmpKeyVal("KYOKUFLG") = "0"
    tmpKeyVal("TKCODE") = "0"
    tmpKeyVal("SAYUKUBUN") = "0"
    tmpKeyVal("SPKUBUN") = "1"
    tmpKeyVal("KIKAKUC") = "0"
    tmpKeyVal("GBFLG") = "2"
    tmpKeyVal("BLOCKCODE") = "0"
    tmpKeyVal("KINGAKU") = "0"
    tmpKeyVal("YOBI") = "0"
    tmpKeyVal("SHOHINC") = "0"
    tmpKeyVal("KEIRYOBI") = "'" & TxtNyukoDate.Text & "'"
    tmpKeyVal("LABELJI") = "0"
    tmpKeyVal("COMMENTC") = "0"
    tmpKeyVal("TJCODE") = "0"
    tmpKeyVal("KFLG") = "0"
    tmpKeyVal("OLDTKC") = "0"
    tmpKeyVal("UTKCODE") = "0"
    tmpKeyVal("TANKA") = "0"
    tmpKeyVal("BAIKA") = "0"
    tmpKeyVal("DENNO") = "0"
    tmpKeyVal("GYONO") = "0"
    tmpKeyVal("NSZFLG") = "0"
    tmpKeyVal("SETCD") = "0"
    tmpKeyVal("DKUBUN") = "0"
    tmpKeyVal("NKUBUN") = "0"
    tmpKeyVal("BICODE") = "0"

    tmpKeyVal("SRCODE") = ComNothing2ZeroText(CmbMstCustomer1.SelectedValue)
    tmpKeyVal("SYUBETUC") = ComNothing2ZeroText(CmbMstCattle1.SelectedValue)
    tmpKeyVal("GENSANCHIC") = ComNothing2ZeroText(CmbMstOriginPlace1.SelectedValue)
    tmpKeyVal("KAKUC") = ComNothing2ZeroText(CmbMstRating1.SelectedValue)
    tmpKeyVal("KIKAKUC") = ComNothing2ZeroText(CmbMstKikaku1.SelectedValue)
    tmpKeyVal("EBCODE") = ComBlank2ZeroText(TxtEdaban1.Text)
    tmpKeyVal("KOTAINO") = ComBlank2ZeroText(TxtKotaiNo1.Text)

    If IsDate(TxtKakouDate.Text) Then
      tmpKeyVal("KAKOUBI") = "'" & TxtKakouDate.Text & "'"
    Else
      tmpKeyVal("KAKOUBI") = "NULL"
    End If

    Dim hi As Long = 30

    ' 部位コードが変更された場合
    If Me.TxtLblMstItem1.CodeTxt.Equals(TxtBuiCode1.Text) = False Then

      tmpKeyVal("BICODE") = ComBlank2ZeroText(Me.TxtLblMstItem1.CodeTxt)

      Dim tmpDb As New clsSqlServer
      Dim sqlBuim As String = String.Empty

      ' SQL文の作成
      sqlBuim = SqlReadBuim(Me.TxtLblMstItem1.CodeTxt)

      Dim tmpDt As New DataTable
      Call tmpDb.GetResult(tmpDt, sqlBuim)

      If (1 <= tmpDt.Rows.Count) Then

        Dim dtRow As DataRow
        dtRow = tmpDt.Rows(0)

        tmpKeyVal("GBFLG") = ComBlank2ZeroText(dtRow("GBFLG").ToString)
        tmpKeyVal("BLOCKCODE") = ComBlank2ZeroText(dtRow("BLOCKCODE").ToString)
        tmpKeyVal("SHOHINC") = ComBlank2ZeroText(dtRow("SHOHINC").ToString)
        tmpKeyVal("COMMENTC") = ComBlank2ZeroText(dtRow("COMMENTC").ToString)
        tmpKeyVal("BUDOMARI") = ComBlank2ZeroText(dtRow("BUDOMARI").ToString)
        hi = ComBlank2Zero(dtRow("SHOMINISU").ToString)
      End If
    End If

    Dim dt As DateTime
    dt = DateTime.Parse(TxtNyukoDate.Text)
    tmpKeyVal("KIGENBI") = DateAdd(DateInterval.Day, hi, dt).ToString("yyMMdd")
    tmpKeyVal("TOOSINO") = ComBlank2ZeroText(TxtCartonNumber1.Text)

    tmpKeyVal("HONSU") = ComBlank2ZeroText(TxtSyukoCount.Text)

    tmpKeyVal("JYURYO") = (Decimal.Parse(ComBlank2ZeroText(TxtWeitghtKg1.Text)) * 1000).ToString()
    tmpKeyVal("GENKA") = ComBlank2ZeroText(TxtUnitPrice.Text)

    tmpKeyVal("TANTO") = ComNothing2ZeroText(CmbMstStaff1.SelectedValue)

    tmpKeyVal("KDATE") = "'" & prmProcTime & "'"

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst = tmpDst & tmpKey & " ,"
      tmpVal = tmpVal & tmpKeyVal(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= "INSERT INTO CUTJ(" & tmpDst & ")"
    sql &= "          VALUES(" & tmpVal & ")"

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' データ更新SQL文作成
  ''' </summary>
  ''' <param name="prmEditData">編集された値</param>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutJ(prmEditData As Dictionary(Of String, String) _
                            , prmSelectedRow As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    Dim setTxt As String = prmEditData(prmEditData.Keys(0))
    ' 入力値が空白判定
    If String.IsNullOrWhiteSpace(setTxt) Then
      ' 入力値が空白の場合は0を代入
      setTxt = "0"
    End If

    sql &= " UPDATE CUTJ "
    sql &= " SET " & prmEditData.Keys(0) & " =" & setTxt

    sql &= "    , KDATE = '" & ComGetProcTime() & "'"

    sql &= " WHERE TDATE = '" & prmSelectedRow("TDATE") & "'"
    sql &= "   AND KUBUN = " & prmSelectedRow("KUBUN")
    sql &= "   AND KIKAINO = " & prmSelectedRow("KIKAINO")
    sql &= "   AND SIRIALNO = " & prmSelectedRow("SIRIALNO")
    sql &= "   AND KYOKUFLG = " & prmSelectedRow("KYOKUFLG")
    sql &= "   AND BICODE = " & prmSelectedRow("BICODE")
    sql &= "   AND TOOSINO =" & prmSelectedRow("TOOSINO")
    sql &= "   AND EBCODE =" & prmSelectedRow("EBCODE")
    sql &= "   AND NSZFLG =" & prmSelectedRow("NSZFLG")
    sql &= "   AND KDATE ='" & prmSelectedRow("KDATE") & "'"

    Return sql

  End Function

  ''' <summary>
  ''' 入庫データ削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   NSZFLGを0に変更することで在庫に戻す
  ''' </remarks>
  Private Function SqlDelCutJ() As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= "  SET KUBUN = 9 "
    sql &= "    , KYOKUFLG =  9"
    sql &= "    , TANTO =  " & ComNothing2ZeroText(CmbMstStaff1.SelectedValue)
    sql &= "    , KDATE = '" & ComGetProcTime() & "'"

    sql &= " WHERE TDATE = '" & _SelectedData("TDATE") & "'"
    sql &= "   AND KUBUN = " & _SelectedData("KUBUN")
    sql &= "   AND KIKAINO = " & _SelectedData("KIKAINO")
    sql &= "   AND SIRIALNO = " & _SelectedData("SIRIALNO")
    sql &= "   AND KYOKUFLG = " & _SelectedData("KYOKUFLG")
    sql &= "   AND BICODE = " & _SelectedData("BICODE")
    sql &= "   AND TOOSINO =" & _SelectedData("TOOSINO")
    sql &= "   AND EBCODE =" & _SelectedData("EBCODE")
    sql &= "   AND NSZFLG =" & _SelectedData("NSZFLG")
    sql &= "   AND KDATE ='" & _SelectedData("KDATE") & "'"

    Return sql

  End Function


  ''' <summary>
  ''' 入庫データ返品削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   NSZFLGを0に変更することで在庫に戻す
  ''' </remarks>
  Private Function SqlDelHenpinCutJ() As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= "  SET NSZFLG = 0 "
    sql &= "    , HTANKA = 0 "
    sql &= "    , GYONO = 0 "
    sql &= "    , KDATE = '" & ComGetProcTime() & "'"

    sql &= " FROM CUTJ "
    sql &= " WHERE 1=1 "

    ' 入庫日付が日付データかどうか判定
    If IsDate(TxtNyukoDate.Text) Then
      sql &= " AND CUTJ.KEIRYOBI = '" & TxtNyukoDate.Text & "'"
    End If

    ' 仕入先
    sql &= "    AND CUTJ.SRCODE   =  " & ComNothing2ZeroText(CmbMstCustomer1.SelectedValue)
    ' 枝番
    sql &= "    AND CUTJ.EBCODE   =  " & ComBlank2ZeroText(TxtEdaban1.Text)
    ' 個体識別番号
    sql &= "    AND CUTJ.KOTAINO  =  " & ComBlank2ZeroText(TxtKotaiNo1.Text)
    sql &= "    AND CUTJ.KUBUN <> 9 "

    ' 加工日が日付データかどうか判定
    If IsDate(TxtKakouDate.Text) Then
      sql &= "    AND CUTJ.KAKOUBI = '" & TxtKakouDate.Text & "'"
    End If

    ' 部位コード
    sql &= "    AND CUTJ.BICODE  =  " & ComBlank2ZeroText(TxtLblMstItem1.CodeTxt)
    ' カートンNo
    sql &= "    AND CUTJ.TOOSINO  =  " & ComBlank2ZeroText(TxtCartonNumber1.Text)

    Return sql

  End Function

  ''' <summary>
  ''' 入庫一覧印刷用ワークテーブル挿入SQL文作成
  ''' </summary>
  ''' <param name="prmSelected">挿入するデータ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsWkTblFromGrid(prmSelected As Dictionary(Of String, String)) As String

    _targetData = prmSelected

    Dim sql As String = String.Empty
    Dim tmpKeyVal As Dictionary(Of String, String) = TargetValExtractionCutJVal()
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    ' 更新項目を修正
    tmpKeyVal("KOTAINOW") = "'" & ComBlank2NullText(_targetData("KOTAINO")) & "'"
    tmpKeyVal("DLTKNAME") = "'" & ComBlank2NullText(_targetData("DLTKNAME")) & "'"
    tmpKeyVal("DKZNAME") = "'" & ComBlank2NullText(_targetData("DKZNAME")) & "'"
    tmpKeyVal("DKKNAME") = "'" & ComBlank2NullText(_targetData("DKKNAME")) & "'"
    tmpKeyVal("DBINAME") = "'" & ComBlank2NullText(_targetData("DBINAME").TrimStart("0"c)) & "'"
    tmpKeyVal("DBLNAME") = "'" & ComBlank2NullText(_targetData("DBLNAME")) & "'"
    tmpKeyVal("DGNNAME") = "'" & ComBlank2NullText(_targetData("DGNNAME")) & "'"
    tmpKeyVal("DSBNAME") = "'" & ComBlank2NullText(_targetData("DSBNAME")) & "'"
    tmpKeyVal("DCMNAME") = "'" & ComBlank2NullText(_targetData("DCMNAME")) & "'"
    tmpKeyVal("DLSRNAME") = "'" & ComBlank2NullText(_targetData("DLSRNAME")) & "'"
    tmpKeyVal("DTJNAME") = "'" & ComBlank2NullText(_targetData("DTJNAME")) & "'"

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst = tmpDst & tmpKey & " ,"
      tmpVal = tmpVal & tmpKeyVal(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= "INSERT INTO WK_NYUKO(" & tmpDst & ")"
    sql &= "              VALUES(" & tmpVal & ")"

    Return sql

  End Function

  ''' <summary>
  ''' 入庫一覧印刷用ワークテーブル削除SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelWK_NYUKO() As String

    Dim sql As String = String.Empty

    sql &= " DELETE FROM WK_NYUKO "

    Return sql

  End Function

  ''' <summary>
  '''CUTJ項目初期設定
  ''' </summary>
  ''' <returns>CUTJ項目のみ設定した連想配列</returns>
  Private Function TargetValNewCutJVal() As Dictionary(Of String, String)

    Dim ret As New Dictionary(Of String, String)

    With ret

      ' 日付形式データ
      ' 空白は"NULL"に置き換え
      Dim tmpKeyName As String = String.Empty

      .Add("KAKOUBI", "NULL")
      .Add("KEIRYOBI", "NULL")
      .Add("SYUKKABI", "NULL")
      .Add("HENPINBI", "NULL")
      .Add("TDATE", "NULL")
      .Add("KDATE", "NULL")

      ' 数値形式データ
      '  空白は"0"に置き換え
      .Add("KUBUN", "0")
      .Add("KIKAINO", "0")
      .Add("SIRIALNO", "0")
      .Add("KYOKUFLG", "0")
      .Add("TKCODE", "0")
      .Add("KAKUC", "0")
      .Add("EBCODE", "0")
      .Add("HONSU", "0")
      .Add("SAYUKUBUN", "0")
      .Add("SPKUBUN", "0")
      .Add("KIKAKUC", "0")
      .Add("BICODE", "0")
      .Add("JYURYO", "0")
      .Add("GBFLG", "0")
      .Add("TOOSINO", "0")
      .Add("BLOCKCODE", "0")
      .Add("KINGAKU", "0")
      .Add("KIGENBI", "0")
      .Add("SYUBETUC", "0")
      .Add("KOTAINO", "0")
      .Add("YOBI", "0")
      .Add("SHOHINC", "0")
      .Add("LABELJI", "0")
      .Add("GENSANCHIC", "0")
      .Add("COMMENTC", "0")
      .Add("SRCODE", "0")
      .Add("TJCODE", "0")
      .Add("KFLG", "0")
      .Add("BUDOMARI", "0")
      .Add("OLDTKC", "0")
      .Add("UTKCODE", "0")
      .Add("TANKA", "0")
      .Add("BAIKA", "0")
      .Add("DENNO", "0")
      .Add("GYONO", "0")
      .Add("TANTO", "0")
      .Add("NSZFLG", "0")
      .Add("SETCD", "0")
      .Add("GENKA", "0")
      .Add("DKUBUN", "0")
      .Add("NDENNO", "NULL")
      .Add("NGYONO", "NULL")
      .Add("NKUBUN", "0")
      .Add("HTANKA", "NULL")
    End With

    Return ret
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

#Region "データ更新"

  ''' <summary>
  ''' データ更新を行う
  ''' </summary>
  ''' <remarks>[登録]ボタン押下時更新処理</remarks>
  Private Sub UpDateDb()

    Dim sql As String = String.Empty
    Dim tmpDb As New clsSqlServer
    Dim tmpProcTime As String = ComGetProcTime()

    ' 一覧から選択されているか？
    If (_SelectedData.Keys.Count > 0) Then
      ' 選択されている
      Sql = SqlUpdData(tmpProcTime)         ' 更新
    Else
      ' 選択されていない
      Sql = SqlInsData(tmpProcTime)         ' 追加
    End If

    ' 実行
    Try
      ' トランザクション開始
      tmpDb.TrnStart()

      ' SQL実行結果が1件か？
      If tmpDb.Execute(Sql) = 1 Then
        ' 更新成功
        tmpDb.TrnCommit()
      Else
        ' 更新失敗
        Throw New Exception("CUTJの更新に失敗しました。他のユーザーによって修正されています。")
      End If

    Catch ex As Exception
      ' Error
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
      Throw New Exception("入庫データの更新に失敗しました。")
    End Try

  End Sub

  ''' <summary>
  ''' データ更新を行う
  ''' </summary>
  ''' <returns>
  '''   True  - 更新成功
  '''   False - 更新失敗
  ''' </returns>
  ''' <remarks>一覧を直接編集時の更新処理</remarks>
  Private Function UpDateDbFromList() As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True

    Try
      With tmpDb
        ' 更新処理実行
        .TrnStart()

        ' 明細（入荷単価、売上単価）を直接修正
        sql = SqlUpdCutJ(Controlz(DG2V1.Name).EditData _
                         , Controlz(DG2V1.Name).SelectedRow)

        If .Execute(sql) = 1 Then
          ' 更新OK
          .TrnCommit()
        Else
          ' 更新失敗
          ' 更新件数は必ず1件です
          Throw New Exception("データ更新に失敗しました。他のユーザーによって変更された可能性があります。")
        End If

      End With
    Catch ex As Exception

      Call ComWriteErrLog(ex)

      ' Error ロールバック
      tmpDb.TrnRollBack()
      ret = False
      Throw New Exception("データの更新に失敗しました。")
    End Try

    Return ret

  End Function


  ''' <summary>
  ''' データ削除を行う
  ''' </summary>
  Private Sub DeleteDb()
    Dim tmpDb As New clsSqlServer

    Try
      With tmpDb

        ' 削除処理実行（中身はフラグ更新）
        .TrnStart()

        ' 入庫データ削除SQL文の作成
        If .Execute(SqlDelCutJ()) <> 1 Then
          '更新失敗
          Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
        End If

        ' 返品データの場合
        If (ComBlank2Zero(_SelectedData("NSZFLG")) = 8) Then
          If .Execute(SqlDelHenpinCutJ()) <> 1 Then
            '更新失敗
            Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
          End If
        End If

        ' 更新OK
        .TrnCommit()

      End With
    Catch ex As Exception
      Call ComWriteErrLog(ex)

      ' Error ロールバック
      tmpDb.TrnRollBack()
      Throw New Exception("データの削除に失敗しました。")
    End Try
  End Sub
#End Region

#Region "コントロール制御"

  ''' <summary>
  ''' データは選択されているか？
  ''' </summary>
  ''' <returns>
  '''   True  - データは選択されている
  '''   false - データは選択されていない
  ''' </returns>
  Private Function IsSelected(msg As String) As Boolean

    ' データグリッドの選択件数が０件の場合
    Dim ret As Boolean = (_SelectedData.Keys.Count > 0)

    If ret = False Then
      ComMessageBox("データが選択されていません。" & msg,
                    PRG_TITLE, typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
    End If

    Return ret
  End Function

  ''' <summary>
  ''' 編集不可コントロールを使用不可に
  ''' </summary>
  Private Sub LockCtrl()
    ' 仕入先
    With Me.CmbMstCustomer1
      .Enabled = False
    End With

    ' 個体識別番号
    With Me.TxtKotaiNo1
      .Enabled = False
      .ReadOnly = True
    End With

  End Sub

  ''' <summary>
  ''' 編集不可コントロールを使用可に
  ''' </summary>
  Private Sub UnLockCtrl()
    ' 仕入先
    With Me.CmbMstCustomer1
      .Enabled = True
    End With

    ' 個体識別番号
    With Me.TxtKotaiNo1
      .Enabled = True
      .ReadOnly = False
    End With

  End Sub

  ''' <summary>
  ''' 入力状態初期化
  ''' </summary>
  Private Sub ResetData()
    ' 入庫日、仕入先、担当者以外の全項目をクリア
    MyBase.AllClear(New List(Of Control)({Me.TxtNyukoDate, Me.CmbMstCustomer1, Me.CmbMstStaff1}))

    ' 選択内容をクリア
    _SelectedData.Clear()

    ' 編集不可コントロールを入力可に
    Call UnLockCtrl()

    ' 畜種（種別）にフォーカスを当てる
    Me.CmbMstCattle1.Focus()

  End Sub

#End Region

#End Region

#End Region

#Region "イベントプロシージャー"

#Region "ボタン"
  ''' <summary>
  ''' 登録ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnRegist_Click(sender As Object, e As EventArgs) Handles btnRegist.Click

    ' 確認メッセージ表示
    If typMsgBoxResult.RESULT_CANCEL = ComMessageBox("登録更新しますか？", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OKCANCEL) Then
      TxtLblMstItem1.Focus()
      Exit Sub
    End If

    ' 部位コードの指定を必須とする
    If String.IsNullOrWhiteSpace(Me.TxtLblMstItem1.CodeTxt) Then
      ComMessageBox("部位コードが無い明細は登録できません！！", PRG_TITLE, typMsgBox.MSG_NORMAL)
      TxtLblMstItem1.Focus()
      Exit Sub
    End If

    ' 返品データは更新不可
    If _SelectedData.ContainsKey("NSZFLG") _
          AndAlso ComBlank2Zero(_SelectedData("NSZFLG")) = 8 Then

      ComMessageBox("返品データは入庫できません！！", PRG_TITLE, typMsgBox.MSG_NORMAL)
      Call ResetData()
      Exit Sub

    End If

    Try
      Call UpDateDb()   ' 更新（or 追加）

      Call ResetData()  ' 入力状態リセット
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
    End Try

    ' 再表示
    Controlz(DG2V1.Name).ShowList(True)

  End Sub

  ''' <summary>
  ''' 返品ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnHenpin_Click(sender As Object, e As EventArgs) Handles btnDspStockList.Click

    If IsSelected("返品できません！！") Then

      ' データが選択されているなら、確認メッセージを表示し返品処理実行
      If typMsgBoxResult.RESULT_CANCEL = ComMessageBox("表示中の明細を返品しますか？" _
                                                  , PRG_TITLE _
                                                  , typMsgBox.MSG_NORMAL _
                                                  , typMsgBoxButton.BUTTON_OKCANCEL) Then

        TxtLblMstItem1.Focus()
        Exit Sub
      End If

      ' 返品データは返品不可
      If (ComBlank2Zero(_SelectedData("NSZFLG")) = 8) Then
        Exit Sub
      End If

      ' 返品金額入力画面
      Dim tmpSubForm As New Form_HenkinInput(Me)

      Try
        ' 返品金額入力画面表示
        Select Case tmpSubForm.ShowSubForm(_SelectedData)
          Case SFBase.typSfResult.SF_OK

            Call ResetData()

            ' 再表示
            Controlz(DG2V1.Name).ShowList(True)

          Case SFBase.typSfResult.SF_CANCEL
        End Select

      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      End Try

    End If

  End Sub

  ''' <summary>
  ''' 削除ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    If IsSelected("削除できません！！") Then

      ' データが選択されているなら、確認メッセージを表示し削除処理実行
      If typMsgBoxResult.RESULT_OK = ComMessageBox("表示中の明細を削除しますか？" _
                                                  , PRG_TITLE _
                                                  , typMsgBox.MSG_NORMAL _
                                                  , typMsgBoxButton.BUTTON_OKCANCEL) Then
        Try
          ' 削除（中身はフラグ更新）
          Call DeleteDb()
        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        End Try

        Call ResetData()

        ' 先頭の列を選択状態とする
        DG2V1.CurrentCell = DG2V1(DG2V1.FirstDisplayedScrollingColumnIndex, DG2V1.FirstDisplayedScrollingRowIndex)

        ' 再表示
        Controlz(DG2V1.Name).ShowList(True)

      End If

    End If

  End Sub

  ''' <summary>
  ''' 印刷ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    Dim tmpDb As New clsReport(clsGlobalData.REPORT_FILENAME)

    ' 対象のデータが存在しない場合は処理を実行しない
    If (dataGridCount = 0) Then
      Exit Sub
    End If

    If typMsgBoxResult.RESULT_OK = ComMessageBox("入庫情報一覧を画面表示しますか？" _
                                                , PRG_TITLE _
                                                , typMsgBox.MSG_NORMAL _
                                                , typMsgBoxButton.BUTTON_OKCANCEL) Then
      Try
        With tmpDb
          ' ワークテーブル削除
          .Execute(SqlDelWK_NYUKO())

          ' Gridに表示中のデータをワークテーブルに書き込み
          For Each tmpRowData As Dictionary(Of String, String) In Controlz(DG2V1.Name).GetAllData
            .Execute(SqlInsWkTblFromGrid(tmpRowData))
          Next

          .DbDisconnect()

          ' ACCESSの入庫情報一覧表レポートを表示
          ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_NYUKO")

        End With

      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      Finally
        tmpDb.Dispose()

        Controlz(DG2V1.Name).AutoSearch = True

      End Try
    End If
  End Sub

  ''' <summary>
  ''' 終了ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

    ' 自動検索OFF
    Controlz(DG2V1.Name).AutoSearch = False
    Controlz(DG2V1.Name).ResetPosition()

    MyBase.AllClear()
    Controlz(DG2V1.Name).InitSort()

    _SelectedData.Clear()
    Me.TxtNyukoDate.Focus()
    Me.Hide()

    Controlz(DG2V1.Name).AutoSearch = True
    DG2V1.Visible = True

  End Sub

#End Region

#Region "フォーム"

  ''' <summary>
  ''' 画面起動時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Nyuko_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = PRG_TITLE


    Call InitForm02()

    '最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

    Me.TxtNyukoDate.Text = Now().ToString("yyyy/MM/dd")

    ' グリッドダブルクリック時処理追加
    Controlz(DG2V1.Name).lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick

    ' グリッド再表示時処理
    Controlz(DG2V1.Name).lcCallBackReLoadData = AddressOf DgvReload

    ' 入庫明細表示
    Controlz(DG2V1.Name).AutoSearch = True

    ' 基本コントロール以外のメッセージを設定
    TxtNyukoDate.SetMsgLabelText("入庫日（仕入日）を入力します。")                           ' 入庫日
    TxtKakouDate.SetMsgLabelText("加工日を入力します。年月日を6桁数字で入力します。")        ' 加工日
    TxtCartonNumber1.SetMsgLabelText("カートン番号（連番）を入力します。カートン番号が無い場合は入力せずにEnterを押して下さい。")   ' カートンNo
    TxtSyukoCount.SetMsgLabelText("入庫数を入力します。")                                    ' 入庫数
    TxtUnitPrice.SetMsgLabelText("Kg当りの売単価を入力します。")                             ' Kg売単価
    Controlz(DG2V1.Name).SetMsgLabelText("入庫明細を修正したいときは明細行を選びＥｎｔｅｒを押して下さい。")
    TxtKotaiNo1.SetMsgLabelText("個体識別番号を入力します。個体識別番号が無い場合は入力せずにＥｎｔｅｒを押して下さい。")

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)

    ' 個体識別番号入力時処理
    With TxtKotaiNo1
      .lcCallBackEnterEnableKotaiNo = AddressOf EnterEnabelKotaiNo    ' 有効
      .lcCallBackEnterInvalidKotaiNo = AddressOf EnterInvalidKotaiNo  ' 無効
    End With


    ' IPC通信起動
    InitIPC(PRG_ID)

    ' 入庫日にフォーカスをあてる
    Me.ActiveControl = Me.TxtNyukoDate
  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Nyuko_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 更新ボタン押下処理
        Me.btnRegist.Focus()
        If (TxtKotaiNo1CancelFlg = False) Then
          Me.btnRegist.PerformClick()
        End If
      ' F3キー押下時
      Case Keys.F2
        ' 返品ボタン押下処理
        Me.btnDspStockList.Focus()
        If (TxtKotaiNo1CancelFlg = False) Then
          Me.btnDspStockList.PerformClick()
        End If
      ' F5キー押下時
      Case Keys.F5
        ' 削除ボタン押下処理
        Me.btnDelete.Focus()
        If (TxtKotaiNo1CancelFlg = False) Then
          Me.btnDelete.PerformClick()
        End If
      ' F9キー押下時
      Case Keys.F9
        ' 印刷ボタン押下処理
        Me.btnPrint.Focus()
        If (TxtKotaiNo1CancelFlg = False) Then
          Me.btnPrint.PerformClick()
        End If
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        Me.btnClose.Focus()
        If (TxtKotaiNo1CancelFlg = False) Then
          Me.btnClose.PerformClick()
        End If
    End Select

  End Sub


#End Region

#Region "グリッド関連"

  ''' <summary>
  ''' データグリッド更新時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate">最終更新日時</param>
  ''' <param name="DataCount">データ件数</param>
  Private Sub DgvReload(sender As DataGridView, LastUpdate As String, DataCount As Long, DataJuryo As Decimal, DataKingaku As Decimal)

    ' データグリッドの選択件数設定
    dataGridCount = DataCount
    ' データグリッドの検索日付設定
    dataGridDate = ComGetProcTime()

    Me.Label_GridData.AutoSize = False
    Me.Label_GridData.TextAlign = ContentAlignment.MiddleCenter
    '文字列をDateTime値に変換する
    Dim dt As DateTime = DateTime.Parse(LastUpdate)
    Me.Label_GridData.Text = dt.ToString("yyyy年M月d日HH：mm") & " 現在 " & Space(6) & " 入庫明細 件数：" & Space(6) & DataCount.ToString()

  End Sub

  ''' <summary>
  ''' グリッドダブルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' 選択された値を保持
  ''' 選択内容を編集コントロールに設定
  ''' </remarks>
  Private Sub DgvCellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    With DG2V1

      _SelectedData = Controlz(.Name).SelectedRow

      ' 入庫明細表示時は選択された得意先を編集コントロールに設定
      Me.CmbMstCustomer1.SelectedValue = Integer.Parse(_SelectedData("SRCODE"))                   ' 仕入先
      Me.CmbMstCattle1.SelectedValue = Integer.Parse(_SelectedData("SYUBETUC"))                   ' 畜種
      Me.CmbMstOriginPlace1.SelectedValue = Integer.Parse(_SelectedData("GENSANCHIC"))            ' 原産地
      Me.CmbMstRating1.SelectedValue = Integer.Parse(_SelectedData("KAKUC"))                      ' 格付
      Me.CmbMstKikaku1.SelectedValue = Integer.Parse(_SelectedData("KIKAKUC"))                    ' 規格

      Me.TxtEdaban1.Text = _SelectedData("EBCODE")                                                ' 枝番
      Me.TxtKotaiNo1.Text = _SelectedData("KOTAINO")                                              ' 個体識別番号

      If String.IsNullOrWhiteSpace(_SelectedData("KAKOUBI")) Then
        Me.TxtKakouDate.Text = String.Empty
      Else
        Me.TxtKakouDate.Text = Date.Parse(_SelectedData("KAKOUBI")).ToString("yyyy/MM/dd")          ' 加工日
      End If

      Me.TxtLblMstItem1.CodeTxt = _SelectedData("BICODE")                               　        ' 部位
      Me.TxtBuiCode1.Text = _SelectedData("BICODE")                               　              ' 部位の修正前
      Me.TxtCartonNumber1.Text = _SelectedData("TOOSINO")                                         ' カートンNo
      Me.TxtWeitghtKg1.Text = ((Math.Floor(Long.Parse(_SelectedData("JYURYO")))) / 1000).ToString ' 重量
      Me.TxtSyukoCount.Text = _SelectedData("HONSU")                                              ' 本数
      Me.TxtUnitPrice.Text = _SelectedData("GENKA")                                               ' 仕入単価

      ' 編集不可コントロールを使用不可に
      Call LockCtrl()
    End With

  End Sub

  ''' <summary>
  ''' データグリッドフォーカス選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_Enter(sender As Object, e As EventArgs) Handles DataGridView1.Enter

    'フォーカスが戻ると元の選択状態に戻る
    DG2V1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
    DG2V1.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText

  End Sub

  Private Sub DataGridView1_Leave(sender As Object, e As EventArgs) Handles DataGridView1.Leave

    'フォーカスが外れると選択状況を隠す
    DG2V1.DefaultCellStyle.SelectionBackColor = DG2V1.DefaultCellStyle.BackColor
    DG2V1.DefaultCellStyle.SelectionForeColor = DG2V1.DefaultCellStyle.ForeColor

    ' データグリッドのフォーカス喪失時の位置設定
    If DG2V1.SelectedCells.Count > 0 Then
      rowIndexDataGrid = DG2V1.SelectedCells(0).RowIndex
    End If

  End Sub

#End Region

#Region "テキストボックス"
  ''' <summary>
  ''' バリデーション時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtNyukoDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtNyukoDate.Validating

    ' 未入力なら本日日付を設定する
    With Me.TxtNyukoDate
      If .Text.Length <= 0 Then
        .Text = ComGetProcDate()
      End If
    End With

  End Sub

  ''' <summary>
  ''' 入庫日にフォーカス設定時、入庫日以外の全項目をクリア
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtNyukoDate_Enter(sender As Object, e As EventArgs) Handles TxtNyukoDate.Enter

    ' 入庫日
    If Me.TxtNyukoDate.Text.Trim = "" Then
      Me.TxtNyukoDate.Text = ComGetProcDate()
    End If

    If IsDate(Me.TxtNyukoDate.Text） = False Then
      Me.TxtNyukoDate.Text = ComGetProcDate()
    End If

    ' 一覧の自動検索を停止
    Controlz(DG2V1.Name).AutoSearch = False

    ' 入庫日、担当者以外の全項目をクリア
    MyBase.AllClear(New List(Of Control)({Me.TxtNyukoDate, Me.CmbMstStaff1}))

    ' 入庫明細を表示
    DG2V1.Visible = True
    Controlz(DG2V1.Name).AutoSearch = True

    ' 選択内容をクリア
    _SelectedData.Clear()

    ' 編集不可コントロールを入力可に
    Call UnLockCtrl()

  End Sub

  ''' <summary>
  ''' 個体識別番号有効時処理
  ''' </summary>
  ''' <param name="prmKotaiNo">個体識別番号</param>
  Private Sub EnterEnabelKotaiNo(prmKotaiNo As String)
    TxtKotaiNo1CancelFlg = False
  End Sub

  ''' <summary>
  ''' 個体識別番号無効時処理
  ''' </summary>
  ''' <param name="prmKotaiNo">個体識別番号</param>
  Private Sub EnterInvalidKotaiNo(prmKotaiNo As String)

    Dim rtn As typMsgBoxResult

    TxtKotaiNo1CancelFlg = False

    rtn = clsCommonFnc.ComMessageBox("個体識別番号が正しくありません！!" & vbCrLf &
                                   "入力しなおすときはキャンセルをクリックしてください" & vbCrLf &
                                   "間違ったまま次の入力に進むときＯＫをクリックしてください",
                                   "個体識別番号入力",
                                   typMsgBox.MSG_NORMAL,
                                   typMsgBoxButton.BUTTON_OKCANCEL)

    ' 確認メッセージボックスで、CANCELボタン選択時
    If rtn = typMsgBoxResult.RESULT_CANCEL Then
      Me.ActiveControl = TxtKotaiNo1
      Controlz(DG2V1.Name).AutoSearch = True
      TxtKotaiNo1CancelFlg = True

    End If

  End Sub


#End Region

#End Region

End Class
