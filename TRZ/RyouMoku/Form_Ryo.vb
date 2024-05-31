Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting

Public Class Form_Ryo
  Implements IDgvForm02

#Region "定数定義"

  ''' <summary>
  ''' グリッド高さ最大表示
  ''' </summary>
  Private Const GRID_HEIGHT_MAX As Integer = 720
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

  Private Const PRG_ID As String = "RyouMoku"
  ' ワークテーブル名
  Private Const WK_TBL As String = "WK_RYO1"

#End Region

#Region "メンバ"

#Region "プライベート"
  '１つ目のDataGridViewオブジェクト格納先
  Private DG2V1 As DataGridView
  ' データグリッドのフォーカス喪失時の位置
  Private rowIndexDataGrid As Integer
  ' データグリッドの選択件数
  Private dataGridCount As Long
  ' データグリッドの検索日付
  Private dataGridDate As String
  ' 量目表一時テーブル名
  Private tmpRyoumokuTblName As String
  ' １つ目のプロセスＩＤ
  Private Shared ryoProcesID_01 As System.Diagnostics.Process
  ' ２つ目のプロセスＩＤ
  Private Shared ryoProcesID_02 As System.Diagnostics.Process
#End Region

#End Region

  ''' <summary>
  ''' 集計ワークテーブル構造体
  ''' </summary>
  Public Structure structRyoMoku

    Public Property wKUCHISU As Integer       '口数
    Public Property wSETTANKA As Decimal      'セット別の単価待避
    Public Property wTOTAL_L As Double        'セット別の左重量合計
    Public Property wTOTAL_R As Double        'セット別の右重量合計
    Public Property wTOTAL_KIN As Decimal     'セット別の金額合計
    Public Property wPARTIAL_KIN As Decimal   'セット別の個別の金額合計
    Public Property wTOTAL_L2 As Double       '得意先別の左重量合計
    Public Property wTOTAL_R2 As Double       '得意先別の右重量合計
    Public Property wTOTAL_KIN2 As Decimal    '得意先別の金額合計

    Public Property wKINGAKU As Decimal       '左右合計重量　×　単価　のエリア

    Public Property wSHUKAYMD As DateTime     '出荷日
    Public Property wTOKUICD As Long          '得意先コード
    Public Property wEDANo As Long            '枝番No
    Public Property wSCODE As Long            '商品コード
    Public Property wKotaiNo As Long          '個体識別番号
    Public Property wTANKA As Long            '単価
    Public Property wSETCD As Long            'セットコード

  End Structure

#Region "プライベート"
  ' 量目用ワークテーブル
  Private tmpRyouMokuDT As New DataTable
  ' セットレコード
  Private TBL_SCNT As Integer
  ' 単品レコード
  Private TBL_TCNT As Integer
  ' 連番
  Private TBL_RENNO As Integer
  ' 初期化処理フラグ
  Private flgInit As Boolean = False

#End Region


#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_Ryo, AddressOf ComRedisplay)
  End Sub
#End Region

