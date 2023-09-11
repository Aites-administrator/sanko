Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports T.R.ZCommonClass.clsDataGridSearchControl

Imports System

Public Class Form_ReportPrnRange


#Region "列挙体"

  ''' <summary>
  ''' 削除方法
  ''' </summary>
  Enum typMasterList
    ''' <summary>
    ''' 仕入先マスター
    ''' </summary>
    TBL_CUTSR = 1
    ''' <summary>
    ''' 得意先マスター
    ''' </summary>
    TBL_TOKUISAKI
    ''' <summary>
    ''' 店舗名マスター
    ''' </summary>
    TBL_TENPO
    ''' <summary>
    ''' 規格マスター
    ''' </summary>
    TBL_KIKA
    ''' <summary>
    ''' 格付マスター
    ''' </summary>
    TBL_KAKU
    ''' <summary>
    ''' 部位マスター
    ''' </summary>
    TBL_BUIM
    ''' <summary>
    ''' 原産地マスター
    ''' </summary>
    TBL_GENSN
    ''' <summary>
    ''' 種別マスター
    ''' </summary>
    TBL_SHUB
    ''' <summary>
    ''' 屠場マスター
    ''' </summary>
    TBL_TOJM
    ''' <summary>
    ''' 担当者マスター
    ''' </summary>
    TBL_TANTO
    ''' <summary>
    ''' ブロックコードマスター
    ''' </summary>
    TBL_BLOCK
    ''' <summary>
    ''' 牛豚ＦＬＧマスター
    ''' </summary>
    TBL_GBFLG
    ''' <summary>
    ''' コメントマスター
    ''' </summary>
    TBL_COMNT
    ''' <summary>
    ''' 製造元マスター
    ''' </summary>
    TBL_SEIZOU
    ''' <summary>
    ''' テーブル詳細マスター
    ''' </summary>
    TBL_SUM
    ''' <summary>
    ''' 商品変換マスター
    ''' </summary>
    TBL_SHENKAN
    ''' <summary>
    ''' 得意先変換マスター
    ''' </summary>
    TBL_THENKAN
    ''' <summary>
    ''' 仕入先変換マスター
    ''' </summary>
    TBL_SIHENKA
    ''' <summary>
    ''' 商品マスター
    ''' </summary>
    TBL_SHOHIN

  End Enum

#End Region

