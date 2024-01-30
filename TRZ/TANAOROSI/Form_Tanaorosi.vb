Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm01
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridSearchControl


Public Class Form_Tanaorosi
  Implements IDgvForm01
  '
  ' VisualStudioのツール>コード スニペット マネージャーの​インポートボタンで登録します
  ' 

#Region "定数定義"

  Private Const PRG_TITLE As String = "棚卸処理"
  Private Const PRG_ID As String = "tanaorosi"

#End Region

#Region "メンバ"

#Region "プライベート"
  ''' <summary>
  ''' 現在選択中データ
  ''' </summary>
  Private _SelectedRow As New Dictionary(Of String, String)

  ''' <summary>
  ''' 現在抽出中データ
  ''' </summary>
  ''' <remarks>
  '''  一覧表示中データとは異なる
  '''  選択中データと同一の入庫日・仕入先コード・個体識別番号・加工日の一覧
  '''  [次表示]押下時はこのリストを順次表示していく
  ''' </remarks>
  Private _SelectedList As New List(Of Dictionary(Of String, String))

  ''' <summary>
  ''' 現在選択中データの現在抽出データ位置
  ''' </summary>
  Private _SelectedListIdx As Integer = Integer.MinValue
#End Region

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_Tanaorosi)
  End Sub
#End Region