#Region "データグリッドビュー操作関連共通"


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
  ''' ACCESSファイルを開く
  ''' </summary>
  ''' <param name="printPreview">プレビューフラグ</param>
  ''' <param name="strReportName">レポートファイル名</param>
  ''' <param name="prmWaitFlag">待機フラグ</param>
  ''' <returns>
  ''' True :ファイルオープン成功
  ''' False:ファイルオープン失敗
  ''' </returns>
  Public Shared Function RyoAccessRun(printPreview As Integer, strReportName As String, Optional prmWaitFlag As Boolean = False) As Boolean
    Try

      ' Threadオブジェクトを作成する
      Dim MultiProgram_run = New System.Threading.Thread(AddressOf DoRyoSomething01)
      ' １つ目のスレッドを開始する
      MultiProgram_run.Start(New prmReport(printPreview.ToString, strReportName))

      If (prmWaitFlag) Then
        MultiProgram_run.Join()
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Return False
    End Try

    Return True

  End Function

  ''' <summary>
  ''' ２つ目のACCESSファイルを開く
  ''' </summary>
  ''' <param name="printPreview">プレビューフラグ</param>
  ''' <param name="strReportName">レポートファイル名</param>
  ''' <returns>
  ''' True :ファイルオープン成功
  ''' False:ファイルオープン失敗
  ''' </returns>
  Public Shared Function RyoAccessRun02(printPreview As Integer, strReportName As String) As Boolean
    Try

      ' Threadオブジェクトを作成する
      Dim MultiProgram_run = New System.Threading.Thread(AddressOf DoRyoSomething02)
      ' ２つ目のスレッドを開始する
      MultiProgram_run.Start(New prmReport(printPreview.ToString, strReportName))

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Return False
    End Try

    Return True

  End Function

  ''' <summary>
  ''' １つ目の印刷スレッド
  ''' </summary>
  ''' <param name="arg"></param>
  Private Shared Sub DoRyoSomething01(arg As Object)

    Try
      Dim prm As prmReport = DirectCast(arg, prmReport)
      Dim myPath As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, clsGlobalData.REPORT_FILENAME)

      Dim strPrintPrwview As String
      If (prm.printPreview.Equals("1")) Then
        strPrintPrwview = "1"
      Else
        strPrintPrwview = "0"
      End If

      'ファイルを開く
      ryoProcesID_01 = System.Diagnostics.Process.Start(myPath, " /runtime /cmd " & strPrintPrwview & prm.strReportName)
      If ryoProcesID_01 IsNot Nothing Then
        '終了するまで待機する
        ryoProcesID_01.WaitForExit()
        ryoProcesID_01 = Nothing
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
    End Try

  End Sub

  ''' <summary>
  ''' ２つ目の印刷スレッド
  ''' </summary>
  ''' <param name="arg"></param>
  Private Shared Sub DoRyoSomething02(arg As Object)

    Try
      Dim prm As prmReport = DirectCast(arg, prmReport)
      Dim myPath As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, clsGlobalData.REPORT_FILENAME2)

      Dim strPrintPrwview As String
      If (prm.printPreview.Equals("1")) Then
        strPrintPrwview = "1"
      Else
        strPrintPrwview = "0"
      End If

      'ファイルを開く
      ryoProcesID_02 = System.Diagnostics.Process.Start(myPath, " /runtime /cmd " & strPrintPrwview & prm.strReportName)
      If ryoProcesID_02 IsNot Nothing Then
        '終了するまで待機する
        ryoProcesID_02.WaitForExit()
        ryoProcesID_02 = Nothing
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
    End Try

  End Sub

  ''' <summary>
  ''' プロセス状態確認
  ''' </summary>
  ''' <returns></returns>
  Public Shared Function RyoProcessStatus() As Boolean

    Dim ret As Boolean = False

    If ryoProcesID_01 IsNot Nothing Then
      ret = True
    End If

    If ryoProcesID_02 IsNot Nothing Then
      ret = True
    End If

    Return ret

  End Function


  ''' <summary>
  ''' プロセスの終了
  ''' </summary>
  Public Shared Sub RyoProcessKill()

    If ryoProcesID_01 IsNot Nothing Then
      ' 起動した１つ目のプロセスの終了
      ryoProcesID_01.Kill()
      ryoProcesID_01 = Nothing
    End If

    If ryoProcesID_02 IsNot Nothing Then
      ' 起動した２つ目のプロセスの終了
      ryoProcesID_02.Kill()
      ryoProcesID_02 = Nothing
    End If

  End Sub


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
        '  .AddSearchControl(Me.CmbDateShukaBi_01, "SYUKKABI", typExtraction.EX_EQ, typColumnKind.CK_Date)
        ' .AddSearchControl(Me.CmbMstCustomer_01, "TNAME", typExtraction.EX_EQ, typColumnKind.CK_Number)

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

    sql &= " SELECT RENNO "
    sql &= "       ,SHUKAYMD "
    sql &= "       ,TCODE "
    sql &= "       ,TNAME "
    sql &= "       ,SETCD "
    sql &= "       ,SETNAME "
    sql &= "       ,EDANo "
    sql &= "       ,BUICODE "
    sql &= "       ,SCODE "
    sql &= "       ,SNAME "
    sql &= "       ,LJYU "
    sql &= "       ,RJYU "
    sql &= "       ,KEIJYU "
    sql &= "       ,TANKA "
    sql &= "       ,TANKARPT "
    sql &= "       ,KINGAKU "
    sql &= "       ,KINGAKURPT "
    sql &= "       ,INJI_RYO2 "
    sql &= "       ,KOTAINO "
    sql &= "       ,GENSN"
    sql &= "       ,JISYA"
    sql &= " FROM " & tmpRyoumokuTblName
    sql &= " ORDER BY RENNO "

    Return sql

  End Function

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid2Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm02.CreateGrid2layout1
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("得意先名", "TNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("区分", "SETNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=160))
      .Add(New clsDGVColumnSetting("枝No", "EDANo", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=64, argFormat:="####"))
      .Add(New clsDGVColumnSetting("商品名", "SNAME", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
      .Add(New clsDGVColumnSetting("左重量", "LJYU", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("右重量", "RJYU", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("合計重量", "KEIJYU", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=110))
      .Add(New clsDGVColumnSetting("単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=110, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("金額", "KINGAKU", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=140, argFormat:="#,###"))
      .Add(New clsDGVColumnSetting("個体識別", "KOTAINO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=112, argFormat:="0000000000"))
      .Add(New clsDGVColumnSetting("量目印字", "INJI_RYO2", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=100))
    End With

    Return ret

  End Function

  ''' <summary>
  ''' 表示値が０の場合、空白に置き換えて表示する
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting

    Dim dgv As DataGridView = CType(sender, DataGridView)
    If dgv.Columns(e.ColumnIndex).Name = "LJYU" Or
       dgv.Columns(e.ColumnIndex).Name = "RJYU" Or
       dgv.Columns(e.ColumnIndex).Name = "KEIJYU" Then
      If (DTConvDouble(e.Value).Equals(0.0)) Then
        e.Value = ""
      End If
    End If

  End Sub

  ''' <summary>
  ''' １つ目のDataGridViewオブジェクトの一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  Public Function CreateGrid2EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm02.CreateGrid2EditCol1
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
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
  ''' グリッドを最新状態にする
  ''' </summary>
  Private Sub RefleshGrid()
    Call InitSelectCmb(Me.CmbDateShukaBi_01.Text, Me.CmbMstCustomer_01.Text)
    DataGrid_ShowList()
    DG2V1.Focus()
  End Sub

  ''' <summary>
  ''' 検索コンボボックス初期化
  ''' </summary>
  ''' <param name="prmSyukabiText"></param>
  ''' <param name="prmCustomerText"></param>
  Private Sub InitSelectCmb(Optional prmSyukabiText As String = Nothing _
                          , Optional prmCustomerText As String = Nothing)

    flgInit = False

    ' 出荷日のコンボボックスを更新
    Me.CmbDateShukaBi_01.InitCmb()
    ' 出荷日のコンボボックスの選択状態を設定
    If CmbDateShukaBi_01.FindStringExact(prmSyukabiText) = -1 Then
      CmbDateShukaBi_01.SelectedIndex = 0
    Else
      CmbDateShukaBi_01.SelectedIndex = CmbDateShukaBi_01.FindStringExact(prmSyukabiText)
    End If

    ' 得意先名のコンボボックスを更新
    CmbMstCustomer_01.InitCmb()
    ' 得意先名のコンボボックスの選択状態を設定
    CmbMstCustomer_01.SelectedIndex = CmbMstCustomer_01.FindStringExact(prmCustomerText)

    flgInit = True
  End Sub



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
  ''' データグリッドの再描画
  ''' </summary>
  Private Sub DataGrid_ShowList()

    If (flgInit = False) Then
      Return
    End If

    'Stopwatchオブジェクトを作成する 
    Dim sw As New System.Diagnostics.Stopwatch()
    'ストップウォッチを開始する 
    sw.Start()

    SSQL_Rtn()

    updateWorkTableRyoMoku()

    With DG2V1
      Controlz(.Name).ShowList()

      .CurrentCell = Nothing

    End With

    'ストップウォッチを止める 
    sw.Stop()

    '結果を表示する 
    Console.WriteLine(sw.Elapsed)

  End Sub

  ''' <summary>
  ''' 新規追加
  ''' </summary>
  ''' <param name="stRyo"></param>
  ''' <param name="dtRow"></param>
  ''' <returns></returns>
  Private Function NEW_ADD_RYOMOKU(ByRef stRyo As structRyoMoku, dtRow As DataRow, dtGENSN As DataTable) As Boolean

    Dim WKRYOTb1 As Dictionary(Of String, String) = TargetValNewRyouMokuVal()

    stRyo.wTOTAL_KIN = stRyo.wTOTAL_KIN + stRyo.wPARTIAL_KIN
    stRyo.wPARTIAL_KIN = 0

    ' 口数初期化
    stRyo.wKUCHISU = 1

    ' 更新FLGが1の場合「済」、0の場合「未」
    If DTConvLong(dtRow("KFLG")) = 1 Then
      WKRYOTb1("INJI_RYO2") = "済"
    Else
      WKRYOTb1("INJI_RYO2") = "未"
    End If
    '
    TBL_RENNO = TBL_RENNO + 1
    WKRYOTb1("RENNO") = TBL_RENNO.ToString
    WKRYOTb1("SHUKAYMD") = dtRow("SYUKKABI").ToString
    WKRYOTb1("TCODE") = dtRow("UTKCODE").ToString
    WKRYOTb1("TNAME") = dtRow("LTKNAME").ToString
    WKRYOTb1("SETCD") = dtRow("SETCD").ToString
    WKRYOTb1("SETNAME") = dtRow("HINMEI").ToString
    WKRYOTb1("EDANo") = dtRow("EBCODE").ToString
    WKRYOTb1("KOTAINO") = dtRow("KOTAINO").ToString.PadLeft(10, "0"c)
    WKRYOTb1("SCODE") = dtRow("SHOHINC").ToString
    WKRYOTb1("SNAME") = dtRow("BINAME").ToString
    WKRYOTb1("LJYU") = "0"
    WKRYOTb1("RJYU") = "0"
    WKRYOTb1("KEIJYU") = "0"
    WKRYOTb1("JISYA") = JISSYA_SYAMEI

    If DTConvLong(dtRow("SAYUKUBUN")) <> 2 Then   '右以外
      If String.IsNullOrWhiteSpace(WKRYOTb1("LJYU").ToString) Then
        WKRYOTb1("LJYU") = ""
      End If
      WKRYOTb1("LJYU") = (DTConvDouble(WKRYOTb1("LJYU")) + (DTConvLong(dtRow("JYURYOG")) \ 100) / 10).ToString
      stRyo.wTOTAL_L = stRyo.wTOTAL_L + (DTConvLong(dtRow("JYURYOG")) \ 100) / 10
    Else
      If String.IsNullOrWhiteSpace(WKRYOTb1("RJYU").ToString) Then
        WKRYOTb1("RJYU") = ""
      End If
      WKRYOTb1("RJYU") = (DTConvDouble(WKRYOTb1("RJYU")) + (DTConvLong(dtRow("JYURYOG")) \ 100) / 10).ToString
      stRyo.wTOTAL_R = stRyo.wTOTAL_R + (DTConvLong(dtRow("JYURYOG")) \ 100) / 10
    End If
    WKRYOTb1("KEIJYU") = (DTConvDouble(WKRYOTb1("LJYU"))).ToString
    WKRYOTb1("KEIJYU") = (DTConvDouble(WKRYOTb1("KEIJYU")) + DTConvDouble(WKRYOTb1("RJYU"))).ToString

    If DTConvLong(dtRow("SETCD")) = 0 Then
      '(ピースの場合)
      TBL_TCNT = TBL_TCNT + 1
      WKRYOTb1("TANKA") = (dtRow("TANKA")).ToString
      WKRYOTb1("TANKARPT") = (dtRow("TANKA")).ToString
      stRyo.wKINGAKU = CDec(Math.Floor(DTConvDouble(WKRYOTb1("KEIJYU")) * DTConvLong(WKRYOTb1("TANKA")) + 0.01))

      WKRYOTb1("KINGAKU") = (stRyo.wKINGAKU).ToString
      WKRYOTb1("KINGAKURPT") = (stRyo.wKINGAKU).ToString
      stRyo.wPARTIAL_KIN = CDec(Math.Floor((DTConvDouble(WKRYOTb1("LJYU")) + (DTConvDouble(WKRYOTb1("RJYU")))) * DTConvLong(WKRYOTb1("TANKA"))))
    Else
      '(セットの場合)
      TBL_SCNT = TBL_SCNT + 1
      WKRYOTb1("TANKA") = (dtRow("TANKA")).ToString
      stRyo.wKINGAKU = CDec(Math.Floor(DTConvDouble(WKRYOTb1("KEIJYU")) * DTConvLong(WKRYOTb1("TANKA")) + 0.01))
      WKRYOTb1("KINGAKU") = stRyo.wKINGAKU.ToString
      If (stRyo.wSETTANKA <= DTConvLong(dtRow("TANKA"))) Then
        stRyo.wSETTANKA = DTConvLong(dtRow("TANKA"))
      End If

      WKRYOTb1("TANKARPT") = (dtRow("TANKA")).ToString
      stRyo.wPARTIAL_KIN = CDec((DTConvDouble(WKRYOTb1("LJYU")) + (DTConvDouble(WKRYOTb1("RJYU")))) * DTConvLong(WKRYOTb1("TANKARPT")))
      WKRYOTb1("KINGAKURPT") = (stRyo.wPARTIAL_KIN).ToString
    End If

    ' 部位名と口数表示
    WKRYOTb1("SNAME") = dtRow("BINAME").ToString & "（" & stRyo.wKUCHISU & "口）"

    Dim drGENSN As DataRow()
    ' 原産地マスタの検索
    drGENSN = dtGENSN.Select("GNCODE = " & dtRow("GENSANCHIC").ToString)
    If (1 = drGENSN.Count) Then
      ' 原産地名の取得
      WKRYOTb1("GENSN") = drGENSN(0)("GNNAME").ToString
    Else
      WKRYOTb1("GENSN") = "国産"
    End If

    stRyo.wTANKA = DTConvLong(dtRow("TANKA"))
    ComSetDictionaryVal(WKRYOTb1, "JYOUJYOU", dtRow("JYOUJYOU").ToString)
    ComSetDictionaryVal(WKRYOTb1, "HINSYU", dtRow("HINSYU").ToString)
    ComSetDictionaryVal(WKRYOTb1, "BRAND_NAME", dtRow("BRAND_NAME").ToString)
    ComSetDictionaryVal(WKRYOTb1, "BUICODE", dtRow("BUICODE").ToString)

    Dim rowSyukei As DataRow
    rowSyukei = tmpRyouMokuDT.NewRow
    For Each tmpKey As String In WKRYOTb1.Keys
      rowSyukei(tmpKey) = WKRYOTb1(tmpKey)
    Next
    tmpRyouMokuDT.Rows.Add(rowSyukei)

    Return True

  End Function

  ''' <summary>
  ''' 同じ単価の場合加算更新
  ''' </summary>
  ''' <param name="stRyo"></param>
  ''' <param name="dtRow"></param>
  ''' <param name="dtGENSN"></param>
  ''' <returns></returns>
  Private Function UPD_RYOMOKU(ByRef stRyo As structRyoMoku, dtRow As DataRow, dtGENSN As DataTable) As Boolean

    Dim rowTmp As DataRow()

    Dim sql As String = String.Empty
    sql &= " TCODE = '" & DTConvLong(dtRow("UTKCODE")).ToString & "'"
    sql &= " AND SETCD = '" & DTConvLong(dtRow("SETCD")).ToString & "'"
    sql &= " AND EDANo = '" & DTConvLong(dtRow("EBCODE")).ToString & "'"
    sql &= " AND KOTAINO = '" & DTConvLong(dtRow("KOTAINO")).ToString.PadLeft(10, "0"c) & "'"
    sql &= " AND SCODE = '" & DTConvLong(dtRow("SHOHINC")).ToString & "'"

    If (tmpRyouMokuDT.Columns.Count <= 0) Then
      Return False
    End If
    rowTmp = tmpRyouMokuDT.Select(sql)

    If (1 <= rowTmp.Count) Then

      For Each WKRYOTb1 As DataRow In rowTmp
        If DTConvLong(dtRow("SAYUKUBUN")) <> 2 Then   '右以外
          If String.IsNullOrWhiteSpace(WKRYOTb1("LJYU").ToString) Then
            WKRYOTb1("LJYU") = ""
          End If
          WKRYOTb1("LJYU") = (DTConvDouble(WKRYOTb1("LJYU")) + (DTConvLong(dtRow("JYURYOG")) \ 100) / 10).ToString
          stRyo.wTOTAL_L = stRyo.wTOTAL_L + (DTConvLong(dtRow("JYURYOG")) \ 100) / 10
        Else
          If String.IsNullOrWhiteSpace(WKRYOTb1("RJYU").ToString) Then
            WKRYOTb1("RJYU") = ""
          End If
          WKRYOTb1("RJYU") = (DTConvDouble(WKRYOTb1("RJYU")) + (DTConvLong(dtRow("JYURYOG")) \ 100) / 10).ToString
          stRyo.wTOTAL_R = stRyo.wTOTAL_R + (DTConvLong(dtRow("JYURYOG")) \ 100) / 10
        End If
        WKRYOTb1("KEIJYU") = (DTConvDouble(WKRYOTb1("LJYU"))).ToString
        WKRYOTb1("KEIJYU") = (DTConvDouble(WKRYOTb1("KEIJYU")) + DTConvDouble(WKRYOTb1("RJYU"))).ToString

        If DTConvLong(dtRow("SETCD")) = 0 Then
          '(ピースの場合)
          TBL_TCNT = TBL_TCNT + 1
          WKRYOTb1("TANKA") = (dtRow("TANKA")).ToString
          WKRYOTb1("TANKARPT") = (dtRow("TANKA")).ToString
          stRyo.wKINGAKU = CDec(Math.Floor(DTConvDouble(WKRYOTb1("KEIJYU")) * DTConvLong(WKRYOTb1("TANKA")) + 0.5))

          WKRYOTb1("KINGAKU") = (stRyo.wKINGAKU).ToString
          WKRYOTb1("KINGAKURPT") = (stRyo.wKINGAKU).ToString
          stRyo.wPARTIAL_KIN = CDec(Math.Floor((DTConvDouble(WKRYOTb1("LJYU")) + (DTConvDouble(WKRYOTb1("RJYU")))) * DTConvLong(WKRYOTb1("TANKA"))))
        Else
          '(セットの場合)
          TBL_SCNT = TBL_SCNT + 1
          WKRYOTb1("TANKA") = ""
          WKRYOTb1("KINGAKU") = ""
          stRyo.wSETTANKA = DTConvLong(dtRow("TANKA"))
          WKRYOTb1("TANKARPT") = (dtRow("TANKA")).ToString
          stRyo.wPARTIAL_KIN = CDec((DTConvDouble(WKRYOTb1("LJYU")) + (DTConvDouble(WKRYOTb1("RJYU")))) * DTConvLong(WKRYOTb1("TANKARPT")))
          WKRYOTb1("KINGAKURPT") = (stRyo.wPARTIAL_KIN).ToString
        End If

        ' 部位名と口数表示
        WKRYOTb1("SNAME") = dtRow("BINAME").ToString & "（" & stRyo.wKUCHISU & "口）"

        Dim drGENSN As DataRow()
        ' 原産地マスタの検索
        drGENSN = dtGENSN.Select("GNCODE = " & dtRow("GENSANCHIC").ToString)
        If (1 = drGENSN.Count) Then
          ' 原産地名の取得
          WKRYOTb1("GENSN") = drGENSN(0)("GNNAME").ToString
        Else
          WKRYOTb1("GENSN") = "国産"
        End If

        stRyo.wTANKA = DTConvLong(dtRow("TANKA"))
      Next
    End If


    Return True

  End Function

  ''' <summary>
  ''' 新規データ読込
  ''' </summary>
  ''' <param name="stRyo"></param>
  ''' <param name="dtRow"></param>
  ''' <returns></returns>
  Private Function Tbl_Re_Open(ByRef stRyo As structRyoMoku, dtRow As DataRow, ByRef recordCount As Integer) As Boolean
    Try

      stRyo.wTOTAL_KIN = stRyo.wTOTAL_KIN + stRyo.wPARTIAL_KIN
      stRyo.wPARTIAL_KIN = 0

      recordCount = 0

      stRyo.wSHUKAYMD = DTConvDateTime(dtRow("SYUKKABI"))
      stRyo.wTOKUICD = DTConvLong(dtRow("UTKCODE"))
      stRyo.wSETCD = DTConvLong(dtRow("SETCD"))
      stRyo.wEDANo = DTConvLong(dtRow("EBCODE"))
      stRyo.wSCODE = DTConvLong(dtRow("SHOHINC"))
      stRyo.wKotaiNo = DTConvLong(dtRow("KOTAINO"))

      Dim sql As String = String.Empty
      sql &= " TCODE = '" & stRyo.wTOKUICD.ToString & "'"
      sql &= " AND SETCD = '" & stRyo.wSETCD.ToString & "'"
      sql &= " AND EDANo = '" & DTConvLong(dtRow("EBCODE")).ToString & "'"
      sql &= " AND KOTAINO = '" & DTConvLong(dtRow("KOTAINO")).ToString.PadLeft(10, "0"c) & "'"
      sql &= " AND SCODE = '" & DTConvLong(dtRow("SHOHINC")).ToString & "'"

      If (tmpRyouMokuDT.Columns.Count <= 0) Then
        Return False
      End If

      Dim rowUpd As DataRow()
      rowUpd = tmpRyouMokuDT.Select(sql)

      If (1 <= rowUpd.Count) Then

        recordCount = rowUpd.Count

        For Each tmpKeyVal As DataRow In rowUpd
          tmpKeyVal("RENNO") = rowUpd(0)("RENNO")           ' 表示順番
          tmpKeyVal("SHUKAYMD") = rowUpd(0)("SHUKAYMD")     ' 出荷年月日
          tmpKeyVal("TCODE") = rowUpd(0)("TCODE")           ' 得意先コード
          tmpKeyVal("TNAME") = rowUpd(0)("TNAME")           ' 得意先名  
          tmpKeyVal("SETCD") = rowUpd(0)("SETCD")           ' セットコード
          tmpKeyVal("SETNAME") = rowUpd(0)("SETNAME")       ' セット名称
          tmpKeyVal("EDANo") = rowUpd(0)("EDANo")           ' 枝番コード
          tmpKeyVal("BUICODE") = rowUpd(0)("BUICODE")       ' 部位コード
          tmpKeyVal("SCODE") = rowUpd(0)("SCODE")           ' 商品コード
          tmpKeyVal("SNAME") = rowUpd(0)("SNAME")           ' 商品名
          tmpKeyVal("LJYU") = rowUpd(0)("LJYU")             ' 左重量
          tmpKeyVal("RJYU") = rowUpd(0)("RJYU")             ' 右重量
          tmpKeyVal("KEIJYU") = rowUpd(0)("KEIJYU")         ' 合計重量
          tmpKeyVal("TANKA") = rowUpd(0)("TANKA")           ' 単価
          tmpKeyVal("TANKARPT") = rowUpd(0)("TANKARPT")     ' 単価
          tmpKeyVal("KINGAKU") = rowUpd(0)("KINGAKU")       ' 金額
          tmpKeyVal("KINGAKURPT") = rowUpd(0)("KINGAKURPT") ' 金額
          tmpKeyVal("INJI_RYO2") = rowUpd(0)("INJI_RYO2")   ' 量目表印字：未　：済
          tmpKeyVal("KOTAINO") = rowUpd(0)("KOTAINO")       ' 個体識別
          tmpKeyVal("GENSN") = rowUpd(0)("GENSN")           ' 原産地名
        Next
      End If

    Catch ex As Exception
      ' Error
      Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）

    Finally

    End Try

    Return True

  End Function

  ''' <summary>
  ''' 区分合計設定
  ''' </summary>
  ''' <param name="stRyo"></param>
  ''' <param name="dtRow"></param>
  ''' <returns></returns>
  Private Function SHU_KEI1(ByRef stRyo As structRyoMoku, dtRow As DataRow) As Boolean

    Try
      stRyo.wTOTAL_KIN = stRyo.wTOTAL_KIN + stRyo.wPARTIAL_KIN
      stRyo.wPARTIAL_KIN = 0

      Dim tmpKeyVal As Dictionary(Of String, String) = TargetValNewRyouMokuVal()

      '   （集計行の追加：セットごと）
      TBL_RENNO = TBL_RENNO + 1
      tmpKeyVal("RENNO") = TBL_RENNO.ToString
      tmpKeyVal("TCODE") = stRyo.wTOKUICD.ToString
      tmpKeyVal("TNAME") = " "
      tmpKeyVal("EDANo") = ""
      tmpKeyVal("SCODE") = "0"
      tmpKeyVal("SNAME") = "   【　 区分合計   】 "
      tmpKeyVal("TANKA") = "0"
      tmpKeyVal("LJYU") = "0"
      tmpKeyVal("RJYU") = "0"
      tmpKeyVal("KEIJYU") = (stRyo.wTOTAL_L + stRyo.wTOTAL_R).ToString
      If stRyo.wTOTAL_L > 0 Then
        tmpKeyVal("LJYU") = (stRyo.wTOTAL_L).ToString
      End If
      If stRyo.wTOTAL_R > 0 Then
        tmpKeyVal("RJYU") = (stRyo.wTOTAL_R).ToString
      End If
      If stRyo.wSETCD = 0 Then
        tmpKeyVal("KINGAKU") = stRyo.wTOTAL_KIN.ToString
      Else
        tmpKeyVal("TANKA") = stRyo.wSETTANKA.ToString
        '（セット単価）×（左右重量）
        'Dim dTanka As Double = stRyo.wSETTANKA
        'stRyo.wTOTAL_KIN = Convert.ToDecimal(Math.Floor(dTanka * (stRyo.wTOTAL_L + stRyo.wTOTAL_R) + 0.5))
        'tmpKeyVal("KINGAKU") = stRyo.wTOTAL_KIN.ToString
        tmpKeyVal("KINGAKU") = CDec(Math.Floor(stRyo.wTOTAL_KIN) + 0.01).ToString

        stRyo.wSETTANKA = 0
      End If

      '
      Dim rowSyukei As DataRow
      rowSyukei = tmpRyouMokuDT.NewRow
      For Each tmpKey As String In tmpKeyVal.Keys
        rowSyukei(tmpKey) = tmpKeyVal(tmpKey)
      Next
      tmpRyouMokuDT.Rows.Add(rowSyukei)

      stRyo.wTOTAL_KIN2 = stRyo.wTOTAL_KIN2 + stRyo.wTOTAL_KIN
      stRyo.wTOTAL_L2 = stRyo.wTOTAL_L2 + stRyo.wTOTAL_L
      stRyo.wTOTAL_R2 = stRyo.wTOTAL_R2 + stRyo.wTOTAL_R
      stRyo.wTOTAL_KIN = 0
      stRyo.wTOTAL_L = 0
      stRyo.wTOTAL_R = 0

    Catch ex As Exception
      ' Error
      Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）

    Finally

    End Try

    Return True

  End Function

  ''' <summary>
  ''' 合計設定
  ''' </summary>
  ''' <param name="stRyo"></param>
  ''' <param name="dtRow"></param>
  ''' <returns></returns>
  Private Function SHU_KEI2(ByRef stRyo As structRyoMoku, dtRow As DataRow) As Boolean

    Try

      Dim tmpKeyVal As Dictionary(Of String, String) = TargetValNewRyouMokuVal()

      '集計行の追加（得意先ごと）
      TBL_RENNO = TBL_RENNO + 1
      tmpKeyVal("RENNO") = TBL_RENNO.ToString
      tmpKeyVal("TCODE") = stRyo.wTOKUICD.ToString
      tmpKeyVal("TNAME") = " "
      tmpKeyVal("EDANo") = ""
      tmpKeyVal("SCODE") = "0"
      tmpKeyVal("SNAME") = "   【　 合　　計   】 "
      tmpKeyVal("TANKA") = "0"
      tmpKeyVal("LJYU") = "0"
      tmpKeyVal("RJYU") = "0"
      tmpKeyVal("KEIJYU") = (stRyo.wTOTAL_L2 + stRyo.wTOTAL_R2).ToString
      tmpKeyVal("KINGAKU") = CDec(Math.Floor(stRyo.wTOTAL_KIN2) + 0.01).ToString

      If stRyo.wTOTAL_L2 > 0 Then
        tmpKeyVal("LJYU") = stRyo.wTOTAL_L2.ToString
      End If
      If stRyo.wTOTAL_R2 > 0 Then
        tmpKeyVal("RJYU") = stRyo.wTOTAL_R2.ToString
      End If

      Dim rowSyukei As DataRow
      rowSyukei = tmpRyouMokuDT.NewRow
      For Each tmpKey As String In tmpKeyVal.Keys
        rowSyukei(tmpKey) = tmpKeyVal(tmpKey)
      Next
      tmpRyouMokuDT.Rows.Add(rowSyukei)

      '空行追加
      tmpKeyVal.Clear()

      TBL_RENNO = TBL_RENNO + 1
      tmpKeyVal("RENNO") = TBL_RENNO.ToString
      tmpKeyVal("TCODE") = ""
      tmpKeyVal("TNAME") = " "
      tmpKeyVal("EDANo") = ""
      tmpKeyVal("SCODE") = "0"
      tmpKeyVal("SNAME") = ""
      tmpKeyVal("TANKA") = "0"
      tmpKeyVal("LJYU") = "0"
      tmpKeyVal("RJYU") = "0"
      tmpKeyVal("KEIJYU") = "0"
      tmpKeyVal("KINGAKU") = "0"

      rowSyukei = tmpRyouMokuDT.NewRow
      For Each tmpKey As String In tmpKeyVal.Keys
        rowSyukei(tmpKey) = tmpKeyVal(tmpKey)
      Next
      tmpRyouMokuDT.Rows.Add(rowSyukei)

      '(クリア)
      stRyo.wTOTAL_KIN2 = 0
      stRyo.wTOTAL_L2 = 0
      stRyo.wTOTAL_R2 = 0

    Catch ex As Exception
      ' Error
      Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）

    Finally

    End Try

    Return True

  End Function

  ''' <summary>
  ''' 一覧変更時イベント（編集されたレコードのみを更新）
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function SSQL_Rtn() As Boolean

    Dim tmpDb As New clsSqlServer()
    Dim sql As String = String.Empty
    Dim ret As Boolean = True
    Dim stRyo As New structRyoMoku

    With tmpDb
      ' 実行
      Try

        ' 原産地マスタの取得
        sql = SqlReadGensan()
        Dim dtGENSN As New DataTable
        Call .GetResult(dtGENSN, sql)

        ' SQL文の作成
        sql = SqlRyomokuQuery()
        Dim tmpDt As New DataTable
        Call .GetResult(tmpDt, sql)

        Dim dtRow As DataRow
        Dim tmpDtSub As New DataTable

        Dim DATAON_FLG As Integer = 0

        TBL_RENNO = 0
        TBL_SCNT = 0
        TBL_TCNT = 0

        tmpRyouMokuDT.Clear()

        ' ファイルレコード → DataRow
        For i = 0 To tmpDt.Rows.Count - 1

          dtRow = tmpDt.Rows(i)

          If DATAON_FLG = 1 Then
            If stRyo.wTOKUICD <> DTConvLong(dtRow("UTKCODE")) Then
              SHU_KEI1(stRyo, dtRow)
              SHU_KEI2(stRyo, dtRow)
            ElseIf stRyo.wSETCD <> DTConvLong(dtRow("SETCD")) Or
                   stRyo.wEDANo <> DTConvLong(dtRow("EBCODE")) Or
                   stRyo.wKotaiNo <> DTConvLong(dtRow("KOTAINO")) Then
              SHU_KEI1(stRyo, dtRow)
            End If
          End If
          DATAON_FLG = 1

          Dim recoerCount As Integer
          If stRyo.wSHUKAYMD.CompareTo(DTConvDateTime(dtRow("SYUKKABI"))) <> 0 Or
             stRyo.wTOKUICD <> DTConvLong(dtRow("UTKCODE")) Or
             stRyo.wSETCD <> DTConvLong(dtRow("SETCD")) Or
             stRyo.wEDANo <> DTConvLong(dtRow("EBCODE")) Or
             stRyo.wSCODE <> DTConvLong(dtRow("SHOHINC")) Or
             stRyo.wKotaiNo <> DTConvLong(dtRow("KOTAINO")) Then

            Tbl_Re_Open(stRyo, dtRow, recoerCount)
          Else
            recoerCount = tmpRyouMokuDT.Rows.Count
          End If

          If recoerCount <> 0 Then
            ' 口数の加算
            stRyo.wKUCHISU = stRyo.wKUCHISU + 1
            '（同じ単価かどうかの確認）
            If stRyo.wTANKA <> DTConvLong(dtRow("TANKA")) Then
              NEW_ADD_RYOMOKU(stRyo, dtRow, dtGENSN)
            Else
              UPD_RYOMOKU(stRyo, dtRow, dtGENSN)
            End If
          Else
            NEW_ADD_RYOMOKU(stRyo, dtRow, dtGENSN)
          End If

          ' 最終区分合計・合計設定
          If (i = tmpDt.Rows.Count - 1) Then
            SHU_KEI1(stRyo, dtRow)
            SHU_KEI2(stRyo, dtRow)
          End If
        Next i

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）

        ret = False
      Finally

      End Try

    End With

    Return ret

  End Function


  ''' <summary>
  ''' 量目表（単品）・ 量目表（セット）印刷処理
  ''' </summary>
  Public Sub Report_Prn()

    ' 単品レコード印刷対象有りの場合
    If TBL_TCNT <> 0 Then

      ' 量目表（単品）を表示
      '  （単品量目表）
      updateReportRyoMoku()
      RyoAccessRun(clsGlobalData.PRINT_PREVIEW, "R_RYO", True)

    End If


    ' セットレコード印刷対象有りの場合
    If TBL_SCNT <> 0 Then

      ' 量目表（セット）
      '   （セット量目表）
      updateReportRyoMokuSet()
      RyoAccessRun02(clsGlobalData.PRINT_PREVIEW, "R_RYOSET")

    End If

    '' セットレコード印刷対象有りの場合
    'If TBL_SCNT <> 0 Then

    '  ' 量目表（セット）
    '  '   （セット量目表）
    '  updateReportRyoMokuSet()
    '  RyoAccessRun02(clsGlobalData.PRINT_PREVIEW, "R_RYOSET")

    'End If

    '  （印刷済に変更）
    PRT_FLG_Set()

    DataGrid_ShowList()

    lblInformation.Text = "　印刷終了　"

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
    Me.Label_GridData.Text = "量　目　表　" & dt.ToString("yyyy年M月d日HH：mm") & "現在"

  End Sub

  ''' <summary>
  ''' 量目表印刷区分の解除
  ''' </summary>
  ''' <param name="tkCode">得意先コード</param>
  ''' <param name="edaNo">枝番号</param>
  ''' <param name="shohinCd">商品番号</param>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function PRT_FLG_UnSet(tkCode As String,
                                 edaNo As String,
                                 shohinCd As String) As Boolean

    Dim tmpDb As New clsSqlServer()

    ' 実行
    With tmpDb

      Try

        Dim sql As String = String.Empty

        ' 量目表印刷区分の検索SQL文作成
        sql = SqlSelectRyouMokuFlg(tkCode, edaNo, shohinCd)

        Dim tmpDt As New DataTable
        Call tmpDb.GetResult(tmpDt, sql)

        If (1 <= tmpDt.Rows.Count) Then

          ' トランザクション開始
          .TrnStart()

          Dim dt As DateTime = DateTime.Parse(ComGetProcTime())

          ' 量目表印刷区分の解除SQL文作成
          sql = SqlUpdRyouMokuFlgOff(dt, tkCode, edaNo, shohinCd)

          Console.WriteLine(sql)


          .Execute(sql)

          ' 更新成功
          .TrnCommit()
        End If

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("ワークテーブルの書き込みに失敗しました")
      End Try

    End With

    Return True

  End Function

  ''' <summary>
  ''' 量目表印刷区分の設定
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function PRT_FLG_Set() As Boolean

    Dim tmpDb As New clsSqlServer()

    ' 実行
    With tmpDb

      Try

        Dim sql As String = String.Empty

        ' トランザクション開始
        .TrnStart()

        Dim dt As DateTime = DateTime.Parse(ComGetProcTime())

        ' 量目表印刷区分の設定SQL文作成
        sql = SqlUpdRyouMokuFlgOn(dt)

        Console.WriteLine(sql)

        .Execute(sql)

        ' 更新成功
        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("ワークテーブルの書き込みに失敗しました")
      End Try

    End With

    Return True

  End Function

  ''' <summary>
  ''' 量目表ワークテーブル作成
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function createWorkTable() As Boolean

    Dim tmpDb As New clsSqlServer()

    ' 実行
    With tmpDb
      Try
        ' トランザクション開始
        .TrnStart()

        ' SQL文の作成
        Dim sql As String = String.Empty
        sql = SqlSetTmpRyouMoku()
        ' 積算一時テーブル作成
        .Execute(sql)

        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)

        Throw New Exception("ワークテーブルの作成に失敗しました")
      End Try

    End With

    Return True

  End Function

  ''' <summary>
  ''' 量目表ワークテーブル削除と新規作成
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function updateWorkTableRyoMoku() As Boolean

    Dim tmpDb As New clsSqlServer

    ' 実行
    With tmpDb

      Try
        ' SQL文の作成
        .Execute("DELETE FROM " & tmpRyoumokuTblName)

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("ワークテーブルの削除に失敗しました")

      End Try

      Try

        ' トランザクション開始
        .TrnStart()

        Dim sql As String = String.Empty
        Dim dt As DateTime = DateTime.Parse(ComGetProcTime())

        ' データテーブルから追加SQL文を作成
        For Each row As DataRow In tmpRyouMokuDT.Rows
          sql = SqlInsRyouMoku(tmpRyoumokuTblName, row, dt)
          If String.IsNullOrWhiteSpace(sql) = False Then
            .Execute(sql)
          End If
        Next

        ' 更新成功
        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("ワークテーブルの書き込みに失敗しました")
      End Try


    End With

    Return True

  End Function

  ''' <summary>
  ''' 量目表（単品）ワークテーブル削除と新規作成
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function updateReportRyoMoku() As Boolean

    Dim tmpDb As New clsReport(clsGlobalData.REPORT_FILENAME)

    ' 実行
    With tmpDb

      Try
        ' SQL文の作成
        .Execute("DELETE FROM " & WK_TBL)

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("量目表（単品）ワークテーブルの削除に失敗しました")

      End Try

      Try

        Dim sql As String = String.Empty
        Dim dt As DateTime = DateTime.Parse(ComGetProcTime())

        Dim rowDt As DataRow()
        ' 単品レコードの絞り込み（SETCD=0）、区分合計、合計行の除外（EDANo <>　0）
        rowDt = tmpRyouMokuDT.Select("SETCD = '0' ")
        If (rowDt.Length = 0) Then
          Return False
        End If

        ' トランザクション開始
        .TrnStart()

        ' データテーブルから追加SQL文を作成
        For Each row As DataRow In rowDt
          sql = SqlInsRyouMoku(WK_TBL, row, dt)
          If String.IsNullOrWhiteSpace(sql) = False Then
            .Execute(sql)
          End If
        Next

        ' 更新成功
        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("量目表（単品）ワークテーブルの書き込みに失敗しました")
      End Try

      .Dispose()

    End With

    Return True

  End Function

  ''' <summary>
  ''' 量目表（セット）ワークテーブル削除と新規作成
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function updateReportRyoMokuSet() As Boolean

    Dim tmpDb As New clsReport(clsGlobalData.REPORT_FILENAME2)

    ' 実行
    With tmpDb

      Try
        ' SQL文の作成
        '.Execute("DELETE FROM " & WK_TBL)

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("量目表（セット）ワークテーブルの削除に失敗しました")

      End Try

      Try
        Dim sql As String = String.Empty
        ' セットレコードの絞り込み（SETCD<>0）、区分合計、合計行の除外（EDANo <>　0）
        Dim dt As DateTime = DateTime.Parse(ComGetProcTime())

        Dim rowDt As DataRow()
        rowDt = tmpRyouMokuDT.Select("SETCD > '0'")
        If (rowDt.Length = 0) Then
          Return False
        End If

        ' トランザクション開始
        .TrnStart()

        ' データテーブルから追加SQL文を作成
        For Each row As DataRow In rowDt
          sql = SqlInsRyouMoku(WK_TBL, row, dt)
          If String.IsNullOrWhiteSpace(sql) = False Then
            .Execute(sql)
          End If
        Next

        ' 更新成功
        .TrnCommit()

      Catch ex As Exception
        Call ComWriteErrLog(ex)
        .TrnRollBack()
        Throw New Exception("量目表（セット）ワークテーブルの書き込みに失敗しました")
      End Try

      .Dispose()

    End With

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
  Private Sub Form_Ryo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = "量目表印刷"

    '最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

    ' 加工日のコンボボックスを先頭に設定
    CmbDateShukaBi_01.SelectedIndex = 0

    ' 加工日のコンボボックスは未入力不可
    CmbDateShukaBi_01.AvailableBlank = True

    ' 未発行のみチェック済
    CHK_P.Checked = True

    ' 量目計用ワークテーブルの初回定義
    Dim tmpKeyVal As Dictionary(Of String, String) = TargetValNewRyouMokuVal()
    For Each tmpKey As String In tmpKeyVal.Keys
      tmpRyouMokuDT.Columns.Add(tmpKey)
    Next

    ' 積算一時テーブル名作成
    tmpRyoumokuTblName = "tmpRyouMoku" & GetIPv4()

    createWorkTable()

    Call InitForm02()

    flgInit = True

    DataGrid_ShowList()

    ' グリッドダブルクリック時処理追加
    Controlz(DG2V1.Name).lcCallBackCellDoubleClick = AddressOf Dgv1CellDoubleClick 'ダブルクリック時イベント設定

    ' グリッド再表示時処理
    Controlz(DG2V1.Name).lcCallBackReLoadData = AddressOf DgvReload

    Controlz(DG2V1.Name).AutoSearch = True

    Controlz(DG2V1.Name).SetMsgLabelText("明細をダブルクリックすると印刷区分を取り消せます")

    Controlz(DG2V1.Name).SortActive = False

    ' Controlz(DG2V1.Name).SetMsgLabelText("仕入単価、原価、売単価を修正をする場合は、直接明細に入力してＥｎｔｅｒキーをしてください。")

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)
    lblInformation.Text = String.Empty

    ' ボタンのテキスト設定
    ' 終了ボタン
    ButtonEnd.Text = "F12：終了"
    ' 印刷ボタン
    ButtonPrint.Text = "F9：印刷"
    ' 再読込ボタン
    ButtonReflesh.Text = "F5：再読込"

    ' 終了ボタン
    ButtonEnd.CausesValidation = False
    ' 印刷ボタン
    ButtonPrint.CausesValidation = False

    ' IPC通信起動
    InitIPC(PRG_ID)

    ' グリッドノ選択状況を隠す
    '    DG2V1.DefaultCellStyle.SelectionBackColor = DG2V1.DefaultCellStyle.BackColor
    '    DG2V1.DefaultCellStyle.SelectionForeColor = DG2V1.DefaultCellStyle.ForeColor

    ' 非表示 → 表示時処理設定
    MyBase.lcCallBackShowFormLc = AddressOf ReStartPrg

    Controlz(DG2V1.Name).AutoSearch = False

    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Me.CmbDateShukaBi_01

  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Ryo_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      ' F5キー押下時
      Case Keys.F5
        Me.ButtonReflesh.Focus()
        Me.ButtonReflesh.PerformClick()
      ' F9キー押下時
      Case Keys.F9
        ' 印刷ボタン押下処理
        Me.ButtonPrint.Focus()
        Me.ButtonPrint.PerformClick()
      Case Keys.F12
        ' 終了ボタン押下処理
        Me.ButtonEnd.Focus()
        Me.ButtonEnd.PerformClick()
    End Select

  End Sub

  ''' <summary>
  ''' 得意先名の選択
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbDateShukaBi_01_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDateShukaBi_01.SelectedIndexChanged

    DataGrid_ShowList()

  End Sub

  ''' <summary>
  ''' 出荷日の選択
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbMstCustomer_01_TextChanged(sender As Object, e As EventArgs) Handles CmbMstCustomer_01.TextChanged

    DataGrid_ShowList()

  End Sub


  ''' <summary>
  ''' 未発行のみチェックボックス
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CHK_P_Click(sender As Object, e As EventArgs) Handles CHK_P.Click

    DataGrid_ShowList()

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

      ' プロセス動作中の場合、処理を行わない
      If (RyoProcessStatus()) Then
        ' レポートを前面に表示する
        ProcessActiveByWindowTitle("量目表（セット）")
        ProcessActiveByWindowTitle("量目表（単品）")
        Return
      End If

      ' 印刷プレビューの表示有無 
      If clsGlobalData.PRINT_PREVIEW = 1 Then
        rtn = clsCommonFnc.ComMessageBox("量目表の画面表示を行いますか？",
                                       "量目表印刷処理",
                                       typMsgBox.MSG_NORMAL,
                                       typMsgBoxButton.BUTTON_OKCANCEL)
      Else
        rtn = clsCommonFnc.ComMessageBox("印刷を行います。よろしいですか？",
                                       "量目表印刷処理",
                                       typMsgBox.MSG_NORMAL,
                                       typMsgBoxButton.BUTTON_OKCANCEL)
      End If

      ' 確認メッセージボックスで、ＯＫボタン選択時
      If rtn = typMsgBoxResult.RESULT_OK Then

        '印刷ボタン非表示
        ButtonPrint.Enabled = False

        Try
          Report_Prn()
        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        End Try

        '印刷ボタン再表示
        ButtonPrint.Enabled = True

      Else

        CmbDateShukaBi_01.Focus()

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
    flgInit = False
    Controlz(DG2V1.Name).AutoSearch = False
    Controlz(DG2V1.Name).ResetPosition()

    MyBase.AllClear()
    Controlz(DG2V1.Name).InitSort()

    ' 未発行のみチェック済
    CHK_P.Checked = True

    RyoProcessKill()

    Me.CmbDateShukaBi_01.Focus()
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
  ''' グリッドフォーカスダブルクリック時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Dgv1CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    With DG2V1

      ' １つ目のDataGridViewオブジェクトのグリッド動作設定
      With Controlz(.Name)
        ' 得意先コード
        Dim tkCode As String = .SelectedRow("TCODE")
        ' 枝No
        Dim ebCode As String = .SelectedRow("EDANo")
        ' 商品コード
        Dim shohinCode As String = .SelectedRow("SCODE")

        Dim rtn As typMsgBoxResult

        '' 枝番号が0の場合、処理しない
        'If Val(ebCode) = 0 Then
        '  Return
        'End If

        rtn = clsCommonFnc.ComMessageBox("量目表印刷区分を取り消します。よろしいですか？",
                                   "量目表印刷処理",
                                   typMsgBox.MSG_NORMAL,
                                   typMsgBoxButton.BUTTON_OKCANCEL)

        ' 確認メッセージボックスで、ＯＫボタン選択時
        If rtn = typMsgBoxResult.RESULT_OK Then

          '選択している行の行番号の取得
          Dim i As Integer = DataGridView1.SelectedCells(0).RowIndex

          Dim dgv As DataGridView = CType(sender, DataGridView)

          PRT_FLG_UnSet(tkCode, ebCode, shohinCode)

          DataGrid_ShowList()

          lblInformation.Text = "　量目印刷フラグを変更しました。　"

        End If

      End With
    End With

  End Sub

  Private Overloads Sub BaseForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
    'Control+Rの時再表示を行う
    If (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.R Then
      Call RefleshGrid()
    End If
  End Sub

  Private Sub BtnReflesh_Click(sender As Object, e As EventArgs) Handles ButtonReflesh.Click
    Call RefleshGrid()
  End Sub



  ''' <summary>
  ''' 画面再表示時処理
  ''' </summary>
  ''' <remarks>
  ''' 非表示→表示時に実行
  ''' FormLoad時に設定
  ''' </remarks>
  Private Sub ReStartPrg()
    ' 検索コンボボックス初期化
    Call InitSelectCmb()

    CHK_P.Checked = True

    DataGrid_ShowList()

  End Sub
