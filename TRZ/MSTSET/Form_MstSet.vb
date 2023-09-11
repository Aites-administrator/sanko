Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_MstSet

#Region "定数定義"

  ''' <summary>
  ''' プログラムID
  ''' </summary>
  ''' <remarks>
  ''' システム内でユニーク
  ''' 二重起動防止とIPC通信に使用
  ''' </remarks>
  Private Const PRG_ID As String = "MstSet"

  Private Const PRG_TITLE As String = "マスターセット"
  Private Const PROGRESS_MAX As Integer = 10
  Private Const TBL_MAX As Integer = 12
#End Region

#Region "スタートアップ"

  <STAThread>
  Shared Sub main()

    ' 二重起動防止のみ
    Call ComStartPrg(PRG_ID, Form_MstSet)
  End Sub

#End Region

#Region "メンバ"
#Region "プライベート"
#End Region
  ' プログレスバーコントロール配列のフィールド
  Private progressBarList As New List(Of ProgressBar)

#End Region

#Region "メソッド"

  ''' <summary>
  ''' 
  ''' </summary>
  Private Sub Mst_Data_set()

    For i = 0 To progressBarList.Count - 1
      progressBarList(i).Minimum = 0
      progressBarList(i).Maximum = 100
      progressBarList(i).Value = 0
    Next

    Dim j As Integer
    For i = 0 To TBL_MAX
      j = i
      If i > 9 Then j = i - 7
      If i = 12 Then j = 9

      ' ファイル作成情報設定
      Tbl_Open(i)

      progressBarList(j).Value = 100

    Next

    lblInformation.Text = "データセット準備できました。ハンディでマスターセットをしてください。"

    btnClose.Focus()

  End Sub

  ''' <summary>
  ''' ファイル作成情報設定
  ''' </summary>
  ''' <param name="Index"></param>
  Public Sub Tbl_Open(Index As Integer)

    Dim sql As String = String.Empty            ' sql(取得先)
    Dim fileName As String       ' 出力ファイル名
    Dim listLen As New List(Of Integer)

    Select Case Index
      ' 部門マスター
      Case 0
        sql = " SELECT GBCODE AS COD, GBNAME AS WNAME FROM GBFLG_TBL ORDER BY GBCODE "
        fileName = "BUMON.DAT"
        listLen.Add(1)          ' 1フィールド目桁数
        listLen.Add(12)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      ' 種別マスター
      Case 1
        sql = " SELECT SBCODE AS COD,SBNAME AS WNAME, " _
            & " CASE " _
            & " WHEN SBCODE = 1 OR SBCODE = 2 OR SBCODE = 3 OR SBCODE = 8 THEN 2 " _
            & " WHEN SBCODE = 4 OR SBCODE = 5 THEN 1 " _
            & " WHEN SBCODE = 6 OR SBCODE = 7 OR SBCODE = 9 THEN 3 " _
            & " ELSE 0 " _
            & " END AS FLD3 " _
            & " FROM SHUB WHERE KUBUN <> 9 ORDER BY SBCODE "
        fileName = "SHUB.DAT"
        listLen.Add(1)          ' 1フィールド目桁数
        listLen.Add(12)         ' 2フィールド目桁数
        listLen.Add(1)          ' 3フィールド目桁数  

      ' 部位マスター
      Case 2
        sql = " (SELECT SHOHINC AS COD, BINAME AS WNAME, '05' AS FLD3, 1 AS TBLNO, BICODE AS CODE FROM BUIM WHERE KUBUN <> 9 AND SHOHINC > 0) " _
            & " UNION" _
            & " (SELECT SHOHINC AS COD, BINAME AS WNAME, '05' AS FLD3, 2 AS TBLNO, MKCODE AS CODE FROM BIHKN WHERE SHOHINC > 0) " _
            & " ORDER BY TBLNO, SHOHINC, CODE "
        fileName = "BUIM.DAT"
        listLen.Add(5)          ' 1フィールド目桁数
        listLen.Add(30)         ' 2フィールド目桁数
        listLen.Add(2)          ' 3フィールド目桁数  

      ' 仕入先マスター
      Case 3
        sql = " SELECT SRCODE AS COD, LSRNAME AS WNAME, KIGYOCD AS FLD3 FROM CUTSR WHERE KUBUN <> 9 ORDER BY SRCODE "
        fileName = "SHIRE.DAT"
        listLen.Add(4)          ' 1フィールド目桁数
        listLen.Add(24)         ' 2フィールド目桁数
        listLen.Add(7)          ' 3フィールド目桁数  

      ' 得意先マスター
      Case 4
        sql = " SELECT TKCODE AS COD,TNAME AS WNAME FROM TOKUISAKI WHERE KUBUN <> 9 ORDER BY TKCODE "
        fileName = "TOKUI.DAT"
        listLen.Add(4)          ' 1フィールド目桁数
        listLen.Add(24)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      ' 担当者マスター
      Case 5
        sql = " SELECT TANTOC AS COD,TANTOMEI AS WNAME FROM TANTO_TBL ORDER BY TANTOC "
        fileName = "TANTO.DAT"
        listLen.Add(6)          ' 1フィールド目桁数
        listLen.Add(24)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      ' 原産地マスター　　
      Case 6
        sql = " SELECT GNCODE AS COD,GNNAME AS WNAME FROM GENSN ORDER BY GNCODE "
        fileName = "GENSN.DAT"
        listLen.Add(4)          ' 1フィールド目桁数
        listLen.Add(24)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      ' 格付マスター
      Case 7
        sql = " SELECT KKCODE AS COD,KZNAME AS WNAME FROM KAKU ORDER BY KKCODE "
        fileName = "KAKU.DAT"
        listLen.Add(2)          ' 1フィールド目桁数
        listLen.Add(12)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      ' 規格マスター
      Case 8
        sql = " SELECT KICODE AS COD,KKNAME AS WNAME FROM KIKA ORDER BY KICODE "
        fileName = "KIKA.DAT"
        listLen.Add(2)          ' 1フィールド目桁数
        listLen.Add(12)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      ' 商品マスター
      Case 9
        sql = " SELECT SHCODE AS COD,HINMEI AS WNAME FROM SHOHIN ORDER BY SHCODE "
        fileName = "SHOHIN.DAT"
        listLen.Add(4)          ' 1フィールド目桁数
        listLen.Add(24)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      ' 仕入先マスター
      Case 10
        sql = " SELECT SRCODE AS COD,LSRNAME AS WNAME,KIGYOCD AS FLD3 FROM CUTSR WHERE KUBUN <> 9 ORDER BY SRCODE "
        fileName = "SHIRE2.DAT"
        listLen.Add(6)          ' 1フィールド目桁数
        listLen.Add(24)         ' 2フィールド目桁数
        listLen.Add(7)          ' 3フィールド目桁数  

      ' 得意先マスター
      Case 11
        sql = " SELECT TKCODE AS COD,TNAME AS WNAME FROM TOKUISAKI WHERE KUBUN <> 9 ORDER BY TKCODE "
        fileName = "TOKUI2.DAT"
        listLen.Add(6)          ' 1フィールド目桁数
        listLen.Add(24)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      ' 商品マスター
      Case 12
        sql = " SELECT SHCODE AS COD,HINMEI AS WNAME FROM SHOHIN ORDER BY SHCODE "
        fileName = "SHOHIN2.DAT"
        listLen.Add(6)          ' 1フィールド目桁数
        listLen.Add(24)         ' 2フィールド目桁数
        listLen.Add(0)          ' 3フィールド目桁数  

      Case Else
        Exit Sub

    End Select

    ' マスターセット用ファイル作成
    tblToCsv(sql, fileName, listLen)

  End Sub

  ''' <summary>
  ''' マスターセット用ファイル作成
  ''' </summary>
  ''' <param name="sql">sql(取得先)</param>
  ''' <param name="fileName">出力ファイル名</param>
  ''' <param name="lenList">フィールド桁数リスト</param>
  Public Shared Function tblToCsv(sql As String,
                       fileName As String,
                       lenList As List(Of Integer)) As Boolean

    Dim ret As Boolean = False

    ' フォルダがなければ作成する
    If System.IO.Directory.Exists(clsGlobalData.FTPDir) = False Then
      System.IO.Directory.CreateDirectory(clsGlobalData.FTPDir)
    End If

    Dim myPath As String = System.IO.Path.Combine(clsGlobalData.FTPDir, fileName)

    Dim sw As New System.IO.StreamWriter(myPath, False, System.Text.Encoding.GetEncoding("shift_jis"))

    Try

      Dim tmpDb As New clsSqlServer
      Dim tmpDt As New DataTable
      Dim wCode As Integer = 999999

      Call tmpDb.GetResult(tmpDt, sql)

      Dim tmpCcd As Integer = 0
      Dim byteLen As Integer = 0

      With tmpDt
        If .Rows.Count > 0 Then
          For idx As Integer = 0 To .Rows.Count - 1

            byteLen = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(.Rows(idx)("COD").ToString)
            If (lenList(0) >= byteLen) Then

              tmpCcd = Integer.Parse(.Rows(idx)("COD").ToString)

              ' 連続した同一コードの場合、処理を飛ばす
              If (wCode <> tmpCcd) Then
                wCode = tmpCcd

                ' フィールド項目１書き込み
                sw.Write(wCode.ToString.PadLeft(lenList(0), "0"c))

                ' フィールド項目２書き込み
                sw.Write(setPadding(.Rows(idx)("WNAME").ToString, lenList(1), " "c))

                ' フィールド項目３書き込み判定
                If (lenList(2) <> 0) Then

                  ' フィールド項目３書き込み
                  sw.Write(setPadding(.Rows(idx)("FLD3").ToString, lenList(2), "0"c))
                End If
                ' 改行
                sw.WriteLine()
              End If
            End If
          Next
        End If
      End With

      ret = True

    Catch ex As Exception
      Call ComWriteErrLog(ex)
    Finally
      sw.Close()
    End Try

    Return ret

  End Function

  ''' <summary>
  ''' 指定した文字で指定桁数詰める、指定桁数以上の場合は切り取り
  ''' </summary>
  ''' <param name="targetData"></param>
  ''' <param name="keta"></param>
  ''' <param name="moji"></param>
  ''' <returns></returns>
  Public Shared Function setPadding(ByRef targetData As String,
                             ByRef keta As Integer,
                             ByRef moji As Char) As String

    If targetData Is Nothing Then
      targetData = ""
    End If

    Dim value As String = String.Empty
    Dim byteLen As Integer = 0


    byteLen = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(targetData)

    If (byteLen >= keta) Then
      value = MidB(targetData, 1, keta)
    Else
      'パディングする文字数を演算
      '（文字数　=　桁　-　(対象文字列のバイト数 - 対象文字列の文字列数)）
      Dim padLength As Integer = keta - (byteLen - targetData.Length)
      value = targetData.PadRight(padLength, moji)
    End If


    Return value

  End Function

  ''' <summary>
  ''' Mid関数のバイト版。文字数と位置をバイト数で指定して文字列を切り抜く。
  ''' </summary>
  ''' <param name="str">対象の文字列</param>
  ''' <param name="Start">切り抜き開始位置。全角文字を分割するよう位置が指定された場合、戻り値の文字列の先頭は意味不明の半角文字となる。</param>
  ''' <param name="Length">切り抜く文字列のバイト数</param>
  ''' <returns>切り抜かれた文字列</returns>
  ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
  Public Shared Function MidB(ByVal str As String, ByVal Start As Integer, Optional ByVal Length As Integer = 0) As String
    '▼空文字に対しては常に空文字を返す

    If str = "" Then
      Return ""
    End If

    '▼Lengthのチェック

    'Lengthが0か、Start以降のバイト数をオーバーする場合はStart以降の全バイトが指定されたものとみなす。

    Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str) - Start + 1

    If Length = 0 OrElse Length > RestLength Then
      Length = RestLength
    End If

    '▼切り抜き

    Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
    Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

    Array.Copy(SJIS.GetBytes(str), Start - 1, B, 0, Length)

    Dim st1 As String = SJIS.GetString(B)

    '▼切り抜いた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。

    Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1) - Start + 1

    If Asc(Strings.Right(st1, 1)) = 0 Then
      'VB.NET2002,2003の場合、最後の１バイトが全角の半分の時
      Return st1.Substring(0, st1.Length - 1)
    ElseIf Length = ResultLength - 1 Then
      'VB2005の場合で最後の１バイトが全角の半分の時
      Return st1.Substring(0, st1.Length - 1)
    Else
      'その他の場合
      Return st1
    End If

  End Function

