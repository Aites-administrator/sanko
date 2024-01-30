Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports T.R.ZCommonClass.clsDataGridSearchControl

Imports System

Public Class Form_EdaSeisan
  Implements IDgvForm02

#Region "定数定義"

  ''' <summary>
  ''' グリッド高さ最大表示
  ''' </summary>
  Private Const GRID_HEIGHT_MAX As Integer = 720
  ''' <summary>
  ''' グリッド高さ最大表示
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

  Private Const PRG_ID As String = "EdaSei"

#End Region

#Region "メンバ"

#Region "プライベート"
  ' 古い枝別精算表データを削除フラグ
  Private SyokaiFlg As Boolean = False
  '１つ目のDataGridViewオブジェクト格納先
  Private DG2V1 As DataGridView
  ' データグリッドのフォーカス喪失時の位置
  Private rowIndexDataGrid As Integer
  ' データグリッドの選択件数
  Private dataGridCount As Long
  ' データグリッドの検索日付
  Private dataGridDate As String
  ' 積算一時テーブル名
  Private tmpSeisanTblName As String

  ' フォーカスの行位置変更指示
  Private gridRowIdx As Integer = -1
  ' フォーカス変更有無フラグ
  Dim forcusChg As EventArgs = Nothing

#End Region

#End Region
#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_EdaSeisan, AddressOf ComRedisplay)
  End Sub
#End Region

#Region "データグリッドビュー操作関連共通"

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

    '  DG2V1.Height = SetFrameInDisp(False)
    DG2V1.Left = GRID_POS_X
    DG2V1.Top = GRID_POS_Y

    ' グリッド、グリッドのタイトル、メッセージの幅を統一する
    Label_GridData.Width = GRIDWIDTH_MAX
    DG2V1.Width = GRIDWIDTH_MAX
    lblInformation.Width = GRIDWIDTH_MAX

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
        .FixedRow = 1

        ' 検索コントロール設定
        .AddSearchControl(Me.CmbDateProcessing_01, "KAKOUBI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxtEdaban_01, "EDABAN", typExtraction.EX_EQ, typColumnKind.CK_Number)

        '１つ目のDataGridViewオブジェクトの検索コントロール設定

        ' 編集可能列設定
        .EditColumnList = CreateGrid2EditCol1()

      End With
    End With

  End Sub

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' ''' <remarks>
  '''   画面毎の抽出内容をここに記載する
  ''' </remarks>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM ("
    sql &= "  SELECT * "
    sql &= "      , IIF (SAYU = " & PARTS_SIDE_LEFT.ToString & ",'左','右') AS SAYUTYPE"
    sql &= "      , FORMAT(CONVERT(int, KOTAINO), '0000000000') AS KOTAINOZERO "
    sql &= "  FROM SEISAN "
    sql &= " ) AS src "
    sql &= " ORDER BY EDABAN "
    sql &= "        , SAYU "
    sql &= "        , SHOHINC"

    Return sql

  End Function

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("枝番号", "EDABAN", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("個体識別", "KOTAINOZERO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("加工日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112, argFormat:="yyyy/MM/dd"))
      .Add(New clsDGVColumnSetting("上場番号", "JYOUBAN", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=96, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("水引重量", "MIZUHIKIGO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=96, argFormat:="#,##0.0"))
      .Add(New clsDGVColumnSetting("仕入単価", "STANKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=96, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("格付", "KAKU", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=128))
      .Add(New clsDGVColumnSetting("品種", "SHUB", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("原産地", "GENSAN", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=128))
      .Add(New clsDGVColumnSetting("部位", "BUICODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("部位名", "BUIMEI", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      If (clsGlobalData.SEISAN_TYPE = 1) Then
        .Add(New clsDGVColumnSetting("左右", "SAYUTYPE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=64))
        .Add(New clsDGVColumnSetting("原価", "STANKA2", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="#,##0"))
      End If
      If (clsGlobalData.SEISAN_TYPE = 1) Then
        .Add(New clsDGVColumnSetting("重量", "HIDARI", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="#,##0.0"))
      Else
        .Add(New clsDGVColumnSetting("左重量", "HIDARI", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="#,##0.0"))
        .Add(New clsDGVColumnSetting("右重量", "MIGI", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="#,##0.0"))
      End If
      .Add(New clsDGVColumnSetting("売単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80, argFormat:="#,##0"))
      .Add(New clsDGVColumnSetting("仕入日", "SIIREBI", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112, argFormat:="yyyy/MM/dd"))
      .Add(New clsDGVColumnSetting("仕向先", "TCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("仕向先名", "TNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("得意先", "UTCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("得意先名", "UTNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("売上日", "URIAGEBI", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112))
      .Add(New clsDGVColumnSetting("歩留", "BUDOMARI", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
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
      .Add(New clsDataGridEditTextBox("仕入単価", prmUpdateFnc:=AddressOf UpDateAllDb, prmValueType:=VT_SIGNED_NUMBER))
      .Add(New clsDataGridEditTextBox("原価", prmUpdateFnc:=AddressOf UpDateDb, prmValueType:=VT_SIGNED_NUMBER))
      .Add(New clsDataGridEditTextBox("売単価", prmUpdateFnc:=AddressOf UpDateDb, prmValueType:=VT_SIGNED_NUMBER))
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

#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' データグリッドの再描画
  ''' </summary>
  Private Sub DataGrid_ShowList()

    With DG2V1
      Controlz(.Name).ShowList()
    End With

  End Sub

  ''' <summary>
  ''' 更新用画面を閉じる
  ''' </summary>
  Private Sub Frame_UnDsp()

    ' 更新用画面が表示中の場合
    If (Frame_IN.Visible) Then

      DG2V1.Height = SetFrameInDisp(False)

    End If

  End Sub

  ''' <summary>
  ''' IPアドレスから自身のIPv4を抽出する
  ''' </summary>
  ''' <returns>IPv4の文字列</returns>
  Private Function GetIPv4() As String


    ' ホスト名を取得する
    Dim hostName As String = System.Net.Dns.GetHostName()

    ' ホスト名からIPアドレスを取得する
    Dim ipAdList As System.Net.IPAddress() = System.Net.Dns.GetHostAddresses(hostName)
    ' IPアドレスの一覧からIPv4を抽出する
    Dim rec As String = ""
    For Each address As System.Net.IPAddress In ipAdList
      rec = address.ToString()
    Next address

    rec = rec.Replace(".", "")

    Console.WriteLine(rec)

    Return rec

  End Function


  ''' <summary>
  ''' 一覧変更時イベント（編集されたレコードのみを更新）
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function UpDateDb() As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True

    ' 実行
    With tmpDb
      Try
        '更新日取得
        Dim UpDateDateTime As String
        UpDateDateTime = ComGetProcTime()

        ' トランザクション開始
        .TrnStart()

        ' 明細を直接修正
        sql = SqlUpdSeisan(UpDateDateTime _
                         , Controlz(DG2V1.Name).EditData _
                         , Controlz(DG2V1.Name).SelectedRow _
                         , False)

        ' SQL実行結果が1件か？
        If .Execute(sql) = 1 Then

          ' 「原価」更新の場合
          If (Controlz(DG2V1.Name).EditData.Keys(0).Equals("STANKA2")) Then

            Dim sqlCount As Long = 0

            sql = SqlUpdCutjGenka(UpDateDateTime _
                                , Controlz(DG2V1.Name).EditData _
                                , Controlz(DG2V1.Name).SelectedRow)
            sqlCount = .Execute(sql)

          End If

          ' 更新成功
          .TrnCommit()
        Else
          ' 更新失敗
          Throw New Exception("Seisanの更新に失敗しました。他のユーザーによって修正されています。")
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
  ''' 任意の枝別精算表データをバックアップする
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function BackupSeisan() As Boolean

    Dim tmpDb As New clsSeisanBackup()

    ' 実行
    With tmpDb

      Try
        ' トランザクション開始
        .TrnStart()

        'グリッドから追加SQL文を作成
        For Each item As Dictionary(Of String, String) In Controlz(DG2V1.Name).GetAllData
          .Execute(SqlInsEdaSeisan("SEISAN", item))
        Next

        ' 更新成功
        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("枝別精算表データのバックアップに失敗しました")
      End Try

      .Dispose()

    End With

    Return True

  End Function

  ''' <summary>
  ''' 精算テーブルから13ｶ月前の削除SQL文作成データを削除する（１回のみ）
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function DelOldSeisan() As Boolean

    ' １回削除したら、この処理は行わない
    If (SyokaiFlg) Then
      Return True
    End If

    ' 元のカーソルを保持
    Dim preCursor As Cursor = Cursor.Current

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True

    ' 実行
    With tmpDb
      Try
        ' トランザクション開始
        .TrnStart()

        ' 精算テーブルから、13ｶ月前の加工日を削除するSQL文作成
        sql = SqlInitDelSeisan()

        .Execute(sql)

        .TrnCommit()

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
        .TrnRollBack()                   ' RollBack
        ret = False
      Finally
        ' カーソルを元に戻す
        Cursor.Current = preCursor
        SyokaiFlg = True
      End Try

    End With

    Return ret

  End Function


  ''' <summary>
  ''' 任意の枝別精算表データを削除する
  ''' </summary>
  ''' <param name="kensu">削除件数</param>
  ''' <param name="TargetDate">加工日</param>
  ''' <param name="TargetEdaban">枝番No</param>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function DelToCreateSeisan(kensu As Long,
                                     TargetDate As String,
                                     TargetEdaban As String) As Boolean

    ' 元のカーソルを保持
    Dim preCursor As Cursor = Cursor.Current

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True

    ' 実行
    With tmpDb
      Try

        ' 古い枝別精算表データを削除する
        DelOldSeisan()

        ' 指定した加工日と枝番の精算テーブルを削除
        sql = SqlDelSeisan(CmbDateProcessing_01.Text, TxtEdaban_01.Text.Trim)

        ' SQL実行結果が想定した件数以外？
        If .Execute(sql) <> kensu Then

          ' 更新失敗
          Throw New Exception("Seisanの削除に失敗しました。他のユーザーによって修正されています。")

        Else

          ' カーソルを待機カーソルに変更
          Cursor.Current = Cursors.WaitCursor

          sqlEdabanExec(TargetDate, TargetEdaban, tmpDb, False)

        End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
        .TrnRollBack()                   ' RollBack
        ret = False
      Finally
        ' カーソルを元に戻す
        Cursor.Current = preCursor
      End Try

    End With

    Return ret

  End Function

  ''' <summary>
  ''' 一覧変更時イベント（編集されたレコードと枝番・個体識別番号・左右が同一のレコードも同時に更新）
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function UpDateAllDb() As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True
    Dim sqlKensu As Integer = 0


    ' 実行
    With tmpDb
      Try

        Dim keys As String = Controlz(DG2V1.Name).EditData.Keys(0)
        Dim Vals As String = Controlz(DG2V1.Name).EditData(keys)

        ' SQL文の作成
        sql = SqlGetSeisanCount(Controlz(DG2V1.Name).SelectedRow)

        Dim tmpDt As New DataTable
        Call tmpDb.GetResult(tmpDt, sql)

        If (1 <= tmpDt.Rows.Count) Then

          Dim dtRow As DataRow
          dtRow = tmpDt.Rows(0)
          Int32.TryParse(dtRow("SEISAN_COUNT").ToString, sqlKensu)
          If (sqlKensu = 0) Then
            ' 更新失敗
            Throw New Exception("対象データがありません")
          End If

          '更新日取得
          Dim UpDateDateTime As String
          UpDateDateTime = ComGetProcTime()

          ' トランザクション開始
          .TrnStart()

          ' 明細を直接修正
          sql = SqlUpdSeisan(UpDateDateTime _
                           , Controlz(DG2V1.Name).EditData _
                           , Controlz(DG2V1.Name).SelectedRow _
                           , True)

          ' SQL実行結果が指定した件数か？
          Dim cntRun As Integer = .Execute(sql)
          If cntRun = sqlKensu Then

            ' 修正対象の開始位置を検索する
            Dim serchIdx As Integer = 0
            For Each dr As DataGridViewRow In DG2V1.Rows
              If dr.Cells("EDABAN").Value.ToString.Equals(Controlz(DG2V1.Name).SelectedRow("EDABAN").ToString) And
               dr.Cells("SAYU").Value.ToString.Equals(Controlz(DG2V1.Name).SelectedRow("SAYU").ToString) And
               dr.Cells("KOTAINO").Value.ToString.Equals(Controlz(DG2V1.Name).SelectedRow("KOTAINO").ToString) Then
                serchIdx = dr.Index
                Exit For
              End If
            Next
            ' フォーカスの行位置変更指示
            gridRowIdx = serchIdx + sqlKensu - 1

            ' 更新成功
            .TrnCommit()
          Else
            ' 更新失敗
            Throw New Exception("Seisanの更新に失敗しました。他のユーザーによって修正されています。")
          End If
        Else
          ' 更新失敗
          Throw New Exception("Seisanの取得に失敗しました。")
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
  ''' 精算テーブル追加
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function InsertSeisanTbl(TargetDate As String _
                                 , ProcDate As String) As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True
    Dim sqlCount As Long = 0

    Try
      ' 実行
      With tmpDb

        ' 更新処理実行
        .TrnStart()

        ' 精算テーブル追加用SQL文作成
        sql = SqlInsSeisan(TargetDate, ProcDate)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 更新OK
        .TrnCommit()

      End With

    Catch ex As Exception

      Call ComWriteErrLog(ex)

      ' Error ロールバック
      tmpDb.TrnRollBack()
      Throw New Exception("Seisanテーブルの追加に失敗しました。")
      ret = False
    Finally

    End Try

    Return ret

  End Function

  ''' <summary>
  ''' 枝番精算テーブル追加
  ''' </summary>
  ''' <param name="TargetEdaban">枝番No</param>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function InsertEdabanSeisanTbl(TargetDate As String _
                                 , ProcDate As String _
                                 , TargetEdaban As String) As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True
    Dim sqlCount As Long = 0

    Try
      ' 実行
      With tmpDb

        ' 更新処理実行
        .TrnStart()

        ' 精算テーブル追加用SQL文作成
        ' 枝別精算テーブル追加用SQL文作成
        sql = SqlInsSeisanEdaban(TargetDate, ProcDate, TargetEdaban)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 更新OK
        .TrnCommit()

      End With

    Catch ex As Exception

      Call ComWriteErrLog(ex)

      ' Error ロールバック
      tmpDb.TrnRollBack()
      Throw New Exception("Seisanテーブルの追加に失敗しました。")
      ret = False
    Finally

    End Try

    Return ret

  End Function

  ''' <summary>
  ''' 精算テーブル・実績テーブル追加更新処理
  ''' </summary>
  ''' <param name="TargetDate">加工日</param>
  Private Sub CreateSeisan(TargetDate As String)

    ' 元のカーソルを保持
    Dim preCursor As Cursor = Cursor.Current

    Dim tmpDb As New clsSqlServer
    Dim sqlCount As Long = 0
    Dim Sql As String = String.Empty

    Try
      With tmpDb

        ' カーソルを待機カーソルに変更
        Cursor.Current = Cursors.WaitCursor

        '更新日取得
        Dim UpDateDateTime As String
        UpDateDateTime = ComGetProcTime()

        ' 古い枝別精算表データを削除する
        DelOldSeisan()

        If (InsertSeisanTbl(TargetDate, UpDateDateTime)) Then

          ' 精算一時テーブル作成
          Sql = SqlSetTmpSeisan()
          Console.WriteLine(Sql)
          sqlCount = .Execute(Sql)

          ' 精算テーブル取得
          Sql = SqlGetSeisan(TargetDate)
          Console.WriteLine(Sql)
          sqlCount = .Execute(Sql)
          Console.WriteLine("処理件数=" & sqlCount)

          ' 更新処理実行
          .TrnStart()

          ' 精算テーブル更新用SQL文作成（CUTJテーブル検索結果）
          Sql = SqlUpdCutJToSeisan(TargetDate, UpDateDateTime)
          Console.WriteLine(Sql)
          sqlCount = .Execute(Sql)
          Console.WriteLine("処理件数=" & sqlCount)

          ' 精算テーブルの原価自動設定SQL文作成
          Sql = SqlUpdCalcCost(TargetDate, UpDateDateTime)
          Console.WriteLine(Sql)
          sqlCount = .Execute(Sql)
          Console.WriteLine("処理件数=" & sqlCount)

          ' 精算テーブルの得意先コード、売上日を更新する
          Sql = SqlUpdCode(TargetDate, UpDateDateTime)
          Console.WriteLine(Sql)
          sqlCount = .Execute(Sql)
          Console.WriteLine("処理件数=" & sqlCount)

          ' 精算テーブルの仕向先名を更新する
          Sql = SqlUpdSimukeName(TargetDate, UpDateDateTime)
          Console.WriteLine(Sql)
          sqlCount = .Execute(Sql)
          Console.WriteLine("処理件数=" & sqlCount)

          ' 精算テーブルの得意先名を設定する
          Sql = SqlUpdTokuiName(TargetDate, UpDateDateTime)
          Console.WriteLine(Sql)
          sqlCount = .Execute(Sql)
          Console.WriteLine("処理件数=" & sqlCount)

          ' 実績テーブルの原単価更新
          Sql = SqlUpdCutjGenka(TargetDate, UpDateDateTime, False)
          Console.WriteLine(Sql)
          sqlCount = .Execute(Sql)
          Console.WriteLine("処理件数=" & sqlCount)

          ' 更新OK
          .TrnCommit()
        End If

      End With
    Catch ex As Exception

      Call ComWriteErrLog(ex)

      ' Error ロールバック
      tmpDb.TrnRollBack()
      Throw New Exception("CUTJの更新に失敗しました。枝番を指定し、初期化処理を行ってください")
    Finally
      ' カーソルを元に戻す
      Cursor.Current = preCursor

    End Try

    Exit Sub

  End Sub


  ''' <summary>
  ''' 枝別精算テーブル・実績テーブル追加更新処理
  ''' </summary>
  ''' <param name="TargetDate"></param>
  ''' <param name="TargetEdaban">枝番No</param>
  ''' <param name="tmpDb"></param>
  ''' <param name="flgCompal">原価自動計算必ず実行フラグ</param>
  Private Sub sqlEdabanExec(TargetDate As String,
                            TargetEdaban As String,
                            tmpDb As clsSqlServer,
                            flgCompal As Boolean)

    Dim sql As String = String.Empty
    Dim sqlCount As Long = 0

    '更新日取得
    Dim UpDateDateTime As String
    UpDateDateTime = ComGetProcTime()

    With tmpDb

      If (InsertEdabanSeisanTbl(TargetDate, UpDateDateTime, TargetEdaban)) Then

        ' 精算一時テーブル作成
        sql = SqlSetTmpSeisan()
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)

        ' 枝番精算テーブル取得
        sql = SqlGetSeisanEdaban(TargetDate, flgCompal, TargetEdaban)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 更新処理実行
        .TrnStart()

        ' 精算テーブル更新用SQL文作成（CUTJテーブル検索結果）
        sql = ComAddSqlSearchCondition(SqlUpdCutJToSeisan(TargetDate, UpDateDateTime) _
                                              , "SEISAN.EDABAN = " & TargetEdaban)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 枝別精算テーブルの原価自動設定SQL文作成
        sql = SqlUpdCalcCostEdaban(TargetDate, UpDateDateTime, flgCompal, TargetEdaban)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 精算テーブルの得意先コード、売上日を更新する
        sql = ComAddSqlSearchCondition(SqlUpdCode(TargetDate, UpDateDateTime) _
                                                , "SEISAN.EDABAN = " & TargetEdaban)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 精算テーブルの仕向先名を更新する
        sql = ComAddSqlSearchCondition(SqlUpdSimukeName(TargetDate, UpDateDateTime) _
                                                    , "SEISAN.EDABAN = " & TargetEdaban)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 精算テーブルの得意先名を設定する
        sql = ComAddSqlSearchCondition(SqlUpdTokuiName(TargetDate, UpDateDateTime) _
                                                   , "SEISAN.EDABAN = " & TargetEdaban)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 実績テーブルの原単価更新
        sql = SqlUpdCutjGenka(TargetDate, UpDateDateTime, flgCompal)
        Console.WriteLine(sql)
        sqlCount = .Execute(sql)
        Console.WriteLine("処理件数=" & sqlCount)

        ' 更新OK
        .TrnCommit()

      End If
    End With

  End Sub

  ''' <summary>
  ''' 原価自動計算処理実行（枝番指定）
  ''' </summary>
  ''' <param name="TargetDate">加工日</param>
  ''' <param name="TargetEdaban">枝番No</param>
  ''' <param name="flgCompal">原価自動計算必ず実行フラグ</param>
  Private Sub CreateSeisanEdaban(TargetDate As String,
                                 TargetEdaban As String,
                                 flgCompal As Boolean)
    ' 元のカーソルを保持
    Dim preCursor As Cursor = Cursor.Current

    Dim tmpDb As New clsSqlServer
    Dim sqlCount As Long = 0
    Dim Sql As String = String.Empty

    Try
      With tmpDb

        ' カーソルを待機カーソルに変更
        Cursor.Current = Cursors.WaitCursor

        ' 古い枝別精算表データを削除する
        DelOldSeisan()

        sqlEdabanExec(TargetDate, TargetEdaban, tmpDb, flgCompal)

        ' 更新OK
        .TrnCommit()

      End With
    Catch ex As Exception

      Call ComWriteErrLog(ex)

      ' Error ロールバック
      tmpDb.TrnRollBack()
      Throw New Exception("データの更新に失敗しました。")
    Finally
      ' カーソルを元に戻す
      Cursor.Current = preCursor

    End Try

    Exit Sub

  End Sub

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
    Me.Label_GridData.Text = dt.ToString("yyyy年M月d日HH：mm") & " 現在 " & Space(6) & DataCount.ToString() & "件"

    With DG2V1
      If String.IsNullOrWhiteSpace(TxtEdaban_01.Text) Then
        Frame_UnDsp()
      Else
        EDA_JYOUHO_SET()
        'DataGridView1.Focus()
        .Height = SetFrameInDisp(True)
      End If
    End With

  End Sub

  ''' <summary>
  ''' フォーカスの行位置変更指示があった場合、アプリケーションがアイドル状態になったときに呼び出すイベントハンドラ
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

    If (gridRowIdx >= 0) Then
      If forcusChg Is Nothing Then
        forcusChg = e
        AddHandler Application.Idle, AddressOf OnIdle
      End If
    End If

  End Sub

  ''' <summary>
  '''アプリケーションがアイドル状態になった時、フォーカスの行位置変更
  ''' </summary>
  ''' <param name="s"></param>
  ''' <param name="e"></param>
  Private Sub OnIdle(ByVal s As Object, ByVal e As EventArgs)

    If Not (forcusChg Is Nothing) Then
      Try
        DataGridView1.CurrentCell = DataGridView1(DataGridView1.CurrentCell.ColumnIndex, gridRowIdx)
        gridRowIdx = -1
      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      Finally
        forcusChg = Nothing
        RemoveHandler Application.Idle, AddressOf OnIdle
      End Try
    End If

  End Sub

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
      gridHeight = GRID_HEIGHT_MAX
    End If

    Return gridHeight

  End Function

  ''' <summary>
  ''' 枝別情報表示
  ''' </summary>
  Private Sub EDA_JYOUHO_SET()

    Dim numMizuhiki As Double = 0
    Dim numKingaku As Double = 0
    Dim numGenka As Double = 0

    ' 左
    Dim totalLeftJyuryou As Double = 0
    Dim totalLeftUriKingaku As Double = 0
    Dim totalLeftGenka As Double = 0
    Dim numLeftSori As Double = 0
    Dim numLeftSoriRitu As Double = 0
    Dim numLeftMizuhiki As Double = 0
    Dim numLeftKingaku As Double = 0

    '右
    Dim totalRightJyuryou As Double = 0
    Dim totalRightUriKingaku As Double = 0
    Dim totalRightGenka As Double = 0
    Dim numRightSori As Double = 0
    Dim numRightSoriRitu As Double = 0
    Dim numRightMizuhiki As Double = 0
    Dim numRightKingaku As Double = 0

    Dim flgSyokai As Boolean = True
    Dim flgRight As Boolean = False
    Dim flgLeft As Boolean = False

    If (DG2V1.Rows.Count = 0) Then

      ' 枝No
      Label_EdaNo.Text = String.Empty
      ' 個体識別
      Label_KotaiNo.Text = String.Empty
      ' 原産地
      Label_Gensan.Text = String.Empty
      ' 種別
      Label_Syubetu.Text = String.Empty
    Else
      For Each row As DataGridViewRow In DG2V1.Rows

        If (flgSyokai) Then
          flgSyokai = False
          ' 枝No
          Label_EdaNo.Text = row.Cells("EDABAN").Value.ToString
          ' 個体識別
          Label_KotaiNo.Text = row.Cells("KOTAINO").Value.ToString
          Label_KotaiNo.Text = Label_KotaiNo.Text.PadLeft(10, "0"c)
          ' 原産地
          Label_Gensan.Text = row.Cells("GENSAN").Value.ToString
          ' 種別
          Label_Syubetu.Text = row.Cells("SHUB").Value.ToString
          ' 水引重量
          If String.IsNullOrWhiteSpace(row.Cells("MIZUHIKIGO").Value.ToString) Then
            numMizuhiki = 0.0
            numKingaku = 0.0
          Else
            numMizuhiki = CType(row.Cells("MIZUHIKIGO").Value, Double)
            numKingaku = CType(row.Cells("MIZUHIKIGO").Value, Double) * CType(row.Cells("STANKA").Value, Double)
          End If
        End If

        ' 左右対応
        If (CType(row.Cells("SAYU").Value, Integer) = PARTS_SIDE_LEFT) Then
          ' 左
          flgLeft = True
          ' 重量計
          totalLeftJyuryou += CType(row.Cells("HIDARI").Value, Double)
          ' 売金額
          totalLeftUriKingaku += Math.Round(CType(row.Cells("HIDARI").Value, Double) * CType(row.Cells("TANKA").Value, Double))
          ' 原価金額
          totalLeftGenka += Math.Floor(CType(row.Cells("HIDARI").Value, Double) * CType(row.Cells("STANKA2").Value, Double) + 0.5)


          ' 水引重量
          If String.IsNullOrWhiteSpace(row.Cells("MIZUHIKIGO").Value.ToString) Then
            numLeftMizuhiki = 0
            numLeftKingaku = 0.0
          Else
            numLeftMizuhiki = CType(row.Cells("MIZUHIKIGO").Value, Double)
            numLeftKingaku = CType(row.Cells("MIZUHIKIGO").Value, Double) * CType(row.Cells("STANKA").Value, Double)
          End If

        ElseIf (CType(row.Cells("SAYU").Value, Integer) = PARTS_SIDE_RIGHT) Then
          ' 右
          flgRight = True
          ' 重量計
          totalRightJyuryou += CType(row.Cells("HIDARI").Value, Double)
          ' 売金額
          totalRightUriKingaku += Math.Round(CType(row.Cells("HIDARI").Value, Double) * CType(row.Cells("TANKA").Value, Double))
          ' 原価金額
          totalRightGenka += Math.Floor(CType(row.Cells("HIDARI").Value, Double) * CType(row.Cells("STANKA2").Value, Double) + 0.5)

          ' 水引重量
          If String.IsNullOrWhiteSpace(row.Cells("MIZUHIKIGO").Value.ToString) Then
            numRightMizuhiki = 0
            numRightKingaku = 0.0
          Else
            numRightMizuhiki = CType(row.Cells("MIZUHIKIGO").Value, Double)
            numRightKingaku = CType(row.Cells("MIZUHIKIGO").Value, Double) * CType(row.Cells("STANKA").Value, Double)
          End If
        End If
      Next

      ' 粗利
      numLeftSori = totalLeftUriKingaku - numKingaku
      numRightSori = totalRightUriKingaku - numKingaku

      ' 粗利率
      numLeftSoriRitu = Math.Round(numLeftSori / totalLeftUriKingaku * 100, 1, MidpointRounding.AwayFromZero)
      numRightSoriRitu = Math.Round(numRightSori / totalRightUriKingaku * 100, 1, MidpointRounding.AwayFromZero)

    End If

    ' 左
    If (flgLeft) Then
      ' 水引重量
      Labe_LData1.Text = numLeftMizuhiki.ToString("#,##0.0")
      ' 仕入金額
      Labe_LData2.Text = numLeftKingaku.ToString("#,##0")
      ' 原価金額
      Labe_LData3.Text = totalLeftGenka.ToString("#,##0")
      ' 重量計
      Labe_LData4.Text = totalLeftJyuryou.ToString("#,##0.0")
      ' 売金額
      Labe_LData5.Text = totalLeftUriKingaku.ToString("#,##0")
      ' 粗利
      Labe_LData6.Text = numLeftSori.ToString("#,##0")
      ' 粗利率
      Labe_LData7.Text = numLeftSoriRitu.ToString("#,##0.0")
    Else
      Labe_LData1.Text = String.Empty
      ' 仕入金額
      Labe_LData2.Text = String.Empty
      ' 原価金額
      Labe_LData3.Text = "0"
      ' 重量計
      Labe_LData4.Text = "0"
      ' 売金額
      Labe_LData5.Text = "0"
      ' 粗利
      Labe_LData6.Text = String.Empty
      ' 粗利率
      Labe_LData7.Text = String.Empty

    End If

    ' 右
    If (flgRight) Then
      ' 水引重量
      Labe_RData1.Text = numRightMizuhiki.ToString("#,##0.0")
      ' 仕入金額
      Labe_RData2.Text = numRightKingaku.ToString("#,##0")
      ' 原価金額
      Labe_RData3.Text = totalRightGenka.ToString("#,##0")
      ' 重量計
      Labe_RData4.Text = totalRightJyuryou.ToString("#,##0.0")
      '売金額
      Labe_RData5.Text = totalRightUriKingaku.ToString("#,##0")
      ' 粗利
      Labe_RData6.Text = numRightSori.ToString("#,##0")
      ' 粗利率
      Labe_RData7.Text = numRightSoriRitu.ToString("#,##0.0")
    Else
      ' 水引重量
      Labe_RData1.Text = String.Empty
      ' 仕入金額
      Labe_RData2.Text = String.Empty
      ' 原価金額
      Labe_RData3.Text = "0"
      ' 重量計
      Labe_RData4.Text = "0"
      '売金額
      Labe_RData5.Text = "0"
      ' 粗利
      Labe_RData6.Text = String.Empty
      ' 粗利率
      Labe_RData7.Text = String.Empty
    End If

  End Sub

  ''' <summary>
  ''' 枝別精算表印刷処理（出力ワークの作成とレポートの表示）
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function printEdaSeisan() As Boolean

    Dim tmpDb As New clsReport(clsGlobalData.REPORT_FILENAME)

    ' 実行
    With tmpDb

      Try
        ' SQL文の作成
        .Execute("DELETE FROM WK_EDASEISAN")

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("ワークテーブルの削除に失敗しました")

      End Try

      Try
        ' トランザクション開始
        .TrnStart()

        'グリッドから追加SQL文を作成
        For Each item As Dictionary(Of String, String) In Controlz(DataGridView1.Name).GetAllData
          .Execute(SqlInsEdaSeisan("WK_EDASEISAN", item))
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

    ' ACCESSの枝別精算表レポートを表示
    ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_EDASEISAN")

    Return True

  End Function


#End Region

#End Region

#Region "イベントプロシージャ"
  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>to
  ''' <param name="e"></param>
  Private Sub Form_EdaSeisan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = "枝別精算表"

    '最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

    Call InitForm02()

    ' グリッドダブルクリック時処理追加
    '    Controlz(DG2V1.Name).lcCallBackCellDoubleClick = AddressOf Dgv1CellDoubleClick 'ダブルクリック時イベント設定

    ' グリッド再表示時処理
    Controlz(DG2V1.Name).lcCallBackReLoadData = AddressOf DgvReload

    ' 加工日のコンボボックスを先頭に設定
    CmbDateProcessing_01.SelectedIndex = 0

    ' 加工日のコンボボックスは未入力不可
    CmbDateProcessing_01.AvailableBlank = True

    Controlz(DG2V1.Name).AutoSearch = True

    Controlz(DG2V1.Name).SetMsgLabelText("仕入単価、原価、売単価を修正をする場合は、直接明細に入力してＥｎｔｅｒキーをしてください。")

    Controlz(DG2V1.Name).SortActive = False

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)
    lblInformation.Text = String.Empty

    ' ボタンのテキスト設定
    ' 終了ボタン
    ButtonEnd.Text = "F12：終了"
    ' 印刷ボタン
    ButtonPrint.Text = "F5：印刷"
    ' 原価自動計算ボタン
    ButtonGenkaJidou.Text = "F8：原価" & vbCrLf & "自動計算"

    ' 検証実行の有無設定
    ' 初期化ボタン
    Button_Init.CausesValidation = False
    ' 終了ボタン
    ButtonEnd.CausesValidation = False
    ' 印刷ボタン
    ButtonPrint.CausesValidation = False
    ' 原価自動計算ボタン
    ButtonGenkaJidou.CausesValidation = False

    ' IPC通信起動
    InitIPC(PRG_ID)

    ' グリッドノ選択状況を隠す
    '    DG2V1.DefaultCellStyle.SelectionBackColor = DG2V1.DefaultCellStyle.BackColor
    '    DG2V1.DefaultCellStyle.SelectionForeColor = DG2V1.DefaultCellStyle.ForeColor

    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Me.CmbDateProcessing_01

    ' 積算一時テーブル名作成
    tmpSeisanTblName = "#tmpSEISAN" & GetIPv4()

    ' 非表示 → 表示時処理設定
    MyBase.lcCallBackShowFormLc = AddressOf ReStartPrg
  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_EdaSeisan_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      ' F5キー押下時
      Case Keys.F5
        ' 印刷ボタン押下処理
        Me.ButtonPrint.Focus()
        Me.ButtonPrint.PerformClick()
      ' F8キー押下時
      Case Keys.F8
        ' 原価自動計算ボタン押下処理
        Me.ButtonGenkaJidou.Focus()
        Me.ButtonGenkaJidou.PerformClick()
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        Me.ButtonEnd.Focus()
        Me.ButtonEnd.PerformClick()
    End Select

  End Sub

  ''' <summary>
  ''' 加工日でTABを押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbDateProcessing_01_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles CmbDateProcessing_01.PreviewKeyDown

    Select Case e.KeyCode
     'Tabキーが押された時に本来の機能であるフォーカスの移動を無効にして、KeyDown、KeyUpイベントが発生させる
      Case Keys.Tab
        e.IsInputKey = True
    End Select

  End Sub

  ''' <summary>
  ''' 加工日でENTERを押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbDateProcessing_01_KeyDown(sender As Object, e As KeyEventArgs) Handles CmbDateProcessing_01.KeyDown

    Try
      Select Case e.KeyCode
        Case Keys.Enter, Keys.Tab
          ' 加工日が入力済みの場合
          If IsDate(CmbDateProcessing_01.Text) Then

            If String.IsNullOrWhiteSpace(TxtEdaban_01.Text) Then

              ' 枝番・個体識別番号で原価未設定のレコードが3レコード以上存在する場合、原価自動計算機能を実行
              Call CreateSeisan(CmbDateProcessing_01.Text)
            Else
              ' 原価自動計算処理実行（枝番指定）
              Call CreateSeisanEdaban(CmbDateProcessing_01.Text, TxtEdaban_01.Text.Trim, False)
            End If

            ' データグリッド再描画
            DataGrid_ShowList()

          End If
      End Select

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' 枝No.でENTERを押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtEdaban_01_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtEdaban_01.KeyDown

    Try
      If e.KeyCode = Keys.Enter Then
        DataGrid_ShowList()
        DataGridView1.Focus()
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' 原価自動計算ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonGenkaJidou_Click(sender As Object, e As EventArgs) Handles ButtonGenkaJidou.Click

    Try
      DataGrid_ShowList()

      ' 原価自動計算時は枝番号の指定を必須とする
      If String.IsNullOrWhiteSpace(TxtEdaban_01.Text) Then

        clsCommonFnc.ComMessageBox("原価自動計算時は必ず枝Noの指定を行って下さい。",
                                 "枝別精算表",
                                 typMsgBox.MSG_NORMAL,
                                 typMsgBoxButton.BUTTON_OK)
        TxtEdaban_01.Focus()
        Exit Sub
      End If

      ' 対象のデータが存在しない場合は処理を実行しない
      If (dataGridCount = 0) Then

        ' 更新失敗
        clsCommonFnc.ComMessageBox("対象データがありません。",
                           "枝別精算表",
                           typMsgBox.MSG_NORMAL,
                           typMsgBoxButton.BUTTON_OK)

        TxtEdaban_01.Focus()
      Else

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

        Dim rtn As typMsgBoxResult
        rtn = clsCommonFnc.ComMessageBox("原価自動計算を行いますか？" & vbCrLf & "入力されている原価を消して再計算します",
                                     "枝別精算表",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OKCANCEL)

        ' 確認メッセージボックスで、ＯＫボタン選択時
        If (rtn = typMsgBoxResult.RESULT_OK) Then

          ' パスワード入力画面
          Dim tmpSubForm As New Form_PassWordInput

          Try
            ' 未検索時の場合
            If (Controlz(DG2V1.Name).SelectedRow) Is Nothing Then
              Exit Sub
            End If

            ' パスワード入力画面表示
            Select Case tmpSubForm.ShowSubForm(Controlz(DG2V1.Name).SelectedRow, Me)
              Case SFBase.typSfResult.SF_OK

                ' 原価自動計算処理実行（枝番指定）
                Call CreateSeisanEdaban(CmbDateProcessing_01.Text, TxtEdaban_01.Text.Trim, True)

                ' データグリッド再描画
                DataGrid_ShowList()

                TxtEdaban_01.Focus()

              Case SFBase.typSfResult.SF_CANCEL
            End Select

          Catch ex As Exception
            Call ComWriteErrLog(ex, False)
          End Try

        End If
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' 初期化ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_Init_Click(sender As Object, e As EventArgs) Handles Button_Init.Click

    Try

      DataGrid_ShowList()

      ' 加工日未入力時は処理を抜ける
      If String.IsNullOrWhiteSpace(CmbDateProcessing_01.Text) Then
        clsCommonFnc.ComMessageBox("初期化時は必ず加工日の指定を行って下さい。",
                                 "枝別精算表",
                                 typMsgBox.MSG_NORMAL,
                                 typMsgBoxButton.BUTTON_OK)
        ButtonPrint.Focus()
        Exit Sub
      End If

      ' 原価自動計算時は枝番号の指定を必須とする
      If String.IsNullOrWhiteSpace(TxtEdaban_01.Text) Then


        clsCommonFnc.ComMessageBox("初期化時は必ず枝Noの指定を行って下さい。",
                                 "枝別精算表",
                                 typMsgBox.MSG_NORMAL,
                                 typMsgBoxButton.BUTTON_OK)
        TxtEdaban_01.Focus()
        Exit Sub
      End If

      ' 対象のデータが存在しない場合は処理を実行しない
      If (dataGridCount = 0) Then

        ' 更新失敗
        clsCommonFnc.ComMessageBox("対象データがありません。",
                           "枝別精算表",
                           typMsgBox.MSG_NORMAL,
                           typMsgBoxButton.BUTTON_OK)

        TxtEdaban_01.Focus()
      Else

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

        ' パスワード入力画面
        Dim tmpSubForm As New Form_PassWordInput

        ' パスワード入力画面表示
        Select Case tmpSubForm.ShowSubForm(Controlz(DG2V1.Name).SelectedRow, Me)
          Case SFBase.typSfResult.SF_OK

            ' 表示中データのバックアップ
            Call BackupSeisan()

            ' 表示中の枝番データ削除後、原価自動計算機能を実行
            Call DelToCreateSeisan(dataGridCount, CmbDateProcessing_01.Text, TxtEdaban_01.Text.Trim)

            'データグリッド再描画
            DataGrid_ShowList()

            TxtEdaban_01.Focus()

          Case SFBase.typSfResult.SF_CANCEL
        End Select
      End If

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

    Try

      Dim rtn As typMsgBoxResult

      ' データグリッドが０件の場合、処理を行わない
      If (DG2V1.Rows.Count < 1) Then
        Return
      End If

      ' 印刷プレビューの表示有無 
      If clsGlobalData.PRINT_PREVIEW = 1 Then
        rtn = clsCommonFnc.ComMessageBox("枝別精算表の画面表示を行います？",
                                       "枝別精算表",
                                       typMsgBox.MSG_NORMAL,
                                       typMsgBoxButton.BUTTON_OKCANCEL)
      Else
        rtn = clsCommonFnc.ComMessageBox("印刷を行います。よろしいですか？",
                                       "枝別精算表",
                                       typMsgBox.MSG_NORMAL,
                                       typMsgBoxButton.BUTTON_OKCANCEL)
      End If

      ' 確認メッセージボックスで、ＯＫボタン選択時
      If rtn = typMsgBoxResult.RESULT_OK Then

        '印刷ボタン非表示
        ButtonPrint.Enabled = False

        Try
          printEdaSeisan()
        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        End Try

        '印刷ボタン再表示
        ButtonPrint.Enabled = True

      Else

        CmbDateProcessing_01.Focus()

      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' アプリケーション終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ButtonEnd_Click(sender As Object, e As EventArgs) Handles ButtonEnd.Click

    ' 自動検索OFF
    Controlz(DG2V1.Name).AutoSearch = False
    Controlz(DG2V1.Name).ResetPosition()

    MyBase.AllClear()
    Controlz(DG2V1.Name).InitSort()

    ' 加工日のコンボボックスを更新
    CmbDateProcessing_01.InitCmb()

    ' 加工日のコンボボックスを先頭に設定
    CmbDateProcessing_01.SelectedIndex = 0
    Me.CmbDateProcessing_01.Focus()
    Me.Hide()

    Controlz(DG2V1.Name).AutoSearch = True
    DG2V1.Visible = True
  End Sub

  ''' <summary>
  ''' データグリッドフォーカス選択時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter

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

  End Sub

  ''' <summary>
  ''' 画面再表示時処理
  ''' </summary>
  ''' <remarks>
  ''' 非表示→表示時に実行
  ''' FormLoad時に設定
  ''' </remarks>
  Private Sub ReStartPrg()
    Me.CmbDateProcessing_01.InitCmb()
  End Sub
#End Region

#Region "SQL関連"

  ''' <summary>
  ''' 精算テーブルから、13ｶ月前の加工日を削除するSQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInitDelSeisan() As String

    Dim sql As String = String.Empty
    Dim dt As DateTime = DateTime.Parse(ComGetProcTime())

    sql &= " DELETE SEISAN "
    sql &= " WHERE KAKOUBI <= '" & DateAdd(DateInterval.Month, -13, dt).ToString("yyyy/MM/dd") & "'"

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' 任意の枝別精算表データを削除するSQL文の作成
  ''' </summary>
  ''' <param name="ProcessingData">加工日</param>
  ''' <param name="Edaban">枝番No</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelSeisan(ProcessingData As String _
                              , Edaban As String) As String

    Dim sql As String = String.Empty

    sql &= " DELETE SEISAN "
    sql &= " WHERE KAKOUBI = '" & ProcessingData & "'"
    sql &= "   AND EDABAN =  " & Edaban

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' 精算テーブル件数取得SQL文作成
  ''' </summary>
  ''' <param name="prmSelectedRow">編集前の値</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetSeisanCount(prmSelectedRow As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty

    sql &= " SELECT COUNT(*) AS SEISAN_COUNT FROM SEISAN"
    sql &= " WHERE EDABAN =    " & prmSelectedRow("EDABAN")
    sql &= "   AND KOTAINO =   " & prmSelectedRow("KOTAINO")
    sql &= "   AND SAYU =      " & prmSelectedRow("SAYU")

    Return sql

  End Function

  ''' <summary>
  ''' 精算一時テーブル作成SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSetTmpSeisan() As String

    Dim sql As String = String.Empty

    sql &= " DROP TABLE IF EXISTS " & tmpSeisanTblName & ";"

    sql &= " CREATE TABLE " & tmpSeisanTblName
    sql &= "      (EDABAN  numeric(7,0) NOT NULL,"
    sql &= "       KOTAINO nvarchar(10) NOT NULL,"
    sql &= "       SAYU    numeric(1,0) NOT NULL,"
    sql &= "       BUICODE numeric(4,0) NOT NULL);"

    Return sql

  End Function

  ''' <summary>
  ''' 精算テーブル取得SQL文作成
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetSeisan(TargetDate As String) As String

    Dim sql As String = String.Empty
    Dim dt As DateTime = DateTime.Parse(TargetDate)

    sql &= " INSERT INTO " & tmpSeisanTblName & "(EDABAN, KOTAINO, SAYU, BUICODE)"
    sql &= " SELECT  EDABAN, KOTAINO, SAYU, BUICODE"
    sql &= " FROM SEISAN SEI1"

    ' 原価未設定のレコードが3レコード判定SQL文作成
    sql &= SqlCountStanka2()

    sql &= "   AND SEI1.KAKOUBI >= '" & TargetDate & "'"
    sql &= "   AND SEI1.KAKOUBI <= '" & DateAdd(DateInterval.Day, 3, dt).ToString("yyyy/MM/dd") & "'"
    sql &= " ORDER BY KAKOUBI, EDABAN, BUICODE"

    Return sql

  End Function

  ''' <summary>
  ''' 枝番精算テーブル取得SQL文作成
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="flgCompal">原価自動計算必ず実行フラグ</param>
  ''' <param name="TargetEdaban">枝番No</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetSeisanEdaban(TargetDate As String,
                                      flgCompal As Boolean,
                                      TargetEdaban As String) As String

    Dim sql As String = String.Empty
    Dim dt As DateTime = DateTime.Parse(TargetDate)

    sql &= " INSERT INTO " & tmpSeisanTblName & "(EDABAN, KOTAINO, SAYU, BUICODE)"
    sql &= " SELECT  EDABAN, KOTAINO, SAYU, BUICODE"
    sql &= " FROM SEISAN SEI1"

    If (flgCompal) Then
      sql &= " WHERE 1=1 "
    Else
      ' 原価未設定のレコードが3レコード判定SQL文作成
      sql &= SqlCountStanka2()
    End If

    sql &= "   AND SEI1.KAKOUBI >= '" & TargetDate & "'"
    sql &= "   AND SEI1.KAKOUBI <= '" & DateAdd(DateInterval.Day, 3, dt).ToString("yyyy/MM/dd") & "'"
    sql &= "   AND SEI1.EDABAN = " & TargetEdaban
    sql &= " ORDER BY KAKOUBI, EDABAN, BUICODE"


    Return sql

  End Function

  ''' <summary>
  ''' 精算テーブル更新SQL文作成
  ''' </summary>
  ''' <param name="prmEditData">編集された値</param>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <param name="prmAllFlg">True：編集されたレコードと枝番・個体識別番号・左右が同一のレコードも同時に更新
  ''' 　　　　　　　　　　　  False：編集されたレコードのみを更新</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdSeisan(ProcDate As String _
                              , prmEditData As Dictionary(Of String, String) _
                              , prmSelectedRow As Dictionary(Of String, String) _
                              , prmAllFlg As Boolean) As String

    Dim sql As String = String.Empty

    Dim setTxt As String = prmEditData(prmEditData.Keys(0))
    ' 入力値が空白判定
    If String.IsNullOrWhiteSpace(setTxt) Then
      ' 入力値が空白の場合は0を代入
      setTxt = "0"
    End If

    sql &= " UPDATE SEISAN "
    sql &= " SET " & prmEditData.Keys(0) & " =" & setTxt
    sql &= "      ,KDATE =      '" & ProcDate & "'"
    sql &= " WHERE EDABAN =    " & prmSelectedRow("EDABAN")
    sql &= "   AND KOTAINO =   " & prmSelectedRow("KOTAINO")
    sql &= "   AND SAYU =      " & prmSelectedRow("SAYU")
    If prmAllFlg = False Then
      sql &= "   AND BUICODE =      " & prmSelectedRow("BUICODE")
    End If

    sql &= "   AND KDATE <=   '" & dataGridDate & "'"

    Return sql

  End Function

  ''' <summary>
  ''' CUTJ検索用SQL文作成
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelCutJ(TargetDate As String _
                            , ProcDate As String) As String

    Dim sql As String = String.Empty
    Dim dt As DateTime = DateTime.Parse(TargetDate)

    sql &= " SELECT CUTJ.EBCODE"
    sql &= "      , CUTJ.KOTAINO"
    sql &= "      , CUTJ.SAYUKUBUN"
    sql &= "      , CUTJ.BICODE"
    sql &= "      , ISNULL(BUIM.BINAME,'未登録:' + STR(CUTJ.BICODE)) AS BUINAME"
    sql &= "      , MAX(CUTJ.TKCODE) AS SHIMUKESAKICODE"
    sql &= "      , ISNULL(KAKU.KZNAME,'未登録:' + STR(CUTJ.KAKUC)) AS KZNAME"
    sql &= "      , ISNULL(GENSN.GNNAME,'未登録:' + STR(CUTJ.GENSANCHIC)) AS GNNAME"
    sql &= "      , ISNULL(KIKA.KKNAME,'未登録:' + STR(CUTJ.KIKAKUC)) AS KKNAME"
    sql &= "      , MIN(CUTJ.KAKOUBI) AS KAKOUBI"
    sql &= "      , COUNT(CUTJ.BICODE) AS HAKOSU"
    sql &= "      , SUM(IIF(CUTJ.HONSU = 0,1,CUTJ.HONSU)) AS HONSU"
    sql &= "      , CONVERT(DECIMAL(10,1), SUM(FLOOR(CUTJ.JYURYO / 100) / 10.0)) AS JYURYOK"
    sql &= "      , MAX(CUTJ.TANKA) AS TANKA"
    sql &= "      , MAX(CUTJ.SHOHINC) AS SHOHINC"
    sql &= "      , ISNULL(MAX(BUIM.BUDOMARI),MAX(CUTJ.BUDOMARI) ) AS BUDOMARI"
    sql &= "      , MAX(EDA_INFO.EDC) AS EDC"
    sql &= "      , IIF(CUTJ.SAYUKUBUN = 1"
    sql &= "              ,CONVERT(DECIMAL(10,1),MAX(FLOOR(IIF(EDA_INFO.JYURYO1 = 0 AND EDA_INFO.JYURYO2 = 0, EDA_INFO.JYURYO / 2, EDA_INFO.JYURYO1) / 100) / 10.0)) "
    sql &= "              ,CONVERT(DECIMAL(10,1),MAX(FLOOR(IIF(EDA_INFO.JYURYO1 = 0 AND EDA_INFO.JYURYO2 = 0, EDA_INFO.JYURYO / 2, EDA_INFO.JYURYO2) / 100) / 10.0)) ) AS MIZUHIKIGO "
    sql &= "      , ISNULL(MAX(EDA_INFO.SIIREGAKU),0) AS SIIREGAKU"
    sql &= "      , MAX(EDA_INFO.SIIREBI) AS SIIREBI"
    sql &= "      , 0 AS ITTOU "

    sql &= " FROM (((("
    sql &= "       CUTJ"
    sql &= "         LEFT JOIN BUIM                   ON CUTJ.BICODE = BUIM.BICODE   AND BUIM.KUBUN <> 9)"
    sql &= "         LEFT JOIN KAKU                   ON CUTJ.KAKUC = KAKU.KKCODE    AND KAKU.KUBUN <> 9)"
    sql &= "         LEFT JOIN GENSN                  ON CUTJ.GENSANCHIC = GENSN.GNCODE AND GENSN.KUBUN <> 9)"
    sql &= "         LEFT JOIN KIKA                   ON CUTJ.KIKAKUC = KIKA.KICODE AND KIKA.KUBUN <> 9)"
    sql &= "         LEFT " & SqlSelJoinEdab("CUTJ.EBCODE", ProcDate)
    sql &= " WHERE CUTJ.KUBUN = 1 "
    sql &= "   AND (CUTJ.NSZFLG <= 3 OR CUTJ.NSZFLG = 99)"
    sql &= "   AND CUTJ.LABELJI > 0 "
    sql &= "   AND CUTJ.KIKAINO <> 999"
    sql &= "   AND ISDATE(CUTJ.HENPINBI) <> 1"
    sql &= "   AND CUTJ.KOTAINO > 0 "
    sql &= "   AND CUTJ.EBCODE > 0"

    ' 特定の得意先コードと商品コードを除外もしくは内包するSQL文作成
    sql &= SetSqlTkCodeHani()

    ' 加工賃部位コードは対象から除外する
    sql &= SetSqlWageCode()

    sql &= "   AND CUTJ.KAKOUBI >= '" & TargetDate & "'"
    sql &= "   AND CUTJ.KAKOUBI <= '" & DateAdd(DateInterval.Day, 3, dt).ToString("yyyy/MM/dd") & "'"
    sql &= " GROUP BY CUTJ.EBCODE"
    sql &= "        , CUTJ.KOTAINO"
    sql &= "        , CUTJ.SAYUKUBUN"
    sql &= "        , CUTJ.BICODE"
    sql &= "        , BUIM.BINAME"
    sql &= "        , CUTJ.KAKUC"
    sql &= "        , KAKU.KZNAME"
    sql &= "        , CUTJ.GENSANCHIC"
    sql &= "        , GENSN.GNNAME"
    sql &= "        , CUTJ.KIKAKUC"
    sql &= "        , KIKA.KKNAME"

    Return sql

  End Function


  ''' <summary>
  ''' 精算テーブル更新用SQL文作成（CUTJテーブル検索結果）
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutJToSeisan(TargetDate As String _
                                    , ProcDate As String) As String

    Dim sql As String = String.Empty

    sql &= " UPDATE SEISAN "
    sql &= " SET HONSU      = SRC.HONSU"
    sql &= "   , HAKOSU     = SRC.HAKOSU"
    sql &= "   , MIZUHIKIGO = SRC.MIZUHIKIGO"
    sql &= "   , HIDARI     = SRC.JYURYOK"
    sql &= "   , TANKA      = SRC.TANKA"
    sql &= "   , BUDOMARI   = SRC.BUDOMARI"
    sql &= "   , SHOHINC    = SRC.SHOHINC"

    sql &= "   , SIIREBI    = SRC.SIIREBI"
    sql &= "   , KDATE      = '" & ProcDate & "'"
    sql &= " FROM SEISAN"
    sql &= "       INNER JOIN ("
    sql &= SqlSelCutJ(TargetDate, ProcDate)
    sql &= "       ) SRC ON SEISAN.EDABAN = SRC.EBCODE"
    sql &= "            AND SEISAN.KOTAINO = SRC.KOTAINO"
    sql &= "            AND SEISAN.SAYU = SRC.SAYUKUBUN "
    sql &= "            AND SEISAN.BUICODE = SRC.BICODE "

    sql &= " WHERE 1=1  "

    Return sql

  End Function

  ''' <summary>
  ''' 精算テーブルの原価自動設定SQL文作成
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <param name="TargetEdaban">枝番No</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCalcCostBase(TargetDate As String _
                                    , ProcDate As String _
                                    , TargetEdaban As String) As String

    ' 左右別に以下の値を原価として設定する
    ' 枝肉重量 x 枝肉仕入原価(編集可)  / 製造重量（編集可）
    '  原価         ：SEISAN.STANKA2
    '  枝肉重量     ：JYURYO1 or JYURYO2
    '  枝肉仕入原価 ：SEISAN.STANKA
    '  製造重量     ：SEISAN.HIDARI(枝番・左右単位合計)
    Dim sql As String = String.Empty

    Dim dt As DateTime = DateTime.Parse(TargetDate)

    sql &= " UPDATE SEISAN"
    If String.IsNullOrWhiteSpace(TargetEdaban) Then
      sql &= " SET STANKA2 = IIF(STANKA2 = 0, COST.COST,  STANKA2)"
    Else
      sql &= " SET STANKA2 = COST.COST"
    End If

    sql &= "   , KDATE   = '" & ProcDate & "'"
    sql &= " FROM SEISAN AS SEI1"
    sql &= "       INNER JOIN (SELECT SRC.EDABAN"
    sql &= "                        , SRC.SAYU"
    sql &= "                        , IIF(SRC.JYURYOK <> 0"
    sql &= "                        , FLOOR(IIF(SRC.SAYU = 1"
    sql &= "                           ,IIF(EDA_INFO.JYURYO1 = 0 AND EDA_INFO.JYURYO2 = 0, "
    sql &= "                            (FLOOR(EDA_INFO.JYURYO / 2 / 100) / 10.0 ) * SRC.SIIREGAKU, "
    sql &= "                            (FLOOR(EDA_INFO.JYURYO1 / 100) / 10.0 ) * SRC.SIIREGAKU)"
    sql &= "                           ,IIF(EDA_INFO.JYURYO1 = 0 And EDA_INFO.JYURYO2 = 0, "
    sql &= "                            (FLOOR(EDA_INFO.JYURYO / 2 / 100) / 10.0 ) * SRC.SIIREGAKU, "
    sql &= "                            (FLOOR(EDA_INFO.JYURYO2 / 100) / 10.0 ) * SRC.SIIREGAKU)) / SRC.JYURYOK  + 0.99"
    sql &= "                           ) ,0"
    sql &= "                          ) AS COST"
    sql &= "                   FROM ( "
    sql &= "                         (SELECT SEI2.EDABAN"
    sql &= "                               , SEI2.KOTAINO"
    sql &= "                               , SEI2.SAYU"
    sql &= "                               , MAX(STANKA) AS SIIREGAKU"
    sql &= "                               , SUM(SEI2.HIDARI) AS JYURYOK"
    sql &= "                          FROM SEISAN AS SEI2"
    sql &= "                          WHERE SEI2.KAKOUBI >= '" & TargetDate & "'"
    sql &= "                            AND SEI2.KAKOUBI <= '" & DateAdd(DateInterval.Day, 3, dt).ToString("yyyy/MM/dd") & "'"
    sql &= "                          GROUP BY SEI2.EDABAN"
    sql &= "                                 , SEI2.KOTAINO"
    sql &= "                                 , SEI2.SAYU ) AS SRC "
    sql &= "                          INNER " & SqlSelJoinEdab("SRC.EDABAN", ProcDate) & ")) AS COST "
    sql &= "            ON  SEI1.EDABAN = COST.EDABAN"
    sql &= "            AND SEI1.SAYU = COST.SAYU"


    Return sql

  End Function


  ''' <summary>
  ''' 精算テーブルの原価自動設定SQL文作成(原価未設定のレコードが3レコード判定)
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCalcCost(TargetDate As String _
                                , ProcDate As String) As String

    Dim sql As String = String.Empty

    ' 精算テーブルの原価自動設定SQL文作成
    sql &= SqlUpdCalcCostBase(TargetDate, ProcDate, "")

    ' 原価未設定のレコードが3レコード判定SQL文作成
    sql &= SqlCountStanka2()

    Return sql

  End Function

  ''' <summary>
  ''' 原価未設定のレコードが3レコード判定SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlCountStanka2() As String

    Dim sql As String = String.Empty

    sql &= " WHERE EXISTS( SELECT COUNT(*)  "
    sql &= "               FROM SEISAN AS SEI3"
    sql &= "               WHERE SEI3.EDABAN  = SEI1.EDABAN "
    sql &= "                 AND SEI3.SAYU    = SEI1.SAYU  "
    sql &= "                 AND SEI3.STANKA2 = 0  "
    sql &= "                 GROUP BY EDABAN, SAYU  "
    sql &= "                 HAVING COUNT(*) > 2) "

    Return sql

  End Function
 _

  ''' <summary>
  ''' 枝別精算テーブルの原価自動設定SQL文作成
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <param name="flgCompal">原価自動計算必ず実行フラグ</param>
  ''' <param name="TargetEdaban">枝番No</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCalcCostEdaban(TargetDate As String _
                              , ProcDate As String _
                              , flgCompal As Boolean _
                              , TargetEdaban As String) As String

    Dim sql As String = String.Empty

    ' 精算テーブルの原価自動設定SQL文作成
    sql &= SqlUpdCalcCostBase(TargetDate, ProcDate, TargetEdaban)

    If (flgCompal = False) Then

      ' 原価未設定のレコードが3レコード判定SQL文作成
      sql &= SqlCountStanka2()

      If String.IsNullOrWhiteSpace(TargetEdaban) Then
        sql &= " AND 1=1"
      Else
        sql &= " AND SEI1.EDABAN = " & TargetEdaban
      End If
    Else
      If String.IsNullOrWhiteSpace(TargetEdaban) Then
        sql &= " WHERE 1=1"
      Else
        sql &= " WHERE SEI1.EDABAN = " & TargetEdaban
      End If
    End If


    Return sql

  End Function

  ''' <summary>
  ''' 精算テーブルの得意先コード、売上日を更新する
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCode(TargetDate As String _
                              , ProcDate As String) As String

    Dim sql As String = String.Empty

    Dim dt As DateTime = DateTime.Parse(TargetDate)

    sql &= " UPDATE SEISAN"
    sql &= " SET UTCODE  = SRC.UTKCODE"
    sql &= "   , URIAGEBI  = SRC.SYUKKABI"
    sql &= "   , KDATE   = '" & ProcDate & "'"
    sql &= " FROM SEISAN "
    sql &= "       INNER JOIN ("

    sql &= "        SELECT * FROM CUTJ"
    sql &= "        WHERE CUTJ.KAKOUBI >= '" & TargetDate & "'"
    sql &= "          AND CUTJ.KAKOUBI <= '" & DateAdd(DateInterval.Day, 3, dt).ToString("yyyy/MM/dd") & "'"

    sql &= "          AND CUTJ.KUBUN = 1 "
    sql &= "          AND (CUTJ.NSZFLG = 2)"
    sql &= "          AND CUTJ.LABELJI > 0 "
    sql &= "          AND CUTJ.KIKAINO <> 999"
    sql &= "          AND ISDATE(CUTJ.HENPINBI) <> 1"
    sql &= "          AND CUTJ.KOTAINO > 0 "
    sql &= "          AND CUTJ.EBCODE > 0"

    ' 特定の得意先コードと商品コードを除外もしくは内包するSQL文作成
    sql &= SetSqlTkCodeHani()

    ' 加工賃部位コードは対象から除外する
    sql &= SetSqlWageCode()

    sql &= "       ) SRC ON SEISAN.EDABAN = SRC.EBCODE"
    sql &= "            AND SEISAN.KOTAINO = SRC.KOTAINO"
    sql &= "            AND SEISAN.SAYU = SRC.SAYUKUBUN "
    sql &= "            AND SEISAN.BUICODE = SRC.BICODE "

    sql &= " WHERE 1=1"

    Return sql

  End Function


  ''' <summary>
  ''' 精算テーブルの仕向先名を更新する
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdSimukeName(TargetDate As String _
                              , ProcDate As String) As String

    Dim sql As String = String.Empty

    Dim dt As DateTime = DateTime.Parse(TargetDate)

    sql &= " UPDATE SEISAN"
    sql &= " SET TNAME = ISNULL(SIMUKE.LTKNAME,'未登録:' + STR(SEISAN.TCODE))"
    sql &= "   , KDATE   = '" & ProcDate & "'"
    sql &= " FROM SEISAN "
    sql &= " LEFT JOIN TOKUISAKI AS SIMUKE ON SEISAN.TCODE = SIMUKE.TKCODE AND SIMUKE.KUBUN <> 9 "
    sql &= " WHERE SEISAN.KAKOUBI >= '" & TargetDate & "'"
    sql &= "   AND SEISAN.KAKOUBI <= '" & DateAdd(DateInterval.Day, 3, dt).ToString("yyyy/MM/dd") & "'"
    sql &= "   AND SEISAN.TCODE <> 0"

    Return sql

  End Function

  ''' <summary>
  ''' 精算テーブルの得意先名を更新する
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdTokuiName(TargetDate As String _
                              , ProcDate As String) As String

    Dim sql As String = String.Empty

    Dim dt As DateTime = DateTime.Parse(TargetDate)

    sql &= " UPDATE SEISAN"
    sql &= " SET UTNAME = ISNULL(TOKUI.LTKNAME,'未登録:' + STR(SEISAN.UTCODE))"
    sql &= "   , KDATE   = '" & ProcDate & "'"
    sql &= " FROM SEISAN "
    sql &= " LEFT JOIN TOKUISAKI AS TOKUI  ON SEISAN.UTCODE = TOKUI.TKCODE AND TOKUI.KUBUN <> 9 "
    sql &= " WHERE SEISAN.KAKOUBI >= '" & TargetDate & "'"
    sql &= "   AND SEISAN.KAKOUBI <= '" & DateAdd(DateInterval.Day, 3, dt).ToString("yyyy/MM/dd") & "'"
    sql &= " 　AND SEISAN.UTCODE　<> 0"
    sql &= " 　AND SEISAN.URIAGEBI　<> ''"

    Return sql

  End Function

  ''' <summary>
  ''' 枝別情報SQL文作成
  ''' </summary>
  ''' <param name="JoinOn">結合するSQL文</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelJoinEdab(JoinOn As String _
                                , ProcDate As String) As String

    Dim sql As String = String.Empty
    Dim dt As DateTime = DateTime.Parse(ProcDate)

    sql &= " JOIN (SELECT *"
    sql &= "            , ROW_NUMBER() OVER(PARTITION BY EBCODE ORDER BY NYUKOBI DESC) As IndexOrder"
    sql &= "       FROM EDAB"
    sql &= "       WHERE "
    sql &= "       KUBUN <> 9 "
    sql &= " AND EDAB.TDATE   > '" & DateAdd(DateInterval.Month, -13, dt).ToString("yyyy/MM/dd") & "'"
    sql &= " ) AS EDA_INFO "
    sql &= " ON " & JoinOn & " = EDA_INFO.EBCODE "
    sql &= " AND EDA_INFO.IndexOrder = 1 "


    Return sql

  End Function

  ''' <summary>
  ''' 精算テーブル追加用SQL文作成
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsSeisan(TargetDate As String _
                              , ProcDate As String) As String

    Dim sql As String = String.Empty

    sql &= " INSERT INTO SEISAN( EDABAN "
    sql &= "                   , KOTAINO"
    sql &= "                   , SAYU "
    sql &= "                   , BUICODE"
    sql &= "                   , BUIMEI"
    sql &= "                   , TCODE "
    sql &= "                   , KAKU "
    sql &= "                   , GENSAN"
    sql &= "                   , SHUB "
    sql &= "                   , KAKOUBI"
    sql &= "                   , JYOUBAN"
    sql &= "                   , SIIREBI"
    sql &= "                   , STANKA"
    sql &= "                   , ITTOU"
    sql &= "                   , KDATE"

    sql &= " ) "
    sql &= " SELECT EBCODE"
    sql &= "      , KOTAINO"
    sql &= "      , SAYUKUBUN"
    sql &= "      , BICODE"
    sql &= "      , BUINAME"
    sql &= "      , SHIMUKESAKICODE"
    sql &= "      , KZNAME"
    sql &= "      , GNNAME"
    sql &= "      , KKNAME"
    sql &= "      , KAKOUBI"
    sql &= "      , EDC"
    sql &= "      , SIIREBI"
    sql &= "      , SIIREGAKU"
    sql &= "      , ITTOU "
    sql &= "      , '" & ProcDate & "'"
    sql &= " FROM ("
    sql &= SqlSelCutJ(TargetDate, ProcDate)
    sql &= " ) AS SRC "
    sql &= " WHERE NOT EXISTS( SELECT * "
    sql &= "                   FROM SEISAN "
    sql &= "                   WHERE SEISAN.KOTAINO = SRC.KOTAINO "
    sql &= "                     AND SEISAN.BUICODE = SRC.BICODE  "
    sql &= "                     AND SEISAN.SAYU = SRC.SAYUKUBUN  "
    sql &= "                     AND SEISAN.KAKOUBI = SRC.KAKOUBI ) "

    Return sql

  End Function


  ''' <summary>
  ''' 枝別精算テーブル追加用SQL文作成
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <param name="TargetEdaban">枝番No</param> 
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsSeisanEdaban(TargetDate As String _
                            , ProcDate As String _
                            , TargetEdaban As String) As String

    Dim sql As String = String.Empty

    sql &= SqlInsSeisan(TargetDate, ProcDate)
    sql &= " AND SRC.EBCODE = " & TargetEdaban

    Return sql

  End Function


  ''' <summary>
  ''' 精算テーブル追加SQL文作成
  ''' </summary>
  ''' <param name="tblName"></param>
  ''' <param name="prmSelected">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsEdaSeisan(tblName As String, prmSelected As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty


    sql &= " INSERT INTO "
    sql &= tblName
    sql &= "                   ( EDABAN "                     '01:                       
    sql &= "                   , JYOUBAN"                     '02:
    sql &= "                   , KOTAINO "                    '03:
    sql &= "                   , SIIREBI "                    '04:  
    sql &= "                   , KAKOUBI"                     '05:
    sql &= "                   , TCODE"                       '06:
    sql &= "                   , TNAME "                      '07:
    sql &= "                   , BUICODE "                    '08:
    sql &= "                   , BUIMEI"                      '09:
    sql &= "                   , HONSU "                      '10:
    sql &= "                   , HAKOSU"                      '11:
    sql &= "                   , ITTOU"                       '12:
    sql &= "                   , MIZUHIKIGO"                  '13:
    sql &= "                   , STANKA"                      '14:
    sql &= "                   , BUDOMARI"                    '15:
    sql &= "                   , KAKU"                        '16:
    sql &= "                   , SHUB"                        '17:
    sql &= "                   , GENSAN"                      '18:
    sql &= "                   , HIDARI"                      '19:
    sql &= "                   , MIGI"                        '20:
    sql &= "                   , TANKA"                       '21:
    sql &= "                   , UTCODE"                      '22:
    sql &= "                   , UTNAME"                      '23:
    sql &= "                   , URIAGEBI"                    '24:
    sql &= "                   , KDATE"                       '25:
    sql &= "                   , SAYU"                        '26:
    sql &= "                   , ITTOU2"                      '27:
    sql &= "                   , MIZUHIKIGO2"                 '28:
    sql &= "                   , STANKA2"                     '29:
    sql &= "                   , TANKA2"                      '30:
    sql &= "                   , SHOHINC"                     '31:
    sql &= ") VALUES("

    sql &= prmSelected("EDABAN") & ","                        '01:
    If String.IsNullOrWhiteSpace(prmSelected("JYOUBAN")) Then
      sql &= "0,"                                             '02:
    Else
      sql &= prmSelected("JYOUBAN") & ","                     '02:
    End If
    sql &= prmSelected("KOTAINO") & ","                       '03:
    If String.IsNullOrWhiteSpace(prmSelected("SIIREBI")) Then
      sql &= "Null,"                                          '04:
    Else
      sql &= "'" & prmSelected("SIIREBI") & "'" & ","         '04:
    End If
    If String.IsNullOrWhiteSpace(prmSelected("KAKOUBI")) Then
      sql &= "Null,"                                          '05:
    Else
      sql &= "'" & prmSelected("KAKOUBI") & "'" & ","         '05:  
    End If
    sql &= prmSelected("TCODE") & ","                         '06:
    sql &= "'" & prmSelected("TNAME") & "'" & ","             '07:
    sql &= prmSelected("BUICODE") & ","                       '08:
    sql &= "'" & prmSelected("BUIMEI") & "'" & ","            '09:
    sql &= prmSelected("HONSU") & ","                  　     '10:
    sql &= prmSelected("HAKOSU") & ","                        '11:
    sql &= prmSelected("ITTOU") & ","                         '12:
    If String.IsNullOrWhiteSpace(prmSelected("MIZUHIKIGO")) Then
      sql &= "0,"                                             '13:
    Else
      sql &= prmSelected("MIZUHIKIGO") & ","                  '13:
    End If

    sql &= prmSelected("STANKA") & ","                        '14:
    sql &= prmSelected("BUDOMARI") & ","                      '15:
    sql &= "'" & prmSelected("KAKU") & "'" & ","              '16:
    sql &= "'" & prmSelected("SHUB") & "'" & ","              '17:
    sql &= "'" & prmSelected("GENSAN") & "'" & ","            '18:
    sql &= prmSelected("HIDARI") & ","           　           '19:
    sql &= prmSelected("MIGI") & ","               　         '20:
    sql &= prmSelected("TANKA") & ","                         '21:
    sql &= prmSelected("UTCODE") & ","                        '22:
    sql &= "'" & prmSelected("UTNAME") & "'" & ","            '23:
    If String.IsNullOrWhiteSpace(prmSelected("URIAGEBI")) Then
      sql &= "Null,"                                          '24:
    Else
      sql &= "'" & prmSelected("URIAGEBI") & "'" & ","        '24:
    End If
    sql &= "'" & prmSelected("KDATE") & "'" & ","             '25:
    sql &= prmSelected("SAYU") & ","                          '26:
    sql &= prmSelected("ITTOU2") & ","                        '27:
    sql &= prmSelected("MIZUHIKIGO2") & ","                   '28:
    sql &= prmSelected("STANKA2") & ","                       '29:
    sql &= prmSelected("TANKA2") & ","                        '30:
    sql &= prmSelected("SHOHINC") & ")"                       '31:

    Return sql

  End Function


  ''' <summary>
  ''' 実績テーブルの原単価を更新する
  ''' </summary>
  ''' <param name="ProcDate">更新日付</param> 
  ''' <param name="prmSelected">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutjGenka(ProcDate As String _
                                 , prmEditData As Dictionary(Of String, String) _
                                 , prmSelected As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty

    Dim setTxt As String = prmEditData(prmEditData.Keys(0))
    ' 入力値が空白判定
    If String.IsNullOrWhiteSpace(setTxt) Then
      ' 入力値が空白の場合は0を代入
      setTxt = "0"
    End If

    sql &= " UPDATE CUTJ"
    sql &= " SET GENKA  = " & setTxt
    sql &= "   , KDATE  = '" & ProcDate & "'"
    sql &= " FROM CUTJ "
    sql &= " WHERE EBCODE =     " & prmSelected("EDABAN")
    sql &= "   AND KOTAINO =    " & prmSelected("KOTAINO")
    sql &= "   AND SAYUKUBUN =  " & prmSelected("SAYU")
    sql &= "   AND BICODE =     " & prmSelected("BUICODE")

    Return sql

  End Function

  ''' <summary>
  ''' 実績テーブルの原単価を更新する
  ''' 仮入庫対応
  ''' 計量器よりあがってきたデータは原価を設定するまで仮入庫状態(NSZFLG = 99)とし
  ''' 本画面で原価を設定した時点で在庫状態(NSZFLG=0)とする
  ''' </summary>
  ''' <param name="TargetDate">加工日</param> 
  ''' <param name="ProcDate">更新日付</param> 
  ''' <param name="flgCompal">原価自動計算必ず実行フラグ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutjGenka(TargetDate As String _
                                 , ProcDate As String _
                                 , flgCompal As Boolean) As String
    Dim sql As String = String.Empty

    Dim dt As DateTime = DateTime.Parse(TargetDate)

    sql &= " UPDATE CUTJ"
    sql &= " SET GENKA  = SRC.STANKA2"
    sql &= "   , NSZFLG = IIF(CUTJ.NSZFLG = 99, 0, CUTJ.NSZFLG)"
    sql &= "   , KDATE  = '" & ProcDate & "'"
    sql &= " FROM CUTJ "

    sql &= "       INNER JOIN "

    If (flgCompal) Then
      sql &= "       SEISAN"
    Else
      ' 原価未設定のレコードが3レコード判定

      sql &= " ( SELECT * FROM SEISAN AS SEI1  "
      sql &= " WHERE EXISTS( SELECT * FROM " & tmpSeisanTblName & " AS SEI3"
      sql &= "               WHERE SEI3.EDABAN  = SEI1.EDABAN "
      sql &= "                 AND SEI3.KOTAINO = SEI1.KOTAINO  "
      sql &= "                 AND SEI3.SAYU    = SEI1.SAYU   "
      sql &= "                 AND SEI3.BUICODE = SEI1.BUICODE))  "
    End If

    sql &= "        AS SRC"
    sql &= "             ON CUTJ.EBCODE    = SRC.EDABAN "
    sql &= "            AND CUTJ.KOTAINO   = SRC.KOTAINO "
    sql &= "            AND CUTJ.SAYUKUBUN = SRC.SAYU "
    sql &= "            AND CUTJ.BICODE    = SRC.BUICODE "
    sql &= "            AND CUTJ.KAKOUBI   = SRC.KAKOUBI "

    sql &= " WHERE CUTJ.KAKOUBI >= '" & TargetDate & "'"
    sql &= "   AND CUTJ.KAKOUBI <= '" & DateAdd(DateInterval.Day, 3, dt).ToString("yyyy/MM/dd") & "'"

    sql &= "   AND CUTJ.KUBUN = 1 "
    sql &= "   AND (CUTJ.NSZFLG <= 3 OR CUTJ.NSZFLG = 99)"
    sql &= "   AND CUTJ.LABELJI > 0 "
    sql &= "   AND CUTJ.KIKAINO <> 999"
    sql &= "   AND ISDATE(CUTJ.HENPINBI) <> 1"
    sql &= "   AND CUTJ.KOTAINO > 0 "
    sql &= "   AND CUTJ.EBCODE > 0"

    ' 特定の得意先コードと商品コードを除外もしくは内包するSQL文作成
    sql &= SetSqlTkCodeHani()

    ' 加工賃部位コードは対象から除外する
    sql &= SetSqlWageCode()

    Return sql

  End Function

#End Region

End Class

