Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass.clsReport

Imports System

Public Class Form_Zaiko
  Implements IDgvForm02

#Region "定数定義"

  ''' <summary>
  ''' グリッド高さ最大表示
  ''' </summary>
  Private Const GRID_HEIGHT_MAX As Integer = 715
  ''' <summary>
  ''' グリッド幅最大表示
  ''' </summary>
  Private Const GRIDWIDTH_MAX As Integer = 1525
  ''' <summary>
  ''' グリッド配置開始位置（縦）
  ''' </summary>
  Private Const GRID_POS_X As Integer = 5
  ''' <summary>
  ''' グリッド配置開始位置（横）
  ''' </summary>
  Private Const GRID_POS_Y As Integer = 160

  Private Const PRG_ID As String = "zaiko"

#End Region

#Region "メンバ"

#Region "プライベート"
  ' データグリッドのフォーカス喪失時の位置
  Private rowIndexDataGrid As Integer

  ' 更新用画面を閉じないコントロールのリスト
  Private nonCloseControl As New List(Of String) From
    {"CmbMstCustomer_02",
     "CmbMstSetType_02",
     "TxTanka_02",
     "TxBaika_02",
     "ButtonUpdate",
     "ButtonSyuka",
     "CmbMstStaff1"}

#End Region
#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_Zaiko, AddressOf ComRedisplay)
  End Sub
#End Region

