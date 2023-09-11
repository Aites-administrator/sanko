Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports T.R.ZCommonClass.clsDataGridSearchControl

Imports System

Public Class Form_TanaPrn

#Region "定数定義"

  Private Const PRG_ID As String = "TanaPrn1"

  'ダブルクォーテーション
  Private Const CSV_SPACE As String = ControlChars.Quote

  ' カンマ
  Private Const CSV_COMMA As String = ","

#End Region

#Region "メンバ"
#Region "プライベート"
  '1:明細、２：データ出力
  Private inMode As Integer
#End Region

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_TanaPrn)
  End Sub
#End Region

#Region "メソッド"

#Region "プライベート"
  ''' <summary>
  ''' Object型から整数型への変換
  ''' </summary>
  ''' <param name="prmTargetObj"></param>
  ''' <returns></returns>
  Private Function DTConvLong(prmTargetObj As Object) As Long

    Dim ret As Long = 0

    If prmTargetObj IsNot Nothing Then
      Long.TryParse(prmTargetObj.ToString, ret)
    End If

    Return ret

  End Function

  ''' <summary>
  ''' Object型から倍精度小数点型への変換
  ''' </summary>
  ''' <param name="prmTargetObj"></param>
  ''' <returns></returns>
  Private Function DTConvDouble(prmTargetObj As Object) As Double

    Dim ret As Double = 0

    If prmTargetObj IsNot Nothing Then
      Double.TryParse(prmTargetObj.ToString, ret)
    End If

    Return ret

  End Function

  ''' <summary>
  ''' Object型から日付型への変換
  ''' </summary>
  ''' <param name="prmTargetObj"></param>
  ''' <returns></returns>
  Private Function DTConvDateTime(prmTargetObj As Object) As DateTime

    Dim ret As DateTime

    If prmTargetObj IsNot Nothing Then
      DateTime.TryParse(prmTargetObj.ToString, ret)
    End If

    Return ret

  End Function

  ''' <summary>
  ''' 集計データ作成
  ''' </summary>
  ''' <param name="syoriNo"></param>
  Private Function Data_Set(syoriNo As Integer) As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True

    ' 実行
    With tmpDb
      Try

        ' SQL文の作成
        sql = SqlTanaoroshiOpen(syoriNo)
        Dim tmpDt As New DataTable
        Call .GetResult(tmpDt, sql)

        .TrnStart()

        'プログラスバー幅設定
        PrgBar.Width = Me.Width
        'プログラスバー最大値設定
        PrgBar.Maximum = 50
        'プログラスバー表示
        PrgBar.Visible = True
        PrgBar.Value = 0

        Dim dtRow As DataRow

        ' ファイルレコード → DataRow
        For i = 0 To tmpDt.Rows.Count - 1

          'プログラスバーの描画
          PrgBar.Value = PrgBar.Value + 1
          If PrgBar.Value >= PrgBar.Maximum Then
            PrgBar.Value = 1
          End If

          dtRow = tmpDt.Rows(i)

        Next i

        ' データ件数が０件の場合
        If (tmpDt.Rows.Count = 0) Then
          ComMessageBox("該当するデータが存在しません。",
                        "棚卸一覧表", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
          ret = False
        Else

          Try
            .TrnCommit()

            ' アプリケーション名設定
            If (inMode = 1) Then

              WorkTbl_Output(tmpDt)

            Else

              ' 棚卸データ出力
              outputCsvTanaorosi(tmpDt)

            End If

            .Dispose()

          Catch ex As Exception
            .TrnRollBack()
              Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
            End Try

          End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）

        ret = False
      Finally
        'プログラスバー非表示
        PrgBar.Visible = False
      End Try

      Return ret

    End With

  End Function

  ''' <summary>
  ''' ACCESSレポートへの出力
  ''' </summary>
  Private Sub WorkTbl_Output(tmpDt As DataTable)

    Dim rptDb As New clsReport(clsGlobalData.REPORT_FILENAME)

    With rptDb
      Try

        ' ACCESS集計テーブルの削除
        .Execute("DELETE FROM WK_CUTJ")

        .TrnStart()

        ' 集計用ワークテーブルからの取得
        Dim sql As String = String.Empty
        For i = 0 To tmpDt.Rows.Count - 1

          'プログラスバーの描画
          PrgBar.Value = PrgBar.Value + 1
          If PrgBar.Value >= PrgBar.Maximum Then
            PrgBar.Value = 1
          End If

          sql = SqlInsDataAccess(tmpDt.Rows(i))
          .Execute(sql)
        Next

        .TrnCommit()

        ' ACCESSの在庫一覧表レポートを表示
        ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_TANAOROSIMEI")


      Catch ex As Exception
        .TrnRollBack()
        Call ComWriteErrLog(ex)
        Throw New Exception("ACCESS集計テーブの書き込みに失敗しました")
      Finally
        .Dispose()
      End Try
    End With
  End Sub

  ''' <summary>
  ''' 棚卸一覧表データ出力
  ''' </summary>
  ''' <param name="tmpDt"></param>
  ''' <returns></returns>
  Private Function outputCsvTanaorosi(tmpDt As DataTable) As Boolean

    Dim ret As Boolean = True
    'ファイルStreamWriter
    Dim sw As System.IO.StreamWriter = Nothing

    ' 実行
    Try

      'CSVファイル書込に使うEncoding
      Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

      '自分自身の実行ファイルのパスを取得する
      Dim appPath As String = System.Environment.CurrentDirectory

      '書き込むファイルを開く
      sw = New System.IO.StreamWriter(System.IO.Path.Combine(appPath, "TANAOROSI.CSV"), False, enc)

      Dim KDATE As String
      Dim dtRow As DataRow
      Dim GYO As Integer = 1
      Dim wLINE As Integer = 0
      Dim wSETCD As Long = -1
      Dim wSAYU As Long = 0
      Dim pKEIRYOBI As Date = Nothing
      Dim pKAKOUBI As Date = Nothing
      Dim pSRCODE As Long = 0
      Dim pLSRNAME As String = ""
      Dim pSBNAME As String = ""
      Dim pBINAME As String = ""
      Dim pEBCODE As Long = 0
      Dim pKOTAINO As Long = 0
      Dim pJYURYO As Double = 0.0
      Dim pGENKA As Decimal = 0
      Dim pKINGAKU As Decimal = 0
      Dim pGNNAME As String = ""
      Dim pLTKNAME As String = ""
      Dim wGBFLG As Long = 0
      Dim wSYUBETUC As Long = 0

      KDATE = String.Empty

      sw.Write(CSV_SPACE & "棚卸し日" & CSV_SPACE & CSV_COMMA)

      sw.Write(CSV_SPACE & CmbDateTanaorosiBi1.Text & CSV_SPACE)

      '改行
      sw.Write(vbCrLf)

      sw.Write(CSV_SPACE & "入庫日" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "加工日" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "仕入先コード" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "仕入先名" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "畜種" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "品名" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "枝番" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "個体識別" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "重量" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "単価" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "金額" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "原産地" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "仕向先" & CSV_SPACE)
      '改行
      sw.Write(vbCrLf)

      If (1 <= tmpDt.Rows.Count) Then
        For i = 0 To tmpDt.Rows.Count - 1

          'プログラスバーの描画
          PrgBar.Value = PrgBar.Value + 1
          If PrgBar.Value >= PrgBar.Maximum Then
            PrgBar.Value = 1
          End If

          dtRow = tmpDt.Rows(i)

          If wGBFLG <> DTConvLong(dtRow("GBFLG")) Or
             wSETCD <> DTConvLong(dtRow("SETCD")) Or
             wSYUBETUC <> DTConvLong(dtRow("SYUBETUC")) Or
             pSRCODE <> DTConvLong(dtRow("SRCODE")) Or
             pKOTAINO <> DTConvLong(dtRow("KOTAINO")) Or
             wSAYU <> DTConvLong(dtRow("SAYUKUBUN")) Then
            If (pJYURYO > 0) Then
              sw.Write(pKEIRYOBI.ToString("yyyy/MM/dd") & CSV_COMMA)
              sw.Write(pKAKOUBI.ToString("yyyy/MM/dd") & CSV_COMMA)
              sw.Write(pSRCODE.ToString & CSV_COMMA)
              sw.Write(pLSRNAME & CSV_COMMA)
              sw.Write(pSBNAME & CSV_COMMA)
              sw.Write(pBINAME & CSV_COMMA)
              sw.Write(pEBCODE.ToString & CSV_COMMA)
              sw.Write(pKOTAINO.ToString.PadLeft(10, "0"c) & CSV_COMMA)
              sw.Write(pJYURYO.ToString("#,##0.0") & CSV_COMMA)
              sw.Write(pGENKA.ToString & CSV_COMMA)
              sw.Write(pKINGAKU.ToString & CSV_COMMA)
              sw.Write(pGNNAME & CSV_COMMA)
              sw.Write(pLTKNAME)
              '改行
              sw.Write(vbCrLf)
            End If
            pJYURYO = 0
            pGENKA = 0
            pKINGAKU = 0
          End If

          pKEIRYOBI = DTConvDateTime(dtRow("KEIRYOBI"))
          pKAKOUBI = DTConvDateTime(dtRow("KAKOUBI"))
          pSRCODE = DTConvLong(dtRow("SRCODE"))
          pLSRNAME = dtRow("LSRNAME").ToString
          pSBNAME = dtRow("SBNAME").ToString
          pBINAME = dtRow("BINAME").ToString

          If wSETCD > 0 Or wSETCD = -1 Then
            pBINAME = "セット品"
          End If
          If DTConvLong(dtRow("SAYUKUBUN")) = 1 Then
            pBINAME = pBINAME & " 左"
          ElseIf DTConvLong(dtRow("SAYUKUBUN")) = 2 Then
            pBINAME = pBINAME & " 右"
          End If

          pEBCODE = DTConvLong(dtRow("EBCODE"))
          pKOTAINO = DTConvLong(dtRow("KOTAINO"))

          pJYURYO = DTConvDouble(pJYURYO + (DTConvLong(dtRow("JYURYO")) \ 100) / 10)
          '     pKINGAKU = pKINGAKU + CDec(Math.Floor(((DTConvLong(dtRow("JYURYO")) \ 100) / 10) * DTConvLong(dtRow("GENKA"))))
          pKINGAKU = pKINGAKU + CDec(Math.Floor((DTConvLong(dtRow("GENKA")) * Math.Floor(DTConvDouble(dtRow("JYURYO")) / 100) / 10)))

          If CType(pJYURYO, Integer) = 0 Then
            pGENKA = 0
          Else
            pGENKA = CDec(Math.Floor((pKINGAKU / Math.Floor(pJYURYO * 10)) * 10))
          End If
          pGNNAME = dtRow("GNNAME").ToString
          pLTKNAME = dtRow("LTKNAME").ToString
          wGBFLG = DTConvLong(dtRow("GBFLG"))
          wSETCD = DTConvLong(dtRow("SETCD"))
          wSYUBETUC = DTConvLong(dtRow("SYUBETUC"))
          wSAYU = DTConvLong(dtRow("SAYUKUBUN"))
          GYO = GYO + 1
          wLINE = wLINE + 1
        Next
      End If

      ' 棚卸一覧表データをEXCELで開く
      Dim officeFileProc As New Process
      With officeFileProc
        .StartInfo.FileName = System.IO.Path.Combine(appPath, "TANAOROSI.CSV")
        .Start()
      End With

    Catch ex As Exception
      'エラー
      MsgBox(ex.Message)
      ret = False
    Finally
      '閉じる
      If sw IsNot Nothing Then
        sw.Close()
      End If
    End Try

    Return ret

  End Function

  ''' <summary>
  ''' ACESSレポート更新用SQL文作成
  ''' </summary>
  ''' <param name="dtRow"></param>
  ''' <returns>SQL文</returns>
  Private Function SqlInsDataAccess(dtRow As DataRow) As String

    Dim tmpKeyVal As Dictionary(Of String, String) = TargetValNewCutJVal()


    tmpKeyVal("KIKAINO") = dtRow("KIKAINO").ToString
    tmpKeyVal("KYOKUFLG") = dtRow("KYOKUFLG").ToString
    tmpKeyVal("KEIRYOBI") = "'" & dtRow("KEIRYOBI").ToString & "'"
    tmpKeyVal("KAKOUBI") = "'" & dtRow("KAKOUBI").ToString & "'"
    tmpKeyVal("GBFLG") = dtRow("GBFLG").ToString
    tmpKeyVal("SRCODE") = dtRow("SRCODE").ToString
    tmpKeyVal("EBCODE") = dtRow("EBCODE").ToString
    tmpKeyVal("KOTAINO") = dtRow("KOTAINO").ToString.PadLeft(10, "0"c)
    tmpKeyVal("BICODE") = dtRow("BICODE").ToString
    tmpKeyVal("SHOHINC") = dtRow("SHOHINC").ToString
    tmpKeyVal("SAYUKUBUN") = dtRow("SAYUKUBUN").ToString
    tmpKeyVal("TOOSINO") = dtRow("TOOSINO").ToString

    If (DTConvLong(dtRow("KYOKUFLG")) <> 0) Then
      tmpKeyVal("JYURYO") = "0"
      tmpKeyVal("HONSU") = "0"
    Else
      tmpKeyVal("JYURYO") = dtRow("JYURYO").ToString
      tmpKeyVal("HONSU") = dtRow("HONSU").ToString
    End If
    tmpKeyVal("GENKA") = dtRow("GENKA").ToString
    tmpKeyVal("TANKA") = dtRow("TANKA").ToString
    tmpKeyVal("KINGAKU") = dtRow("KINGAKU").ToString
    tmpKeyVal("SPKUBUN") = dtRow("SPKUBUN").ToString
    tmpKeyVal("SETCD") = dtRow("SETCD").ToString
    tmpKeyVal("TKCODE") = dtRow("TKCODE").ToString
    tmpKeyVal("TANTO") = dtRow("TANTO").ToString
    tmpKeyVal("KFLG") = dtRow("KFLG").ToString
    tmpKeyVal("TDATE") = "'" & dtRow("TDATE").ToString & "'"
    tmpKeyVal("KDATE") = "'" & dtRow("KDATE").ToString & "'"
    tmpKeyVal("KIKAKUC") = dtRow("KIKAKUC").ToString
    tmpKeyVal("KAKUC") = dtRow("KAKUC").ToString
    tmpKeyVal("KIGENBI") = dtRow("KIGENBI").ToString
    tmpKeyVal("GENSANCHIC") = dtRow("GENSANCHIC").ToString
    tmpKeyVal("SYUBETUC") = dtRow("SYUBETUC").ToString
    tmpKeyVal("BINAME") = "'" & dtRow("BINAME").ToString & "'"
    tmpKeyVal("LSRNAME") = "'" & dtRow("LSRNAME").ToString & "'"
    tmpKeyVal("GBNAME") = "'" & dtRow("GBNAME").ToString & "'"
    tmpKeyVal("SBNAME") = "'" & dtRow("SBNAME").ToString & "'"
    tmpKeyVal("TANTOMEI") = "'" & dtRow("TANTOMEI").ToString & "'"
    tmpKeyVal("KKNAME") = "'" & dtRow("KKNAME").ToString & "'"
    tmpKeyVal("KZNAME") = "'" & dtRow("KZNAME").ToString & "'"
    tmpKeyVal("GNNAME") = "'" & dtRow("GNNAME").ToString & "'"
    tmpKeyVal("LTKNAME") = "'" & dtRow("LTKNAME").ToString & "'"
    tmpKeyVal("NSZFLG") = "0"
    tmpKeyVal("JYOUTAI") = "''"
    tmpKeyVal("OROSIBI") = "'" & CmbDateTanaorosiBi1.Text & "'"
    tmpKeyVal("OROSIKUBUN") = dtRow("OROSIKUBUN").ToString
    tmpKeyVal("RHONSU") = dtRow("RHONSU").ToString
    tmpKeyVal("RJYURYO") = dtRow("RJYURYO").ToString
    tmpKeyVal("SYUKKABI") = "NULL"
    tmpKeyVal("HENPINBI") = "NULL"

    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst = tmpDst & tmpKey & " ,"
      tmpVal = tmpVal & tmpKeyVal(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    Dim sql As String = String.Empty

    sql &= "INSERT INTO WK_CUTJ (" & tmpDst & ")"
    sql &= "          VALUES(" & tmpVal & ")"

    Return sql

  End Function

#End Region

#End Region

#Region "イベントプロシージャ"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>to
  ''' <param name="e"></param>
  Private Sub Form_TanaPrn_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Dim prmArg As String = String.Empty

    'コマンドライン引数を取得する
    If My.Application.CommandLineArgs.Count > 0 Then
      prmArg = My.Application.CommandLineArgs(0)
    End If

    ' 引数が1の場合、
    If (prmArg.Equals("2")) Then
      inMode = 2
    Else
      inMode = 1
    End If

    ' アプリケーション名設定
    If (inMode = 1) Then
      Me.Text = "棚卸一覧表(明細)"
    Else
      Me.Text = "棚卸一覧表(データ出力)"
    End If

    ' 最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

    '名称表示用テキストボックスの表示位置を下側に設定
    TxtLblMstGB1.TxtPos = True
    TxtLblMstGB2.TxtPos = True
    TxtLblMstSyubetsu1.TxtPos = True
    TxtLblMstSyubetsu2.TxtPos = True
    TxtLblMstItem1.TxtPos = True
    TxtLblMstItem2.TxtPos = True

    ' プログレスバーの非表示
    PrgBar.Visible = False

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)
    lblInformation.Text = String.Empty

    ' 基本コントロール以外のメッセージを設定
    CmbDateTanaorosiBi1.SetMsgLabelText("棚卸日選択してください。")                                                               ' 棚卸日
    TxtLblMstGB1.SetMsgLabelText("部門コード(*)を入力してください。開始部門　ＥＮＴＥＲで入力候補を表示します。")                 ' 開始部門
    TxtLblMstGB2.SetMsgLabelText("部門コード(*)を入力してください。終了部門　ＥＮＴＥＲで入力候補を表示します。")                 ' 終了部門
    TxtLblMstSyubetsu1.SetMsgLabelText("畜種コード(*)を入力してください。開始畜種　ＥＮＴＥＲで入力候補を表示します。")           ' 開始畜種
    TxtLblMstSyubetsu2.SetMsgLabelText("畜種コード(*)を入力してください。終了畜種　ＥＮＴＥＲで入力候補を表示します。")           ' 終了畜種
    TxtLblMstItem1.SetMsgLabelText("部位コード(*)を入力してください。開始部位　ＥＮＴＥＲで入力候補を表示します。")               ' 開始部位
    TxtLblMstItem2.SetMsgLabelText("部位コード(*)を入力してください。終了部位　ＥＮＴＥＲで入力候補を表示します。")               ' 終了部位

    ' メッセージラベル表示のための設定
    Me.ActiveControl = TxtLblMstGB1

    ' 棚卸日のコンボボックスを先頭に設定
    If (CmbDateTanaorosiBi1.Items.Count > 0) Then
      CmbDateTanaorosiBi1.SelectedIndex = 0
    End If
    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Me.CmbDateTanaorosiBi1
    Me.CmbDateTanaorosiBi1.Select()

    ' ボタンのテキスト設定
    ' 表示ボタン
    Button_Hyouji.Text = "F1： 表示"
    ' 終了ボタン
    Button_End.Text = "F12：終了"

    ' 検証実行の有無設定
    ' 初期化ボタン
    Button_Hyouji.CausesValidation = True
    ' 終了ボタン
    Button_End.CausesValidation = False

  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_TanaPrn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Dim tmpTargetBtn As Button = Nothing

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 表示ボタン押下処理
        tmpTargetBtn = Me.Button_Hyouji
      ' F10キー押下時
      Case Keys.F10
        e.Handled = True
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        tmpTargetBtn = Me.Button_End
    End Select

    If tmpTargetBtn IsNot Nothing Then
      With tmpTargetBtn
        .Focus()
        .PerformClick()
      End With
    End If

  End Sub

  ''' <summary>
  ''' コンボボックスへのキー入力を無効にする
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbDateTanaorosiBi1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbDateTanaorosiBi1.KeyPress

    e.Handled = True

  End Sub

  ''' <summary>
  ''' 表示ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_Hyouji_Click(sender As Object, e As EventArgs) Handles Button_Hyouji.Click

    ' 選択不可設定
    Button_Hyouji.Enabled = False

    Dim rtn As typMsgBoxResult
    ' データ出力の場合
    If inMode = 2 Then
      rtn = clsCommonFnc.ComMessageBox("棚卸データ出力を実行しますか？",
                                 "棚卸一覧表",
                                 typMsgBox.MSG_NORMAL,
                                 typMsgBoxButton.BUTTON_OKCANCEL)
    Else
      ' 印刷プレビューの表示有無 
      If clsGlobalData.PRINT_PREVIEW = 1 Then
        rtn = clsCommonFnc.ComMessageBox("棚卸一覧表を表示しますか？",
                                 "棚卸一覧表",
                                 typMsgBox.MSG_NORMAL,
                                 typMsgBoxButton.BUTTON_OKCANCEL)
      Else
        rtn = clsCommonFnc.ComMessageBox("棚卸一覧表を印刷しますか？",
                                 "棚卸一覧表",
                                 typMsgBox.MSG_NORMAL,
                                 typMsgBoxButton.BUTTON_OKCANCEL)
      End If
    End If

    ' 確認メッセージボックスで、ＯＫボタン選択時
    If rtn = typMsgBoxResult.RESULT_OK Then
      Data_Set(inMode)
    End If

    ' 選択不可解除
    Button_Hyouji.Enabled = True

  End Sub

  ''' <summary>
  ''' 終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_End_Click(sender As Object, e As EventArgs) Handles Button_End.Click

    ' 終了
    Me.Close()
    Application.Exit()

  End Sub

#End Region

#Region "SQL関連"

  ''' <summary>
  ''' 棚卸データ取得SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlTanaoroshiOpen(syoriNo As Integer) As String

    Dim sql As String = String.Empty


    sql &= "SELECT TANAOROSI.* "
    sql &= "      ,BINAME "
    sql &= "      ,LSRNAME "
    sql &= "      ,GBNAME "
    sql &= "      ,SBNAME "
    sql &= "      ,TANTOMEI "
    sql &= "      ,KKNAME "
    sql &= "      ,KZNAME "
    sql &= "      ,GNNAME "
    sql &= "      ,LTKNAME "
    sql &= "FROM ((((((((TANAOROSI  "
    sql &= "LEFT JOIN BUIM ON TANAOROSI.BICODE = BUIM.BICODE) "
    sql &= "LEFT JOIN GBFLG_TBL ON TANAOROSI.GBFLG = GBFLG_TBL.GBCODE) "
    sql &= "LEFT JOIN SHUB ON TANAOROSI.SYUBETUC = SHUB.SBCODE) "
    sql &= "LEFT JOIN CUTSR ON TANAOROSI.SRCODE = CUTSR.SRCODE) "
    sql &= "LEFT JOIN KIKA ON TANAOROSI.KIKAKUC = KIKA.KICODE) "
    sql &= "LEFT JOIN KAKU ON TANAOROSI.KAKUC = KAKU.KKCODE) "
    sql &= "LEFT JOIN GENSN ON TANAOROSI.GENSANCHIC = GENSN.GNCODE) "
    sql &= "LEFT JOIN TOKUISAKI ON TANAOROSI.TKCODE = TOKUISAKI.TKCODE) "
    sql &= "LEFT JOIN TANTO_TBL ON TANAOROSI.TANTO = TANTO_TBL.TANTOC "
    sql &= " WHERE ( OROSIBI = '" & CmbDateTanaorosiBi1.Text & "' "
    sql &= " AND KEIRYOBI <= '" & CmbDateTanaorosiBi1.Text & "' "
    sql &= " AND KOTAINO > 0 "

    ' データ出力の場合
    If syoriNo = 2 Then
      sql &= " AND OROSIKUBUN <= 1 "
    End If

    ' 開始部門
    If String.IsNullOrWhiteSpace(Me.TxtLblMstGB1.CodeTxt) = False Then
      sql &= " AND " & Val(Me.TxtLblMstGB1.CodeTxt) & " <= TANAOROSI.GBFLG "
    End If

    ' 終了部門
    If String.IsNullOrWhiteSpace(Me.TxtLblMstGB2.CodeTxt) = False Then
      sql &= " AND TANAOROSI.GBFLG <= " & Val(Me.TxtLblMstGB2.CodeTxt)
    End If

    ' 開始畜種
    If String.IsNullOrWhiteSpace(Me.TxtLblMstSyubetsu1.CodeTxt) = False Then
      sql &= " AND " & Val(Me.TxtLblMstSyubetsu1.CodeTxt) & " <= TANAOROSI.SYUBETUC "
    End If

    ' 終了畜種
    If String.IsNullOrWhiteSpace(Me.TxtLblMstSyubetsu2.CodeTxt) = False Then
      sql &= " AND TANAOROSI.SYUBETUC <= " & Val(Me.TxtLblMstSyubetsu2.CodeTxt)
    End If

    ' 開始部位
    If String.IsNullOrWhiteSpace(Me.TxtLblMstItem1.CodeTxt) = False Then
      sql &= " AND " & Val(Me.TxtLblMstItem1.CodeTxt) & " <= TANAOROSI.BICODE "
    End If

    ' 終了部位
    If String.IsNullOrWhiteSpace(Me.TxtLblMstItem2.CodeTxt) = False Then
      sql &= " AND TANAOROSI.BICODE <= " & Val(Me.TxtLblMstItem2.CodeTxt)
    End If

    sql &= " )"
    sql &= " ORDER BY TANAOROSI.GBFLG"
    sql &= "         ,SETCD DESC"
    sql &= "         ,TANAOROSI.SYUBETUC"
    sql &= "         ,TANAOROSI.SRCODE"
    sql &= "         ,EBCODE"
    sql &= "         ,KOTAINO"
    sql &= "         ,SAYUKUBUN"
    sql &= "         ,TANAOROSI.BICODE"
    sql &= "         ,KEIRYOBI"
    sql &= "         ,TOOSINO"
    sql &= "         ,TANAOROSI.KDATE"

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  '''CUTJ項目初期設定
  ''' </summary>
  ''' <returns>CUTJ項目のみ設定した連想配列</returns>
  Private Function TargetValNewCutJVal() As Dictionary(Of String, String)

    Dim ret As New Dictionary(Of String, String)

    With ret

      ' 日付形式データ
      ' 空白は"NULL"に置き換え
      Dim tmpKeyName As String = String.Empty

      .Add("KIKAINO", "0")        '01:機械№
      .Add("KYOKUFLG", "0")       '02:極性フラグ
      .Add("KEIRYOBI", "NULL")    '03:入庫日
      .Add("KAKOUBI", "NULL")     '04:加工日
      .Add("GBFLG", "0")          '05:牛豚フラグ
      .Add("SRCODE", "0")         '06:仕入先コード    
      .Add("EBCODE", "0")         '07:肢番コード
      .Add("KOTAINO", "0")        '08:固体識別番号
      .Add("BICODE", "0")         '09:部位コード
      .Add("SHOHINC", "0")        '10:商品コード
      .Add("SAYUKUBUN", "0")      '11:左右
      .Add("TOOSINO", "0")        '12:通№
      .Add("JYURYO", "0")         '13:重量
      .Add("HONSU", "0")          '14:入本数  
      .Add("GENKA", "0")          '15:原単価
      .Add("TANKA", "0")          '16:ｋｇ単価
      .Add("KINGAKU", "0")        '17:金額
      .Add("SPKUBUN", "0")        '18:ＳＰ
      .Add("SETCD", "0")          '19:セットコード
      .Add("TKCODE", "0")         '20:得意先コード
      .Add("TANTO", "0")          '21:担当コード
      .Add("KFLG", "0")           '22:更新ＦＬＧ
      .Add("TDATE", "NULL")       '23:登録日付
      .Add("KDATE", "NULL")       '24:更新日
      .Add("KIKAKUC", "0")        '25:規格コード
      .Add("KAKUC", "0")          '26:格付コード
      .Add("KIGENBI", "0")        '27:期限日
      .Add("GENSANCHIC", "0")     '28:原産地コード
      .Add("SYUBETUC", "0")       '29:種別コード
      .Add("BINAME", "NULL")      '30:部位名
      .Add("LSRNAME", "NULL")     '31:仕入先名
      .Add("GBNAME", "NULL")      '32:名称
      .Add("SBNAME", "NULL")      '33:畜種名
      .Add("TANTOMEI", "NULL")    '34:担当者名
      .Add("KKNAME", "NULL")      '35:規格名
      .Add("KZNAME", "NULL")      '36:格付名
      .Add("GNNAME", "NULL")      '37:原産地名
      .Add("LTKNAME", "NULL")     '38:得意先名
      .Add("NSZFLG", "0")         '39:入出庫ＦＬＧ
      .Add("JYOUTAI", "NULL")     '40:状態
      .Add("OROSIBI", "NULL")     '41:棚卸日
      .Add("OROSIKUBUN", "0")     '42:棚卸区分
      .Add("RHONSU", "0")         '43:棚卸前入本数
      .Add("RJYURYO", "0")        '44:棚卸前重量
      .Add("SYUKKABI", "NULL")    '45:棚卸前重量
      .Add("HENPINBI", "NULL")    '46:返品日

    End With

    Return ret

  End Function

#End Region

End Class

