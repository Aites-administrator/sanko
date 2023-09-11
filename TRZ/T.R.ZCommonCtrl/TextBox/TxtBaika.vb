Public Class TxBaika
  Inherits TxtSignedNumericBase

  ' 売価単価入力テキストボックス

#Region "コンストラクタ"

  Public Sub New()
    ' 数値6桁のみ入力可
    MyBase.New(6)
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("売価単価を入力してください。")
  End Sub

#End Region

End Class
