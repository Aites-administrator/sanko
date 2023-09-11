Public Class ButtonPrint
  Inherits BtnBase

  ' 印刷ボタン

#Region "コンストラクタ"

  Public Sub New()

    '画像を設定
    Me.Image = My.Resources.ButtonPrint
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("ボタン印刷処理を行います。")

  End Sub

#End Region

End Class