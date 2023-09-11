Public Class TxtPassWord
  Inherits TxtNumericBase

  ' パスワード売上単価入力テキストボックス

#Region "コンストラクタ"

  Public Sub New()
    ' 数値4桁のみ入力可
    MyBase.New(4)
    ' 入力された文字がすべて*で表示されるようにする
    MyBase.PasswordChar = "*"c
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("パスワードを入力してください。")
    ' 表示位置左寄せ
    MyBase.TextAlign = HorizontalAlignment.Left

  End Sub

#End Region

End Class
