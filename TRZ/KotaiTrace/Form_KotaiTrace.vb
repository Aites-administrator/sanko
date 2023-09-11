Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass.clsDGVColumnSetting

Public Class Form_KotaiTrace
  Implements IDgvForm02

  '----------------------------------------------
  '                 個体履歴追跡
  '
  '
  '----------------------------------------------

#Region "定数定義"
#Region "プライベート"

  ''' <summary>
  ''' ProgramID
  ''' </summary>
  ''' <remarks>IPC通信でのユニークキーに使用</remarks>
  Private Const PRG_ID As String = "kotaitrace"

  ''' <summary>
  ''' プログラムタイトル
  ''' </summary>
  Private Const PRG_NAME As String = "個体識別履歴追跡"

#End Region
#End Region

#Region "メンバ"
#Region "プライベート"

  ' 枝入力情報一覧DataGrid表示位置
  Private _EdabRowPos As Integer = 0
#End Region
#End Region

#Region "スタートアップ"

  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_KotaiTrace, AddressOf ComRedisplay)
  End Sub

#End Region

#Region "データグリッドビュー２つの画面用操作関連共通"
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

    ' 個体識別履歴追跡DataGrid
    DG2V1 = Me.DataGridView1

    ' 枝入力情報一覧DataGrid
    DG2V2 = Me.DataGridView2

    ' 個体識別履歴追跡グリッド初期化
    Call InitGrid(DG2V1, CreateGrid2Src1(), CreateGrid2Layout1())

    With DG2V1

      .Visible = True

      ' グリッド動作設定
      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 4

        ' 個体識別履歴追跡の検索コントロール設定
        '.AddSearchControl([コントロール], "絞り込み項目名", [絞り込み方法], [絞り込み項目の型])
        .AddSearchControl(Me.TxKotaiNo1, "CUTJ.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.txtSearchValGTE, "CUTJ.KEIRYOBI", typExtraction.EX_GTE, typColumnKind.CK_Date)
        .AddSearchControl(Me.txtSearchValEQ, "CUTJ.KEIRYOBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtEdaban1, "EDAB.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtJyouJyouNo, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstShiresaki1, "CUTJ.SRCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.txtDummy, "CUTJ.KEIRYOBI", typExtraction.EX_LTE, typColumnKind.CK_Date)


        ' 個体識別履歴追跡の編集可能列設定（無し）
        .EditColumnList = CreateGrid2EditCol1()
      End With
    End With


    ' 枝入力情報一覧グリッド初期化
    Call InitGrid(DG2V2, CreateGrid2Src2(), CreateGrid2Layout2())

    With DG2V2

      .Visible = True

      ' グリッド動作設定
      With Controlz(.Name)

        ' 枝入力情報一覧の検索コントロール設定
        '.AddSearchControl([コントロール], "絞り込み項目名", [絞り込み方法], [絞り込み項目の型])
        .AddSearchControl(Me.TxKotaiNo1, "EDAB.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtDateShiirebi, "EDAB.SIIREBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtEdaban1, "EDAB.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtJyouJyouNo, "EDAB.EDC", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstShiresaki1, "EDAB.SRCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.txtDummy, "EDAB.SIIREBI", typExtraction.EX_LTE, typColumnKind.CK_Date)
        ' 枝入力情報一覧の編集可能列設定（無し）
        .EditColumnList = CreateGrid2EditCol2()
      End With
    End With

  End Sub

  ''' <summary>
  ''' 個体識別履歴追跡の一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1
    Dim sql As String = String.Empty

    sql &= " Select CUTJ.* "
    sql &= "      , EDAB.EDC AS EDABAN"
    sql &= "      , CONCAT(FORMAT(CUTJ.UTKCODE,'0000') , ':' , LTKNAME)  AS DLTKNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.KAKUC,'00') , ':' , KZNAME)  AS DKZNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.KIKAKUC,'00') , ':' , KKNAME)  AS DKKNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.BICODE,'0000') , ':' , BINAME)  AS DBINAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.BLOCKCODE,'00') , ':' , BLNAME)  AS DBLNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.GBFLG,'00') , ':' , GBNAME)  AS DGBNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.GENSANCHIC,'00') , ':' , GNNAME)  AS DGNNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.SYUBETUC,'00') , ':' , SBNAME)  AS DSBNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.COMMENTC,'00') , ':' , CMNAME)  AS DCMNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.SRCODE,'0000') , ':' , LSRNAME)  AS DLSRNAME  "
    sql &= "      , CONCAT(FORMAT(CUTJ.TJCODE,'0000') , ':' , TJNAME)  AS DTJNAME  "
    sql &= "      , (ROUND((CUTJ.JYURYO / 100), 0, 1) / 10) AS JYURYOK "
    sql &= "      , IIF(NSZFLG = 0,'在庫' "
    sql &= "      , IIF(NSZFLG = 1,'返品' "
    sql &= "      , IIF(NSZFLG = 2,'出庫' "
    sql &= "      , IIF(NSZFLG = 3,'廃棄' "
    sql &= "      , IIF(NSZFLG = 5,'取消', '不明'))))) AS JYOUTAI "
    sql &= "      , IIF(SAYUKUBUN = 1,'左' "
    sql &= "      , IIF(SAYUKUBUN = 2,'右',' ')) AS LR2 "
    sql &= " FROM ((((((((((CUTJ  "
    sql &= "                  LEFT JOIN TOKUISAKI ON CUTJ.UTKCODE = TOKUISAKI.TKCODE) "
    sql &= "                  LEFT JOIN KAKU ON CUTJ.KAKUC = KAKU.KKCODE) "
    sql &= "                  LEFT JOIN KIKA ON CUTJ.KIKAKUC = KIKA.KICODE) "
    sql &= "                  LEFT JOIN BUIM ON CUTJ.BICODE = BUIM.BICODE)  "
    sql &= "                  LEFT JOIN BLOCK_TBL ON CUTJ.BLOCKCODE = BLOCK_TBL.BLOCKCODE)  "
    sql &= "                  LEFT JOIN GBFLG_TBL ON CUTJ.GBFLG = GBFLG_TBL.GBCODE)  "
    sql &= "                  LEFT JOIN GENSN ON CUTJ.GENSANCHIC = GENSN.GNCODE) "
    sql &= "                  LEFT JOIN SHUB ON CUTJ.SYUBETUC = SHUB.SBCODE) "
    sql &= "                  LEFT JOIN COMNT ON CUTJ.COMMENTC = COMNT.CMCODE)  "
    sql &= "                  LEFT JOIN CUTSR ON CUTJ.SRCODE = CUTSR.SRCODE)  "
    sql &= "                  LEFT JOIN TOJM ON CUTJ.TJCODE = TOJM.TJCODE "
    sql &= "                  LEFT JOIN EDAB ON CUTJ.KOTAINO = EDAB.KOTAINO "
    sql &= "                                and CUTJ.EBCODE = EDAB.EBCODE "
    sql &= " WHERE KYOKUFLG = 0 "
    sql &= "      And CUTJ.KUBUN <> 9 "
    sql &= "      And DKUBUN = 0 "
    sql &= " ORDER BY CUTJ.SRCODE "
    sql &= "        , KOTAINO "
    sql &= "        , KAKOUBI "
    sql &= "        , CUTJ.BICODE "
    sql &= "        , TOOSINO "

    Return sql
  End Function

  ''' <summary>
  ''' 個体識別履歴追跡の一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("状態", "JYOUTAI", argColumnWidth:=70, argFontSize:=11))
      .Add(New clsDGVColumnSetting("個体識別", "KOTAINO", argColumnWidth:=110, argTextAlignment:=typAlignment.MiddleCenter, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("枝番", "EDABAN", argColumnWidth:=70, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("上場コード", "EBCODE", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("仕入先", "DLSRNAME", argFontSize:=11, argColumnWidth:=250))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argFontSize:=11))
      .Add(New clsDGVColumnSetting("加工日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("ｶｰﾄﾝNo", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("格付", "DKZNAME", argFontSize:=11, argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("部位", "DBINAME", argFontSize:=11, argColumnWidth:=180))
      .Add(New clsDGVColumnSetting("左右", "LR2", argFontSize:=11, argColumnWidth:=70, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("S/P", "SPKUBUN", argColumnWidth:=60, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argColumnWidth:=80, argTextAlignment:=typAlignment.MiddleRight, argFormat:="##0.0"))
      .Add(New clsDGVColumnSetting("期限日", "KIGENBI", argFormat:="00/00/00", argColumnWidth:=90, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("商品C", "SHOHINC", argColumnWidth:=100, argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("入庫日", "KEIRYOBI", argTextAlignment:=typAlignment.BottomRight, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("出荷日", "SYUKKABI", argTextAlignment:=typAlignment.BottomRight, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("得意先", "DLTKNAME", argFontSize:=11, argColumnWidth:=200))
      .Add(New clsDGVColumnSetting("原単価", "GENKA", argFormat:="#,##0", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("売単価", "BAIKA", argFormat:="#,##0", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("返品日", "HENPINBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("伝票No", "DENNO", argColumnWidth:=100, argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("伝票行No", "GYONO", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("種別", "DSBNAME", argColumnWidth:=100, argFontSize:=11))
      .Add(New clsDGVColumnSetting("屠場", "DTJNAME", argFontSize:=11, argColumnWidth:=180))
      .Add(New clsDGVColumnSetting("シリアル", "SIRIALNO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=110))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argColumnWidth:=180, argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 個体識別履歴追跡の一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  Public Function CreateGrid2EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol1
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 枝入力情報一覧の一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid2Src2() As String Implements IDgvForm02.CreateGrid2Src2
    Dim sql As String = String.Empty

    sql &= " SELECT DISTINCT EDAB.SIIREBI "
    sql &= "      , CONCAT(FORMAT(EDAB.SRCODE,'0000') , ':' , LSRNAME)  AS DLSRNAME "
    sql &= "      , CONCAT(FORMAT(EDAB.KIKAKUC,'00') , ':' , KKNAME)  AS DKKNAME "
    sql &= "      , CONCAT(FORMAT(EDAB.SYUBETUC,'00') , ':' , SBNAME)  AS DSBNAME "
    sql &= "      , CONCAT(FORMAT(EDAB.KAKUC,'00') , ':' , KZNAME)  AS DKZNAME "
    sql &= "      , CONCAT(FORMAT(EDAB.GENSANCHIC,'00') , ':' , GNNAME)  AS DGNNAME "
    sql &= "      , CONCAT(FORMAT(EDAB.TJCODE,'00') , ':' , TJNAME)  AS DTJNAME "
    sql &= "      , EDAB.KOTAINO "
    sql &= "      , EDAB.EBCODE "
    sql &= "      , EDAB.EDC "
    sql &= "      , EDAB.KUBUN "
    sql &= "      , EDAB.FILER1 "
    sql &= "      , EDAB.FILER2 "
    sql &= "      , EDAB.NYUKOBI "
    sql &= "      , EDAB.JYURYO "
    sql &= "      , EDAB.JYURYO1 "
    sql &= "      , EDAB.JYURYO2 "
    sql &= "      , EDAB.SIIREGAKU "
    sql &= "      , EDAB.SIIREGAKU1 "
    sql &= "      , EDAB.SIIREGAKU2 "
    sql &= "      , EDAB.TDATE "
    sql &= "      , EDAB.KDATE "
    sql &= "      , EDAB.SFLG "
    sql &= "      , EDAB.KIKAKUC "
    sql &= "      , EDAB.SYUBETUC "
    sql &= "      , EDAB.KAKUC "
    sql &= "      , EDAB.SRCODE "
    sql &= "      , EDAB.GENSANCHIC "
    sql &= "      , EDAB.TJCODE  "
    sql &= "      , (ROUND((JYURYO / 100), 0, 1) / 10) AS JYURYOK "
    sql &= " FROM (((((EDAB  "
    sql &= "             LEFT JOIN TOJM  ON TOJM.TJCODE = EDAB.TJCODE)  "
    sql &= "             LEFT JOIN SHUB  ON SHUB.SBCODE = EDAB.SYUBETUC) "
    sql &= "             LEFT JOIN KIKA  ON KIKA.KICODE = EDAB.KIKAKUC) "
    sql &= "             LEFT JOIN KAKU  ON KAKU.KKCODE = EDAB.KAKUC) "
    sql &= "             LEFT JOIN GENSN ON GENSN.GNCODE = EDAB.GENSANCHIC)  "
    sql &= "             LEFT JOIN CUTSR ON EDAB.SRCODE = CUTSR.SRCODE "
    sql &= " WHERE EDAB.KUBUN <> 9  "
    sql &= " ORDER BY EDAB.SRCODE "
    sql &= "        , SIIREBI "
    sql &= "        , KOTAINO "

    Return sql
  End Function

  ''' <summary>
  ''' 枝入力情報一覧の一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("仕入日付", "SIIREBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=105))
      .Add(New clsDGVColumnSetting("仕入先", "DLSRNAME", argFontSize:=11, argColumnWidth:=210))
      .Add(New clsDGVColumnSetting("規格", "DKKNAME", argFontSize:=11, argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("種別", "DSBNAME", argColumnWidth:=100, argFontSize:=11))
      .Add(New clsDGVColumnSetting("等級", "DKZNAME", argColumnWidth:=140, argFontSize:=11))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argFontSize:=11))
      .Add(New clsDGVColumnSetting("個体識別", "KOTAINO", argColumnWidth:=110, argTextAlignment:=typAlignment.MiddleCenter, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("枝番", "EBCODE", argColumnWidth:=70, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("屠場", "DTJNAME", argFontSize:=11, argColumnWidth:=180))
      .Add(New clsDGVColumnSetting("上場コード", "EDC", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("入荷日", "NYUKOBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("1頭重量", "JYURYOK", argColumnWidth:=120, argTextAlignment:=typAlignment.MiddleRight, argFormat:="##0.0"))
      .Add(New clsDGVColumnSetting("1頭仕入額", "SIIREGAKU", argFormat:="#,##0", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argColumnWidth:=180, argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 枝入力情報一覧の一覧表示編集可能カラム設定
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
  ''' 選択された枝番の詳細情報を表示
  ''' </summary>
  Private Sub ReloadEdaList2EdaDetail()
    Try
      EdaList2EdaDetail(MyBase.Controlz(DG2V2.Name).GetAllData(_EdabRowPos))
      Me.lblPage.Text = (_EdabRowPos + 1).ToString() & "/" & DG2V2.Rows.Count.ToString()
      Me.btnNext.Enabled = (_EdabRowPos < (DG2V2.Rows.Count - 1))
      Me.btnPrevious.Enabled = (_EdabRowPos > 0)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("枝情報詳細の表示に失敗しました")
    End Try
  End Sub


  ''' <summary>
  ''' 枝入力情報一覧の任意の1件を枝入力情報に表示する
  ''' </summary>
  ''' <param name="prmSrcData">表示対象の枝入力情報一覧</param>
  ''' <remarks>パラメータ省略時は表示クリア</remarks>
  Private Sub EdaList2EdaDetail(Optional prmSrcData As Dictionary(Of String, String) = Nothing)

    If prmSrcData Is Nothing Then
      ' データクリア
      Me.lblSIIREBI.Text = ""   ' 仕入日
      Me.lblDKKNAME.Text = ""   ' 規格
      Me.lblDKZNAME.Text = ""   ' 格付け
      Me.lblKOTAINO.Text = ""   ' 個体識別番号
      Me.lblDTJNAME.Text = ""   ' 屠場
      Me.lblDLSRNAME.Text = ""  ' 仕入先
      Me.lblDSBNAME.Text = ""   ' 種別
      Me.lblDGNNAME.Text = ""   ' 原産地
      Me.lblEBCODE.Text = ""    ' 枝番
      Me.lblEDC.Text = ""       ' 上場番号
    Else
      'データ表示
      Me.lblSIIREBI.Text = Date.Parse(prmSrcData("SIIREBI")).ToString("yyyy/MM/dd") ' 仕入日付
      Me.lblDKKNAME.Text = prmSrcData("DKKNAME")                                    ' 規格
      Me.lblDKZNAME.Text = prmSrcData("DKZNAME")                                    ' 格付け
      Me.lblKOTAINO.Text = prmSrcData("KOTAINO").PadLeft(10, "0"c)                  ' 個体識別番号
      Me.lblDTJNAME.Text = prmSrcData("DTJNAME")                                    ' 屠場
      Me.lblDLSRNAME.Text = prmSrcData("DLSRNAME")                                  ' 仕入先
      Me.lblDSBNAME.Text = prmSrcData("DSBNAME")                                    ' 種別
      Me.lblDGNNAME.Text = prmSrcData("DGNNAME")                                    ' 原産地
      Me.lblEBCODE.Text = prmSrcData("EBCODE")                                      ' 枝番
      Me.lblEDC.Text = prmSrcData("EDC")                                            ' 上場番号
    End If

  End Sub


  ''' <summary>
  ''' タブコントロール初期化
  ''' </summary>
  Private Sub InitTabCtrl()

    With Me.TabControl1
      .SizeMode = TabSizeMode.Fixed
      .ItemSize = New Size(360, 40)

      .SelectedIndex = 0
      .SelectedIndex = 2
      .SelectedIndex = 1
    End With

    Me.TabPage1.Text = "枝入力情報"
    Me.TabPage2.Text = "個体識別履歴追跡"
    Me.TabPage3.Text = "枝入力情報一覧"

  End Sub

#End Region
#End Region

#Region "イベントプロシージャー"

#Region "フォーム"

  ''' <summary>
  ''' フォームロード時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_KotaiTrace_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = PRG_NAME

    Call InitForm02()

    Call InitTabCtrl()

    'TabControlをオーナードローする
    'DirectCast(Me.Panel1.Controls(0), TabControl).DrawMode = TabDrawMode.OwnerDrawFixed
    TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed

    ' 標準以外のコントロールメッセージ設定
    Me.TxtDateShiirebi.SetMsgLabelText("検索したい仕入日を入力します。")

    ' コントロールメッセージ表示ラベル設定
    MyBase.SetMsgLbl(Me.lblInformation)

    ' ダミー表示
    ' 見出し行のみ表示させるため、存在しない条件で検索
    Me.txtDummy.Text = "1900/01/01"
    MyBase.Controlz(DG2V1.Name).ShowList()
    MyBase.Controlz(DG2V2.Name).ShowList()
    Me.txtDummy.Text = String.Empty

    ' 枝入力情報一覧再表示時処理にコールバックを設定
    MyBase.Controlz(DG2V2.Name).lcCallBackReLoadData = AddressOf ReloadData

    ' IPC通信起動
    Call InitIPC(PRG_ID)

    ' 個体識別番号にフォーカスをあてる
    Me.ActiveControl = Me.TxKotaiNo1

    ' 仕入日付初期値設定
    Me.txtSearchValGTE.Text = Date.Parse(ComGetProcDate()).AddMonths(-2).ToString("yyyy/MM/dd")

    ' 枝情報詳細切替ボタン使用不可
    Me.btnNext.Enabled = False
    Me.btnPrevious.Enabled = False

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
        Me.btnSearch.Focus()
        Me.btnSearch.PerformClick()
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        Me.btnClose.Focus()
        Me.btnClose.PerformClick()
    End Select
  End Sub

#End Region

#Region "ボタン"

  ''' <summary>
  ''' 検索ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

    If TxtEdaban1.Text = "" _
      AndAlso TxtDateShiirebi.Text = "" _
      AndAlso TxKotaiNo1.Text = "" _
      AndAlso ComNothing2ZeroText(CmbMstShiresaki1.SelectedItem) = "0" Then
      txtSearchValGTE.Text = Date.Parse(ComGetProcDate()).AddMonths(-2).ToString("yyyy/MM/dd")
      txtSearchValEQ.Text = ""
    Else
      txtSearchValGTE.Text = ""
      txtSearchValEQ.Text = Me.TxtDateShiirebi.Text
    End If

    ' 検索処理実行
    MyBase.Controlz(DG2V1.Name).ShowList()
    MyBase.Controlz(DG2V2.Name).ShowList()
  End Sub

  ''' <summary>
  ''' 閉じるボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click


    Me.txtDummy.Text = "1900/01/01"


    With MyBase.Controlz(DG2V1.Name)
      .InitSort()
      .ResetPosition()
      .ShowList()
    End With
    DG2V1.Visible = True

    With MyBase.Controlz(DG2V2.Name)
      .InitSort()
      .ResetPosition()
      .ShowList()
    End With
    DG2V2.Visible = True

    ' 画面を閉じる
    Me.Hide()

    MyBase.AllClear()
    Me.txtDummy.Text = ""

    Me.TabControl1.SelectedIndex = 1

    Me.TxKotaiNo1.Focus()

  End Sub


  ''' <summary>
  ''' Nextボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

    Try
      If _EdabRowPos < (DG2V2.Rows.Count - 1) Then
        _EdabRowPos += 1
        ReloadEdaList2EdaDetail()
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' Previousボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click

    Try
      If _EdabRowPos > 0 Then
        _EdabRowPos -= 1
        ReloadEdaList2EdaDetail()
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try
  End Sub

#End Region

#Region "データグリッド"

  ''' <summary>
  ''' データ再表示時処理
  ''' </summary>
  ''' <param name="sender">イベント発生元</param>
  ''' <param name="prmLastUpdate">最終更新時刻</param>
  ''' <param name="prmRowCount">レコード件数</param>
  ''' <remarks>枝入力情報一覧再表示時に枝入力情報の表示を行う</remarks>
  Private Sub ReloadData(sender As DataGridView, prmLastUpdate As String, prmRowCount As Long)

    _EdabRowPos = 0

    If prmRowCount > 0 Then
      ' 枝入力情報一覧の1レコード目を枝入力情報に表示する
      EdaList2EdaDetail(MyBase.Controlz(DG2V2.Name).GetAllData(0))
      Me.lblPage.Text = "1/" & prmRowCount.ToString()
      Me.btnPrevious.Enabled = False
      Me.btnNext.Enabled = (prmRowCount > 1)
    Else
      EdaList2EdaDetail()
      Me.lblPage.Text = "0/0"
      Me.btnPrevious.Enabled = False
      Me.btnNext.Enabled = False
    End If

  End Sub

  ''' <summary>
  ''' セル選択時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
    Static tmpRowPos As Long = -1

    Try
      If e.RowIndex >= 0 Then
        If tmpRowPos <> e.RowIndex Then
          'クリックされた行のデータを枝入力情報に表示する
          tmpRowPos = e.RowIndex
          _EdabRowPos = tmpRowPos
          ReloadEdaList2EdaDetail()
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

