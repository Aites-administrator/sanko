Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass

Public Class Form_Tanasiji
  '----------------------------------------------
  '                 棚卸指示処理
  ' 
  '
  '----------------------------------------------

#Region "定数定義"

  ''' <summary>
  ''' プログラムID
  ''' </summary>
  ''' <remarks>
  ''' システム内でユニーク
  ''' 二重起動防止とIPC通信に使用
  ''' </remarks>
  Private Const PRG_ID As String = "tanasiji"

  ''' <summary>
  ''' プログラムタイトル
  ''' </summary>
  Private Const PRG_TITLE As String = "棚卸指示処理"

#End Region

#Region "スタートアップ"

  <STAThread>
  Shared Sub main()

    ' 二重起動防止のみ
    Call ComStartPrg(PRG_ID, Form_Tanasiji)
  End Sub

#End Region

#Region "メソッド"

#Region "プライベート"

#Region "SQL実行関連"

  ''' <summary>
  ''' 最終棚卸日取得
  ''' </summary>
  Private Sub GetLastTanaorosibi()
    Dim tmpDb As New clsSqlServer()
    Dim tmpDt As New DataTable

    Try
      ' TANAOROSIテーブルより最終の棚卸日を取得
      tmpDb.GetResult(tmpDt, SqlSelLastOrosibi)

      If tmpDt.Rows.Count <= 0 _
          OrElse ComChkDateLimit(tmpDt.Rows(0)("LAST_OROSI").ToString, ComGetProcDate(), 10, 10) = False Then
        ' データが存在しない、もしくは本日日付±10日の範囲外
        Me.txtTanaorosiDate.Text = ComGetProcDate() ' 本日日付を設定
      Else
        Me.txtTanaorosiDate.Text = Date.Parse(tmpDt.Rows(0)("LAST_OROSI").ToString()).ToString("yyyy/MM/dd")
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("棚卸日の取得に失敗しました。")
    Finally
      tmpDb.DbDisconnect()
      tmpDt.Dispose()
    End Try

  End Sub

  ''' <summary>
  ''' 棚卸指示データ作成
  ''' </summary>
  ''' <remarks>
  '''  CUTJの在庫データを棚卸対象のデータとしてTANAOROSIに挿入する
  '''  以下が対象
  '''  ・計量日が画面で入力された棚卸日付より前日付（棚卸日を含む）
  '''  ・出荷日が画面で入力された棚卸日付より後日付（棚卸日を含まない）
  ''' </remarks>
  Private Sub CreateInstructionData()
    Dim tmpDb As New clsSqlServer
    Dim tmpProcTime As String = ComGetProcTime()

    Try

      With tmpDb

        ' WorkTable作成
        .Execute(SqlCreateWorkTbl)

        .TrnStart()

        ' 棚卸テーブルよりWorkTableにデータ挿入
        .Execute("INSERT INTO #WK_TANAOROSI SELECT * FROM TANAOROSI WHERE OROSIBI =" & TanaorosiBi())

        ' 棚卸指示データ作成(WorkTable)
        .Execute(SqlInsTANAOROSIFromCUTJ(tmpProcTime))

        ' 棚卸指示データ更新(WorkTable)
        .Execute(SqlUpdTANAOROSIFromCUTJ(tmpProcTime))

        ' 棚卸指示日データ削除
        .Execute("DELETE FROM TANAOROSI WHERE OROSIBI = " & TanaorosiBi())

        ' WorkTableから棚卸テーブルにデータ挿入
        .Execute("INSERT INTO TANAOROSI SELECT * FROM #WK_TANAOROSI ")

        .TrnCommit()
      End With

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
      Throw New Exception("棚卸指示データ作成に失敗しました。")
    End Try

  End Sub

  ''' <summary>
  ''' 棚卸データ反映
  ''' </summary>
  ''' <remarks>
  ''' TANAOROSIをCUTJの在庫データに反映する
  ''' CUTJの以下の項目が対象
  ''' ・機械番号
  ''' ・極性フラグ
  ''' ・本数
  ''' ・重量
  ''' ・担当
  ''' ・入出庫状態
  ''' </remarks>
  Private Sub ReflectingData()
    Dim tmpDb As New clsSqlServer
    Dim tmpProcTime As String = ComGetProcTime()

    Try
      With tmpDb
        .TrnStart()

        ' CUTJ更新
        .Execute(SqlUpdCUTJFromTANAOROSI(tmpProcTime))

        ' TANAOROSI更新
        .Execute(SqlUpdTANAOROSI())

        .TrnCommit()
      End With
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
      Throw New Exception("確定実行に失敗しました。")
    End Try
  End Sub
#End Region

#Region "SQL文関連"

  ''' <summary>
  ''' CUTJ → TANAOROSI追加 SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsTANAOROSIFromCUTJ(prmProcTime As String) As String
    Dim sql As String = String.Empty
    Dim tmpDic As New Dictionary(Of String, String)
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

#Region "CUTJ → TANAOROSIの項目対応作成"
    With tmpDic
      .Add("KIKAINO", "KIKAINO")
      .Add("KEIRYOBI", "KEIRYOBI")
      .Add("KAKOUBI", "KAKOUBI")
      .Add("GBFLG ", "GBFLG ")
      .Add("SRCODE", "SRCODE")
      .Add("EBCODE", "EBCODE")
      .Add("KOTAINO", "KOTAINO")
      .Add("BICODE ", "BICODE ")
      .Add("SHOHINC", "SHOHINC")
      .Add("SAYUKUBUN ", "SAYUKUBUN ")
      .Add("TOOSINO ", "TOOSINO ")
      .Add("JYURYO", "JYURYO")
      .Add("HONSU ", "HONSU ")
      .Add("GENKA ", "GENKA ")
      .Add("TANKA ", "TANKA ")
      .Add("KINGAKU", "KINGAKU")
      .Add("SPKubun ", "SPKubun ")
      .Add("SETCD", "SETCD")
      .Add("TKCODE", "TKCODE")
      .Add("TANTO", "TANTO")
      .Add("KIKAKUC", "KIKAKUC")
      .Add("KAKUC", "KAKUC")
      .Add("KIGENBI", "KIGENBI")
      .Add("GENSANCHIC", "GENSANCHIC")
      .Add("SYUBETUC", "SYUBETUC")
      .Add("RHONSU", "HONSU")
      .Add("RJYURYO", "JYURYO")
      .Add("OROSIKUBUN", "IIF(NSZFLG <= 1,'0','1')")
      .Add("OROSIBI", TanaorosiBi())
      .Add("KYOKUFLG", "0")
      .Add("KFLG", "0")
      .Add("TDATE", "'" & prmProcTime & "'")
      .Add("KDATE", "'" & prmProcTime & "'")

    End With

    For Each tmpKey As String In tmpDic.Keys
      tmpDst &= tmpKey & " ,"
      tmpVal &= tmpDic(tmpKey) & " ,"
    Next

    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)
#End Region

    sql &= " INSERT INTO #WK_TANAOROSI(" & tmpDst & ")"
    sql &= " SELECT " & tmpVal
    sql &= " FROM CUTJ "
    sql &= " WHERE " & SqlExtractionConditionCUTJ() ' CUTJ抽出条件追加
    sql &= "   AND NOT EXISTS( SELECT * "
    sql &= "                   FROM #WK_TANAOROSI AS T2 "
    sql &= "                   WHERE " & SqlExtractionConditionTANAOROSI("T2")  ' TANAOROSI抽出条件追加
    sql &= "                     AND " & SqlJoinConditionCUTJ2TANAOROSI("T2")   ' CUTJ⇔TANAOROSI結合条件追加
    sql &= "                     AND T2.BICODE = CUTJ.BICODE "                  ' 準備実行時は部位コードを結合条件に含む
    sql &= "                 ) "

    Return sql
  End Function

  ''' <summary>
  ''' CUTJ → TANAOROSI更新 SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdTANAOROSIFromCUTJ(prmProcTime As String) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE #WK_TANAOROSI "
    sql &= " SET RHONSU = CUTJ.HONSU "
    sql &= "   , RJYURYO = CUTJ.JYURYO "
    sql &= "   , KDATE ='" & prmProcTime & "' "
    sql &= " FROM #WK_TANAOROSI "
    sql &= "       INNER JOIN CUTJ ON #WK_TANAOROSI.BICODE = CUTJ.BICODE "                ' 準備実行時は部位コードを結合条件に含む
    sql &= "                 AND " & SqlJoinConditionCUTJ2TANAOROSI(prmAliasTANAOROSI:="#WK_TANAOROSI")                 ' CUTJ⇔TANAOROSI結合条件追加
    sql &= "                 AND " & SqlExtractionConditionTANAOROSI("#WK_TANAOROSI")     ' TANAOROSI抽出条件追加
    sql &= "                 AND " & SqlExtractionConditionCUTJ("CUTJ")               ' CUTJ抽出条件追加
    sql &= "                 AND ( #WK_TANAOROSI.RHONSU <> CUTJ.HONSU "
    sql &= "                     OR #WK_TANAOROSI.RJYURYO <> CUTJ.JYURYO ) "


    Return sql
  End Function

  ''' <summary>
  ''' TANAOROSI → CUTJ更新 SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCUTJFromTANAOROSI(prmProcTime As String) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET KDATE = '" & prmProcTime & "'"
    sql &= "   , KIKAINO = TANAOROSI.KIKAINO "
    sql &= "   , KYOKUFLG = TANAOROSI.KYOKUFLG "
    sql &= "   , HONSU = TANAOROSI.HONSU "
    sql &= "   , JYURYO = TANAOROSI.JYURYO "
    sql &= "   , TANTO = TANAOROSI.TANTO "
    sql &= "   , NSZFLG = IIF(TANAOROSI.OROSIKUBUN = 2 ,3 "
    sql &= "            , IIF(TANAOROSI.OROSIKUBUN = 3 ,4 "
    sql &= "            , IIF(TANAOROSI.OROSIKUBUN = 4 ,2 ,NSZFLG)))"
    sql &= " FROM CUTJ AS C2 "
    sql &= "        INNER JOIN TANAOROSI ON TANAOROSI.SHOHINC = C2.SHOHINC "       ' 確定実行時は商品コードを結合条件に含む
    sql &= "          AND  " & SqlJoinConditionCUTJ2TANAOROSI(prmAliasCUTJ:="C2")  ' CUTJ⇔TANAOROSI結合条件追加
    sql &= " WHERE " & SqlExtractionConditionTANAOROSI("TANAOROSI")                ' TANAOROSI抽出条件追加
    sql &= "   AND " & SqlExtractionConditionCUTJ("C2")                            ' CUTJ抽出条件追加

    Return sql
  End Function

  ''' <summary>
  ''' TANAOROSI更新 SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 確定実行済のフラグ更新
  ''' </remarks>
  Private Function SqlUpdTANAOROSI() As String
    Dim sql As String = String.Empty

    sql &= " UPDATE TANAOROSI "
    sql &= " SET KFLG = 1 "
    sql &= " WHERE KFLG = 0 "
    sql &= "   And " & SqlExtractionConditionTANAOROSI()

    Return sql
  End Function

  ''' <summary>
  ''' 棚卸データ作成用テンポラリーテーブルを作成するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlCreateWorkTbl() As String
    Dim sql As String = String.Empty

    sql &= "  DROP TABLE IF EXISTS #WK_TANAOROSI;"

    sql &= "	CREATE TABLE #WK_TANAOROSI (  "
    sql &= "  [OROSIBI] [datetime] NOT NULL, "
    sql &= "	[OROSIKUBUN] [numeric](2, 0) NULL, "
    sql &= "	[KIKAINO] [numeric](4, 0) NULL, "
    sql &= "	[KYOKUFLG] [numeric](2, 0) NULL, "
    sql &= "	[KEIRYOBI] [datetime] NOT NULL, "
    sql &= "	[KAKOUBI] [datetime] NULL, "
    sql &= "	[GBFLG] [numeric](2, 0) NOT NULL, "
    sql &= "	[SRCODE] [numeric](6, 0) NOT NULL, "
    sql &= "	[EBCODE] [numeric](6, 0) NOT NULL, "
    sql &= "	[KOTAINO] [numeric](10, 0) NULL, "
    sql &= "	[BICODE] [numeric](6, 0) NOT NULL, "
    sql &= "	[SHOHINC] [numeric](8, 0) NULL, "
    sql &= "	[SAYUKUBUN] [numeric](2, 0) NOT NULL, "
    sql &= "	[TOOSINO] [numeric](6, 0) NOT NULL, "
    sql &= "	[JYURYO] [numeric](7, 0) NULL, "
    sql &= "	[HONSU] [numeric](3, 0) NULL, "
    sql &= "	[GENKA] [numeric](8, 0) NULL, "
    sql &= "	[TANKA] [numeric](6, 0) NULL, "
    sql &= "	[KINGAKU] [numeric](8, 0) NULL, "
    sql &= "	[SPKUBUN] [numeric](2, 0) NULL, "
    sql &= "	[SETCD] [numeric](6, 0) NULL, "
    sql &= "	[TKCODE] [numeric](7, 0) NULL, "
    sql &= "	[TANTO] [numeric](6, 0) NULL, "
    sql &= "	[KFLG] [numeric](2, 0) NULL, "
    sql &= "	[TDATE] [datetime] NULL, "
    sql &= "	[KDATE] [datetime] NULL, "
    sql &= "	[KIKAKUC] [numeric](4, 0) NULL, "
    sql &= "	[KAKUC] [numeric](4, 0) NULL, "
    sql &= "	[KIGENBI] [numeric](8, 0) NULL, "
    sql &= "	[GENSANCHIC] [numeric](4, 0) NULL, "
    sql &= "	[SYUBETUC] [numeric](4, 0) NULL, "
    sql &= "	[RHONSU] [numeric](2, 0) NULL, "
    sql &= "	[RJYURYO] [numeric](7, 0) NULL "
    sql &= " )  "


    Return sql

  End Function

  ''' <summary>
  ''' 最終の棚卸実行日を取得するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelLastOrosibi() As String
    Dim sql As String = String.Empty


    sql &= " SELECT MAX(OROSIBI) AS LAST_OROSI "
    sql &= " FROM TANAOROSI "

    Return sql
  End Function

  ''' <summary>
  ''' CUTJ抽出条件文作成
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks>
  '''  以下が対象
  '''   画面で入力された棚卸日以前に計量された在庫データ（棚卸日を含む）
  '''   画面で入力された棚卸日以降に出庫された出庫データ（棚卸日を含まない）
  ''' </remarks>
  Private Function SqlExtractionConditionCUTJ(Optional prmAlias As String = "") As String
    Dim sql As String = String.Empty

    With prmAlias
      If .Length > 0 AndAlso .Substring(.Length - 1, 1) <> "." Then
        prmAlias &= "."
      End If
    End With

    sql &= "     " & prmAlias & "KEIRYOBI <= " & TanaorosiBi()
    sql &= " And " & prmAlias & "KYOKUFLG = 0 "
    sql &= " And " & prmAlias & "DKUBUN = 0 "
    sql &= " And ( " & prmAlias & "NSZFLG <= 1 "
    sql &= "        Or (" & prmAlias & "NSZFLG <= 4 And " & prmAlias & "SYUKKABI > " & TanaorosiBi() & ") "
    sql &= "     ) "

    'sql &= " And " & prmAlias & "KOTAINO Is Not NULL "
    sql &= " And " & prmAlias & "KUBUN <> 9 "


    Return sql
  End Function

  ''' <summary>
  ''' TANAOROSI抽出条件文作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlExtractionConditionTANAOROSI(Optional prmAlias As String = "") As String
    Dim sql As String = String.Empty

    With prmAlias
      If .Length > 0 AndAlso .Substring(.Length - 1, 1) <> "." Then
        prmAlias &= "."
      End If
    End With


    sql &= "     " & prmAlias & "OROSIBI = " & TanaorosiBi()
    sql &= " And " & prmAlias & "KEIRYOBI <= " & TanaorosiBi()

    Return sql

  End Function

  ''' <summary>
  ''' CUTJとTANAOROSIの結合条件文作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlJoinConditionCUTJ2TANAOROSI(Optional prmAliasTANAOROSI As String = "TANAOROSI." _
                                                  , Optional prmAliasCUTJ As String = "CUTJ.") As String
    Dim sql As String = String.Empty

    With prmAliasTANAOROSI
      If .Substring(.Length - 1, 1) <> "." Then
        prmAliasTANAOROSI &= "."
      End If
    End With

    With prmAliasCUTJ
      If .Substring(.Length - 1, 1) <> "." Then
        prmAliasCUTJ &= "."
      End If
    End With

    sql &= prmAliasTANAOROSI & "GBFLG = " & prmAliasCUTJ & "GBFLG "
    sql &= " And " & prmAliasTANAOROSI & "SRCODE = " & prmAliasCUTJ & "SRCODE "
    sql &= " And " & prmAliasTANAOROSI & "TOOSINO = " & prmAliasCUTJ & "TOOSINO "
    sql &= " And " & prmAliasTANAOROSI & "KOTAINO = " & prmAliasCUTJ & "KOTAINO "
    sql &= " And " & prmAliasTANAOROSI & "EBCODE = " & prmAliasCUTJ & "EBCODE "
    sql &= " And " & prmAliasTANAOROSI & "KAKOUBI = " & prmAliasCUTJ & "KAKOUBI "
    Return sql


  End Function
