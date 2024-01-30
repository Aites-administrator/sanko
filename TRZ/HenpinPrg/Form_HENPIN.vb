Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports T.R.ZCommonClass.clsDataGridSearchControl

Public Class Form_HENPIN
  Implements IDgvForm02

  '----------------------------------------------
  '          返品処理画面
  '
  '
  '----------------------------------------------
#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "henpin"
  Private Const PRG_TITLE As String = "返品処理"
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

    ' 返品明細オブジェクトの設定
    DG2V1 = Me.DataGridView1

    ' グリッド初期化
    Call InitGrid(DG2V1, CreateGrid2Src1(), CreateGrid2Layout1())

    ' 返品明細設定
    With DG2V1

      '表示する
      .Visible = True

      ' グリッド動作設定
      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 3

        ' 検索コントロール設定
        .AddSearchControl(Me.TxtHenpinDate, "CUTJ.HENPINBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.CmbMstCustomer1, "CUTJ.UTKCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        '        .AddSearchControl(Me.TxtKotaiNo1, "CUTJ.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)

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
  '''   返品明細抽出用
  ''' </remarks>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1

    Dim sql As String = String.Empty

    sql &= CreateSqlBase()
    sql &= " WHERE CUTJ.KYOKUFLG = 0 "
    sql &= "   AND CUTJ.KUBUN <> 9  "
    sql &= "   AND CUTJ.DKUBUN = 0 "
    sql &= "   AND CUTJ.NSZFLG = 2 "
    sql &= "   AND CUTJ.NKUBUN = 1 "

    sql &= " ORDER BY CUTJ.UTKCODE"
    sql &= "         ,CUTJ.KOTAINO"
    sql &= "         ,CUTJ.KAKOUBI"
    sql &= "         ,CUTJ.BICODE"
    sql &= "         ,CUTJ.TOOSINO"
    sql &= "         ,CUTJ.KDATE DESC"

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' 返品明細DataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1

    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("個体識別", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=110, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("枝番", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=60))
      .Add(New clsDGVColumnSetting("得意先", "DLTKNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=110))
      .Add(New clsDGVColumnSetting("返品事由", "RETURN_REASON", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=110))
      .Add(New clsDGVColumnSetting("加工日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("ｶｰﾄﾝNo", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("格付", "DKZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=110))
      .Add(New clsDGVColumnSetting("部位", "DBINAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argFormat:="###0.0", argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("出荷日", "SYUKKABI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("返品単価", "HTANKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("種別", "DSBNAME", argTextAlignment:=typAlignment.MiddleLeft))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
      '      .Add(New clsDGVColumnSetting("更新日２", "RT_KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret

  End Function

  ''' <summary>
  ''' 返品明細のDataGridViewオブジェクトの一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  ''' <remarks>編集列はありません</remarks>
  Public Function CreateGrid2EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol1
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
      '    .Add(New clsDataGridEditTextBox("原単価", prmUpdateFnc:=AddressOf UpDateDbFromList, prmValueType:=VT_NUMBER, prmMaxChar:=6))
      '    .Add(New clsDataGridEditTextBox("売単価", prmUpdateFnc:=AddressOf UpDateDbFromList, prmValueType:=VT_NUMBER, prmMaxChar:=6))

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
    Call ComStartPrg(PRG_ID, Form_HENPIN, AddressOf ComRedisplay)
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
  ''' 抽出条件、並び順を変更し[返品明細][在庫一覧]で共通で使用
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
    sql &= "      , CASE CUTJ.SAYUKUBUN "
    sql &= "         WHEN 1 THEN '左' "
    sql &= "         WHEN 2 THEN '右' "
    sql &= "         ELSE ' '"
    sql &= "        END AS LR2 "
    sql &= "      , MST_RETURN_TYPE.RETURN_REASON "
    sql &= "      , TRN_RETURN_REASON.RETURN_CODE AS RET_CODE "
    sql &= "      , TRN_RETURN_REASON.KDATE AS RT_KDATE "
    sql &= "  FROM ((((((((((((CUTJ LEFT JOIN TOKUISAKI AS UTK ON CUTJ.UTKCODE = UTK.TKCODE) "
    sql &= "                        LEFT JOIN KAKU ON CUTJ.KAKUC = KAKU.KKCODE) "
    sql &= "                        LEFT JOIN KIKA ON CUTJ.KIKAKUC = KIKA.KICODE) "
    sql &= "                        LEFT JOIN BUIM ON CUTJ.BICODE = BUIM.BICODE) "
    sql &= "                        LEFT JOIN BLOCK_TBL ON CUTJ.BLOCKCODE = BLOCK_TBL.BLOCKCODE) "
    sql &= "                        LEFT JOIN GBFLG_TBL ON CUTJ.GBFLG = GBFLG_TBL.GBCODE) "
    sql &= "                        LEFT JOIN GENSN ON CUTJ.GENSANCHIC = GENSN.GNCODE) "
    sql &= "                        LEFT JOIN SHUB ON CUTJ.SYUBETUC = SHUB.SBCODE) "
    sql &= "                        LEFT JOIN COMNT ON CUTJ.COMMENTC = COMNT.CMCODE) "
    sql &= "                        LEFT JOIN CUTSR ON CUTJ.SRCODE = CUTSR.SRCODE) "
    sql &= "                        LEFT JOIN TOJM ON CUTJ.TJCODE = TOJM.TJCODE) "
    sql &= "                        LEFT JOIN TRN_RETURN_REASON ON CUTJ.KOTAINO = TRN_RETURN_REASON.KOTAINO "
    sql &= "                                                   AND CUTJ.EBCODE = TRN_RETURN_REASON.EBCODE "
    sql &= "                                                   AND CUTJ.BICODE = TRN_RETURN_REASON.BICODE "
    sql &= "                                                   AND CUTJ.SAYUKUBUN = TRN_RETURN_REASON.SAYUKUBUN "
    sql &= "                                                   AND CUTJ.TOOSINO = TRN_RETURN_REASON.TOOSINO "
    sql &= "                                                   AND CUTJ.TDATE = TRN_RETURN_REASON.CUTJ_TDATE "
    sql &= "                                                   AND CUTJ.JYURYO = TRN_RETURN_REASON.JYURYO ) "
    sql &= "                        LEFT JOIN MST_RETURN_TYPE ON MST_RETURN_TYPE.RETURN_CODE = TRN_RETURN_REASON.RETURN_CODE "

    Return sql

  End Function


  ''' <summary>
  ''' 返品データ検索用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelUpdateTargetCutJ() As String

    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM CUTJ "
    sql &= " WHERE KYOKUFLG = 0 "
    sql &= "   AND KUBUN    <> 9"
    sql &= "   AND DKUBUN   = 0"
    sql &= "   AND NKUBUN   = 0 "
    sql &= "   AND NSZFLG   = 1 "

    ' 部位コード
    sql &= "   AND BICODE   = " & ComNothing2ZeroText(TxtLblMstItem1.CodeTxt)
    ' カートンNo
    sql &= "   AND TOOSINO  = " & ComNothing2ZeroText(TxtCartonNumber1.Text)
    '' 得意先
    sql &= "   AND UTKCODE  = " & ComNothing2ZeroText(CmbMstCustomer1.SelectedValue)
    ' 加工日
    sql &= "   AND KAKOUBI  = '" & ComNothing2ZeroText(TxtKakouDate.Text) & "'"
    ' 枝番
    sql &= "   AND EBCODE   = " & ComNothing2ZeroText(TxtEdaban1.Text)
    ' 個体識別番号
    sql &= "   AND KOTAINO  = " & ComNothing2ZeroText(TxtKotaiNo1.Text)
    '' 左右区分
    'sql &= "   AND SAYUKUBUN  =  " & ComNothing2ZeroText(TxtSayukubun.Text)

    sql &= " ORDER BY KOTAINO, "
    sql &= "          KAKOUBI, "
    sql &= "          BICODE,"
    sql &= "          TOOSINO,"
    sql &= "          KDATE DESC"


    Return sql

  End Function

  ''' <summary>
  ''' Object型から整数型への変換
  ''' </summary>
  ''' <param name="prmTargetObj"></param>
  ''' <returns></returns>
  Private Function DTConvLong(prmTargetObj As Object) As Long

    Dim ret As Long = 0

    If prmTargetObj IsNot Nothing Then
      Long.TryParse(prmTargetObj.ToString, ret)
    End If

    Return ret

  End Function


  ''' <summary>
  ''' カット肉加工実績データ更新テーブル挿入SQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">処理日時</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdData(prmProcTime As String) As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET SETCD      = " & ComNothing2ZeroText(CmbMstSetType1.SelectedValue)
    sql &= "    ,HONSU      = " & ComBlank2ZeroText(TxtSyukoCount.Text)
    sql &= "    ,JYURYO     = " & (Decimal.Parse(ComBlank2ZeroText(TxJyuryo1.Text)) * 1000).ToString()
    sql &= "    ,HTANKA     = " & ComBlank2ZeroText(TxtUnitPrice.Text)
    sql &= "    ,TANTO      = " & ComNothing2ZeroText(CmbMstStaff1.SelectedValue)

    If Not Me.CmbMstHenpin.SelectedValue Is Nothing Then
      If (DTConvLong(Me.CmbMstHenpin.SelectedValue) = HENPIN_KAIMODOSHI_ID) Then
        sql &= "    ,GENKA      = " & ComBlank2ZeroText(TxtUnitPrice.Text)
      End If
    End If

    sql &= "    ,KDATE      = '" & prmProcTime & "'"

    sql &= " WHERE KYOKUFLG = 0 "
    sql &= "   AND KUBUN    <> 9  "
    sql &= "   AND DKUBUN   = 0 "
    sql &= "   AND NSZFLG   = 2 "
    sql &= "   AND NKUBUN   = 1 "

    ' 返品日
    sql &= "   AND HENPINBI = '" & TxtHenpinDate.Text & "'"
    ' 得意先
    sql &= "   AND UTKCODE  = " & ComNothing2ZeroText(CmbMstCustomer1.SelectedValue)
    ' 枝番
    sql &= "   AND EBCODE   = " & ComNothing2ZeroText(TxtEdaban1.Text)
    ' 個体識別番号
    sql &= "   AND KOTAINO  = " & ComNothing2ZeroText(TxtKotaiNo1.Text)
    ' 部位コード
    sql &= "   AND BICODE   = " & ComNothing2ZeroText(TxtLblMstItem1.CodeTxt)
    ' カートンNo
    sql &= "   AND TOOSINO  = " & ComNothing2ZeroText(TxtCartonNumber1.Text)
    ' NKUBUN
    sql &= "   AND NKUBUN   = '" & _SelectedData("NKUBUN") & "'"
    ' 登録日
    sql &= "   AND TDATE    = '" & _SelectedData("TDATE") & "'"

    Return sql

  End Function

  ''' <summary>
  ''' 返品データ削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   NSZFLGを2に変更することで出庫に戻す
  ''' </remarks>
  Private Function SqlDelCutJ() As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= "  SET KUBUN     = 9 "
    sql &= "    , KYOKUFLG  = 2 "
    sql &= "    , KDATE     = '" & ComGetProcTime() & "'"
    sql &= "    , TANTO     = " & ComNothing2ZeroText(CmbMstStaff1.SelectedValue)

    sql &= " WHERE TDATE    = '" & _SelectedData("TDATE") & "'"
    sql &= "   AND KUBUN    = " & _SelectedData("KUBUN")
    sql &= "   AND KIKAINO  = " & _SelectedData("KIKAINO")
    sql &= "   AND SIRIALNO = " & _SelectedData("SIRIALNO")
    sql &= "   AND KYOKUFLG = " & _SelectedData("KYOKUFLG")
    sql &= "   AND BICODE   = " & _SelectedData("BICODE")
    sql &= "   AND TOOSINO  = " & _SelectedData("TOOSINO")
    sql &= "   AND EBCODE   = " & _SelectedData("EBCODE")
    sql &= "   AND NSZFLG   = " & _SelectedData("NSZFLG")
    ' 登録日
    sql &= "   AND TDATE    = '" & _SelectedData("TDATE") & "'"

    Return sql

  End Function

  ''' <summary>
  ''' 出庫データ返品削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   返品単価=0に、返品日をNULLに変更することで出庫に戻す
  ''' </remarks>
  Private Function SqlDelSyukoCutJ() As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    '    sql &= "  SET SIRIALNO  = " & (Long.Parse(_SelectedData("SIRIALNO")) + 5).ToString
    sql &= "  SET SIRIALNO  = SIRIALNO + 5 "
    sql &= "    , HTANKA    = 0 "
    sql &= "    , HENPINBI  = NULL "
    sql &= "    , KDATE     = '" & ComGetProcTime() & "'"
    sql &= "    , TANTO     = " & ComNothing2ZeroText(CmbMstStaff1.SelectedValue)

    sql &= " FROM CUTJ "
    sql &= " WHERE NKUBUN  = 0 "
    sql &= " 　AND NSZFLG  = 2 "
    ' 枝番
    sql &= "   AND EBCODE  = " & ComNothing2ZeroText(TxtEdaban1.Text)
    ' 個体識別番号
    sql &= "   AND KOTAINO = " & ComNothing2ZeroText(TxtKotaiNo1.Text)
    ' 加工日が日付データかどうか判定
    If IsDate(TxtKakouDate.Text) Then
      sql &= " AND KAKOUBI = '" & TxtKakouDate.Text & "'"
    End If
    ' 部位コード
    sql &= "   AND BICODE  = " & ComNothing2ZeroText(TxtLblMstItem1.CodeTxt)
    ' カートンNo
    sql &= "   AND TOOSINO = " & ComNothing2ZeroText(TxtCartonNumber1.Text)
    ' 登録日
    sql &= "   AND TDATE    = '" & _SelectedData("TDATE") & "'"

    Return sql

  End Function

  ''' <summary>
  '''返品データ返品削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   NSZFLGを0に変更することで在庫に戻す
  ''' </remarks>
  Private Function SqlDelHenpinCutJ(dtRow As DataRow) As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= "  SET KUBUN     = 9 "
    sql &= "    , KYOKUFLG  = 2 "
    sql &= "    , KDATE     = '" & ComGetProcTime() & "'"
    sql &= "    , TANTO     =  " & ComNothing2ZeroText(CmbMstStaff1.SelectedValue)

    sql &= " FROM CUTJ "

    sql &= " WHERE TDATE    = '" & dtRow("TDATE").ToString & "'"
    sql &= "   AND KUBUN    = " & dtRow("KUBUN").ToString
    sql &= "   AND KIKAINO  = " & dtRow("KIKAINO").ToString
    sql &= "   AND SIRIALNO = " & dtRow("SIRIALNO").ToString
    sql &= "   AND KYOKUFLG = " & dtRow("KYOKUFLG").ToString
    sql &= "   AND BICODE   = " & dtRow("BICODE").ToString
    sql &= "   AND TOOSINO  = " & dtRow("TOOSINO").ToString
    sql &= "   AND EBCODE   = " & dtRow("EBCODE").ToString
    sql &= "   AND NSZFLG   = " & dtRow("NSZFLG").ToString
    ' 登録日
    sql &= "   AND TDATE    = '" & _SelectedData("TDATE") & "'"

    Return sql

  End Function

  ''' <summary>
  ''' 返品事由データ更新テーブル挿入SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdReturnReason() As String

    Dim sql As String = String.Empty

    sql &= " UPDATE TRN_RETURN_REASON "
    sql &= " SET RETURN_CODE = " & ComNothing2ZeroText(CmbMstHenpin.SelectedValue)
    sql &= "    ,JYURYO     = " & (Decimal.Parse(ComBlank2ZeroText(TxJyuryo1.Text)) * 1000).ToString()
    sql &= "    ,KDATE       = '" & ComGetProcTime() & "'"

    sql &= " WHERE "

    ' 個体識別番号
    sql &= "   KOTAINO  = " & ComNothing2ZeroText(TxtKotaiNo1.Text)
    ' 枝番
    sql &= "   AND EBCODE   = " & ComNothing2ZeroText(TxtEdaban1.Text)
    ' 部位コード
    sql &= "   AND BICODE   = " & ComNothing2ZeroText(TxtLblMstItem1.CodeTxt)
    ' 左右
    sql &= "   AND SAYUKUBUN   = " & TxtSayukubun.Text
    ' カートンNo
    sql &= "   AND TOOSINO  = " & ComNothing2ZeroText(TxtCartonNumber1.Text)
    ' 重量
    sql &= "   AND JYURYO = " & (Decimal.Parse(ComBlank2ZeroText(TxOldJyuryo.Text)) * 1000).ToString()
    ' 更新日
    sql &= "   AND KDATE = '" & _SelectedData("RT_KDATE") & "'"

    Console.WriteLine(sql)

    Return sql

  End Function


  ''' <summary>
  ''' 返品一覧印刷用ワークテーブル挿入SQL文作成
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
    tmpKeyVal("KIKAINO") = _targetData("KIKAINO").ToString
    tmpKeyVal("KYOKUFLG") = _targetData("KYOKUFLG").ToString
    tmpKeyVal("KEIRYOBI") = "'" & _targetData("KEIRYOBI").ToString & "'"
    tmpKeyVal("KAKOUBI") = "'" & _targetData("KAKOUBI").ToString & "'"
    tmpKeyVal("GBFLG") = _targetData("GBFLG").ToString
    tmpKeyVal("SRCODE") = _targetData("SRCODE").ToString
    tmpKeyVal("EBCODE") = _targetData("EBCODE").ToString
    tmpKeyVal("KOTAINO") = _targetData("KOTAINO").ToString.PadLeft(10, "0"c)
    tmpKeyVal("BICODE") = _targetData("BICODE").ToString
    tmpKeyVal("SHOHINC") = _targetData("SHOHINC").ToString
    tmpKeyVal("SAYUKUBUN") = _targetData("SAYUKUBUN").ToString
    tmpKeyVal("TOOSINO") = _targetData("TOOSINO").ToString
    tmpKeyVal("JYURYO") = _targetData("JYURYO").ToString
    tmpKeyVal("HONSU") = _targetData("HONSU").ToString
    tmpKeyVal("GENKA") = _targetData("GENKA").ToString
    tmpKeyVal("TANKA") = _targetData("TANKA").ToString
    tmpKeyVal("KINGAKU") = _targetData("KINGAKU").ToString
    tmpKeyVal("SPKUBUN") = _targetData("SPKUBUN").ToString
    tmpKeyVal("SETCD") = _targetData("SETCD").ToString
    tmpKeyVal("TKCODE") = _targetData("TKCODE").ToString
    tmpKeyVal("TANTO") = _targetData("TANTO").ToString
    tmpKeyVal("KFLG") = _targetData("KFLG").ToString
    tmpKeyVal("TDATE") = "'" & _targetData("TDATE").ToString & "'"
    tmpKeyVal("KDATE") = "'" & _targetData("KDATE").ToString & "'"
    tmpKeyVal("KIKAKUC") = _targetData("KIKAKUC").ToString
    tmpKeyVal("KAKUC") = _targetData("KAKUC").ToString
    tmpKeyVal("KIGENBI") = _targetData("KIGENBI").ToString
    tmpKeyVal("GENSANCHIC") = _targetData("GENSANCHIC").ToString
    tmpKeyVal("SYUBETUC") = _targetData("SYUBETUC").ToString
    tmpKeyVal("BINAME") = "'" & _targetData("DBINAME").ToString & "'"
    tmpKeyVal("LSRNAME") = "'" & _targetData("DLSRNAME").ToString & "'"
    tmpKeyVal("GBNAME") = "'" & _targetData("DGBNAME").ToString & "'"
    tmpKeyVal("SBNAME") = "'" & _targetData("DSBNAME").ToString & "'"
    tmpKeyVal("TANTOMEI") = "'" & ComNothing2ZeroText(CmbMstStaff1.SelectedValue) & "'"
    tmpKeyVal("KKNAME") = "'" & _targetData("DKKNAME").ToString & "'"
    tmpKeyVal("KZNAME") = "'" & _targetData("DKZNAME").ToString & "'"
    tmpKeyVal("GNNAME") = "'" & _targetData("DGNNAME").ToString & "'"
    tmpKeyVal("LTKNAME") = "'" & _targetData("DLTKNAME").ToString & "'"
    tmpKeyVal("NSZFLG") = _targetData("NSZFLG").ToString
    tmpKeyVal("JYOUTAI") = "'" & _targetData("JYOUTAI").ToString & "'"
    tmpKeyVal("OROSIBI") = "NULL"
    tmpKeyVal("OROSIKUBUN") = "0"
    tmpKeyVal("RHONSU") = "0"
    tmpKeyVal("RJYURYO") = "0"
    tmpKeyVal("SYUKKABI") = "'" & _targetData("SYUKKABI").ToString & "'"
    tmpKeyVal("HENPINBI") = "'" & _targetData("HENPINBI").ToString & "'"
    tmpKeyVal("HTANKA") = _targetData("HTANKA").ToString

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst = tmpDst & tmpKey & " ,"
      tmpVal = tmpVal & tmpKeyVal(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= "INSERT INTO WK_CUTJ(" & tmpDst & ")"
    sql &= "              VALUES(" & tmpVal & ")"

    Return sql

  End Function

  ''' <summary>
  ''' 返品一覧印刷用ワークテーブル削除SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelWK_NYUKO() As String

    Dim sql As String = String.Empty

    sql &= " DELETE FROM WK_CUTJ "

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
      Dim tmpKeyName As String = String.Empty

      .Add("KIKAINO", "0")        '01:機械№
      .Add("KYOKUFLG", "0")       '02:極性フラグ
      .Add("KEIRYOBI", "NULL")    '03:入庫日
      .Add("KAKOUBI", "NULL")     '04:加工日
      .Add("GBFLG", "0")          '05:牛豚フラグ
      .Add("SRCODE", "0")         '06:仕入先コード    
      .Add("EBCODE", "0")         '07:肢番コード
      .Add("KOTAINO", "0")        '08:固体識別番号
      .Add("BICODE", "0")         '09:部位コード
      .Add("SHOHINC", "0")        '10:商品コード
      .Add("SAYUKUBUN", "0")      '11:左右
      .Add("TOOSINO", "0")        '12:通№
      .Add("JYURYO", "0")         '13:重量
      .Add("HONSU", "0")          '14:入本数  
      .Add("GENKA", "0")          '15:原単価
      .Add("TANKA", "0")          '16:ｋｇ単価
      .Add("KINGAKU", "0")        '17:金額
      .Add("SPKUBUN", "0")        '18:ＳＰ
      .Add("SETCD", "0")          '19:セットコード
      .Add("TKCODE", "0")         '20:得意先コード
      .Add("TANTO", "0")          '21:担当コード
      .Add("KFLG", "0")           '22:更新ＦＬＧ
      .Add("TDATE", "NULL")       '23:登録日付
      .Add("KDATE", "NULL")       '24:更新日
      .Add("KIKAKUC", "0")        '25:規格コード
      .Add("KAKUC", "0")          '26:格付コード
      .Add("KIGENBI", "0")        '27:期限日
      .Add("GENSANCHIC", "0")     '28:原産地コード
      .Add("SYUBETUC", "0")       '29:種別コード
      .Add("BINAME", "NULL")      '30:部位名
      .Add("LSRNAME", "NULL")     '31:仕入先名
      .Add("GBNAME", "NULL")      '32:名称
      .Add("SBNAME", "NULL")      '33:畜種名
      .Add("TANTOMEI", "NULL")    '34:担当者名
      .Add("KKNAME", "NULL")      '35:規格名
      .Add("KZNAME", "NULL")      '36:格付名
      .Add("GNNAME", "NULL")      '37:原産地名
      .Add("LTKNAME", "NULL")     '38:得意先名
      .Add("NSZFLG", "0")         '39:入出庫ＦＬＧ
      .Add("JYOUTAI", "NULL")     '40:状態
      .Add("OROSIBI", "NULL")     '41:棚卸日
      .Add("OROSIKUBUN", "0")     '42:棚卸区分
      .Add("RHONSU", "0")         '43:棚卸前入本数
      .Add("RJYURYO", "0")        '44:棚卸前重量
      .Add("SYUKKABI", "NULL")    '45:出荷日
      .Add("HENPINBI", "NULL")    '46:返品日
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

    ' 選択されている
    sql = SqlUpdData(tmpProcTime)         ' 更新

    ' 実行
    Try
      ' トランザクション開始
      tmpDb.TrnStart()

      ' SQL実行結果が1件か？
      If tmpDb.Execute(sql) = 1 Then
        ' 更新成功
        tmpDb.TrnCommit()
      Else
        ' 更新失敗
        Throw New Exception("返品データの更新に失敗しました。他のユーザーによって修正されています。")
      End If

    Catch ex As Exception
      ' Error
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
    End Try

  End Sub

  ''' <summary>
  ''' データ削除を行う
  ''' </summary>
  Private Sub DeleteDb()
    Dim tmpDb As New clsSqlServer

    Try
      With tmpDb

        ' 削除処理実行（中身はフラグ更新）
        .TrnStart()

        ' 返品データ削除SQL文の作成
        If .Execute(SqlDelCutJ()) = 1 Then
          ' 更新OK
          .TrnCommit()
        Else
          ' 更新失敗
          ' 更新件数は必ず1件です
          Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
        End If

        .TrnStart()

        ' 出庫データの場合
        If .Execute(SqlDelSyukoCutJ()) = 1 Then
          ' 更新OK
          .TrnCommit()
        Else
          ' 更新失敗
          ' 更新件数は必ず1件です
          Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
        End If

        Dim tmpDt As New DataTable
        .GetResult(tmpDt, SqlSelUpdateTargetCutJ())

        ' 対象のCUJは存在するか？
        If tmpDt.Rows.Count > 0 Then

          .TrnStart()

          Dim dtRow As DataRow
          dtRow = tmpDt.Rows(0)

          ' 返品データの場合
          If .Execute(SqlDelHenpinCutJ(dtRow)) = 1 Then
            ' 更新OK
            .TrnCommit()
          Else
            ' 更新失敗
            ' 更新件数は必ず1件です
            Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
          End If
        End If
      End With
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      ' Error ロールバック
      tmpDb.TrnRollBack()
    End Try
  End Sub


  ''' <summary>
  ''' 返品事由データ更新を行う
  ''' </summary>
  ''' <remarks>[登録]ボタン押下時更新処理</remarks>
  Private Sub UpReturnReasonDateDb()

    Dim sql As String = String.Empty
    Dim tmpDb As New clsSqlServer
    Dim tmpProcTime As String = ComGetProcTime()

    ' 選択されている
    sql = SqlUpdReturnReason()         ' 更新

    ' 実行
    Try
      ' トランザクション開始
      tmpDb.TrnStart()

      ' SQL実行結果が1件か？
      If tmpDb.Execute(sql) = 1 Then
        ' 更新成功
        tmpDb.TrnCommit()
      Else
        ' 更新失敗
        Throw New Exception("返品事由データの更新に失敗しました。他のユーザーによって修正されています。")
      End If

    Catch ex As Exception
      ' Error
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
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

    ' 得意先
    With Me.CmbMstCustomer1
      .Enabled = False
    End With

    ' 畜種
    With Me.CmbMstCattle1
      .Enabled = False
    End With

    ' 原産地
    With Me.CmbMstOriginPlace1
      .Enabled = False
    End With

    ' 格付
    With Me.CmbMstRating1
      .Enabled = False
    End With

    ' 枝番
    With Me.TxtEdaban1
      .Enabled = False
      .ReadOnly = True
    End With

    ' 個体識別番号
    With Me.TxtKotaiNo1
      .Enabled = False
      .ReadOnly = True
    End With

    ' 加工日
    With Me.TxtKakouDate
      .Enabled = False
      .ReadOnly = True
    End With

    ' 部位コード
    With Me.TxtLblMstItem1
      .Enabled = False
    End With

    ' カートンNo
    With Me.TxtCartonNumber1
      .Enabled = False
      .ReadOnly = True
    End With

  End Sub

  ''' <summary>
  ''' 編集不可コントロールを使用可に
  ''' </summary>
  Private Sub UnLockCtrl()

    ' 得意先
    With Me.CmbMstCustomer1
      .Enabled = True
    End With

    ' 畜種
    With Me.CmbMstCattle1
      .Enabled = True
    End With

    ' 原産地
    With Me.CmbMstOriginPlace1
      .Enabled = True
    End With

    ' 格付
    With Me.CmbMstRating1
      .Enabled = True
    End With

    ' 枝番
    With Me.TxtEdaban1
      .Enabled = True
      .ReadOnly = False
    End With

    ' 個体識別番号
    With Me.TxtKotaiNo1
      .Enabled = True
      .ReadOnly = False
    End With

    ' 加工日
    With Me.TxtKakouDate
      .Enabled = True
      .ReadOnly = False
    End With

    ' 部位コード
    With Me.TxtLblMstItem1
      .Enabled = True
    End With

    ' カートンNo
    With Me.TxtCartonNumber1
      .Enabled = True
      .ReadOnly = False
    End With

  End Sub

  ''' <summary>
  ''' 入力状態初期化
  ''' </summary>
  Private Sub ResetData()
    ' 返品日付、仕入先、担当者以外の全項目をクリア
    MyBase.AllClear(New List(Of Control)({Me.TxtHenpinDate, Me.CmbMstCustomer1, Me.CmbMstStaff1}))

    ' 選択内容をクリア
    _SelectedData.Clear()

    ' 編集不可コントロールを入力可に
    Call UnLockCtrl()

    ' 畜種（種別）にフォーカスを当てる
    Me.CmbMstCattle1.Focus()

  End Sub

  ''' <summary>
  ''' 画面再表示時処理
  ''' </summary>
  ''' <remarks>
  ''' 非表示→表示時に実行
  ''' FormLoad時に設定
  ''' </remarks>
  Private Sub ReStartPrg()
    Me.TxtHenpinDate.Text = ComGetProcDate()
    Controlz(DG2V1.Name).ShowList()
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

    ' 一覧から選択されていない？
    If (_SelectedData.Keys.Count = 0) Then
      ComMessageBox("入庫されていません！！" & vbCrLf & "登録できません！！", PRG_TITLE, typMsgBox.MSG_NORMAL)
      Call ResetData()
      Exit Sub
    End If

    Try
      Call UpDateDb()   ' 更新
      ' 返品事由コンボボックス空白判定
      If String.IsNullOrWhiteSpace(_SelectedData("RET_CODE")) = False Then
        ' 返品事由コンボボックスが空白以外の場合
        Call UpReturnReasonDateDb()
      End If

      Call ResetData()  ' 入力状態リセット
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
    End Try

    ' 再表示
    Controlz(DG2V1.Name).ShowList(True)

  End Sub

  ''' <summary>
  ''' 削除ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    If IsSelected("削除できません！！") Then

      ' データが選択されているなら、確認メッセージを表示し削除処理実行
      If typMsgBoxResult.RESULT_OK = ComMessageBox("表示中の明細を削除（出庫戻し）しますか？" _
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

    If typMsgBoxResult.RESULT_OK = ComMessageBox("返品情報一覧を画面表示しますか？" _
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

          ' ACCESSの返品情報一覧表レポートを表示
          ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_HENPIN")

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
    Me.TxtHenpinDate.Focus()
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
  Private Sub Form_HENPIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    Me.Text = PRG_TITLE


    Call InitForm02()

    '最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

    Me.TxtHenpinDate.Text = Now().ToString("yyyy/MM/dd")

    ' グリッドダブルクリック時処理追加
    Controlz(DG2V1.Name).lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick

    ' グリッド再表示時処理
    Controlz(DG2V1.Name).lcCallBackReLoadData = AddressOf DgvReload


    ' 返品明細表示
    Controlz(DG2V1.Name).AutoSearch = True

    ' 基本コントロール以外のメッセージを設定
    Controlz(DG2V1.Name).SetMsgLabelText("返品明細を修正したいときは明細行を選びＥｎｔｅｒを押して下さい。")
    TxtHenpinDate.SetMsgLabelText("返品日を入力します。")                           ' 返品日付
    CmbMstCustomer1.SetMsgLabelText("得意先を選択入力します。")                     ' 得意先
    CmbMstCattle1.SetMsgLabelText("畜種（種別）を選択入力します。")                 ' 畜種（種別） 
    CmbMstStaff1.SetMsgLabelText("担当者を選択入力します。")                        ' 担当者
    CmbMstOriginPlace1.SetMsgLabelText("原産地を選択入力します。")                  ' 原産地
    CmbMstRating1.SetMsgLabelText("格付を選択入力します。")                         ' 格付
    TxtEdaban1.SetMsgLabelText("枝番を入力します。枝番が無い場合は入力せずにＥｎｔｅｒを押して下さい。")   '枝番
    TxtKotaiNo1.SetMsgLabelText("個体識別番号を入力します。個体識別番号が無い場合は入力せずにＥｎｔｅｒを押して下さい。")  '個体識別番号
    TxtKakouDate.SetMsgLabelText("加工日を入力します。年月日を６桁数字で入力します。")                     ' 加工日
    TxtLblMstItem1.SetMsgLabelText("部位を入力します。入力なしにＥｎｔｅｒを押すと選択候補が表示されます") '部位コード
    TxtCartonNumber1.SetMsgLabelText("カートン番号（連番）を入力します。カートン番号が無い場合は入力せずにＥｎｔｅｒを押して下さい。")   ' カートンNo
    TxtSyukoCount.SetMsgLabelText("返品数を入力します。")                           ' 出庫数　 
    TxJyuryo1.SetMsgLabelText("重量をｋｇ単位で入力します。")                       ' 重量（kg）
    TxtUnitPrice.SetMsgLabelText("ｋｇ当りの売単価を入力します。")                  ' 返品Kg売単価
    CmbMstHenpin.SetMsgLabelText("返品事由を選択入力します。")           　         ' 返品事由 
    CmbMstSetType1.SetMsgLabelText("セット区分を入力します。")                      ' セット

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)

    ' 個体識別番号入力時処理
    With TxtKotaiNo1
      .lcCallBackEnterEnableKotaiNo = AddressOf EnterEnabelKotaiNo    ' 有効
      .lcCallBackEnterInvalidKotaiNo = AddressOf EnterInvalidKotaiNo  ' 無効
    End With

    ' IPC通信起動
    InitIPC(PRG_ID)

    ' 返品日付にフォーカスをあてる
    Me.ActiveControl = Me.TxtHenpinDate

    ' 再表示時処理
    MyBase.lcCallBackShowFormLc = AddressOf ReStartPrg

  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_HENPIN_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 更新ボタン押下処理
        Me.btnRegist.Focus()
        If (TxtKotaiNo1CancelFlg = False) Then
          Me.btnRegist.PerformClick()
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
      ' F10キー押下時
      Case Keys.F10
        e.Handled = True
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
    Me.Label_GridData.Text = dt.ToString("yyyy年M月d日HH：mm") & " 現在 " & Space(6) & " 返品明細 件数：" & Space(6) & DataCount.ToString()

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

      ' 返品明細表示時は選択された得意先を編集コントロールに設定
      Me.CmbMstCustomer1.SelectedValue = Integer.Parse(_SelectedData("UTKCODE"))                  ' 得意先
      Me.CmbMstCattle1.SelectedValue = Integer.Parse(_SelectedData("SYUBETUC"))                   ' 畜種
      Me.CmbMstOriginPlace1.SelectedValue = Integer.Parse(_SelectedData("GENSANCHIC"))            ' 原産地
      Me.CmbMstRating1.SelectedValue = Integer.Parse(_SelectedData("KAKUC"))                      ' 格付

      Me.TxtEdaban1.Text = _SelectedData("EBCODE")                                                ' 枝番
      Me.TxtKotaiNo1.Text = _SelectedData("KOTAINO")                                              ' 個体識別番号

      If String.IsNullOrWhiteSpace(_SelectedData("KAKOUBI")) Then
        Me.TxtKakouDate.Text = String.Empty
      Else
        Me.TxtKakouDate.Text = Date.Parse(_SelectedData("KAKOUBI")).ToString("yyyy/MM/dd")        ' 加工日
      End If

      Me.TxtLblMstItem1.CodeTxt = _SelectedData("BICODE")                               　        ' 部位
      Me.TxtSayukubun.Text = _SelectedData("SAYUKUBUN")                           　              ' 左右区分
      Me.TxtCartonNumber1.Text = _SelectedData("TOOSINO")                                         ' カートンNo
      Me.TxtSyukoCount.Text = _SelectedData("HONSU")                                              ' 本数
      Me.TxJyuryo1.Text = ((Math.Floor(Long.Parse(_SelectedData("JYURYO")))) / 1000).ToString ' 重量
      Me.TxOldJyuryo.Text = Me.TxJyuryo1.Text

      If (Long.Parse(_SelectedData("HTANKA")) = 0) Then
        Me.TxtUnitPrice.Text = _SelectedData("TANKA")

        ComMessageBox("返品単価が０のため、単価" & _SelectedData("TANKA").ToString & "を返品ｋｇ単価に設定します。", PRG_TITLE, typMsgBox.MSG_NORMAL)

      Else
        Me.TxtUnitPrice.Text = _SelectedData("HTANKA")
      End If

      '     CmbMstStaff1.SelectedValue = Integer.Parse(_SelectedData("TANTO"))                           ' 担当者
      ' 返品事由コンボボックス空白判定
      If String.IsNullOrWhiteSpace(_SelectedData("RET_CODE")) Then
        ' 返品事由コンボボックスが空白以外の場合
        CmbMstHenpin.Enabled = False
        CmbMstHenpin.SelectedValue = 0
      Else
        CmbMstHenpin.Enabled = True
        CmbMstHenpin.SelectedValue = Integer.Parse(_SelectedData("RET_CODE"))                      ' 返品事由
      End If
      CmbMstSetType1.SelectedValue = Integer.Parse(_SelectedData("SETCD"))                         ' セット

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

    ' フォーカスが戻ると元の選択状態に戻る
    DG2V1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
    DG2V1.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText

  End Sub

  Private Sub DataGridView1_Leave(sender As Object, e As EventArgs) Handles DataGridView1.Leave

    ' フォーカスが外れると選択状況を隠す
    DG2V1.DefaultCellStyle.SelectionBackColor = DG2V1.DefaultCellStyle.BackColor
    DG2V1.DefaultCellStyle.SelectionForeColor = DG2V1.DefaultCellStyle.ForeColor

    ' データグリッドのフォーカス喪失時の位置設定
    If DG2V1.SelectedCells.Count > 0 Then
      rowIndexDataGrid = DG2V1.SelectedCells(0).RowIndex
    End If

  End Sub

  ''' <summary>
  ''' セットから入力フォーカスがコントロールを離れた場合
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbMstSetType1_Leave(sender As Object, e As EventArgs) Handles CmbMstSetType1.Leave

    ' データグリッドにフォーカスを当てる
    DataGridView1.Focus()

  End Sub

  ''' <summary>
  ''' 閉じるボタンから入力フォーカスがコントロールを離れた場合
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Leave(sender As Object, e As EventArgs) Handles btnClose.Leave

    ' 担当者にフォーカスを当てる
    CmbMstStaff1.Focus()

  End Sub

#End Region

#Region "テキストボックス"
  ''' <summary>
  ''' バリデーション時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtNyukoDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtHenpinDate.Validating

    ' 未入力なら本日日付を設定する
    With Me.TxtHenpinDate
      If .Text.Length <= 0 Then
        .Text = ComGetProcDate()
      End If
    End With

  End Sub

  ''' <summary>
  ''' 返品日付にフォーカス設定時、返品日付以外の全項目をクリア
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtNyukoDate_Enter(sender As Object, e As EventArgs) Handles TxtHenpinDate.Enter

    ' 返品日付
    If Me.TxtHenpinDate.Text.Trim = "" Then
      Me.TxtHenpinDate.Text = ComGetProcDate()
    End If

    If IsDate(Me.TxtHenpinDate.Text） = False Then
      Me.TxtHenpinDate.Text = ComGetProcDate()
    End If

    ' 一覧の自動検索を停止
    Controlz(DG2V1.Name).AutoSearch = False

    ' 返品日付、担当者以外の全項目をクリア
    MyBase.AllClear(New List(Of Control)({Me.TxtHenpinDate, Me.CmbMstStaff1}))

    ' 返品明細を表示
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
