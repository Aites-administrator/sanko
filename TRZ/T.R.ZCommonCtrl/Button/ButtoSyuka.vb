Public Class ButtonSyuka
  Inherits BtnBase

  ' 出荷処理ボタン

#Region "コンストラクタ"

  Public Sub New()

    '画像を設定
    'Me.Image = My.Resources.ButtonSyuka.png
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("ボタン押下で出荷処理を行います。")
  End Sub

#End Region

End Class