#End Region

#Region "コントロール制御関連"

  ''' <summary>
  ''' 画面に入力されている棚卸日を返す
  ''' </summary>
  ''' <returns>画面に入力されている棚卸日</returns>
  Private Function TanaorosiBi(Optional prmDelimiter As String = "'") As String
    Dim ret As String = Me.txtTanaorosiDate.Text
    Dim tmpDate As Date

    If Date.TryParse(ret, tmpDate) Then
      Return prmDelimiter & ret & prmDelimiter
    Else
      Throw New Exception("日付が正しくありません。入力し直して下さい。")
    End If
  End Function
#End Region
#End Region

#End Region

#Region "イベントプロシージャー"

#Region "フォーム"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Tanasiji_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    ' タイトル設定
    Me.Text = PRG_TITLE

    ' 最終棚卸日取得
    Call GetLastTanaorosibi()

  End Sub

#End Region

#Region "ボタン"

  ''' <summary>
  ''' 準備実行ボタン押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnPreparation_Click(sender As Object, e As EventArgs) Handles btnPreparation.Click

    Try
      If typMsgBoxResult.RESULT_OK _
          = ComMessageBox("棚卸指示用データを作成しますか？", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OKCANCEL) Then

        If ComChkDateLimit(TanaorosiBi(""), ComGetProcDate, 10, 10) = False Then
          Call ComMessageBox("日付範囲が不正です", PRG_TITLE, typMsgBox.MSG_ERROR)
        Else
          ' 棚卸指示データ作成
          Call CreateInstructionData()
          ComMessageBox("処理を正常に終了しました。", PRG_TITLE, typMsgBox.MSG_NORMAL)

        End If

      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try

  End Sub

  ''' <summary>
  ''' 確定実行ボタン押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnReflect_Click(sender As Object, e As EventArgs) Handles btnReflect.Click
    Try

      If typMsgBoxResult.RESULT_OK _
          = ComMessageBox("棚卸データ在庫に更新、反映しますか？", PRG_TITLE, typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OKCANCEL) Then

        If ComChkDateLimit(TanaorosiBi(""), ComGetProcDate, 10, 28) = False Then
          Call ComMessageBox("日付範囲が不正です", PRG_TITLE, typMsgBox.MSG_ERROR)
        Else
          ' 確定処理実行
          Call ReflectingData()
          ComMessageBox("処理を正常に終了しました。", PRG_TITLE, typMsgBox.MSG_NORMAL)
        End If

      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try
  End Sub

  ''' <summary>
  ''' 閉じるボタン押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

    Me.Close()

  End Sub

#End Region

#Region "テキストボックス"

#End Region

#End Region

End Class
