Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm01
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData

Public Class Form_Edainp
  Implements IDgvForm01

  '----------------------------------------------
  '                 枝情報入力画面
  '
  '
  '----------------------------------------------

#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "edainput"

  ''' <summary>
  ''' マスタMDBファイルパス
  ''' </summary>
  ''' <remarks>テスト時平行稼働用</remarks>
  Private Const MST_ACCESS_PATH As String = "D:\trasa\dat\MST.mdb"

  ''' <summary>
  ''' 一覧表示範囲
  ''' </summary>
  ''' <remarks>一覧抽出用SQL文に適用</remarks>
  Private Const DSP_LIMIT As String = "EDAB.KDATE > DateAdd(Month, -3, GETDATE())"
#End Region

#Region "パブリック"

  ''' <summary>
  ''' プログラムタイトル
  ''' </summary>
  ''' <remarks>
  ''' 印刷条件入力サブフォームからも参照する為
  ''' Publicにしてます
  ''' </remarks>
  Public Const PRG_TITLE As String = "枝情報入力"

#End Region

#End Region

#Region "メンバ"

#Region "プライベート"

  ''' <summary>
  ''' 一覧より選択されたデータを保持
  ''' </summary>
  Private _SelectedData As New Dictionary(Of String, String)

#End Region

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_Edainp, AddressOf ComRedisplay)
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

        ' 検索コントロール設定
        '.AddSearchControl([コントロール], "絞り込み項目名", [絞り込み方法], [絞り込み項目の型])

        ' 編集可能列設定
        .EditColumnList = CreateGrid1EditCol1()
      End With
    End With
  End Sub

  ''' <summary>
  ''' 枝情報一覧表示用SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid1Src1() As String Implements IDgvForm01.CreateGridSrc
    Dim sql As String = String.Empty
    Dim sqlWhere As String = String.Empty

    ' 枝情報抽出共通部分設定
    sql &= SqlEdaInfSrcBase()

    ' 一覧表示用抽出条件設定
    sql = ComAddSqlSearchCondition(sql, DSP_LIMIT)

    ' 並び順設定
    sql &= " ORDER BY SIIREBI DESC "
    sql &= "                , EBCODE DESC "
    sql &= "                , KIKAKUC "

    Return sql
  End Function


  ''' <summary>
  ''' 一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid1Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm01.CreateGridlayout
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("肢番", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("規格コード", "DKKNAME", argTextAlignment:=typAlignment.MiddleLeft, argFontSize:=11, argColumnWidth:=115))
      .Add(New clsDGVColumnSetting("種別コード", "DSBNAME", argTextAlignment:=typAlignment.MiddleLeft, argFontSize:=11, argColumnWidth:=115))
      .Add(New clsDGVColumnSetting("格付コード", "DKZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=140, argFontSize:=11))
      .Add(New clsDGVColumnSetting("仕入先コード", "DLSRNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=200, argFontSize:=11))
      .Add(New clsDGVColumnSetting("原産地コード", "DGNNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=120, argFontSize:=11))
      .Add(New clsDGVColumnSetting("ブランド", "DBRNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=100, argFontSize:=11))
      .Add(New clsDGVColumnSetting("個体識別番号", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=130, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("屠場コード", "DTJNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=180, argFontSize:=11))
      .Add(New clsDGVColumnSetting("上場コード", "EDC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("入庫日付", "NYUKOBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("仕入日付", "SIIREBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("1頭重量", "JYURYO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("左重量", "JYURYO1", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("右重量", "JYURYO2", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=90))
      '.Add(New clsDGVColumnSetting("1頭仕入単価", "SIIREGAKU", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=120))
      '.Add(New clsDGVColumnSetting("左仕入単価", "SIIREGAKU1", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=120))
      '.Add(New clsDGVColumnSetting("右仕入単価", "SIIREGAKU2", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("登録日付", "TDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
      .Add(New clsDGVColumnSetting("更新日付", "KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
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

#Region "DB更新関連"

#Region "SQL実行"

  ''' <summary>
  ''' 枝情報検索
  ''' </summary>
  ''' <remarks>
  '''  仕入日付・屠場コード・上場番号の一致する枝情報を表示
  ''' </remarks>
  Private Sub SerchEdaInfo()
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    If Me.TxtLblMstTojyo1.CodeTxt <> "" _
      AndAlso Me.TxtEdaban2.Text <> "" _
      AndAlso Me.TxtEdaban1.Text = "" Then

      Try
        ' 枝情報検索
        tmpDb.GetResult(tmpDt, SqlSelSearchEdaInfo)
        If tmpDt.Rows.Count <= 0 Then
          ' 該当データが存在しない場合は枝番を採番
          _SelectedData.Clear()                 ' 選択データクリア(新規入力扱い)
          Me.TxtEdaban1.Text = NumberingEdaban(Me.TxtShiireDate.Text).ToString
        Else
          _SelectedData.Clear()                 ' 選択データクリア
          _SelectedData = Dt2Dic(tmpDt)(0)      ' SQL実行結果を選択データにつめる
          Call Dic2Dsp()                        ' 選択データを画面表示
        End If

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("枝情報の検索に失敗しました。")
      End Try
    End If

  End Sub

  ''' <summary>
  ''' 枝情報新規登録
  ''' </summary>
  Private Sub CreateEdaInfo()
    Dim tmpDb As New clsSqlServer
    Dim tmpProcTime As String = ComGetProcTime()

    With tmpDb
      Try
        .TrnStart()

        ' EDAB作成
        Call .Execute(SqlInsEDAB(tmpProcTime))
        ' CUTJ作成
        Call .Execute(SqlInsCutJ(tmpProcTime))

        .TrnCommit()

        ' 画面再表示
        MyBase.AllClear(New List(Of Control)({Me.TxtShiireDate}))
        Controlz(DG1V1.Name).ShowList()
        Me.TxtLblMstTojyo1.CodeCtrl.Focus()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()

        Throw New Exception("枝情報の登録に失敗しました。")
      End Try

    End With
  End Sub

  ''' <summary>
  ''' 枝情報更新
  ''' </summary>
  Private Sub UpDateEdaInfo()
    Dim tmpDb As New clsSqlServer
    Dim tmpProcTime As String = ComGetProcTime()
    Dim tmpNSZFlg As String = String.Empty

    With tmpDb
      Try
        .TrnStart()

        ' EDAB更新
        If .Execute(SqlUpdEdab(tmpProcTime)) <> 1 Then
          Throw New Exception("枝番コード[" & _SelectedData("EBCODE") & "];" _
                             & "屠場コード[" & _SelectedData("TJCODE") & "];" _
                             & "上場コード[" & _SelectedData("EDC") & "]" _
                             & "の枝情報は他のユーザーによって修正されている可能性があります。")
        End If

        ' 更新対象のCUTJ確認
        If ExistsCutJ(tmpNSZFlg) = False Then
          ' 対象データ無し
          ' CUTJ作成
          Call .Execute(SqlInsCutJ(tmpProcTime))
        Else
          ' 対象データ有り
          If tmpNSZFlg = "0" _
            OrElse tmpNSZFlg = "6" Then
            ' 在庫状態

            ' CUTJ更新
            If .Execute(SqlUpdCutJ(tmpProcTime)) <> 1 Then
              Throw New Exception("枝入庫情報の更新に失敗しました。")
            End If

          End If
        End If

        .TrnCommit()

        ' 画面再表示
        MyBase.AllClear()
        Controlz(DG1V1.Name).ShowList()
        Me.TxtShiireDate.Focus()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()

        Throw New Exception("枝情報の更新に失敗しました。")
      End Try

    End With

  End Sub

  ''' <summary>
  ''' 枝情報削除
  ''' </summary>
  Private Sub DelEdaInfo()

    Dim tmpDb As New clsSqlServer
    Dim tmpNSZFlg As String = String.Empty

    '<---------- 2023/08/23 koumoto.T Del
    ''-------------------------------------
    ''       MDB接続（平行稼働時のみ）
    'Dim tmpMstAccess As New clsComDatabase
    'With tmpMstAccess
    '  .DataSource = MST_ACCESS_PATH
    '  .Provider = clsComDatabase.typProvider.Mdb
    'End With
    ''-------------------------------------
    ' 2023/08/23 koumoto.T Del ---------->

    With tmpDb
      Try
        .TrnStart()
        '<---------- 2023/08/23 koumoto.T Del
        'tmpMstAccess.TrnStart()
        ' 2023/08/23 koumoto.T Del ---------->

        ' EDAB削除
        If .Execute(SqlDelEDAB) <> 1 Then
          Throw New Exception("枝番コード[" & _SelectedData("EBCODE") & "];" _
                             & "屠場コード[" & _SelectedData("TJCODE") & "];" _
                             & "上場コード[" & _SelectedData("EDC") & "]" _
                             & "の枝情報は他のユーザーによって修正されている可能性があります。")
        End If

        '<---------- 2023/08/23 koumoto.T Del
        '並行稼働が不要のため処理を削除

        '' EDAB削除（ACCESSテーブル）平行稼働用ロジック
        'If tmpMstAccess.Execute(SqlDelEDAB(True)) <> 1 Then
        '  Throw New Exception("枝番コード[" & _SelectedData("EBCODE") & "];" _
        '                     & "屠場コード[" & _SelectedData("TJCODE") & "];" _
        '                     & "上場コード[" & _SelectedData("EDC") & "]" _
        '                     & "の枝情報は他のユーザーによって修正されている可能性があります。")
        'End If
        ' 2021/05/10 shiomitsu.T  Del ---------->


        ' CUTJ存在確認
        If ExistsCutJ(tmpNSZFlg) Then
          ' 削除
          If .Execute(SqlDelCutJ) <> 1 Then
            Throw New Exception("枝入庫データの削除に失敗しました。")
          End If
        End If

        .TrnCommit()
        ' 2023/08/23 koumoto.T Del ---------->
        'tmpMstAccess.TrnCommit()

        ' 画面再表示
        MyBase.AllClear()
        Controlz(DG1V1.Name).ShowList()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        ' 2023/08/23 koumoto.T Del ---------->
        'tmpMstAccess.TrnRollBack()

        Throw New Exception("枝情報の削除に失敗しました。")
      End Try

    End With

  End Sub

  ''' <summary>
  ''' 登録する枝情報と一致するレコードが存在するか？
  ''' </summary>
  ''' <returns>
  '''   True  - レコードが存在する
  '''   False - レコードが存在しない
  ''' </returns>
  ''' <remarks>
  ''' 過去3か月以内の更新を対象に以下の項目の一致を確認
  ''' ・肢番コード	EBCODE
  ''' ・上場コード	EDC
  ''' ・屠場コード	TJCODE
  ''' </remarks>
  Private Function ExistsEdab() As Boolean
    Dim ret As Boolean = False
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim sql As String = String.Empty
    Dim sqlWhere As String = String.Empty

    With tmpDb
      Try
        sql = SqlSelDuplicateEDAB()

        ' 更新時は（選択されたデータ以外）を抽出条件に追加
        If (_SelectedData.Keys.Count > 0) Then
          ' 一覧で選択されたデータを抽出条件に設定
          sqlWhere &= " ( "
          sqlWhere &= "  EBCODE <> " & _SelectedData("EBCODE")
          sqlWhere &= "   Or EDC <> " & _SelectedData("EDC")
          sqlWhere &= "   Or EDAB.TJCODE <> " & _SelectedData("TJCODE")
          sqlWhere &= "   Or KOTAINO <> " & _SelectedData("KOTAINO")
          sqlWhere &= "   Or EDAB.KDATE <> '" & _SelectedData("KDATE") & "'"
          sqlWhere &= " ) "

          sql = ComAddSqlSearchCondition(sql, sqlWhere)
        End If

        .GetResult(tmpDt, sql)
        ret = (tmpDt.Rows.Count > 0)
      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("枝情報の確認に失敗しました。")
      End Try

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 枝情報更新時にEDABに対応するCUTJが存在するか確認
  ''' </summary>
  ''' <returns>
  '''  True  - 存在する
  '''  False - 存在しない
  ''' </returns>
  Private Function ExistsCutJ(ByRef prmNSZFlg As String) As Boolean
    Dim ret As Boolean = False
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    With tmpDb

      Try
        .GetResult(tmpDt, SqlSelUpdateTargetCutJ())

        ' 対象のCUJは存在するか？
        If tmpDt.Rows.Count > 0 Then

          ret = True                                      ' Trueを返す
          prmNSZFlg = tmpDt.Rows(0)("NSZFLG").ToString()  ' 存在するならNSZFLGを保持する
        End If

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("枝入庫データの確認に失敗しました。")
      Finally
        tmpDt.Clear()
        tmpDt.Dispose()
        tmpDb.DbDisconnect()
      End Try

    End With

    Return ret
  End Function
#End Region

#Region "SQL文作成"

  ''' <summary>
  ''' 画面の入力項目より枝情報を検索するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelSearchEdaInfo() As String
    Dim sql As String = String.Empty
    Dim sqlWhere As String = String.Empty

    ' 入力項目より検索条件作成
    sqlWhere &= " EDAB.SIIREBI = '" & Me.TxtShiireDate.Text & "'"   ' 仕入日付
    sqlWhere &= " AND EDAB.TJCODE = " & Me.TxtLblMstTojyo1.CodeTxt  ' 屠場コード
    sqlWhere &= " AND EDAB.EDC = " & Me.TxtEdaban2.Text             ' 上場コード

    sql = ComAddSqlSearchCondition(SqlEdaInfSrcBase, DSP_LIMIT)     ' 範囲絞り込み
    sql = ComAddSqlSearchCondition(sql, sqlWhere)                   ' 入力項目を検索条件に追加

    Return sql
  End Function

  ''' <summary>
  ''' 期間内に最終に登録された枝番を取得するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelLastEdaban() As String
    Dim sql As String = String.Empty

    sql &= " SELECT TOP(1) src.EBCODE AS LAST_EDABAN "
    sql &= " FROM (" & ComAddSqlSearchCondition(SqlEdaInfSrcBase, DSP_LIMIT) & ") as src"
    sql &= " ORDER BY src.TDATE DESC "

    Return sql
  End Function

  ''' <summary>
  ''' 期間内の最大の枝番を取得するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelMaxEdaban(targetDate As String) As String
    Dim sql As String = String.Empty
    Dim targetYear As String

    ' 年度開始月
    Const START_MONTH = "5"

    targetYear = Year(DateAdd("m", (CInt(START_MONTH) - 1) * -1, targetDate))
    'EDABより仕入日基準の年度で最大の枝番を取得
    sql = ""
    sql = sql & " SELECT MAX(EBCODE) as MAX_EDABAN "
    sql = sql & " FROM EDAB "
    sql = sql & " WHERE YEAR(DateAdd(m,-" & CInt(START_MONTH) - 1 & ",NYUKOBI)) = " & targetYear
    sql = sql & "  GROUP BY YEAR(DateAdd(m,-" & CInt(START_MONTH - 1) & ",NYUKOBI))"

    'sql &= " SELECT MAX(src.EBCODE) AS MAX_EDABAN "
    'sql &= " FROM (" & ComAddSqlSearchCondition(SqlEdaInfSrcBase, DSP_LIMIT) & ") as src"

    Return sql
  End Function

  ''' <summary>
  ''' 枝情報マスタ挿入SQL文の作成
  ''' </summary>
  ''' <param name="prmProcTime">登録日時</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsEDAB(prmProcTime As String _
                              , Optional bRunAccess As Boolean = False) As String
    Dim sql As String = String.Empty
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    ' 日付区切り文字
    Dim dateDelimiter As String = GetDateDelimiter(bRunAccess)

    ' 編集コントロールの値を保持する連想配列
    ' Key - EDABの項目名 VALUE－ 登録する値
    Dim tmpKeyValue As Dictionary(Of String, String) = CtrlVal2Dec4EDAB(prmProcTime)

    ' 新規登録に必要な値を設定
    With tmpKeyValue
      .Add("TDATE", dateDelimiter & prmProcTime & dateDelimiter)  ' 登録日付
      .Add("FILER2", "0")                     ' ？
    End With

    ' 画面の値をSQL文に変換
    For Each tmpKey As String In tmpKeyValue.Keys
      tmpDst &= tmpKey & ","
      tmpVal &= tmpKeyValue(tmpKey) & ","
    Next

    sql &= " INSERT INTO EDAB( "
    sql &= tmpDst.Substring(0, tmpDst.Length - 1)
    sql &= ")"
    sql &= " VALUES("
    sql &= tmpVal.Substring(0, tmpVal.Length - 1)
    sql &= ")"

    Return sql
  End Function

  ''' <summary>
  ''' CUJ挿入SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsCutJ(prmProcTime As String) As String
    Dim sql As String = String.Empty

    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    ' 編集コントロールの値を保持する連想配列
    ' Key - EDABの項目名 VALUE－ 登録する値
    Dim tmpKeyValue As Dictionary(Of String, String) = CtrlVal2Dec4CUTJ(prmProcTime)

    ' 新規登録に必要な値を設定
    With tmpKeyValue
      .Add("BICODE", EDANIKU_CODE.ToString())
      .Add("TDATE", "'" & prmProcTime & "'")
      .Add("KIKAINO", "999")
      .Add("SPKUBUN", "1")
      .Add("GBFLG", "2")
      .Add("KEIRYOBI", "'" & Me.TxtShiireDate.Text & "'")
      '<---------- 2023/08/23 koumoto.T Mod
      '.Add("SETCD", "101")
      .Add("SETCD", "1000")
      '2023/08/23 koumoto.T Mod ----------> 
      .Add("KAKOUBI", "'" & Me.TxtShiireDate.Text & "'")
      .Add("KIGENBI", Date.Parse(Me.TxtShiireDate.Text).AddDays(30).ToString("yyMMdd"))
      .Add("HONSU", "1")
    End With


    ' 画面の値をSQL文に変換
    For Each tmpKey As String In tmpKeyValue.Keys
      tmpDst &= tmpKey & ","
      tmpVal &= tmpKeyValue(tmpKey) & ","
    Next

    sql &= " INSERT INTO CUTJ( "
    sql &= tmpDst.Substring(0, tmpDst.Length - 1)
    sql &= ")"
    sql &= " VALUES("
    sql &= tmpVal.Substring(0, tmpVal.Length - 1)
    sql &= ")"

    Return sql

  End Function

  ''' <summary>
  ''' 枝情報マスタ更新SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdEdab(prmProcTime As String _
                              , Optional bRunAccess As Boolean = False) As String
    Dim sql As String = String.Empty
    ' 編集コントロールの値を保持する連想配列
    ' Key - EDABの項目名 VALUE－ 登録する値
    Dim tmpKeyValue As Dictionary(Of String, String) = CtrlVal2Dec4EDAB(prmProcTime, bRunAccess)

    sql &= " UPDATE EDAB "
    sql &= " SET "
    For Each tmpKey As String In tmpKeyValue.Keys
      sql &= tmpKey & "=" & tmpKeyValue(tmpKey) & " ,"
    Next

    ' 最終のカンマを削除
    sql = sql.Substring(0, sql.Length - 1)

    ' 一覧で選択されたデータを抽出条件に設定
    sql &= SqlWhereUpdateTargetEDAB(bRunAccess)

    Return sql

  End Function

  ''' <summary>
  ''' CUTJ更新SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutJ(prmProcTime As String) As String
    Dim sql As String = String.Empty

    ' 編集コントロールの値を保持する連想配列
    ' Key - EDABの項目名 VALUE－ 登録する値
    Dim tmpKeyValue As Dictionary(Of String, String) = CtrlVal2Dec4CUTJ(prmProcTime)

    sql &= " UPDATE CUTJ "
    sql &= " SET "
    For Each tmpKey As String In tmpKeyValue.Keys
      sql &= tmpKey & " = " & tmpKeyValue(tmpKey)
      sql &= ","
    Next

    ' 最終のカンマを削除
    sql = sql.Substring(0, sql.Length - 1)

    sql &= SqlWhereUpdateTargetCutJ()

    Return sql
  End Function

  ''' <summary>
  ''' 枝情報マスタ削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelEDAB(Optional bRunAccess As Boolean = False) As String
    Dim sql As String = String.Empty

    If bRunAccess Then
      sql &= " DELETE * FROM EDAB "
    Else
      sql &= " DELETE FROM EDAB "
    End If

    sql &= SqlWhereUpdateTargetEDAB(bRunAccess)

    Return sql
  End Function

  ''' <summary>
  ''' CUTJ削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''  フラグ変更で削除状態とする
  ''' </remarks>
  Private Function SqlDelCutJ() As String
    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET KYOKUFLG = 2 "
    sql &= SqlWhereUpdateTargetCutJ()

    Return sql
  End Function

  ''' <summary>
  ''' EDAB重複チェック用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelDuplicateEDAB() As String
    Dim sql As String = String.Empty
    ' 編集コントロールの値を保持する連想配列(更新日付は使用しないのでブランク)
    Dim tmpKeyValue As Dictionary(Of String, String) = CtrlVal2Dec4EDAB("")

    Dim sqlWher As String = String.Empty

    ' 画面上の項目で枝番・屠場コード・上場コードのみ検索条件とする
    For Each tmpKey As String In tmpKeyValue.Keys
      If tmpKey.Equals("EBCODE") _
        OrElse tmpKey.Equals("TJCODE") _
        OrElse tmpKey.Equals("EDC") Then
        sqlWher &= "EDAB." & tmpKey & "=" & tmpKeyValue(tmpKey)
        sqlWher &= " AND "
      End If
    Next

    ' 最終の" AND "を削除
    sqlWher = sqlWher.Substring(0, sqlWher.Length - " AND ".Length)

    ' 一覧抽出用SQL文に画面上の項目を検索条件として追加
    sql &= ComAddSqlSearchCondition(CreateGrid1Src1(), sqlWher)

    Return sql
  End Function

  ''' <summary>
  ''' EDAB更新時の更新対象となるCUTJ抽出SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelUpdateTargetCutJ() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM CUTJ "
    sql &= SqlWhereUpdateTargetCutJ()

    Return sql
  End Function

  ''' <summary>
  ''' 画面上の編集項目の値を連想配列につめて返す
  ''' </summary>
  ''' <returns>値をつめた連想配列</returns>
  ''' <remarks>
  '''  連想配列のキーはEDABの項目名とする
  ''' </remarks>
  Private Function CtrlVal2Dec4EDAB(prmProcDate As String _
                                    , Optional bRunAccess As Boolean = False) As Dictionary(Of String, String)
    Dim ret As New Dictionary(Of String, String)

    ' 日付区切り文字取得
    Dim dateDelimiter As String = GetDateDelimiter(bRunAccess)

    With ret
      .Add("KDATE", dateDelimiter & prmProcDate & dateDelimiter)                ' 更新日付
      .Add("SIIREBI", dateDelimiter & Me.TxtShiireDate.Text & dateDelimiter)    ' 仕入日
      .Add("NYUKOBI", dateDelimiter & Me.TxtShiireDate.Text & dateDelimiter)    ' 入庫日
      .Add("EBCODE", Me.TxtEdaban1.Text)                                        ' 枝番コード
      .Add("TJCODE", ComBlank2ZeroText(Me.TxtLblMstTojyo1.CodeTxt))             ' 屠場コード
      .Add("EDC", ComBlank2ZeroText(Me.TxtEdaban2.Text))                        ' 上場コード
      .Add("KIKAKUC", ComBlank2ZeroText(Me.TxtLblMstKikaku1.CodeTxt))           ' 規格コード
      .Add("SYUBETUC", ComBlank2ZeroText(Me.TxtLblMstSyubetsu1.CodeTxt))        ' 種別コード
      .Add("KAKUC", ComBlank2ZeroText(Me.TxtLblMstKakuduke1.CodeTxt))           ' 格付コード
      .Add("SRCODE", ComBlank2ZeroText(Me.TxtLblMstShiresaki1.CodeTxt))         ' 仕入先コード
      .Add("GENSANCHIC", ComBlank2ZeroText(Me.TxtLblMstGensanchi1.CodeTxt))     ' 原産地コード
      .Add("KOTAINO", ComBlank2ZeroText(Me.TxKotaiNo1.Text))                    ' 個体識別番号
      .Add("JYURYO", ComKg2G(Me.TxtWeitghtKgBoth.Text))                         ' 1頭重量
      .Add("JYURYO2", ComKg2G(Me.TxtWeitghtKgRight.Text))                       ' 右重量
      .Add("JYURYO1", ComKg2G(Me.TxtWeitghtKgLeft.Text))                        ' 左重量
      .Add("SIIREGAKU", ComBlank2ZeroText(Me.TxTankaBoth.Text))                 ' 1頭仕入単価
      .Add("SIIREGAKU2", ComBlank2ZeroText(Me.TxTankaRight.Text))               ' 右仕入単価
      .Add("SIIREGAKU1", ComBlank2ZeroText(Me.TxTankaLeft.Text))                ' 左仕入単価
      .Add("FILER1", ComBlank2ZeroText(Me.TxtLblMstBrand1.CodeTxt))             ' ブランド

      .Add("KUBUN", "1")                                                        ' 区分
      .Add("SFLG", "1")                                                         ' 送信フラグ
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 画面上の編集項目の値を連想配列につめて返す
  ''' </summary>
  ''' <returns>値をつめた連想配列</returns>
  ''' <remarks>
  '''  連想配列のキーはCUTJの項目名とする
  ''' </remarks>
  Private Function CtrlVal2Dec4CUTJ(prmProcDate As String) As Dictionary(Of String, String)
    Dim ret As New Dictionary(Of String, String)

    With ret
      .Add("KDATE", "'" & prmProcDate & "'")                                    ' 更新日付
      .Add("TJCODE", ComBlank2ZeroText(Me.TxtLblMstTojyo1.CodeTxt))             ' 屠場コード
      .Add("EBCODE", Me.TxtEdaban2.Text)                                        ' 枝番コード(上場コードを設定)
      .Add("KIKAKUC", ComBlank2ZeroText(Me.TxtLblMstKikaku1.CodeTxt))           ' 規格コード
      .Add("SYUBETUC", ComBlank2ZeroText(Me.TxtLblMstSyubetsu1.CodeTxt))        ' 種別コード
      .Add("KAKUC", ComBlank2ZeroText(Me.TxtLblMstKakuduke1.CodeTxt))           ' 格付コード
      .Add("SRCODE", ComBlank2ZeroText(Me.TxtLblMstShiresaki1.CodeTxt))         ' 仕入先コード
      .Add("GENSANCHIC", ComBlank2ZeroText(Me.TxtLblMstGensanchi1.CodeTxt))     ' 原産地コード
      .Add("KOTAINO", ComBlank2ZeroText(Me.TxKotaiNo1.Text))                    ' 個体識別番号

      ' 入力されている重量項目から左右区分・重量・原価を決定
      If ComBlank2ZeroText(Me.TxtWeitghtKgBoth.Text) <> "0" Then
        ' 1頭重量が入力されている（＝1頭）
        .Add("SAYUKUBUN", PARTS_SIDE_BOTH.ToString())
        .Add("JYURYO", ComKg2G(Me.TxtWeitghtKgBoth.Text))
        .Add("GENKA", ComBlank2ZeroText(Me.TxTankaBoth.Text))
      ElseIf ComBlank2ZeroText(Me.TxtWeitghtKgRight.Text) <> "0" Then
        ' 右重量が入力されている（＝右半頭）
        .Add("SAYUKUBUN", PARTS_SIDE_RIGHT.ToString())
        .Add("JYURYO", ComKg2G(Me.TxtWeitghtKgRight.Text))
        .Add("GENKA", ComBlank2ZeroText(Me.TxTankaRight.Text))
      Else
        .Add("SAYUKUBUN", PARTS_SIDE_LEFT.ToString())
        .Add("JYURYO", ComKg2G(Me.TxtWeitghtKgLeft.Text))
        .Add("GENKA", ComBlank2ZeroText(Me.TxTankaLeft.Text))
      End If

    End With

    Return ret

  End Function

  ''' <summary>
  ''' 枝情報更新時の対応するCUTJ抽出条件文作成
  ''' </summary>
  ''' <returns>抽出条件文</returns>
  Private Function SqlWhereUpdateTargetCutJ() As String
    Dim sql As String = String.Empty

    sql &= " WHERE KYOKUFLG = 0 "
    sql &= "   And KUBUN <> 9 "
    sql &= "   And BICODE = " & EDANIKU_CODE.ToString()
    sql &= "   And KIKAINO = 999 "
    sql &= "   And KAKOUBI >= '" & _SelectedData("SIIREBI") & "'"
    sql &= "   And KAKOUBI <= '" & Date.Parse(_SelectedData("SIIREBI")).AddDays(10).ToString("yyyy/MM/dd") & "'"
    sql &= "   And EBCODE = " & _SelectedData("EDC")
    sql &= "   And KOTAINO = " & _SelectedData("KOTAINO")
    sql &= "   And TJCODE = " & _SelectedData("TJCODE")

    Return sql
  End Function

  ''' <summary>
  ''' 一覧選択時のEDAB抽出条件文作成
  ''' </summary>
  ''' <returns>抽出条件文</returns>
  Private Function SqlWhereUpdateTargetEDAB(Optional bRunAccess As Boolean = False) As String
    Dim sql As String = String.Empty

    ' 日付区切り文字取得
    Dim dateDelimiter As String = GetDateDelimiter(bRunAccess)


    sql &= " WHERE EBCODE =" & _SelectedData("EBCODE")
    sql &= "   AND EDC = " & _SelectedData("EDC")
    sql &= "   AND TJCODE = " & _SelectedData("TJCODE")
    sql &= "   AND KOTAINO = " & _SelectedData("KOTAINO")
    sql &= "   AND KDATE = " & dateDelimiter & _SelectedData("KDATE") & dateDelimiter

    Return sql
  End Function

  ''' <summary>
  ''' 日付区切り文字取得
  ''' </summary>
  ''' <param name="bRunAccess"></param>
  ''' <returns></returns>
  Private Function GetDateDelimiter(bRunAccess As Boolean) As String
    Return IIf(bRunAccess, "#", "'")
  End Function
#End Region

#End Region

#Region "肢番関連"

  ''' <summary>
  ''' 肢番採番
  ''' </summary>
  ''' <returns></returns>
  Private Function NumberingEdaban(targetDate As String) As Integer
    Dim ret As Integer = 0
    Dim tmpEdaban As Integer = 0

    ' 枝番の最大値を取得
    tmpEdaban = GetMaxEdaban(targetDate)

    If tmpEdaban < EDABAN_MAX Then
      ret = tmpEdaban + 1
    Else
      ' 最終番号に到達
      ' 期間内の最終登録枝番を取得
      tmpEdaban = GetLastEdaban()
      If tmpEdaban < EDABAN_MAX Then
        ret = tmpEdaban + 1
      Else
        ret = EDABAN_MIN
      End If

    End If

    Return ret
  End Function

  ''' <summary>
  ''' 期間内の最終登録の枝番を取得するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function GetLastEdaban() As Integer
    Dim ret As Integer = EDABAN_MIN
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    With tmpDb
      Try
        .GetResult(tmpDt, SqlSelLastEdaban())
        If tmpDt.Rows.Count > 0 Then
          ret = Integer.Parse(tmpDt.Rows(0)("LAST_EDABAN"))
        End If
      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("枝番の最終値の取得に失敗しました。")
      Finally
        tmpDb.DbDisconnect()
        tmpDt.Dispose()
      End Try

    End With

    Return ret
  End Function


  ''' <summary>
  ''' 期間内の枝番の最大値を取得
  ''' </summary>
  ''' <returns></returns>
  Private Function GetMaxEdaban(targetDate As String) As Integer
    Dim ret As Integer = EDABAN_MIN
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    With tmpDb
      Try
        Dim tmpEdaNo As Integer
        .GetResult(tmpDt, SqlSelMaxEdaban(targetDate))
        If tmpDt.Rows.Count = 0 Then
          ret = 0
        ElseIf tmpDt.Rows.Count > 0 _
          AndAlso Integer.TryParse(tmpDt.Rows(0)("MAX_EDABAN").ToString(), tmpEdaNo) Then
          ret = tmpEdaNo
        End If
      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("枝番の最大値取得に失敗しました。")
      Finally
        tmpDb.DbDisconnect()
        tmpDt.Dispose()
      End Try
    End With

    Return ret
  End Function
#End Region

#Region "コントロール制御"

  ''' <summary>
  ''' 一覧表示グリッドに合わせて画面サイズを修正する
  ''' </summary>
  Private Sub ReSizeForm()

    'Gridサイズ設定
    Me.DataGridView1.Width = Me.Width - 40

    ' フォームサイズ設定
    RemoveHandler MyBase.Resize, AddressOf Form_Resize
    Me.Width = Me.DataGridView1.Width + 40
    AddHandler MyBase.Resize, AddressOf Form_Resize


    With Me.DataGridView1
      '高さ修正
      .Height = Me.Height - .Location.Y - btnClose.Height - 70
    End With


    ' コマンドボタン再配置
    Dim tmpBtnLocationY As Integer = Me.DataGridView1.Height + Me.DataGridView1.Location.Y + 20
    With btnClose
      .Location = New Point(Me.Width - 30 - .Width, tmpBtnLocationY)
    End With

    With btnPrint
      .Location = New Point(Me.Width - 30 - btnClose.Width - .Width - 20, tmpBtnLocationY)
    End With

    With btnDelete
      .Location = New Point(Me.Width - 30 - btnClose.Width - btnPrint.Width - .Width - 20 - 20, tmpBtnLocationY)
    End With

    With btnRegist
      .Location = New Point(Me.Width - 30 - btnClose.Width - btnPrint.Width - btnDelete.Width - .Width - 20 - 20 - 20, tmpBtnLocationY)
    End With


    ' インフォメーションラベル再配置
    With lblInformation
      .Location = New Point(10, Me.Height - .Height - 50)
    End With
  End Sub


  ''' <summary>
  ''' 連想配列の保持された枝情報を画面のコントロールに表示
  ''' </summary>
  Private Sub Dic2Dsp()

    Me.TxtShiireDate.Text = _SelectedData("SIIREBI").Substring(0, 10) ' 仕入日(yyyy/MM/dd のみ切り出し)
    Me.TxtEdaban1.Text = _SelectedData("EBCODE")                      ' 枝番コード
    Me.TxtLblMstTojyo1.CodeTxt = _SelectedData("TJCODE")              ' 屠場コード
    Me.TxtEdaban2.Text = _SelectedData("EDC")                         ' 上場コード
    Me.TxtLblMstKikaku1.CodeTxt = _SelectedData("KIKAKUC")            ' 規格コード
    Me.TxtLblMstSyubetsu1.CodeTxt = _SelectedData("SYUBETUC")         ' 種別コード
    Me.TxtLblMstShiresaki1.CodeTxt = _SelectedData("SRCODE")          ' 仕入先コード
    Me.TxtLblMstKakuduke1.CodeTxt = _SelectedData("KAKUC")            ' 格付コード
    Me.TxtLblMstGensanchi1.CodeTxt = _SelectedData("GENSANCHIC")      ' 原産地コード
    Me.TxtLblMstBrand1.CodeTxt = _SelectedData("FILER1")              ' ブランド
    Me.TxKotaiNo1.Text = _SelectedData("KOTAINO")                     ' 個体識別番号

    Me.TxtWeitghtKgBoth.Text = ComG2Kg(_SelectedData("JYURYO"))       ' 1頭重量（g→Kg変換）
    Me.TxtWeitghtKgRight.Text = ComG2Kg(_SelectedData("JYURYO2"))     ' 右重量（g→Kg変換）
    Me.TxtWeitghtKgLeft.Text = ComG2Kg(_SelectedData("JYURYO1"))      ' 左重量（g→Kg変換）

    Me.TxTankaBoth.Text = _SelectedData("SIIREGAKU")                  ' 1頭仕入単価
    Me.TxTankaRight.Text = _SelectedData("SIIREGAKU2")                ' 右仕入単価
    Me.TxTankaLeft.Text = _SelectedData("SIIREGAKU1")                 ' 左仕入単価

  End Sub
#End Region

#Region "データ操作"

  ''' <summary>
  ''' DataTabale → Dictionary(String,String)変換
  ''' </summary>
  ''' <param name="prmSrc">DataTabale（元データ）</param>
  ''' <returns>変換したDictionary</returns>
  Private Function Dt2Dic(prmSrc As DataTable) As List(Of Dictionary(Of String, String))
    Dim ret As New List(Of Dictionary(Of String, String))
    Dim tmpKeyList As New List(Of String)

    ' 列名をリストに保持
    For Each tmpCol As DataColumn In prmSrc.Columns
      tmpKeyList.Add(tmpCol.ColumnName)
    Next

    ' データテーブルの最終行までデータをループ
    For Each tmpDr As DataRow In prmSrc.Rows
      Dim tmpDic As New Dictionary(Of String, String)

      ' 全列データを連想配列に保持
      For Each tmpKey As String In tmpKeyList
        tmpDic.Add(tmpKey, tmpDr(tmpKey).ToString())
      Next

      ' 連想配列をリストに保持
      ret.Add(tmpDic)
    Next

    Return ret
  End Function

#End Region

#End Region

#Region "パブリック"
  ''' <summary>
  ''' 枝情報抽出用SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Public Function SqlEdaInfSrcBase() As String
    Dim sql As String = String.Empty

    sql &= " SELECT DISTINCT EDAB.KUBUN "
    sql &= "              , EDAB.EBCODE "
    sql &= "              , CONCAT(FORMAT(EDAB.KAKUC,'00') , ':' , KZNAME)  AS DKZNAME  "
    sql &= "              , CONCAT(FORMAT(EDAB.KIKAKUC,'00') , ':' , KKNAME)  AS DKKNAME  "
    sql &= "              , CONCAT(FORMAT(EDAB.SYUBETUC,'00') , ':' , SBNAME)  AS DSBNAME  "
    sql &= "              , CONCAT(FORMAT(EDAB.GENSANCHIC,'00') , ':' , GNNAME)  AS DGNNAME  "
    sql &= "              , CONCAT(FORMAT(EDAB.SRCODE,'0000') , ':' , CUTSR.LSRNAME)  AS DLSRNAME  "
    sql &= "              , CONCAT(FORMAT(EDAB.FILER1,'00') , ':' , BLNAME)  AS DBRNAME  "
    sql &= "              , EDAB.FILER1 "
    sql &= "              , EDAB.FILER2 "
    sql &= "              , EDAB.KOTAINO "
    sql &= "              , CONCAT(FORMAT(EDAB.TJCODE,'00') , ':' , TJNAME)  AS DTJNAME  "
    sql &= "              , EDAB.EDC "
    sql &= "              , EDAB.NYUKOBI "
    sql &= "              , EDAB.SIIREBI "
    sql &= "              , EDAB.JYURYO "
    sql &= "              , EDAB.JYURYO1 "
    sql &= "              , EDAB.JYURYO2 "
    sql &= "              , EDAB.SIIREGAKU "
    sql &= "              , EDAB.SIIREGAKU1 "
    sql &= "              , EDAB.SIIREGAKU2 "
    sql &= "              , EDAB.TDATE "
    sql &= "              , EDAB.KDATE "
    sql &= "              , EDAB.SFLG "
    sql &= "              , EDAB.KIKAKUC "
    sql &= "              , EDAB.SYUBETUC "
    sql &= "              , EDAB.KAKUC "
    sql &= "              , EDAB.SRCODE "
    sql &= "              , EDAB.GENSANCHIC "
    sql &= "              , EDAB.TJCODE "
    sql &= "              , KZNAME "
    sql &= "              , KKNAME "
    sql &= "              , SBNAME "
    sql &= "              , GNNAME "
    sql &= "              , LSRNAME "
    sql &= " FROM (((((( EDAB  "
    sql &= "              LEFT JOIN TOJM ON  TOJM.TJCODE = EDAB.TJCODE) "
    sql &= "              LEFT JOIN SHUB ON SHUB.SBCODE = EDAB.SYUBETUC) "
    sql &= "              LEFT JOIN KIKA ON KIKA.KICODE = EDAB.KIKAKUC) "
    sql &= "              LEFT JOIN KAKU ON KAKU.KKCODE = EDAB.KAKUC) "
    sql &= "              LEFT JOIN GENSN ON GENSN.GNCODE = EDAB.GENSANCHIC) "
    sql &= "              LEFT JOIN CUTSR ON EDAB.SRCODE = CUTSR.SRCODE ) "
    sql &= "              LEFT JOIN BLOCK_TBL ON EDAB.FILER1 = BLOCK_TBL.BLOCKCODE "

    Return sql
  End Function
#End Region

#End Region

#Region "イベントプロシージャー"

#Region "フォーム"

  ''' <summary>
  ''' フォームロード時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Edainp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    ' データグリッド初期化
    Call InitForm01()
    With Controlz(DG1V1.Name)
      .AutoSearch = True                                        ' 検索処理実行
      .lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick ' ダブルクリック時処理設定
    End With

    ' メッセージ設定（標準以外）
    Me.TxtEdaban2.SetMsgLabelText("上場コードを入力して下さい（出荷NO.）1から9999")

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)

    ' 画面タイトル設定
    Me.Text = PRG_TITLE

    ' IPC通信起動
    InitIPC(PRG_ID)

    ' 屠場コード更新後処理コールバック
    ' 屠場コード・上場コードが入力されたら枝番を自動採番
    With Me.TxtLblMstTojyo1.CodeCtrl
      .lcCallBackMstTxtValidated = AddressOf EdaKindValidated
      .lcCallBackSetText = AddressOf EdaKindDataChanged
    End With

    With Me.TxtEdaban2
      .lcCallBackValidated = AddressOf EdaKindValidated
      .lcCallBackSetText = AddressOf EdaKindDataChanged
    End With


    '枝番コード入力不可
    Me.TxtEdaban1.ReadOnly = True
    Me.TxtEdaban1.TabStop = False

    ' 最大化で表示
    Me.WindowState = FormWindowState.Maximized

    ' 画面サイズに合わせてグリッドサイズ変更
    Call ReSizeForm()

    '仕入日にフォーカスをあてる
    Me.ActiveControl = Me.TxtShiireDate
  End Sub

  ''' <summary>
  ''' フォームサイズ変更時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' フォームロード後にイベントを割り当てる
  ''' </remarks>
  Private Sub Form_Resize(sender As Object, e As EventArgs)
    Call ReSizeForm()
  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 更新ボタン押下処理
        With btnRegist
          .Focus()
          .PerformClick()
        End With
      ' F5キー押下時
      Case Keys.F5
        ' 削除ボタン押下処理
        With btnDelete
          .Focus()
          .PerformClick()
        End With
      ' F9キー押下時
      Case Keys.F9
        ' 印刷ボタン押下処理
        With btnPrint
          .Focus()
          .PerformClick()
        End With
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        With btnClose
          .Focus()
          .PerformClick()
        End With
    End Select
  End Sub


#End Region

#Region "ボタン"

  ''' <summary>
  ''' 登録ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnRegist_Click(sender As Object, e As EventArgs) Handles btnRegist.Click

    ' 個体識別番号確認
    If ComBlank2ZeroText(Me.TxKotaiNo1.Text) = "0" _
      AndAlso typMsgBoxResult.RESULT_YES <> ComMessageBox("個体識別番号がまちがっています" _
                                                            & vbCrLf & "それでも登録しますか？" _
                                                         , PRG_TITLE _
                                                         , typMsgBox.MSG_WARNING _
                                                         , typMsgBoxButton.BUTTON_YESNO) Then

      Exit Sub
    End If

    ' 確認メッセージを表示し更新処理実行
    If typMsgBoxResult.RESULT_OK <> ComMessageBox("登録更新しますか？" _
                                              , PRG_TITLE _
                                              , typMsgBox.MSG_NORMAL _
                                              , typMsgBoxButton.BUTTON_OKCANCEL _
                                              , MessageBoxDefaultButton.Button1) Then
      Exit Sub
    End If

    Try
      ' 重複チェック
      If ExistsEdab() Then
        ComMessageBox("枝番がダブって（重複して）います。" _
                    , PRG_TITLE _
                    , typMsgBox.MSG_WARNING _
                    , typMsgBoxButton.BUTTON_OK)

      Else
        If (_SelectedData.Keys.Count > 0) Then
          ' データは一覧から選択された（＝更新処理）
          Call UpDateEdaInfo()
        Else
          ' データは一覧から選択されていない（＝追加処理）
          Call CreateEdaInfo()
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' 削除ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    ' 確認メッセージを表示し更新処理実行
    If typMsgBoxResult.RESULT_OK <> ComMessageBox("削除しますか？" _
                                              , PRG_TITLE _
                                              , typMsgBox.MSG_NORMAL _
                                              , typMsgBoxButton.BUTTON_OKCANCEL) Then
      Exit Sub
    End If


    If (_SelectedData.Keys.Count <= 0) Then
      ComMessageBox("データが選択されていません" _
                    , PRG_TITLE _
                    , typMsgBox.MSG_NORMAL _
                    , typMsgBoxButton.BUTTON_OK)

    Else
      Try
        Call DelEdaInfo()
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
    Dim tmpPrintForm As New SF_Edainp

    Try
      ' 印刷フォーム起動
      tmpPrintForm.ShowSubForm(Nothing, Me)
    Catch ex As Exception
      ' Error
      Call ComWriteErrLog(ex, False)
    Finally
      tmpPrintForm.Dispose()
    End Try

  End Sub

  ''' <summary>
  ''' 閉じるボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    MyBase.AllClear()
    With Controlz(DG1V1.Name)
      .InitSort()
      .ResetPosition()
    End With
    _SelectedData.Clear()
    Me.TxtShiireDate.Focus()
    Me.Hide()
  End Sub


#End Region

#Region "グリッド関連"

  ''' <summary>
  ''' グリッドダブルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' ダブルクリックされたデータを編集欄に設定する
  ''' </remarks>
  Private Sub DgvCellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    ' グリッドで選択されたデータを取得
    ' ※_SelectedDataは新規追加 or 更新 の判断に使用
    _SelectedData = Controlz(sender.Name).SelectedRow

    ' 画面表示
    Dic2Dsp()

  End Sub


#End Region

#Region "テキストボックス"

  ''' <summary>
  ''' 仕入日フォーカス取得時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtShiireDate_GotFocus(sender As Object, e As EventArgs) Handles TxtShiireDate.GotFocus

    ' 日付形式文字列が入力されているなら仕入日以外の入力内容をクリア
    ' ※マウスでのフォーカス移動時に日付エラーが発生した場合の対応
    If Me.TxtShiireDate.Text.Trim = "" Then
      Me.TxtShiireDate.Text = ComGetProcDate()
    End If

    If DirectCast(sender, TxtDateBase).HasError = False Then

      ' 全編集項目をクリア
      MyBase.AllClear(）

      ' 選択内容をクリア
      _SelectedData.Clear()

    End If

  End Sub

  ''' <summary>
  ''' バリデーション時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtShiireDateValidating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtShiireDate.Validating
    ' 未入力なら本日日付を設定する
    With Me.TxtShiireDate
      If .Text.Length <= 0 Then
        .Text = ComGetProcDate()
      End If
    End With
  End Sub

  ''' <summary>
  ''' データ変更後処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' 屠場コード・上場コード変更後処理
  ''' ※ 屠場コードはコールバックによる呼出し
  ''' </remarks>
  Private Sub EdaKindValidated(sender As Object, e As EventArgs)


    Try
      ' 枝情報検索
      Call SerchEdaInfo()
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try


  End Sub

  ''' <summary>
  ''' データ変更後処理
  ''' </summary>
  ''' <remarks>
  ''' 屠場コード・上場コードのテキスト変更時に実行
  ''' ※ コールバックによる呼出し
  ''' </remarks>
  Private Sub EdaKindDataChanged()

    Try
      ' 枝情報検索
      Call SerchEdaInfo()
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

#End Region

#End Region

End Class