#End Region

#Region "タブコントロール"

  ''' <summary>
  ''' タブコントロール表示切り替え時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' 参考)
  ''' https://dobon.net/vb/dotnet/control/tabownerdraw.html
  ''' </remarks>
  Private Sub TabControl1_DrawItem(ByVal sender As Object _
                                 , ByVal e As DrawItemEventArgs) Handles TabControl1.DrawItem

    '対象のTabControlを取得
    Dim tab As TabControl = CType(sender, TabControl)
    'タブページのテキストを取得
    Dim txt As String = tab.TabPages(e.Index).Text

    'タブのテキストと背景を描画するためのブラシを決定する
    Dim foreBrush, backBrush As Brush
    If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
      '選択されているタブのテキストを白、背景を青とする
      foreBrush = Brushes.White
      backBrush = Brushes.Blue
    Else
      '選択されていないタブのテキストは灰色、背景を白とする
      foreBrush = Brushes.Gray
      backBrush = Brushes.White
    End If

    'StringFormatを作成
    Dim sf As New StringFormat
    '中央に表示する
    sf.Alignment = StringAlignment.Center
    sf.LineAlignment = StringAlignment.Center

    '背景の描画
    e.Graphics.FillRectangle(backBrush, e.Bounds)
    'Textの描画
    e.Graphics.DrawString(txt, e.Font, foreBrush, RectangleF.op_Implicit(e.Bounds), sf)
  End Sub

  ''' <summary>
  ''' 在庫の場合、文字色を青色とする
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting

    Dim dgv As DataGridView = CType(sender, DataGridView)
    If dgv.Columns(e.ColumnIndex).Name = "JYOUTAI" Then
      If (e.Value.ToString = "在庫") Then
        e.CellStyle.ForeColor = Color.Blue
      Else
        e.CellStyle.ForeColor = Color.Black
      End If
    End If

  End Sub

  Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

  End Sub

#End Region
#End Region

End Class
