Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_ReportPrn01

#Region "メンバ"
#Region "プライベート"
  'ボタンコントロール配列のフィールド
  Private buttonList As New List(Of Button)

#End Region

#End Region

#Region "定数定義"

  Private Const PRG_TITLE As String = "マスター登録リスト"

  ''' <summary>
  ''' プログラムID
  ''' </summary>
  ''' <remarks>IPC通信で使用</remarks>
  Private Const PRG_ID As String = "reportprn"

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_ReportPrn01)
  End Sub
#End Region

#Region "イベントプロシージャ"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>to
  ''' <param name="e"></param>
  Private Sub Form_ReportPrn01_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    ' アプリケーション名設定
    Me.Text = PRG_TITLE

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

    ' IPC通信起動
    InitIPC(PRG_ID)

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
          lblInformation.Text = "仕入先マスターリストを起動します。"
        Case 1
          lblInformation.Text = "得意先マスターリストを起動します。"
        Case 2
          lblInformation.Text = "店舗名マスターリストを起動します。"
        Case 3
          lblInformation.Text = "品種マスターリストを起動します。"
        Case 4
          lblInformation.Text = "格付マスターリストを起動します。"
        Case 5
          lblInformation.Text = "部位マスターリストを起動します。"
        Case 6
          lblInformation.Text = "原産地マスターリストを起動します。"
        Case 7
          lblInformation.Text = "種別マスターリストを起動します。"
        Case 8
          lblInformation.Text = "と場マスターリストを起動します。"
        Case 9
          lblInformation.Text = "担当者マスターリストを起動します。"
        Case 10
          lblInformation.Text = "次ページに進みます。"
        Case 11
          lblInformation.Text = "初期メニューに戻ります。"
      End Select
    End If

  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_ReportPrn01_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown


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
        ReportPrint(9)
      ' F11キー押下時
      Case Keys.F11
        tmpTargetBtn = Me.Cmd_Buton11
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
  ''' 表示ボタン押下時処理
  ''' </summary>
  Private Sub ReportPrint(buttonNo As Integer)

    Dim stReport As New Form_ReportPrnRange.structReport
    stReport.wKubunChk = True
    stReport.wLeftJoinChk = False
    stReport.wSerchFlg = True

    Select Case buttonNo
      Case 0
        ' 仕入先マスター
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_CUTSR
        stReport.wPrnTitle = "仕入先マスター"
        stReport.wTblName = "CUTSR"
        stReport.wOrderPtn = "SRCODE"
        stReport.wSerchTblName = "CUTSR"
        stReport.wSerchCodeId = "SRCODE"
        stReport.wSerchCodeName = "LSRNAME"

      Case 1
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_TOKUISAKI
        stReport.wPrnTitle = "得意先マスター"
        stReport.wTblName = "TOKUISAKI"
        stReport.wOrderPtn = "TKCODE"
        stReport.wSerchTblName = "TOKUISAKI"
        stReport.wSerchCodeId = "TKCODE"
        stReport.wSerchCodeName = "LTKNAME"

      Case 2
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_TENPO
        stReport.wPrnTitle = "店舗名マスター"
        stReport.wTblName = "TENPO_TBL"
        stReport.wOrderPtn = "TENPOCODE"
        stReport.wSerchTblName = "TENPO_TBL"
        stReport.wSerchCodeId = "SSCODE"
        stReport.wSerchCodeName = "TENPOCODE"
        stReport.wKubunChk = False          ' 区分チェック行わない

      Case 3
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_KIKA
        stReport.wPrnTitle = "品種マスター"
        stReport.wTblName = "KIKA"
        stReport.wOrderPtn = "KICODE"
        stReport.wSerchTblName = "KIKA"
        stReport.wSerchCodeId = "KICODE"
        stReport.wSerchCodeName = "KKNAME"

      Case 4
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_KAKU
        stReport.wPrnTitle = "格付マスター"
        stReport.wTblName = "KAKU"
        stReport.wOrderPtn = "KKCODE"
        stReport.wSerchTblName = "KAKU"
        stReport.wSerchCodeId = "KKCODE"
        stReport.wSerchCodeName = "KZNAME"

      Case 5
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_BUIM
        stReport.wPrnTitle = "部位マスター"
        stReport.wTblName = "BUIM"
        stReport.wOrderPtn = "BICODE"
        stReport.wSerchTblName = "BUIM"
        stReport.wSerchCodeId = "BICODE"
        stReport.wSerchCodeName = "BINAME"

      Case 6
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_GENSN
        stReport.wPrnTitle = "原産地マスター"
        stReport.wTblName = "GENSN"
        stReport.wOrderPtn = "GNCODE"
        stReport.wSerchTblName = "GENSN"
        stReport.wSerchCodeId = "GNCODE"
        stReport.wSerchCodeName = "GNNAME"

      Case 7
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_SHUB
        stReport.wPrnTitle = "種別マスター"
        stReport.wTblName = "SHUB"
        stReport.wOrderPtn = "SBCODE"
        stReport.wSerchTblName = "SHUB"
        stReport.wSerchCodeId = "SBCODE"
        stReport.wSerchCodeName = "SBNAME"

      Case 8
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_TOJM
        stReport.wPrnTitle = "と場マスター"
        stReport.wTblName = "TOJM"
        stReport.wOrderPtn = "TJCODE"
        stReport.wSerchTblName = "TOJM"
        stReport.wSerchCodeId = "TJCODE"
        stReport.wSerchCodeName = "TJNAME"

      Case 9
        stReport.wSyoriNo = Form_ReportPrnRange.typMasterList.TBL_TANTO
        stReport.wPrnTitle = "担当者マスター"
        stReport.wTblName = "TANTO_TBL"
        stReport.wOrderPtn = "TANTOC"
        stReport.wSerchTblName = "TANTO_TBL"
        stReport.wSerchCodeId = "TANTOC"
        stReport.wSerchCodeName = "TANTOMEI"
        stReport.wKubunChk = False

      Case Else
        Return

    End Select

    Form_ReportPrnRange.Data_Set(stReport)

  End Sub

  ''' <summary>
  ''' 次のページへ進むボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Cmd_Buton11_Click(sender As Object, e As EventArgs) Handles Cmd_Buton11.Click

    Me.Hide()
    Form_ReportPrn02.Show()

  End Sub

  ''' <summary>
  ''' 終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_End_Click(sender As Object, e As EventArgs) Handles Cmd_Buton12.Click

    ' 終了
    Me.Close()
    Application.Exit()

  End Sub

#End Region

End Class