#End Region

#Region "SQL関連"

  ''' <summary>
  ''' 量目表一時テーブル作成SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSetTmpRyouMoku() As String

    Dim sql As String = String.Empty

    sql &= " DROP TABLE IF EXISTS " & tmpRyoumokuTblName & ";"

    sql &= " CREATE TABLE " & tmpRyoumokuTblName
    sql &= "      (RENNO      int PRIMARY KEY,"
    sql &= "       SHUKAYMD   Datetime,"
    sql &= "       TCODE      numeric(7,0),"
    sql &= "       TNAME　    varchar(30),"
    sql &= "       SETCD      numeric(6,0),"
    sql &= "       SETNAME    varchar(40),"
    sql &= "       EDANo      numeric(6,0),"
    sql &= "       BUICODE    numeric(6,0),"
    sql &= "       SCODE      numeric(8,0),"
    sql &= "       SNAME      varchar(40),"
    sql &= "       LJYU       numeric(10,1),"
    sql &= "       RJYU       numeric(10,1),"
    sql &= "       KEIJYU     numeric(10,1),"
    sql &= "       TANKA      numeric(10,0),"
    sql &= "       TANKARPT   numeric(10,0),"
    sql &= "       KINGAKU    numeric(10,0),"
    sql &= "       KINGAKURPT numeric(10,0),"
    sql &= "       INJI_RYO2  varchar(2),"
    sql &= "       GENSN      varchar(60),"
    sql &= "       KOTAINO    varchar(10),"
    sql &= "       JISYA      varchar(60),"
    sql &= "       JYOUJYOU   varchar(5),"
    sql &= "       HINSYU     varchar(12),"
    sql &= "       BRAND_NAME varchar(30),"
    sql &= "       KDATE      Datetime)"

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' 量目表クエリー
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlRyomokuQuery() As String

    Dim sql As String = String.Empty

    '  （量目表クエリー）
    sql = " SELECT CUTJ.SYUKKABI"
    sql &= "      ,CUTJ.UTKCODE "
    sql &= "      ,TOKUISAKI.LTKNAME "
    sql &= "      ,CUTJ.SETCD "
    sql &= "      ,SHOHIN.HINMEI "
    sql &= "      ,CUTJ.EBCODE "
    sql &= "      ,CUTJ.SHOHINC "
    sql &= "      ,BUIM.BINAME "
    sql &= "      ,CUTJ.SAYUKUBUN "
    sql &= "      ,(CUTJ.JYURYO) AS JYURYOG "
    sql &= "      ,CUTJ.BAIKA "
    sql &= "      ,CUTJ.KFLG "
    sql &= "      ,CUTJ.TANKA "
    sql &= "      ,CUTJ.KOTAINO "
    sql &= "      ,CUTJ.GENSANCHIC "
    sql &= "      ,EDAB.EDC AS JYOUJYOU "
    sql &= "      ,KIKA.KKNAME AS HINSYU "
    sql &= "      ,BLOCK_TBL.BLNAME AS BRAND_NAME "
    sql &= "      ,CUTJ.BICODE AS BUICODE "
    sql &= " FROM (((((CUTJ LEFT JOIN TOKUISAKI ON CUTJ.UTKCODE = TOKUISAKI.TKCODE) "
    sql &= " LEFT JOIN BUIM ON CUTJ.BICODE = BUIM.BICODE) "
    sql &= " LEFT JOIN SHOHIN ON CUTJ.SETCD = SHOHIN.SHCODE "
    sql &= "                 AND CUTJ.GBFLG = SHOHIN.GBFLG )"
    sql &= " LEFT JOIN EDAB ON CUTJ.KOTAINO = EDAB.KOTAINO AND CUTJ.EBCODE = EDAB.EBCODE ) "
    sql &= " LEFT JOIN KIKA ON CUTJ.KIKAKUC = KIKA.KICODE) "
    sql &= " LEFT JOIN BLOCK_TBL ON CUTJ.BLOCKCODE = BLOCK_TBL.BLOCKCODE "
    sql &= " WHERE CUTJ.NSZFLG = 2 "      ' 入出庫FLG ２：出庫
    sql &= "   AND CUTJ.DKUBUN = 0 "
    sql &= "   AND CUTJ.NKUBUN = 0 "

    '  （未発行のみかどうか）
    If CHK_P.Checked Then
      ' 更新FLGが0の場合「未」
      sql &= " AND (CUTJ.KFLG = 0) "
    End If

    '  （出荷日指定かどうか）
    If IsDate(CmbDateShukaBi_01.Text) Then
      sql &= "AND CUTJ.SYUKKABI = '" & CmbDateShukaBi_01.Text & "'"
    Else
      Dim dt As DateTime = DateTime.Parse(ComGetProcTime())
      sql &= "AND CUTJ.SYUKKABI >= '" & DateAdd(DateInterval.Day, -7, dt).ToString("yyyy/MM/dd") & "'"
    End If

    '  （得意先が指定かどうか）
    If Len(CmbMstCustomer_01.Text) > 0 Then
      sql &= "AND CUTJ.UTKCODE = " & Val(CmbMstCustomer_01.Text)
    End If

    sql &= " ORDER BY CUTJ.SYUKKABI "
    sql &= "         ,CUTJ.UTKCODE "
    sql &= "         ,CUTJ.SETCD"
    sql &= "         ,CUTJ.EBCODE"
    sql &= "         ,CUTJ.BICODE"
    sql &= "         ,CUTJ.SHOHINC"
    sql &= "         ,CUTJ.SAYUKUBUN"

    Return sql

  End Function

  ''' <summary>
  ''' 原産マスタ読込用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 原産コードを指定した得意先マスタ情報を取得する
  ''' </remarks>
  Private Function SqlReadGensan() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * FROM GENSN"
    sql &= " WHERE KUBUN <> 9 "
    sql &= " ORDER BY GNCODE;"

    Return sql

  End Function

  ''' <summary>
  ''' 量目表印刷区分の検索SQL文作成
  ''' </summary>
  ''' <param name="tkCode">得意先コード</param>
  ''' <param name="edaNo">枝番号</param>
  ''' <param name="shohinCd">商品番号</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelectRyouMokuFlg(tkCode As String,
                                        edaNo As String,
                                        shohinCd As String) As String

    Dim sql As String = String.Empty

    sql &= " SELECT * FROM CUTJ"
    sql &= " WHERE SYUKKABI = '" & CmbDateShukaBi_01.Text & "'"
    sql &= "   AND UTKCODE = " & tkCode
    sql &= "   AND EBCODE = " & edaNo
    sql &= "   AND SHOHINC = " & shohinCd
    sql &= "   AND NSZFLG = 2 "       ' 入出庫FLG ２：出庫
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KFLG = 1 "         ' 更新FLGが1の場合「済」


    Return sql

  End Function

  ''' <summary>
  ''' 量目表印刷区分の設定SQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">更新日付</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdRyouMokuFlgOn(prmProcTime As DateTime) As String

    Dim sql As String = String.Empty

    ' 出荷日と得意先コード
    sql &= " UPDATE CUTJ SET KFLG = 1 ,"
    sql &= "                 KDATE = '" & prmProcTime & "'"
    sql &= " WHERE SYUKKABI = '" & CmbDateShukaBi_01.Text & "'"
    If (Val(CmbMstCustomer_01.Text) <> 0) Then
      sql &= "   AND UTKCODE = " & Val(CmbMstCustomer_01.Text)
    End If
    sql &= "   AND NSZFLG = 2 "       ' 入出庫FLG ２：出庫
    sql &= "   AND DKUBUN = 0 "


    Return sql

  End Function

  ''' <summary>
  ''' 量目表印刷区分の解除SQL文作成
  ''' </summary>
  ''' <param name="prmProcTime">更新日付</param>
  ''' <param name="tkCode">得意先コード</param>  
  ''' <param name="edaNo">枝番号</param>
  ''' <param name="shohinCd">商品番号</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdRyouMokuFlgOff(prmProcTime As DateTime,
                                        tkCode As String,
                                        edaNo As String,
                                        shohinCd As String) As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ SET KFLG = 0 ,"
    sql &= "                 KDATE = '" & prmProcTime & "'"
    sql &= " WHERE SYUKKABI = '" & CmbDateShukaBi_01.Text & "'"
    sql &= "   AND UTKCODE = " & tkCode
    sql &= "   AND EBCODE = " & edaNo
    sql &= "   AND SHOHINC = " & shohinCd
    sql &= "   AND NSZFLG = 2 "     ' 入出庫FLG ２：出庫
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KFLG = 1 "       ' 更新FLGが1の場合「済」


    Return sql

  End Function

  ''' <summary>
  ''' 量目表テーブル追加SQL文作成
  ''' </summary>
  ''' <param name="tblName">テーブル名</param>
  ''' <param name="tmpRow">設定値</param>
  ''' <param name="dt">更新日付</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsRyouMoku(tblName As String,
                                  tmpRow As DataRow,
                                  dt As DateTime) As String

    Dim sql As String = String.Empty


    ' 連番が未設定の場合、SQL文を作成しない
    If String.IsNullOrWhiteSpace(tmpRow("RENNO").ToString) Then
      Return sql
    End If

    sql &= " INSERT INTO " & tblName
    sql &= "                   ( RENNO "                      '01:                       
    sql &= "                   , SHUKAYMD "                   '02:
    sql &= "                   , TCODE "                      '03:
    sql &= "                   , TNAME "                      '04:  
    sql &= "                   , SETCD "                      '05:
    sql &= "                   , SETNAME "                    '06:
    sql &= "                   , EDANo "                      '07:
    sql &= "                   , SCODE "                      '08:
    sql &= "                   , SNAME "                      '09:
    sql &= "                   , LJYU "                       '10:
    sql &= "                   , RJYU "                       '11:
    sql &= "                   , KEIJYU "                     '12:
    sql &= "                   , TANKA "                      '13:
    sql &= "                   , TANKARPT "                   '14:
    sql &= "                   , KINGAKU "                    '15:
    sql &= "                   , KINGAKURPT "                 '16:
    sql &= "                   , INJI_RYO2 "                  '17:
    sql &= "                   , GENSN "  　　　              '18:
    sql &= "                   , KOTAINO "                    '19:
    sql &= "                   , JISYA "                      '20:
    sql &= "                   , KDATE "                      '21:
    sql &= "                   , JYOUJYOU"                    '22:
    sql &= "                   , HINSYU "                     '23:
    sql &= "                   , BRAND_NAME "                 '24:
    sql &= "                   , BUICODE"                     '25:
    sql &= ") VALUES("

    sql &= tmpRow("RENNO").ToString & ","                     '01:
    If String.IsNullOrWhiteSpace(tmpRow("SHUKAYMD").ToString) Then
      sql &= "Null,"                                          '02:
    Else
      sql &= "'" & tmpRow("SHUKAYMD").ToString & "'" & ","    '02:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("TCODE").ToString) Then
      sql &= "0,"                                          '03:
    Else
      sql &= tmpRow("TCODE").ToString & ","                   '03:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("TNAME").ToString) Then
      sql &= "NULL,"                                          '03:
    Else
      sql &= "'" & tmpRow("TNAME").ToString & "'" & ","       '04:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("SETCD").ToString) Then
      sql &= "0,"                                          '05:
    Else
      sql &= tmpRow("SETCD").ToString & ","                   '05:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("SETNAME").ToString) Then
      sql &= "NULL,"                                          '06:
    Else
      sql &= "'" & tmpRow("SETNAME").ToString & "'" & ","     '06:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("EDANo").ToString) Then
      sql &= "0,"                                          '07:
    Else
      sql &= tmpRow("EDANo").ToString & ","                   '07:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("SCODE").ToString) Then
      sql &= "0,"                                          '08:
    Else
      sql &= tmpRow("SCODE").ToString & ","                   '08:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("SNAME").ToString) Then
      sql &= "NULL,"                                          '09:
    Else
      sql &= "'" & tmpRow("SNAME").ToString & "'" & ","       '09:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("LJYU").ToString) Then
      sql &= "0,"                                          '10:
    Else
      sql &= tmpRow("LJYU").ToString & ","                  　'10:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("RJYU").ToString) Then
      sql &= "0,"                                          '11:
    Else
      sql &= tmpRow("RJYU").ToString & ","                  　'11:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("KEIJYU").ToString) Then
      sql &= "0,"                                          '12:
    Else
      sql &= tmpRow("KEIJYU").ToString & ","                　'12:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("TANKA").ToString) Then
      sql &= "0,"                                          '13:
    Else
      sql &= tmpRow("TANKA").ToString & ","               　  '13:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("TANKARPT").ToString) Then
      sql &= "0,"                                          '14:
    Else
      sql &= tmpRow("TANKARPT").ToString & ","              　'14:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("KINGAKU").ToString) Then
      sql &= "0,"                                          '15:
    Else
      sql &= tmpRow("KINGAKU").ToString & ","               　'15:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("KINGAKURPT").ToString) Then
      sql &= "0,"                                          '16:
    Else
      sql &= tmpRow("KINGAKURPT").ToString & ","              '16:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("INJI_RYO2").ToString) Then
      sql &= "NULL,"                                          '17:
    Else
      sql &= "'" & tmpRow("INJI_RYO2").ToString & "'" & ","   '17:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("GENSN").ToString) Then
      sql &= "NULL,"                                          '18:
    Else
      sql &= "'" & tmpRow("GENSN").ToString & "'" & ","       '18:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("KOTAINO").ToString) Then
      sql &= "NULL,"                                          '19:
    Else
      sql &= "'" & tmpRow("KOTAINO").ToString() & "'" & ","   '19:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("JISYA").ToString) Then
      sql &= "Null,"                                          '20:
    Else
      sql &= "'" & tmpRow("JISYA").ToString & "'" & ","       '20:
    End If

    sql &= tmpRow("KDATE").ToString & "'" & dt.ToString & "',"       '21:

    If String.IsNullOrWhiteSpace(tmpRow("JYOUJYOU").ToString) Then
      sql &= "Null,"                                          '22:
    Else
      sql &= "'" & tmpRow("JYOUJYOU").ToString & "',"         '22:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("HINSYU").ToString) Then
      sql &= "Null,"                                          '23:
    Else
      sql &= "'" & tmpRow("HINSYU").ToString & "',"           '23:
    End If

    If String.IsNullOrWhiteSpace(tmpRow("BRAND_NAME").ToString) Then
      sql &= "Null,"                                          '24:
    Else
      sql &= "'" & tmpRow("BRAND_NAME").ToString & "',"       '24:
    End If

    'BUICODE
    If String.IsNullOrWhiteSpace(tmpRow("BUICODE").ToString) Then
      sql &= "Null)"                                          '24:
    Else
      sql &= "'" & tmpRow("BUICODE").ToString & "')"       '24:
    End If

    Console.WriteLine(sql)

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
  '''集計項目初期設定
  ''' </summary>
  ''' <returns>CUTJ項目のみ設定した連想配列</returns>
  Private Function TargetValNewRyouMokuVal() As Dictionary(Of String, String)

    Dim ret As New Dictionary(Of String, String)

    With ret

      ' 日付形式データ
      ' 空白は"NULL"に置き換え
      Dim tmpKeyName As String = String.Empty

      .Add("RENNO", "1")                   ' 表示順番
      .Add("SHUKAYMD", "")                 ' 出荷年月日
      .Add("TCODE", "")                    ' 得意先コード
      .Add("TNAME", "")                    ' 得意先名  
      .Add("SETCD", "")                    ' セットコード
      .Add("SETNAME", "")                  ' セット名称
      .Add("EDANo", "")                    ' 枝番コード
      .Add("BUICODE", "")                  ' 部位枝番コード
      .Add("SCODE", "")                    ' 商品コード
      .Add("SNAME", "")                    ' 商品名
      .Add("LJYU", "")                     ' 左重量
      .Add("RJYU", "")                     ' 右重量
      .Add("KEIJYU", "")                   ' 合計重量  
      .Add("TANKA", "")                    ' 単価
      .Add("TANKARPT", "")                 ' 単価
      .Add("KINGAKU", "")                  ' 金額
      .Add("KINGAKURPT", "")               ' 金額
      .Add("INJI_RYO2", "")                ' 量目表印字：未　：済
      .Add("GENSN", "")                    ' 原産地名
      .Add("KOTAINO", "")                  ' 個体識別
      .Add("JISYA", "")                    ' 自社名
      .Add("KDATE", "")                    ' 更新日付
      .Add("JYOUJYOU", "")                 ' 上場番号
      .Add("HINSYU", "")                   ' 品種
      .Add("BRAND_NAME", "")               ' ブランド名

    End With

    Return ret
  End Function

#End Region

End Class

