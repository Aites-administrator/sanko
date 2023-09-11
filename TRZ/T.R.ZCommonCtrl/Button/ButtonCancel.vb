Public Class ButtonCancel
  Inherits BtnBase

  ' 取消ボタン

#Region "コンストラクタ"

  ''' <summary>
  ''' 取消ボタン
  ''' </summary>
  Public Sub New()

    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("ボタン押下で取消します。")

  End Sub

  Protected Overrides Sub InitLayout()

    '画像を設定
    If Me.Height <= 63 Then
      Me.Image = My.Resources.ButtonCancelSmall
    Else
      Me.Image = My.Resources.ButtonCancel
    End If

  End Sub

#End Region

End Class