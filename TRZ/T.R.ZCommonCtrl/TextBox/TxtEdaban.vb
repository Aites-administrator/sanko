Public Class TxtEdaban
  Inherits TxtNumericBase

  ' 枝番入力テキストボックス

#Region "コンストラクタ"

  Public Sub New()
    ' 数値5桁のみ入力可
    MyBase.New(5)
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("枝番号を入力してください。")
  End Sub

#End Region

End Class
