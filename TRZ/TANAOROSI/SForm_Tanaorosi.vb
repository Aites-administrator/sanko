Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class SForm_Tanaorosi

  Private Const PRG_TITLE As String = "棚卸日選択"

#Region "イベントプロシージャー"

  Private Sub CmbDateTanaorosi1_KeyDown(sender As Object, e As KeyEventArgs) Handles CmbDateTanaorosi1.KeyDown
    If e.KeyCode = Keys.Enter Then
      MyBase.SfResult = typSfResult.SF_OK
      DirectCast(Owner, Form_Tanaorosi).txtTanaorosibi.Text = Me.CmbDateTanaorosi1.SelectedValue
      Me.Close()
    End If
  End Sub

  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    MyBase.SfResult = typSfResult.SF_CLOSE
    Me.Close()
  End Sub

  Private Sub SForm_Tanaorosi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.SfResult = typSfResult.SF_CLOSE

    Me.Text = PRG_TITLE

    With Me.CmbDateTanaorosi1
      .AvailableBlank = True
      .SelectedIndex = 0
    End With

    Me.ActiveControl = Me.CmbDateTanaorosi1
  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub SForm_Tanaorosi_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Dim tmpTargetBtn As Button = Nothing

    Select Case e.KeyCode
      ' F10キー押下時
      Case Keys.F10
        e.Handled = True
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

#End Region

End Class
