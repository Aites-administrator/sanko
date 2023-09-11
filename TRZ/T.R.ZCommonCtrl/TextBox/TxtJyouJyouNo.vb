Public Class TxtJyouJyouNo
  Inherits TxtNumericBase

  ' 上場番号入力テキストボックス

#Region "コンストラクタ"

  Public Sub New()
    ' 数値4桁のみ入力可
    MyBase.New(4)
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("上場番号を入力してください。")
  End Sub

#End Region

End Class
