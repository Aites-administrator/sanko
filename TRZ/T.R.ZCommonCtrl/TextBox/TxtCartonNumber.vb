Public Class TxtCartonNumber
  Inherits TxtNumericBase

  '----------------------------------------------
  '          カートン番号入力テキストボックス
  '
  '
  '----------------------------------------------

#Region "コンストラクタ"

  Public Sub New()
    ' 数値6桁のみ入力可
    MyBase.New(6)
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("カートン番号（連番）を入力します。カートン番号が無い場合は入力せずにEnterを押してください。")
  End Sub

#End Region

End Class
