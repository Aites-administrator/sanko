Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonCon.DbConnectData
Imports CommonPcaDx
Imports OUTCONV

Public Class Form_OutConv4
  Implements IDgvForm02

#Region "定数定義"

  Private Const PRG_TITLE As String = "仕入データ変換選択画面"
  Private Const PRG_ID As String = "OutConv4"

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_OutConv4, AddressOf ComRedisplay)
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

    '１つ目のDataGridViewオブジェクトの設定
    DG2V1 = Me.DataGridView1
    '２つ目のDataGridViewオブジェクトの設定
    DG2V2 = Me.DataGridView2

    ' グリッド初期化
    Call InitGrid(DG2V1, CreateGrid2Src1(), CreateGrid2Layout1(), New clsDataGridSelecter(New List(Of String)({"SIIRECODE"})))

    With DG2V1

      ' １つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)
        ' １つ目のDataGridViewオブジェクトの固定列設定
        '.FixedRow = 2

        ' １つ目のDataGridViewオブジェクトの検索コントロール設定
        .AddSearchControl(Me.CmbDateShiirebi1, "SIIREBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.CmbMstShiresaki1, "SIIRECODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' １つ目のDataGridViewオブジェクトの編集可能列設定
        .EditColumnList = CreateGrid2EditCol1()

      End With
    End With

    Call InitGrid(DG2V2, CreateGrid2Src2(), CreateGrid2Layout2() _
                  , New clsDataGridSelecter(New List(Of String)({"SIIRECODE", "SYOHINC", "GBIKOU"}) _
                                            , prmSelectingCondition:=New Dictionary(Of String, String)() From {{"POST_STAT", "未完了"}}))

    With DG2V2
      ' ２つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)

        ' ２つ目のDataGridViewオブジェクトの検索コントロール設定
        .AddSearchControl(Me.CmbDateShiirebi1, "SIIREBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.CmbMstShiresaki1, "SIIRECODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' ２つ目のDataGridViewオブジェクトの編集可能列設定
        .EditColumnList = CreateGrid2EditCol2()

      End With
    End With

  End Sub

  ''' <summary>
  ''' 仕入先一覧表示SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1
    Dim sql As String = String.Empty

    sql &= " SELECT SRC.* "
    sql &= "       ,IIF(GFLG <> 1 ,  '未完了', IIF(DEN = 0  , '未完了' ,  '完了')) as POST_STAT "
    sql &= " FROM ( SELECT SIIREBI "
    sql &= "             , SIIRECODE "
    sql &= "             , MIN(DENPYOUNO) AS DEN "
    sql &= "             , MIN(GYOBAN) AS GYO "
    sql &= "             , SIIRESAKIMEI "
    sql &= "             , MIN(FLG) AS GFLG "
    sql &= "             , FORMAT(CONVERT(int, SIIRECODE) ,'0000') AS SORT_NUM"
    sql &= "         FROM SIIRE   "
    sql &= "         WHERE FLG < 2   "
    sql &= "         GROUP BY SIIREBI "
    sql &= "                , SIIRECODE "
    sql &= "                , SIIRESAKIMEI  "
    sql &= "      ) AS SRC"
    sql &= " ORDER BY SIIREBI DESC  "
    sql &= "        , SORT_NUM  "

    Return sql
  End Function

  ''' <summary>
  ''' 仕入先一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("PCA送信", "POST_STAT", argColumnWidth:=100, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("仕入日", "SIIREBI", argTextAlignment:=typAlignment.MiddleRight, argFormat:="yyyy/MM/dd"))
      .Add(New clsDGVColumnSetting("仕入先コード", "SIIRECODE", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("仕入先名", "SIIRESAKIMEI", argColumnWidth:=260))
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

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 仕入明細一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid2Src2() As String Implements IDgvForm02.CreateGrid2Src2
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= "     , ROUND(TANKA * GSURYO , 0, 1) AS KINGAKUW "
    sql &= "     , IIF(FLG > 0, '完了' , '未完了') AS POST_STAT "
    sql &= "     , IsNULL(SIHENKA.ZeiRitu, 8) AS ZeiRitu1"
    sql &= " FROM( SELECT  NHOUHO "
    sql &= "       , KAMOKU "
    sql &= "       , DENKU "
    sql &= "       , SIIREBI "
    sql &= "       , FLG "
    sql &= "       , SIIRECODE "
    sql &= "       , SYOHINC "
    sql &= "       , DENPYOUNO "
    sql &= "       , GYOBAN "
    sql &= "       , SEISANBI "
    sql &= "       , SIIRESAKIMEI "
    sql &= "       , SENPOUTANTOU "
    sql &= "       , BUMONC "
    sql &= "       , TANTOUC "
    sql &= "       , TEKIYOUC "
    sql &= "       , TEKIYOUMEI "
    sql &= "       , MASKUBUN "
    sql &= "       , HINMEI "
    sql &= "       , KU "
    sql &= "       , SOUKO "
    sql &= "       , IRISU "
    sql &= "       , TANNI "
    sql &= "       , TANKA "
    sql &= "       , ZEIKU "
    sql &= "       , ZEIKOMI "
    sql &= "       , MIN(BIKOU) AS GBIKOU "
    sql &= "       , KIKAKU "
    sql &= "       , IRO "
    sql &= "       , SIZE "
    sql &= "       , SIKI "
    sql &= "       , SKOUMOKU1 "
    sql &= "       , (SKOUMOKU2) AS GSKOUMOKU2 "
    sql &= "       , Min(SKOUMOKU3) AS  GSKOUMOKU3 "
    sql &= "       , UKOUMOKU1 "
    sql &= "       , UKOUMOKU2 "
    sql &= "       , UKOUMOKU3 "
    sql &= "       , Sum(HAKOSU) AS GHAKOSU "
    sql &= "       , Sum(SURYO) AS GSURYO "
    sql &= "       , Min(SURYO) AS SORT_SURYO "
    sql &= "       , Sum(KINGAKU) AS GKINGAKU "
    sql &= "       , Sum(SOTOZEI) AS GSOTOZEI "
    sql &= "       , Sum(UCHIZEI) AS GUCHIZEI  "
    sql &= "       , MIN([UPDATE]) AS GUPDATE "
    sql &= "       , COUNT(SURYO) AS KENSU "
    sql &= " From SIIRE "
    sql &= " WHERE   FLG < 2 "
    sql &= " GROUP BY NHOUHO "
    sql &= "        , KAMOKU "
    sql &= "        , DENKU "
    sql &= "        , SIIREBI "
    sql &= "        , FLG "
    sql &= "        , SIIRECODE "
    sql &= "        , SYOHINC "
    sql &= "        , DENPYOUNO "
    sql &= "        , GYOBAN "
    sql &= "        , SEISANBI "
    sql &= "        , SIIRESAKIMEI "
    sql &= "        , SENPOUTANTOU "
    sql &= "        , BUMONC "
    sql &= "        , TANTOUC "
    sql &= "        , TEKIYOUC "
    sql &= "        , TEKIYOUMEI "
    sql &= "        , MASKUBUN "
    sql &= "        , HINMEI "
    sql &= "        , KU "
    sql &= "        , SOUKO "
    sql &= "        , IRISU "
    sql &= "        , TANNI "
    sql &= "        , TANKA "
    sql &= "        , ZEIKU "
    sql &= "        , ZEIKOMI "
    sql &= "        , KIKAKU "
    sql &= "        , IRO "
    sql &= "        , SIZE "
    sql &= "        , SIKI "
    sql &= "        , SKOUMOKU1 "
    sql &= "        , SKOUMOKU2 "
    sql &= "        , UKOUMOKU1 "
    sql &= "        , UKOUMOKU2 "
    sql &= "        , UKOUMOKU3 "
    sql &= "        , BIKOU ) AS SRC LEFT JOIN SIHENKA ON SRC.SIIRECODE = SIHENKA.SRCODE "
    sql &= " ORDER BY SIIREBI DESC "
    sql &= "        , FORMAT(convert(int,SIIRECODE) , '0000') "
    sql &= "        , FLG "
    sql &= "        , DENPYOUNO "
    sql &= "        , GYOBAN"
    sql &= "        , convert(int,SYOHINC) "
    sql &= "        , SORT_SURYO"

    Return sql
  End Function

  ''' <summary>
  ''' 仕入明細一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("PCA送信", "POST_STAT", argColumnWidth:=100, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("仕入日", "SIIREBI", argTextAlignment:=typAlignment.MiddleRight, argFormat:="yyyy/MM/dd"))
      .Add(New clsDGVColumnSetting("仕入先コード", "SIIRECODE", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("仕入先名", "SIIRESAKIMEI", argColumnWidth:=260))
      .Add(New clsDGVColumnSetting("枝番", "GSKOUMOKU2", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("商品コード", "SYOHINC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=110))
      .Add(New clsDGVColumnSetting("商品名", "HINMEI", argColumnWidth:=200))
      .Add(New clsDGVColumnSetting("重量", "GSURYO", argTextAlignment:=typAlignment.MiddleRight, argFormat:="0.0", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("金額", "KINGAKUW", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("伝票番号", "DENPYOUNO", argTextAlignment:=typAlignment.MiddleRight))
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

#Region "データ送信関連"

  ''' <summary>
  ''' 仕入明細一覧データをPCA仕入伝票として登録する
  ''' </summary>
  ''' <param name="prmDb">データベースオブジェクト</param>
  ''' <remarks>
  '''  下段GRIDのチェックを付けた行を仕入伝票データとしてAPIを用いてPCAに送信
  ''' </remarks>
  Private Sub DataPost(prmDb As clsSqlServer _
                       , prmStartNumber As Long)
    Dim tmpPcaNyk As New clsPcaNYK(PCAAPI_USERID, PCAAPI_PASSWORD, PCAAPI_PG_ID, PCAAPI_PG_NAME, PCAAPI_DATAAREANAME)
    Dim tmpSlipNumber As Long = prmStartNumber
    Dim tmpLastSIIRECODE As Long = Long.MinValue
    Dim tmpLineNumber As Long = 0
    Dim tmpUriZeiSbt As Decimal = Decimal.MinValue
    Dim tmpGenZeiSbt As Decimal = Decimal.MinValue
    Dim PcaDataBase As New clsPcaDb
    Dim tmpSmsData As Dictionary(Of String, String)
    Dim tmpZeiritsu As Decimal

    ' 仕入明細一覧を最終行までループ
    For Each tmpGridData As Dictionary(Of String, String) In Controlz(DG2V2.Name).GetAllData()

      ' 選択データのみ対象
      If tmpGridData("SelecterCol") <> "〇" Then
        Continue For
      End If

      ' 伝票番号操作
      If Long.Parse(tmpGridData("SIIRECODE")) <> tmpLastSIIRECODE Then
        ' 仕入先が変更されたら(もしくは1件目データの場合)伝票番号を採番

        tmpLastSIIRECODE = Long.Parse(tmpGridData("SIIRECODE"))
        tmpSlipNumber += 1
        tmpLineNumber = 0

        ' 仕入伝票ヘッダー作成
        tmpPcaNyk.AddHeader(CreateNykHeader(tmpSlipNumber, tmpGridData))
      ElseIf tmpLineNumber >= 8 Then
        ' 明細数が8行以上なら伝票番号を採番

        tmpSlipNumber += 1
        tmpLineNumber = 0

        ' 仕入伝票ヘッダー作成
        tmpPcaNyk.AddHeader(CreateNykHeader(tmpSlipNumber, tmpGridData))
      End If

      tmpLineNumber += 1

      ' 集計処理
      Call TallieUp(tmpGridData)

      tmpSmsData = PcaDataBase.GetZeiSbt(tmpGridData("SYOHINC").ToString())
      tmpZeiritsu = PcaDataBase.GetTaxRate(tmpLastSIIRECODE, tmpGridData("SYOHINC").ToString(), True)

      ' 仕入伝票明細作成
      tmpPcaNyk.AddDetail(CreateNykDetail(tmpGridData, tmpSmsData, tmpZeiritsu))

      'SIIREテーブル更新
      Call UpDateSIIRE(prmDb, Long.Parse(tmpGridData("KENSU")), tmpSlipNumber, tmpLineNumber, tmpGridData)

    Next

    If tmpPcaNyk.EntryCount >= 1 Then

      ' 伝票番号テーブル更新
      prmDb.Execute(SqlUpdDENNOTB(tmpSlipNumber))

      ' 仕入伝票送信
      tmpPcaNyk.Create()

    End If

  End Sub

  ''' <summary>
  ''' 集計処理
  ''' </summary>
  ''' <param name="tmpDicDst"></param>
  ''' <remarks>
  '''  内税額・外税額・粗利額を集計する
  ''' </remarks>
  Private Sub TallieUp(ByRef tmpDicDst As Dictionary(Of String, String))
    Dim tmpZeiRitu As Decimal = Decimal.Parse(tmpDicDst("ZeiRitu1"))
    Dim tmpGsuryo As Decimal = Decimal.Parse(tmpDicDst("GSURYO"))
    Dim tmpTANKA As Decimal = Decimal.Parse(tmpDicDst("TANKA"))

    Dim tmpGosa As Decimal = 0
    Dim tmpWKingaku As Decimal = 0
    Dim tmpArari As Decimal = 0
    Dim tmpSotozei As Decimal = 0
    Dim tmpUchizei As Decimal = 0

    If tmpGsuryo >= 0 Then
      tmpGosa = 0.01
    Else
      tmpGosa = -0.01
    End If

    tmpWKingaku = Fix(tmpTANKA * tmpGsuryo + tmpGosa)
    Select Case tmpDicDst("ZEIKOMI")
      Case "0"
        tmpArari = tmpWKingaku
        tmpSotozei = Fix(tmpWKingaku * (tmpZeiRitu / 100) + tmpGosa)
        tmpUchizei = 0
      Case "1"
        tmpUchizei = Fix((tmpWKingaku / (100 + tmpZeiRitu)) * tmpZeiRitu + tmpGosa)
        tmpArari = tmpWKingaku - tmpUchizei
        tmpSotozei = 0
      Case "2"
        tmpArari = tmpWKingaku
        tmpSotozei = 0
        tmpUchizei = 0
    End Select

    ComSetDictionaryVal(tmpDicDst, "ARARI", tmpArari.ToString())
    ComSetDictionaryVal(tmpDicDst, "Sotozei", tmpSotozei.ToString())
    ComSetDictionaryVal(tmpDicDst, "UchiZei", tmpUchizei.ToString())
    ComSetDictionaryVal(tmpDicDst, "WKingaku", tmpWKingaku.ToString())

  End Sub

  ''' <summary>
  ''' 伝票番号の採番を行う
  ''' </summary>
  ''' <returns>伝票番号</returns>
  Private Function AssignNumber(prmDb As clsSqlServer) As Long
    Dim ret As Long = Long.MinValue
    Dim tmpDt As New DataTable

    Try
      With prmDb
        .GetResult(tmpDt, " SELECT * FROM DenNoTB WHERE KUBUN = 1 ")

        If tmpDt.Rows.Count <= 0 Then
          Throw New Exception("伝票番号管理テーブル不正")
        Else
          If Long.Parse(tmpDt.Rows(0)("DENNO")) + 1 > 999999 Then
            ret = 500000
          Else
            ret = Long.Parse(tmpDt.Rows(0)("DENNO"))
          End If
        End If

      End With

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("伝票番号の採番に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try

    Return ret
  End Function


  ''' <summary>
  ''' 仕入伝票ヘッダー作成
  ''' </summary>
  ''' <param name="prmSlipNumber">伝票番号</param>
  ''' <param name="prmDicRead">仕入伝票データ</param>
  ''' <returns>仕入伝票ヘッダーオブジェクト</returns>
  Private Function CreateNykHeader(prmSlipNumber As Long _
                                 , prmDicRead As Dictionary(Of String, String)) As clsPcaNYKH
    Dim tmpNykH As New clsPcaNYKH

    With tmpNykH
      .仕入科目 = prmDicRead("KAMOKU")
      .伝区 = prmDicRead("DENKU")
      .仕入日 = Date.Parse(prmDicRead("SIIREBI")).ToString("yyyyMMdd")
      .精算日 = Date.Parse(prmDicRead("SEISANBI")).ToString("yyyyMMdd")
      .伝票番号 = prmSlipNumber.ToString()
      .仕入先コード = prmDicRead("SIIRECODE")
      .部門コード = prmDicRead("BUMONC")
      .担当者コード = prmDicRead("TANTOUC")
      .摘要コード = prmDicRead("TEKIYOUC")
      If prmDicRead("TEKIYOUMEI") <> "" Then
        .摘要名 = prmDicRead("TEKIYOUMEI")
      End If
    End With

    Return tmpNykH
  End Function

  ''' <summary>
  ''' 仕入伝票明細の作成
  ''' </summary>
  ''' <param name="prmDicRead">仕入明細データ</param>
  ''' <returns>仕入伝票明細オブジェクト</returns>
  Private Function CreateNykDetail(prmDicRead As Dictionary(Of String, String) _
                                     , prmSmsData As Dictionary(Of String, String) _
                                     , prmZeiritsu As Decimal) As clsPcaNYKD
    Dim tmpNykD As New clsPcaNYKD

    With tmpNykD
      .商品コード = prmDicRead("SYOHINC")
      .マスター区分 = prmDicRead("MASKUBUN")
      .税区分 = prmSmsData("smsp_tax")
      .税込区分 = prmSmsData("smsp_kankomi")
      .品名 = prmDicRead("HINMEI")
      .規格型番 = prmDicRead("KIKAKU")
      .色 = prmDicRead("IRO")
      .サイズ = prmDicRead("SIZE")
      .倉庫 = prmDicRead("SOUKO")
      .区 = prmDicRead("KU")
      .入数 = prmDicRead("IRISU")
      .箱数 = prmDicRead("GHAKOSU")
      .数量 = prmDicRead("GSURYO")
      .単位 = prmDicRead("TANNI")
      .単価 = prmDicRead("TANKA")
      .金額 = prmDicRead("WKingaku")
      .税率 = prmZeiritsu.ToString()
      .商品項目1 = prmDicRead("SKOUMOKU1")
      .商品項目2 = prmDicRead("GSKOUMOKU2")
      .商品項目3 = prmDicRead("GSKOUMOKU3")
      .仕入項目1 = prmDicRead("UKOUMOKU1")
      .仕入項目2 = prmDicRead("UKOUMOKU2")
      .仕入項目3 = prmDicRead("UKOUMOKU3")

      Dim tmpWsuryo As Decimal = Decimal.Parse(prmDicRead("GSURYO")) * 1000
      If tmpWsuryo Mod 10 <> 0 Then
        .数量小数桁 = "3"
      ElseIf tmpWsuryo Mod 100 <> 0 Then
        .数量小数桁 = "2"
      Else
        .数量小数桁 = "1"
      End If

      Select Case prmDicRead("ZEIKOMI")
        Case "0"
          ' 外税
          .外税額 = prmDicRead("Sotozei")
        Case "1"
          ' 内税
          .内税額 = prmDicRead("UchiZei")
      End Select

      If Decimal.Parse(prmDicRead("GBIKOU")) > 0 Then
        .備考 = prmDicRead("GBIKOU").PadLeft(10, "0"c)
      Else
        .備考 = prmDicRead("GBIKOU")
      End If

      .仕入税種別 = prmSmsData("sms_kantaxkind")
    End With

    Return tmpNykD
  End Function

  ''' <summary>
  ''' SIIREテーブル更新
  ''' </summary>
  ''' <param name="prmDb">データベース接続</param>
  ''' <param name="prmPlannedCount">更新予定数</param>
  ''' <remarks>
  ''' 更新件数が更新予定数と異なる場合は例外を上げる
  ''' トランザクションのコミットorロールバックは呼出し元で行う
  ''' </remarks>
  Private Sub UpDateSIIRE(prmDb As clsSqlServer _
                        , prmPlannedCount As Long _
                        , prmSlipNumber As Long _
                        , prmLineNumber As Long _
                        , prmDicPost As Dictionary(Of String, String))
    Try
      If prmPlannedCount <> prmDb.Execute(SqlUpdSIIREFromGrid(prmSlipNumber, prmLineNumber, prmDicPost, ComGetProcTime())) Then
        Throw New Exception("SIIREテーブルの更新に失敗しました。他のユーザーによって更新されている可能性があります。")
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("SIIREテーブルの更新に失敗しました。")
    End Try


  End Sub
#End Region

#Region "データ件数取得"
  ''' <summary>
  ''' 未送信データ件数取得
  ''' </summary>
  ''' <returns>未送信データ件数</returns>
  Private Function GetUnSendCount() As Long
    Dim ret As Long = 0

    For Each tmpRow As Dictionary(Of String, String) In Me.Controlz(DG2V2.Name).GetAllData
      ret += Long.Parse(IIf(tmpRow("POST_STAT").ToString() = "未完了", "1", "0").ToString())
    Next

    Return ret
  End Function

  ''' <summary>
  ''' 送信予定件数表示
  ''' </summary>
  ''' <remarks>仕入明細一覧上のチェックがつけらている件数を表示する</remarks>
  Private Sub ShowPlanedCount()
    Dim tmpCount As Long = 0

    For Each tmpRow As Dictionary(Of String, String) In Me.Controlz(DG2V2.Name).GetAllData
      tmpCount += Long.Parse(IIf(tmpRow("SelecterCol").ToString() = "〇", "1", "0").ToString())
    Next

    If tmpCount > 0 Then
      Me.lblPostPlanedCount.Text = tmpCount.ToString() & "行のデータがPCA用データとして作成されます。"
    Else
      Me.lblPostPlanedCount.Text = ""
    End If

  End Sub
#End Region

#Region "SIIREテーブル作成関連"

  ''' <summary>
  ''' SIIREテーブル更新
  ''' </summary>
  ''' <remarks>
  ''' CUTJよりSIIREテーブルを追加更新削除する
  ''' </remarks>
  Private Sub CreateSiireTbl()
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim tmpDic As New List(Of Dictionary(Of String, String))
    Dim tmpProcTime As String = ComGetProcTime()

    Try

      ' 対象のCUTJ抽出
      tmpDb.GetResult(tmpDt, SqlSelCutJ())

      If tmpDt.Rows.Count > 0 Then
        tmpDic = ComDt2Dic(tmpDt)

        tmpDb.TrnStart()

        ' 指定日のSIIREテーブル削除
        tmpDb.Execute(SqlDelSIIRE())

        For Each tmpDr As Dictionary(Of String, String) In tmpDic

          Call GetTaxData(tmpDr)

          If tmpDr("KYOKUFLG") <> "0" Then
            ' KYOKUFLG = 0 なら対合のSIIREテーブル削除
            tmpDb.Execute(SqlDelSIIREFromCutJ(tmpDr))
          Else
            ' 取り合えず更新
            If 0 = tmpDb.Execute(SqlUpdSIIREFromCutJ(tmpDr, tmpProcTime)) Then
              ' 更新出来ない場合は追加
              If 1 <> tmpDb.Execute(SqlInsSIIREFromCutJ(tmpDr, tmpProcTime)) Then
                Throw New Exception("SIIREテーブルレコード追加に失敗しました。")
              End If
            End If
          End If
        Next

        tmpDb.TrnCommit()
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
      Throw New Exception("SIIREテーブルの作成に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

  End Sub

  ''' <summary>
  ''' 税関連データ取得
  ''' </summary>
  ''' <param name="prmCutJData"></param>
  ''' <remarks>
  ''' 粗利・内税額・外税額・税区分・税込区分を取得する
  ''' </remarks>
  Private Sub GetTaxData(ByRef prmCutJData As Dictionary(Of String, String))
    Dim tmpGosa As Decimal
    Dim tmpTanka As Decimal = Decimal.Parse(prmCutJData("GENKA"))
    Dim tmpSuryo As Decimal = Decimal.Parse(prmCutJData("SURYO"))
    Dim tmpKingaku As Decimal = 0
    Dim tmpZeiRitu As Decimal = Decimal.Parse(prmCutJData("ZeiRitu"))
    Dim tmpARARI As Decimal = 0
    Dim tmpSOTOZEI As Decimal = 0
    Dim tmpUCHIZEI As Decimal = 0
    Dim tmpZEIKOMI As Decimal = 0
    Dim tmpZEIKU As Decimal = 0

    If tmpSuryo >= 0 Then
      tmpGosa = 0.01
    Else
      tmpGosa = -0.01
    End If
    tmpKingaku = Fix(tmpTanka * tmpSuryo + tmpGosa)


    Select Case prmCutJData("ZEIKUBUN")
      Case "0"
        tmpARARI = tmpKingaku
        tmpSOTOZEI = Fix(tmpKingaku * (tmpZeiRitu / 100) + tmpGosa)
        tmpUCHIZEI = 0
        tmpZEIKOMI = 0
        tmpZEIKU = 2
      Case "1"
        tmpUCHIZEI = Fix((tmpKingaku / (100 + tmpZeiRitu)) * tmpZeiRitu + tmpGosa)
        tmpARARI = tmpKingaku - tmpUCHIZEI
        tmpSOTOZEI = 0
        tmpZEIKOMI = 1
        tmpZEIKU = 2
      Case "2"
        tmpARARI = tmpKingaku
        tmpSOTOZEI = 0
        tmpUCHIZEI = 0
        tmpZEIKOMI = 0
        tmpZEIKU = 0
    End Select

    ComSetDictionaryVal(prmCutJData, "Kingaku", tmpKingaku.ToString())
    ComSetDictionaryVal(prmCutJData, "ARARI", tmpARARI.ToString())
    ComSetDictionaryVal(prmCutJData, "SOTOZEI", tmpSOTOZEI.ToString())
    ComSetDictionaryVal(prmCutJData, "UCHIZEI", tmpUCHIZEI.ToString())
    ComSetDictionaryVal(prmCutJData, "ZEIKOMI", tmpZEIKOMI.ToString())
    ComSetDictionaryVal(prmCutJData, "ZEIKU", tmpZEIKU.ToString())

  End Sub

#End Region

#Region "SQL文作成"

  ''' <summary>
  ''' 伝票テーブル更新SQL文作成
  ''' </summary>
  ''' <param name="prmSlipNumber">伝票番号</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdDENNOTB(prmSlipNumber As Long) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE DENNOTB "
    sql &= " SET DENNO = " & prmSlipNumber.ToString()
    sql &= " WHERE KUBUN = 1 "

    Return sql
  End Function

  ''' <summary>
  ''' SIIREテーブル削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelSIIRE() As String
    Dim sql As String = String.Empty

    sql &= " DELETE FROM SIIRE "
    sql &= " WHERE SIIREBI = '" & CmbDateShiirebi1.SelectedValue.ToString() & "'"
    sql &= "   AND DENPYOUNO = 0 "
    sql &= "   AND FLG = 0 "

    Return sql
  End Function

  ''' <summary>
  ''' CUTJ抽出SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelCutJ() As String
    Dim sql As String = String.Empty

    sql &= " SELECT SRC.* "
    sql &= "      , IsNULL(BUIM.BINAME,' ') AS HINMEI "
    sql &= "      , IIF(SURYO >=0 , 0 ,1 ) AS KU "
    sql &= " FROM ( SELECT CUTJ.*  "
    sql &= "             , (ROUND(CUTJ.JYURYO / 100, 0 ,1) / 10) AS SURYO "
    sql &= "             , IsNULL(KIKA.KKNAME, CUTJ.KIKAKUC) AS KIKAKU "
    sql &= "             , IIF(CUTJ.BICODE <> 0, CUTJ.BICODE , IIF(CUTJ.KIKAKUC < 20, 800, 810)) AS SYOHINCODE "
    sql &= "             , IsNULL(CUTSR.LSRNAME,'') AS SIIRESAKIMEI "
    sql &= "             , IsNULL(SIHENKA.HENKAN ,CUTJ.SRCODE) AS SIIRECODE "
    sql &= "             , IsNULL(SIHENKA.ZEIKUBUN, 0 ) AS ZEIKUBUN "
    sql &= "             , IsNULL(SIHENKA.ZeiRitu, 8) AS ZeiRitu "
    sql &= "             , IsNULL(SIHENKA.GYONO, 8) AS MAXGYO "
    sql &= "        FROM (( CUTJ LEFT JOIN CUTSR ON CUTJ.SRCODE = CUTSR.SRCODE ) "
    sql &= "                     LEFT JOIN SIHENKA ON CUTJ.SRCODE = SIHENKA.SRCODE)  "
    sql &= "                     LEFT JOIN KIKA ON CUTJ.KIKAKUC = KIKA.KICODE "
    sql &= "        WHERE KEIRYOBI = '" & CmbDateShiirebi1.SelectedValue.ToString() & "'"
    sql &= "       	  AND CUTJ.KUBUN = 0  "
    sql &= "       	  AND (KYOKUFLG = 0 OR KYOKUFLG = 2) "
    sql &= "        	AND (CUTJ.GYONO <> 9999 OR NSZFLG = 8) "
    sql &= "          AND CUTJ.EBCODE IS NOT NULL "
    sql &= "      ) AS SRC LEFT JOIN BUIM ON SRC.SYOHINCODE = BUIM.BICODE "
    sql &= " ORDER BY SRCODE "
    sql &= "        , BICODE "
    sql &= " 				, KYOKUFLG DESC "
    sql &= " 				, TDATE "

    Return sql
  End Function


  ''' <summary>
  ''' CUTJ.KYOKUFLG=0の場合時にSIIREテーブルの該当レコードを削除するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelSIIREFromCutJ(prmCutJData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    sql &= " DELETE FROM SIIRE "
    sql &= SqlWhereSIIRE(prmCutJData)

    Return sql
  End Function

  ''' <summary>
  ''' CUTJよりSIIREテーブルを更新するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdSIIREFromCutJ(prmCutJData As Dictionary(Of String, String) _
                                       , prmProcTime As String) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE SIIRE "
    sql &= " SET SURYO = " & prmCutJData("SURYO")
    sql &= "   , TANKA = " & prmCutJData("GENKA")
    sql &= "   , Kingaku = " & prmCutJData("Kingaku")
    sql &= "   , SOTOZEI = " & prmCutJData("SOTOZEI")
    sql &= "   , UCHIZEI = " & prmCutJData("UCHIZEI")
    sql &= "   , ZEIKU = " & prmCutJData("ZEIKU")
    sql &= "   , ZEIKOMI = " & prmCutJData("ZEIKOMI")
    sql &= "   , [Update] = '" & prmProcTime & "'"
    sql &= SqlWhereSIIRE(prmCutJData)

    Return sql
  End Function

  ''' <summary>
  ''' SIIREテーブル追加SQL文作成
  ''' </summary>
  ''' <param name="prmCutJData">挿入対象のCUTJデータ</param>
  ''' <param name="prmProcTime">更新日時</param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' CUTJよりSIIREテーブルの新規レコードを追加する
  ''' </remarks>
  Private Function SqlInsSIIREFromCutJ(prmCutJData As Dictionary(Of String, String) _
                                       , prmProcTime As String) As String
    Dim sql As String = String.Empty
    Dim tmpKeyVal As New Dictionary(Of String, String)
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    With tmpKeyVal
      .Add("SIIREBI", "'" & prmCutJData("KEIRYOBI") & "'")
      .Add("SEISANBI", "'" & prmCutJData("KEIRYOBI") & "'")
      .Add("SIIRECODE", prmCutJData("SIIRECODE"))
      .Add("SIIRESAKIMEI", "'" & prmCutJData("SIIRESAKIMEI") & "'")
      .Add("TANTOUC", "0")
      .Add("SYOHINC", prmCutJData("SYOHINCODE"))
      .Add("HINMEI", "'" & prmCutJData("HINMEI") & "'")
      .Add("BIKOU", prmCutJData("KOTAINO"))
      .Add("SKOUMOKU2", prmCutJData("EBCODE"))
      .Add("SKOUMOKU3", prmCutJData("TOOSINO"))
      .Add("KU", prmCutJData("KU"))
      .Add("KIKAKU", "'" & prmCutJData("KIKAKU") & "'")
      .Add("Size", "' '")
      .Add("IRO", "' '")
      .Add("SOUKO", "0")
      .Add("SENPOUTANTOU", "' '")

      .Add("SURYO", prmCutJData("SURYO"))
      .Add("TANKA", prmCutJData("GENKA"))       ' ← ※注意！ CUTJでの仕入単価は TANKAではなくGENKAです。
      .Add("Kingaku", prmCutJData("Kingaku"))
      .Add("SOTOZEI", prmCutJData("SOTOZEI"))
      .Add("UCHIZEI", prmCutJData("UCHIZEI"))
      .Add("ZEIKU", prmCutJData("ZEIKU"))
      .Add("ZEIKOMI", prmCutJData("ZEIKOMI"))
      .Add("[Update]", "'" & prmProcTime & "'")
    End With

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst &= tmpKey & ","
      tmpVal &= tmpKeyVal(tmpKey) & ","
    Next

    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= "INSERT INTO SIIRE (" & tmpDst & ")"
    sql &= "            VALUES(" & tmpVal & ") "

    Return sql
  End Function

  ''' <summary>
  ''' SIIREテーブル抽出条件共通
  ''' </summary>
  ''' <param name="prmCutJData"></param>
  ''' <returns>SQL文のWHERE句</returns>
  Private Function SqlWhereSIIRE(prmCutJData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    sql &= " WHERE CONVERT(int,SIIRECODE) = " & prmCutJData("SIIRECODE")
    sql &= "     AND SIIREBI = '" & CmbDateShiirebi1.SelectedValue.ToString() & "'"
    sql &= "     AND CONVERT(int, SYOHINC) = " & prmCutJData("SYOHINCODE")
    sql &= "     AND SURYO = " & prmCutJData("SURYO")
    sql &= "     AND BIKOU = " & prmCutJData("KOTAINO")
    sql &= "     AND SKOUMOKU2 = " & prmCutJData("EBCODE")
    sql &= "     AND SKOUMOKU3 = " & prmCutJData("TOOSINO")

    Return sql
  End Function

  ''' <summary>
  ''' 仕入伝票送結果よSIIREテーブルを更新するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdSIIREFromGrid(prmSlipNumver As Long _
                                      , prmLineNumber As Long _
                                      , prmPostData As Dictionary(Of String, String) _
                                      , prmProcTimes As String) As String
    Dim sql As String = String.Empty


    sql &= " UPDATE SIIRE"
    sql &= " SET [Update] =  '" & prmProcTimes & "'"
    sql &= "      , FLG = 1 "
    sql &= "      , DENPYOUNO = " & prmSlipNumver.ToString()
    sql &= "      , GYOBAN = " & prmLineNumber.ToString()
    sql &= " WHERE SIIREBI = '" & prmPostData("SIIREBI") & "'"
    sql &= "   AND SIIRECODE = '" & prmPostData("SIIRECODE") & "'"
    sql &= "   AND SYOHINC = '" & prmPostData("SYOHINC") & "'"
    sql &= "   AND TANKA = " & prmPostData("TANKA")
    sql &= "   AND KU = " & prmPostData("KU")
    sql &= "   AND SKOUMOKU2 = " & prmPostData("GSKOUMOKU2")
    sql &= "   AND BIKOU = '" & prmPostData("GBIKOU") & "'"

    Return sql
  End Function
#End Region

#End Region

#End Region

#Region "イベントプロシージャー"

#Region "フォーム関連"
  ''' <summary>
  ''' フォームロード時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_OutConv4_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    ' IPC通信起動
    InitIPC(PRG_ID)

    ' 画面タイトル設定
    Me.Text = PRG_TITLE

    ' Grid初期化
    Call InitForm02()

    ' 仕入先一覧イベント設定
    With Controlz(Me.DG2V1.Name)
      .lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick  ' ダブルクリック時処理
      .lcCallBackReLoadData = AddressOf ReLoadDataSupplierList   ' 再表示時処理
    End With

    ' 入荷明細一覧イベント設定
    With Controlz(Me.DG2V2.Name)
      .lcCallBackReLoadData = AddressOf ReLoadDataItemDetail   ' 再表示時処理
    End With

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)

    ' 仕入日の先頭を選択
    Me.CmbDateShiirebi1.SelectedIndex = 0

    ' 自動検索ON
    Controlz(DG2V1.Name).AutoSearch = True
    Controlz(DG2V2.Name).AutoSearch = True

    ' 並び替え不可
    Controlz(DG2V1.Name).SortActive = False
    Controlz(DG2V2.Name).SortActive = False

    ' 非表示 → 表示時処理設定
    MyBase.lcCallBackShowFormLc = AddressOf ReStartPrg

    Me.ActiveControl = Me.CmbDateShiirebi1
  End Sub

  ''' <summary>
  ''' フォームキーダウン時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>アクセスキー対応</remarks>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        With Me.btnPostPca
          .Focus()
          .PerformClick()
        End With
      Case Keys.F5
        With Me.btnRefresh
          .Focus()
          .PerformClick()
        End With
      Case Keys.F12
        With Me.btnEnd
          .Focus()
          .PerformClick()
        End With
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
    With Me.CmbDateShiirebi1
      .InitCmb()
      .SelectedIndex = 0
    End With
    Call CreateSiireTbl()
    Controlz(DG2V1.Name).ShowList()
    Controlz(DG2V2.Name).ShowList()
  End Sub

#End Region

#Region "コンボボックス関連"

  ''' <summary>
  ''' 仕入日コンボボックス変更時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Shiirebi_Changed(sender As Object, e As EventArgs) Handles CmbDateShiirebi1.SelectedIndexChanged _
                                                                        , CmbDateShiirebi1.Validated
    Static sLastDate As String = String.Empty

    If Me.CmbDateShiirebi1.SelectedValue IsNot Nothing Then
      If sLastDate <> Me.CmbDateShiirebi1.SelectedValue.ToString() Then
        sLastDate = Me.CmbDateShiirebi1.SelectedValue.ToString()
        Try
          Controlz(DG2V1.Name).ClearSelectedList()
          Controlz(DG2V2.Name).ClearSelectedList()
          Call CreateSiireTbl()
        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        End Try

      End If
    End If
  End Sub
#End Region

#Region "グリッド関連"
  ''' <summary>
  ''' 仕入先一覧ダブルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DgvCellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
    Dim tmpCurrentData As Dictionary(Of String, String) = Me.Controlz(Me.DG2V1.Name).SelectedRow
    Dim tmpEqList As New Dictionary(Of String, String)

    '操作対象条件設定
    tmpEqList.Add("SIIREBI", tmpCurrentData("SIIREBI"))     ' 仕入先一覧でダブルクリックされた行の
    tmpEqList.Add("SIIRECODE", tmpCurrentData("SIIRECODE")) ' 仕入日と仕入先が一致
    tmpEqList.Add("POST_STAT", "未完了")                    ' 送信ステータスが"未完了" 

    If tmpCurrentData("SelecterCol") <> "" Then
      ' 一覧が選択された
      ' 仕入先が一致し、未完了のデータ全てにチェックを付ける
      Controlz(DG2V2.Name).SetRowSelectMark(tmpEqList)
    Else
      ' 一覧の選択が解除された
      ' 仕入先が一致し、未完了のデータ全てのチェックを外す
      Controlz(DG2V2.Name).UnSetRowSelectMark(tmpEqList)
    End If

    ' 送信予定件数表示
    Call ShowPlanedCount()

  End Sub


  ''' <summary>
  ''' 仕入先一覧再表示時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate"></param>
  ''' <param name="DataCount"></param>
  Private Sub ReLoadDataSupplierList(sender As DataGridView, LastUpdate As String, DataCount As Long)
    Me.lblSupplierListStat.Text = LastUpdate & "   現在 " & DataCount & "件"

  End Sub

  ''' <summary>
  ''' 入荷明細一覧再表示時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate"></param>
  ''' <param name="DataCount"></param>
  Private Sub ReLoadDataItemDetail(sender As DataGridView, LastUpdate As String, DataCount As Long)
    Me.lblItemDetailListStat.Text = LastUpdate & "   現在 " & DataCount & "件"

    Try
      Me.lblUnSendCount.Text = "未送信明細は、" & GetUnSendCount() & "件です。"
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

    ' 送信予定件数表示
    Call ShowPlanedCount()
  End Sub

#End Region

#Region "ボタン関連"

  ''' <summary>
  ''' 終了ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click

    With Controlz(DG2V1.Name)
      .ClearSelectedList()
      .ResetPosition()
    End With

    With Controlz(DG2V2.Name)
      .ClearSelectedList()
      .ResetPosition()
    End With

    Me.Hide()

    Me.ActiveControl = Me.CmbDateShiirebi1
    Me.CmbDateShiirebi1.SelectedIndex = 0
    Me.CmbMstShiresaki1.SelectedIndex = -1

  End Sub

  ''' <summary>
  ''' 仕入明細データ作成ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnPostPca_Click_1(sender As Object, e As EventArgs) Handles btnPostPca.Click
    Dim tmpDb As New clsSqlServer
    Dim tmpSlipNumber As Long = 0

    If Controlz(DG2V2.Name).SelectCount <= 0 Then
      Call ComMessageBox("送信対象が選択されていません！", PRG_TITLE, typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
    Else
      If typMsgBoxResult.RESULT_YES = ComMessageBox("入荷情報データを出力しますか" _
                                                  , PRG_TITLE _
                                                  , typMsgBox.MSG_NORMAL _
                                                  , typMsgBoxButton.BUTTON_YESNO) Then
        Try
          ' 伝票番号取得
          tmpSlipNumber = AssignNumber(tmpDb)

          ' 送信処理
          tmpDb.TrnStart()
          Call DataPost(tmpDb, tmpSlipNumber)
          tmpDb.TrnCommit()

          Call ComMessageBox("伝票の受渡が成功しました！", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)

          ' 一覧再表示
          With Controlz(DG2V1.Name)
            .ClearSelectedList()
            .AutoSearch = True
          End With

          With Controlz(DG2V2.Name)
            .ClearSelectedList()
            .AutoSearch = True
          End With

        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
          tmpDb.TrnRollBack()
        Finally
          tmpDb.Dispose()
        End Try
      End If

    End If

  End Sub

  ''' <summary>
  ''' 最新表示ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>最新日付データを再表示</remarks>
  Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

    Try
      With Me.CmbDateShiirebi1
        .InitCmb()
        .SelectedIndex = 0
      End With

      Controlz(DG2V1.Name).ClearSelectedList()
      Controlz(DG2V2.Name).ClearSelectedList()
      Call CreateSiireTbl()
      Controlz(DG2V1.Name).ShowList()
      Controlz(DG2V2.Name).ShowList()
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try
  End Sub


#End Region

#End Region

End Class