#End Region

#Region "イベントプロシージャ"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>to
  ''' <param name="e"></param>
  Private Sub Form_MstSet_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    ' アプリケーション名設定
    Me.Text = PRG_TITLE

    ' プログレスバーの配列作成
    progressBarList.Add(ProgressBar01)
    progressBarList.Add(ProgressBar02)
    progressBarList.Add(ProgressBar03)
    progressBarList.Add(ProgressBar04)
    progressBarList.Add(ProgressBar05)
    progressBarList.Add(ProgressBar06)
    progressBarList.Add(ProgressBar07)
    progressBarList.Add(ProgressBar08)
    progressBarList.Add(ProgressBar09)
    progressBarList.Add(ProgressBar10)

    lblInformation.Text = String.Empty

    ' IPC通信起動
    InitIPC(PRG_ID)

  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_MstSet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Dim tmpTargetBtn As Button = Nothing

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 実行ボタン押下処理
        tmpTargetBtn = Me.btnExecute
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        tmpTargetBtn = Me.btnClose
    End Select

    If tmpTargetBtn IsNot Nothing Then
      With tmpTargetBtn
        .Focus()
        .PerformClick()
      End With
    End If

  End Sub

  ''' <summary>
  ''' 実行ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click

    Mst_Data_set()

  End Sub

  ''' <summary>
  ''' 終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

    ' 終了
    Me.Close()
    Application.Exit()

  End Sub

  ''' <summary>
  ''' 画面右上×押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_MstSet_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    ' 終了
    Me.Close()
    Application.Exit()

  End Sub

#End Region

End Class