#Region "データグリッドビュー操作関連共通"
  '１つ目のDataGridViewオブジェクト格納先
  Private DG2V1 As DataGridView
  '２つ目のDataGridViewオブジェクト格納先
  Private DG2V2 As DataGridView

  ''' <summary>
  ''' 初期化処理
  ''' </summary>
  ''' <remarks>
  ''' コントロールの初期化（Form_Loadで実行して下さい）
  ''' </remarks>
  Private Sub InitForm02() Implements IDgvForm02.InitForm02

    ' データグリッドのフォーカス喪失時の位置初期化
    rowIndexDataGrid = -1

    '１つ目のDataGridViewオブジェクトの設定
    DG2V1 = Me.DataGridView1

    DG2V1.Height = SetFrameInDisp(False)
    DG2V1.Left = GRID_POS_X
    DG2V1.Top = GRID_POS_Y

    '２つ目のDataGridViewオブジェクトの設定
    DG2V2 = Me.DataGridView2

    DG2V2.Height = SetFrameInDisp(False)
    DG2V2.Left = GRID_POS_X
    DG2V2.Top = GRID_POS_Y

    ' グリッド、グリッドのタイトルの幅を統一する
    Label_GridData.Width = GRIDWIDTH_MAX
    DG2V1.Width = GRIDWIDTH_MAX
    DG2V2.Width = GRIDWIDTH_MAX
    Panel_Msg.Width = GRIDWIDTH_MAX

    ' グリッド初期化
    Call InitGrid(DG2V1, CreateGrid2Src1(), CreateGrid2Layout1())

    With DG2V1

      '１つ目のDataGridViewオブジェクトを表示する
      .Visible = True

      '１つ目のDataGridViewオブジェクトの左側4列を固定する
      '.Columns(3).Frozen = True

      ' １つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)

        ' １つ目のDataGridViewオブジェクトの固定列設定
        .FixedRow = 4

        '１つ目のDataGridViewオブジェクトの検索コントロール設定
        .AddSearchControl(Me.CmbMstCustomer_01, "CUTJ.TKCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtEdaban_01, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstSetType_01, "CUTJ.SETCD", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstItem_01, "CUTJ.BICODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtKakoubi_01, "CUTJ.KAKOUBI", typExtraction.EX_LT, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtKakoubi_02, "CUTJ.KAKOUBI", typExtraction.EX_GTE, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtKakoubi_03, "CUTJ.KAKOUBI", typExtraction.EX_LT, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtKakoubi_04, "CUTJ.KAKOUBI", typExtraction.EX_LTE, typColumnKind.CK_Date)    ' △のみ △X共にチェック時条件

        ' 編集可能列設定
        .EditColumnList = CreateGrid2EditCol1()

        ' タイトル以外の並び替え設定
        .AddSortKey("商品名", "SHOHINC")

        ' 左右の並び替え設定
        .AddOrderKey("左右", " ORDER BY CUTJ.SAYUKUBUN,CUTJ.TKCODE,CUTJ.SETCD,CUTJ.EBCODE,CUTJ.SHOHINC,CUTJ.TOOSINO,CUTJ.KAKOUBI",
                               " ORDER BY CUTJ.SAYUKUBUN DESC,CUTJ.TKCODE,CUTJ.SETCD,CUTJ.EBCODE,CUTJ.SHOHINC,CUTJ.TOOSINO,CUTJ.KAKOUBI")

      End With
    End With

    Call InitGrid(DG2V2, CreateGrid2Src2(), CreateGrid2Layout2())

    With DG2V2

      '２つ目のDataGridViewオブジェクトを非表示する
      .Visible = False

      ' ２つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)

        ' ２つ目のDataGridViewオブジェクトの検索コントロール設定
        .AddSearchControl(Me.CmbMstCustomer_01, "CUTJ.TKCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtEdaban_01, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstSetType_01, "CUTJ.SETCD", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstItem_01, "CUTJ.BICODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtKakoubi_01, "CUTJ.KAKOUBI", typExtraction.EX_LT, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtKakoubi_02, "CUTJ.KAKOUBI", typExtraction.EX_GTE, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtKakoubi_03, "CUTJ.KAKOUBI", typExtraction.EX_LT, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtKakoubi_04, "CUTJ.KAKOUBI", typExtraction.EX_LTE, typColumnKind.CK_Date)    ' △のみ △X共にチェック時条件

      End With
    End With

  End Sub

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   画面毎の抽出内容をここに記載する
  ''' </remarks>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1

    Dim sql As String = String.Empty

    ' データベースより現在日付けを取得する
    Dim dt As DateTime = DateTime.Parse(ComGetProcDate())

    sql = ""
    sql &= " SELECT CUTJ.*"
    sql &= "      , TOKUISAKI.LTKNAME AS TOKUISAKI_NAME "
    sql &= "      , CUTJ.SETCD AS SETCD "
    sql &= "      , SHOHIN.HINMEI AS SHOHIN_HINMEI "
    sql &= "      , CUTJ.SHOHINC AS SHOHIN_CODE "
    sql &= "      , KAKU.KZNAME AS KAKU_KZNAME "
    sql &= "      , BUIM.BINAME AS BUIM_BINAME "
    sql &= "      , CUTSR.LSRNAME AS SHIIRE_NAME "
    sql &= "      , GENSN.GNNAME AS GENSAN_NAME "
    sql &= "      , STUFF(STUFF(CUTJ.KIGENBI, 5, 0, '/'), 3, 0, '/') AS KIGEN "                   'yy/mm/dd形式に変換するために、SQLのSTUFF関数で３文字目と５文字目に/を代入
    sql &= "      , CUTJ.SAYUKUBUN "
    sql &= "      , IIf(CUTJ.SAYUKUBUN = " & PARTS_SIDE_LEFT.ToString & ",'左' "
    sql &= "      , IIF(CUTJ.SAYUKUBUN = " & PARTS_SIDE_RIGHT.ToString & ",'右',' ')) AS LR2 "
    sql &= "      , (ROUND((CUTJ.JYURYO / 100), 0, 1) / 10) AS JYURYOK "
    sql &= "      , ROUND((CUTJ.GENKA * ROUND((CUTJ.JYURYO / 100), 0, 1) / 10), 0, 1) AS KINGAKUW "
    sql &= "      , IIF(CUTJ.KAKOUBI <  '" & DateAdd(DateInterval.Day, -14, dt).ToString("yyyy/MM/dd") & "','×', "
    sql &= "        IIF(CUTJ.KAKOUBI <= '" & DateAdd(DateInterval.Day, -7, dt).ToString("yyyy/MM/dd") & "','△','　')) AS WKIGEN "
    sql &= "      , EDAB.EDC "
    sql &= " FROM (((((((CUTJ LEFT JOIN BUIM ON CUTJ.BICODE = BUIM.BICODE) "
    sql &= "                 LEFT JOIN SHOHIN ON CUTJ.SETCD = SHOHIN.SHCODE AND CUTJ.GBFLG = SHOHIN.GBFLG) "
    sql &= "                 LEFT JOIN KAKU ON CUTJ.KAKUC = KAKU.KKCODE) "
    sql &= "                 LEFT JOIN TOKUISAKI ON CUTJ.TKCODE = TOKUISAKI.TKCODE) "
    sql &= "                 LEFT JOIN CUTSR ON CUTJ.SRCODE = CUTSR.SRCODE) "
    sql &= "                 LEFT JOIN GENSN ON CUTJ.GENSANCHIC = GENSN.GNCODE) "
    sql &= "                 LEFT JOIN EDAB ON CUTJ.KOTAINO = EDAB.KOTAINO "
    sql &= "                                and CUTJ.EBCODE = EDAB.EBCODE) "
    sql &= " WHERE   (CUTJ.NSZFLG <= 1 "
    sql &= "      AND CUTJ.DKUBUN = 0 "
    sql &= "      AND CUTJ.KYOKUFLG = 0 "
    sql &= "      AND CUTJ.NKUBUN = 0 "
    sql &= "      AND CUTJ.BICODE <> 1000) "
    sql &= " ORDER BY CUTJ.TKCODE "
    sql &= "        , CUTJ.SETCD "
    sql &= "        , CUTJ.EBCODE "
    sql &= "        , CUTJ.KAKOUBI "

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("期限", "WKIGEN", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("得意先名", "TOKUISAKI_NAME", argFontSize:=11, argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=176))
      .Add(New clsDGVColumnSetting("ｾｯﾄC", "SETCD", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("セット名", "SHOHIN_HINMEI", argFontSize:=11, argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=144))
      .Add(New clsDGVColumnSetting("商品名", "BUIM_BINAME", argFontSize:=11, argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=176))
      .Add(New clsDGVColumnSetting("等級", "KAKU_KZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=128))
      .Add(New clsDGVColumnSetting("製造日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("枝No.", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("上場コード", "EDC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("左右", "LR2", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("ｶｰﾄﾝ", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="#,##0.0"))
      .Add(New clsDGVColumnSetting("原単価", "GENKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("金額", "KINGAKUW", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("売単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("個体識別番号", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=128, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("期限日", "KIGEN", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=128, argFormat:="yy/MM/dd"))
      .Add(New clsDGVColumnSetting("仕入先", "SHIIRE_NAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("原産地", "GENSAN_NAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=176))
      .Add(New clsDGVColumnSetting("入庫日", "KEIRYOBI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=128, argFormat:="yy/MM/dd"))
      .Add(New clsDGVColumnSetting("部位C", "BICODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=96))
      .Add(New clsDGVColumnSetting("標準ｺｰﾄﾞ", "SHOHINC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=96))
      .Add(New clsDGVColumnSetting("格付C", "KAKUC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=96))
      .Add(New clsDGVColumnSetting("原産地C", "GENSANCHIC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=96))
      .Add(New clsDGVColumnSetting("処理日付", "KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=192, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret

  End Function

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  Public Function CreateGrid2EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol1
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
      .Add(New clsDataGridEditTextBox("ｾｯﾄC", prmUpdateFnc:=AddressOf UpDateDb, prmValueType:=VT_NUMBER, prmMaxChar:=2))
      .Add(New clsDataGridEditTextBox("枝No.", prmUpdateFnc:=AddressOf UpDateDb, prmValueType:=VT_NUMBER, prmMaxChar:=4))
      .Add(New clsDataGridEditTextBox("原単価", prmUpdateFnc:=AddressOf UpDateNoReload, prmValueType:=VT_SIGNED_NUMBER, prmIsReload:=False, prmMaxChar:=8))
      .Add(New clsDataGridEditTextBox("売単価", prmUpdateFnc:=AddressOf UpDateNoReload, prmValueType:=VT_SIGNED_NUMBER, prmIsReload:=False, prmMaxChar:=6))
      .Add(New clsDataGridEditTextBox("部位C", prmUpdateFnc:=AddressOf UpDateDb, prmValueType:=VT_NUMBER, prmMaxChar:=4))
      .Add(New clsDataGridEditTextBox("格付C", prmUpdateFnc:=AddressOf UpDateDb, prmValueType:=VT_NUMBER, prmMaxChar:=2))
      .Add(New clsDataGridEditTextBox("原産地C", prmUpdateFnc:=AddressOf UpDateDb, prmValueType:=VT_NUMBER, prmMaxChar:=2))

    End With

    Return ret

  End Function

  ''' <summary>
  ''' ２つ目のDataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   画面毎の抽出内容をここに記載する
  ''' </remarks>
  Private Function CreateGrid2Src2() As String Implements IDgvForm02.CreateGrid2Src2
    Dim sql As String = String.Empty

    ' データベースより現在日付けを取得する
    Dim dt As DateTime = DateTime.Parse(ComGetProcDate())

    sql = ""
    sql &= " SELECT CUTJ.TKCODE "
    sql &= "      , TOKUISAKI.LTKNAME AS TOKUISAKI_NAME "
    sql &= "      , CUTJ.SETCD AS SETCD "
    sql &= "      , SHOHIN.HINMEI AS SHOHIN_HINMEI "
    sql &= "      , COUNT(CUTJ.BICODE) AS KENSU "
    sql &= "      , CUTJ.KAKUC "
    sql &= "      , KAKU.KZNAME AS KAKU_KZNAME "
    sql &= "      , CUTJ.KAKOUBI "
    sql &= "      , CUTJ.EBCODE  "
    sql &= "      , CUTJ.TANKA "
    sql &= "      , CUTJ.GENKA "
    sql &= "      , SUM(ROUND((CUTJ.JYURYO / 100), 0, 1) / 10) AS JYURYOK "
    sql &= "      , SUM(ROUND((CUTJ.GENKA * ROUND((CUTJ.JYURYO / 100), 0, 1) / 10), 0, 1)) AS KINGAKUW "
    sql &= "      , IIF(CUTJ.KAKOUBI <  '" & DateAdd(DateInterval.Day, -14, dt).ToString("yyyy/MM/dd") & "','×', "
    sql &= "        IIF(CUTJ.KAKOUBI <= '" & DateAdd(DateInterval.Day, -7, dt).ToString("yyyy/MM/dd") & "','△','　')) AS WKIGEN "
    sql &= "      , EDAB.KOTAINO "
    sql &= "      , IIf(CUTJ.SAYUKUBUN = " & PARTS_SIDE_LEFT.ToString & ",'左' "
    sql &= "      , IIF(CUTJ.SAYUKUBUN = " & PARTS_SIDE_RIGHT.ToString & ",'右',' ')) AS LR2 "
    sql &= "      , CUTJ.SAYUKUBUN "
    sql &= "      , MAX(CUTJ.KDATE) AS KDATE_MAX "
    sql &= "      , EDAB.EDC "
    sql &= " FROM (((CUTJ LEFT JOIN SHOHIN ON CUTJ.SETCD = SHOHIN.SHCODE AND CUTJ.GBFLG = SHOHIN.GBFLG) "
    sql &= "              LEFT JOIN KAKU ON CUTJ.KAKUC = KAKU.KKCODE)"
    sql &= "              LEFT JOIN TOKUISAKI ON CUTJ.TKCODE = TOKUISAKI.TKCODE) "
    sql &= "              LEFT JOIN EDAB ON CUTJ.KOTAINO = EDAB.KOTAINO "
    sql &= "                             and CUTJ.EBCODE = EDAB.EDC "
    sql &= " WHERE   (CUTJ.NSZFLG <= 1 "
    sql &= "      AND CUTJ.DKUBUN = 0 "
    sql &= "      AND CUTJ.KYOKUFLG = 0 "
    sql &= "      AND CUTJ.NKUBUN = 0 "
    sql &= "      AND CUTJ.BICODE <> 1000) "
    sql &= " GROUP BY CUTJ.TKCODE"
    sql &= "        , TOKUISAKI.LTKNAME"
    sql &= "        , CUTJ.SETCD"
    sql &= "        , SHOHIN.HINMEI"
    sql &= "        , CUTJ.KAKUC"
    sql &= "        , KAKU.KZNAME"
    sql &= "        , CUTJ.KAKOUBI"
    sql &= "        , CUTJ.EBCODE"
    sql &= "        , EDAB.EDC "
    sql &= "        , CUTJ.TANKA"
    sql &= "        , CUTJ.GENKA"
    sql &= "        , IIF(CUTJ.KAKOUBI <  '" & DateAdd(DateInterval.Day, -14, dt).ToString("yyyy/MM/dd") & "','×', "
    sql &= "          IIF(CUTJ.KAKOUBI <= '" & DateAdd(DateInterval.Day, -7, dt).ToString("yyyy/MM/dd") & "','△','　')) "
    sql &= "        , EDAB.KOTAINO"
    sql &= "        , CUTJ.SAYUKUBUN"
    sql &= " ORDER BY CUTJ.SETCD "
    sql &= "        , CUTJ.EBCODE "
    sql &= "        , CUTJ.KAKOUBI; "

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' ２つ目のDataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("期限", "WKIGEN", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("得意先名", "TOKUISAKI_NAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("セット名", "SHOHIN_HINMEI", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=144))
      .Add(New clsDGVColumnSetting("等級", "KAKU_KZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=128))
      .Add(New clsDGVColumnSetting("製造日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("枝No.", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("上場コード", "EDC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("左右", "LR2", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("個体識別番号", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=128, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="#,##0.0"))
      .Add(New clsDGVColumnSetting("原単価", "GENKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("金額", "KINGAKUW", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("個数", "KENSU", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("売単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("更新日付", "KDATE_MAX", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=192, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' ２つ目のDataGridViewオブジェクトの一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  Public Function CreateGrid2EditCol2() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol2
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
    End With

    Return ret
  End Function
#End Region

#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' データグリッド名判定
  ''' </summary>
  ''' <returns>データグリッド名</returns>
  Public Function GetDataGridName() As String

    Dim dataGridName As String

    ' 枝別合計表示チェックボックス判定
    If (CheckBox_EdaBetu.Checked) Then
      dataGridName = DG2V2.Name
    Else
      dataGridName = DG2V1.Name
    End If

    Return dataGridName

  End Function

  ''' <summary>
  ''' データグリッドの件数を取得
  ''' </summary>
  ''' <returns>データグリッドの件数</returns>
  Public Function GetDataGridKensu() As Integer

    Dim strCnt As String = Controlz(GetDataGridName()).SelectedRow("KENSU")
    Dim numCnt As Integer = 0

    ' 実行対象の件数が空白かどうか判定
    If String.IsNullOrWhiteSpace(strCnt) = False Then
      Int32.TryParse(strCnt, numCnt)
    End If

    Return numCnt

  End Function

#End Region

#Region "プライベート"

  ''' <summary>
  ''' 有効期限指定１と２のチェック状態を判定し、データグリッドを再描画
  ''' </summary>
  Private Sub CheckBoxSetDate()

    Dim FormatText As String
    ' データベースより現在日付けを取得する
    FormatText = ComGetProcDate()

    ' 文字列をDateTime値に変換する
    Dim dt As DateTime = DateTime.Parse(FormatText)

    ' 有効期限指定１と２のチェック状態を判定
    If (CheckBox_Sample01.Checked) And (CheckBox_Sample02.Checked) Then
      ' （有効期限指定１と２両方）
      TxtKakoubi_01.Text = ""
      TxtKakoubi_02.Text = ""
      TxtKakoubi_04.Text = DateAdd(DateInterval.Day, -7, dt).ToString("yyyy/MM/dd")
    ElseIf (CheckBox_Sample01.Checked) And (CheckBox_Sample02.Checked = False) Then
      ' （有効期限指定１のみ）
      TxtKakoubi_01.Text = ""
      TxtKakoubi_02.Text = DateAdd(DateInterval.Day, -14, dt).ToString("yyyy/MM/dd")
      TxtKakoubi_04.Text = DateAdd(DateInterval.Day, -7, dt).ToString("yyyy/MM/dd")
    ElseIf (CheckBox_Sample01.Checked = False) And (CheckBox_Sample02.Checked) Then
      ' （有効期限指定２のみ）
      TxtKakoubi_01.Text = DateAdd(DateInterval.Day, -14, dt).ToString("yyyy/MM/dd")
      TxtKakoubi_02.Text = ""
      TxtKakoubi_04.Text = ""
    Else
      ' 初期化
      TxtKakoubi_01.Text = ""
      TxtKakoubi_02.Text = ""
      TxtKakoubi_04.Text = ""
    End If

    'データグリッド再描画
    DataGrid_ShowList()

  End Sub



  ''' <summary>
  ''' データグリッドの再描画
  ''' </summary>
  Private Sub DataGrid_ShowList()

    ' 枝別合計表示チェックボックス判定
    If (CheckBox_EdaBetu.Checked) Then
      With DG2V2
        Controlz(.Name).ShowList()
      End With
    Else
      With DG2V1
        Controlz(.Name).ShowList()
      End With
    End If

  End Sub

  ''' <summary>
  ''' グリッド更新時処理
  ''' </summary>
  ''' <returns>
  '''  True  - 成功
  '''  False - 失敗
  ''' </returns>
  ''' <remarks>更新後の再読み込みを行わない為、更新データをGrid上に手動で書き込む</remarks>
  Private Function UpDateNoReload() As Boolean

    Dim tmpDb As New clsSqlServer()
    Dim ret As Boolean = True

    With tmpDb
      Try
        Dim sql As String = String.Empty
        Dim tmpProcTime As String = ComGetProcTime()
        Dim tmpKey As String = Controlz(DG2V1.Name).EditData.Keys(0)
        Dim tmpVal As String = Controlz(DG2V1.Name).EditData(tmpKey)
        Dim tmpSelectedRow As Dictionary(Of String, String) = Controlz(DG2V1.Name).SelectedRow
        Dim tmpGenka As Long = 0
        Dim tmpJYuryoK As Decimal = 0.0D

        sql = SqlUpdCutJ(Controlz(DG2V1.Name).EditData _
                         , tmpSelectedRow _
                         , tmpProcTime)
        .TrnStart()

        If 1 <> .Execute(sql) Then
          Throw New Exception("CUTJの更新に失敗しました。他のユーザーによって修正されています。")
        End If

        ' 更新項目・更新日をグリッドに設定する
        ' 一覧の再読み込みを行わない為
        Controlz(DG2V1.Name).SetCellVal(tmpKey, DG2V1.SelectedCells(0).RowIndex, tmpVal)
        Controlz(DG2V1.Name).SetCellVal("KDATE", DG2V1.SelectedCells(0).RowIndex, tmpProcTime)

        ' 原単価更新時は金額（原価）の更新を行う
        If tmpKey = "GENKA" Then

          tmpJYuryoK = Decimal.Parse(tmpSelectedRow("JYURYOK"))
          tmpGenka = Long.Parse(tmpVal)                         ' 原単価は入力された値を使用

          Controlz(DG2V1.Name).SetCellVal("KINGAKUW", DG2V1.SelectedCells(0).RowIndex, Math.Truncate((tmpJYuryoK * tmpGenka)).ToString())

        End If

        .TrnCommit()

        ' データグリッドの重量と金額を合計を再描画
        dispTotalKingaku()

      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
        .TrnRollBack()
        ret = False
      Finally
        tmpDb.Dispose()
      End Try

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 一覧変更時イベント
  ''' </summary>
  ''' <param name="flgNumeric">
  '''  True   -   明細（入荷単価、売上単価）を直接修正
  '''  False  -   明細をダブルクリックし、得意先、セット、入荷単価、売上単価を修正
  ''' </param>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function UpDateDb(Optional flgNumeric As Boolean = True, Optional flgEda As Boolean = True) As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True
    Dim sqlKensu As Integer = 0

    ' 実行
    With tmpDb
      Try
        ' トランザクション開始
        .TrnStart()

        ' SQL文の作成方法による分岐
        If (flgNumeric) Then

          ' 明細（入荷単価、売上単価）を直接修正

          sql = SqlUpdCutJ(Controlz(GetDataGridName()).EditData _
                         , Controlz(GetDataGridName()).SelectedRow)

          ' 実行対象の件数を1件に設定
          sqlKensu = 1

        Else
          ' 明細をダブルクリックし、得意先、セット、入荷単価、売上単価を修正
          ' 得意先の選択テキストを取得
          Dim setTokuisakiCmbText As String = CmbMstCustomer_02.Text
          ' 得意先が未入力もしくは未選択の場合
          If String.IsNullOrWhiteSpace(setTokuisakiCmbText) Then
            setTokuisakiCmbText = ""
          Else
            ' 得意先コードを取得
            setTokuisakiCmbText = setTokuisakiCmbText.Substring(0, 4)
          End If

          ' セットの選択テキストを取得
          Dim setCdCmbText As String = CmbMstSetType_02.Text
          ' セットが未入力もしくは未選択の場合
          If String.IsNullOrWhiteSpace(setCdCmbText) Then
            setCdCmbText = ""
          Else
            ' セットコードを取得
            setCdCmbText = setCdCmbText.Substring(0, setCdCmbText.IndexOf(":"))
          End If
          ' CutJ更新SQL文作成
          ' 枝別合計表示チェックボックス判定
          If (CheckBox_EdaBetu.Checked) Then
            sql = SqlUpdEdaCutJ(setTokuisakiCmbText _
                              , setCdCmbText _
                              , TxTanka_02.Text _
                              , TxBaika_02.Text _
                              , Controlz(GetDataGridName()).SelectedRow)

            ' 実行対象の件数を取得
            sqlKensu = GetDataGridKensu()
            If sqlKensu = 0 Then
              ' 更新失敗
              Throw New Exception("件数の取得に失敗しました。")
            End If

          Else

            sql = SqlUpdCutJ(setTokuisakiCmbText _
                         , setCdCmbText _
                         , TxTanka_02.Text _
                         , TxBaika_02.Text _
                         , Controlz(GetDataGridName()).SelectedRow)

            ' 実行対象の件数を1件に設定
            sqlKensu = 1

          End If
        End If

        ' SQL実行結果が指定した件数か？
        If .Execute(sql) = sqlKensu Then
          ' 更新成功
          .TrnCommit()
          'データグリッド再描画
          DataGrid_ShowList()

        Else
          ' 更新失敗
          Throw New Exception("CutJの更新に失敗しました。他のユーザーによって修正されています。")
        End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
        .TrnRollBack()                   ' RollBack
        ret = False
      End Try

    End With

    Return ret

  End Function

  ''' <summary>
  ''' 更新用画面を閉じる
  ''' </summary>
  Private Sub Frame_UnDsp()

    ' 更新用画面が表示中の場合
    If (Frame_IN.Visible) Then

      ' 枝別合計表示チェックボックスがチェック状態を判定
      If (CheckBox_EdaBetu.Checked) Then
        DG2V2.Height = SetFrameInDisp(False)
      Else
        DG2V1.Height = SetFrameInDisp(False)
      End If

    End If

  End Sub

  ''' <summary>
  ''' 更新用画面を閉じる
  ''' </summary>
  Private Sub chkFrame_UnDsp()

    ' アクティブなコントロールを取得
    Dim c As Control = Me.ActiveControl
    Dim runFlg As Boolean = True

    ' 取得できた場合はコンソールに出力
    If Not c Is Nothing Then

      ' 更新用画面を閉じないコントロールのリスト分繰り返す
      For Each item In nonCloseControl
        ' 更新用画面を閉じないコントロールの場合
        If c.Name.Equals(item) Then
          runFlg = False
          Exit For
        End If
      Next

      ' 更新用画面を閉じないコントロールの場合、更新用画面を閉じない
      If (runFlg) Then
        Frame_UnDsp()
      End If

    End If

  End Sub

  ''' <summary>
  ''' 在庫一覧表印刷処理（出力ワークの作成とレポートの表示）
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function printZaiko() As Boolean

    Dim tmpDb As New clsReport(clsGlobalData.REPORT_FILENAME)

    ' 実行
    With tmpDb

      Try
        ' SQL文の作成
        .Execute("DELETE FROM WK_ZAIKO")

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("ワークテーブルの削除に失敗しました")

      End Try

      Try
        ' トランザクション開始
        .TrnStart()

        'グリッドから追加SQL文を作成
        For Each item As Dictionary(Of String, String) In Controlz(GetDataGridName()).GetAllData
          .Execute(SqlInsZaiko01(item))
        Next

        ' 更新成功
        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("ワークテーブルの書き込みに失敗しました")
      End Try

      .Dispose()

    End With

    ' ACCESSの在庫一覧表レポートを表示
    ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_ZAIKO")

    Return True

  End Function

  ''' <summary>
  ''' 枝別在庫一覧印刷処理（出力ワークの作成とレポートの表示）
  ''' </summary>
  ''' <param name="edaNo"></param>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function printZaiko(edaNo As String) As Boolean

    Dim tmpDb As New clsReport(clsGlobalData.REPORT_FILENAME)

    ' 実行
    With tmpDb

      Try
        ' SQL文の作成
        .Execute("DELETE FROM WK_ZAIKO")

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("ワークテーブルの削除に失敗しました")

      End Try

      Try
        ' トランザクション開始
        .TrnStart()

        'グリッドから追加SQL文を作成
        For Each item As Dictionary(Of String, String) In Controlz(GetDataGridName()).GetAllData
          .Execute(SqlInsZaiko02(item))
        Next

        ' 更新成功
        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("ワークテーブルの書き込みに失敗しました")
      End Try

      .Dispose()

    End With

    ' ACCESSの枝別在庫一覧レポートを表示
    ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_EDAZAIKO")

    Return True

  End Function

  ''' <summary>
  ''' 更新用画面の表示有無設定
  ''' </summary>
  ''' <param name="dspFlag">更新用画面の表示有無</param>
  ''' <returns>データグリッドの高さ</returns>
  Private Function SetFrameInDisp(dspFlag As Boolean) As Integer

    Dim gridHeight As Integer = 0

    ' 更新用画面の表示有無設定
    Frame_IN.Visible = dspFlag

    ' 更新用画面の表示有無判定
    If dspFlag Then
      ' 更新用画面が表示時は、データグリッドを更新画面の高さ分、縮める
      gridHeight = GRID_HEIGHT_MAX - Frame_IN.Height
    Else
      ' 更新用画面が非表示時は、データグリッド情報画面の高さ分、縮める
      gridHeight = GRID_HEIGHT_MAX - Panel_Msg.Height
    End If

    Return gridHeight

  End Function

  ''' <summary>
  ''' データグリッド更新時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate">最終更新日時</param>
  ''' <param name="DataCount">データ件数</param>
  Private Sub DgvReload(sender As DataGridView, LastUpdate As String, DataCount As Long)

    Me.Label_GridData.AutoSize = False
    Me.Label_GridData.TextAlign = ContentAlignment.MiddleCenter
    '文字列をDateTime値に変換する
    Dim dt As DateTime = DateTime.Parse(LastUpdate)
    Me.Label_GridData.Text = dt.ToString("yyyy年M月d日HH：mm") & " 現在 " & Space(6) & " 出 荷 一 覧 " & Space(6) & DataCount.ToString() & "件"

    dispTotalKingaku()

  End Sub

  ''' <summary>
  ''' データグリッドの重量と金額を合計を再描画
  ''' </summary>
  Private Sub dispTotalKingaku()

    Dim totalJyuryou As Double = 0
    Dim totalKingaku As Long = 0
    Dim dgvRows As DataGridViewRowCollection

    ' 枝別合計表示チェックボックス判定
    If (CheckBox_EdaBetu.Checked) Then
      dgvRows = DG2V2.Rows
    Else
      dgvRows = DG2V1.Rows
    End If

    ' データグリッドの重量と金額を合計
    For Each row As DataGridViewRow In dgvRows
      totalKingaku += ComBlank2Zero(row.Cells("KINGAKUW").Value.ToString())
      totalJyuryou += CType(row.Cells("JYURYOK").Value, Double)
    Next

    Me.Label_Kensu.Text = "現在在庫　重量" & totalJyuryou.ToString("#,##0.0") & "Ｋｇ" & "   金額  " & totalKingaku.ToString("#,##0") & "円"

  End Sub

  ''' <summary>
  ''' 枝別合計表示チェックボックス選択時再描画処理
  ''' </summary>
  Private Sub CheckBoxEdaBetuReload()

    ' データグリッドのフォーカス喪失時の位置初期化
    rowIndexDataGrid = -1

    ' 枝別合計表示チェックボックスのチェック状態を判定
    If (CheckBox_EdaBetu.Checked) Then

      '２つ目のDataGridViewオブジェクトを表示する
      With DG2V2
        .Visible = True
        Controlz(.Name).AutoSearch = True
        .Height = SetFrameInDisp(False)
      End With

      ' 枝別合計表示チェックボックスがチェック選択済の場合
      '１つ目のDataGridViewオブジェクトを非表示にする
      With DG2V1
        .Visible = False
        Controlz(.Name).AutoSearch = False
      End With

      ' 取消ボタン選択不可にする
      ButtonTorikeshi.Enabled = False

    Else
      ' 枝別合計表示チェックボックスがチェック未選択の場合
      '１つ目のDataGridViewオブジェクトを表示する
      With DG2V1
        .Visible = True
        Controlz(.Name).AutoSearch = True
      End With

      '２つ目のDataGridViewオブジェクトを非表示にする
      With DG2V2
        .Visible = False
        Controlz(.Name).AutoSearch = False
      End With

      ' 取消ボタン選択可能にする
      ButtonTorikeshi.Enabled = True
    End If

    Frame_UnDsp()

    lblInformation.Text = String.Empty

    'データグリッド再描画
    DataGrid_ShowList()

  End Sub

#End Region

#End Region

#Region "イベントプロシージャ"

#Region "フォーム関連"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Zaiko_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = "在庫問合せ画面"

    '最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

    Call InitForm02()

    ' グリッドダブルクリック時処理追加
    Controlz(DG2V1.Name).lcCallBackCellDoubleClick = AddressOf Dgv1CellDoubleClick 'ダブルクリック時イベント設定
    Controlz(DG2V2.Name).lcCallBackCellDoubleClick = AddressOf Dgv2CellDoubleClick 'ダブルクリック時イベント設定

    ' グリッド再表示時処理
    Controlz(DG2V1.Name).lcCallBackReLoadData = AddressOf DgvReload
    Controlz(DG2V2.Name).lcCallBackReLoadData = AddressOf DgvReload

    Controlz(DG2V1.Name).AutoSearch = True

    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Me.CmbMstCustomer_01

    Controlz(DG2V1.Name).SetMsgLabelText("明細のダブルクリックで修正できます。ｾｯﾄC,枝№,原単価,売単価,部位C,格付C,原産地Cは直接変更可。")
    Controlz(DG2V2.Name).SetMsgLabelText("明細のダブルクリックで修正できます。")

    ' メッセージラベル設定
    lblInformation.Text = String.Empty
    Call SetMsgLbl(Me.lblInformation)

    'ボタンのテキスト設定
    '終了ボタン
    ButtonEnd.Text = "F12：終了"
    '印刷ボタン
    ButtonPrint.Text = "F9：一覧" & vbCrLf & "印刷"
    '取消ボタン
    ButtonTorikeshi.Text = "F5：取消"
    '修正ボタン
    ButtonUpdate.Text = "F1：修正"
    '出荷ボタン
    ButtonSyuka.Text = "F3：出荷"

    ' IPC通信起動
    InitIPC(PRG_ID)

  End Sub

#End Region

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Shuka_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 更新ボタン押下処理
        If Me.Frame_IN.Visible Then
          Me.ButtonUpdate.Focus()
          Me.ButtonUpdate.PerformClick()
        End If
      ' F3キー押下時
      Case Keys.F3
        ' 出荷ボタン押下処理
        Me.ButtonSyuka.Focus()
        Me.ButtonSyuka.PerformClick()
      ' F5キー押下時
      Case Keys.F5
        ' 取消ボタン押下処理
        If Me.ButtonTorikeshi.Visible Then
          Me.ButtonTorikeshi.Focus()
          Me.ButtonTorikeshi.PerformClick()
        End If
      ' F9キー押下時
      Case Keys.F9
        ' 印刷ボタン押下処理
        Me.ButtonPrint.Focus()
        Me.ButtonPrint.PerformClick()
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        Me.ButtonEnd.Focus()
        Me.ButtonEnd.PerformClick()
    End Select
  End Sub

#Region "チェックボックス関連"

  ''' <summary>
  ''' 枝別合計表示チェックボックス選択時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CheckBox_EdaBetu_Click(sender As Object, e As EventArgs) Handles CheckBox_EdaBetu.Click

    ' 枝別合計表示チェックボックス選択時再描画
    CheckBoxEdaBetuReload()

    ' 枝テキストボックスにフォーカス設定
    Me.TxtEdaban_01.Focus()

  End Sub

  ''' <summary>
  ''' △抽出（製造後1週間～2週間まで）チェックボックス選択時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CheckBox_Sample01_Click(sender As Object, e As EventArgs) Handles CheckBox_Sample01.Click

    CheckBoxSetDate()

    ' 枝テキストボックスにフォーカス設定
    Me.TxtEdaban_01.Focus()

  End Sub

  ''' <summary>
  ''' ×抽出（製造後2週間経過）チェックボックス選択時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CheckBox_Sample02_Click(sender As Object, e As EventArgs) Handles CheckBox_Sample02.Click

    CheckBoxSetDate()

    ' 枝テキストボックスにフォーカス設定
    Me.TxtEdaban_01.Focus()

  End Sub

#End Region

#Region "データグリッド関連"

  ''' <summary>
  ''' データグリッドセル選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click

    '更新用画面を閉じる
    Frame_UnDsp()

  End Sub

  ''' <summary>
  ''' データグリッドフォーカス選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter

    '更新用画面を閉じる
    Frame_UnDsp()

    With DG2V1
      'フォーカスが戻ると元の選択状態に戻る
      .DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
      .DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
    End With

  End Sub

  ''' <summary>
  ''' グリッドフォーカス非選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_Leave(sender As Object, e As EventArgs) Handles DataGridView1.Leave

    With DG2V1
      'フォーカスが外れると選択状況を隠す
      .DefaultCellStyle.SelectionBackColor = .DefaultCellStyle.BackColor
      .DefaultCellStyle.SelectionForeColor = .DefaultCellStyle.ForeColor

      ' データグリッドのフォーカス喪失時の位置設定
      If .SelectedCells.Count > 0 Then
        rowIndexDataGrid = .SelectedCells(0).RowIndex
      End If
    End With

    chkFrame_UnDsp()

  End Sub

  ''' <summary>
  ''' グリッドフォーカスダブルクリック時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Dgv1CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    With DG2V1

      .Height = SetFrameInDisp(True)

      ' １つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)
        ' 製造日
        Label_SeizouBi.Text = .SelectedRow("KAKOUBI")
        ' 枝No
        Label_EdaNo_02.Text = .SelectedRow("EBCODE")
        ' 左右
        Label_Sayu.Text = .SelectedRow("LR2")
        ' カートン
        Label_KotaiNo.Text = .SelectedRow("TOOSINO")
        Label_KotaiNo.Visible = True

        ' 重量
        Dim numJyuryou As Double = 0
        numJyuryou = CType(.SelectedRow("JYURYOK"), Double)
        Label_Jyuryo.Text = numJyuryou.ToString("#,##0.0")
        ' 得意先コンボボックス
        Dim Number1 As Integer = 0
        Int32.TryParse(.SelectedRow("TKCODE"), Number1)
        Dim SearchStr1 As String = String.Format("{0:D4}:{1}", Number1, .SelectedRow("TOKUISAKI_NAME"))
        CmbMstCustomer_02.SelectedIndex = CmbMstCustomer_02.FindStringExact(SearchStr1)
        ' セットコンボボックス
        Dim Number2 As Integer = 0
        Int32.TryParse(.SelectedRow("SETCD"), Number2)
        Dim SearchStr2 As String = String.Format("{0:D4}:{1}", Number2, .SelectedRow("SHOHIN_HINMEI"))
        CmbMstSetType_02.SelectedIndex = CmbMstSetType_02.FindStringExact(SearchStr2)
        ' 入荷単価テキストボックス
        TxTanka_02.Text = .SelectedRow("GENKA")
        ' 売上単価テキストボックス
        TxBaika_02.Text = .SelectedRow("TANKA")

      End With
    End With

  End Sub

  ''' <summary>
  ''' データグリッドセル選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView2_Click(sender As Object, e As EventArgs) Handles DataGridView2.Click

    '更新用画面を閉じる
    Frame_UnDsp()

  End Sub

  ''' <summary>
  ''' データグリッドフォーカス選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEnter

    'フォーカスが戻ると元の選択状態に戻る
    DG2V2.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
    DG2V2.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText

  End Sub

  ''' <summary>
  ''' グリッドフォーカス非選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView2_Leave(sender As Object, e As EventArgs) Handles DataGridView2.Leave

    'フォーカスが外れると選択状況を隠す
    DG2V2.DefaultCellStyle.SelectionBackColor = DG2V2.DefaultCellStyle.BackColor
    DG2V2.DefaultCellStyle.SelectionForeColor = DG2V2.DefaultCellStyle.ForeColor

    ' データグリッドのフォーカス喪失時の位置設定
    If DG2V2.SelectedCells.Count > 0 Then
      rowIndexDataGrid = DG2V2.SelectedCells(0).RowIndex
    End If

    chkFrame_UnDsp()

  End Sub

  ''' <summary>
  ''' グリッドフォーカスダブルクリック時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Dgv2CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    With DG2V2

      .Height = SetFrameInDisp(True)

      ' １つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)
        '製造日
        Label_SeizouBi.Text = .SelectedRow("KAKOUBI")
        '枝No
        Label_EdaNo_02.Text = .SelectedRow("EBCODE")
        '左右
        Label_Sayu.Text = .SelectedRow("LR2")
        '重量
        Dim numJyuryou As Double = 0
        Label_KotaiNo.Visible = False
        numJyuryou = CType(.SelectedRow("JYURYOK"), Double)
        Label_Jyuryo.Text = numJyuryou.ToString("#,##0.0")
        '得意先コンボボックス
        Dim Number1 As Integer = 0
        Int32.TryParse(.SelectedRow("TKCODE"), Number1)
        Dim SearchStr1 As String = String.Format("{0:D4}:{1}", Number1, .SelectedRow("TOKUISAKI_NAME"))
        CmbMstCustomer_02.SelectedIndex = CmbMstCustomer_02.FindStringExact(SearchStr1)
        'セットコンボボックス
        Dim Number2 As Integer = 0
        Int32.TryParse(.SelectedRow("SETCD"), Number2)
        Dim SearchStr2 As String = String.Format("{0:D4}:{1}", Number2, .SelectedRow("SHOHIN_HINMEI"))
        CmbMstSetType_02.SelectedIndex = CmbMstSetType_02.FindStringExact(SearchStr2)
        '入荷単価テキストボックス
        TxTanka_02.Text = .SelectedRow("GENKA")
        '売上単価テキストボックス
        TxBaika_02.Text = .SelectedRow("TANKA")

      End With
    End With

  End Sub

#End Region

  ''' <summary>
  ''' 更新用画面からフォーカスが外れた場合
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Frame_IN_Leave(sender As Object, e As EventArgs) Handles Frame_IN.Leave

    chkFrame_UnDsp()

  End Sub

  ''' <summary>
  ''' 担当差yコンボボックスからフォーカスが外れた場合
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbMstStaff1_Leave(sender As Object, e As EventArgs) Handles CmbMstStaff1.Leave

    chkFrame_UnDsp()

  End Sub

  ''' <summary>
  ''' アプリケーション終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonEnd_Click(sender As Object, e As EventArgs) Handles ButtonEnd.Click

    Controlz(DG2V1.Name).AutoSearch = False
    Controlz(DG2V2.Name).AutoSearch = False
    Controlz(DG2V1.Name).ResetPosition()
    Controlz(DG2V2.Name).ResetPosition()
    Controlz(DG2V1.Name).InitSort()
    Controlz(DG2V2.Name).InitSort()

    Me.Hide()

    ' コンボボックス・テキストボックスの入力初期化
    MyBase.AllClear()

    ' チェックボックス解除
    CheckBox_Sample01.Checked = False
    CheckBox_Sample02.Checked = False
    CheckBox_EdaBetu.Checked = False

    Me.CmbMstCustomer_01.Focus()

    ' 単品表示に更新
    Call CheckBoxEdaBetuReload()


  End Sub

  ''' <summary>
  ''' 印刷ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click

    Dim rtn As typMsgBoxResult

    If (CheckBox_EdaBetu.Checked) Then
      ' データグリッドが０件の場合、処理を行わない
      If (DG2V2.Rows.Count < 1) Then
        Return
      End If
    Else
      ' データグリッドが０件の場合、処理を行わない
      If (DG2V1.Rows.Count < 1) Then
        Return
      End If
    End If

    ' 枝別合計表示チェックボックスのチェックを外す
    CheckBox_EdaBetu.Checked = False

    CheckBoxEdaBetuReload()

    ' 印刷プレビューの表示有無 
    If clsGlobalData.PRINT_PREVIEW = 1 Then
      rtn = clsCommonFnc.ComMessageBox("在庫一覧表の画面表示を行います？",
                                     "在庫一覧",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OKCANCEL)
    Else
      rtn = clsCommonFnc.ComMessageBox("印刷を行います。よろしいですか？",
                                     "在庫一覧",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OKCANCEL)
    End If

    ' 確認メッセージボックスで、ＯＫボタン選択時
    If rtn = typMsgBoxResult.RESULT_OK Then

      '印刷ボタン非表示
      ButtonPrint.Enabled = False

      Try
        '枝テキストボックスが空白の場合
        If String.IsNullOrWhiteSpace(Me.TxtEdaban_01.Text) Then
          printZaiko()
        Else
          printZaiko(Me.TxtEdaban_01.Text)
        End If

      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      End Try

      '印刷ボタン再表示
      ButtonPrint.Enabled = True

    End If

  End Sub

  ''' <summary>
  ''' 更新ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click

    Dim msg As String = ""
    Dim kakoubi As String = Controlz(GetDataGridName()).SelectedRow("KAKOUBI")
    msg = "製造日：" & kakoubi.Substring(0, 10) _
        & "　枝No：" & Controlz(GetDataGridName()).SelectedRow("EBCODE") _
        & "　のデータを更新します。" _
        & vbCrLf _
        & "更新：「ＯＫ」  やり直し：「キャンセル」"


    Dim rtn As typMsgBoxResult
    rtn = clsCommonFnc.ComMessageBox(msg,
                                     "在庫修正処理",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OKCANCEL)
    ' 確認メッセージボックスで、ＯＫボタン選択時
    If rtn = typMsgBoxResult.RESULT_OK Then
      '更新用SQL実行
      UpDateDb(False)

      'データグリッド再描画
      DataGrid_ShowList()

      '更新用画面を閉じる
      Frame_UnDsp()

    End If

  End Sub

  ''' <summary>
  ''' 取消ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonTorikeshi_Click(sender As Object, e As EventArgs) Handles ButtonTorikeshi.Click

    Dim tmpSubForm As New Form_ZaikoTorikeshi

    ' データグリッドが０件の場合、処理を行わない
    If (DG2V1.Rows.Count < 1) Then
      Return
    End If

    ' データｸﾞﾘｯﾄﾞが未選択の場合
    If DG2V1.CurrentCell Is Nothing Then
      ' フォーカス喪失時の列が設定済かどうか
      If (rowIndexDataGrid = -1) Then
        ' 先頭の列を選択状態とする
        DG2V1.CurrentCell = DG2V1(DG2V1.FirstDisplayedScrollingColumnIndex, DG2V1.FirstDisplayedScrollingRowIndex)
      Else
        ' フォーカス喪失時の列を選択状態とする
        DG2V1.CurrentCell = DG2V1(DG2V1.FirstDisplayedScrollingColumnIndex, rowIndexDataGrid)
      End If
    End If

    Try
      Select Case tmpSubForm.ShowSubForm(Controlz(DG2V1.Name).SelectedRow, Me)
        Case SFBase.typSfResult.SF_OK
          'データグリッド再描画
          DataGrid_ShowList()

          '更新用画面を閉じる
          Frame_UnDsp()

        Case SFBase.typSfResult.SF_CANCEL
      End Select

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' 出荷ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonSyuka_Click(sender As Object, e As EventArgs) Handles ButtonSyuka.Click

    Dim tmpSubForm As New Form_ShukaDateInput(Me)

    Try
      Select Case tmpSubForm.ShowSubForm(Controlz(GetDataGridName()).SelectedRow, Me)
        Case SFBase.typSfResult.SF_OK

          'データグリッド再描画
          DataGrid_ShowList()

          '更新用画面を閉じる
          Frame_UnDsp()

        Case SFBase.typSfResult.SF_CANCEL
      End Select

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

#End Region

#Region "SQL関連"

  ''' <summary>
  ''' CutJ更新SQL文作成
  ''' </summary>
  ''' <param name="prmEditData">編集された値</param>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutJ(prmEditData As Dictionary(Of String, String) _
                            , prmSelectedRow As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty
    'Dim tmpDb As New clsSqlServer
    'Dim tmpDt As New DataTable


    Dim setTxt As String = prmEditData(prmEditData.Keys(0))
    ' 入力値が空白判定
    If String.IsNullOrWhiteSpace(setTxt) Then
      ' 入力値が空白の場合は0を代入
      setTxt = "0"
    End If

    sql &= " UPDATE CUTJ "
    sql &= " SET " & prmEditData.Keys(0) & " =" & setTxt

    ''個体識別番号を設定
    'If prmEditData.ContainsKey("EBCODE") Then
    '  tmpDb.GetResult(tmpDt, "SELECT KOTAINO FROM EDAB WHERE EBCODE = " & prmEditData("EBCODE") & "ORDER BY SIIREBI DESC ")
    '  sql &= (" ,KOTAINO = " & If(tmpDt.Rows.Count = 0, "0", tmpDt(0).Item("KOTAINO").ToString))
    'End If

    ' SP区分を設定
    If (prmEditData.Keys(0).Equals("SETCD")) Then
      If Val(setTxt) = 0 Then
        sql &= (" ,SPKUBUN = 0 ")
      Else
        sql &= (" ,SPKUBUN = 1 ")
      End If
    End If

    sql &= " 　 ,KDATE = '" & ComGetProcTime() & "'"
    sql &= " WHERE EBCODE =    " & prmSelectedRow("EBCODE")
    sql &= "   AND KDATE =   ' " & prmSelectedRow("KDATE") & "'"
    sql &= "   AND BICODE =    " & prmSelectedRow("BICODE")
    sql &= "   AND TOOSINO =   " & prmSelectedRow("TOOSINO")
    sql &= "   AND KAKOUBI = ' " & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG <= 1 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KYOKUFLG = 0 "
    sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")
    sql &= "   AND TKCODE =    " & prmSelectedRow("TKCODE")

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' CutJ更新SQL文作成
  ''' </summary>
  ''' <param name="prmEditData">編集された値</param>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <param name="prmProcTime">更新日時</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutJ(prmEditData As Dictionary(Of String, String) _
                            , prmSelectedRow As Dictionary(Of String, String) _
                            , prmProcTime As String) As String

    Dim sql As String = String.Empty

    Dim setTxt As String = prmEditData(prmEditData.Keys(0))
    ' 入力値が空白判定
    If String.IsNullOrWhiteSpace(setTxt) Then
      ' 入力値が空白の場合は0を代入
      setTxt = "0"
    End If

    sql &= " UPDATE CUTJ "
    sql &= " SET " & prmEditData.Keys(0) & " =" & setTxt

    ' SP区分を設定
    If (prmEditData.Keys(0).Equals("SETCD")) Then
      If Val(setTxt) = 0 Then
        sql &= (" ,SPKUBUN = 0 ")
      Else
        sql &= (" ,SPKUBUN = 1 ")
      End If
    End If

    sql &= "    ,KDATE =      '" & prmProcTime & "'"
    sql &= " WHERE EBCODE =    " & prmSelectedRow("EBCODE")
    sql &= "   AND KDATE =   ' " & prmSelectedRow("KDATE") & "'"
    sql &= "   AND BICODE =    " & prmSelectedRow("BICODE")
    sql &= "   AND TOOSINO =   " & prmSelectedRow("TOOSINO")
    sql &= "   AND KAKOUBI = ' " & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG <= 1 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KYOKUFLG = 0 "
    sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")
    sql &= "   AND TKCODE =    " & prmSelectedRow("TKCODE")

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' CutJ更新SQL文作成（修正）
  ''' </summary>
  ''' <param name="prmTokuisaki">得意先コード</param>
  ''' <param name="prmSetCd">セットコード</param>
  ''' <param name="prmTanka">原価</param>
  ''' <param name="prmBaika">売価</param>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutJ(prmTokuisaki As String _
                            , prmSetCd As String _
                            , prmTanka As String _
                            , prmBaika As String _
                            , prmSelectedRow As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty
    sql &= " UPDATE CUTJ "
    sql &= " SET "

    Dim updateList As New List(Of String)

    ' 得意先が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTokuisaki) = False Then
      ' 得意先が空白以外の場合
      updateList.Add(" TKCODE = " & prmTokuisaki)
      updateList.Add(" UTKCODE = " & prmTokuisaki)
    End If

    ' セットが空白かどうか判定
    If String.IsNullOrWhiteSpace(prmSetCd) = False Then
      ' セットが空白以外の場合
      updateList.Add(" SETCD = " & prmSetCd)
    End If

    ' 原価が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTanka) = False Then
      ' 原価が空白以外の場合
      updateList.Add(" GENKA = " & prmTanka)
    Else
      ' 原価が空白の場合は0を代入
      updateList.Add(" GENKA = 0 ")
    End If

    ' 売原価が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmBaika) = False Then
      ' 売原価が空白以外の場合
      updateList.Add(" TANKA = " & prmBaika)
      updateList.Add(" BAIKA = " & prmBaika)
    Else
      ' 売原価が空白の場合は0を代入
      updateList.Add(" TANKA = 0 ")
      updateList.Add(" BAIKA = 0 ")
    End If

    ' SP区分を設定
    If Val(prmSetCd) = 0 Then
      updateList.Add(" SPKUBUN = 0 ")
    Else
      updateList.Add(" SPKUBUN = 1 ")
    End If

    sql &= String.Join(",", updateList.ToArray)
    sql &= ", KDATE = '" & ComGetProcTime() & "'"
    sql &= " WHERE EBCODE =    " & prmSelectedRow("EBCODE")
    sql &= "   AND BICODE =    " & prmSelectedRow("BICODE")
    sql &= "   AND TOOSINO =   " & prmSelectedRow("TOOSINO")
    sql &= "   AND KDATE =   ' " & prmSelectedRow("KDATE") & "'"
    sql &= "   AND KAKOUBI = ' " & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG <= 1 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KYOKUFLG = 0 "
    sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")
    sql &= "   AND TKCODE =    " & prmSelectedRow("TKCODE")

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' CutJ更新SQL文作成（枝別集計修正）
  ''' </summary>
  ''' <param name="prmTokuisaki">得意先コード</param>
  ''' <param name="prmSetCd">セットコード</param>
  ''' <param name="prmTanka">原価</param>
  ''' <param name="prmBaika">売価</param>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdEdaCutJ(prmTokuisaki As String _
                               , prmSetCd As String _
                               , prmTanka As String _
                               , prmBaika As String _
                               , prmSelectedRow As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty
    sql &= " UPDATE CUTJ "
    sql &= " SET "

    Dim updateList As New List(Of String)

    ' 得意先が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTokuisaki) = False Then
      ' 得意先が空白以外の場合
      updateList.Add(" TKCODE = " & prmTokuisaki)
      updateList.Add(" UTKCODE = " & prmTokuisaki)
    End If

    ' セットが空白かどうか判定
    If String.IsNullOrWhiteSpace(prmSetCd) = False Then
      ' セットが空白以外の場合
      updateList.Add(" SETCD = " & prmSetCd)
    End If

    ' 原価が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTanka) = False Then
      ' 原価が空白以外の場合
      updateList.Add(" GENKA = " & prmTanka)
    Else
      ' 原価が空白の場合は0を代入
      updateList.Add(" GENKA = 0 ")
    End If

    ' 売原価が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmBaika) = False Then
      ' 売原価が空白以外の場合
      updateList.Add(" TANKA = " & prmBaika)
      updateList.Add(" BAIKA = " & prmBaika)
    Else
      ' 売原価が空白の場合は0を代入
      updateList.Add(" TANKA = 0 ")
      updateList.Add(" BAIKA = 0 ")
    End If

    ' SP区分を設定
    If Val(prmSetCd) = 0 Then
      updateList.Add(" SPKUBUN = 0 ")
    Else
      updateList.Add(" SPKUBUN = 1 ")
    End If

    sql &= String.Join(",", updateList.ToArray)
    sql &= ", KDATE = '" & ComGetProcTime() & "'"
    sql &= " WHERE EBCODE =    " & prmSelectedRow("EBCODE")
    sql &= "   AND KDATE <=  ' " & prmSelectedRow("KDATE_MAX") & "'"
    sql &= "   AND KAKOUBI = ' " & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG <= 1 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KYOKUFLG = 0 "
    sql &= "   AND TKCODE =    " & prmSelectedRow("TKCODE")
    sql &= "   AND SETCD =     " & prmSelectedRow("SETCD")
    sql &= "   AND KAKUC =     " & prmSelectedRow("KAKUC")
    sql &= "   AND TANKA =     " & prmSelectedRow("TANKA")
    sql &= "   AND GENKA =     " & prmSelectedRow("GENKA")
    sql &= "   AND KOTAINO =   " & prmSelectedRow("KOTAINO")
    sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' 印刷用ワークテーブル追加SQL文作成
  ''' </summary>
  ''' <param name="prmSelected"></param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsZaiko01(prmSelected As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty

    Dim getHenpinBi As String = ""
    getHenpinBi = prmSelected("HENPINBI")

    Dim flgGHenpin As Boolean = False
    ' 返品日が日付データかどうか判定
    If (IsDate(getHenpinBi)) Then
      ' 返品日が日付データの場合
      flgGHenpin = True
    End If

    sql &= " INSERT INTO WK_ZAIKO("
    sql &= "WTCODE,"        '01:
    sql &= "WTNAME,"        '02:
    sql &= "WSETKBN,"       '03:
    sql &= "WSETNAME,"      '04:
    sql &= "WBUICODE,"      '05:
    sql &= "WGNAME,"        '06:等級
    sql &= "WSHCODE,"       '07:
    sql &= "WHINMEI,"       '08:  
    sql &= "WTANKA,"        '14:
    sql &= "WLR,"           '09:
    sql &= "WEDANo,"        '12:
    sql &= "WRENNo,"        '10:
    sql &= "WSEIZOUYMD,"    '11:
    If (flgGHenpin) Then
      sql &= "WHENPINYMD,"  '19:
    End If
    sql &= "WJYURYO,"       '13:
    sql &= "WTANKAU,"       '21:
    sql &= "WKINGAKU,"      '15:  
    sql &= "WJIKAN,"        '27:  
    sql &= "WKOTAINO,"      '26:
    sql &= "WKIGEN"
    sql &= ") VALUES("

    sql &= prmSelected("TKCODE") & ","                        '01:

    Dim tokuisaki As String =prmSelected("TOKUISAKI_NAME")
    If String.IsNullOrWhiteSpace(tokuisaki) Then
      sql &= "'　　　＊　未設定　＊'" & ","                   '02:
    Else
      sql &= "'" & prmSelected("TOKUISAKI_NAME") & "'" & ","  '02:
    End If
    sql &= prmSelected("SETCD") & ","                         '03:
    sql &= "'" & prmSelected("SHOHIN_HINMEI") & "'" & ","     '04:
    sql &= prmSelected("BICODE") & ","                        '05:
    sql &= "'" & prmSelected("KAKU_KZNAME") & "'" & ","       '06:等級
    sql &= prmSelected("SHOHIN_CODE") & ","                   '07:
    sql &= "'" & prmSelected("BUIM_BINAME") & "'" & ","       '08:
    sql &= ComBlank2ZeroText(prmSelected("GENKA")) & ","      '14:
    sql &= "'" & prmSelected("LR2") & "'" & ","               '09:
    sql &= prmSelected("EBCODE") & ","                        '12:
    sql &= prmSelected("TOOSINO") & ","                       '10:
    sql &= "'" & prmSelected("KAKOUBI") & "'" & ","           '11:
    ' 返品日が日付データの場合
    If (flgGHenpin) Then
      sql &= "'" & prmSelected("HENPINBI") & "'" & ","        '19:
    End If
    sql &= prmSelected("JYURYOK") & ","                       '13:
    sql &= prmSelected("TANKA") & ","                         '21:
    sql &= ComBlank2ZeroText(prmSelected("KINGAKUW")) & ","   '15:
    sql &= "'" & ComGetProcTime() & "'" & ","                 '27: 
    sql &= prmSelected("KOTAINO") & ","                       '26:
    sql &= "'" & prmSelected("WKIGEN") & "'" & ")"

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' 印刷用ワークテーブル追加SQL文作成（枝別集計）
  ''' </summary>
  ''' <param name="prmSelected"></param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsZaiko02(prmSelected As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty

    sql &= " INSERT INTO WK_ZAIKO("
    sql &= "WTCODE,"        '01:
    sql &= "WTNAME,"        '02:
    sql &= "WSETKBN,"       '03:
    sql &= "WSETNAME,"      '04:
    sql &= "WBUICODE,"      '05:
    sql &= "WGNAME,"        '06:等級
    sql &= "WSHCODE,"       '07:
    sql &= "WHINMEI,"       '08:
    sql &= "WTANKA,"        '14:  
    sql &= "WLR,"           '09:
    sql &= "WEDANo,"        '12:
    sql &= "WRENNo,"        '10:
    sql &= "WSEIZOUYMD,"    '11:  
    sql &= "WJYURYO,"       '13:    
    sql &= "WTANKAU,"       '21:  
    sql &= "WKINGAKU,"      '15:
    sql &= "WJIKAN,"        '27:
    sql &= "WKOTAINO,"      '26:
    sql &= "WKIGEN"
    sql &= ") VALUES("

    sql &= prmSelected("TKCODE") & ","                         '01:
    sql &= "'" & prmSelected("TOKUISAKI_NAME") & "'" & ","     '02:
    sql &= "0,"                                                '03:
    sql &= "'" & prmSelected("SHOHIN_HINMEI") & "'" & ","      '04:
    sql &= "0,"                                                '05:
    sql &= "'" & prmSelected("KAKU_KZNAME") & "'" & ","        '06:等級
    sql &= prmSelected("SHOHIN_CODE") & ","                    '07: 
    sql &= "'" & prmSelected("BUIM_BINAME") & "'" & ","        '08:
    sql &= prmSelected("GENKA") & ","                          '14:
    sql &= "'" & prmSelected("LR2") & "'" & ","                '09: 
    sql &= prmSelected("EBCODE") & ","                         '12: 
    sql &= prmSelected("TOOSINO") & ","                        '10:
    sql &= " '" & prmSelected("KAKOUBI") & "'" & ","           '11: 
    sql &= prmSelected("JYURYOK") & ","                        '13:         
    sql &= prmSelected("TANKA") & ","                          '21: 
    sql &= prmSelected("KINGAKUW") & ","                       '15: 
    sql &= "'" & ComGetProcTime() & "'" & ","                  '27: 
    sql &= prmSelected("KOTAINO") & ","                        '26:
    sql &= "'" & prmSelected("WKIGEN") & "'" & ")"

    Console.WriteLine(sql)

    Return sql

  End Function


#End Region

    ' 修正テスト
End Class
