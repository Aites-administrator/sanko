Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.DgvForm01
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonCtrl
Imports System.Text
Imports System.IO

Public Class OutCsvData
  Implements IDgvForm01

  '----------------------------------------------
  '          出荷データ出力画面
  '
  '
  '----------------------------------------------
#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "keiryo"
  Private Const PRG_TITLE As String = "計量データ出力処理"

  ''' <summary>
  ''' テーブル名定数
  ''' </summary>
  Private Const ITEM_CODE As String = "ITEM_CODE"
  Private Const JURYO As String = "JYURYO"
  Private Const SEIZOUBI As String = "SEIZOUBI"
  Private Const CARTONID As String = "CARTONID"
  Private Const KOTAINO As String = "KOTAINO"
  Private Const EDABAN As String = "EDABAN"
  Private Const LOTNO As String = "LOTNO"
  Private Const CUTKIKAKUNO As String = "CUTKIKAKUNO"
  Public Const MOJI_CODE As String = "Shift-JIS"


#End Region
#End Region

#Region "プライベート"

  ''' <summary>
  ''' Gridより選択されたデータ
  ''' </summary>
  ''' <remarks>
  ''' ダブルクリック・Enter入力時に編集対象として保持する
  ''' </remarks>
  Private _SelectedData As New Dictionary(Of String, String)
  ' データグリッドのフォーカス喪失時の位置
  Private rowIndexDataGrid As Integer
  ' データグリッドの選択件数
  Private dataGridCount As Long
  ' データグリッドの検索日付
  Private dataGridDate As String
  ' 個体識別番号入力チェックキャンセル判定
  Private TxtKotaiNo1CancelFlg As Boolean
  'エンコーディング
  Private encoding As Encoding = Encoding.GetEncoding(MOJI_CODE)

#End Region

#Region "データグリッドビュー２つの画面用操作関連共通"
  '１つ目のDataGridViewオブジェクト格納先
  Private DG2V1 As DataGridView

  ''' <summary>
  ''' 初期化処理
  ''' </summary>
  ''' <remarks>
  ''' コントロールの初期化（Form_Loadで実行して下さい）
  ''' </remarks>
  Private Sub InitForm() Implements IDgvForm01.InitForm
    ' データグリッドのフォーカス喪失時の位置初期化
    rowIndexDataGrid = -1


    '入庫明細オブジェクトの設定
    DG2V1 = Me.DataGridView1

    ' グリッド初期化
    Call InitGrid(DG2V1, CreateGridSrc(), CreateGridlayout())

    ' 入庫明細設定
    With DG2V1

      '表示する
      .Visible = True

      ' グリッド動作設定
      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 0

        ' 検索コントロール設定
        .AddSearchControl(Me.CmbMstItem1, "BICODE", typExtraction.EX_EQ, typColumnKind.CK_Text)
        .AddSearchControl(Me.CmbDateKakouBi1, "KAKOUBI", typExtraction.EX_GTE, typColumnKind.CK_Date)
        .AddSearchControl(Me.CmbDateKakouBi2, "KAKOUBI", typExtraction.EX_LTE, typColumnKind.CK_Date)
        .AddSearchControl(Me.TxKotaiNo1, "CUTJ.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Text)
        .AddSearchControl(Me.TxtEdaban1, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbCustomerSelect1, "GROUP_ID", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.CmbSayu1, "SAYUKUBUN", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' 編集可能列設定
        .EditColumnList = CreateGridEditCol()

      End With
    End With

  End Sub

  ''' <summary>
  ''' DataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  'Private Function CreateGridSrc() As String Implements IDgvForm01.CreateGridSrc
  '  Dim sql As String = String.Empty
  '  Dim tmpDb As New clsSqlServer
  '  Dim tmpDt As New DataTable

  '  tmpDb.GetResult(tmpDt, GetFixColumnTblSql)

  '  sql &= " Select "
  '  sql &= "'" & tmpDt.Select("COLUMN_NAME = 'PACKING_CODE'")(0).Item("COLUMN_VALUE").ToString.Replace(Chr(13), "").Replace(Chr(10), "") & "'+'" & tmpDt.Select("COLUMN_NAME = 'JAN_MAKER_CODE'")(0).Item("COLUMN_VALUE").ToString.Replace(Chr(13), "").Replace(Chr(10), "") & "'+" & "trim(str(CUTJ.GBFLG)) " & "+" & "trim(str(RIGHT(CUTJ.SHOHINC,4)))" & "AS ITEM_CODE "
  '  sql &= ", FORMAT(CUTJ.JYURYO,REPLICATE('0', 6)) AS JYURYO "
  '  sql &= ", TRIM(CONVERT(varchar,FORMAT(CONVERT(date,CUTJ.KAKOUBI),'yyMMdd'))) AS SEIZOUBI"
  '  sql &= ",'" & tmpDt.Select("COLUMN_NAME = 'OFFICE_CODE'")(0).Item("COLUMN_VALUE").ToString.Replace(Chr(13), "").Replace(Chr(10), "") & "'+" & "CONVERT(varchar,FORMAT(CUTJ.TOOSINO, REPLICATE('0',4)))" & "+" & "CONVERT(varchar,FORMAT(CUTJ.KIKAINO, REPLICATE('0',10))) AS CARTONID "
  '  sql &= ", FORMAT(ISNULL(EDAB.KOTAINO,0),REPLICATE('0',10)) AS KOTAINO "
  '  sql &= ", CONVERT(varchar,FORMAT(ISNULL(EDAB.TJCODE,0), REPLICATE('0',3))) + RIGHT (FORMAT(CUTJ.EBCODE,REPLICATE('0', 4)), 4) +CONVERT(varchar,FORMAT(isnull(CUTJ.SAYUKUBUN,0), REPLICATE('0',1))) + CONVERT(varchar,FORMAT(isnull(EDAB.SYUBETUC,0), REPLICATE('0',1))) + CONVERT(varchar,FORMAT(CUTJ.KIKAKUC, REPLICATE('0',2))) + CONVERT(varchar,FORMAT(CUTJ.GENSANCHIC, REPLICATE('0',2))) + '" & tmpDt.Select("COLUMN_NAME = 'JAS_KUBUN'")(0).Item("COLUMN_VALUE").ToString.Replace(Chr(13), "").Replace(Chr(10), "") & "' As  EDABAN "
  '  sql &= ", '" & tmpDt.Select("COLUMN_NAME = 'LOTNO'")(0).Item("COLUMN_VALUE").ToString.Replace(Chr(13), "").Replace(Chr(10), "") & "' AS LOTNO"
  '  sql &= ", '" & tmpDt.Select("COLUMN_NAME = 'CUTKIKAKUNO'")(0).Item("COLUMN_VALUE").ToString.Replace(Chr(13), "").Replace(Chr(10), "") & "' AS CUTKIKAKUNO"
  '  sql &= " FROM	CUTJ "
  '  sql &= " LEFT JOIN EDAB "
  '  sql &= " On EDAB.EBCODE = CUTJ.EBCODE "
  '  sql &= " And EDAB.KOTAINO = CUTJ.KOTAINO "
  '  sql &= " where CUTJ.BICODE <> 0 "
  '  sql &= " And CUTJ.NSZFLG = 2 "
  '  Return sql
  'End Function

  ''' <summary>
  ''' DataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  Private Function CreateGridSrc() As String Implements IDgvForm01.CreateGridSrc
    Dim sql As String = String.Empty
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim tmpCustmerDb As New clsSqlServer
    Dim tmpCustmerDt As New DataTable
    Dim clsCustomerSelect As New clsCustomerSelect
    Dim nowCnt As Integer = 0
    Dim lastCnt As Integer = 0

    tmpDb.GetResult(tmpDt, GetColumnDetailTblSql)
    '' TODO 得意先設定を抽出 (OUTCSVDATAのクラスを作成 共有部分を取得)
    'tmpCustmerDt = clsCustomerSelect.GetSettingCustomerTbl(Me.CmbMstCustomer1)
    'lastCnt = tmpCustmerDt.Rows.Count

    sql &= " Select "
    sql &= GetColumnValue(tmpDt, ITEM_CODE)
    sql &= GetColumnValue(tmpDt, JURYO)
    sql &= GetColumnValue(tmpDt, SEIZOUBI)
    sql &= GetColumnValue(tmpDt, CARTONID)
    sql &= GetColumnValue(tmpDt, KOTAINO)
    sql &= GetColumnValue(tmpDt, EDABAN)
    sql &= GetColumnValue(tmpDt, LOTNO)
    sql &= GetColumnValue(tmpDt, CUTKIKAKUNO)
    '結合テーブル設定
    sql &= GetTableValue()
    sql &= " where (CUTJ.KUBUN = 1 "
    sql &= " AND  ((KYOKUFLG = 0 "
    sql &= " AND    KIKAINO <> 999 "
    sql &= " AND    NKUBUN = 0 "
    sql &= " AND    NSZFLG <> 1) "
    sql &= "  OR    NSZFLG = 4)  "
    sql &= " AND  LABELJI > 0  "
    sql &= " AND GBFLG = 2 "
    sql &= " AND  LABELJI > 0  "
    sql &= " AND GBFLG = 2  "
    sql &= " )  "
    'If lastCnt > 0 Then
    '  sql &= " AND TKCODE IN ( "
    '  For Each tmpRow In tmpCustmerDt.Rows
    '    nowCnt = nowCnt + 1
    '    sql &= tmpRow("CUSTOMER_CODE") & If(nowCnt = lastCnt, " ", ", ")
    '  Next
    '  sql &= " ) "
    'End If
    Return sql
  End Function


  ''' <summary>
  ''' DataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  Private Function CreateGridlayout() As List(Of clsDGVColumnSetting) Implements IDgvForm01.CreateGridlayout
    Dim ret As New List(Of clsDGVColumnSetting)
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    tmpDb.GetResult(tmpDt, GetColumnSettingTblSql)

    With ret
      For Each tmpDr As DataRow In tmpDt.Rows
        .Add(New clsDGVColumnSetting(tmpDr.Item("DISP_COLUMN_NAME").ToString, tmpDr.Item("COLUMN_NAME").ToString, argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=180))
      Next

    End With

    Return ret

  End Function

  ''' <summary>
  ''' DataGridViewオブジェクトの一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  Private Function CreateGridEditCol() As List(Of clsDataGridEditTextBox) Implements IDgvForm01.CreateGridEditCol
    Dim ret As New List(Of clsDataGridEditTextBox)

    Return ret

  End Function

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, OutCsvData, AddressOf ComRedisplay)
  End Sub
#End Region


  Private Sub OutCsvData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.Text = PRG_TITLE
    Me.CmbDateKakouBi1.SelectedIndex = 0
    Me.CmbDateKakouBi2.SelectedIndex = 0
    Me.CmbCustomerSelect1.SelectedIndex = 0
    Me.CmbCustomerSelect1.DropDownStyle = ComboBoxStyle.DropDownList

    Call InitForm()
    Controlz(DG2V1.Name).AutoSearch = True

    ' IPC通信起動
    InitIPC(PRG_ID)

  End Sub

  Private Sub BtnCsv_Click(sender As Object, e As EventArgs) Handles BtnCsv.Click
    Dim tmpDate As String
    Dim tmpFolder As String = GetRemovable()
    Dim tmpExistsFolder As Boolean = (tmpFolder <> "") 'フォルダ取得できた時True
    Dim tmpOpenFolder As String = String.Empty

    tmpDate = ComGetProcTime()
    tmpDate = DateTime.Parse(tmpDate).ToString("yyyyMMddHHmmss")

    Dim BackUpFolderName As String = "..\keiryo_csv"
    Dim BackUpFileName As String = BackUpFolderName & "\keiryo_data_" & tmpDate & ".csv"
    Dim SendFolderName As String = GetRemovable() & "SendData"
    Dim SendFileName As String = SendFolderName & "\送信データ.csv"

    'OutCsv(Controlz(DG2V1.Name).GetAllData(), BackUpFileName, True)
    'System.Diagnostics.Process.Start(
    '"EXPLORER.EXE", "/select," & BackUpFileName & "")

    tmpOpenFolder = If(tmpExistsFolder, SendFileName, BackUpFileName)

    OutCsv(Controlz(DG2V1.Name).GetAllData(), BackUpFileName, Not tmpExistsFolder)
    If tmpOpenFolder = SendFileName Then
      OutCsv(Controlz(DG2V1.Name).GetAllData(), SendFileName, tmpExistsFolder)
    Else
      ComMessageBox("USBが接続されておりません。手動でUSBにコピーしてください。", "USB有無確認", typMsgBox.MSG_NORMAL, typMsgBoxButton.BUTTON_OK)
    End If

    System.Diagnostics.Process.Start(
    "EXPLORER.EXE", "/select," & tmpOpenFolder & "")
  End Sub

  Private Sub BtnCustomerSelect_Click(sender As Object, e As EventArgs) Handles BtnCustomerSelect.Click
    CustomerSelect.Show()
  End Sub



  Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
    Me.Close()
  End Sub

#Region "項目値操作"
  Private Function AddCheckDigit(prmText As String) As String
    Dim len As Integer = 0
    Dim ret As String = prmText
    Dim sum As Integer = 0

    len = prmText.Length


    If String.IsNullOrWhiteSpace(prmText) Then
      Return ret
    End If


    If (len < 14) Then

      For Dataindex = 1 To len - 1
        If Not (Dataindex Mod 2 = 0) Then
          sum = sum + (Mid(prmText, Dataindex, 1) * 3)
        Else
          sum = sum + (Mid(prmText, Dataindex, 1) * 1)
        End If
      Next

      Dim NotMuch As Integer = 0

      NotMuch = sum Mod 10

      '// チェックディジット付与
      If NotMuch = 0 Then
        ret = Replace(prmText, """", "") & 0
      Else
        ret = Replace(prmText, """", "") & (10 - NotMuch)
      End If

    End If

    Return ret
  End Function

  Private Function GetColumnValue(prmDt As DataTable, ColumnName As String) As String
    Dim sql As String = String.Empty
    Dim tmpDtRows As DataRow()

    tmpDtRows = prmDt.Select("CSV_COLUMN = '" & ColumnName & "'")

    If tmpDtRows(0).Item("DISP_SETTING").ToString = "0" Then
      Return sql
    End If

    If tmpDtRows(0).Item("DISP_ORDER").ToString <> "1" Then
      sql += ","
    End If


    For Each tmpRow In tmpDtRows
      If tmpRow("STRING_KUBUN") = 1 Then
        sql += "+ '" & tmpRow("COLUMN_NAME") & "'"
      Else
        sql += "+ CONVERT(varchar, "
        If tmpRow("COLUMN_TYPE") = "date" Then
          sql += "+  TRIM(CONVERT(varchar,FORMAT(CONVERT(date," & tmpRow("TABLE_NAME") & "." & tmpRow("COLUMN_NAME") & "),'" & tmpRow("COLUMN_FORMAT") & "'))))"
        ElseIf tmpRow("COLUMN_TYPE") = "right" Then
          sql += "+ RIGHT(REPLICATE('" & tmpRow("COLUMN_FORMAT") & "'," & tmpRow("COLUMN_LENGTH") & ") + TRIM(STR(" & tmpRow("TABLE_NAME") & "." & tmpRow("COLUMN_NAME") & "))," & tmpRow("COLUMN_LENGTH") & "))"
        ElseIf tmpRow("COLUMN_TYPE") = "weight" Then
          sql += "+ FORMAT(" & tmpRow("TABLE_NAME") & "." & tmpRow("COLUMN_NAME") & "/10" & ", REPLICATE('" & tmpRow("COLUMN_FORMAT") & "'," & tmpRow("COLUMN_LENGTH") & ")))"
        Else
          sql += "+ FORMAT(" & tmpRow("TABLE_NAME") & "." & tmpRow("COLUMN_NAME") & ", REPLICATE('" & tmpRow("COLUMN_FORMAT") & "'," & tmpRow("COLUMN_LENGTH") & ")))"
        End If
      End If

    Next

    sql += "AS " & ColumnName


    Return sql
  End Function

  Private Function GetTableValue() As String
    Dim sql As String = String.Empty
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    tmpDb.GetResult(tmpDt, GetTableNameSql)


    For Each tmpRow As DataRow In tmpDt.Rows
      If tmpRow("TABLE_TYPE") = "MAIN" Then
        sql += " FROM " & tmpRow("TABLE_NAME")
      Else
        If (tmpRow("JOIN_KEY_ORDER") = 1) Then

          sql += " LEFT JOIN " & tmpRow("TABLE_NAME")
          sql += " ON " & tmpRow("TABLE_NAME") & "." & tmpRow("JOIN_KEY") & "=" & tmpRow("JOIN_TABLE") & "." & tmpRow("JOIN_KEY")
        Else
          sql += " AND " & tmpRow("TABLE_NAME") & "." & tmpRow("JOIN_KEY") & "=" & tmpRow("JOIN_TABLE") & "." & tmpRow("JOIN_KEY")
        End If
      End If

    Next


    Return sql
  End Function

  '------------------------------------------------------------------------------
  ' Excel出力処理
  ' 2022/2/21 kohmoto.T Add 
  ' Excel出力
  '------------------------------------------------------------------------------
  Private Function OutCsv(prmDtList As List(Of Dictionary(Of String, String)), fileName As String, Optional ByRef MsgFlg As Boolean = False)
    Dim BlnErr As Boolean = False
    Dim cnt As Integer = 0
    Dim ColumnCount As Integer = 0
    Dim RowCount As Integer = 0
    Dim tmpDb As New clsSqlServer
    Dim tmpColumnDt As New DataTable
    Dim tmpColumnRow As DataRow

    Try
      '書き込むファイルを開く
      Dim sr As New IO.StreamWriter(fileName, False, encoding)

      'ヘッダ処理

      For Each tmpDt As Dictionary(Of String, String) In prmDtList
        ColumnCount = 0
        RowCount = 0
        If cnt = 0 Then
          For Each tmpCol In tmpDt
            tmpDb.GetResult(tmpColumnDt, GetColumnSettingTblSql)
            Dim field As String = tmpCol.Key
            tmpColumnRow = tmpColumnDt.Select("COLUMN_NAME = '" & field & "'")(0)
            field = EncloseDoubleQuotes(tmpColumnRow.Item("DISP_COLUMN_NAME"))
            sr.Write(field)
            'カンマ付与
            If ColumnCount < tmpDt.Count - 1 Then
              sr.Write(","c)
            End If
            ColumnCount = ColumnCount + 1
          Next
          sr.Write(vbCrLf)
        End If

        For Each tmpCol In tmpDt

          Dim field As String = tmpCol.Value.Replace(Chr(13), "").Replace(Chr(10), "")

          If tmpCol.Key = ITEM_CODE Then
            field = AddCheckDigit(field)
          End If

          field = EncloseDoubleQuotes(field)
          sr.Write(field)
          'カンマ付与
          If RowCount < tmpDt.Count - 1 Then
            sr.Write(","c)
          End If
          RowCount = RowCount + 1
        Next

        cnt = cnt + 1
        sr.Write(vbCrLf)
      Next
      '閉じる
      sr.Close()

    Catch ex As Exception
      MsgBox(ex.Message)
      BlnErr = True
      Return BlnErr
      Exit Function
    Finally
    End Try

    If MsgFlg Then
      MsgBox("ファイルを出力しました。")
    End If
    Return BlnErr

  End Function

  '------------------------------------------------------------------------------
  ' ダブルクォーテーション付与処理
  ' 2022/2/21 kohmoto.T Add 
  ' 引数　field
  ' ダブルクォーテーション付与
  '------------------------------------------------------------------------------
  Private Function EncloseDoubleQuotes(field As String) As String
    Return """" & field & """"
  End Function

  '------------------------------------------------------------------------------
  ' ダブルクォーテーション付与処理
  ' 2022/2/21 kohmoto.T Add 
  ' 引数　field
  ' ダブルクォーテーション付与
  '------------------------------------------------------------------------------
  Private Function GetRemovable() As String
    Dim rtnFolder As String = String.Empty


    For Each drive As DriveInfo In DriveInfo.GetDrives()
      If drive.DriveType = DriveType.Removable Then
        rtnFolder = drive.Name
      End If

    Next


    Return rtnFolder
  End Function


#Region "SQL"
  ''' <summary>
  ''' 項目内容テーブル取得SQL
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  Private Function GetColumnDetailTblSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT "
    sql &= " CSV_COLUMN "
    sql &= " ,	 ISNULL(FIXED_COLUMN_TBL.COLUMN_VALUE,COLUMN_DETAIL_TBL.CUT_COLUMN) COLUMN_NAME "
    sql &= " ,	 COLUMN_TYPE "
    sql &= " ,	 COLUMN_LENGTH "
    sql &= " ,	 COLUMN_FORMAT "
    sql &= " ,	 STRING_KUBUN "
    sql &= " ,	 TABLE_NAME "
    sql &= " ,	 DISP_SETTING "
    sql &= " ,	 DISP_ORDER "
    sql &= " FROM	COLUMN_DETAIL_TBL "
    sql &= " LEFT JOIN FIXED_COLUMN_TBL "
    sql &= " ON COLUMN_DETAIL_TBL.CUT_COLUMN = FIXED_COLUMN_TBL.COLUMN_NAME "
    sql &= " LEFT JOIN MST_TABLE "
    sql &= " ON MST_TABLE.TABLE_ID = COLUMN_DETAIL_TBL.TABLE_ID "
    sql &= " LEFT JOIN COLUMN_SETTING_TBL "
    sql &= " ON COLUMN_SETTING_TBL.COLUMN_NAME = COLUMN_DETAIL_TBL.CSV_COLUMN "
    sql &= " ORDER BY COLUMN_DETAIL_TBL.CSV_COLUMN,JOIN_ORDER "

    Console.WriteLine(sql)

    Return sql
  End Function

  ''' <summary>
  ''' 項目内容テーブル取得SQL
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  Private Function GetTableNameSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT TABLE_TYPE "
    sql &= " ,	JOIN_KEY "
    sql &= " ,	JOIN_TABLE "
    sql &= " ,	JOIN_KEY_ORDER "
    sql &= " ,	TABLE_NAME "
    sql &= " FROM	TABLE_JOIN_SETTING "
    sql &= " LEFT JOIN MST_TABLE "
    sql &= " on MST_TABLE.TABLE_ID = TABLE_JOIN_SETTING.TABLE_ID "
    sql &= " ORDER BY MST_TABLE.TABLE_ID,JOIN_KEY_ORDER "

    Console.WriteLine(sql)

    Return sql
  End Function

  ''' <summary>
  ''' 固定値テーブル取得SQL
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  Private Function GetFixColumnTblSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT "
    sql &= "      COLUMN_NAME "
    sql &= " ,	  COLUMN_VALUE  "
    sql &= " FROM	FIXED_COLUMN_TBL "

    Console.WriteLine(sql)

    Return sql
  End Function

  ''' <summary>
  ''' 項目選択テーブル取得SQL
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  Private Function GetColumnSettingTblSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT "
    sql &= " COLUMN_NAME " ' 項目名
    sql &= " ,	 DISP_COLUMN_NAME " 'タイトル項目名
    sql &= " ,	 DISP_SETTING " '表示設定
    sql &= " ,	 DISP_ORDER " ' 列表示順
    sql &= " FROM	COLUMN_SETTING_TBL "
    sql &= " WHERE DISP_SETTING = 1 "
    sql &= " ORDER BY DISP_ORDER "

    Console.WriteLine(sql)

    Return sql
  End Function

#End Region
#End Region
End Class
