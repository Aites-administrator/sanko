Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_MentMenu01

#Region "定数定義"

  Private Const PROCESSM01_MAX As Integer = 10

  Private Const PRG_ID As String = "mstmentmenu"
#End Region

#Region "メンバ"
#Region "プライベート"
  'ボタンコントロール配列のフィールド
  Private buttonList As New List(Of Button)
  Private sMenu1(PROCESSM01_MAX) As ClassMentMenu.structMenu
  Private pProcess1(PROCESSM01_MAX) As System.Diagnostics.Process
  ' プロセス終了判定フラグ
  Private chkProcessKill As Boolean = True
#End Region

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_MentMenu01)
  End Sub
#End Region

#Region "メソッド"

  ''' <summary>
  ''' 
  ''' </summary>
  Public Sub iniFileRead()

    '自分自身の存在するフォルダ  
    Dim strPath As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
    strPath = System.IO.Path.Combine(strPath, ClassMentMenu.PRG_FILENAME)

    Dim strKey As String = String.Empty
    Dim color As String = String.Empty
    Dim titleName As String = String.Empty
    Dim exeName As String = String.Empty
    Dim argument As String = String.Empty
    Dim msg As String = String.Empty

    For i = 0 To PROCESSM01_MAX - 1

      strKey = "M01F" & (i + 1).ToString("00")

      ' タイトル名の読込
      titleName = ClassMentMenu.GetIniString(strKey, "TITLE", strPath)
      ' EXEパスの読込
      exeName = ClassMentMenu.GetIniString(strKey, "EXE", strPath)
      ' コマンドライン引数の読込
      argument = ClassMentMenu.GetIniString(strKey, "ARG", strPath)
      ' 色の読込
      color = ClassMentMenu.GetIniString(strKey, "COLOR", strPath)
      ' メッセージの取得
      msg = ClassMentMenu.GetIniString(strKey, "MSG", strPath)


      If (String.IsNullOrEmpty(titleName)) Then
        ' クリア
        sMenu1(i).wSyoriNo = 0
        sMenu1(i).wTitle = ""
        sMenu1(i).wColorR = 0
        sMenu1(i).wColorG = 0
        sMenu1(i).wColorG = 0
        sMenu1(i).wExePath = ""
        sMenu1(i).wArgument = ""
      Else
        ' iniファイルから読み込んだ値を設定
        sMenu1(i).wSyoriNo = i + 1
        sMenu1(i).wTitle = titleName
        If (color.Length >= 6) Then
          sMenu1(i).wColorR = Convert.ToInt32((color.Substring(0, 2)), 16)
          sMenu1(i).wColorG = Convert.ToInt32((color.Substring(2, 2)), 16)
          sMenu1(i).wColorB = Convert.ToInt32((color.Substring(4, 2)), 16)
        End If
        sMenu1(i).wExePath = exeName
        sMenu1(i).wArgument = argument
        sMenu1(i).wMsg = msg
      End If

    Next i

  End Sub

  ''' <summary>
  ''' プロセス起動
  ''' </summary>
  Public Sub procProcessStart(buttonNo As Integer)

    If (pProcess1(buttonNo)) Is Nothing Then

      ' 子プロセスの起動パラメータを作成する
      Dim psi As New ProcessStartInfo(sMenu1(buttonNo).wExePath)

      psi.Arguments = sMenu1(buttonNo).wArgument

      pProcess1(buttonNo) = System.Diagnostics.Process.Start(psi)
    Else
      pProcess1(buttonNo).Start()
    End If

  End Sub

  ''' <summary>
  ''' プロセス終了、画面クローズ処理
  ''' </summary>
  Private Sub ProcessClose()

    If (chkProcessKill) Then
      chkProcessKill = False

      ' 開いているプロセスの終了
      For i = 0 To PROCESSM01_MAX - 1
        Try
          If Not (pProcess1(i)) Is Nothing Then
            pProcess1(i).Kill()
          End If
        Catch ex As Exception
          Call ComWriteErrLog(ex)
        End Try
      Next

      ' 終了
      Me.Close()
      Application.Exit()
    End If

  End Sub

  ''' <summary>
  ''' ボタン設定
  ''' </summary>
  Public Sub setMenu()

    Dim bVisible As Boolean = False
    Dim bData As Button = Cmd_Buton01

    For i = 0 To PROCESSM01_MAX - 1

      If (sMenu1(i).wSyoriNo = i + 1) Then
        bVisible = True
      Else
        bVisible = False
      End If

      Select Case i
        Case 0
          bData = Cmd_Buton01
        Case 1
          bData = Cmd_Buton02
        Case 2
          bData = Cmd_Buton03
        Case 3
          bData = Cmd_Buton04
        Case 4
          bData = Cmd_Buton05
        Case 5
          bData = Cmd_Buton06
        Case 6
          bData = Cmd_Buton07
        Case 7
          bData = Cmd_Buton08
        Case 8
          bData = Cmd_Buton09
        Case 9
          bData = Cmd_Buton10
      End Select

      bData.Visible = bVisible
      bData.Text = sMenu1(i).wTitle
      bData.BackColor = Color.FromArgb(sMenu1(i).wColorR, sMenu1(i).wColorG, sMenu1(i).wColorB)

    Next i

  End Sub

  ''' <summary>
  ''' 表示ボタン押下時処理
  ''' </summary>
  Private Sub MenuFunc(buttonNo As Integer)

    If (sMenu1(buttonNo).wSyoriNo <> 0) Then

      procProcessStart(buttonNo)

    End If

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
    Me.Text = ClassMentMenu.PRG_TITLE

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

    iniFileRead()

    setMenu()

    ' IPC通信起動
    InitIPC(PRG_ID)

  End Sub

  ''' <summary>
  ''' ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_Click(ByVal sender As Object, ByVal e As EventArgs)

    Try

      ' クリックされたボタンのインデックス番号を取得する
      Dim buttonNo As Integer = -1
      Dim i As Integer
      For i = 0 To buttonList.Count - 1
        If buttonList(i).Equals(sender) Then
          buttonNo = i
          Exit For
        End If
      Next i
      If buttonNo > -1 Then
        If buttonNo <= PROCESSM01_MAX Then
          If (sMenu1(buttonNo).wSyoriNo <> 0) Then

            procProcessStart(buttonNo)

          End If
        End If
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
    End Try


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

      Dim msg As String = String.Empty
      If index <= PROCESSM01_MAX Then
        msg = sMenu1(index).wMsg
      End If

      Select Case index
        Case 0
          lblInformation.Text = msg
        Case 1
          lblInformation.Text = msg
        Case 2
          lblInformation.Text = msg
        Case 3
          lblInformation.Text = msg
        Case 4
          lblInformation.Text = msg
        Case 5
          lblInformation.Text = msg
        Case 6
          lblInformation.Text = msg
        Case 7
          lblInformation.Text = msg
        Case 8
          lblInformation.Text = msg
        Case 9
          lblInformation.Text = msg
        Case 10
          lblInformation.Text = msg
        Case 11
          lblInformation.Text = "前ページに戻ります。"
        Case 12
          lblInformation.Text = "システムメニューを終了します。"
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
        MenuFunc(0)
      ' F2キー押下時
      Case Keys.F2
        MenuFunc(1)
      ' F3キー押下時
      Case Keys.F3
        MenuFunc(2)
      ' F4キー押下時
      Case Keys.F4
        MenuFunc(3)
      ' F5キー押下時
      Case Keys.F5
        MenuFunc(4)
      ' F6キー押下時
      Case Keys.F6
        MenuFunc(5)
      ' F7キー押下時
      Case Keys.F7
        MenuFunc(6)
      ' F8キー押下時
      Case Keys.F8
        MenuFunc(7)
      ' F9キー押下時
      Case Keys.F9
        MenuFunc(8)
      ' F10キー押下時
      Case Keys.F10
        MenuFunc(9)
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
  ''' 次のページへ進むボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Cmd_Buton11_Click(sender As Object, e As EventArgs) Handles Cmd_Buton11.Click

    Me.Hide()
    Form_MEntMenu02.Show()

  End Sub

  ''' <summary>
  ''' 終了ボタン押下処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Button_End_Click(sender As Object, e As EventArgs) Handles Cmd_Buton12.Click

    ProcessClose()

  End Sub

  ''' <summary>
  ''' 画面右上×押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_MentMenu01_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    ProcessClose()

  End Sub

#End Region

End Class

