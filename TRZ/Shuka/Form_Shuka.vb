Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports T.R.ZCommonClass.clsDataGridSearchControl

Public Class Form_Shuka
  Implements IDgvForm02

  '----------------------------------------------
  '          出庫問合せ画面
  '
  '
  '----------------------------------------------

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

  Private Const PRG_ID As String = "shukka"
  Private Const PRG_TITLE As String = "出荷問合せ画面"
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
     "ButtonUpdate"}

#End Region

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_Shuka, AddressOf ComRedisplay)
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
        .FixedRow = 3

        '１つ目のDataGridViewオブジェクトの検索コントロール設定
        .AddSearchControl(Me.txtSyukkabi, "SYUKKABI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.txtSyukkabiFrom, "SYUKKABI", typExtraction.EX_GTE, typColumnKind.CK_Date)
        .AddSearchControl(Me.CmbMstCustomer_01, "UTKCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtEdaban_01, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtJyouJyouNo, "EDAB.EDC", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxKotaiNo_01, "CUTJ.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstSetType_01, "CUTJ.SETCD", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstItem_01, "CUTJ.BICODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' 編集可能列設定
        .EditColumnList = CreateGrid2EditCol1()

      End With
    End With

    Call InitGrid(DG2V2, CreateGrid2Src2(), CreateGrid2Layout2())

    With DG2V2

      '２つ目のDataGridViewオブジェクトを非表示する
      .Visible = False

      ' ２つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)

        ' ２つ目のDataGridViewオブジェクトの検索コントロール設定
        .AddSearchControl(Me.txtSyukkabi, "SYUKKABI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.txtSyukkabiFrom, "SYUKKABI", typExtraction.EX_GTE, typColumnKind.CK_Date)
        .AddSearchControl(Me.CmbMstCustomer_01, "UTKCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtEdaban_01, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtJyouJyouNo, "EDAB.EDC", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxKotaiNo_01, "CUTJ.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstSetType_01, "CUTJ.SETCD", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstItem_01, "CUTJ.BICODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' ２つ目のDataGridViewオブジェクトの編集可能列設定
        '        .EditColumnList = CreateGrid2EditCol2()
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

    sql = ""
    sql &= " SELECT CUTJ.*"
    sql &= "      , EDAB.EDC AS JYOUJYOU "
    sql &= "      , TOKUISAKI.LTKNAME AS TOKUISAKI_NAME "
    sql &= "      , SHOHIN.HINMEI AS SHOHIN_HINMEI "
    sql &= "      , CUTJ.SHOHINC AS SHOHIN_CODE "
    sql &= "      , KAKU.KZNAME AS KAKU_KZNAME "
    sql &= "      , BUIM.BINAME AS BUIM_BINAME "
    sql &= "      , IIF(CUTJ.SAYUKUBUN = " & PARTS_SIDE_LEFT.ToString & ",'左' "
    sql &= "      , IIF(CUTJ.SAYUKUBUN = " & PARTS_SIDE_RIGHT.ToString & ",'右',' ')) AS LR2 "
    sql &= "      , (ROUND((CUTJ.JYURYO / 100), 0, 1) / 10) AS JYURYOK "
    sql &= "      , ROUND((CUTJ.TANKA * ROUND((CUTJ.JYURYO / 100), 0, 1) / 10), 0, 1) AS KINGAKUW "
    sql &= "      , ROUND(((CUTJ.TANKA - CUTJ.GENKA) * ROUND((CUTJ.JYURYO / 100), 0, 1) / 10 ), 0, 1) AS SAGAKU "
    sql &= "      , IIF(NKUBUN=1,'返',' ') AS HENKUBUN "
    sql &= " FROM ((((CUTJ LEFT JOIN BUIM ON CUTJ.BICODE = BUIM.BICODE) "
    sql &= "               LEFT JOIN SHOHIN ON CUTJ.SETCD = SHOHIN.SHCODE AND CUTJ.GBFLG = SHOHIN.GBFLG) "
    sql &= "               LEFT JOIN KAKU ON CUTJ.KAKUC = KAKU.KKCODE) "
    sql &= "               LEFT JOIN TOKUISAKI ON CUTJ.UTKCODE = TOKUISAKI.TKCODE) "
    sql &= "               LEFT JOIN EDAB ON CUTJ.KOTAINO = EDAB.KOTAINO "
    sql &= "                             and CUTJ.EBCODE = EDAB.EBCODE "
    sql &= " WHERE   (CUTJ.NSZFLG = 2 "
    sql &= "      AND CUTJ.DKUBUN = 0 "
    sql &= "      AND CUTJ.KYOKUFLG = 0) "
    sql &= " ORDER BY CUTJ.UTKCODE "
    sql &= "        , CUTJ.SETCD "
    sql &= "        , CUTJ.EBCODE "
    sql &= "        , CUTJ.KAKOUBI; "

    Return sql

  End Function

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("得意先名", "TOKUISAKI_NAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=250, argFontSize:=10))
      .Add(New clsDGVColumnSetting("セット名", "SHOHIN_HINMEI", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=155, argFontSize:=10))
      .Add(New clsDGVColumnSetting("商品ｺｰﾄﾞ", "SHOHIN_CODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=98))
      .Add(New clsDGVColumnSetting("商品名", "BUIM_BINAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=194, argFontSize:=10))
      .Add(New clsDGVColumnSetting("等級", "KAKU_KZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=100, argFontSize:=10))
      .Add(New clsDGVColumnSetting("製造日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("枝No.", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=75))
      .Add(New clsDGVColumnSetting("左右", "LR2", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=64, argFontSize:=10))
      .Add(New clsDGVColumnSetting("小口", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("返", "HENKUBUN", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=48))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="#,##0.0"))
      .Add(New clsDGVColumnSetting("入荷単価", "GENKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=98, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("売上単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=98, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("金額", "KINGAKUW", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("個体識別", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=128, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("上場コード", "JYOUJYOU", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("入庫日", "KEIRYOBI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=128))
      .Add(New clsDGVColumnSetting("出荷日", "SYUKKABI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=128))
      .Add(New clsDGVColumnSetting("差額金額", "SAGAKU", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=128, argFormat:="#,###"))
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
      .Add(New clsDataGridEditTextBox("入荷単価", prmUpdateFnc:=AddressOf UpDateNoReload, prmValueType:=VT_SIGNED_NUMBER, prmIsReload:=False))
      .Add(New clsDataGridEditTextBox("売上単価", prmUpdateFnc:=AddressOf UpDateNoReload, prmValueType:=VT_SIGNED_NUMBER, prmIsReload:=False))
      .Add(New clsDataGridEditTextBox("出荷日", prmUpdateFnc:=AddressOf UpDateNoReload, prmValueType:=VT_DATE, prmIsReload:=False))

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

    sql = ""
    sql &= " SELECT CUTJ.UTKCODE,TOKUISAKI.LTKNAME AS TOKUISAKI_NAME "
    sql &= "       ,CUTJ.SETCD,SHOHIN.HINMEI AS SHOHIN_HINMEI "
    sql &= "       ,COUNT(CUTJ.BICODE) AS KENSU "
    sql &= "       ,CUTJ.KAKUC,KAKU.KZNAME AS KAKU_KZNAME "
    sql &= "       ,CUTJ.KAKOUBI "
    sql &= "       ,CUTJ.EBCODE "
    sql &= "       ,EDAB.EDC AS JYOUJYOU "
    sql &= "       ,CUTJ.TANKA "
    sql &= "       ,CUTJ.GENKA "
    sql &= "       ,SUM(ROUND((CUTJ.JYURYO / 100), 0, 1) / 10) AS JYURYOK "
    sql &= "       ,SUM(ROUND((CUTJ.TANKA * ROUND((CUTJ.JYURYO / 100), 0, 1) / 10 ), 0, 1)) AS KINGAKUW "
    sql &= "       ,SUM(ROUND(((CUTJ.TANKA - CUTJ.GENKA) * ROUND((CUTJ.JYURYO / 100), 0, 1) / 10 ), 0, 1)) AS SAGAKU "
    sql &= "       ,CUTJ.KOTAINO,CUTJ.SYUKKABI "
    sql &= "       ,IIF(CUTJ.SAYUKUBUN = " & PARTS_SIDE_LEFT.ToString & ",'左' "
    sql &= "       ,IIF(CUTJ.SAYUKUBUN = " & PARTS_SIDE_RIGHT.ToString & ",'右',' ')) AS LR2 "
    sql &= "       ,CUTJ.SAYUKUBUN "
    sql &= "       ,MAX(CUTJ.KDATE) AS KDATE_MAX "
    sql &= "       ,COUNT(SYUKKABI) AS RCount"
    sql &= " FROM (((CUTJ LEFT JOIN SHOHIN ON CUTJ.SETCD = SHOHIN.SHCODE AND CUTJ.GBFLG = SHOHIN.GBFLG) "
    sql &= "              LEFT JOIN KAKU ON CUTJ.KAKUC = KAKU.KKCODE) "
    sql &= "              LEFT JOIN TOKUISAKI ON CUTJ.UTKCODE = TOKUISAKI.TKCODE) "
    sql &= "              LEFT JOIN EDAB ON CUTJ.KOTAINO = EDAB.KOTAINO "
    sql &= "                            and CUTJ.EBCODE = EDAB.EBCODE  "
    sql &= " WHERE   (CUTJ.NSZFLG = 2 "
    sql &= "      AND CUTJ.DKUBUN = 0 "
    sql &= "      AND CUTJ.KYOKUFLG = 0) "
    sql &= " GROUP BY CUTJ.UTKCODE "
    sql &= "         ,TOKUISAKI.LTKNAME "
    sql &= "         ,CUTJ.SETCD "
    sql &= "         ,CUTJ.TANKA "
    sql &= "         ,SHOHIN.HINMEI "
    sql &= "         ,KAKU.KZNAME "
    sql &= "         ,CUTJ.KAKOUBI "
    sql &= "         ,CUTJ.EBCODE "
    sql &= "         ,EDAB.EDC "
    sql &= "         ,CUTJ.GENKA "
    sql &= "         ,CUTJ.KOTAINO "
    sql &= "         ,CUTJ.KAKUC "
    sql &= "         ,CUTJ.SYUKKABI"
    sql &= "         ,CUTJ.SAYUKUBUN "

    Return sql

  End Function

  ''' <summary>
  ''' ２つ目のDataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' Debug用
      '      .Add(New clsDGVColumnSetting("レコードカウント", "RCount", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("得意先名", "TOKUISAKI_NAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=250, argFontSize:=10))
      .Add(New clsDGVColumnSetting("セット名", "SHOHIN_HINMEI", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=155, argFontSize:=10))
      .Add(New clsDGVColumnSetting("等級", "KAKU_KZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=100, argFontSize:=10))
      .Add(New clsDGVColumnSetting("製造日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("枝No.", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("上場コード", "JYOUJYOU", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("左右", "LR2", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=64, argFontSize:=10))
      .Add(New clsDGVColumnSetting("個体識別", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=128, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="#,##0.0"))
      .Add(New clsDGVColumnSetting("入荷単価", "GENKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=98, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("売上単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=98, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("金額", "KINGAKUW", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("個数", "KENSU", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("出荷日", "SYUKKABI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=128))
      .Add(New clsDGVColumnSetting("差額金額", "SAGAKU", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=128, argFormat:="#,###"))
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

#Region "プライベート"

  ''' <summary>
  ''' データグリッド名判定
  ''' </summary>
  ''' <returns>データグリッド名</returns>
  Private Function GetDataGridName() As String

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
  ''' データグリッドの件数を取得
  ''' </summary>
  ''' <param name="tmpLastUpdate">最終更新日時（取得）</param>
  ''' <returns>データグリッドの件数</returns>
  Private Function GetDataGridKensu(ByRef tmpLastUpdate As String) As Integer

    Dim numCnt As Integer = 0


    If CheckBox_EdaBetu.Checked = False Then
      ' 単品
      tmpLastUpdate = Controlz(GetDataGridName()).SelectedRow("KDATE")
      numCnt = 1
    Else
      ' 枝別
      tmpLastUpdate = Controlz(GetDataGridName()).SelectedRow("KDATE_MAX")
      Dim tmpEdaban As String = Controlz(DG2V2.Name).SelectedRow("EBCODE")
      Dim tmpSayukubun As String = Controlz(DG2V2.Name).SelectedRow("SAYUKUBUN")
      Dim tmpUtkcode As String = Controlz(DG2V2.Name).SelectedRow("UTKCODE")
      Dim tmpSetCd As String = Controlz(DG2V2.Name).SelectedRow("SETCD")
      Dim tmpKakoubi As String = Controlz(DG2V2.Name).SelectedRow("KAKOUBI")
      Dim tmpKotaino As String = Controlz(DG2V2.Name).SelectedRow("KOTAINO")
      Dim tmpKakuc As String = Controlz(DG2V2.Name).SelectedRow("KAKUC")
      Dim tmpSyukkabi As String = Controlz(DG2V2.Name).SelectedRow("SYUKKABI")

      ' 一覧表示より、出荷先・枝番・左右・セットコードが一致するデータの
      ' ・件数合計
      ' ・最終更新日時
      ' を取得する
      For Each tmpKeyVal As Dictionary(Of String, String) In Controlz(DG2V2.Name).GetAllData()

        If tmpEdaban = tmpKeyVal("EBCODE") _
          AndAlso tmpSayukubun = tmpKeyVal("SAYUKUBUN") _
          AndAlso tmpUtkcode = tmpKeyVal("UTKCODE") _
          AndAlso tmpSetCd = tmpKeyVal("SETCD") _
          AndAlso tmpKakoubi = tmpKeyVal("KAKOUBI") _
          AndAlso tmpKotaino = tmpKeyVal("KOTAINO") _
          AndAlso tmpKakuc = tmpKeyVal("KAKUC") _
          AndAlso tmpSyukkabi = tmpKeyVal("SYUKKABI") Then

          numCnt += Integer.Parse(tmpKeyVal("KENSU"))

          If tmpLastUpdate < tmpKeyVal("KDATE_MAX") Then
            tmpLastUpdate = tmpKeyVal("KDATE_MAX")
          End If
        End If
      Next

    End If

    Return numCnt
  End Function

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
        Dim tmpTanka As Long = 0
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

        ' 入荷単価・売上単価更新時は金額・差額の更新を行う
        If tmpKey = "TANKA" _
          OrElse tmpKey = "GENKA" Then

          tmpJYuryoK = Decimal.Parse(tmpSelectedRow("JYURYOK"))

          ' 売上単価更新時
          If tmpKey = "TANKA" Then
            tmpTanka = Long.Parse(tmpVal)                         ' 売上単価は入力された値を使用
            tmpGenka = Long.Parse(tmpSelectedRow("GENKA"))
          End If

          ' 入荷単価更新時
          If tmpKey = "GENKA" Then
            tmpTanka = Long.Parse(tmpSelectedRow("TANKA"))
            tmpGenka = Long.Parse(tmpVal)                         ' 入荷単価は入力された値を使用
          End If

          Controlz(DG2V1.Name).SetCellVal("KINGAKUW", DG2V1.SelectedCells(0).RowIndex _
                                          , Math.Truncate((tmpJYuryoK * tmpTanka)).ToString())

          Controlz(DG2V1.Name).SetCellVal("SAGAKU", DG2V1.SelectedCells(0).RowIndex _
                                          , Math.Truncate(tmpJYuryoK * (tmpTanka - tmpGenka)).ToString())

        End If

        .TrnCommit()

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
  Private Function UpDateDb(Optional flgNumeric As Boolean = True) As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True
    Dim sqlKensu As Integer = 0
    Dim tmpProcTime As String = ComGetProcTime()
    Dim tmpLastUpdate As String = String.Empty

    ' 実行
    With tmpDb
      Try
        ' トランザクション開始
        .TrnStart()

        ' SQL文の作成方法による分岐
        If (flgNumeric) Then
          ' 明細（入荷単価、売上単価）を直接修正
          sql = SqlUpdCutJ(Controlz(DG2V1.Name).EditData _
                         , Controlz(DG2V1.Name).SelectedRow _
                         , tmpProcTime)

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

          ' 実行対象の件数・最終更新日時を取得
          sqlKensu = GetDataGridKensu(tmpLastUpdate)

          ' CutJ更新SQL文作成
          If (CheckBox_EdaBetu.Checked) Then
            sql = SqlUpdEdaCutJ(setTokuisakiCmbText _
                              , setCdCmbText _
                              , TxTanka_02.Text _
                              , TxBaika_02.Text _
                              , tmpLastUpdate _
                              , Controlz(GetDataGridName()).SelectedRow)

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
  ''' 更新用画面を閉じないコントロール以外の場合、更新用画面を閉じる
  ''' </summary>
  Private Sub chkFrame_UnDsp()

    ' アクティブなコントロールを取得
    Dim c As Control = Me.ActiveControl
    Dim runFlg As Boolean = True

    ' アクティブなコントロールを取得できた場合
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
  ''' 出荷一覧表印刷処理（出力ワークの作成とレポートの表示）
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function printShuka() As Boolean

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

    ' ACCESSの出荷一覧表レポートを表示
    ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_SYUKKA")

    Return True

  End Function

  ''' <summary>
  ''' 枝別出荷一覧印刷処理（出力ワークの作成とレポートの表示）
  ''' </summary>
  ''' <param name="edaNo"></param>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function printShuka(edaNo As String) As Boolean

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

    ' ACCESSの枝別出荷一覧レポートを表示
    ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_EDASYUKKA")

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
      ' 更新用画面が非表示時は、データグリッドを最大表示
      gridHeight = GRID_HEIGHT_MAX
    End If

    Return gridHeight

  End Function

  ''' <summary>
  ''' データグリッド更新時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate">最終更新日時</param>
  ''' <param name="DataCount">データ件数</param>
  Private Sub DgvReload(sender As DataGridView, LastUpdate As String, DataCount As Long, DataJuryo As Decimal, DataKingaku As Decimal)

    Me.Label_GridData.AutoSize = False
    Me.Label_GridData.TextAlign = ContentAlignment.MiddleCenter
    '文字列をDateTime値に変換する
    Dim dt As DateTime = DateTime.Parse(LastUpdate)
    Me.Label_GridData.Text = dt.ToString("yyyy年M月d日HH：mm") & " 現在 " & Space(6) & " 出 荷 一 覧 " & Space(6) & DataCount.ToString() & "件" & Space(2) & DataJuryo.ToString() & "kg" & Space(2) & DataKingaku.ToString() & "円"

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
  Private Sub Form_Shuka_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = PRG_TITLE

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

    ' グリッド手動編集エラー時イベント追加
    Controlz(DG2V1.Name).lcCallBackValidatingFailed = AddressOf DataGridValidatingFailed

    ' 出荷日のコンボボックスを先頭に設定
    CmbDateProcessing_01.SelectedIndex = 0

    ' 出荷日のコンボボックスは未入力可
    CmbDateProcessing_01.AvailableBlank = True

    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Me.CmbDateProcessing_01

    ' 出庫明細表示
    Controlz(DG2V1.Name).AutoSearch = True

    Controlz(DG2V1.Name).SetMsgLabelText("明細のダブルクリックで修正できます。入荷単価、売上単価のみ直接修正できます。")
    Controlz(DG2V2.Name).SetMsgLabelText("明細のダブルクリックで修正できます。")

    ' メッセージラベル設定
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

    ' 非表示 → 表示時処理設定
    MyBase.lcCallBackShowFormLc = AddressOf ReStartPrg

    ' IPC通信起動
    InitIPC(PRG_ID)

  End Sub

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

  ''' <summary>
  ''' 画面再表示時処理
  ''' </summary>
  ''' <remarks>
  ''' 非表示→表示時に実行
  ''' FormLoad時に設定
  ''' </remarks>
  Private Sub ReStartPrg()
    ' 出荷日のコンボボックスを更新
    CmbDateProcessing_01.InitCmb()

    ' 出荷日のコンボボックスを先頭に設定
    CmbDateProcessing_01.SelectedIndex = 0

  End Sub

#End Region

#Region "チェックボックス関連"
  ''' <summary>
  ''' 枝別合計表示チェックボックス選択時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox_EdaBetu.Click

    ' データグリッドのフォーカス喪失時の位置初期化
    rowIndexDataGrid = -1

    ' 枝別合計表示チェックボックスがチェック状態を判定
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

      '更新用画面を閉じる
      Frame_UnDsp()

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

    End If

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
  Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

    '更新用画面を閉じる
    Frame_UnDsp()

  End Sub

  ''' <summary>
  ''' データグリッドフォーカス選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_Enter(sender As Object, e As EventArgs) Handles DataGridView1.Enter

    '更新用画面を閉じる
    Frame_UnDsp()

    'フォーカスが戻ると元の選択状態に戻る
    DG2V1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
    DG2V1.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText

  End Sub

  ''' <summary>
  ''' グリッドフォーカス非選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_Leave(sender As Object, e As EventArgs) Handles DataGridView1.Leave

    'フォーカスが外れると選択状況を隠す
    DG2V1.DefaultCellStyle.SelectionBackColor = DG2V1.DefaultCellStyle.BackColor
    DG2V1.DefaultCellStyle.SelectionForeColor = DG2V1.DefaultCellStyle.ForeColor

    ' データグリッドのフォーカス喪失時の位置設定
    If DG2V1.SelectedCells.Count > 0 Then
      rowIndexDataGrid = DG2V1.SelectedCells(0).RowIndex
    End If

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
        '製造日
        Label_SeizouBi.Text = .SelectedRow("KAKOUBI")
        '枝No
        Label_EdaNo_02.Text = .SelectedRow("EBCODE")
        '左右
        Label_Sayu.Text = .SelectedRow("LR2")
        ' カートン
        Label_KotaiNo.Text = .SelectedRow("TOOSINO")
        '重量
        Dim numJyuryou As Double = 0
        numJyuryou = CType(.SelectedRow("JYURYOK"), Double)
        Label_Jyuryo.Text = numJyuryou.ToString("#,##0.0")
        '得意先コンボボックス
        Dim Number1 As Integer = 0
        Int32.TryParse(.SelectedRow("UTKCODE"), Number1)
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

      'ドロップダウンリスト部を非表示にさせる
      Me.CmbMstSetType_02.DroppedDown = False
      ' セット区分にフォーカスをあてる
      Me.CmbMstSetType_02.Enabled = True
      Me.ActiveControl = Me.CmbMstSetType_02

    End With

  End Sub

  ''' <summary>
  ''' データグリッドフォーカス選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView2_Enter(sender As Object, e As EventArgs) Handles DataGridView2.Enter

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
    Dim tmpSelectedRow As Dictionary(Of String, String) = Controlz(DG2V2.Name).SelectedRow

    With DG2V2

      .Height = SetFrameInDisp(True)

      ' １つ目のDataGridViewオブジェクトのグリッド動作設定
      '製造日
      Label_SeizouBi.Text = tmpSelectedRow("KAKOUBI")
      '枝No
      Label_EdaNo_02.Text = tmpSelectedRow("EBCODE")
      '左右
      Label_Sayu.Text = tmpSelectedRow("LR2")
      ' カートン
      Label_KotaiNo.Text = ""
      '重量
      Dim numJyuryou As Double = 0
      numJyuryou = CType(tmpSelectedRow("JYURYOK"), Double)
      Label_Jyuryo.Text = numJyuryou.ToString("#,##0.0")
      '得意先コンボボックス
      Dim Number1 As Integer = 0
      Int32.TryParse(tmpSelectedRow("UTKCODE"), Number1)
      Dim SearchStr1 As String = String.Format("{0:D4}:{1}", Number1, tmpSelectedRow("TOKUISAKI_NAME"))
      CmbMstCustomer_02.SelectedIndex = CmbMstCustomer_02.FindStringExact(SearchStr1)
      'セットコンボボックス
      Dim Number2 As Integer = 0
      Int32.TryParse(tmpSelectedRow("SETCD"), Number2)
      Dim SearchStr2 As String = String.Format("{0:D4}:{1}", Number2, tmpSelectedRow("SHOHIN_HINMEI"))
      CmbMstSetType_02.SelectedIndex = CmbMstSetType_02.FindStringExact(SearchStr2)
      '入荷単価テキストボックス
      TxTanka_02.Text = tmpSelectedRow("GENKA")
      '売上単価テキストボックス
      TxBaika_02.Text = tmpSelectedRow("TANKA")

    End With

    'ドロップダウンリスト部を非表示にさせる
    Me.CmbMstSetType_02.DroppedDown = False
    ' セット区分にフォーカスをあてる
    Me.CmbMstSetType_02.Enabled = True
    Me.ActiveControl = Me.CmbMstSetType_02

  End Sub


  ''' <summary>
  ''' グリッド手動編集失敗時イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>エラーメッセージを表示する</remarks>
  Private Sub DataGridValidatingFailed(sender As Object, e As Exception)
    Dim tmpMsg As String = String.Empty

    tmpMsg &= e.Message & vbCrLf
    tmpMsg &= "日付は以下、何れかの形式で入力して下さい。" & vbCrLf
    tmpMsg &= "・ 年4桁/月2桁/日2桁 例) 2021/08/05 " & vbCrLf
    tmpMsg &= "・ 年4桁月2桁日2桁 例) 20210805" & vbCrLf
    tmpMsg &= "・ 年2桁/月2桁/日2桁 例) 21/08/05" & vbCrLf
    tmpMsg &= "・ 年2桁月2桁日2桁 例) 210805" & vbCrLf
    tmpMsg &= "・ 月2桁/日2桁 例) 08/05" & vbCrLf
    tmpMsg &= "・ 月2桁日2桁 例) 0805" & vbCrLf
    tmpMsg &= "・ 月1桁/日2桁 例) 8/05" & vbCrLf
    tmpMsg &= "・ 月1桁日2桁 例) 805" & vbCrLf
    tmpMsg &= "・ 日2桁 例) 05" & vbCrLf
    tmpMsg &= "・ 日1桁 例) 5"

    Call ComMessageBox(tmpMsg, PRG_TITLE, typMsgBox.MSG_ERROR)
  End Sub
#End Region

#Region "フレーム関連"
  ''' <summary>
  ''' 更新用画面からフォーカスが外れた場合
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Frame_IN_Leave(sender As Object, e As EventArgs) Handles Frame_IN.Leave

    chkFrame_UnDsp()

  End Sub
#End Region

#Region "ボタン関連"

  ''' <summary>
  ''' アプリケーション終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonEnd_Click(sender As Object, e As EventArgs) Handles ButtonEnd.Click

    ' 自動検索OFF
    Controlz(DG2V1.Name).AutoSearch = False
    Controlz(DG2V2.Name).AutoSearch = False
    Controlz(DG2V1.Name).ResetPosition()
    Controlz(DG2V2.Name).ResetPosition()

    Me.Hide()

    MyBase.AllClear()
    CheckBox_EdaBetu.Checked = False
    Controlz(DG2V1.Name).InitSort()
    Controlz(DG2V2.Name).InitSort()


    ' 出荷日のコンボボックスを更新
    CmbDateProcessing_01.InitCmb()

    ' 出荷日のコンボボックスを先頭に設定
    CmbDateProcessing_01.SelectedIndex = 0
    Me.TxtEdaban_01.Focus()

    Controlz(DG2V1.Name).AutoSearch = True
    DG2V1.Visible = True
    Me.ButtonTorikeshi.Enabled = True
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
                                     "出荷修正処理",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OKCANCEL,
                                     prmDefaultButton:=MessageBoxDefaultButton.Button1)
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

    Dim tmpSubForm As New Form_Torikeshi
    Dim tmpTargeDgv As DataGridView


    If CheckBox_EdaBetu.Checked Then
      ' 枝別表示
      tmpTargeDgv = DG2V2
    Else
      ' 通常表示
      tmpTargeDgv = DG2V1
    End If

    ' データグリッドが０件の場合、処理を行わない
    If (tmpTargeDgv.Rows.Count < 1) Then
      Return
    End If

    ' データｸﾞﾘｯﾄﾞが未選択の場合
    If tmpTargeDgv.CurrentCell Is Nothing Then
      ' フォーカス喪失時の列が設定済かどうか
      If (rowIndexDataGrid = -1) Then
        ' 先頭の列を選択状態とする
        tmpTargeDgv.CurrentCell = tmpTargeDgv(tmpTargeDgv.FirstDisplayedScrollingColumnIndex, tmpTargeDgv.FirstDisplayedScrollingRowIndex)
      Else
        ' フォーカス喪失時の列を選択状態とする
        tmpTargeDgv.CurrentCell = tmpTargeDgv(tmpTargeDgv.FirstDisplayedScrollingColumnIndex, rowIndexDataGrid)
      End If
    End If

    Try
      Select Case tmpSubForm.ShowSubForm(Controlz(tmpTargeDgv.Name).SelectedRow, Me)
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
  ''' 印刷ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click

    ' 出荷日が未選択もしくは未入力の場合 
    If CmbDateProcessing_01.SelectedItem Is Nothing Then
      clsCommonFnc.ComMessageBox("出荷日は必須項目です。",
                               "印刷処理",
                               typMsgBox.MSG_WARNING,
                               typMsgBoxButton.BUTTON_OK)
      Exit Sub
    End If

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


    Dim rtn As typMsgBoxResult
    ' 印刷プレビューの表示有無 
    If clsGlobalData.PRINT_PREVIEW = 1 Then
      rtn = clsCommonFnc.ComMessageBox("出荷一覧を画面表示しますか？",
                                     "印刷処理",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OKCANCEL)
    Else
      rtn = clsCommonFnc.ComMessageBox("出荷一覧を印刷しますか？",
                                     "印刷処理",
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
          printShuka()
        Else
          printShuka(Me.TxtEdaban_01.Text)
        End If

      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      End Try

      '印刷ボタン再表示
      ButtonPrint.Enabled = True

    End If

  End Sub

#End Region

#Region "コンボボックス関連"

  ''' <summary>
  ''' 出荷日コンボバリデーション
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub SyukkabiValidated(sender As Object, e As EventArgs) Handles CmbDateProcessing_01.Validated _
                                                                        , CmbDateProcessing_01.SelectedIndexChanged
    If DG2V1 IsNot Nothing AndAlso DG2V2 IsNot Nothing Then

      If Me.txtSyukkabi.Text <> Me.CmbDateProcessing_01.Text Then

        If Me.CmbDateProcessing_01.SelectedValue Is Nothing Then
          ' 出荷日未選択時は出荷日の過去7か月を抽出対象
          Me.txtSyukkabiFrom.Text = Date.Parse(ComGetProcDate()).AddMonths(-7).ToString("yyyy/MM/dd")
          Me.txtSyukkabi.Text = ""
        Else
          ' 出荷日選択時は選択された出荷日を抽出対象
          Me.txtSyukkabi.Text = Me.CmbDateProcessing_01.Text
          Me.txtSyukkabiFrom.Text = ""
        End If

        ' 検索処理実行
        Controlz(GetDataGridName()).ShowList()
      End If
    End If
  End Sub

#End Region
#End Region

#Region "SQL関連"

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
    If prmEditData.Keys(0).ToLower() <> ("SYUKKABI").ToLower() Then
      ' 出荷日以外の更新
      ' 入力値が空白判定
      If String.IsNullOrWhiteSpace(setTxt) Then
        ' 入力値が空白の場合は0を代入
        setTxt = "0"
      End If
    Else
      ' 出荷日の更新
      If String.IsNullOrWhiteSpace(setTxt) Then
        Throw New Exception("出荷日の入力が不正です。")
      End If
      setTxt = "'" & setTxt & "'"

    End If

    sql &= " UPDATE CUTJ "
    sql &= " SET " & prmEditData.Keys(0) & " =" & setTxt
    sql &= "   ,KDATE =      '" & prmProcTime & "'"
    sql &= " WHERE EBCODE =    " & prmSelectedRow("EBCODE")
    sql &= "   AND KAKOUBI =  '" & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG =    " & prmSelectedRow("NSZFLG")
    sql &= "   AND KDATE =    '" & prmSelectedRow("KDATE") & "'"
    sql &= "   AND TDATE =    '" & prmSelectedRow("TDATE") & "'"
    sql &= "   AND DKUBUN =    " & prmSelectedRow("DKUBUN")
    sql &= "   AND KYOKUFLG =  " & prmSelectedRow("KYOKUFLG")
    sql &= "   AND TOOSINO =   " & prmSelectedRow("TOOSINO")
    sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")
    sql &= "   AND UTKCODE =   " & prmSelectedRow("UTKCODE")

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' CutJ更新SQL文作成
  ''' </summary>
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
    Else
      ' 売原価が空白の場合は0を代入
      updateList.Add(" TANKA = 0 ")
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
    sql &= "   AND KDATE =    '" & prmSelectedRow("KDATE") & "'"
    sql &= "   AND KAKOUBI =  '" & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG = 2 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KYOKUFLG = 0 "
    sql &= "   AND TOOSINO =   " & prmSelectedRow("TOOSINO")
    sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")
    sql &= "   AND UTKCODE =   " & prmSelectedRow("UTKCODE")
    sql &= "   AND JYURYO =    " & prmSelectedRow("JYURYO")

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' CutJ更新SQL文作成（枝別集計修正）
  ''' </summary>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdEdaCutJ(prmTokuisaki As String _
                               , prmSetCd As String _
                               , prmTanka As String _
                               , prmBaika As String _
                               , prmLastUpDate As String _
                               , prmSelectedRow As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty
    sql &= " UPDATE CUTJ "
    sql &= " SET "

    Dim updateList As New List(Of String)

    ' 得意先が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTokuisaki) = False Then
      ' 得意先が空白以外の場合
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
      ' 売原価が空白の場合は0を代入
      updateList.Add(" TANKA = " & prmBaika)
    Else
      updateList.Add(" TANKA = 0 ")
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
    sql &= "   AND KDATE <=   '" & prmLastUpDate & "'"
    sql &= "   AND KAKOUBI =  '" & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG = 2 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KYOKUFLG = 0 "
    sql &= "   AND UTKCODE =   " & prmSelectedRow("UTKCODE")
    sql &= "   AND SETCD =     " & prmSelectedRow("SETCD")
    'sql &= "   AND TANKA =     " & prmSelectedRow("TANKA")
    'sql &= "   AND GENKA =     " & prmSelectedRow("GENKA")
    sql &= "   AND KOTAINO =   " & prmSelectedRow("KOTAINO")
    sql &= "   AND KAKUC =     " & prmSelectedRow("KAKUC")
    sql &= "   AND SYUKKABI = '" & prmSelectedRow("SYUKKABI") & "'"
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
    Dim tmpKeyVal As New Dictionary(Of String, String)
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    Dim flgHenpin As Boolean = False
    If prmSelected.ContainsKey("NKUBUN") _
      AndAlso prmSelected("NKUBUN").Equals("1") Then
      flgHenpin = True
    End If

    With tmpKeyVal

      ' 個別表示時のみ有効
      If GetDataGridName().Equals(DG2V1.Name) Then
        .Add("WSHCODE", prmSelected("SHOHIN_CODE"))
        .Add("WBUICODE", prmSelected("BICODE"))
        .Add("WHINMEI", "'" & prmSelected("BUIM_BINAME") & "'")
        .Add("WRENNo", prmSelected("TOOSINO"))
        If IsDate(prmSelected("HENPINBI")) Then
          .Add("WHENPINYMD", "'" & prmSelected("HENPINBI") & "'")
        End If
      End If

      .Add("WTANKA", ComBlank2ZeroText(prmSelected("TANKA")))
      .Add("WTCODE", prmSelected("UTKCODE"))
      .Add("WTNAME", "'" & prmSelected("TOKUISAKI_NAME") & "'")

      If flgHenpin Then
        .Add("WSETKBN", "99")
        .Add("WSETNAME", "'返品'")
      Else
        If GetDataGridName().Equals(DG2V1.Name) Then
          .Add("WSETKBN", prmSelected("SETCD"))
        Else
          ' 枝別合計表示時はセットコードをグループ化から除外
          .Add("WSETKBN", "9999")
        End If
        .Add("WSETNAME", "'" & prmSelected("SHOHIN_HINMEI") & "'")
      End If

      .Add("WGNAME", "'" & prmSelected("KAKU_KZNAME") & "'")
      .Add("WLR", "'" & prmSelected("LR2") & "'")
      .Add("WSEIZOUYMD", "'" & prmSelected("KAKOUBI") & "'")
      .Add("WEDANo", prmSelected("EBCODE"))
      .Add("WJYURYO", prmSelected("JYURYOK"))
      .Add("WKINGAKU", prmSelected("KINGAKUW"))


      .Add("WSHUKAYMD", "'" & prmSelected("SYUKKABI") & "'")
      .Add("WSAGAKU", prmSelected("SAGAKU"))
      .Add("WKOTAINO", prmSelected("KOTAINO"))
    End With

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst &= tmpKey & " ,"
      tmpVal &= tmpKeyVal(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= " INSERT INTO WK_ZAIKO(" & tmpDst & ") "
    sql &= " VALUES(" & tmpVal & ")"

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
    Dim tmpKeyVal As New Dictionary(Of String, String)
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    Dim flgHenpin As Boolean = False
    If prmSelected.ContainsKey("NKUBUN") _
      AndAlso prmSelected("NKUBUN").Equals("1") Then
      flgHenpin = True
    End If


    With tmpKeyVal

      ' 個別表示時のみ有効
      If GetDataGridName().Equals(DG2V1.Name) Then
        .Add("WTANKAU", ComBlank2ZeroText(prmSelected("BAIKA")))
        .Add("WSHCODE", prmSelected("SHOHIN_CODE"))
        .Add("WHINMEI", "'" & prmSelected("BUIM_BINAME") & "'")
        .Add("WRENNo", ComBlank2ZeroText(prmSelected("TOOSINO")))
      End If

      If (flgHenpin) Then
        .Add("WSETKBN", "99")
        .Add("WSETNAME", "'返品'")
      Else
        .Add("WSETKBN", prmSelected("SETCD"))
        .Add("WSETNAME", "'" & prmSelected("SHOHIN_HINMEI") & "'")
      End If

      .Add("WTCODE", prmSelected("UTKCODE"))
      .Add("WTNAME", "'" & prmSelected("TOKUISAKI_NAME") & "'")

      .Add("WBUICODE", "0")
      .Add("WGNAME", "'" & prmSelected("KAKU_KZNAME") & "'")
      .Add("WLR", "'" & prmSelected("LR2") & "'")
      .Add("WSEIZOUYMD", "'" & prmSelected("KAKOUBI") & "'")
      .Add("WEDANo", prmSelected("EBCODE"))
      .Add("WJYURYO", prmSelected("JYURYOK"))
      .Add("WTANKA", ComBlank2ZeroText(prmSelected("TANKA")))
      .Add("WKINGAKU", prmSelected("KINGAKUW"))
      .Add("WSHUKAYMD", "'" & prmSelected("SYUKKABI") & "'")

      .Add("WSAGAKU", prmSelected("SAGAKU"))
      .Add("WKOTAINO", prmSelected("KOTAINO"))
    End With

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst &= tmpKey & " ,"
      tmpVal &= tmpKeyVal(tmpKey) & " ,"
    Next

    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= " INSERT INTO WK_ZAIKO(" & tmpDst & ") "
    sql &= " VALUES(" & tmpVal & ")"

    Console.WriteLine(sql)

    Return sql
  End Function


#End Region

End Class
