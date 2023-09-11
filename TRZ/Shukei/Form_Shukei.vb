Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.DgvForm02
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports T.R.ZCommonClass.clsDataGridSearchControl

Imports System

Public Class Form_Shukei

#Region "定数定義"

  Private Const PRG_ID As String = "Shukei"

  'ダブルクォーテーション
  Private Const CSV_SPACE As String = ControlChars.Quote

  ' カンマ
  Private Const CSV_COMMA As String = ","

  Public Enum typOption
    ''' <summary>
    ''' セット処理横計明細
    ''' </summary>
    No01 = 0
    ''' <summary>
    ''' パーツ処理横計明細
    ''' </summary>
    No02
    ''' <summary>
    ''' 得意先別集計表
    ''' </summary>
    No03
    ''' <summary>
    ''' 日別得意先別集計表
    ''' </summary>
    No04
    ''' <summary>
    ''' 枝別セット処理横計明細
    ''' </summary>
    No05
    ''' <summary>
    ''' 加工日報データ出力
    ''' </summary>
    No06
  End Enum

#End Region

#Region "メンバ"

  ''' <summary>
  ''' 集計ワークテーブル構造体
  ''' </summary>
  Public Structure structShukei

    Public Property wPageNo As Integer      ' ページ番号
    Public Property wKDATE As DateTime      ' 更新日付
    Public Property wSEQNO As Long          ' 連番
    Public Property wTKCODE As Long         ' 得意先コード
    Public Property wSHUBm As Long          ' 種別コード
    Public Property wEBCODE As Long         ' 枝番コード
    Public Property wGYO As Integer         ' 行番号
    Public Property wSHUB1 As Long          ' 種別コード
    Public Property wGENSAN1 As Long        ' 原産地コード
    Public Property wBICODE As Long         ' 部位コード
    Public Property wTNAME As String        ' 得意先名
    Public Property LMAEw As Long           ' 左重量
    Public Property RMAEw As Long           ' 右重量
    Public Property JyoujyouNo As Long      ' 上場番号
    Public Property wSHUB As String         ' 種別名
    Public Property wGENSN As String        ' 原産地名
    Public Property wBUIMEI As String       ' 歩留まり
    Public Property wBUDOMARI As Long

  End Structure

#Region "プライベート"
  ' 集計用ワークテーブル
  Private tmpSyukeiDT As New DataTable

#End Region

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_Shukei, AddressOf ComRedisplay)
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
    Dim stShukei As New structShukei

    ' 実行
    With tmpDb
      Try

        ' 得意先マスタの取得
        sql = SqlReadTokuisaki()
        Dim dtTOKUISAKI As New DataTable
        Call .GetResult(dtTOKUISAKI, sql)

        ' 肢番マスターの取得
        sql = SqlReadEdab()
        Dim dtEBAB As New DataTable
        Call .GetResult(dtEBAB, sql)

        ' 種別マスタの取得
        sql = SqlReadShub()
        Dim dtSHUB As New DataTable
        Call .GetResult(dtSHUB, sql)

        ' 原産地マスタの取得
        sql = SqlReadGensan()
        Dim dtGENSN As New DataTable
        Call .GetResult(dtGENSN, sql)

        ' 部位マスタの取得
        sql = SqlReadBuim()
        Dim dtBUI As New DataTable
        Call .GetResult(dtBUI, sql)

        ' 集計ワークテーブルの初期化
        tmpSyukeiDT.Clear()

        ' 帳票出力行数
        Dim MaxGyo As Integer
        ' セット処理表（0： 横、１：縦、２：縦・種別別）
        If (SHUKEI_TATEYOKO = 0) Then
          MaxGyo = 5
        Else
          MaxGyo = 3
        End If

        ' SQL文の作成
        sql = SqlJissekiOpen(syoriNo)
        Dim tmpDt As New DataTable
        Call .GetResult(tmpDt, sql)

        stShukei.wPageNo = 0
        stShukei.wSEQNO = 0

        Dim dtRow As DataRow
        Dim drTK As DataRow()
        Dim drEBAD As DataRow()
        Dim drSHUB As DataRow()
        Dim drBUI As DataRow()
        Dim drGENSN As DataRow()

        Dim tmpDtSub As New DataTable

        .TrnStart()

        'プログラスバー幅設定
        PrgBar.Width = Me.Width
        'プログラスバー最大値設定
        PrgBar.Maximum = 50
        'プログラスバー表示
        PrgBar.Visible = True
        PrgBar.Value = 0

        ' ファイルレコード → DataRow
        For i = 0 To tmpDt.Rows.Count - 1

          'プログラスバーの描画
          PrgBar.Value = PrgBar.Value + 1
          If PrgBar.Value >= PrgBar.Maximum Then
            PrgBar.Value = 1
          End If

          dtRow = tmpDt.Rows(i)

          ' 枝番号変更判定
          If syoriNo = typOption.No06 And DTConvLong(dtRow("EBCODE")) <> stShukei.wEBCODE Then
            stShukei.wTKCODE = 0
          End If

          ' 加工日変更判定
          If stShukei.wKDATE.CompareTo(DTConvDateTime(dtRow("KAKOUBI"))) <> 0 And syoriNo <> typOption.No05 Then
            stShukei.wTKCODE = 0
            stShukei.wKDATE = DTConvDateTime(dtRow("KAKOUBI"))
          End If

          ' セット処理表（２：縦・種別別）
          If SHUKEI_TATEYOKO = 2 Then
            If stShukei.wSHUBm <> (DTConvLong(dtRow("SHOHINC")) \ 10000) Then
              stShukei.wTKCODE = 0
            End If
          End If
          stShukei.wSHUBm = DTConvLong(dtRow("SHOHINC")) \ 10000

          '得意先コード変更判定
          If DTConvLong(dtRow("OLDTKC")) <> stShukei.wTKCODE Then
            stShukei.wPageNo = stShukei.wPageNo + 1
            stShukei.wGYO = 0
            stShukei.wEBCODE = 9999999
            stShukei.wTKCODE = DTConvLong(dtRow("OLDTKC"))

            ' 得意先マスタの検索
            drTK = dtTOKUISAKI.Select("TKCODE = " & dtRow("OLDTKC").ToString)
            If (1 = drTK.Count) Then
              ' 得意先名の取得
              stShukei.wTNAME = dtRow("OLDTKC").ToString & ":" & drTK(0)("LTKNAME").ToString
              If syoriNo = typOption.No06 Then
                stShukei.wTNAME = drTK(0)("LTKNAME").ToString
              End If
            Else
              stShukei.wTNAME = "未登録：" & dtRow("OLDTKC").ToString
            End If

            If Not (syoriNo = typOption.No01 Or syoriNo = typOption.No05 Or syoriNo = typOption.No06) Then
              stShukei.wSHUB1 = 0
              stShukei.wGENSAN1 = 0
              stShukei.wBICODE = 0
            End If
          End If

          '枝番号変更判定
          If DTConvLong(dtRow("EBCODE")) <> stShukei.wEBCODE Then
            stShukei.wEBCODE = DTConvLong(dtRow("EBCODE"))

            If syoriNo = typOption.No01 Or syoriNo = typOption.No05 Or syoriNo = typOption.No06 Then
              stShukei.wGYO = stShukei.wGYO + 1
              If stShukei.wEBCODE = 0 Then
                stShukei.LMAEw = 0
                stShukei.RMAEw = 0
                stShukei.JyoujyouNo = 0
              Else

                ' 肢番マスターから左重量、右重量を取得
                drEBAD = dtEBAB.Select("EBCODE = " & stShukei.wEBCODE.ToString &
                                       " AND SIIREBI <= #" & String.Format(dtRow("KAKOUBI").ToString, "yyyy/MM/dd") & "#")
                If (drEBAD.Count >= 1) Then

                  stShukei.LMAEw = DTConvLong(drEBAD(0)("JYURYO1"))
                  stShukei.RMAEw = DTConvLong(drEBAD(0)("JYURYO2"))
                  stShukei.JyoujyouNo = DTConvLong(drEBAD(0)("EDC"))
                  If stShukei.LMAEw = 0 And stShukei.RMAEw = 0 Then
                    stShukei.LMAEw = DTConvLong(drEBAD(0)("JYURYO")) \ 2
                    stShukei.RMAEw = DTConvLong(drEBAD(0)("JYURYO")) \ 2
                  End If
                Else
                  stShukei.LMAEw = 0
                  stShukei.RMAEw = 0
                  stShukei.JyoujyouNo = 0
                End If
              End If
            End If

            ' 種別マスタの検索
            drSHUB = dtSHUB.Select("SBCODE = " & stShukei.wSHUBm.ToString)
            If (1 = drSHUB.Count) Then
              ' 種別名の取得
              stShukei.wSHUB = drSHUB(0)("SBNAME").ToString
            Else
              stShukei.wSHUB = "未登録： " & stShukei.wSHUBm.ToString
            End If

            ' 原産地マスタの検索
            drGENSN = dtGENSN.Select("GNCODE = " & dtRow("GENSANCHIC").ToString)
            If (1 = drGENSN.Count) Then
              ' 原産地名の取得
              stShukei.wGENSN = drGENSN(0)("GNNAME").ToString
            Else
              stShukei.wGENSN = "未登録：" & dtRow("GENSANCHIC").ToString
            End If

          End If

          ' 帳票出力行数超過時、改ページ
          If stShukei.wGYO > MaxGyo Then
            ' 改ページ
            stShukei.wPageNo = stShukei.wPageNo + 1
            ' 行数初期化
            stShukei.wGYO = 1
          End If

          If syoriNo = typOption.No01 Or syoriNo = typOption.No05 Or syoriNo = typOption.No06 Then
            stShukei.wBICODE = 0
            stShukei.wSEQNO = stShukei.wBICODE
            stShukei.wBUIMEI = ""
            stShukei.wBUDOMARI = 0

            Dim sqlW1 As String = String.Empty

            sqlW1 = "PAGENO = '" & stShukei.wPageNo.ToString & "'"
            sqlW1 &= " AND TCODE = '" & dtRow("OLDTKC").ToString & "'"
            sqlW1 &= " AND BUICODE = '0' "

            ' 更新or追加処理判定
            WorkTb_Set_Rtn(stShukei, dtRow, sqlW1)

            If syoriNo = typOption.No06 And DTConvLong(dtRow("BUDOMARI")) = 0 Then
              WorkTb_Upd01(stShukei, dtRow, sqlW1)
            End If

          Else
            If SHUKEI_NARABI = 0 Then      '０４・０９・１２　修正
              If stShukei.wBICODE <> DTConvLong(dtRow("BICODE")) Or
                 stShukei.wSHUB1 <> stShukei.wSHUBm Or
                 stShukei.wGENSAN1 <> DTConvLong(dtRow("GENSANCHIC")) Then
                stShukei.wSEQNO = stShukei.wSEQNO + 1
                stShukei.wGYO = 0
              End If
            Else
              If stShukei.wBICODE <> DTConvLong(dtRow("SHOHINC")) Or
                 stShukei.wSHUB1 <> stShukei.wSHUBm Or
                 stShukei.wGENSAN1 <> DTConvLong(dtRow("GENSANCHIC")) Then
                stShukei.wSEQNO = stShukei.wSEQNO + 1
                stShukei.wGYO = 0
              End If
            End If
            stShukei.wGYO = stShukei.wGYO + 1
            If stShukei.wGYO > 2 Then
              stShukei.wGYO = 1
              stShukei.wSEQNO = stShukei.wSEQNO + 1
            End If
            stShukei.wSHUB1 = stShukei.wSHUBm
            stShukei.wGENSAN1 = DTConvLong(dtRow("GENSANCHIC"))
          End If
          '
          If SHUKEI_NARABI = 0 Then
            stShukei.wBICODE = DTConvLong(dtRow("BICODE"))
          Else
            stShukei.wBICODE = DTConvLong(dtRow("SHOHINC"))
          End If

          ' 部位マスタから取得
          drBUI = dtBUI.Select("BICODE = " & dtRow("BICODE").ToString)
          If (1 = drBUI.Count) Then
            ' 部位名の取得　
            stShukei.wBUIMEI = drBUI(0)("BINAME").ToString
            stShukei.wBUDOMARI = DTConvLong(drBUI(0)("BUDOMARI"))
          Else
            stShukei.wBUIMEI = "未登録：" & dtRow("BICODE").ToString
            stShukei.wBUDOMARI = DTConvLong(dtRow("BUDOMARI"))
          End If

          Dim sqlW2 As String = String.Empty
          If syoriNo = typOption.No01 Or syoriNo = typOption.No05 Or syoriNo = typOption.No06 Then
            sqlW2 = "PAGENO = '" & stShukei.wPageNo.ToString & "'"
            stShukei.wSEQNO = stShukei.wBICODE + stShukei.wSHUBm * 10000
          Else
            sqlW2 = "SEQNO = '" & stShukei.wSEQNO.ToString & "'"
          End If

          sqlW2 &= " AND TCODE = '" & dtRow("OLDTKC").ToString & "'"
          If SHUKEI_NARABI = 0 Then
            sqlW2 &= " AND BUICODE = '" & dtRow("BICODE").ToString & "'"
          Else
            sqlW2 &= " AND BUICODE = '" & dtRow("SHOHINC").ToString & "'"
          End If

          ' 更新or追加処理判定
          WorkTb_Set_Rtn(stShukei, dtRow, sqlW2)
          WorkTb_Upd02(syoriNo, dtRow, stShukei, sqlW2)

        Next i

        .TrnCommit()

        ' データ件数が０件の場合
        If (tmpDt.Rows.Count = 0) Then
          ComMessageBox("該当するデータが存在しません。",
                        "加工実績集計表", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)
          ret = False
        Else
          'ACCESSレポートへの出力
          WorkTbl_Output(syoriNo)
        End If

        .Dispose()

      Catch ex As Exception
        .TrnRollBack()
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
  Private Sub WorkTbl_Output(syoriNo As Integer)

    Dim rptDb As New clsReport(clsGlobalData.REPORT_FILENAME)

    With rptDb
      Try

        ' ACCESS集計テーブルの削除
        .Execute("DELETE FROM WK_SHUKEI")

        .TrnStart()

        ' 集計用ワークテーブルからの取得
        Dim sql As String = String.Empty
        For i = 0 To tmpSyukeiDT.Rows.Count - 1
          sql = SqlInsDataAccess(tmpSyukeiDT.Rows(i))
          .Execute(sql)
        Next

        .TrnCommit()

        If (syoriNo = typOption.No06) Then
          ' 加工日日報データ出力
          KAKOU_NIPPO(rptDb)
        End If

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
  ''' 加工日日報データの合計行
  ''' </summary>
  ''' <param name="sw"></param>
  ''' <param name="GYO"></param>
  ''' <param name="wLINE"></param>
  ''' <returns></returns>
  Private Function GOKEI_RTN(sw As System.IO.StreamWriter,
                             ByRef GYO As Integer,
                             ByRef wLINE As Integer) As Boolean

    Do
      sw.Write(CSV_COMMA)
      sw.Write(CSV_COMMA)
      sw.Write(CSV_COMMA)
      sw.Write(CSV_COMMA)
      sw.Write(CSV_COMMA)
      sw.Write(CSV_COMMA)
      sw.Write("=INT(C" & GYO.ToString & "*F" & GYO.ToString & ")")

      '改行
      sw.Write(vbCrLf)
      GYO = GYO + 1
      wLINE = wLINE + 1
    Loop While (wLINE < 15)

    sw.Write("豚" & CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(NIPPO_BSABAKI.ToString & CSV_COMMA)
    sw.Write("=INT(C" & GYO.ToString & "*F" & GYO.ToString & ")")

    '改行
    sw.Write(vbCrLf)
    GYO = GYO + 1
    wLINE = wLINE + 1

    sw.Write(CSV_COMMA)
    sw.Write("重量計" & CSV_COMMA)
    sw.Write("=SUM(C" & (GYO - wLINE).ToString & ":C" & (GYO - 1).ToString & ")")
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)

    '改行
    sw.Write(vbCrLf)
    GYO = GYO + 1
    wLINE = wLINE + 1

    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write("金額計" & CSV_COMMA)
    sw.Write("=SUM(G" & (GYO - wLINE).ToString & ":G" & (GYO - 1).ToString & ")")

    '改行
    sw.Write(vbCrLf)
    GYO = GYO + 1
    wLINE = wLINE + 1

    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)
    sw.Write(CSV_COMMA)

    '改行の書込
    sw.Write(vbCrLf)
    GYO = GYO + 1
    wLINE = wLINE + 1

    Return False

  End Function

  ''' <summary>
  ''' 加工日日報データ出力
  ''' </summary>
  ''' <param name="rptDb">レポート出力先</param>
  ''' <returns></returns>
  Private Function KAKOU_NIPPO(rptDb As clsReport) As Boolean

    Dim ret As Boolean = True
    Dim Sql As String
    'ファイルStreamWriter
    Dim sw As System.IO.StreamWriter = Nothing

    With rptDb
      ' 実行
      Try

        'CSVファイル書込に使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        '自分自身の実行ファイルのパスを取得する
        Dim appPath As String = System.Environment.CurrentDirectory

        '書き込むファイルを開く
        sw = New System.IO.StreamWriter(System.IO.Path.Combine(appPath, "NIPPOW.CSV"), False, enc)

        Dim tmpDt As New DataTable
        Dim CountDt As New DataTable
        Sql = "Select * From WK_SHUKEI WHERE BUICODE = 0  ORDER BY KDATE, EDABAN1;"
        Call .GetResult(tmpDt, Sql)

        Dim KDATE As String
        Dim timDate As Date
        Dim dtRow As DataRow
        Dim GYO As Integer = 1
        Dim wLINE As Integer = 0

        KDATE = String.Empty

        If (1 <= tmpDt.Rows.Count) Then
          For i = 0 To tmpDt.Rows.Count - 1

            dtRow = tmpDt.Rows(i)

            If (KDATE.Equals(dtRow("KDATE").ToString) = False) Then
              ' 更新日付が設定済みかどうか判定
              If (IsDate(KDATE)) Then
                ' 加工日日報データの合計行
                GOKEI_RTN(sw, GYO, wLINE)
              End If

              Sql = "Select KDATE,EDABAN1 From WK_SHUKEI WHERE BUICODE = 0 AND KDATE = #" & dtRow("KDATE").ToString & "# GROUP BY KDATE,EDABAN1;"

              Call .GetResult(CountDt, Sql)

              sw.Write(CSV_SPACE & "加工日" & CSV_SPACE & CSV_COMMA)
              KDATE = dtRow("KDATE").ToString

              timDate = Date.Parse(KDATE)
              sw.Write(CSV_SPACE & timDate.ToShortDateString & CSV_SPACE & CSV_COMMA)
              sw.Write(CSV_SPACE & "加工頭数" & CSV_SPACE & CSV_COMMA)
              sw.Write(CSV_SPACE & CountDt.Rows.Count & "頭" & CSV_SPACE)

              '改行
              sw.Write(vbCrLf)

              sw.Write(CSV_SPACE & "枝番号" & CSV_SPACE & CSV_COMMA)
              sw.Write(CSV_SPACE & "実貫重量" & CSV_SPACE & CSV_COMMA)
              sw.Write(CSV_SPACE & "水引重量" & CSV_SPACE & CSV_COMMA)
              sw.Write(CSV_SPACE & "加工重量" & CSV_SPACE & CSV_COMMA)
              sw.Write(CSV_SPACE & "仕向先" & CSV_SPACE & CSV_COMMA)
              sw.Write(CSV_SPACE & "単価" & CSV_SPACE & CSV_COMMA)
              sw.Write(CSV_SPACE & "金額" & CSV_SPACE)

              '改行
              sw.Write(vbCrLf)
              wLINE = 0
              GYO = GYO + 2
            End If

            '　左重量が０以外の場合
            If (DTConvDouble(dtRow("HIDARI1")) <> 0) Then
              sw.Write(dtRow("EDABAN1").ToString & CSV_COMMA)
              sw.Write(CSV_COMMA)
              sw.Write(DTConvDouble(dtRow("LMAE1")).ToString("#,##0.0") & CSV_COMMA)
              sw.Write(DTConvDouble(dtRow("HIDARI1")).ToString("#,##0.0") & CSV_COMMA)
              sw.Write(dtRow("TNAME").ToString & CSV_COMMA)
              sw.Write(NIPPO_GSABAKI.ToString & CSV_COMMA)
              sw.Write("=INT(C" & GYO.ToString & "*F" & GYO.ToString & ")")

              '改行
              sw.Write(vbCrLf)
              GYO = GYO + 1
              wLINE = wLINE + 1
            End If

            '　右重量が０以外の場合
            If (DTConvDouble(dtRow("MIGI1")) <> 0) Then
              sw.Write(dtRow("EDABAN1").ToString & CSV_COMMA)
              sw.Write(CSV_COMMA)
              sw.Write(DTConvDouble(dtRow("RMAE1")).ToString("#,##0.0") & CSV_COMMA)
              sw.Write(DTConvDouble(dtRow("MIGI1")).ToString("#,##0.0") & CSV_COMMA)
              sw.Write(dtRow("TNAME").ToString & CSV_COMMA)
              sw.Write(NIPPO_GSABAKI.ToString & CSV_COMMA)
              sw.Write("=INT(C" & GYO.ToString & "*F" & GYO.ToString & ")")

              '改行
              sw.Write(vbCrLf)
              GYO = GYO + 1
              wLINE = wLINE + 1
            End If
          Next

          ' 加工日日報データの合計行の計算
          GOKEI_RTN(sw, GYO, wLINE)
        End If

        '加工日日報データをEXCELで開く
        Dim officeFileProc As New Process
        With officeFileProc
          .StartInfo.FileName = System.IO.Path.Combine(appPath, "NIPPOW.CSV")
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
    End With

    Return ret

  End Function

  ''' <summary>
  ''' 更新or追加処理判定
  ''' </summary>
  ''' <param name="stShukei"></param>
  ''' <param name="sqlOption"></param>
  Private Sub WorkTb_Set_Rtn(stShukei As structShukei, dtRow As DataRow, sqlOption As String)

    Try

      Dim rowTmp As DataRow()
      rowTmp = tmpSyukeiDT.Select(sqlOption)

      If (1 <= rowTmp.Count) Then

        '更新
        UpdShukeiData(stShukei, dtRow, rowTmp)

      Else

        ' 新規追加
        InsShukeiData(stShukei, dtRow)

      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
    Finally
    End Try

  End Sub

  ''' <summary>
  ''' 追加処理
  ''' </summary>
  ''' <param name="stShukei"></param>
  ''' <param name="dtRow"></param>
  Private Sub InsShukeiData(stShukei As structShukei, dtRow As DataRow)

    Dim tmpKeyVal As Dictionary(Of String, String) = TargetValNewShukeiVal()

    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    tmpKeyVal("SEQNO") = stShukei.wSEQNO.ToString
    tmpKeyVal("KDATE") = dtRow("KAKOUBI").ToString
    tmpKeyVal("PAGENO") = stShukei.wPageNo.ToString
    tmpKeyVal("TCODE") = dtRow("OLDTKC").ToString
    tmpKeyVal("TNAME") = stShukei.wTNAME
    tmpKeyVal("BUICODE") = stShukei.wBICODE.ToString
    tmpKeyVal("BUIMEI") = stShukei.wBUIMEI
    tmpKeyVal("HONSU") = "0"
    tmpKeyVal("HAKOSU") = "0"
    tmpKeyVal("BUDOMARI") = stShukei.wBUDOMARI.ToString

    ' 27-41
    tmpKeyVal("EDABAN1") = "0"
    tmpKeyVal("EDABAN2") = "0"
    tmpKeyVal("EDABAN3") = "0"
    tmpKeyVal("EDABAN4") = "0"
    tmpKeyVal("EDABAN5") = "0"
    tmpKeyVal("HIDARI1") = "0"
    tmpKeyVal("HIDARI2") = "0"
    tmpKeyVal("HIDARI3") = "0"
    tmpKeyVal("HIDARI4") = "0"
    tmpKeyVal("HIDARI5") = "0"
    tmpKeyVal("MIGI1") = "0"
    tmpKeyVal("MIGI2") = "0"
    tmpKeyVal("MIGI3") = "0"
    tmpKeyVal("MIGI4") = "0"
    tmpKeyVal("MIGI5") = "0"

    ' 47-61
    tmpKeyVal("ITTOU1") = "0"
    tmpKeyVal("ITTOU2") = "0"
    tmpKeyVal("ITTOU3") = "0"
    tmpKeyVal("ITTOU4") = "0"
    tmpKeyVal("ITTOU5") = "0"
    tmpKeyVal("LMAE1") = "0"
    tmpKeyVal("LMAE2") = "0"
    tmpKeyVal("LMAE3") = "0"
    tmpKeyVal("LMAE4") = "0"
    tmpKeyVal("LMAE5") = "0"
    tmpKeyVal("RMAE1") = "0"
    tmpKeyVal("RMAE2") = "0"
    tmpKeyVal("RMAE3") = "0"
    tmpKeyVal("RMAE4") = "0"
    tmpKeyVal("RMAE5") = "0"

    Select Case stShukei.wGYO
      Case 1
        tmpKeyVal("KOTAINO1") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
        tmpKeyVal("SHUB1") = stShukei.wSHUB
        tmpKeyVal("GENSAN1") = stShukei.wGENSN
        tmpKeyVal("EDABAN1") = dtRow("EBCODE").ToString
        tmpKeyVal("KAKU1") = stShukei.JyoujyouNo.ToString
        tmpKeyVal("ITTOU1") = dtRow("SAYUKUBUN").ToString
        tmpKeyVal("LMAE1") = ((stShukei.LMAEw \ 100) / 10).ToString
        tmpKeyVal("RMAE1") = ((stShukei.RMAEw \ 100) / 10).ToString
      Case 2
        tmpKeyVal("KOTAINO2") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
        tmpKeyVal("SHUB2") = stShukei.wSHUB
        tmpKeyVal("GENSAN2") = stShukei.wGENSN
        tmpKeyVal("EDABAN2") = dtRow("EBCODE").ToString
        tmpKeyVal("KAKU2") = stShukei.JyoujyouNo.ToString
        tmpKeyVal("ITTOU2") = dtRow("SAYUKUBUN").ToString
        tmpKeyVal("LMAE2") = ((stShukei.LMAEw \ 100) / 10).ToString
        tmpKeyVal("RMAE2") = ((stShukei.RMAEw \ 100) / 10).ToString
      Case 3
        tmpKeyVal("KOTAINO3") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
        tmpKeyVal("SHUB3") = stShukei.wSHUB
        tmpKeyVal("GENSAN3") = stShukei.wGENSN
        tmpKeyVal("EDABAN3") = dtRow("EBCODE").ToString
        tmpKeyVal("KAKU3") = stShukei.JyoujyouNo.ToString
        tmpKeyVal("ITTOU3") = dtRow("SAYUKUBUN").ToString
        tmpKeyVal("LMAE3") = ((stShukei.LMAEw \ 100) / 10).ToString
        tmpKeyVal("RMAE3") = ((stShukei.RMAEw \ 100) / 10).ToString
      Case 4
        tmpKeyVal("KOTAINO4") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
        tmpKeyVal("SHUB4") = stShukei.wSHUB
        tmpKeyVal("GENSAN4") = stShukei.wGENSN
        tmpKeyVal("EDABAN4") = dtRow("EBCODE").ToString
        tmpKeyVal("KAKU4") = stShukei.JyoujyouNo.ToString
        tmpKeyVal("ITTOU4") = dtRow("SAYUKUBUN").ToString
        tmpKeyVal("LMAE4") = ((stShukei.LMAEw \ 100) / 10).ToString
        tmpKeyVal("RMAE4") = ((stShukei.RMAEw \ 100) / 10).ToString
      Case 5
        tmpKeyVal("KOTAINO5") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
        tmpKeyVal("SHUB5") = stShukei.wSHUB
        tmpKeyVal("GENSAN5") = stShukei.wGENSN
        tmpKeyVal("EDABAN5") = dtRow("EBCODE").ToString
        tmpKeyVal("KAKU5") = stShukei.JyoujyouNo.ToString
        tmpKeyVal("ITTOU5") = dtRow("SAYUKUBUN").ToString
        tmpKeyVal("LMAE5") = ((stShukei.LMAEw \ 100) / 10).ToString
        tmpKeyVal("RMAE5") = ((stShukei.RMAEw \ 100) / 10).ToString
    End Select

    Dim rowSyukei As DataRow
    rowSyukei = tmpSyukeiDT.NewRow
    For Each tmpKey As String In tmpKeyVal.Keys
      rowSyukei(tmpKey) = tmpKeyVal(tmpKey)
    Next
    tmpSyukeiDT.Rows.Add(rowSyukei)

  End Sub

  ''' <summary>
  ''' ACESSレポート更新用SQL文作成
  ''' </summary>
  ''' <param name="dtRow"></param>
  ''' <returns>SQL文</returns>
  Private Function SqlInsDataAccess(dtRow As DataRow) As String

    Dim tmpKeyVal As Dictionary(Of String, String) = TargetValNewShukeiVal()
    Dim sql As String = String.Empty

    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    tmpKeyVal("SEQNO") = dtRow("SEQNO").ToString
    tmpKeyVal("KDATE") = "'" & dtRow("KDATE").ToString & "'"
    tmpKeyVal("PAGENO") = dtRow("PAGENO").ToString
    tmpKeyVal("TCODE") = dtRow("TCODE").ToString
    tmpKeyVal("TNAME") = "'" & dtRow("TNAME").ToString & "'"
    tmpKeyVal("BUICODE") = dtRow("BUICODE").ToString
    tmpKeyVal("BUIMEI") = "'" & dtRow("BUIMEI").ToString & "'"
    tmpKeyVal("HONSU") = dtRow("HONSU").ToString
    tmpKeyVal("HAKOSU") = dtRow("HAKOSU").ToString
    tmpKeyVal("KINGAKU") = dtRow("KINGAKU").ToString
    tmpKeyVal("JYURYOKEI") = dtRow("JYURYOKEI").ToString
    tmpKeyVal("BUDOMARI") = dtRow("BUDOMARI").ToString
    tmpKeyVal("KOTAINO1") = "'" & dtRow("KOTAINO1").ToString & "'"
    tmpKeyVal("KOTAINO2") = "'" & dtRow("KOTAINO2").ToString & "'"
    tmpKeyVal("KOTAINO3") = "'" & dtRow("KOTAINO3").ToString & "'"
    tmpKeyVal("KOTAINO4") = "'" & dtRow("KOTAINO4").ToString & "'"
    tmpKeyVal("KOTAINO5") = "'" & dtRow("KOTAINO5").ToString & "'"
    tmpKeyVal("SHUB1") = "'" & dtRow("SHUB1").ToString & "'"
    tmpKeyVal("SHUB2") = "'" & dtRow("SHUB2").ToString & "'"
    tmpKeyVal("SHUB3") = "'" & dtRow("SHUB3").ToString & "'"
    tmpKeyVal("SHUB4") = "'" & dtRow("SHUB4").ToString & "'"
    tmpKeyVal("SHUB5") = "'" & dtRow("SHUB5").ToString & "'"
    tmpKeyVal("GENSAN1") = "'" & dtRow("GENSAN1").ToString & "'"
    tmpKeyVal("GENSAN2") = "'" & dtRow("GENSAN2").ToString & "'"
    tmpKeyVal("GENSAN3") = "'" & dtRow("GENSAN3").ToString & "'"
    tmpKeyVal("GENSAN4") = "'" & dtRow("GENSAN4").ToString & "'"
    tmpKeyVal("GENSAN5") = "'" & dtRow("GENSAN5").ToString & "'"
    tmpKeyVal("EDABAN1") = dtRow("EDABAN1").ToString
    tmpKeyVal("EDABAN2") = dtRow("EDABAN2").ToString
    tmpKeyVal("EDABAN3") = dtRow("EDABAN3").ToString
    tmpKeyVal("EDABAN4") = dtRow("EDABAN4").ToString
    tmpKeyVal("EDABAN5") = dtRow("EDABAN5").ToString
    tmpKeyVal("HIDARI1") = dtRow("HIDARI1").ToString
    tmpKeyVal("HIDARI2") = dtRow("HIDARI2").ToString
    tmpKeyVal("HIDARI3") = dtRow("HIDARI3").ToString
    tmpKeyVal("HIDARI4") = dtRow("HIDARI4").ToString
    tmpKeyVal("HIDARI5") = dtRow("HIDARI5").ToString
    tmpKeyVal("MIGI1") = dtRow("MIGI1").ToString
    tmpKeyVal("MIGI2") = dtRow("MIGI2").ToString
    tmpKeyVal("MIGI3") = dtRow("MIGI3").ToString
    tmpKeyVal("MIGI4") = dtRow("MIGI4").ToString
    tmpKeyVal("MIGI5") = dtRow("MIGI5").ToString
    tmpKeyVal("KAKU1") = "'" & dtRow("KAKU1").ToString & "'"
    tmpKeyVal("KAKU2") = "'" & dtRow("KAKU2").ToString & "'"
    tmpKeyVal("KAKU3") = "'" & dtRow("KAKU3").ToString & "'"
    tmpKeyVal("KAKU4") = "'" & dtRow("KAKU4").ToString & "'"
    tmpKeyVal("KAKU5") = "'" & dtRow("KAKU5").ToString & "'"
    tmpKeyVal("ITTOU1") = dtRow("ITTOU1").ToString
    tmpKeyVal("ITTOU2") = dtRow("ITTOU2").ToString
    tmpKeyVal("ITTOU3") = dtRow("ITTOU3").ToString
    tmpKeyVal("ITTOU4") = dtRow("ITTOU4").ToString
    tmpKeyVal("ITTOU5") = dtRow("ITTOU5").ToString
    tmpKeyVal("LMAE1") = dtRow("LMAE1").ToString
    tmpKeyVal("LMAE2") = dtRow("LMAE2").ToString
    tmpKeyVal("LMAE3") = dtRow("LMAE3").ToString
    tmpKeyVal("LMAE4") = dtRow("LMAE4").ToString
    tmpKeyVal("LMAE5") = dtRow("LMAE5").ToString
    tmpKeyVal("RMAE1") = dtRow("RMAE1").ToString
    tmpKeyVal("RMAE2") = dtRow("RMAE2").ToString
    tmpKeyVal("RMAE3") = dtRow("RMAE3").ToString
    tmpKeyVal("RMAE4") = dtRow("RMAE4").ToString
    tmpKeyVal("RMAE5") = dtRow("RMAE5").ToString

    For Each tmpKey As String In tmpKeyVal.Keys
      tmpDst = tmpDst & tmpKey & " ,"
      tmpVal = tmpVal & tmpKeyVal(tmpKey) & " ,"
    Next
    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= "INSERT INTO WK_SHUKEI (" & tmpDst & ")"
    sql &= "          VALUES(" & tmpVal & ")"

    Return sql

  End Function

  ''' <summary>
  ''' 集計用ワークテーブル更新
  ''' </summary>
  ''' <param name="stShukei"></param>
  ''' <param name="dtRow"></param>
  ''' <param name="dtUpd"></param>
  Private Sub UpdShukeiData(stShukei As structShukei,
                            dtRow As DataRow,
                            ByRef dtUpd As DataRow())

    For Each tmpKeyVal As DataRow In dtUpd

      Select Case stShukei.wGYO
        Case 1
          tmpKeyVal("KOTAINO1") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
          tmpKeyVal("SHUB1") = stShukei.wSHUB
          tmpKeyVal("GENSAN1") = stShukei.wGENSN
          tmpKeyVal("EDABAN1") = dtRow("EBCODE").ToString
          tmpKeyVal("KAKU1") = stShukei.JyoujyouNo.ToString
          tmpKeyVal("ITTOU1") = dtRow("SAYUKUBUN").ToString
          tmpKeyVal("LMAE1") = ((stShukei.LMAEw \ 100) / 10).ToString
          tmpKeyVal("RMAE1") = ((stShukei.RMAEw \ 100) / 10).ToString
        Case 2
          tmpKeyVal("KOTAINO2") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
          tmpKeyVal("SHUB2") = stShukei.wSHUB
          tmpKeyVal("GENSAN2") = stShukei.wGENSN
          tmpKeyVal("EDABAN2") = dtRow("EBCODE").ToString
          tmpKeyVal("KAKU2") = stShukei.JyoujyouNo.ToString
          tmpKeyVal("ITTOU2") = dtRow("SAYUKUBUN").ToString
          tmpKeyVal("LMAE2") = ((stShukei.LMAEw \ 100) / 10).ToString
          tmpKeyVal("RMAE2") = ((stShukei.RMAEw \ 100) / 10).ToString
        Case 3
          tmpKeyVal("KOTAINO3") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
          tmpKeyVal("SHUB3") = stShukei.wSHUB
          tmpKeyVal("GENSAN3") = stShukei.wGENSN
          tmpKeyVal("EDABAN3") = dtRow("EBCODE").ToString
          tmpKeyVal("KAKU3") = stShukei.JyoujyouNo.ToString
          tmpKeyVal("ITTOU3") = dtRow("SAYUKUBUN").ToString
          tmpKeyVal("LMAE3") = ((stShukei.LMAEw \ 100) / 10).ToString
          tmpKeyVal("RMAE3") = ((stShukei.RMAEw \ 100) / 10).ToString
        Case 4
          tmpKeyVal("KOTAINO4") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
          tmpKeyVal("SHUB4") = stShukei.wSHUB
          tmpKeyVal("GENSAN4") = stShukei.wGENSN
          tmpKeyVal("EDABAN4") = dtRow("EBCODE").ToString
          tmpKeyVal("KAKU4") = stShukei.JyoujyouNo.ToString
          tmpKeyVal("ITTOU4") = dtRow("SAYUKUBUN").ToString
          tmpKeyVal("LMAE4") = ((stShukei.LMAEw \ 100) / 10).ToString
          tmpKeyVal("RMAE4") = ((stShukei.RMAEw \ 100) / 10).ToString
        Case 5
          tmpKeyVal("KOTAINO5") = DTConvLong(dtRow("KOTAINO")).ToString("0000000000")
          tmpKeyVal("SHUB5") = stShukei.wSHUB
          tmpKeyVal("GENSAN5") = stShukei.wGENSN
          tmpKeyVal("EDABAN5") = dtRow("EBCODE").ToString
          tmpKeyVal("KAKU5") = stShukei.JyoujyouNo.ToString
          tmpKeyVal("ITTOU5") = dtRow("SAYUKUBUN").ToString
          tmpKeyVal("LMAE5") = ((stShukei.LMAEw \ 100) / 10).ToString
          tmpKeyVal("RMAE5") = ((stShukei.RMAEw \ 100) / 10).ToString
      End Select
    Next

  End Sub

  ''' <summary>
  ''' 集計用ワークテーブル更新1
  ''' </summary>
  ''' <param name="stShukei"></param>
  ''' <param name="dtRow"></param>
  ''' <param name="sqlOption"></param>
  Private Sub WorkTb_Upd01(stShukei As structShukei,
                           dtRow As DataRow,
                           sqlOption As String)

    Try

      Dim rowTmp As DataRow()
      rowTmp = tmpSyukeiDT.Select(sqlOption)

      If (1 <= rowTmp.Count) Then

        If DTConvLong(dtRow("SAYUKUBUN")) <= 1 Then
          For Each tmpKeyVal As DataRow In rowTmp
            Select Case stShukei.wGYO
              Case 1
                tmpKeyVal("HIDARI1") = (DTConvDouble(tmpKeyVal("HIDARI1")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              Case 2
                tmpKeyVal("HIDARI2") = (DTConvDouble(tmpKeyVal("HIDARI2")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              Case 3
                tmpKeyVal("HIDARI3") = (DTConvDouble(tmpKeyVal("HIDARI3")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              Case 4
                tmpKeyVal("HIDARI4") = (DTConvDouble(tmpKeyVal("HIDARI4")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              Case 5
                tmpKeyVal("HIDARI5") = (DTConvDouble(tmpKeyVal("HIDARI5")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
            End Select
            tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + 1).ToString
          Next
        Else
          For Each tmpKeyVal As DataRow In rowTmp
            Select Case stShukei.wGYO
              Case 1
                tmpKeyVal("MIGI1") = (DTConvDouble(tmpKeyVal("MIGI1")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              Case 2
                tmpKeyVal("MIGI2") = (DTConvDouble(tmpKeyVal("MIGI2")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              Case 3
                tmpKeyVal("MIGI3") = (DTConvDouble(tmpKeyVal("MIGI3")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              Case 4
                tmpKeyVal("MIGI4") = (DTConvDouble(tmpKeyVal("MIGI4")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              Case 5
                tmpKeyVal("MIGI5") = (DTConvDouble(tmpKeyVal("MIGI5")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
            End Select
            tmpKeyVal("HAKOSU") = (DTConvLong(tmpKeyVal("HAKOSU")) + 1).ToString
          Next
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
    Finally

    End Try

  End Sub

  ''' <summary>
  ''' 集計用ワークテーブル更新2
  ''' </summary>
  ''' <param name="syoriNo"></param>
  ''' <param name="dtRow"></param>
  ''' <param name="stShukei"></param>
  ''' <param name="sqlOption"></param>
  Private Sub WorkTb_Upd02(syoriNo As Integer,
                           dtRow As DataRow,
                           stShukei As structShukei,
                           sqlOption As String)

    Try

      Dim rowTmp As DataRow()
      rowTmp = tmpSyukeiDT.Select(sqlOption)

      If (1 <= rowTmp.Count) Then

        If syoriNo = typOption.No01 Or syoriNo = typOption.No05 Or syoriNo = typOption.No06 Then
          If DTConvLong(dtRow("SAYUKUBUN")) <= 1 Then
            For Each tmpKeyVal As DataRow In rowTmp
              Select Case stShukei.wGYO
                Case 1
                  tmpKeyVal("HIDARI1") = (DTConvDouble(tmpKeyVal("HIDARI1")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 2
                  tmpKeyVal("HIDARI2") = (DTConvDouble(tmpKeyVal("HIDARI2")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 3
                  tmpKeyVal("HIDARI3") = (DTConvDouble(tmpKeyVal("HIDARI3")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 4
                  tmpKeyVal("HIDARI4") = (DTConvDouble(tmpKeyVal("HIDARI4")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 5
                  tmpKeyVal("HIDARI5") = (DTConvDouble(tmpKeyVal("HIDARI5")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              End Select
              If DTConvLong(dtRow("HONSU")) > 0 Then
                tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + DTConvLong(dtRow("HONSU"))).ToString
              Else
                tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + 1).ToString
              End If
              tmpKeyVal("HAKOSU") = (DTConvLong(tmpKeyVal("HAKOSU")) + 1).ToString
            Next
          Else
            For Each tmpKeyVal As DataRow In rowTmp
              Select Case stShukei.wGYO
                Case 1
                  tmpKeyVal("MIGI1") = (DTConvDouble(tmpKeyVal("MIGI1")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 2
                  tmpKeyVal("MIGI2") = (DTConvDouble(tmpKeyVal("MIGI2")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 3
                  tmpKeyVal("MIGI3") = (DTConvDouble(tmpKeyVal("MIGI3")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 4
                  tmpKeyVal("MIGI4") = (DTConvDouble(tmpKeyVal("MIGI4")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 5
                  tmpKeyVal("MIGI5") = (DTConvDouble(tmpKeyVal("MIGI5")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              End Select
              If DTConvLong(dtRow("HONSU")) > 0 Then
                tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + DTConvLong(dtRow("HONSU"))).ToString
              Else
                tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + 1).ToString
              End If
              tmpKeyVal("HAKOSU") = (DTConvLong(tmpKeyVal("HAKOSU")) + 1).ToString
            Next
          End If
        Else
          If stShukei.wGYO <= 1 Then
            For Each tmpKeyVal As DataRow In rowTmp
              Select Case stShukei.wGYO
                Case 1
                  tmpKeyVal("HIDARI1") = (DTConvDouble(tmpKeyVal("HIDARI1")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 2
                  tmpKeyVal("HIDARI2") = (DTConvDouble(tmpKeyVal("HIDARI2")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 3
                  tmpKeyVal("HIDARI3") = (DTConvDouble(tmpKeyVal("HIDARI3")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 4
                  tmpKeyVal("HIDARI4") = (DTConvDouble(tmpKeyVal("HIDARI4")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 5
                  tmpKeyVal("HIDARI5") = (DTConvDouble(tmpKeyVal("HIDARI5")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              End Select
              If DTConvLong(dtRow("HONSU")) > 0 Then
                tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + DTConvLong(dtRow("HONSU"))).ToString
              Else
                tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + 1).ToString
              End If
              tmpKeyVal("HAKOSU") = (DTConvLong(tmpKeyVal("HAKOSU")) + 1).ToString
            Next
          Else
            For Each tmpKeyVal As DataRow In rowTmp
              Select Case stShukei.wGYO
                Case 1
                  tmpKeyVal("MIGI1") = (DTConvDouble(tmpKeyVal("MIGI1")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 2
                  tmpKeyVal("MIGI2") = (DTConvDouble(tmpKeyVal("MIGI2")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 3
                  tmpKeyVal("MIGI3") = (DTConvDouble(tmpKeyVal("MIGI3")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 4
                  tmpKeyVal("MIGI4") = (DTConvDouble(tmpKeyVal("MIGI4")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
                Case 5
                  tmpKeyVal("MIGI5") = (DTConvDouble(tmpKeyVal("MIGI5")) + (DTConvLong(dtRow("JYURYO")) \ 100) / 10).ToString
              End Select
              If DTConvLong(dtRow("HONSU")) > 0 Then
                tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + DTConvLong(dtRow("HONSU"))).ToString
              Else
                tmpKeyVal("HONSU") = (DTConvLong(tmpKeyVal("HONSU")) + 1).ToString
              End If
              tmpKeyVal("HAKOSU") = (DTConvLong(tmpKeyVal("HAKOSU")) + 1).ToString
            Next
          End If
        End If

      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
    Finally
    End Try

  End Sub

#End Region

#End Region

#Region "イベントプロシージャ"
  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>to
  ''' <param name="e"></param>
  Private Sub Form_EdaSeisan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = "加工実績集計表"

    ' 最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

    ' 日付範囲前のコンボボックスを先頭に設定
    CmbDateProcessing_01.SelectedIndex = 0

    ' 日付範囲後のコンボボックスを先頭に設定
    CmbDateProcessing_02.SelectedIndex = 0

    ' 日付範囲前のコンボボックスは未入力不可
    CmbDateProcessing_01.AvailableBlank = True

    ' 日付範囲後のコンボボックスは未入力不可
    CmbDateProcessing_02.AvailableBlank = True

    ' メッセージラベル設定
    Call SetMsgLbl(Me.lblInformation)
    lblInformation.Text = String.Empty

    ' ボタンのテキスト設定
    ' 表示ボタン
    Button_Hyouji.Text = "F1： 表示"
    ' 終了ボタン
    Button_End.Text = "F12：終了"

    ' 検証実行の有無設定
    ' 初期化ボタン
    Button_Hyouji.CausesValidation = False
    ' 終了ボタン
    Button_End.CausesValidation = False

    ' IPC通信起動
    InitIPC(PRG_ID)

    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Me.CmbDateProcessing_01

    ' セット処理横計明細を初期値とする
    RadioButton1.Checked = True

    '集計用ワークテーブルの初回定義
    Dim tmpKeyVal As Dictionary(Of String, String) = TargetValNewShukeiVal()
    For Each tmpKey As String In tmpKeyVal.Keys
      tmpSyukeiDT.Columns.Add(tmpKey)
    Next

  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_EdaSeisan_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 表示ボタン押下処理
        Me.Button_Hyouji.Focus()
        Me.Button_Hyouji.PerformClick()
      ' F10キー押下時
      Case Keys.F10
        e.Handled = True
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        Me.Button_End.Focus()
        Me.Button_End.PerformClick()
    End Select

  End Sub

  ''' <summary>
  ''' 加工日でTABを押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbDateProcessing_01_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) 

    Select Case e.KeyCode
     'Tabキーが押された時に本来の機能であるフォーカスの移動を無効にして、KeyDown、KeyUpイベントが発生させる
      Case Keys.Tab
        e.IsInputKey = True
    End Select

  End Sub

  ''' <summary>
  ''' 表示ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_Hyouji_Click(sender As Object, e As EventArgs) Handles Button_Hyouji.Click

    Dim ret As Boolean

    ' 選択不可設定
    Button_Hyouji.Enabled = False
    RadioButton1.Enabled = False
    RadioButton6.Enabled = False

    ' セット処理横計明細
    If (RadioButton1.Checked) Then
      ' 集計データ作成
      ret = Data_Set(typOption.No01)
      If (ret) Then
        If (SHUKEI_TATEYOKO = 0) Then
          ' ACCESSの在庫一覧表レポートを表示
          ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_SHUKEI1")
        Else
          ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_SHUKEI2")
        End If
      End If

    End If

    ' 加工日報データ出力
    If (RadioButton6.Checked) Then
      ' 集計データ作成
      ret = Data_Set(typOption.No06)
    End If

    ' 選択不可解除
    Button_Hyouji.Enabled = True
    RadioButton1.Enabled = True
    RadioButton6.Enabled = True

  End Sub

  ''' <summary>
  ''' 終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_End_Click(sender As Object, e As EventArgs) Handles Button_End.Click

    MyBase.AllClear()
    ' 日付範囲前のコンボボックスを更新
    CmbDateProcessing_01.InitCmb()
    ' 日付範囲後のコンボボックスを更新
    CmbDateProcessing_02.InitCmb()

    ' 日付範囲前コンボボックスを先頭に設定
    CmbDateProcessing_01.SelectedIndex = 0
    ' 日付範囲後コンボボックスを先頭に設定
    CmbDateProcessing_02.SelectedIndex = 0
    Me.CmbDateProcessing_01.Focus()

    ' セット処理横計明細を初期値とする
    RadioButton1.Checked = True

    Me.Hide()

  End Sub

#End Region

#Region "SQL関連"

  ''' <summary>
  ''' カット肉加工実績データ取得SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlJissekiOpen(syoriNo As Integer) As String

    Dim sql As String = String.Empty

    sql &= " SELECT * FROM　CUTJ"
    sql &= " WHERE (KUBUN = 1 "
    sql &= "   AND  ((KYOKUFLG = 0 "
    sql &= "   AND    KIKAINO <> 999 "
    sql &= "   AND    NKUBUN = 0 "
    sql &= "   AND    NSZFLG <> 1) "
    sql &= "    OR    NSZFLG = 4)  "
    sql &= "   AND  LABELJI > 0 "

    ' 特定の得意先コードと商品コードを除外もしくは内包するSQL文作成
    sql &= SetSqlTkCodeHani()

    ' 加工賃部位コードは対象から除外する
    sql &= SetSqlWageCode()

    If syoriNo = typOption.No01 Or syoriNo = typOption.No05 Then
      sql &= "   AND GBFLG = 2"
      If (SHUKEI_SP_KUBUN <> 0) Then
        sql &= "   AND SPKUBUN = 1"
      End If
    ElseIf syoriNo = typOption.No02 Then
      If (SHUKEI_SP_KUBUN <> 0) Then
        sql &= "   AND SPKUBUN <> 1"
      End If
    End If
    sql &= " ) "

    ' 日付範囲
    Dim dt As DateTime = DateTime.Parse(ComGetProcTime())
    If IsDate(CmbDateProcessing_01.Text) Then
      sql &= " AND KAKOUBI >= '" & CmbDateProcessing_01.Text & "'"
    Else
      sql &= " AND TDATE > '" & DateAdd(DateInterval.Month, -13, dt).ToString("yyyy/MM/dd") & "'"
    End If
    If IsDate(CmbDateProcessing_02.Text) Then
      sql &= "AND KAKOUBI <= '" & CmbDateProcessing_02.Text & "'"
    End If

    ' 得意先範囲
    If Val(CmbMstCustomer_01.Text) > 0 Then
      sql &= " AND OLDTKC >= " & ComNothing2ZeroText(CmbMstCustomer_01.SelectedValue)
    End If
    If Val(CmbMstCustomer_02.Text) > 0 Then
      sql &= " AND OLDTKC <= " & ComNothing2ZeroText(CmbMstCustomer_02.SelectedValue)
    End If

    ' 枝番範囲
    If Val(TxtEdaban_01.Text) > 0 Then
      sql &= " AND EBCODE >= " & Val(TxtEdaban_01.Text)
    End If
    If Val(TxtEdaban_02.Text) > 0 Then
      sql &= " AND EBCODE <= " & Val(TxtEdaban_02.Text)
    End If

    ' 部位範囲
    If Val(CmbMstItem_01.Text) > 0 Then
      sql &= " AND BICODE >= " & Val(CmbMstItem_01.Text)
    End If
    If Val(CmbMstItem_02.Text) > 0 Then
      sql &= " AND BICODE <= " & Val(CmbMstItem_02.Text)
    End If
    '
    If syoriNo = typOption.No01 Then
      If SHUKEI_NARABI = 0 Then
        '部位コード順
        sql &= " ORDER BY KAKOUBI"
        sql &= "         ,OLDTKC"
        sql &= "         ,SYUBETUC"
        sql &= "         ,EBCODE"
        sql &= "         ,SAYUKUBUN"
        sql &= "         ,BICODE"
        sql &= "         ,SHOHINC"
      Else
        '商品コード順
        sql &= " ORDER BY KAKOUBI"
        sql &= "         ,OLDTKC"
        sql &= "         ,SYUBETUC"
        sql &= "         ,EBCODE"
        sql &= "         ,SAYUKUBUN"
        sql &= "         ,SHOHINC"
        sql &= "         ,BICODE"
      End If
    ElseIf syoriNo = typOption.No02 Then
      sql &= " ORDER BY KAKOUBI"
      sql &= "         ,OLDTKC"
      sql &= "         ,SYUBETUC"
      sql &= "         ,GENSANCHIC"
      sql &= "         ,BICODE"
      sql &= "         ,EBCODE"
      sql &= "         ,SAYUKUBUN"
    ElseIf syoriNo = typOption.No05 Then
      If SHUKEI_NARABI = 0 Then
        '部位コード順
        sql &= " ORDER BY OLDTKC"
        sql &= "         ,SYUBETUC"
        sql &= "         ,EBCODE"
        sql &= "         ,BICODE"
        sql &= "         ,SHOHINC"
      Else
        '商品コード順
        sql &= " ORDER BY OLDTKC"
        sql &= "         ,SYUBETUC"
        sql &= "         ,EBCODE"
        sql &= "         ,SHOHINC"
        sql &= "         ,BICODE"
      End If
    ElseIf syoriNo = typOption.No06 Then
      If SHUKEI_NARABI = 0 Then
        '部位コード順
        sql &= " ORDER BY EBCODE"
        sql &= "         ,SAYUKUBUN"
        sql &= "         ,BICODE"
        sql &= "         ,SHOHINC"
      Else
        '商品コード順
        sql &= " ORDER BY EBCODE"
        sql &= "         ,SAYUKUBUN"
        sql &= "         ,SHOHINC"
        sql &= "         ,BICODE"
      End If
    Else
      sql &= " ORDER BY KAKOUBI"
      sql &= "         ,OLDTKC"
      sql &= "         ,EBCODE"
      sql &= "         ,BICODE"
    End If

    Return sql

  End Function

  ''' <summary>
  ''' 部位マスタ読込用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 部位コードを指定した部位マスタ情報を取得する
  ''' </remarks>
  Private Function SqlReadBuim() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * FROM BUIM"
    sql &= " WHERE KUBUN <> 9 "
    sql &= " ORDER BY BICODE;"

    Return sql

  End Function

  ''' <summary>
  ''' 得意先マスタ読込用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 得意先コードを指定した得意先マスタ情報を取得する
  ''' </remarks>
  Private Function SqlReadTokuisaki() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * FROM TOKUISAKi"
    sql &= " WHERE KUBUN <> 9 "
    sql &= " ORDER BY TKCODE;"

    Return sql

  End Function

  ''' <summary>
  ''' 肢番マスタ読込用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 肢番コードを指定した得意先マスタ情報を取得する
  ''' </remarks>
  Private Function SqlReadEdab() As String
    Dim sql As String = String.Empty
    Dim dt As DateTime = DateTime.Parse(ComGetProcTime())

    sql &= " SELECT * FROM EDAB"
    sql &= " WHERE KUBUN <> 9 "
    sql &= " AND EDAB.TDATE   > '" & DateAdd(DateInterval.Month, -3, dt).ToString("yyyy/MM/dd") & "'"
    sql &= " ORDER BY EBCODE"
    sql &= "        , SIIREBI DESC;"

    Return sql

  End Function

  ''' <summary>
  ''' 種別マスタ読込用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' 種別コードを指定した種別マスタ情報を取得する
  ''' </remarks>
  Private Function SqlReadShub() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * FROM SHUB"
    sql &= " WHERE KUBUN <> 9 "
    sql &= " ORDER BY SBCODE;"

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
  '''集計項目初期設定
  ''' </summary>
  ''' <returns>CUTJ項目のみ設定した連想配列</returns>
  Private Function TargetValNewShukeiVal() As Dictionary(Of String, String)

    Dim ret As New Dictionary(Of String, String)

    With ret

      ' 日付形式データ
      ' 空白は"NULL"に置き換え
      Dim tmpKeyName As String = String.Empty

      .Add("KDATE", "")
      .Add("TNAME", "")
      .Add("BUIMEI", "")

      .Add("KOTAINO1", "")
      .Add("KOTAINO2", "")
      .Add("KOTAINO3", "")
      .Add("KOTAINO4", "")
      .Add("KOTAINO5", "")
      .Add("SHUB1", "")
      .Add("SHUB2", "")
      .Add("SHUB3", "")
      .Add("SHUB4", "")
      .Add("SHUB5", "")
      .Add("GENSAN1", "")
      .Add("GENSAN2", "")
      .Add("GENSAN3", "")
      .Add("GENSAN4", "")
      .Add("GENSAN5", "")
      .Add("KAKU1", "")
      .Add("KAKU2", "")
      .Add("KAKU3", "")
      .Add("KAKU4", "")
      .Add("KAKU5", "")

      ' 数値形式データ
      '  空白は"0"に置き換え
      .Add("SEQNO", "1")
      .Add("PAGENO", "0")
      .Add("TCODE", "0")
      .Add("BUICODE", "0")
      .Add("HONSU", "0")
      .Add("HAKOSU", "0")
      .Add("KINGAKU", "0")
      .Add("JYURYOKEI", "0")
      .Add("BUDOMARI", "0")
      .Add("EDABAN1", "0")
      .Add("EDABAN2", "0")
      .Add("EDABAN3", "0")
      .Add("EDABAN4", "0")
      .Add("EDABAN5", "0")
      .Add("HIDARI1", "0")
      .Add("HIDARI2", "0")
      .Add("HIDARI3", "0")
      .Add("HIDARI4", "0")
      .Add("HIDARI5", "0")
      .Add("MIGI1", "0")
      .Add("MIGI2", "0")
      .Add("MIGI3", "0")
      .Add("MIGI4", "0")
      .Add("MIGI5", "0")
      .Add("ITTOU1", "0")
      .Add("ITTOU2", "0")
      .Add("ITTOU3", "0")
      .Add("ITTOU4", "0")
      .Add("ITTOU5", "0")
      .Add("LMAE1", "0")
      .Add("LMAE2", "0")
      .Add("LMAE3", "0")
      .Add("LMAE4", "0")
      .Add("LMAE5", "0")
      .Add("RMAE1", "0")
      .Add("RMAE2", "0")
      .Add("RMAE3", "0")
      .Add("RMAE4", "0")
      .Add("RMAE5", "0")
    End With

    Return ret
  End Function

#End Region

End Class

