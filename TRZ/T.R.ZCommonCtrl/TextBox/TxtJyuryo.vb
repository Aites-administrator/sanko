Public Class TxJyuryo
  Inherits TxtSignedNumericBase

  ' 重量入力テキストボックス

#Region "コンストラクタ"

  Public Sub New()
    ' 数値6桁のみ入力可
    MyBase.New(6, True)
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("重量をｋｇ単位で入力します。")
  End Sub

#End Region

End Class
