Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_PassWordInput


#Region "プライベート"
  ' 画面を閉じるボタン通知有無
  Private confirm As Boolean

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

#Region "イベントプロシージャ"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_SyukaDateInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = "在庫変更処理"

    Me.LabelMsg.Text = "続けるにはパスワードが必要です" & vbCrLf & "パスワードを入力してください"

    Me.TxtPassWord.Text = ""

    ' キャンセルボタンにフォーカス設定
    Me.ActiveControl = Me.TxtPassWord

  End Sub

  ''' <summary>
  ''' ＯＫボタン押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

    ' パスワードが一致した場合、画面を閉じる
    If (EDASEISAN_PASSWORD.Equals(Me.TxtPassWord.Text)) Then

      ' 画面を閉じるボタン通知しない
      confirm = True

      MyBase.SfResult = typSfResult.SF_OK
      Me.Close()
    Else
      clsCommonFnc.ComMessageBox("パスワードが違います",
                                 "在庫変更処理",
                                 typMsgBox.MSG_WARNING,
                                 typMsgBoxButton.BUTTON_OK)
    End If

  End Sub

  ''' <summary>
  ''' キャンセルボタン押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

    ' 画面を閉じるボタン通知しない
    confirm = True

    MyBase.SfResult = typSfResult.SF_CANCEL
    Me.Close()

  End Sub

  ''' <summary>
  ''' 画面を閉じる場合
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_PassWordInput_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    ' ＯＫボタン、キャンセルボタンを押していない場合
    If (confirm = False) Then
      MyBase.SfResult = typSfResult.SF_CLOSE
    End If

  End Sub

#End Region



End Class