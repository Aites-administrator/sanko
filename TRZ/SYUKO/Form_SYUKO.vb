Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData

Public Class Form_SYUKO
  Implements IDgvForm02

  '----------------------------------------------
  '               出庫入力画面
  '
  '
  '----------------------------------------------
#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "syuko"
  Private Const PRG_TITLE As String = "出庫処理"
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

    '出荷明細オブジェクトの設定
    DG2V1 = Me.DataGridView1
    '在庫一覧オブジェクトの設定
    DG2V2 = Me.DataGridView2

    ' グリッド初期化
    Call InitGrid(DG2V1, CreateGrid2Src1(), CreateGrid2Layout1())

    ' 出荷明細設定
    With DG2V1

      '表示する
      .Visible = True

      ' グリッド動作設定
      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 3

        ' 検索コントロール設定
        .AddSearchControl(Me.TxtSyukoDate, "SYUKKABI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        .AddSearchControl(Me.CmbMstCustomer1, "UTKCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

      End With
    End With

    ' グリッド初期化
    Call InitGrid(DG2V2, CreateGrid2Src2(), CreateGrid2Layout2())

    ' 在庫一覧設定
    With DG2V2

      ' 非表示にする
      .Visible = False

      ' グリッド動作設定
      With Controlz(.Name)
        .FixedRow = 2

        ' 在庫一覧検索コントロール設定
        .AddSearchControl(Me.CmbMstOriginPlace1, "CUTJ.GENSANCHIC", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbMstRating1, "CUTJ.KAKUC", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtEdaban1, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtKotaiNo1, "CUTJ.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.TxtLblMstItem1.CodeCtrl, "CUTJ.BICODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

      End With
    End With

  End Sub

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   出庫明細抽出用
  ''' </remarks>
  Private Function CreateGrid2Src1() As String Implements IDgvForm02.CreateGrid2Src1
    Dim sql As String = String.Empty

    sql &= CreateSqlBase()
    sql &= " WHERE KYOKUFLG = 0 "
    sql &= "   AND CUTJ.KUBUN <> 9  "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND NSZFLG = 2 "
    sql &= "   AND ISDATE(HENPINBI) <> 1 "
    sql &= " ORDER BY CUTJ.UTKCODE"
    sql &= "        , KOTAINO"
    sql &= "        , KAKOUBI"
    sql &= "        , CUTJ.BICODE"
    sql &= "        , TOOSINO"
    sql &= "        , CUTJ.KDATE DESC"

    Return sql
  End Function

  ''' <summary>
  ''' 出庫明細DataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("個体識別番号", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("枝番", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("得意先", "DLUTKNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=300))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("加工日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("カートンNo", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("格付", "DKZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("部位", "DBINAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0.0", argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("出荷日", "SYUKKABI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("売単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="\\#,###", argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("種別", "DSBNAME", argTextAlignment:=typAlignment.MiddleLeft))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 出庫明細のDataGridViewオブジェクトの一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  ''' <remarks>編集列はありません</remarks>
  Public Function CreateGrid2EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol1
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 在庫一覧のDataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   在庫一覧表示用SQL文
  ''' </remarks>
  Private Function CreateGrid2Src2() As String Implements IDgvForm02.CreateGrid2Src2
    Dim sql As String = String.Empty

    sql &= CreateSqlBase()
    sql &= " WHERE KYOKUFLG = 0 "
    sql &= "   And CUTJ.KUBUN <> 9 "
    sql &= "   And DKUBUN = 0 "
    sql &= "   And NSZFLG <= 1"
    sql &= " ORDER BY KAKOUBI"
    sql &= "        , EBCODE"
    sql &= "        , KOTAINO"
    sql &= "        , CUTJ.BICODE"
    sql &= "        , TOOSINO"


    Return sql
  End Function

  ''' <summary>
  ''' 在庫一覧のDataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout2() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout2
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      ' .Add(New clsDGVColumnSetting("タイトル", "データソース", argTextAlignment:=[表示位置]))
      .Add(New clsDGVColumnSetting("個体識別番号", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("枝番", "EBCODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("得意先", "DLTKNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=300))
      .Add(New clsDGVColumnSetting("原産地", "DGNNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("加工日", "KAKOUBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("カートンNo", "TOOSINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("格付", "DKZNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=140))
      .Add(New clsDGVColumnSetting("部位", "DBINAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("重量", "JYURYOK", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0.0", argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("入庫日", "KEIRYOBI", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="yy/MM/dd", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("原単価", "GENKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="\\#,###", argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("種別", "DSBNAME", argTextAlignment:=typAlignment.MiddleLeft))
      .Add(New clsDGVColumnSetting("更新日", "KDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=220, argFormat:="yyyy/MM/dd HH:mm:ss"))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 在庫一覧のDataGridViewオブジェクトの一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  ''' <remarks>編集列はありません</remarks>
  Public Function CreateGrid2EditCol2() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol2
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
    End With

    Return ret
  End Function
#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_SYUKO, AddressOf ComRedisplay)
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
  ''' 抽出条件、並び順を変更し[出庫明細][在庫一覧]で共通で使用
  ''' </remarks>
  Private Function CreateSqlBase() As String
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
    sql &= "         WHEN 1 THEN '左' "
    sql &= "         WHEN 2 THEN '右' "
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

    Return sql
  End Function

  ''' <summary>
  ''' データ更新SQL文作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlUpdCutJ() As String
    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= "  SET SYUBETUC =" & ComNothing2ZeroText(CmbMstCattle1.SelectedValue)
    sql &= "    , GENSANCHIC = " & ComNothing2ZeroText(CmbMstOriginPlace1.SelectedValue)
    sql &= "    , KAKUC =  " & ComNothing2ZeroText(CmbMstRating1.SelectedValue)
    sql &= "    , EBCODE =  " & ComNothing2ZeroText(TxtEdaban1.Text)
    sql &= "    , Kotaino = " & ComNothing2ZeroText(TxtKotaiNo1.Text)
    sql &= "    , SETCD =  " & ComNothing2ZeroText(CmbMstSetType1.SelectedValue)
    sql &= "    , UTKCODE =  " & ComNothing2ZeroText(CmbMstCustomer1.SelectedValue)
    sql &= "    , HONSU =  " & ComNothing2ZeroText(txtSyukoCount.Text)
    sql &= "    , JYURYO =  " & Decimal.Parse(ComNothing2ZeroText(CDbl(TxtWeitghtKg1.Text))) * 1000
    sql &= "    , TANKA =  " & ComNothing2ZeroText(ComNothing2ZeroText(TxtUnitPrice.Text))
    sql &= "    , SYUKKABI =  '" & TxtSyukoDate.Text & "'"
    sql &= "    , TANTO =  " & ComNothing2ZeroText(CmbMstStaff1.SelectedValue)
    sql &= "    , NSZFLG = 2 "
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
  ''' 出荷データ削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   NSZFLGを0に変更することで在庫に戻す
  ''' </remarks>
  Private Function SqlDelCutJ() As String
    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= "  SET NSZFLG = 0 "
    sql &= "    , KDATE = '" & ComGetProcTime() & "'"
    sql &= "    , TANTO =  " & ComNothing2ZeroText(CmbMstStaff1.SelectedValue)

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
  ''' 出荷一覧印刷用ワークテーブル挿入SQL文作成
  ''' </summary>
  ''' <param name="prmItemList">挿入するデータ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsWkTblFromGrid(prmItemList As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty

    sql &= " INSERT INTO WK_SYUKO( SYUKKABI "
    sql &= "                     , DLUTKNAME"
    sql &= "                     , DSBNAME"
    sql &= "                     , DBINAME"
    sql &= "                     , HANTOU"
    sql &= "                     , KAKOUBI"
    sql &= "                     , KOTAINO"
    sql &= "                     , HONSU"
    sql &= "                     , JYURYOK"
    sql &= "                     , TANKA"
    sql &= "                     , TOOSINO"
    sql &= "                     , DGNNAME"
    sql &= "                     , DKZNAME"
    sql &= "                     , EBCODE"
    sql &= "                     )"
    sql &= " VALUES(#" & prmItemList("SYUKKABI") & "#"
    sql &= " ,'" & prmItemList("DLUTKNAME") & "'"
    sql &= " ,'" & prmItemList("DSBNAME") & "'"
    sql &= " ,'" & prmItemList("DBINAME") & "'"

    If prmItemList("BICODE").Equals(EDANIKU_CODE.ToString()) = False Then
      sql &= ", ''"
    ElseIf prmItemList("SAYUKUBUN").Equals(PARTS_SIDE_BOTH.ToString()) Then
      sql &= ", '１頭'"
    ElseIf prmItemList("SAYUKUBUN").Equals(PARTS_SIDE_LEFT.ToString()) Then
      sql &= ", '左半頭'"
    Else
      sql &= ", '右半頭'"
    End If

    sql &= " ,#" & prmItemList("KAKOUBI") & "#"
    sql &= " ,'" & prmItemList("KOTAINO").PadLeft(10, "0"c) & "'"
    sql &= " ," & prmItemList("HONSU")
    sql &= " ," & prmItemList("JYURYOK")
    sql &= " ," & prmItemList("TANKA")
    sql &= " ," & prmItemList("TOOSINO")
    sql &= " ,'" & prmItemList("DGNNAME") & "'"
    sql &= " ,'" & prmItemList("DKZNAME") & "'"
    sql &= " ," & prmItemList("EBCODE")
    sql &= ")"

    Return sql
  End Function

  ''' <summary>
  ''' 出庫一覧印刷用ワークテーブル削除SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelWK_SYUKO() As String
    Dim sql As String = String.Empty

    sql &= " DELETE FROM WK_SYUKO "

    Return sql

  End Function
#End Region

#Region "データ更新"

  ''' <summary>
  ''' データ更新を行う
  ''' </summary>
  Private Sub UpDateDb()
    Dim tmpDb As New clsSqlServer

    Try
      With tmpDb
        ' 更新処理実行
        .TrnStart()

        If .Execute(SqlUpdCutJ) = 1 Then
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
      Throw New Exception("データの更新に失敗しました。")
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

        If .Execute(SqlDelCutJ) = 1 Then
          ' 更新OK
          .TrnCommit()
        Else
          '更新失敗
          ' 更新件数は必ず1件です
          Throw New Exception("データ削除に失敗しました。他のユーザーによって変更された可能性があります。")
        End If

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
  ''' 編集不可コントロールを使用不可に
  ''' </summary>
  Private Sub LockCtrl()
    ' 加工日
    With Me.TxtKakouDate
      .Enabled = False
      .ReadOnly = True
    End With

    ' 部位
    With Me.TxtLblMstItem1.CodeCtrl
      .Enabled = False
      .ReadOnly = True
    End With

    ' カートン
    With Me.TxtCartonNumber1
      .Enabled = False
      .ReadOnly = True
    End With

    ' 出庫数
    With Me.txtSyukoCount
      .Enabled = False
      .ReadOnly = True
    End With

  End Sub

  ''' <summary>
  ''' 編集不可コントロールを使用可に
  ''' </summary>
  ''' <remarks>
  ''' 編集は付加ですが在庫一覧の検索条件に含まれます（部位コードのみ）
  ''' ※ 部位コード以外のコントロールは入力する意味がないのでラベルに変更してもOKかも
  ''' </remarks>
  Private Sub UnLockCtrl()
    ' 加工日
    With Me.TxtKakouDate
      .Enabled = True
      .ReadOnly = False
    End With

    ' 部位
    With Me.TxtLblMstItem1.CodeCtrl
      .Enabled = True
      .ReadOnly = False
    End With

    ' カートン
    With Me.TxtCartonNumber1
      .Enabled = True
      .ReadOnly = False
    End With

    ' 出庫数
    With Me.txtSyukoCount
      .Enabled = True
      .ReadOnly = False
    End With

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
  ''' グリッドを在庫一覧に切り替え
  ''' </summary>
  Private Sub ShowStockList()

    Controlz(DG2V1.Name).AutoSearch = False
    Controlz(DG2V2.Name).ShowList()
    DG2V2.Visible = True
    DG2V1.Visible = False

  End Sub

  ''' <summary>
  ''' グリッドを出荷明細に切替
  ''' </summary>
  Private Sub ShowOutStockDetailList()

    DG2V1.Visible = True
    DG2V2.Visible = False
    Controlz(DG2V1.Name).AutoSearch = True

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

    ' 出庫明細を表示
    Call ShowOutStockDetailList()

    If IsSelected() Then
      ' データが選択されているなら、確認メッセージを表示し更新処理実行
      If typMsgBoxResult.RESULT_OK = ComMessageBox("登録更新しますか？" _
                                                , PRG_TITLE _
                                                , typMsgBox.MSG_NORMAL _
                                                , typMsgBoxButton.BUTTON_OKCANCEL) Then

        Try
          ' 更新
          Call UpDateDb()

          ' 自動検索OFF
          Controlz(DG2V1.Name).AutoSearch = False

          ' 出荷日・担当者以外の全項目をクリア
          MyBase.AllClear(New List(Of Control)({Me.TxtSyukoDate, Me.CmbMstStaff1}))

          ' 選択内容をクリア
          _SelectedData.Clear()

          ' 出荷日にフォーカスを当てる
          Me.TxtSyukoDate.Focus()

        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        Finally
          ' 自動検索ON（再表示）
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

  End Sub

  ''' <summary>
  ''' 削除ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    ' 出庫明細を表示
    Call ShowOutStockDetailList()

    If IsSelected() Then
      ' データが選択されているなら、確認メッセージを表示し削除処理実行
      If typMsgBoxResult.RESULT_OK = ComMessageBox("表示中の明細を削除（出庫戻し）しますか？" _
                                                  , PRG_TITLE _
                                                  , typMsgBox.MSG_NORMAL _
                                                  , typMsgBoxButton.BUTTON_OKCANCEL) Then

        Try
          ' 削除（中身はフラグ更新）
          Call DeleteDb()

          ' 自動検索OFF
          Controlz(DG2V1.Name).AutoSearch = False

          ' 出荷日・担当者以外の全項目をクリア
          MyBase.AllClear(New List(Of Control)({Me.TxtSyukoDate, Me.CmbMstStaff1}))

          ' 選択内容をクリア
          _SelectedData.Clear()

          ' 出荷日にフォーカスを当てる
          Me.TxtSyukoDate.Focus()

        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        Finally
          ' 再表示
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

    ' 出庫明細を表示
    Call ShowOutStockDetailList()

    If typMsgBoxResult.RESULT_OK = ComMessageBox("出庫情報一覧を画面表示しますか？" _
                                                , PRG_TITLE _
                                                , typMsgBox.MSG_NORMAL _
                                                , typMsgBoxButton.BUTTON_OKCANCEL) Then
      Try
        With tmpDb
          ' ワークテーブル削除
          .Execute(SqlDelWK_SYUKO())

          ' Gridに表示中のデータをワークテーブルに書き込み
          For Each tmpRowData As Dictionary(Of String, String) In Controlz(DG2V1.Name).GetAllData
            .Execute(SqlInsWkTblFromGrid(tmpRowData))
          Next

          .DbDisconnect()

          ' ACCESSの出庫情報一覧表レポートを表示
          ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_SYUKO")

        End With

      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      Finally
        tmpDb.Dispose()
      End Try
    End If
  End Sub

  ''' <summary>
  ''' 終了ボタン押下時処理
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
    Me.TxtSyukoDate.Text = ComGetProcDate()

    ' 画面非表示
    Me.Hide()

    ' 出庫明細を表示
    Call ShowOutStockDetailList()


  End Sub

#End Region

#Region "フォーム"

  ''' <summary>
  ''' 画面起動時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_SYUKO_Load(sender As Object, e As EventArgs) Handles Me.Load

    Me.Text = PRG_TITLE

    Call InitForm02()
    Me.TxtSyukoDate.Text = Now().ToString("yyyy/MM/dd")

    ' グリッドダブルクリック時処理追加
    Controlz(DG2V1.Name).lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick
    Controlz(DG2V2.Name).lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick

    ' グリッド再表示時処理
    Controlz(DG2V1.Name).lcCallBackReLoadData = AddressOf DgvReload
    Controlz(DG2V2.Name).lcCallBackReLoadData = AddressOf DgvReload

    ' 出庫明細表示
    Controlz(DG2V1.Name).AutoSearch = True

    ' 基本コントロール以外のメッセージを設定
    TxtSyukoDate.SetMsgLabelText("出庫日（納品日）を入力します。")                           ' 出荷日
    TxtKakouDate.SetMsgLabelText("加工日を入力します。年月日を6桁数字で入力します。")        ' 加工日
    TxtCartonNumber1.SetMsgLabelText("カートン番号（連番）を入力します。カートン番号が無い場合は入力せずにEnterを押して下さい。")   ' カートンNo
    txtSyukoCount.SetMsgLabelText("出庫数を入力します。")                                    ' 出庫数
    TxtUnitPrice.SetMsgLabelText("Kg当りの売単価を入力します。")                             ' Kg売単価
    Controlz(DG2V1.Name).SetMsgLabelText("出庫明細を修正したいときは明細行を選びＥｎｔｅｒを押して下さい。")
    Controlz(DG2V2.Name).SetMsgLabelText("出庫したい明細行を選びＥｎｔｅｒを押して下さい。")

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)

    ' IPC通信起動
    InitIPC(PRG_ID)

    ' 出庫日にフォーカスをあてる
    Me.TxtSyukoDate.Focus()
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
    Me.lblGridStat.Text = IIf(sender.Equals(DG2V1), "出庫明細  ", "在庫一覧  ") & "件数：" & DataCount.ToString()
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
    _SelectedData = Controlz(sender.Name).SelectedRow

    If DG2V1.Visible Then
      ' 出荷明細表示時は選択された得意先を編集コントロールに設定
      Me.CmbMstCustomer1.SelectedValue = Integer.Parse(_SelectedData("UTKCODE"))          ' 得意先
    End If

    Me.CmbMstCattle1.SelectedValue = Integer.Parse(_SelectedData("SYUBETUC"))             ' 畜種
    Me.CmbMstOriginPlace1.SelectedValue = Integer.Parse(_SelectedData("GENSANCHIC"))      ' 原産地
    Me.CmbMstRating1.SelectedValue = Integer.Parse(_SelectedData("KAKUC"))                ' 格付
    Me.TxtEdaban1.Text = _SelectedData("EBCODE")                                          ' 枝番
    Me.TxtKotaiNo1.Text = _SelectedData("KOTAINO")                                        ' 個体識別番号
    Me.TxtKakouDate.Text = Date.Parse(_SelectedData("KAKOUBI")).ToString("yyyy/MM/dd")    ' 加工日
    Me.TxtLblMstItem1.CodeTxt = _SelectedData("BICODE")                               ' 部位
    Me.TxtCartonNumber1.Text = _SelectedData("TOOSINO")                                   ' カートンNo
    Me.TxtWeitghtKg1.Text = ((Math.Floor(Long.Parse(_SelectedData("JYURYO")))) / 1000).ToString          ' 重量
    Me.txtSyukoCount.Text = _SelectedData("HONSU")                                        ' 本数
    Me.TxtUnitPrice.Text = _SelectedData("TANKA")                                         ' 売単価
    Me.CmbMstSetType1.SelectedValue = Integer.Parse(_SelectedData("SETCD"))               ' セットコード

    ' 一覧の自動検索を停止
    Controlz(DG2V1.Name).AutoSearch = False

    ' 出庫明細を表示
    Call ShowOutStockDetailList()

    ' 編集不可コントロール設定
    Call LockCtrl()
  End Sub


#End Region

#Region "テキストボックス"

  ''' <summary>
  ''' 出荷日フォーカス取得時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtSyukoDate_GotFocus(sender As Object, e As EventArgs) Handles TxtSyukoDate.GotFocus

    ' 日付形式文字列が入力されているなら出荷日以外の入力内容をクリア
    ' ※マウスでのフォーカス移動時に日付エラーが発生した場合の対応
    If Me.TxtSyukoDate.Text.Trim = "" Then
      Me.TxtSyukoDate.Text = ComGetProcDate()
    End If
    If DirectCast(sender, TxtDateBase).HasError = False Then
      ' 一覧の自動検索を停止
      Controlz(DG2V1.Name).AutoSearch = False

      ' 出荷日・担当者以外の全項目をクリア
      MyBase.AllClear(New List(Of Control)({Me.TxtSyukoDate, Me.CmbMstStaff1}))

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
  Private Sub TxtSyukoDateValidating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtSyukoDate.Validating
    ' 未入力なら本日日付を設定する
    With Me.TxtSyukoDate
      If .Text.Length <= 0 Then
        .Text = ComGetProcDate()
      End If
    End With
  End Sub



#End Region

#End Region

End Class
