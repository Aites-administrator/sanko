Public Class ButtonReflesh
  Inherits BtnBase

  ' 再読込ボタン

#Region "コンストラクタ"

  Public Sub New()

    '画像を設定
    Me.Image = My.Resources.ButtonReflesh
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("再読込処理を行います。")

  End Sub

#End Region

End Class