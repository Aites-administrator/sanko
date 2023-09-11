Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_ReportPrn02

#Region "定数定義"

  Private Const PRG_TITLE As String = "マスター登録リスト２"

#End Region

#Region "メンバ"
#Region "プライベート"
  'ボタンコントロール配列のフィールド
  Private buttonList As New List(Of Button)

#End Region

#End Region

#Region "イベントプロシージャ"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>to
  ''' <param name="e"></param>
  Private Sub Form_ReportPrn02_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    ' アプリケーション名設定
    Me.Text = PRG_TITLE

    Me.Location = Form_ReportPrn01.Location

    ' 最大サイズと最小サイズを現在のサイズに設定する
    Me.MaximumSize = Me.Size
    Me.MinimumSize = Me.Size

    ' ボタンの配列作成
    buttonList.Add(Cmd_Buton01)
    buttonList.Add(Cmd_Buton02)
    buttonList.Add(Cmd_Buton03)
    buttonList.Add(Cmd_Buton04)
    buttonList.Add(Cmd_Buton05)
    buttonList.Add(Cmd_Buton06)
    buttonList.Add(Cmd_Buton07)
    buttonList.Add(Cmd_Buton08)
    buttonList.Add(Cmd_Buton09)
    buttonList.Add(Cmd_Buton10)
    buttonList.Add(Cmd_Buton11)
    buttonList.Add(Cmd_Buton12)

    ' ボタンをクリックした時にButton_Clickが呼び出されるようにする
    Dim btn As Button
    For Each btn In buttonList
      AddHandler btn.Click, AddressOf Button_Click
      AddHandler btn.Enter, AddressOf Button_Enter
    Next btn

    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Cmd_Buton02
    Me.ActiveControl = Cmd_Buton01

  End Sub

  ''' <summary>
  ''' ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_Click(ByVal sender As Object, ByVal e As EventArgs)

    ' クリックされたボタンのインデックス番号を取得する
    Dim index As Integer = -1
    Dim i As Integer
    For i = 0 To buttonList.Count - 1
      If buttonList(i).Equals(sender) Then
        index = i
        Exit For
      End If
    Next i
    If index > -1 Then
      ReportPrint(index)
    End If

  End Sub

  ''' <summary>
  ''' ボタンフォーカス取得時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_Enter(ByVal sender As Object, ByVal e As EventArgs)

    ' クリックされたボタンのインデックス番号を取得する
    Dim index As Integer = -1
    Dim i As Integer
    For i = 0 To buttonList.Count - 1
      If buttonList(i).Equals(sender) Then
        index = i
        Exit For
      End If
    Next i
    If index > -1 Then
      Select Case index
        Case 0
          lblInformation.Text = "牛豚ＦＬＧマスターリストを起動します。"
        Case 1
          lblInformation.Text = "ブランドマスターリストを起動します。"
        Case 2
          lblInformation.Text = "保存温度マスターリストを起動します。"
        Case 3
          lblInformation.Text = "セット商品名マスターリストを起動します。"
        Case 4
          lblInformation.Text = "商品変換マスターリストを起動します。"
        Case 5
          lblInformation.Text = "得意先変換マスターリストを起動します。"
        Case 6
          lblInformation.Text = "仕入先変換マスターリストを起動します。"
        Case 7
          lblInformation.Text = "テーブル詳細マスターリストを起動します。"
        Case 8
          lblInformation.Text = "製造元マスターリストを起動します。"
        Case 11
          lblInformation.Text = "前ページに戻ります。"
      End Select
    End If

  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_ReportPrn02_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Dim tmpTargetBtn As Button = Nothing

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ReportPrint(0)
      ' F2キー押下時
      Case Keys.F2
        ReportPrint(1)
      ' F3キー押下時
      Case Keys.F3
        ReportPrint(2)
      ' F4キー押下時
      Case Keys.F4
        ReportPrint(3)
      ' F5キー押下時
      Case Keys.F5
        ReportPrint(4)
      ' F6キー押下時
      Case Keys.F6
        ReportPrint(5)
      ' F7キー押下時
      Case Keys.F7
        ReportPrint(6)
      ' F8キー押下時
      Case Keys.F8
        ReportPrint(7)
      ' F9キー押下時
      Case Keys.F9
        ReportPrint(8)
      ' F10キー押下時
      Case Keys.F10
        e.Handled = True
      ' F12キー押下時
      Case Keys.F12
        ' 前のページへ戻るボタン押下処理
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
  ''' 表示ボタン押下時処理
  ''' </summary>
  Private Sub ReportPrint(buttonNo As Integer)

    Dim stReport As New Form_ReportPrnRange.structReport
    stReport.wKubunChk = True
    stReport.wLeftJoinChk = False
    stReport.wSerchFlg = True

    Select Case buttonNo
      Case 0
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_GBFLG
        stReport.wPrnTitle = "牛豚ＦＬＧマスター"
        stReport.wTblName = "GBFLG_TBL"
        stReport.wOrderPtn = "GBCODE"
        stReport.wSerchTblName = "GBFLG_TBL"
        stReport.wSerchCodeId = "GBCODE"
        stReport.wSerchCodeName = "GBNAME"
        stReport.wKubunChk = False

      Case 1
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_BLOCK
        stReport.wPrnTitle = "ブランドマスター"
        stReport.wTblName = "BLOCK_TBL"
        stReport.wOrderPtn = "BLOCKCODE"
        stReport.wSerchTblName = "BLOCK_TBL"
        stReport.wSerchCodeId = "BLOCKCODE"
        stReport.wSerchCodeName = "BLNAME"
        stReport.wKubunChk = False

      Case 2
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_COMNT
        stReport.wPrnTitle = "保存温度マスター"
        stReport.wTblName = "COMNT"
        stReport.wOrderPtn = "CMCODE"
        stReport.wSerchTblName = "COMNT"
        stReport.wSerchCodeId = "CMCODE"
        stReport.wSerchCodeName = "CMNAME"

      Case 3
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_SHOHIN
        stReport.wPrnTitle = "セット商品名マスター"
        stReport.wTblName = "SHOHIN"
        stReport.wOrderPtn = "SHCODE"
        stReport.wSerchTblName = "GBFLG_TBL"
        stReport.wSerchCodeId = "GBCODE"
        stReport.wSerchCodeName = "GBNAME"
        stReport.wKubunChk = False
        stReport.wLeftJoinChk = True
        stReport.wLeftJoin = " LEFT JOIN GBFLG_TBL ON SHOHIN.GBFLG = GBFLG_TBL.GBCODE"

      Case 4
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_SHENKAN
        stReport.wPrnTitle = "商品変換マスター"
        stReport.wTblName = "TOKUISAKI"
        stReport.wOrderPtn = "TOKUISAKI.TKCODE"
        stReport.wSerchTblName = "TOKUISAKI"
        stReport.wSerchCodeId = "TKCODE"
        stReport.wSerchCodeName = "LTKNAME"
        stReport.wLeftJoinChk = True
        stReport.wLeftJoin = " INNER JOIN SHENKAN On TOKUISAKI.TKCODE = SHENKAN.TKCODE " _
                            & "INNER JOIN BUIM ON SHENKAN.SCODE  = BUIM.BICODE"
        stReport.wKubunChk = False

      Case 5
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_THENKAN
        stReport.wPrnTitle = "得意先変換マスター"
        stReport.wTblName = "TOKUISAKI"
        stReport.wOrderPtn = "TOKUISAKI.TKCODE"
        stReport.wSerchTblName = "TOKUISAKI"
        stReport.wSerchCodeId = "TKCODE"
        stReport.wSerchCodeName = "LTKNAME"
        stReport.wLeftJoinChk = True
        stReport.wLeftJoin = " INNER JOIN THENKAN ON TOKUISAKI.TKCODE = THENKAN.TKCODE " _
                            & "INNER JOIN TANTO_TBL On THENKAN.TANTOUC = TANTO_TBL.TANTOC"

      Case 6
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_SIHENKA
        stReport.wPrnTitle = "仕入変換マスター"
        stReport.wTblName = "CUTSR"
        stReport.wOrderPtn = "CUTSR.SRCODE"
        stReport.wSerchTblName = "CUTSR"
        stReport.wSerchCodeId = "SRCODE"
        stReport.wSerchCodeName = "LSRNAME"
        stReport.wLeftJoinChk = True
        stReport.wLeftJoin = " INNER JOIN SIHENKA ON CUTSR.SRCODE = SIHENKA.SRCODE"

      Case 7
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_SUM
        stReport.wPrnTitle = "テーブル詳細マスター"
        stReport.wTblName = "Tbl_Sum"
        stReport.wOrderPtn = "FildNo"
        stReport.wSerchCodeId = "FildNo"
        stReport.wSerchCodeName = "Fild_Name"
        stReport.wKubunChk = False
        stReport.wSerchFlg = False

      Case 8
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_SEIZOU
        stReport.wPrnTitle = "製造元マスター"
        stReport.wTblName = "SEIZOU_TBL"
        stReport.wOrderPtn = "SZCODE"
        stReport.wSerchTblName = "SEIZOU_TBL"
        stReport.wSerchCodeId = "SZCODE"
        stReport.wSerchCodeName = "SZNAME"
        stReport.wKubunChk = False

      Case Else
        Return

    End Select

    Form_ReportPrnRange.Data_Set(stReport)


  End Sub

  ''' <summary>
  ''' 前のページへ戻るボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_End_Click(sender As Object, e As EventArgs) Handles Cmd_Buton12.Click

    Me.Hide()
    Form_ReportPrn01.Show()

  End Sub

#End Region


End Class