#Region "データグリッドビュー操作関連共通"

  '１つ目のDataGridViewオブジェクト格納先
  Private DG1V1 As DataGridView

  ''' <summary>
  ''' 初期化処理
  ''' </summary>
  ''' <remarks>
  ''' コントロールの初期化（Form_Loadで実行して下さい）
  ''' </remarks>
  Private Sub InitForm01() Implements IDgvForm01.InitForm

    '１つ目のDataGridViewオブジェクトの設定
    DG1V1 = Me.DataGridView1

    ' グリッド初期化
    Call InitGrid(DG1V1, CreateGrid1Src1(), CreateGrid1Layout1())

    ' グリッド動作設定
    With DG1V1

      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 2

        ' 検索コントロール設定
        .AddSearchControl(Me.txtTanaorosibi, "OROSIBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.txtNyukobi, "KEIRYOBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtLblMstShiresaki1.CodeCtrl, "TANAOROSI.SRCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxKotaiNo1, "KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.txtKakoubi, "KAKOUBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtLblMstItem1.CodeCtrl, "TANAOROSI.BICODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.txtUntreatedFlg, "OROSIKUBUN", typExtraction.EX_EQ, typColumnKind.CK_Number)
        '
        ' 編集可能列設定
        .EditColumnList = CreateGrid1EditCol1()
      End With
    End With
  End Sub

  ''' <summary>
  '''  棚卸一覧抽出SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid1Src1() As String Implements IDgvForm01.CreateGridSrc
    Dim sql As String = String.Empty

    sql &= " SELECT TANAOROSI.*  "
    sql &= "      , BINAME "
    sql &= "      , LSRNAME "
    sql &= "      , GBNAME "
    sql &= "      , SBNAME "
    sql &= "      , TANTOMEI  "
    sql &= "      , KKNAME "
    sql &= "      , KZNAME "
    sql &= "      , GNNAME "
    sql &= "      , LTKNAME  "
    sql &= "      , CONCAT(FORMAT(TANAOROSI.TKCODE,'0000') , ':' , LTKNAME) AS DLTKNAME  "
    sql &= "      , CONCAT(FORMAT(TANAOROSI.KAKUC,'0000') , ':' , KZNAME) AS DKZNAME  "
    sql &= "      , CONCAT(FORMAT(TANAOROSI.KIKAKUC,'0000') , ':' , KKNAME) AS DKKNAME  "
    sql &= "      , CONCAT(FORMAT(TANAOROSI.BICODE,'0000') , ':' , BINAME) AS DBINAME  "
    sql &= "      , CONCAT(FORMAT(TANAOROSI.GENSANCHIC,'0000') , ':' , GNNAME) AS DGNNAME  "
    sql &= "      , CONCAT(FORMAT(TANAOROSI.SYUBETUC,'0000') , ':' , SBNAME) AS DSBNAME  "
    sql &= "      , CONCAT(FORMAT(TANAOROSI.SRCODE,'0000') , ':' , LSRNAME) AS DLSRNAME  "
    sql &= "      , (ROUND((JYURYO / 100), 0, 1) / 10) AS JYURYOK "
    sql &= "      , IIF(SAYUKUBUN = 1,'左',IIF(SAYUKUBUN = 2,'右',' ')) AS LR2 "
    sql &= " FROM ((((((((TANAOROSI  LEFT JOIN BUIM ON TANAOROSI.BICODE = BUIM.BICODE) "
    sql &= "                         LEFT JOIN GBFLG_TBL ON TANAOROSI.GBFLG = GBFLG_TBL.GBCODE) "
    sql &= "                         LEFT JOIN SHUB ON TANAOROSI.SYUBETUC = SHUB.SBCODE) "
    sql &= "                         LEFT JOIN CUTSR ON TANAOROSI.SRCODE = CUTSR.SRCODE) "
    sql &= "                         LEFT JOIN KIKA ON TANAOROSI.KIKAKUC = KIKA.KICODE) "
    sql &= "                         LEFT JOIN KAKU ON TANAOROSI.KAKUC = KAKU.KKCODE) "
    sql &= "                         LEFT JOIN GENSN ON TANAOROSI.GENSANCHIC = GENSN.GNCODE) "
    sql &= "                         LEFT JOIN TOKUISAKI ON TANAOROSI.TKCODE = TOKUISAKI.TKCODE) "
    sql &= "                         LEFT JOIN TANTO_TBL ON TANAOROSI.TANTO = TANTO_TBL.TANTOC "
    sql &= " WHERE  KYOKUFLG = 0  "
    sql &= " ORDER BY KEIRYOBI "
    sql &= "        , TANAOROSI.GBFLG "
    sql &= "        , TANAOROSI.SYUBETUC "
    sql &= "        , TANAOROSI.BICODE "
    sql &= "        , TANAOROSI.SRCODE "
    sql &= "        , TOOSINO "
    sql &= "        , KOTAINO "
    sql &= "        , TANAOROSI.KDATE "

    Return sql
  End Function


  ''' <summary>
  ''' 一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid1Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm01.CreateGridlayout
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("入庫日", "KEIRYOBI", argFormat:="yy/MM/dd", argColumnWidth:=95, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("仕入先", "DLSRNAME", argColumnWidth:=250))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("加工日", "KAKOUBI", argFormat:="yy/MM/dd", argColumnWidth:=95, argTextAlignment:=typAlignment.MiddleCenter))
      .Add(New clsDGVColumnSetting("等級", "DKZNAME", argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("部位", "DBINAME", argColumnWidth:=220))
      .Add(New clsDGVColumnSetting("入庫数", "HONSU", argColumnWidth:=80, argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argFormat:="##0.00", argColumnWidth:=80, argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("原単価", "GENKA", argFormat:="#,##0", argColumnWidth:=90, argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("個体識別番号", "KOTAINO", argFormat:="0000000000", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("カートンNo.", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("担当者コード", "TANTO", argTextAlignment:=typAlignment.MiddleRight))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argFormat:="yyyy/MM/dd HH:mm:ss", argColumnWidth:=220, argTextAlignment:=typAlignment.MiddleCenter))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  Public Function CreateGrid1EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm01.CreateGridEditCol
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
    End With

    Return ret
  End Function

#End Region

#Region "メソッド"

#Region "プライベート"

#Region "状態判断"

  ''' <summary>
  ''' データ選択中か？
  ''' </summary>
  ''' <returns></returns>
  Private Function IsSelecdted() As Boolean
    Return (_SelectedRow.Keys.Count > 0)
  End Function

  ''' <summary>
  ''' 未処理表示中か？
  ''' </summary>
  ''' <returns>
  '''   True  - 未処理表示中
  '''   False - 全件表示中
  ''' </returns>
  Private Function IsShowUntreated() As Boolean
    Return (Me.txtUntreatedFlg.Text = "0")
  End Function

  ''' <summary>
  ''' 現在表示中の一覧から未処理データの件数を取得する
  ''' </summary>
  ''' <returns>未処理データ件数</returns>
  Private Function GetUntreatedCount() As Integer
    Dim tmpUntreatedCount As Integer = 0

    For Each tmpRowData As Dictionary(Of String, String) In Controlz(DG1V1.Name).GetAllData
      If tmpRowData("OROSIKUBUN") = "0" Then
        tmpUntreatedCount += 1
      End If
    Next

    Return tmpUntreatedCount
  End Function

  ''' <summary>
  ''' 在庫表示ステータス設定
  ''' </summary>
  ''' <param name="bEnabel">
  '''  True  - 在庫表示ステータス設定 (default)
  '''  False - 未処理表示ステータス設定
  ''' </param>
  Private Sub SetShowStockList(Optional bEnabel As Boolean = True)
    Dim tmpSetVal As String

    If bEnabel Then
      tmpSetVal = String.Empty
    Else
      tmpSetVal = "0"
    End If

    Me.txtUntreatedFlg.Text = tmpSetVal
  End Sub
#End Region

#Region "画面表示"
  ''' <summary>
  ''' 選択されたデータを画面に表示する
  ''' </summary>
  Private Sub Dic2Dsp()

    Me.txtKakoubi.Text = Date.Parse(_SelectedRow("KAKOUBI")).ToString("yyyy/MM/dd")
    Me.TxtLblMstItem1.CodeTxt = _SelectedRow("BICODE")
    Me.TxtLblMstShiresaki1.CodeTxt = _SelectedRow("SRCODE")
    Me.txtCarton.Text = _SelectedRow("TOOSINO")
    Me.txtNyukobi.Text = Date.Parse(_SelectedRow("KEIRYOBI")).ToString("yyyy/MM/dd")
    Me.txtStockCount.Text = _SelectedRow("HONSU")
    Me.txtCostKg.Text = _SelectedRow("GENKA")
    Me.TxtWeitghtKg1.Text = ComG2Kg(_SelectedRow("JYURYO"))
    Me.TxKotaiNo1.Text = _SelectedRow("KOTAINO")

  End Sub
#End Region

#Region "抽出リスト(次表示で使用のリスト)操作関連"
  ''' <summary>
  ''' 現在表示中データと同じ括りのデータを取得
  ''' </summary>
  Private Sub GetSelectedList()
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    Call ClearSelectedList()

    Try
      tmpDb.GetResult(tmpDt, SqlSelSelectedList())
      If tmpDt.Rows.Count > 0 Then
        _SelectedList = ComDt2Dic(tmpDt)
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("抽出リストの取得に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

  End Sub

  ''' <summary>
  ''' 現在選択中データの抽出リスト内位置取得
  ''' </summary>
  Private Sub GetSelectedListIdx()
    Dim tmpRow As New Dictionary(Of String, String)
    Dim bFined As Boolean = True

    For idx As Integer = 0 To _SelectedList.Count - 1
      bFined = True

      For Each tmpKey As String In _SelectedList(idx).Keys
        Console.Write(tmpKey)
        If _SelectedRow(tmpKey) <> _SelectedList(idx)(tmpKey) Then
          bFined = False
          Exit For
        End If
      Next

      If bFined Then
        _SelectedListIdx = idx
        Exit For
      End If
    Next

  End Sub


  ''' <summary>
  ''' "抽出リストクリア
  ''' </summary>
  Private Sub ClearSelectedList()
    _SelectedList.Clear()
    _SelectedListIdx = Integer.MinValue
  End Sub

#End Region

#Region "データ更新関連"

  ''' <summary>
  ''' 棚卸データ更新処理
  ''' </summary>
  ''' <remarks>
  ''' 選択中の棚卸データ(_SelectedRow)を棚卸済に更新する
  ''' </remarks>
  Private Sub UpDateTanaorosi()
    Dim tmpDb As New clsSqlServer
    Dim tmpAffected As Integer

    Try

      tmpDb.TrnStart()

      tmpAffected = tmpDb.Execute(SqlUpdTanaorosi())

      If tmpAffected > 1 Then
        Throw New Exception("棚卸情報が間違っています。データが重複しています。")
      ElseIf tmpAffected = 0 Then
        Throw New Exception("棚卸情報が間違っています。データが存在しません。")
      End If

      tmpDb.TrnCommit()
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()



      Throw New Exception("棚卸データ更新に失敗しました。")
    Finally
      tmpDb.Dispose()
    End Try
  End Sub

  ''' <summary>
  ''' 棚卸データ削除処理
  ''' </summary>
  ''' <param name="prmExpectation">削除予定件数</param>
  ''' <remarks>
  ''' 削除フラグの更新による削除処理です。
  ''' 実レコードは削除されません。
  ''' </remarks>
  Private Sub DeleteTanaorosi(prmExpectation As Integer)
    Dim tmpDb As New clsSqlServer
    Dim tmpAffected As Integer

    Try
      tmpDb.TrnStart()

      tmpAffected = tmpDb.Execute(SqlDelTanaorosi())

      If tmpAffected <> prmExpectation Then
        Throw New Exception("棚卸データ削除失敗。他のユーザーによって修正された可能性があります。")
      End If

      tmpDb.TrnCommit()
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
      Throw New Exception("棚卸データ削除に失敗しました。")
    End Try

  End Sub

#End Region

#Region "印刷処理関連"

  ''' <summary>
  ''' 棚卸一覧表の印刷を行う
  ''' </summary>
  Private Sub PrintReport()
    Dim tmpDb As New clsReport

    Try
      tmpDb = New clsReport(clsGlobalData.REPORT_FILENAME)

      ' ワークテーブル削除
      tmpDb.Execute(SqlDelWkTbl())

      ' ワークテーブルへデータ挿入
      For Each tmpRowData In Controlz(DG1V1.Name).GetAllData
        tmpDb.Execute(SqlInsWkTbl(tmpRowData))
      Next

      tmpDb.DbDisconnect()

      ' ACCESSの棚卸一覧表を表示
      ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_TANAOROSI")

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("棚卸一覧表の印刷に失敗しました。")
    Finally
      tmpDb.Dispose()
    End Try

  End Sub

#End Region

#Region "SQL関連"

  ''' <summary>
  ''' 印刷用ワークテーブル全件削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelWkTbl() As String
    Dim sql As String = String.Empty

    sql &= " DELETE FROM WK_TANAOROSI "

    Return sql
  End Function




  ''' <summary>
  ''' 印刷用ワークテーブル作成SQL文の作成
  ''' </summary>
  ''' <param name="prmRowData">挿入するデータ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsWkTbl(prmRowData As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty
    Dim tmpKeyVal As New Dictionary(Of String, String)
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    With tmpKeyVal
      .Add("OROSIBI", "'" & prmRowData("OROSIBI") & "'")
      .Add("GBFLG", prmRowData("GBFLG"))
      .Add("GBNAME", "'" & prmRowData("GBNAME") & "'")
      .Add("SYUBETUC", prmRowData("SYUBETUC"))
      .Add("SBNAME", "'" & prmRowData("SBNAME") & "'")
      .Add("BICODE", prmRowData("BICODE"))
      .Add("BINAME", "'" & prmRowData("BINAME") & "'")
      .Add("SRCODE", prmRowData("SRCODE"))
      .Add("LSRNAME", "'" & prmRowData("LSRNAME") & "'")
      .Add("KOTAINO", ComBlank2NullText(prmRowData("KOTAINO")))
      .Add("TOOSINO", prmRowData("TOOSINO"))
      .Add("KEIRYOBI", "'" & prmRowData("KEIRYOBI") & "'")
      .Add("KAKOUBI", "'" & prmRowData("KAKOUBI") & "'")
      .Add("HONSU", prmRowData("HONSU"))
      .Add("JYURYO", (Decimal.Parse(prmRowData("JYURYO")) / 1000).ToString())
      .Add("RHONSU", prmRowData("RHONSU"))
      .Add("RJYURYO", (Decimal.Parse(prmRowData("RJYURYO")) / 1000).ToString())
    End With

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst &= tmpKey & ","
      tmpVal &= tmpKeyVal(tmpKey) & ","
    Next

    sql &= " INSERT INTO WK_TANAOROSI( " & tmpDst.Substring(0, tmpDst.Length - 1) & ")"
    sql &= " VALUES(" & tmpVal.Substring(0, tmpVal.Length - 1) & ")"


    Return sql
  End Function

  ''' <summary>
  ''' 現在表示中のデータに一致する棚卸データ一覧取得SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 現在表示中の、棚卸日・入庫日（計量日）・仕入先コード・個体識別番号・加工日が一致するレコードを抽出
  ''' </remarks>
  Private Function SqlSelSelectedList() As String
    Dim sql As String = String.Empty

    sql = ComAddSqlSearchCondition(CreateGrid1Src1(), "     KEIRYOBI = '" & txtNyukobi.Text & "'" _
                                                    & " AND TANAOROSI.SRCODE = " & TxtLblMstShiresaki1.CodeTxt _
                                                    & " AND KOTAINO = " & TxKotaiNo1.Text _
                                                    & " AND KAKOUBI = '" & txtKakoubi.Text & "'" _
                                                    & " AND OROSIBI = '" & txtTanaorosibi.Text & "'")

    Return sql
  End Function

  ''' <summary>
  ''' 棚卸情報登録SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdTanaorosi() As String
    Dim sql As String = String.Empty

    sql &= " UPDATE TANAOROSI "
    sql &= " SET KDATE = '" & ComGetProcTime() & "'"
    sql &= "   , KIKAINO = 999 "
    sql &= "   , KFLG = 0 "
    sql &= "   , TANTO =  " & ComBlank2ZeroText(Me.TxtLblMstStaff1.CodeTxt)

    If ComBlank2Zero(Me.TxtWeitghtKg1.Text) = 0 Then
      ' 重量0入力時は削除
      sql &= "   , OROSIKUBUN = 3 "
      sql &= "   , KYOKUFLG = 1 "
    Else
      sql &= "   , OROSIKUBUN = 1 "
      sql &= "   , HONSU =  " & ComBlank2ZeroText(Me.txtStockCount.Text)
      sql &= "   , JYURYO = " & ComKg2G(Me.TxtWeitghtKg1.Text)
      sql &= "   , GENKA = " & ComBlank2ZeroText(Me.txtCostKg.Text)
    End If

    ' TANAOROSIテーブルにユニークキーが無いので過去データからユニークになる条件を調査し他結果です
    sql &= " WHERE 1 = 1"
    sql &= "   AND OROSIBI = '" & Date.Parse(_SelectedRow("OROSIBI")).ToString("yyyy/MM/dd") & "'"
    sql &= "   AND KEIRYOBI = '" & Date.Parse(_SelectedRow("KEIRYOBI")).ToString("yyyy/MM/dd") & "'"
    sql &= "   AND GBFLG = " & _SelectedRow("GBFLG")
    sql &= "   AND SRCODE = " & _SelectedRow("SRCODE")
    sql &= "   AND EBCODE = " & _SelectedRow("EBCODE")
    sql &= "   AND BICODE = " & _SelectedRow("BICODE")
    sql &= "   AND SAYUKUBUN = " & _SelectedRow("SAYUKUBUN")
    sql &= "   AND TOOSINO = " & _SelectedRow("TOOSINO")
    sql &= "   AND KDATE = '" & _SelectedRow("KDATE") & "'"

    Return sql
  End Function

  ''' <summary>
  ''' 棚卸情報削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' フラグ更新による削除処理
  ''' </remarks>
  Private Function SqlDelTanaorosi() As String
    Dim sql As String = String.Empty

    sql &= " UPDATE TANAOROSI "
    sql &= " SET KDATE = '" & ComGetProcTime() & "'"
    sql &= "   , KFLG = 0 "
    sql &= "   , KYOKUFLG = 1 "
    sql &= "   , OROSIKUBUN = 3 "
    sql &= "   , TANTO = " & ComBlank2ZeroText(Me.TxtLblMstStaff1.CodeTxt)

    If IsSelecdted() Then
      ' データ選択時は対象データのみ削除
      sql &= "   , KIKAINO = 999 "
      sql &= " WHERE 1 = 1"
      sql &= "   AND OROSIBI = '" & Date.Parse(_SelectedRow("OROSIBI")).ToString("yyyy/MM/dd") & "'"
      sql &= "   AND KEIRYOBI = '" & Date.Parse(_SelectedRow("KEIRYOBI")).ToString("yyyy/MM/dd") & "'"
      sql &= "   AND GBFLG = " & _SelectedRow("GBFLG")
      sql &= "   AND SRCODE = " & _SelectedRow("SRCODE")
      sql &= "   AND EBCODE = " & _SelectedRow("EBCODE")
      sql &= "   AND BICODE = " & _SelectedRow("BICODE")
      sql &= "   AND SAYUKUBUN = " & _SelectedRow("SAYUKUBUN")
      sql &= "   AND TOOSINO = " & _SelectedRow("TOOSINO")
      sql &= "   AND KDATE = '" & _SelectedRow("KDATE") & "'"
    Else

      ' データ未選択時は入力項目に一致するレコードを全て削除
      sql &= " WHERE KYOKUFLG = 0 "
      sql &= "   AND OROSIKUBUN =0 "
      sql &= "   AND OROSIBI ='" & Date.Parse(Me.txtTanaorosibi.Text).ToString("yyyy/MM/dd") & "'"

      If Me.txtNyukobi.Text <> "" Then
        sql &= "   AND KEIRYOBI ='" & Date.Parse(Me.txtNyukobi.Text).ToString("yyyy/MM/dd") & "'"
      End If

      If Me.TxtLblMstShiresaki1.CodeCtrl.Text <> "" Then
        sql &= "   AND SRCODE =" & Me.TxtLblMstShiresaki1.CodeCtrl.Text
      End If

      If Me.TxKotaiNo1.Text <> "" Then
        sql &= "   AND KOTAINO =" & Me.TxKotaiNo1.Text
      End If

      If Me.txtKakoubi.Text <> "" Then
        sql &= "   AND KAKOUBI ='" & Date.Parse(Me.txtKakoubi.Text).ToString("yyyy/MM/dd") & "'"
      End If

      If TxtLblMstItem1.CodeCtrl.Text <> "" Then
        sql &= "   AND BICODE =" & Me.TxtLblMstItem1.CodeCtrl.Text
      End If

    End If


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
  Private Sub Form_Tanaorosi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Dim tmpSubForm As New SForm_Tanaorosi
    Dim tmpSfResult As SFBase.typSfResult

    ' 画面タイトル設定
    Me.Text = PRG_TITLE

    tmpSfResult = tmpSubForm.ShowSubForm(New Dictionary(Of String, String)(), Me)

    If tmpSfResult <> SFBase.typSfResult.SF_OK Then
      Application.Exit()
    Else
      ' データグリッド初期化
      Call InitForm01()

      ' グリッドイベント設定
      With Controlz(Me.DG1V1.Name)
        .lcCallBackReLoadData = AddressOf DgvReload   ' 再表示時
        .lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick ' ダブルクリック時処理
      End With

      ' 検索処理実行
      With Controlz(DG1V1.Name)
        .AutoSearch = True
      End With

      ' 標準以外のメッセージ設定
      Me.txtNyukobi.SetMsgLabelText("入庫日付を入力して下さい。")
      Me.txtKakoubi.SetMsgLabelText("加工日を入力して下さい。")
      Me.txtCarton.SetMsgLabelText("カートンNoを入力して下さい。")
      Me.txtStockCount.SetMsgLabelText("在庫数を入力して下さい。")
      Me.TxtWeitghtKg1.SetMsgLabelText("重量(Kg)を入力して下さい。重量がゼロのときは削除されます。変更時は変更後の値を入力して下さい。")
      Me.txtCostKg.SetMsgLabelText("kg仕入単価を入力して下さい。")


      ' メッセージラベル設定
      Call SetMsgLbl(Me.lblInformation)

      ' 入力項目エンター時イベント設定
      ' 自コントロールより下に配置されているコントロールの入力内容をクリアする
      AddHandler Me.TxtLblMstStaff1.CodeCtrl.Enter, AddressOf TextBox_Enter
      AddHandler Me.txtNyukobi.Enter, AddressOf TextBox_Enter
      AddHandler Me.TxtLblMstShiresaki1.CodeCtrl.Enter, AddressOf TextBox_Enter
      AddHandler Me.TxKotaiNo1.Enter, AddressOf TextBox_Enter
      AddHandler Me.txtKakoubi.Enter, AddressOf TextBox_Enter
      AddHandler Me.TxtLblMstShiresaki1.CodeCtrl.Enter, AddressOf TextBox_Enter
      AddHandler Me.TxtLblMstItem1.CodeCtrl.Enter, AddressOf TextBox_Enter
      AddHandler Me.txtCarton.Enter, AddressOf TextBox_Enter

    End If

  End Sub


  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Dim tmpTargetBtn As Button = Nothing

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 登録
        tmpTargetBtn = Me.btnEntry
      Case Keys.F2
        ' 次表示
        tmpTargetBtn = Me.btnMoveNext
      Case Keys.F3
        ' 在庫表示
        tmpTargetBtn = Me.btnShowStockList
      Case Keys.F5
        ' 未処理表示
        tmpTargetBtn = Me.btnShowUntreated
      Case Keys.F7
        ' 削除
        tmpTargetBtn = Me.btnDelete
      Case Keys.F9
        ' 印刷
        tmpTargetBtn = Me.btnPrint
      Case Keys.F12
        ' 終了
        tmpTargetBtn = Me.btnClose
    End Select

    If tmpTargetBtn IsNot Nothing Then
      With tmpTargetBtn
        .Focus()
        .PerformClick()
      End With
    End If

  End Sub
#End Region

#Region "ボタン関連"

  ''' <summary>
  ''' 在庫表示ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnShowStockList_Click(sender As Object, e As EventArgs) Handles btnShowStockList.Click
    With Controlz(Me.DG1V1.Name)
      .AutoSearch = False

      SetShowStockList()  ' 在庫表示設定

      .AutoSearch = True
    End With

  End Sub

  ''' <summary>
  ''' 未処理表示ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnShowUntreated_Click(sender As Object, e As EventArgs) Handles btnShowUntreated.Click
    With Controlz(Me.DG1V1.Name)
      .AutoSearch = False
      SetShowStockList(False) ' 未処理表示設定
      .AutoSearch = True
    End With

  End Sub

  ''' <summary>
  ''' 次表示ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnMoveNext_Click(sender As Object, e As EventArgs) Handles btnMoveNext.Click
    If _SelectedList.Count > 0 Then

      If _SelectedListIdx >= (_SelectedList.Count - 1) Then
        _SelectedListIdx = 0
      Else
        _SelectedListIdx += 1
      End If

      _SelectedRow = _SelectedList(_SelectedListIdx)

      Call Dic2Dsp()
      Me.ActiveControl = Me.TxtWeitghtKg1

      If _SelectedListIdx = 0 Then
        Me.lblInformation.Text = "入力条件に合う次のデータがありません。先頭に戻ります。"
      End If

    End If
  End Sub

  ''' <summary>
  ''' 登録ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnEntry_Click(sender As Object, e As EventArgs) Handles btnEntry.Click

    If IsSelecdted() = False Then
      Me.lblInformation.Text = "明細の選択が出来ていなので修正登録は出来ません。"
    Else
      If typMsgBoxResult.RESULT_OK = ComMessageBox("データを更新・登録しますか？" _
                                                 , PRG_TITLE _
                                                 , typMsgBox.MSG_NORMAL _
                                                 , typMsgBoxButton.BUTTON_OKCANCEL _
                                                 , typMsgBoxButton.BUTTON_OK) Then

        Try
          ' 更新
          Call UpDateTanaorosi()

          With Controlz(DG1V1.Name)
            .AutoSearch = False

            SetShowStockList()  ' 在庫表示設定

            .AutoSearch = True

            ' 再表示
            Call .ShowList()
          End With

          '抽出リストクリア
          Call ClearSelectedList()

          ' 選択データクリア
          _SelectedRow.Clear()

          ' 入力データクリア
          Me.TxtWeitghtKg1.Text = ""
          Me.txtCostKg.Text = ""
          Me.txtStockCount.Text = ""

        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        End Try

      End If
    End If
  End Sub

  ''' <summary>
  ''' 削除ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    Dim tmpAltMsg As String = String.Empty
    Dim tmpExpectation As Integer = Integer.MinValue

    'データは選択されているか？
    If IsSelecdted() Then
      '-------------------------
      '     データ選択中
      '-------------------------

      ' 確認メッセージ設定
      tmpAltMsg &= "在庫不明でデータを削除しますか？"

      ' 削除対象件数設定
      tmpExpectation = 1
    Else
      '-------------------------
      '     データ未選択
      '-------------------------

      ' データ未選択時は複数行が同時に削除されるのでキモチ強めのメッセージ表示
      tmpAltMsg &= "在庫不明でデータを削除しますか？"
      tmpAltMsg &= vbCrLf
      tmpAltMsg &= "表示されている明細が全て削除されます！！"
      tmpAltMsg &= vbCrLf
      tmpAltMsg &= "この処理の取り消しはできません。"

      ' 削除対象件数取得
      If IsShowUntreated() Then
        ' 未処理表示時は表示中データ全てが削除対象
        tmpExpectation = Controlz(DG1V1.Name).GetAllData().Count
      Else
        ' 在庫表示時は表示中データ中の未処理のみが削除対象
        tmpExpectation = GetUntreatedCount()
      End If

    End If

    If typMsgBoxResult.RESULT_OK = ComMessageBox(tmpAltMsg _
                                                 , PRG_TITLE _
                                                 , typMsgBox.MSG_NORMAL _
                                                 , typMsgBoxButton.BUTTON_OKCANCEL _
                                                 , typMsgBoxButton.BUTTON_OK) Then

      Try

        ' 削除処理実行
        Call DeleteTanaorosi(tmpExpectation)

        ' 再表示
        Call Controlz(DG1V1.Name).ShowList()

        '抽出リストクリア
        Call ClearSelectedList()

        ' 選択データクリア
        _SelectedRow.Clear()

        ' 入力データクリア
        Me.TxtWeitghtKg1.Text = ""
        Me.txtCostKg.Text = ""
        Me.txtStockCount.Text = ""

      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      End Try
    End If


  End Sub

  ''' <summary>
  ''' 印刷ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    Try
      Call PrintReport()
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try
  End Sub

  ''' <summary>
  ''' 終了ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    ' 終了
    Me.Close()
    Application.Exit()
  End Sub

#End Region

#Region "グリッド関連"

  ''' <summary>
  ''' 一覧再表示時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate"></param>
  ''' <param name="DataCount"></param>
  Private Sub DgvReload(sender As DataGridView, LastUpdate As String, DataCount As Long, DataJuryo As Decimal, DataKingaku As Decimal)
    Dim tmpCountTxt As String = String.Empty

    If IsShowUntreated() Then
      ' 未処理表示
      tmpCountTxt = "未処理棚卸明細"
    Else
      ' 全件表示
      tmpCountTxt = "棚卸明細"
    End If

    Me.lblListStat.Text = "棚卸日:" & Me.txtTanaorosibi.Text & "              " & tmpCountTxt & "  " & DataCount & "件"
  End Sub

  ''' <summary>
  ''' 棚卸一覧ダブルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' ダブルクリックされた行を編集枠に設定する
  ''' </remarks>
  Private Sub DgvCellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    Try
      ' 選択データ保持
      _SelectedRow = Controlz(DG1V1.Name).SelectedRow

      ' 選択データ画面表示
      With Controlz(DG1V1.Name)
        .AutoSearch = False
        SetShowStockList()  ' 在庫表示設定
        Call Dic2Dsp()
        .AutoSearch = True
      End With

      ' 表示中の一覧と同じ括りのデータを取得（[次表示]処理用）
      Call GetSelectedList()

      ' ダブルクリックされたデータの抽出リスト内位置取得
      Call GetSelectedListIdx()

      ' 選択行を先頭として抽出リストの並び替え
      If _SelectedListIdx <> 0 Then
        For idx As Integer = 0 To _SelectedListIdx - 1
          _SelectedList.Insert(_SelectedList.Count - 1, _SelectedList(0))
          _SelectedList.RemoveAt(0)
        Next
        _SelectedListIdx = 0
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub



#End Region

#Region "テキストボックス関連"
  ''' <summary>
  ''' 入力項目フォーカス時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TextBox_Enter(sender As Object, e As EventArgs)
    Dim tmpExCtrlList As New List(Of Control)({Me.txtUntreatedFlg, Me.txtTanaorosibi})


    Select Case Me.ActiveControl.Name
      Case Me.TxtLblMstStaff1.Name
        tmpExCtrlList.Add(TxtLblMstStaff1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstStaff1.NameCtrl)
      Case Me.txtNyukobi.Name
        tmpExCtrlList.Add(TxtLblMstStaff1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstStaff1.NameCtrl)
        tmpExCtrlList.Add(txtNyukobi)
      Case Me.TxtLblMstShiresaki1.Name
        tmpExCtrlList.Add(TxtLblMstStaff1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstStaff1.NameCtrl)
        tmpExCtrlList.Add(txtNyukobi)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.NameCtrl)
      Case Me.TxKotaiNo1.Name
        tmpExCtrlList.Add(TxtLblMstStaff1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstStaff1.NameCtrl)
        tmpExCtrlList.Add(txtNyukobi)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.NameCtrl)
        tmpExCtrlList.Add(TxKotaiNo1)
      Case Me.txtKakoubi.Name
        tmpExCtrlList.Add(TxtLblMstStaff1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstStaff1.NameCtrl)
        tmpExCtrlList.Add(txtNyukobi)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.NameCtrl)
        tmpExCtrlList.Add(TxKotaiNo1)
        tmpExCtrlList.Add(txtKakoubi)
      Case Me.TxtLblMstItem1.Name
        tmpExCtrlList.Add(TxtLblMstStaff1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstStaff1.NameCtrl)
        tmpExCtrlList.Add(txtNyukobi)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.NameCtrl)
        tmpExCtrlList.Add(TxKotaiNo1)
        tmpExCtrlList.Add(txtKakoubi)
        tmpExCtrlList.Add(TxtLblMstItem1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstItem1.NameCtrl)
      Case Me.txtCarton.Name
        tmpExCtrlList.Add(TxtLblMstStaff1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstStaff1.NameCtrl)
        tmpExCtrlList.Add(txtNyukobi)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstShiresaki1.NameCtrl)
        tmpExCtrlList.Add(TxKotaiNo1)
        tmpExCtrlList.Add(txtKakoubi)
        tmpExCtrlList.Add(TxtLblMstItem1.CodeCtrl)
        tmpExCtrlList.Add(TxtLblMstItem1.NameCtrl)
        tmpExCtrlList.Add(txtCarton)
    End Select

    With Controlz(DG1V1.Name)
      .AutoSearch = False
      Call AllClear(tmpExCtrlList)
      .AutoSearch = True
    End With

    Call ClearSelectedList()
    _SelectedRow.Clear()

  End Sub


#End Region

#End Region

End Class