#Region "定数定義"

  Private Const PRG_TITLE As String = "マスター登録リスト"

  'ダブルクォーテーション
  Private Const CSV_SPACE As String = ControlChars.Quote

  ' カンマ
  Private Const CSV_COMMA As String = ","

  Private Const PRG_ID As String = "ReportPrn"

  Public Const TBL_CODEID As String = "codeId"

  Public Const TBL_CODENAME As String = "codeNm"

  ''' <summary>
  ''' レポートワークテーブル構造体
  ''' </summary>
  Public Structure structReport

    Public Property wSyoriNo As Integer  　  ' 処理番号
    Public Property wPrnTitle As String       ' 帳票名
    Public Property wTblName As String        ' テーブル名
    Public Property wOrderPtn As String       ' 並び順
    Public Property wSerchTblName As String   ' 検索テーブル
    Public Property wSerchCodeId As String    ' コードID
    Public Property wSerchCodeName As String  ' コード名
    Public Property wKubunChk As Boolean      ' 区分チェック
    Public Property wLeftJoinChk As Boolean   ' LeftJoinチェック
    Public Property wLeftJoin As String       ' LeftJoin条件
    Public Property wSerchFlg As Boolean      ' 絞り込み有無

  End Structure

#End Region

#Region "プライベート"
  ' 画面を閉じるボタン通知有無
  Private confirm As Boolean
  'アプリケーション名
  Private _aplTitle As String
#End Region

#Region "コンストラクタ"
  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()

    ' 画面を閉じるボタン通知する
    confirm = False

    ' この呼び出しはデザイナーで必要です。
    InitializeComponent()

    ' InitializeComponent() 呼び出しの後で初期化を追加します。

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' 一覧表示用SQL
  ''' </summary>
  ''' <returns></returns>
  Public Property aplTitle As String
    Get
      Return _aplTitle
    End Get
    Set(value As String)
      _aplTitle = value
    End Set
  End Property

  ''' <summary>
  ''' 印刷範囲の設定
  ''' </summary>
  ''' <param name="dtStart"></param>
  ''' <param name="dtEnd"></param>
  Public Sub cmbtAddItem(dtStart As DataTable, dtEnd As DataTable)

    ' 印刷範囲の開始コンボボックス選択肢設定
    cbRangeStart.DataSource = dtStart
    cbRangeStart.DisplayMember = TBL_CODENAME
    cbRangeStart.ValueMember = TBL_CODEID

    ' 印刷範囲の終了コンボボックス選択肢設定
    cbRangeEnd.DataSource = dtEnd
    cbRangeEnd.DisplayMember = TBL_CODENAME
    cbRangeEnd.ValueMember = TBL_CODEID

  End Sub

  ''' <summary>
  ''' 印刷範囲の取得
  ''' </summary>
  ''' <param name="selectStartCD"></param>
  ''' <param name="selectEndCD"></param>
  Public Sub cmbtSelectItem(ByRef selectStartCD As String,
                            ByRef selectEndCD As String)


    If (String.IsNullOrEmpty(cbRangeStart.Text)) Then
      selectStartCD = ""
    Else
      selectStartCD = Val(cbRangeStart.Text).ToString
    End If

    If (String.IsNullOrEmpty(cbRangeEnd.Text)) Then
      selectEndCD = ""
    Else
      selectEndCD = Val(cbRangeEnd.Text).ToString
    End If

  End Sub

#End Region

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
  ''' レポートデータ取得
  ''' </summary>
  ''' <param name="stReport"></param>
  Public Function Data_Set(stReport As Form_ReportPrnRange.structReport) As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True

    ' 実行
    With tmpDb
      Try

        ' 登録リスト取得SQL文作成
        sql = sqlReport(stReport)
        Dim tmpDt As New DataTable
        Call .GetResult(tmpDt, sql)

        ' データ件数が０件の場合
        If (tmpDt.Rows.Count = 0) Then
          ComMessageBox("該当するデータが存在しません。",
                        PRG_TITLE, typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
          ret = False
        Else

          If (stReport.wSerchFlg) Then

            ' 登録リスト検索用SQL文の作成
            sql = sqlSerch(stReport)
            Dim tmpSerchDt As New DataTable
            Call .GetResult(tmpSerchDt, sql)

            Dim dtStartRange As New DataTable
            Dim dtEndRange As New DataTable
            dtStartRange.Columns.Add("codeId")
            dtStartRange.Columns.Add("codeNm")
            dtEndRange.Columns.Add("codeId")
            dtEndRange.Columns.Add("codeNm")

            Dim dtRangeRow As DataRow
            Dim serchDtRow As DataRow

            ' 選択用コンボボックス作成
            For i = 0 To tmpSerchDt.Rows.Count - 1

              serchDtRow = tmpSerchDt.Rows(i)

              dtRangeRow = dtStartRange.NewRow
              dtRangeRow(Form_ReportPrnRange.TBL_CODEID) = serchDtRow(stReport.wSerchCodeId).ToString
              dtRangeRow(Form_ReportPrnRange.TBL_CODENAME) = serchDtRow(stReport.wSerchCodeId).ToString & "：" & serchDtRow(stReport.wSerchCodeName).ToString
              dtStartRange.Rows.Add(dtRangeRow)

              dtRangeRow = dtEndRange.NewRow
              dtRangeRow(Form_ReportPrnRange.TBL_CODEID) = serchDtRow(stReport.wSerchCodeId).ToString
              dtRangeRow(Form_ReportPrnRange.TBL_CODENAME) = serchDtRow(stReport.wSerchCodeId).ToString & "：" & serchDtRow(stReport.wSerchCodeName).ToString
              dtEndRange.Rows.Add(dtRangeRow)
            Next i

            ' 範囲選択画面
            Dim tmpSubForm As New Form_ReportPrnRange

            tmpSubForm.aplTitle = stReport.wPrnTitle
            tmpSubForm.cmbtAddItem(dtStartRange, dtEndRange)

            .TrnStart()

            ' 範囲選択画面表示
            Select Case tmpSubForm.ShowSubForm(Me)
              Case SFBase.typSfResult.SF_OK

                Dim cdStart As String = String.Empty
                Dim cdEnd As String = String.Empty

                ' コードの絞り込み条件取得
                tmpSubForm.cmbtSelectItem(cdStart, cdEnd)

                ' コードの絞り込み条件作成
                sql = String.Empty
                If (String.IsNullOrEmpty(cdStart) = False) Then
                  sql = sql & cdStart & " <= " & stReport.wSerchCodeId
                End If
                If (String.IsNullOrEmpty(cdEnd) = False) Then
                  If (String.IsNullOrEmpty(sql) = False) Then
                    sql = sql & " AND "
                  End If
                  sql = sql & stReport.wSerchCodeId & " <= " & cdEnd
                End If

                ' コードの絞り込み
                Dim tmpDtRows As DataRow()
                tmpDtRows = tmpDt.Select(sql)

                ' データ件数が０件の場合
                If (tmpDtRows.Count = 0) Then
                  ComMessageBox("該当するデータが存在しません。",
                              PRG_TITLE, typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
                  ret = False
                Else

                  ' レポートデータ出力
                  outputCsvReport(stReport, tmpDtRows)

                End If

              Case SFBase.typSfResult.SF_CANCEL
            End Select
          Else

            ' コードの絞り込み
            Dim tmpDtRows As DataRow()
            tmpDtRows = tmpDt.Select
            ' レポートデータ出力
            outputCsvReport(stReport, tmpDtRows)

          End If
        End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）

        ret = False
      Finally
      End Try

      Return ret

    End With

  End Function

  ''' <summary>
  ''' レポート一覧表データ出力
  ''' </summary>
  ''' <param name="stReport"></param>
  ''' <param name="tmpDtRow"></param>
  ''' <returns></returns>
  Private Function outputCsvReport(stReport As Form_ReportPrnRange.structReport,
                                   tmpDtRow As DataRow()) As Boolean

    Dim ret As Boolean = True
    'ファイルStreamWriter
    Dim sw As System.IO.StreamWriter = Nothing

    ' 実行
    Try

      'CSVファイル書込に使うEncoding
      Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

      '自分自身の実行ファイルのパスを取得する
      Dim appPath As String = System.Environment.CurrentDirectory

      Dim reportFileName As String = String.Empty
      reportFileName = stReport.wPrnTitle & ".csv"

      '書き込むファイルを開く
      sw = New System.IO.StreamWriter(System.IO.Path.Combine(appPath, reportFileName), False, enc)

      Dim tmpDate As DateTime
      Dim tmpProcTime As String = ComGetProcTime()
      sw.Write(CSV_SPACE & "出力帳票名：" & stReport.wPrnTitle & "登録リスト" & CSV_SPACE & CSV_COMMA)
      sw.Write(CSV_SPACE & "出力日：" & Date.Parse(tmpProcTime).ToString("yyyy/MM/dd") & CSV_SPACE & CSV_COMMA)

      '改行
      sw.Write(vbCrLf)

      Select Case stReport.wSyoriNo
        Case Form_ReportPrnRange.typMasterList.TBL_CUTSR
          ' 仕入先マスター
          sw.Write(CSV_SPACE & "仕入先コード" & CSV_SPACE & CSV_COMMA)
          sw.Write(CSV_SPACE & "ラベル用仕入先名" & CSV_SPACE & CSV_COMMA)
          sw.Write(CSV_SPACE & "仕入先名" & CSV_SPACE & CSV_COMMA)
          sw.Write(CSV_SPACE & "電話番号" & CSV_SPACE & CSV_COMMA)
          sw.Write(CSV_SPACE & "ＦＡＸ番号" & CSV_SPACE & CSV_COMMA)
          sw.Write(CSV_SPACE & "住所" & CSV_SPACE & CSV_COMMA)
          sw.Write(CSV_SPACE & "郵便番号" & CSV_SPACE & CSV_COMMA)
          sw.Write(CSV_SPACE & "担当者名" & CSV_SPACE & CSV_COMMA)

        Case Form_ReportPrnRange.typMasterList.TBL_TOKUISAKI
          ' 得意先マスター
          sw.Write(CSV_SPACE & "得意先コード" & CSV_SPACE & CSV_COMMA)          '01
          sw.Write(CSV_SPACE & "ラベル用得意先名" & CSV_SPACE & CSV_COMMA)      '02
          sw.Write(CSV_SPACE & "印字FLG" & CSV_SPACE & CSV_COMMA)               '03
          sw.Write(CSV_SPACE & "賞味日数" & CSV_SPACE & CSV_COMMA)              '04
          sw.Write(CSV_SPACE & "帳票印字FLG" & CSV_SPACE & CSV_COMMA)           '05
          sw.Write(CSV_SPACE & "加工日印字FLG" & CSV_SPACE & CSV_COMMA)         '06
          sw.Write(CSV_SPACE & "コメントＣ" & CSV_SPACE & CSV_COMMA)            '07   
          sw.Write(CSV_SPACE & "製造元CD" & CSV_SPACE & CSV_COMMA)              '08 
          sw.Write(CSV_SPACE & "グループコード名称" & CSV_SPACE & CSV_COMMA)    '09 
          sw.Write(CSV_SPACE & "得意先名" & CSV_SPACE & CSV_COMMA)              '10    
          sw.Write(CSV_SPACE & "電話番号" & CSV_SPACE & CSV_COMMA)              '11
          sw.Write(CSV_SPACE & "ＦＡＸ番号" & CSV_SPACE & CSV_COMMA)            '12
          sw.Write(CSV_SPACE & "住所" & CSV_SPACE & CSV_COMMA)                  '13
          sw.Write(CSV_SPACE & "郵便番号" & CSV_SPACE & CSV_COMMA)              '14
          sw.Write(CSV_SPACE & "担当者名" & CSV_SPACE & CSV_COMMA)              '15
          sw.Write(CSV_SPACE & "請求締日１" & CSV_SPACE & CSV_COMMA)            '16 
          sw.Write(CSV_SPACE & "請求締日２" & CSV_SPACE & CSV_COMMA)            '17
          sw.Write(CSV_SPACE & "請求締日３" & CSV_SPACE & CSV_COMMA)            '18 
          sw.Write(CSV_SPACE & "回収日１" & CSV_SPACE & CSV_COMMA)              '19  
          sw.Write(CSV_SPACE & "回収日２" & CSV_SPACE & CSV_COMMA)              '20
          sw.Write(CSV_SPACE & "回収日３" & CSV_SPACE & CSV_COMMA)              '21  
          sw.Write(CSV_SPACE & "税通知" & CSV_SPACE & CSV_COMMA)                '22
          sw.Write(CSV_SPACE & "税端数処理" & CSV_SPACE & CSV_COMMA)            '23
          sw.Write(CSV_SPACE & "繰越残高" & CSV_SPACE & CSV_COMMA)              '24  
          sw.Write(CSV_SPACE & "繰越日" & CSV_SPACE & CSV_COMMA)                '25 
          sw.Write(CSV_SPACE & "得意先区分" & CSV_SPACE & CSV_COMMA)            '26 
          sw.Write(CSV_SPACE & "請求先コード" & CSV_SPACE & CSV_COMMA)          '27

        Case Form_ReportPrnRange.typMasterList.TBL_TENPO
          ' 店舗名マスター
          sw.Write(CSV_SPACE & "請求先コード" & CSV_SPACE & CSV_COMMA)    　    '01
          sw.Write(CSV_SPACE & "店舗コード" & CSV_SPACE & CSV_COMMA)      　    '02
          sw.Write(CSV_SPACE & "店舗名" & CSV_SPACE & CSV_COMMA)                '03
          sw.Write(CSV_SPACE & "電話番号" & CSV_SPACE & CSV_COMMA)              '04
          sw.Write(CSV_SPACE & "ＦＡＸ番号" & CSV_SPACE & CSV_COMMA)            '05
          sw.Write(CSV_SPACE & "住所" & CSV_SPACE & CSV_COMMA)                  '06
          sw.Write(CSV_SPACE & "郵便番号" & CSV_SPACE & CSV_COMMA)              '07
          sw.Write(CSV_SPACE & "担当者名" & CSV_SPACE & CSV_COMMA)              '08

        Case Form_ReportPrnRange.typMasterList.TBL_KIKA
          ' 規格マスター
          sw.Write(CSV_SPACE & "畜種コード" & CSV_SPACE & CSV_COMMA)            '01
          sw.Write(CSV_SPACE & "規格コード" & CSV_SPACE & CSV_COMMA)      　    '02
          sw.Write(CSV_SPACE & "ラベル用規格名" & CSV_SPACE & CSV_COMMA)        '03

        Case Form_ReportPrnRange.typMasterList.TBL_KAKU
          ' 格付マスター
          sw.Write(CSV_SPACE & "格付コード" & CSV_SPACE & CSV_COMMA)     　  　 '01
          sw.Write(CSV_SPACE & "ラベル用格付名" & CSV_SPACE & CSV_COMMA)  　　  '02

        Case Form_ReportPrnRange.typMasterList.TBL_BUIM
          ' 部位マスター
          sw.Write(CSV_SPACE & "部位コード" & CSV_SPACE & CSV_COMMA)     　  　 '01
          sw.Write(CSV_SPACE & "ラベル用部位名" & CSV_SPACE & CSV_COMMA)  　　  '02
          sw.Write(CSV_SPACE & "牛豚フラグ" & CSV_SPACE & CSV_COMMA)  　　      '03
          sw.Write(CSV_SPACE & "風袋重量" & CSV_SPACE & CSV_COMMA)  　　　　　  '04
          sw.Write(CSV_SPACE & "入本数" & CSV_SPACE & CSV_COMMA)  　　　　　 　 '05
          sw.Write(CSV_SPACE & "賞味日数" & CSV_SPACE & CSV_COMMA)  　　　　　  '06
          sw.Write(CSV_SPACE & "品質ＦＬＧ" & CSV_SPACE & CSV_COMMA)  　　　　　'07
          sw.Write(CSV_SPACE & "加工日ＦＬＧ" & CSV_SPACE & CSV_COMMA)  　　　　'08
          sw.Write(CSV_SPACE & "コメントコード" & CSV_SPACE & CSV_COMMA)  　　　'09
          sw.Write(CSV_SPACE & "商品コード" & CSV_SPACE & CSV_COMMA)  　　　　　'10
          sw.Write(CSV_SPACE & "定重量" & CSV_SPACE & CSV_COMMA)       　 　　　'11
          sw.Write(CSV_SPACE & "ブロックコード" & CSV_SPACE & CSV_COMMA)  　　　'12
          sw.Write(CSV_SPACE & "Ｋｇ単価" & CSV_SPACE & CSV_COMMA)  　　　　　  '13
          sw.Write(CSV_SPACE & "上限値" & CSV_SPACE & CSV_COMMA)  　　　　 　　 '14
          sw.Write(CSV_SPACE & "下限値" & CSV_SPACE & CSV_COMMA)  　　　  　　  '15
          sw.Write(CSV_SPACE & "ラベル用部位名２" & CSV_SPACE & CSV_COMMA)  　　'16
          sw.Write(CSV_SPACE & "歩留区分" & CSV_SPACE & CSV_COMMA)  　　　 　 　'17

        Case Form_ReportPrnRange.typMasterList.TBL_GENSN
          ' 原産地マスター
          sw.Write(CSV_SPACE & "原産地コード" & CSV_SPACE & CSV_COMMA)     　   '01
          sw.Write(CSV_SPACE & "ラベル用原産地名" & CSV_SPACE & CSV_COMMA)      '02

        Case Form_ReportPrnRange.typMasterList.TBL_SHUB
          ' 種別マスター
          sw.Write(CSV_SPACE & "種別コード" & CSV_SPACE & CSV_COMMA)      　    '01
          sw.Write(CSV_SPACE & "ラベル用種別名" & CSV_SPACE & CSV_COMMA)        '02

        Case Form_ReportPrnRange.typMasterList.TBL_TOJM
          ' 屠場マスター
          sw.Write(CSV_SPACE & "屠場コード" & CSV_SPACE & CSV_COMMA)      　    '01
          sw.Write(CSV_SPACE & "ラベル用屠場名" & CSV_SPACE & CSV_COMMA)        '02
          sw.Write(CSV_SPACE & "屠場住所" & CSV_SPACE & CSV_COMMA)              '03

        Case Form_ReportPrnRange.typMasterList.TBL_TANTO
          ' 担当者マスター
          sw.Write(CSV_SPACE & "コード" & CSV_SPACE & CSV_COMMA)                '01
          sw.Write(CSV_SPACE & "担当者名" & CSV_SPACE & CSV_COMMA)              '02

        Case Form_ReportPrnRange.typMasterList.TBL_BLOCK
          ' ブロックコードマスター
          sw.Write(CSV_SPACE & "コード" & CSV_SPACE & CSV_COMMA)                '01
          sw.Write(CSV_SPACE & "名称" & CSV_SPACE & CSV_COMMA)                  '02

        Case Form_ReportPrnRange.typMasterList.TBL_GBFLG
          ' 牛豚ＦＬＧマスター
          sw.Write(CSV_SPACE & "コード" & CSV_SPACE & CSV_COMMA)                '01
          sw.Write(CSV_SPACE & "名称" & CSV_SPACE & CSV_COMMA)                  '02

        Case Form_ReportPrnRange.typMasterList.TBL_COMNT
          ' コメントマスター
          sw.Write(CSV_SPACE & "コメントコード" & CSV_SPACE & CSV_COMMA)        '01
          sw.Write(CSV_SPACE & "ラベル用コメント名" & CSV_SPACE & CSV_COMMA)    '02

        Case Form_ReportPrnRange.typMasterList.TBL_SEIZOU
          ' 製造元マスター
          sw.Write(CSV_SPACE & "コード" & CSV_SPACE & CSV_COMMA)                '01
          sw.Write(CSV_SPACE & "名称" & CSV_SPACE & CSV_COMMA)                  '02

        Case Form_ReportPrnRange.typMasterList.TBL_SUM
          ' テーブル詳細マスター
          sw.Write(CSV_SPACE & "データベース名" & CSV_SPACE & CSV_COMMA)     　 '01
          sw.Write(CSV_SPACE & "テーブル名" & CSV_SPACE & CSV_COMMA)  　　      '02
          sw.Write(CSV_SPACE & "№" & CSV_SPACE & CSV_COMMA)     　  　         '03
          sw.Write(CSV_SPACE & "フィールド名" & CSV_SPACE & CSV_COMMA)  　　    '04
          sw.Write(CSV_SPACE & "フィールド内容" & CSV_SPACE & CSV_COMMA)  　　  '05
          sw.Write(CSV_SPACE & "タイプ" & CSV_SPACE & CSV_COMMA)  　　　　　    '06
          sw.Write(CSV_SPACE & "長さ" & CSV_SPACE & CSV_COMMA)  　　　　　 　   '07
          sw.Write(CSV_SPACE & "注釈" & CSV_SPACE & CSV_COMMA)  　　　　　      '08
          sw.Write(CSV_SPACE & "送信番号" & CSV_SPACE & CSV_COMMA)  　　　　　  '09
          sw.Write(CSV_SPACE & "キー区分" & CSV_SPACE & CSV_COMMA)  　　　　    '10
          sw.Write(CSV_SPACE & "表示Ｇ" & CSV_SPACE & CSV_COMMA)  　　　        '11
          sw.Write(CSV_SPACE & "表示順" & CSV_SPACE & CSV_COMMA)  　　　　　    '12
          sw.Write(CSV_SPACE & "最小値" & CSV_SPACE & CSV_COMMA)       　 　　　'13
          sw.Write(CSV_SPACE & "印刷Ｇ" & CSV_SPACE & CSV_COMMA)  　　　        '14
          sw.Write(CSV_SPACE & "印刷順" & CSV_SPACE & CSV_COMMA)  　　　　　    '15
          sw.Write(CSV_SPACE & "リンクＭＤＢ" & CSV_SPACE & CSV_COMMA)  　　　　'16
          sw.Write(CSV_SPACE & "リンクテーブル" & CSV_SPACE & CSV_COMMA)  　　　'17
          sw.Write(CSV_SPACE & "リンクフィールド" & CSV_SPACE & CSV_COMMA)  　　'18
          sw.Write(CSV_SPACE & "リンクタイプ" & CSV_SPACE & CSV_COMMA)          '19

        Case Form_ReportPrnRange.typMasterList.TBL_SHOHIN
          ' テーブル商品マスター
          sw.Write(CSV_SPACE & "牛豚ＦＬＧ" & CSV_SPACE & CSV_COMMA)         　 '01
          sw.Write(CSV_SPACE & "名称" & CSV_SPACE & CSV_COMMA)     　           '02
          sw.Write(CSV_SPACE & "商品コード" & CSV_SPACE & CSV_COMMA)     　     '03
          sw.Write(CSV_SPACE & "商品名" & CSV_SPACE & CSV_COMMA)     　         '04
          sw.Write(CSV_SPACE & "セット区分" & CSV_SPACE & CSV_COMMA)     　     '05
          sw.Write(CSV_SPACE & "セット単品区分" & CSV_SPACE & CSV_COMMA)        '06

        Case Form_ReportPrnRange.typMasterList.TBL_SHENKAN
          ' 商品変換マスター
          sw.Write(CSV_SPACE & "得意先コード" & CSV_SPACE & CSV_COMMA)          '01
          sw.Write(CSV_SPACE & "ラベル用得意先名" & CSV_SPACE & CSV_COMMA)      '02
          sw.Write(CSV_SPACE & "セット区分" & CSV_SPACE & CSV_COMMA)      　    '03
          sw.Write(CSV_SPACE & "商品コード" & CSV_SPACE & CSV_COMMA)      　    '04
          sw.Write(CSV_SPACE & "ラベル用部位名" & CSV_SPACE & CSV_COMMA)      　'05
          sw.Write(CSV_SPACE & "変換コード" & CSV_SPACE & CSV_COMMA)      　    '06
          sw.Write(CSV_SPACE & "商品名" & CSV_SPACE & CSV_COMMA)                '07
          sw.Write(CSV_SPACE & "税区分" & CSV_SPACE & CSV_COMMA)                '08
          sw.Write(CSV_SPACE & "税端数" & CSV_SPACE & CSV_COMMA)                '09
          sw.Write(CSV_SPACE & "入り数" & CSV_SPACE & CSV_COMMA)              　'10
          sw.Write(CSV_SPACE & "入り数単位" & CSV_SPACE & CSV_COMMA)            '11
          sw.Write(CSV_SPACE & "桁" & CSV_SPACE & CSV_COMMA)                    '12
          sw.Write(CSV_SPACE & "捌き代" & CSV_SPACE & CSV_COMMA)                '13
          sw.Write(CSV_SPACE & "捌きコード" & CSV_SPACE & CSV_COMMA)            '14
          sw.Write(CSV_SPACE & "枝区分" & CSV_SPACE & CSV_COMMA)                '15

        Case Form_ReportPrnRange.typMasterList.TBL_THENKAN
          ' 得意先変換マスター
          sw.Write(CSV_SPACE & "得意先コード" & CSV_SPACE & CSV_COMMA)          '01
          sw.Write(CSV_SPACE & "ラベル用得意先名" & CSV_SPACE & CSV_COMMA)      '02
          sw.Write(CSV_SPACE & "変換コード" & CSV_SPACE & CSV_COMMA)      　    '03
          sw.Write(CSV_SPACE & "担当差コード" & CSV_SPACE & CSV_COMMA)          '04
          sw.Write(CSV_SPACE & "担当者名" & CSV_SPACE & CSV_COMMA)    　　　    '05
          sw.Write(CSV_SPACE & "税区分" & CSV_SPACE & CSV_COMMA)                '06
          sw.Write(CSV_SPACE & "税率" & CSV_SPACE & CSV_COMMA)                  '07
          sw.Write(CSV_SPACE & "税計算" & CSV_SPACE & CSV_COMMA)                '08
          sw.Write(CSV_SPACE & "伝票最大行" & CSV_SPACE & CSV_COMMA)            '09
          sw.Write(CSV_SPACE & "税端数" & CSV_SPACE & CSV_COMMA)                '10
          sw.Write(CSV_SPACE & "回収種別" & CSV_SPACE & CSV_COMMA)   　         '11

        Case Form_ReportPrnRange.typMasterList.TBL_SIHENKA
          ' 仕入先変換マスター
          sw.Write(CSV_SPACE & "仕入先コード" & CSV_SPACE & CSV_COMMA)          '01
          sw.Write(CSV_SPACE & "ラベル用仕入先名" & CSV_SPACE & CSV_COMMA)      '02
          sw.Write(CSV_SPACE & "変換コード" & CSV_SPACE & CSV_COMMA)      　    '03
          sw.Write(CSV_SPACE & "税区分" & CSV_SPACE & CSV_COMMA)                '04
          sw.Write(CSV_SPACE & "税率" & CSV_SPACE & CSV_COMMA)                  '05
          sw.Write(CSV_SPACE & "税計算" & CSV_SPACE & CSV_COMMA)                '05
          sw.Write(CSV_SPACE & "税端数" & CSV_SPACE & CSV_COMMA)                '05

      End Select

      ' 更新日付と登録日付を出力する
      If ((Form_ReportPrnRange.typMasterList.TBL_CUTSR <= stReport.wSyoriNo) And
          (stReport.wSyoriNo <= Form_ReportPrnRange.typMasterList.TBL_TANTO)) Then
        ' 登録日付
        sw.Write(CSV_SPACE & "登録日付" & CSV_SPACE & CSV_COMMA)
        ' 更新日付
        sw.Write(CSV_SPACE & "更新日付" & CSV_SPACE & CSV_COMMA)
      End If

      Select Case stReport.wSyoriNo
        Case Form_ReportPrnRange.typMasterList.TBL_CUTSR
          ' 仕入先マスター
          sw.Write(CSV_SPACE & "送信ＦＬＧ" & CSV_SPACE & CSV_COMMA)
          sw.Write(CSV_SPACE & "企業コード" & CSV_SPACE & CSV_COMMA)
        Case Form_ReportPrnRange.typMasterList.TBL_TOKUISAKI
          ' 得意先マスター
          sw.Write(CSV_SPACE & "送信ＦＬＧ" & CSV_SPACE & CSV_COMMA)
        Case Form_ReportPrnRange.typMasterList.TBL_KIKA
          ' 規格マスター
          sw.Write(CSV_SPACE & "送信ＦＬＧ" & CSV_SPACE & CSV_COMMA)
        Case Form_ReportPrnRange.typMasterList.TBL_KAKU
          ' 格付マスター
          sw.Write(CSV_SPACE & "送信ＦＬＧ" & CSV_SPACE & CSV_COMMA)
        Case Form_ReportPrnRange.typMasterList.TBL_BUIM
          ' 部位マスター
          sw.Write(CSV_SPACE & "送信ＦＬＧ" & CSV_SPACE & CSV_COMMA)
        Case Form_ReportPrnRange.typMasterList.TBL_GENSN
          ' 原産地マスター
          sw.Write(CSV_SPACE & "送信ＦＬＧ" & CSV_SPACE & CSV_COMMA)
        Case Form_ReportPrnRange.typMasterList.TBL_SHUB
          ' 種別マスター
          sw.Write(CSV_SPACE & "送信ＦＬＧ" & CSV_SPACE & CSV_COMMA)
        Case Form_ReportPrnRange.typMasterList.TBL_TOJM
          ' 屠場マスター
          sw.Write(CSV_SPACE & "送信ＦＬＧ" & CSV_SPACE & CSV_COMMA)

      End Select

      '改行
      sw.Write(vbCrLf)

      For Each dtRow As DataRow In tmpDtRow

        Select Case stReport.wSyoriNo
          Case Form_ReportPrnRange.typMasterList.TBL_CUTSR
            ' 仕入先マスター
            sw.Write(dtRow("SRCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("LSRNAME").ToString & CSV_COMMA)                   '02
            sw.Write(dtRow("SRNAME").ToString & CSV_COMMA)                    '03
            sw.Write(dtRow("TELA").ToString & CSV_COMMA)                      '04
            sw.Write(dtRow("TELF").ToString & CSV_COMMA)                      '05
            sw.Write(dtRow("JYUSYO").ToString & CSV_COMMA)                    '06
            sw.Write(dtRow("YUBIN").ToString & CSV_COMMA)                     '07
            sw.Write(dtRow("TANTO").ToString & CSV_COMMA)                     '08

          Case Form_ReportPrnRange.typMasterList.TBL_TOKUISAKI
            ' 得意先マスター
            sw.Write(dtRow("TKCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("LTKNAME").ToString & CSV_COMMA)                   '02
            sw.Write(dtRow("LINJI").ToString & CSV_COMMA)                     '03
            sw.Write(dtRow("LNISSU").ToString & CSV_COMMA)                    '04
            sw.Write(dtRow("LKIGEN").ToString & CSV_COMMA)                    '05            
            sw.Write(dtRow("LKAKOU").ToString & CSV_COMMA)                    '06
            sw.Write(dtRow("LCOMMENT").ToString & CSV_COMMA)                  '07
            sw.Write(dtRow("LSEIZOU").ToString & CSV_COMMA)                   '08
            sw.Write(dtRow("GROUPCOD").ToString & CSV_COMMA)                  '09
            sw.Write(dtRow("TNAME").ToString & CSV_COMMA)                     '10
            sw.Write(dtRow("TELA").ToString & CSV_COMMA)                      '11
            sw.Write(dtRow("TELF").ToString & CSV_COMMA)                      '12
            sw.Write(dtRow("JYUSYO").ToString & CSV_COMMA)                    '13    
            sw.Write(dtRow("YUBIN").ToString & CSV_COMMA)                     '14
            sw.Write(dtRow("TANTO").ToString & CSV_COMMA)                     '15
            sw.Write(dtRow("SSHIMEBI1").ToString & CSV_COMMA)                 '16
            sw.Write(dtRow("SSHIMEBI2").ToString & CSV_COMMA)                 '17
            sw.Write(dtRow("SSHIMEBI3").ToString & CSV_COMMA)                 '18
            sw.Write(dtRow("KAISYUBI1").ToString & CSV_COMMA)                 '19
            sw.Write(dtRow("KAISYUBI2").ToString & CSV_COMMA)                 '20
            sw.Write(dtRow("KAISYUBI3").ToString & CSV_COMMA)                 '21  
            sw.Write(dtRow("ZEITSUCHI").ToString & CSV_COMMA)                 '22
            sw.Write(dtRow("ZEIHASUU").ToString & CSV_COMMA)                  '23
            sw.Write(dtRow("KURIKOSHIZAN").ToString & CSV_COMMA)              '24
            If (String.IsNullOrEmpty(dtRow("KURIKOSHIBI").ToString)) Then
              sw.Write("" & CSV_COMMA)                                        '25
            Else
              tmpDate = DTConvDateTime(dtRow("KURIKOSHIBI"))
              sw.Write(tmpDate.ToString("yyyy/MM/dd HH:mm:ss") & CSV_COMMA)   '25
            End If
            sw.Write(dtRow("TKUBUN").ToString & CSV_COMMA)                    '26
            sw.Write(dtRow("SSCODE").ToString & CSV_COMMA)                    '27

          Case Form_ReportPrnRange.typMasterList.TBL_TENPO
            ' 店舗名マスター
            sw.Write(dtRow("SSCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("TENPOCODE").ToString & CSV_COMMA)                 '02
            sw.Write(dtRow("TENNAME").ToString & CSV_COMMA)                   '03
            sw.Write(dtRow("TELA").ToString & CSV_COMMA)                      '04
            sw.Write(dtRow("TELF").ToString & CSV_COMMA)                      '05
            sw.Write(dtRow("JYUSYO").ToString & CSV_COMMA)                    '06    
            sw.Write(dtRow("YUBIN").ToString & CSV_COMMA)                     '07
            sw.Write(dtRow("TANTO").ToString & CSV_COMMA)                     '08

          Case Form_ReportPrnRange.typMasterList.TBL_KIKA
            ' 規格マスター
            sw.Write(dtRow("KSBCOD").ToString & CSV_COMMA)                    '03
            sw.Write(dtRow("KICODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("KKNAME").ToString & CSV_COMMA)                    '02

          Case Form_ReportPrnRange.typMasterList.TBL_KAKU
            ' 格付マスター
            sw.Write(dtRow("KKCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("KZNAME").ToString & CSV_COMMA)                    '02

          Case Form_ReportPrnRange.typMasterList.TBL_BUIM
            ' 部位マスター
            sw.Write(dtRow("BICODE").ToString & CSV_COMMA)                   '01
            sw.Write(dtRow("BINAME").ToString & CSV_COMMA)                   '02
            sw.Write(dtRow("GBFLG").ToString & CSV_COMMA)                    '03
            sw.Write(dtRow("FUTAIJ").ToString & CSV_COMMA)                   '04
            sw.Write(dtRow("HONSU").ToString & CSV_COMMA)                    '05
            sw.Write(dtRow("SHOMINISU").ToString & CSV_COMMA)                '06
            sw.Write(dtRow("KIGENFLG").ToString & CSV_COMMA)                 '07
            sw.Write(dtRow("KAKOUBIFLG").ToString & CSV_COMMA)               '08
            sw.Write(dtRow("COMMENTC").ToString & CSV_COMMA)                 '09
            sw.Write(dtRow("SHOHINC").ToString & CSV_COMMA)                  '10
            sw.Write(dtRow("TEIJYURYO").ToString & CSV_COMMA)                '11
            sw.Write(dtRow("BLOCKCODE").ToString & CSV_COMMA)                '12
            sw.Write(dtRow("KGTANKA").ToString & CSV_COMMA)                  '13
            sw.Write(dtRow("JYOGEN").ToString & CSV_COMMA)                   '14
            sw.Write(dtRow("KAGEN").ToString & CSV_COMMA)                    '15
            sw.Write(dtRow("BUIMEI2").ToString & CSV_COMMA)                  '16
            sw.Write(dtRow("BUDOMARI").ToString & CSV_COMMA)                 '17

          Case Form_ReportPrnRange.typMasterList.TBL_GENSN
            ' 原産地マスター
            sw.Write(dtRow("GNCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("GNNAME").ToString & CSV_COMMA)                    '02

          Case Form_ReportPrnRange.typMasterList.TBL_SHUB
            ' 種別マスター
            sw.Write(dtRow("SBCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("SBNAME").ToString & CSV_COMMA)                    '02

          Case Form_ReportPrnRange.typMasterList.TBL_TOJM
            ' 屠場マスター
            sw.Write(dtRow("TJCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("TJNAME").ToString & CSV_COMMA)                    '02
            sw.Write(dtRow("TJADRS").ToString & CSV_COMMA)                    '03

          Case Form_ReportPrnRange.typMasterList.TBL_TANTO
            ' 担当者マスター
            sw.Write(dtRow("TANTOC").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("TANTOMEI").ToString & CSV_COMMA)                  '02

          Case Form_ReportPrnRange.typMasterList.TBL_BLOCK
            ' ブロックコードマスター
            sw.Write(dtRow("BLOCKCODE").ToString & CSV_COMMA)                 '01
            sw.Write(dtRow("BLNAME").ToString & CSV_COMMA)                    '02

          Case Form_ReportPrnRange.typMasterList.TBL_GBFLG
            ' 牛豚ＦＬＧマスター
            sw.Write(dtRow("GBCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("GBNAME").ToString & CSV_COMMA)                    '02

          Case Form_ReportPrnRange.typMasterList.TBL_COMNT
            ' コメントマスター
            sw.Write(dtRow("CMCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("CMNAME").ToString & CSV_COMMA)                    '02


          Case Form_ReportPrnRange.typMasterList.TBL_SEIZOU
            ' 製造元マスター
            sw.Write(dtRow("SZCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("SZNAME").ToString & CSV_COMMA)                    '02

          Case Form_ReportPrnRange.typMasterList.TBL_SUM
            ' テーブル詳細マスター
            sw.Write(dtRow("DbNAME").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("TBLNAME").ToString & CSV_COMMA)                   '02
            sw.Write(dtRow("FildNo").ToString & CSV_COMMA)                    '03
            sw.Write(dtRow("Fild_Name").ToString & CSV_COMMA)                 '04
            sw.Write(dtRow("Fild_Caption").ToString & CSV_COMMA)              '05
            sw.Write(dtRow("Fild_Type").ToString & CSV_COMMA)                 '06
            sw.Write(dtRow("Fild_Leng").ToString & CSV_COMMA)                 '07
            sw.Write(dtRow("Fild_Comment").ToString & CSV_COMMA)              '08
            sw.Write(dtRow("Fild_Send_No").ToString & CSV_COMMA)              '09
            sw.Write(dtRow("Fild_Key").ToString & CSV_COMMA)                  '10
            sw.Write(dtRow("Fild_DspGrp").ToString & CSV_COMMA)               '11
            sw.Write(dtRow("Fild_Dsp").ToString & CSV_COMMA)                  '12
            sw.Write(dtRow("Fild_Check").ToString & CSV_COMMA)                '13
            sw.Write(dtRow("Fild_PrtGrp").ToString & CSV_COMMA)               '14
            sw.Write(dtRow("Fild_Prt").ToString & CSV_COMMA)                  '15
            sw.Write(dtRow("LMDB").ToString & CSV_COMMA)                      '16
            sw.Write(dtRow("LTBL").ToString & CSV_COMMA)                      '17
            sw.Write(dtRow("LFILD").ToString & CSV_COMMA)                     '18
            sw.Write(dtRow("LTYPE").ToString & CSV_COMMA)                     '19

          Case Form_ReportPrnRange.typMasterList.TBL_SHOHIN
            ' テーブル商品マスター
            sw.Write(dtRow("GBFLG").ToString & CSV_COMMA)                     '01
            sw.Write(dtRow("GBNAME").ToString & CSV_COMMA)                    '02
            sw.Write(dtRow("SHCODE").ToString & CSV_COMMA)                    '03
            sw.Write(dtRow("HINMEI").ToString & CSV_COMMA)                    '04
            sw.Write(dtRow("SETKBN").ToString & CSV_COMMA)                    '05
            sw.Write(dtRow("PSETFLG").ToString & CSV_COMMA)                   '06

          Case Form_ReportPrnRange.typMasterList.TBL_SHENKAN
            ' 商品変換マスター
            sw.Write(dtRow("TKCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("LTKNAME").ToString & CSV_COMMA)                   '02
            sw.Write(dtRow("SETKBN").ToString & CSV_COMMA)                    '03
            sw.Write(dtRow("SCODE").ToString & CSV_COMMA)  　                 '04
            sw.Write(dtRow("BINAME").ToString & CSV_COMMA)      　            '05
            sw.Write(dtRow("HENKAN").ToString & CSV_COMMA)                    '06
            sw.Write(dtRow("HINMEI").ToString & CSV_COMMA)                    '07
            sw.Write(dtRow("ZEIKUBUN").ToString & CSV_COMMA)                  '08
            sw.Write(dtRow("HASUU").ToString & CSV_COMMA)                     '09
            sw.Write(dtRow("IRISU").ToString & CSV_COMMA)                     '10
            sw.Write(dtRow("TANNI").ToString & CSV_COMMA)                     '11
            sw.Write(dtRow("KETA").ToString & CSV_COMMA)                      '12
            sw.Write(dtRow("SABAKi").ToString & CSV_COMMA)                    '13
            sw.Write(dtRow("SUB_CODE").ToString & CSV_COMMA)                  '14
            sw.Write(dtRow("EDAKBN").ToString & CSV_COMMA)                    '15

          Case Form_ReportPrnRange.typMasterList.TBL_THENKAN
            ' 得意先変換マスター
            sw.Write(dtRow("TKCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("LTKNAME").ToString & CSV_COMMA)                   '02
            sw.Write(dtRow("HENKAN").ToString & CSV_COMMA)                    '03
            sw.Write(dtRow("TANTOUC").ToString & CSV_COMMA)  　               '04
            sw.Write(dtRow("TANTOMEI").ToString & CSV_COMMA)      　          '05
            sw.Write(dtRow("ZEIKUBUN").ToString & CSV_COMMA)                  '06
            sw.Write(dtRow("ZEIRITU").ToString & CSV_COMMA)                   '07
            sw.Write(dtRow("ZEIPRT").ToString & CSV_COMMA)                    '08
            sw.Write(dtRow("GYONO").ToString & CSV_COMMA)                     '09
            sw.Write(dtRow("HASUU").ToString & CSV_COMMA)                     '10
            sw.Write(dtRow("KAISYU").ToString & CSV_COMMA)                    '11

          Case Form_ReportPrnRange.typMasterList.TBL_SIHENKA
            ' 仕入先変換マスター
            sw.Write(dtRow("SRCODE").ToString & CSV_COMMA)                    '01
            sw.Write(dtRow("LSRNAME").ToString & CSV_COMMA)                   '02
            sw.Write(dtRow("HENKAN").ToString & CSV_COMMA)                    '03
            sw.Write(dtRow("ZEIKUBUN").ToString & CSV_COMMA)                  '04
            sw.Write(dtRow("ZEIRITU").ToString & CSV_COMMA)                   '05
            sw.Write(dtRow("ZEIPRT").ToString & CSV_COMMA)                    '06
            sw.Write(dtRow("HASUU").ToString & CSV_COMMA)                     '07

        End Select

        ' 更新日付と登録日付を出力する
        If ((Form_ReportPrnRange.typMasterList.TBL_CUTSR <= stReport.wSyoriNo) And
            (stReport.wSyoriNo <= Form_ReportPrnRange.typMasterList.TBL_TANTO)) Then
          ' 登録日付
          If (String.IsNullOrEmpty(dtRow("TDATE").ToString)) Then
            sw.Write("" & CSV_COMMA)
          Else
            tmpDate = DTConvDateTime(dtRow("TDATE"))
            sw.Write(tmpDate.ToString("yyyy/MM/dd HH:mm:ss") & CSV_COMMA)
          End If
          ' 更新日付
          If (String.IsNullOrEmpty(dtRow("KDATE").ToString)) Then
            sw.Write("" & CSV_COMMA)
          Else
            tmpDate = DTConvDateTime(dtRow("KDATE"))
            sw.Write(tmpDate.ToString("yyyy/MM/dd HH:mm:ss") & CSV_COMMA)
          End If
        End If

        Select Case stReport.wSyoriNo
          Case Form_ReportPrnRange.typMasterList.TBL_CUTSR
            ' 仕入先マスター
            sw.Write(dtRow("SFLG").ToString & CSV_COMMA)                    '09
            sw.Write(dtRow("KIGYOCD").ToString & CSV_COMMA)                 '10
          Case Form_ReportPrnRange.typMasterList.TBL_TOKUISAKI
            ' 得意先マスター
            sw.Write(dtRow("SFLG").ToString & CSV_COMMA)                    '28
          Case Form_ReportPrnRange.typMasterList.TBL_KIKA
            ' 規格マスター
            sw.Write(dtRow("SFLG").ToString & CSV_COMMA)                    '05
          Case Form_ReportPrnRange.typMasterList.TBL_KAKU
            ' 格付マスター
            sw.Write(dtRow("SFLG").ToString & CSV_COMMA)                    '05
          Case Form_ReportPrnRange.typMasterList.TBL_BUIM
            ' 部位マスター
            sw.Write(dtRow("SFLG").ToString & CSV_COMMA)                    '18
          Case Form_ReportPrnRange.typMasterList.TBL_GENSN
            ' 原産地マスター
            sw.Write(dtRow("SFLG").ToString & CSV_COMMA)                    '03
          Case Form_ReportPrnRange.typMasterList.TBL_SHUB
            ' 種別マスター
            sw.Write(dtRow("SFLG").ToString & CSV_COMMA)                    '03
          Case Form_ReportPrnRange.typMasterList.TBL_TOJM
            ' 屠場マスター
            sw.Write(dtRow("SFLG").ToString & CSV_COMMA)                    '04
        End Select

        '改行
        sw.Write(vbCrLf)

      Next

      ' レポートデータをEXCELで開く
      Dim officeFileProc As New Process
      With officeFileProc
        .StartInfo.FileName = System.IO.Path.Combine(appPath, reportFileName)
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

#End Region

#End Region

#Region "イベントプロシージャ"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>to
  ''' <param name="e"></param>
  Private Sub Form_TanaPrn_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    ' アプリケーション名設定
    Me.Text = "マスター登録リスト範囲選択(" & aplTitle & ")"

    ' 最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_ReportPrn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Dim tmpTargetBtn As Button = Nothing

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        '印刷ボタン押下処理
        tmpTargetBtn = Me.Cmd_Buton01
      ' F10キー押下時
      Case Keys.F10
        e.Handled = True
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        tmpTargetBtn = Me.Cmd_Buton12
    End Select

    If tmpTargetBtn IsNot Nothing Then
      With tmpTargetBtn
        .Focus()
        .PerformClick()
      End With
    End If

  End Sub

  ''' <summary>
  ''' 印刷ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Cmd_Buton01_Click(sender As Object, e As EventArgs) Handles Cmd_Buton01.Click

    confirm = True

    MyBase.SfResult = typSfResult.SF_OK

    ' 終了
    Me.Close()

  End Sub

  ''' <summary>
  ''' 終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_End_Click(sender As Object, e As EventArgs) Handles Cmd_Buton12.Click

    confirm = True

    MyBase.SfResult = typSfResult.SF_CANCEL

    ' 終了
    Me.Close()

  End Sub

  ''' <summary>
  ''' フォームを閉じる場合
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_ReportPrnRange_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    ' ＯＫボタン、キャンセルボタンを押していない場合
    If (confirm = False) Then
      MyBase.SfResult = typSfResult.SF_CLOSE
    End If
  End Sub

#End Region

#Region "SQL関連"

  ''' <summary>
  ''' 登録リスト取得SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function sqlReport(ByRef stReport As Form_ReportPrnRange.structReport) As String

    Dim sql As String = String.Empty

    sql &= "SELECT * FROM " & stReport.wTblName
    If (stReport.wLeftJoinChk) Then
      sql &= stReport.wLeftJoin
    End If
    If (stReport.wKubunChk) Then
      sql &= " WHERE KUBUN <> 9 "
    End If
    sql &= " ORDER BY " & stReport.wOrderPtn

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' 登録リスト検索用SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function sqlSerch(ByRef stReport As Form_ReportPrnRange.structReport) As String

    Dim sql As String = String.Empty

    sql &= "SELECT * FROM " & stReport.wSerchTblName
    sql &= " ORDER BY " & stReport.wSerchCodeId

    Console.WriteLine(sql)

    Return sql

  End Function

#End Region

End Class

