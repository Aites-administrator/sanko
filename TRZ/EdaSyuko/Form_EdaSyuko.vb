Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsGlobalData

Public Class Form_EdaSyuko
  Implements IDgvForm02

  '----------------------------------------------
  '               枝出庫入力画面
  '
  '
  '----------------------------------------------

#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "edasyuko"
  Private Const PRG_TITLE As String = "枝出庫処理"
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
#End Region

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_EdaSyuko, AddressOf ComRedisplay)
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

    '------------------------------
    '       枝出荷明細設定
    '------------------------------
    DG2V1 = Me.DataGridView1

    ' グリッド初期化
    Call InitGrid(DG2V1 _
                  , CreateGrid2Src1() _
                  , CreateGrid2Layout1())

    With DG2V1

      '表示する
      .Visible = True

      ' グリッド動作設定
      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 3

        ' 検索コントロール設定
        '.AddSearchControl([コントロール], "絞り込み項目名", [絞り込み方法], [絞り込み項目の型])
        .AddSearchControl(Me.TxtDateOutStock, "SYUKKABI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        '.AddSearchControl(Me.TxKotaiNo1, "KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' 編集可能列設定(無し)
        .EditColumnList = CreateGrid2EditCol1()
      End With
    End With

    '------------------------------
    '       在庫一覧設定
    '------------------------------
    DG2V2 = Me.DataGridView2

    ' グリッド初期化
    Call InitGrid(DG2V2, CreateGrid2Src2(), CreateGrid2Layout2())

    With DG2V2

      ' 表示しない
      .Visible = False

      ' ２つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 3

        ' 検索コントロール設定
        '.AddSearchControl([コントロール], "絞り込み項目名", [絞り込み方法], [絞り込み項目の型])
        .AddSearchControl(Me.TxtEdaban1, "EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' 編集可能列設定（無し）
        .EditColumnList = CreateGrid2EditCol2()
      End With
    End With

    ' セットコンボボックス初期化
    InitCmbEdaType()

  End Sub

  ''' <summary>
  ''' 枝出荷明細一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1
    Dim sql As String = String.Empty
    Dim sqlWhere As String = String.Empty

    ' 抽出条件を作成
    sqlWhere &= "     NSZFLG = 2"
    sqlWhere &= " AND CUTJ.BICODE = " & EDANIKU_CODE.ToString()
    sqlWhere &= " AND IsDate(HENPINBI) = 0 "

    ' 基本部分に抽出条件を追加
    sql &= ComAddSqlSearchCondition(ComSqlSelCutJBase(), sqlWhere)

    ' 並び順設定
    sql &= " ORDER BY CUTJ.UTKCODE"
    sql &= "        , KOTAINO"
    sql &= "        , KAKOUBI"
    sql &= "        , CUTJ.BICODE"
    sql &= "        , TOOSINO"
    sql &= "        , CUTJ.KDATE DESC"

    Return sql
  End Function

  ''' <summary>
  ''' 枝出庫明細一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("個体識別番号", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=130, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("枝番", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("得意先", "DLUTKNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=300))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("仕入日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("カートンNo", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("格付", "DKZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("部位", "DBINAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0.0", argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("出荷日", "SYUKKABI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("売単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,###", argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("種別", "DSBNAME", argTextAlignment:=typAlignment.MiddleLeft))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 枝出庫一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  ''' <remarks>編集列無し</remarks>
  Public Function CreateGrid2EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol1
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 在庫一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid2Src2() As String Implements IDgvForm02.CreateGrid2Src2
    Dim sql As String = String.Empty
    Dim sqlWhere As String = String.Empty
    Dim tmpLimitDate As String = Date.Parse(ComGetProcDate()).AddDays(-21).ToString("yyyy/MM/dd")

    ' 抽出条件を作成
    sqlWhere &= "     (NSZFLG = 0 Or (NSZFLG = 6 And CUTJ.KDATE >= '" & tmpLimitDate & "'))"
    sqlWhere &= " AND CUTJ.BICODE = " & EDANIKU_CODE.ToString()
    sqlWhere &= " AND CUTJ.GYONO = 0 "

    ' 基本部分に抽出条件を追加
    sql &= ComAddSqlSearchCondition(ComSqlSelCutJBase(), sqlWhere)

    ' 並び順設定
    sql &= " ORDER BY CUTJ.TKCODE"
    sql &= "        , KOTAINO"
    sql &= "        , KAKOUBI"
    sql &= "        , CUTJ.BICODE"
    sql &= "        , TOOSINO"
    sql &= "        , CUTJ.KDATE DESC"

    Return sql
  End Function

  ''' <summary>
  ''' 在庫一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("個体識別番号", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=130, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("枝番", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("得意先", "DLTKNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=300))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("仕入日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("カートンNo", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("格付", "DKZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("部位", "DBINAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0.0", argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("入庫日", "KEIRYOBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("原単価", "GENKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,###", argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("種別", "DSBNAME", argTextAlignment:=typAlignment.MiddleLeft))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
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

#Region "コントロール制御"
  ''' <summary>
  ''' 編集不可コントロールを使用不可に
  ''' </summary>
  Private Sub LockCtrl()

    ' 加工日
    With Me.TxtDateProcess
      .Enabled = False
      .ReadOnly = True
    End With

    ' 枝番
    With Me.TxtEdaban1
      .Enabled = False
      .ReadOnly = True
    End With

    ' 個体識別番号
    With Me.TxKotaiNo1
      .Enabled = False
      .ReadOnly = True
    End With

    ' セットは1頭以外編集不可
    If IsSelected() _
      AndAlso _SelectedData("SAYUKUBUN") <> PARTS_SIDE_BOTH.ToString() Then
      Me.CmbEdaType.Enabled = False
    End If

    ' 出荷明細表示時は得意先コンボ使用不可
    If DG2V1.Visible Then
      Me.CmbMstCustomer1.Enabled = False
    End If
  End Sub

  ''' <summary>
  ''' 編集不可コントロールを使用可に
  ''' </summary>
  Private Sub UnLockCtrl()

    ' 加工日
    With Me.TxtDateProcess
      .Enabled = True
      .ReadOnly = False
    End With

    ' 枝番
    With Me.TxtEdaban1
      .Enabled = True
      .ReadOnly = False
    End With

    ' 個体識別番号
    With Me.TxKotaiNo1
      .Enabled = True
      .ReadOnly = False
    End With

    ' セット
    Me.CmbEdaType.Enabled = True

    Me.CmbMstCustomer1.Enabled = True

  End Sub

  ''' <summary>
  ''' データは選択されているか？
  ''' </summary>
  ''' <returns>
  '''   True  - データは選択されている
  '''   false - データは選択されていない
  ''' </returns>
  Private Function IsSelected() As Boolean
    Dim ret As Boolean = (_SelectedData.Keys.Count > 0)

    If ret = False Then
      ComMessageBox("データが選択されていません。", PRG_TITLE, typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
    End If

    Return ret
  End Function

  ''' <summary>
  ''' セットコンボボックス初期化
  ''' </summary>
  Private Sub InitCmbEdaType()

    Dim tmpDataSrc As New DataTable
    Dim tmpDr As DataRow

    With tmpDataSrc
      .Columns.Add("DisplayMember")
      .Columns.Add("ValueMember")
    End With

    tmpDr = tmpDataSrc.NewRow
    tmpDr("DisplayMember") = PARTS_SIDE_BOTH.ToString & ":1頭"
    tmpDr("ValueMember") = PARTS_SIDE_BOTH
    tmpDataSrc.Rows.Add(tmpDr)

    tmpDr = tmpDataSrc.NewRow
    tmpDr("DisplayMember") = PARTS_SIDE_LEFT.ToString & ":左半頭"
    tmpDr("ValueMember") = PARTS_SIDE_LEFT
    tmpDataSrc.Rows.Add(tmpDr)

    tmpDr = tmpDataSrc.NewRow
    tmpDr("DisplayMember") = PARTS_SIDE_RIGHT.ToString & ":右半頭"
    tmpDr("ValueMember") = PARTS_SIDE_RIGHT
    tmpDataSrc.Rows.Add(tmpDr)

    With Me.CmbEdaType
      .DataSource = tmpDataSrc
      .DisplayMember = "DisplayMember"
      .ValueMember = "ValueMember"
      .SelectedValue = -1
      .DropDownStyle = ComboBoxStyle.DropDownList
    End With

  End Sub

  ''' <summary>
  ''' グリッドを枝在庫一覧に切り替え
  ''' </summary>
  Private Sub ShowStockList()

    ' 枝在庫一覧表示
    Controlz(DG2V1.Name).AutoSearch = False
    Controlz(DG2V2.Name).ShowList()
    DG2V2.Visible = True
    DG2V1.Visible = False

  End Sub

  ''' <summary>
  ''' グリッドを枝出荷明細に切替
  ''' </summary>
  Private Sub ShowOutStockDetailList()

    DG2V1.Visible = True
    DG2V2.Visible = False
    Controlz(DG2V1.Name).AutoSearch = True

  End Sub

#End Region

#Region "データ更新"

  ''' <summary>
  ''' 更新処理
  ''' </summary>
  Private Sub UpDateDb()
    Dim tmpDb As New clsSqlServer()
    Dim tmpProcTime As String = ComGetProcTime()

    With tmpDb
      Try

        .TrnStart()

        If 1 <> .Execute(SqlUpdCutJ(tmpProcTime)) Then
          Throw New Exception("データ更新に失敗しました。他のユーザーによって変更された可能性があります。")
        End If

        ' 1頭から半頭に修正の場合は、残り半頭の在庫を作成する
        If ComBlank2ZeroText(_SelectedData("SAYUKUBUN")) = PARTS_SIDE_BOTH.ToString _
            AndAlso ComNothing2ZeroText(Me.CmbEdaType.SelectedValue) <> PARTS_SIDE_BOTH.ToString Then

          ' 半頭在庫作成
          Call CreateAnotherSideEdaStock(tmpProcTime, tmpDb)

        End If

        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("データの更新に失敗しました。")
      End Try
    End With

  End Sub

  ''' <summary>
  ''' 出荷された側と反対側の枝在庫を作成
  ''' </summary>
  ''' <param name="prmProcTime">更新日時</param>
  ''' <param name="prmDb">SQLサーバー操作オブジェクト</param>
  ''' <remarks>
  ''' 以下の場合に実行
  ''' ・1頭在庫の半頭出庫時
  ''' ・1頭出庫の半頭出庫変更時
  ''' </remarks>
  Private Sub CreateAnotherSideEdaStock(prmProcTime As String _
                                        , prmDb As clsSqlServer)
    Dim tmpWeight As Long = 0

    Try
      tmpWeight = GetAnotherSideEdaWeight()
      If tmpWeight > 0 Then
        prmDb.Execute(SqlInsCutJHelfData(tmpWeight, prmProcTime))
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("半頭枝情報の作成に失敗しました。")
    End Try

  End Sub

  ''' <summary>
  ''' 更新対象と逆側の枝肉重量を取得
  ''' </summary>
  ''' <returns>枝肉重量</returns>
  Private Function GetAnotherSideEdaWeight() As Long
    Dim ret As Long = 0
    Dim tmpDb As New clsSqlServer()
    Dim tmpDt As New DataTable

    Try

      ' 個体識別番号をキーに枝情報を取得
      Call tmpDb.GetResult(tmpDt, SqlSelEdab())

      If tmpDt.Rows.Count > 0 Then
        ' 画面で選択されているセット（1頭、左、右）と逆側の重量を取得する
        ' ※1頭の場合を除く
        Select Case Long.Parse(ComNothing2ZeroText(Me.CmbEdaType.SelectedValue))
          Case PARTS_SIDE_LEFT
            ret = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("JYURYO2").ToString()))
          Case PARTS_SIDE_RIGHT
            ret = Long.Parse(ComBlank2ZeroText(tmpDt.Rows(0)("JYURYO1").ToString()))
        End Select
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("枝肉重量の取得に失敗しました。")
    Finally
      tmpDb.DbDisconnect()
      tmpDt.Dispose()
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 出庫を削除し在庫に戻す
  ''' </summary>
  Private Sub DeleteDb()
    Dim tmpDb As New clsSqlServer
    Dim tmpProcTime As String = ComGetProcTime()
    Dim tmpAnotherSideCondition As String = String.Empty

    ' 反対側の在庫はあるか？
    Dim tmpExistAnotherSideStock As Boolean = False

    With tmpDb
      Try

        ' 削除対象が半頭
        If _SelectedData("SAYUKUBUN") <> PARTS_SIDE_BOTH.ToString() Then

          tmpAnotherSideCondition &= " WHERE EBCODE = " & _SelectedData("EBCODE")
          tmpAnotherSideCondition &= "   AND KOTAINO = " & _SelectedData("KOTAINO")

          ' 対象と逆側を抽出
          If _SelectedData("SAYUKUBUN") = PARTS_SIDE_LEFT.ToString() Then
            tmpAnotherSideCondition &= "   AND SAYUKUBUN = " & PARTS_SIDE_RIGHT.ToString
          ElseIf _SelectedData("SAYUKUBUN") = PARTS_SIDE_RIGHT.ToString() Then
            tmpAnotherSideCondition &= "   AND SAYUKUBUN = " & PARTS_SIDE_LEFT.ToString
          End If

          tmpAnotherSideCondition &= "   AND KYOKUFLG = 0 "
          tmpAnotherSideCondition &= "   AND CUTJ.KUBUN <> 9 "
          tmpAnotherSideCondition &= "   AND DKUBUN = 0 "
          tmpAnotherSideCondition &= "   AND (NSZFLG = 0 Or NSZFLG = 6) "


          ' 反対側の半頭在庫が存在するか？
          tmpExistAnotherSideStock = ExistsAnotherSideStock(tmpAnotherSideCondition)

        End If

        .TrnStart()


        ' 削除対象が反対側が存在するか？
        If tmpExistAnotherSideStock = False Then
          '----------------
          '   存在しない
          '----------------

          ' 削除(出庫戻し)実行
          If 1 <> .Execute(SqlReturnStockCutJ(tmpProcTime)) Then
            Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
          End If
        Else
          '----------------
          '   存在する
          '----------------

          ' 1頭分の重量に変更
          If 1 <> .Execute(SqlUpdCUTJHalf2ALL(tmpAnotherSideCondition, tmpProcTime)) Then
            Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
          End If

          ' 元データ削除
          If 1 <> .Execute("delete from CUTJ WHERE " & ComSqlCutJUniqueKey(_SelectedData)) Then
            Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
          End If

        End If

        .TrnCommit()
      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("データの削除に失敗しました。")
      End Try

    End With
  End Sub

  ''' <summary>
  ''' 更新対象の対になる在庫が存在するか？
  ''' </summary>
  ''' <returns>
  '''  True  -  存在する
  '''  False -  存在しない
  ''' </returns>
  Private Function ExistsAnotherSideStock(prmCondition As String) As Boolean
    Dim ret As Boolean = False
    Dim tmpDb As New clsSqlServer()
    Dim tmpDt As New DataTable


    With tmpDb

      Try

        .GetResult(tmpDt, "SELECT KOTAINO FROM CUTJ " & prmCondition)
        ret = (tmpDt.Rows.Count > 0)

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("対になる枝肉在庫の確認に失敗しました。")
      Finally
        tmpDb.DbDisconnect()
        tmpDt.Dispose()
      End Try
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 枝重量を枝情報より取得する
  ''' </summary>
  ''' <returns></returns>
  Private Function GetEdaWeightFromEDAB(prmPartsSide As Integer) As String
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim tmpWeight As Long = 0
    Dim tmpWeightL As Long = 0
    Dim tmpWeightR As Long = 0
    Dim tmpWeightB As Long = 0

    Try
      tmpDb.GetResult(tmpDt, SqlSelEdab())
      If tmpDt.Rows.Count > 0 Then

        tmpWeightB = Long.Parse(tmpDt.Rows(0)("JYURYO").ToString())
        tmpWeightL = Long.Parse(tmpDt.Rows(0)("Jyuryo1").ToString())
        tmpWeightR = Long.Parse(tmpDt.Rows(0)("Jyuryo2").ToString())

        Select Case prmPartsSide
          Case PARTS_SIDE_BOTH
            tmpWeight = tmpWeightB
          Case PARTS_SIDE_LEFT
            If tmpWeightL > 0 Then
              tmpWeight = tmpWeightL
            Else
              If tmpWeightR > 0 Then
                tmpWeight = tmpWeightR
              End If
            End If
          Case PARTS_SIDE_RIGHT
            If tmpWeightR > 0 Then
              tmpWeight = tmpWeightR
            Else
              If tmpWeightL > 0 Then
                tmpWeight = tmpWeightL
              End If
            End If
        End Select

        If tmpWeight <= 0 Then
          tmpWeight = CLng(tmpWeightB / 2)
        End If

      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("枝重量の取得に失敗しました")
    Finally
      tmpDb.DbDisconnect()
      tmpDt.Dispose()
    End Try

    Return ComG2Kg(tmpWeight.ToString())
  End Function
#End Region

#Region "SQL文作成関連"

#Region "登録関連"

  ''' <summary>
  ''' CUTJ更新SQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">更新日時</param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>登録処理用</remarks>
  Private Function SqlUpdCutJ(prmProcTime As String) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET NSZFLG = 2"
    sql &= "   , HONSU = 1 "
    sql &= "   , NKUBUN = 0 "
    sql &= "   , KDATE = '" & prmProcTime & "'"
    sql &= "   , SAYUKUBUN = " & ComNothing2ZeroText(Me.CmbEdaType.SelectedValue)
    sql &= "   , UTKCODE = " & ComNothing2ZeroText(Me.CmbMstCustomer1.SelectedValue)
    sql &= "   , JYURYO = " & ComKg2G(TxtWeitghtKg1.Text)
    sql &= "   , TANKA = " & ComNothing2ZeroText(Me.TxtUnitPrice.Text)
    sql &= "   , SYUKKABI = '" & Me.TxtDateOutStock.Text & "'"
    sql &= "   , TANTO = " & ComNothing2ZeroText(Me.CmbMstStaff1.SelectedValue)

    ' CUTJの抽出条件を追加
    sql = ComAddSqlSearchCondition(sql, ComSqlCutJUniqueKey(_SelectedData))
    ' 左右区分を追加
    sql = ComAddSqlSearchCondition(sql, " SAYUKUBUN =" & _SelectedData("SAYUKUBUN"))

    Return sql
  End Function

  ''' <summary>
  ''' 半頭分のCUTJデータ作成
  ''' </summary>
  ''' <param name="prmProcTime">更新日時</param>
  ''' <param name="prmWeight">重量</param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>半頭出荷時に残り半身のデータを作成する</remarks>
  Private Function SqlInsCutJHelfData(prmWeight As Long _
                                      , prmProcTime As String) As String
    Dim sql As String = String.Empty
    Dim tmpKeyValue As New Dictionary(Of String, String)
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

#Region "挿入データ設定"

    With tmpKeyValue

#Region "固定値"
      .Add("NSZFLG", "0")
      .Add("SYUKKABI", "NULL")
      .Add("HONSU", "1")
      .Add("TDATE", "'" & prmProcTime & "'")
      .Add("KDATE", "'" & Date.Parse(prmProcTime).AddMinutes(1).ToString("yyyy/MM/dd HH:mm:ss") & "'")  ' TDATEの1分後
#End Region

#Region "画面の入力情報より"
      Select Case ComNothing2ZeroText(Me.CmbEdaType.SelectedValue)  ' 左右区分（画面で選択されてる側と反対を設定）
        Case PARTS_SIDE_LEFT.ToString()
          .Add("SAYUKUBUN", PARTS_SIDE_RIGHT.ToString())
        Case PARTS_SIDE_RIGHT.ToString()
          .Add("SAYUKUBUN", PARTS_SIDE_LEFT.ToString())
        Case Else
          Throw New Exception("セットコード不正")
      End Select

      .Add("TANKA", ComBlank2ZeroText(Me.TxtUnitPrice.Text))
      .Add("TANTO", ComNothing2ZeroText(Me.CmbMstStaff1.SelectedValue))
      .Add("UTKCODE", ComNothing2ZeroText(Me.CmbMstCustomer1.SelectedValue))

#End Region

#Region "その他"
      .Add("JYURYO", prmWeight.ToString())
      .Add("SIRIALNO", (ComBlank2Zero(_SelectedData("SIRIALNO")) + 1).ToString())
#End Region

#Region "元データと同一の値"
      .Add("KUBUN", _SelectedData("KUBUN"))
      .Add("KIKAINO", _SelectedData("KIKAINO"))
      .Add("KYOKUFLG", _SelectedData("KYOKUFLG"))
      .Add("TKCODE", _SelectedData("TKCODE"))
      .Add("KAKUC", _SelectedData("KAKUC"))
      .Add("EBCODE", _SelectedData("EBCODE"))
      .Add("SPKUBUN", _SelectedData("SPKUBUN"))
      .Add("KIKAKUC", _SelectedData("KIKAKUC"))
      .Add("BICODE", _SelectedData("BICODE"))
      .Add("GBFLG", _SelectedData("GBFLG"))
      .Add("TOOSINO", _SelectedData("TOOSINO"))
      .Add("BLOCKCODE", _SelectedData("BLOCKCODE"))
      .Add("KINGAKU", _SelectedData("KINGAKU"))
      .Add("KIGENBI", _SelectedData("KIGENBI"))
      .Add("SYUBETUC", _SelectedData("SYUBETUC"))
      .Add("KOTAINO", _SelectedData("KOTAINO"))
      .Add("YOBI", _SelectedData("YOBI"))
      .Add("SHOHINC", _SelectedData("SHOHINC"))
      .Add("LABELJI", _SelectedData("LABELJI"))
      .Add("GENSANCHIC", _SelectedData("GENSANCHIC"))
      .Add("COMMENTC", _SelectedData("COMMENTC"))
      .Add("SRCODE", _SelectedData("SRCODE"))
      .Add("TJCODE", _SelectedData("TJCODE"))
      .Add("KFLG", _SelectedData("KFLG"))
      .Add("BUDOMARI", _SelectedData("BUDOMARI"))
      .Add("OLDTKC", _SelectedData("OLDTKC"))
      .Add("BAIKA", _SelectedData("BAIKA"))
      .Add("DENNO", _SelectedData("DENNO"))
      .Add("GYONO", _SelectedData("GYONO"))
      .Add("SETCD", _SelectedData("SETCD"))
      .Add("GENKA", _SelectedData("GENKA"))
      .Add("DKUBUN", _SelectedData("DKUBUN"))
      .Add("NDENNO", ComBlank2NullText(_SelectedData("NDENNO")))
      .Add("NGYONO", ComBlank2NullText(_SelectedData("NGYONO")))

      If ComBlank2Zero(_SelectedData("NKUBUN")) = 0 Then
        .Add("NKUBUN", "NULL"）
      Else
        .Add("NKUBUN", _SelectedData("NKUBUN")）
      End If

      .Add("HTANKA", ComBlank2NullText(_SelectedData("HTANKA")))

      If _SelectedData("HENPINBI") <> "" Then
        .Add("HENPINBI", "'" & _SelectedData("HENPINBI") & "'")
      End If

      If _SelectedData("KAKOUBI") <> "" Then
        .Add("KAKOUBI", "'" & _SelectedData("KAKOUBI") & "'")
      End If
      If _SelectedData("KEIRYOBI") <> "" Then
        .Add("KEIRYOBI", "'" & _SelectedData("KEIRYOBI") & "'")
      End If

#End Region

    End With
#End Region

    ' 挿入項目、挿入値を結合
    For Each tmpKey As String In tmpKeyValue.Keys
      tmpDst &= tmpKey & " ,"
      tmpVal &= tmpKeyValue(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    ' SQL文作成
    sql &= " INSERT INTO CUTJ(" & tmpDst & ")"
    sql &= "           VALUES(" & tmpVal & ")"

    Return sql
  End Function
#End Region

#Region "削除関連"

  ''' <summary>
  ''' CUTJ出庫戻しSQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">更新日時</param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>在庫データに戻す</remarks>
  Private Function SqlReturnStockCutJ(prmProcTime As String) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET NSZFLG = 0"
    sql &= "   , SYUKKABI = null "
    sql &= "   , NKUBUN = null "
    sql &= "   , TANTO = " & ComNothing2ZeroText(Me.CmbMstStaff1.SelectedValue)
    sql &= "   , KDATE = '" & prmProcTime & "'"

    ' CUTJの抽出条件を追加
    sql = ComAddSqlSearchCondition(sql, ComSqlCutJUniqueKey(_SelectedData))
    ' 左右区分を追加
    sql = ComAddSqlSearchCondition(sql, " SAYUKUBUN =" & _SelectedData("SAYUKUBUN"))

    Return sql

  End Function

  ''' <summary>
  ''' 半頭を出庫戻しした場合に1頭文の重量に変更する
  ''' </summary>
  ''' <param name="prmCondition"></param>
  ''' <param name="prmProcTime"></param>
  ''' <returns></returns>
  Private Function SqlUpdCUTJHalf2ALL(prmCondition As String _
                                      , prmProcTime As String) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET JYURYO = JYURYO + " & _SelectedData("JYURYO")
    sql &= "   , SAYUKUBUN = " & PARTS_SIDE_BOTH.ToString()
    sql &= "   , SIRIALNO = " & ComBlank2Zero(_SelectedData("SIRIALNO"))
    sql &= "   , TDATE =  '" & _SelectedData("TDATE") & "'"
    sql &= "   , KDATE = ' " & prmProcTime & "'"
    sql &= "   , NKUBUN = null "
    sql &= prmCondition

    Return sql
  End Function
#End Region

#Region "印刷関連"

  ''' <summary>
  ''' 印刷用ワークテーブル削除SQL文作成
  ''' </summary>
  ''' <returns>SQL文</returns>
  Private Function SqlDelWK_EDASYUKO() As String
    Dim sql As String = String.Empty

    sql &= " DELETE FROM WK_EDASYUKO "

    Return sql
  End Function

  ''' <summary>
  ''' 出荷一覧印刷用ワークテーブル挿入SQL文作成
  ''' </summary>
  ''' <param name="prmItemList">挿入するデータ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsWkTblFromGrid(prmItemList As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty
    Dim tmpKeyValue As New Dictionary(Of String, String)
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    With tmpKeyValue
      .Add("SYUKKABI", "#" & prmItemList("SYUKKABI") & "#")
      .Add("DLUTKNAME", "'" & prmItemList("DLUTKNAME") & "'")
      .Add("DSBNAME", "'" & prmItemList("DSBNAME") & "'")
      .Add("DBINAME", "'" & prmItemList("DBINAME") & "'")

      If prmItemList("BICODE").Equals(EDANIKU_CODE.ToString()) = False Then
        .Add("HANTOU", "")
      ElseIf prmItemList("SAYUKUBUN").Equals(PARTS_SIDE_BOTH.ToString()) Then
        .Add("HANTOU", "'１頭'")
      ElseIf prmItemList("SAYUKUBUN").Equals(PARTS_SIDE_LEFT.ToString()) Then
        .Add("HANTOU", "'左半頭'")
      Else
        .Add("HANTOU", "'右半頭'")
      End If

      .Add("KAKOUBI", "#" & prmItemList("KAKOUBI") & "#")
      .Add("KOTAINO", "'" & prmItemList("KOTAINO").PadLeft(10, "0"c) & "'")
      .Add("HONSU", prmItemList("HONSU"))
      .Add("JYURYOK", prmItemList("JYURYOK"))
      .Add("TANKA", prmItemList("TANKA"))
      .Add("TOOSINO", prmItemList("TOOSINO"))
      .Add("DGNNAME", "'" & prmItemList("DGNNAME") & "'")
      .Add("DKZNAME", "'" & prmItemList("DKZNAME") & "'")
      .Add("EBCODE", prmItemList("EBCODE"))
    End With

    For Each tmpKey As String In tmpKeyValue.Keys
      tmpDst &= tmpKey & " ,"
      tmpVal &= tmpKeyValue(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= " INSERT INTO WK_EDASYUKO(" & tmpDst & ")"
    sql &= "                  VALUES(" & tmpVal & ")"

    Return sql
  End Function

#End Region

#Region "共通"
  ''' <summary>
  ''' 枝情報取得SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelEdab() As String
    Dim sql As String = String.Empty

    ' 個体識別番号のみで抽出

    sql &= " SELECT * "
    sql &= " FROM EDAB "
    sql &= " WHERE KOTAINO = " & _SelectedData("KOTAINO")

    Return sql
  End Function

  ''' <summary>
  ''' CUTJユニークキー
  ''' </summary>
  ''' <param name="prmCondition">抽出条件データが含まれる連想配列</param>
  ''' <returns>CUTJの1レコードを抽出する条件文</returns>
  Public Function ComSqlCutJUniqueKey(prmCondition As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    sql &= "       TDATE = '" & prmCondition("TDATE") & "'"
    sql &= "   AND KUBUN = " & prmCondition("KUBUN")
    sql &= "   AND KIKAINO = " & prmCondition("KIKAINO")
    sql &= "   AND SIRIALNO = " & prmCondition("SIRIALNO")
    sql &= "   AND KYOKUFLG = " & prmCondition("KYOKUFLG")
    sql &= "   AND BICODE = " & prmCondition("BICODE")
    sql &= "   AND TOOSINO =" & prmCondition("TOOSINO")
    sql &= "   AND EBCODE =" & prmCondition("EBCODE")
    sql &= "   AND NSZFLG =" & prmCondition("NSZFLG")
    sql &= "   AND KDATE ='" & prmCondition("KDATE") & "'"

    Return sql
  End Function

  ''' <summary>
  ''' CUTJ抽出SQL文基礎
  ''' </summary>
  ''' <returns>SQL文</returns>
  ''' <remarks>
  ''' CUTJテーブルに各種マスタを結合し名称を取得するSQL文
  ''' </remarks>
  Public Function ComSqlSelCutJBase() As String
    Dim sql As String = String.Empty

    sql &= " Select CUTJ.* "
    sql &= "      , CONCAT(FORMAT(CUTJ.UTKCODE,'0000') , ':' , UTK.LTKNAME)  AS DLUTKNAME "
    sql &= "      , CONCAT(FORMAT(CUTJ.TKCODE,'0000') , ':' , ZTK.LTKNAME)  AS DLTKNAME "
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
    sql &= "       ,(ROUND((CUTJ.JYURYO / 100), 0, 1) / 10) AS JYURYOK"
    sql &= "      , CASE NSZFLG "
    sql &= "         WHEN 0 THEN '在庫' "
    sql &= "         WHEN 1 THEN '返品' "
    sql &= "         WHEN 2 THEN '出庫' "
    sql &= "         WHEN 3 THEN '廃棄' "
    sql &= "         WHEN 4 THEN '不明' "
    sql &= "         WHEN 5 THEN '取消' "
    sql &= "         WHEN 8 THEN 'M返品' "
    sql &= "         ELSE '枝入庫'"
    sql &= "        END AS JYOUTAI "
    sql &= "      , CASE SAYUKUBUN "
    sql &= "         WHEN " & PARTS_SIDE_LEFT.ToString & " THEN '左' "
    sql &= "         WHEN " & PARTS_SIDE_RIGHT.ToString & " THEN '右' "
    sql &= "         ELSE ' '"
    sql &= "        END AS LR2 "
    sql &= "  FROM (((((((((((CUTJ LEFT JOIN TOKUISAKI as UTK ON CUTJ.UTKCODE = UTK.TKCODE) "
    sql &= "                       LEFT JOIN TOKUISAKI as ZTK ON CUTJ.TKCODE = ZTK.TKCODE) "
    sql &= "                       LEFT JOIN KAKU ON CUTJ.KAKUC = KAKU.KKCODE) "
    sql &= "                       LEFT JOIN KIKA ON CUTJ.KIKAKUC = KIKA.KICODE) "
    sql &= "                       LEFT JOIN BUIM ON CUTJ.BICODE = BUIM.BICODE) "
    sql &= "                       LEFT JOIN BLOCK_TBL ON CUTJ.BLOCKCODE = BLOCK_TBL.BLOCKCODE) "
    sql &= "                       LEFT JOIN GBFLG_TBL ON CUTJ.GBFLG = GBFLG_TBL.GBCODE) "
    sql &= "                       LEFT JOIN GENSN ON CUTJ.GENSANCHIC = GENSN.GNCODE) "
    sql &= "                       LEFT JOIN SHUB ON CUTJ.SYUBETUC = SHUB.SBCODE) "
    sql &= "                       LEFT JOIN COMNT ON CUTJ.COMMENTC = COMNT.CMCODE) "
    sql &= "                       LEFT JOIN CUTSR ON CUTJ.SRCODE = CUTSR.SRCODE) "
    sql &= "                       LEFT JOIN TOJM ON CUTJ.TJCODE = TOJM.TJCODE "
    sql &= " WHERE KYOKUFLG = 0 "
    sql &= "   AND CUTJ.KUBUN <> 9 "
    sql &= "   AND DKUBUN = 0 "


    Return sql
  End Function

#End Region

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
    ' 枝出荷明細表示
    Call ShowOutStockDetailList()

    If IsSelected() Then
      ' データが選択されているなら、確認メッセージを表示し更新処理実行
      If typMsgBoxResult.RESULT_OK = ComMessageBox("登録更新しますか？" _
                                                , PRG_TITLE _
                                                , typMsgBox.MSG_NORMAL _
                                                , typMsgBoxButton.BUTTON_OKCANCEL) Then

        Try
          Controlz(DG2V1.Name).AutoSearch = False

          '更新処理
          Call UpDateDb()

          ' 出荷日・得意先・担当者以外の全項目をクリア
          MyBase.AllClear(New List(Of Control)({Me.TxtDateOutStock, Me.CmbMstStaff1, Me.CmbMstCustomer1}))

          ' 選択内容をクリア
          _SelectedData.Clear()

          ' 加工日にフォーカスを当てる
          Me.ActiveControl = Me.TxtDateProcess

          ' 編集不可コントロールアンロック
          Call UnLockCtrl()
        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        Finally
          ' 自動検索ON（枝出荷明細再表示）
          Controlz(DG2V1.Name).AutoSearch = True
        End Try

      End If

    End If
  End Sub

  ''' <summary>
  ''' 在庫表示ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnDspStockList_Click(sender As Object, e As EventArgs) Handles btnDspStockList.Click
    ' Gridを在庫一覧に変更
    Call ShowStockList()
    ' 編集不可コントロールアンロック
    Call UnLockCtrl()
  End Sub

  ''' <summary>
  ''' 削除ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    ' 枝出荷明細表示
    Call ShowOutStockDetailList()

    If IsSelected() Then
      ' データが選択されているなら、確認メッセージを表示し更新処理実行
      If typMsgBoxResult.RESULT_OK = ComMessageBox("表示中の明細を削除（出庫戻し）しますか？" _
                                                , PRG_TITLE _
                                                , typMsgBox.MSG_NORMAL _
                                                , typMsgBoxButton.BUTTON_OKCANCEL) Then

        Try
          ' 一覧自動検索OFF
          Controlz(DG2V1.Name).AutoSearch = False

          ' DB更新
          Call DeleteDb()

          ' 出荷日・担当者・得意先以外の全項目をクリア
          MyBase.AllClear(New List(Of Control)({Me.TxtDateOutStock, Me.CmbMstStaff1, Me.CmbMstCustomer1}))

          ' 選択内容をクリア
          _SelectedData.Clear()

          ' 加工日にフォーカスを当てる
          Me.ActiveControl = Me.TxtDateProcess

          ' 編集不可コントロールアンロック
          Call UnLockCtrl()
        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        Finally
          ' 自動検索ON（枝出荷明細再表示）
          Controlz(DG2V1.Name).AutoSearch = True
        End Try

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

    ' 枝出荷明細表示
    Call ShowOutStockDetailList()

    If typMsgBoxResult.RESULT_OK = ComMessageBox("枝出庫情報一覧を画面表示しますか？" _
                                                , PRG_TITLE _
                                                , typMsgBox.MSG_NORMAL _
                                                , typMsgBoxButton.BUTTON_OKCANCEL) Then
      Try
        With tmpDb
          ' ワークテーブル削除
          .Execute(SqlDelWK_EDASYUKO())

          ' Gridに表示中のデータをワークテーブルに書き込み
          For Each tmpRowData As Dictionary(Of String, String) In Controlz(DG2V1.Name).GetAllData
            .Execute(SqlInsWkTblFromGrid(tmpRowData))
          Next

          .DbDisconnect()

          ' ACCESSの出庫情報一覧表レポートを表示
          ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_EDASYUKO")

        End With

      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      Finally
        tmpDb.Dispose()
      End Try
    End If

  End Sub

  ''' <summary>
  ''' 閉じるボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    ' 選択データクリア
    _SelectedData.Clear()

    ' 自動検索OFF
    MyBase.Controlz(DG2V1.Name).AutoSearch = False

    ' 入力項目クリア
    MyBase.AllClear()

    ' 出庫日を本日日付に
    Me.TxtDateOutStock.Text = ComGetProcDate()

    ' 画面非表示
    Me.Hide()

    ' 枝出荷明細表示
    Call ShowOutStockDetailList()

    ' 編集不可コントロールアンロック
    Call UnLockCtrl()

  End Sub

#End Region

#Region "フォーム"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_EdaSyuko_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.Text = PRG_TITLE

    ' 画面初期化
    Call InitForm02()


    ' 出庫日付に本日日付を設定
    Me.TxtDateOutStock.Text = ComGetProcDate()


    ' グリッドダブルクリック時イベント設定
    Controlz(DG2V1.Name).lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick
    Controlz(DG2V2.Name).lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick

    ' グリッド表示更新時イベント設定
    Controlz(DG2V1.Name).lcCallBackReLoadData = AddressOf DgvReload
    Controlz(DG2V2.Name).lcCallBackReLoadData = AddressOf DgvReload

    ' 標準メッセージ以外のメッセージ設定
    TxtDateOutStock.SetMsgLabelText("出庫日（納品日）を入力します。")                           ' 出荷日
    TxtUnitPrice.SetMsgLabelText("Kg当りの売単価を入力します。")                             ' Kg売単価
    Controlz(DG2V1.Name).SetMsgLabelText("出庫明細を修正したいときは明細行を選びＥｎｔｅｒを押して下さい。")
    Controlz(DG2V2.Name).SetMsgLabelText("出庫したい明細行を選びＥｎｔｅｒを押して下さい。")

    ' メッセージラベル設定
    MyBase.SetMsgLbl(Me.lblInformation)

    ' 出庫明細表示
    Controlz(DG2V1.Name).AutoSearch = True

    ' IPC通信起動
    InitIPC(PRG_ID)

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
        ' 登録ボタン押下処理
        With btnRegist
          .Focus()
          .PerformClick()
        End With
      ' F3キー押下時
      Case Keys.F3
        ' 在庫表示ボタン押下処理
        With btnDspStockList
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

#Region "グリッド関連"

  ''' <summary>
  ''' データグリッド更新時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="LastUpdate">最終更新日時</param>
  ''' <param name="DataCount">データ件数</param>
  Private Sub DgvReload(sender As DataGridView, LastUpdate As String, DataCount As Long)
    Me.lblGridStat.Text = CType(IIf(sender.Equals(DG2V1), "枝出庫明細  ", "枝在庫一覧  "), String) & "件数：" & DataCount.ToString()
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
    _SelectedData = Controlz(DirectCast(sender, DataGridView).Name).SelectedRow

    If DG2V1.Visible Then
      ' 出荷明細表示時は選択された得意先を編集コントロールに設定
      Me.CmbMstCustomer1.SelectedValue = Integer.Parse(_SelectedData("UTKCODE"))                  ' 得意先
    End If

    Me.TxtEdaban1.Text = _SelectedData("EBCODE")                                                  ' 枝番
    Me.TxKotaiNo1.Text = _SelectedData("KOTAINO")                                                 ' 個体識別番号
    Me.TxtDateProcess.Text = Date.Parse(_SelectedData("KAKOUBI")).ToString("yyyy/MM/dd")          ' 加工日
    Me.TxtWeitghtKg1.Text = ((Math.Floor(Long.Parse(_SelectedData("JYURYO")))) / 1000).ToString   ' 重量
    Me.TxtUnitPrice.Text = _SelectedData("TANKA")                                                 ' 売単価
    With Me.CmbEdaType
      .SelectedItem = Nothing
      .SelectedValue = Integer.Parse(_SelectedData("SAYUKUBUN"))                                  ' セット
      .Text = _SelectedData("SAYUKUBUN")
    End With

    ' 一覧の自動検索を停止
    Controlz(DG2V1.Name).AutoSearch = False

    ' 編集不可コントロール設定
    Call LockCtrl()

    ' 枝出庫明細を表示
    Call ShowOutStockDetailList()

  End Sub

#End Region

#Region "テキストボックス"

  ''' <summary>
  ''' 出荷日フォーカス取得時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtDateOutStock_GotFocus(sender As Object, e As EventArgs) Handles TxtDateOutStock.GotFocus

    ' 未入力時は本日日付を設定
    If Me.TxtDateOutStock.Text.Trim = "" Then
      Me.TxtDateOutStock.Text = ComGetProcDate()
    End If

    ' 日付形式文字列が入力されているなら出荷日・担当者以外の入力内容をクリア
    ' ※マウスでのフォーカス移動時に日付エラーが発生した場合の対応
    If DirectCast(sender, TxtDateBase).HasError = False Then
      ' 一覧の自動検索を停止
      Controlz(DG2V1.Name).AutoSearch = False

      ' 出荷日・担当者以外の入力内容をクリア
      MyBase.AllClear(New List(Of Control)({Me.TxtDateOutStock, Me.CmbMstStaff1}))

      ' 出庫明細を表示
      Call ShowOutStockDetailList()

      ' 選択内容をクリア
      _SelectedData.Clear()
    End If


    ' 編集不可コントロールアンロック
    Call UnLockCtrl()

  End Sub

  ''' <summary>
  ''' バリデーション時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtDateOutStockValidating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtDateOutStock.Validating
    ' 未入力なら本日日付を設定する
    With Me.TxtDateOutStock
      If .Text.Length <= 0 Then
        .Text = ComGetProcDate()
      End If
    End With
  End Sub

  Private Sub CmbEdaType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbEdaType.SelectedIndexChanged

    If Me.CmbEdaType.SelectedIndex >= PARTS_SIDE_BOTH _
      AndAlso Me.TxKotaiNo1.Text <> "" Then
      Me.TxtWeitghtKg1.Text = GetEdaWeightFromEDAB(Me.CmbEdaType.SelectedIndex)
    End If

  End Sub

#End Region

#End Region

End Class
