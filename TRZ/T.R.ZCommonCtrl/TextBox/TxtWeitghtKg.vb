Public Class TxtWeitghtKg
  Inherits TxtNumericBase

  '----------------------------------------------
  '          重量(Kg)入力テキストボックス
  '
  '
  '----------------------------------------------

#Region "コンストラクタ"
  Public Sub New()
    ' 数値8桁(小数点可)のみ入力可
    MyBase.New(8, True)
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("重量をKg単位で入力します。")
  End Sub

  Private Sub InitializeComponent()
    Me.SuspendLayout()
    '
    'TxtWeitghtKg
    '
    Me.ResumeLayout(False)

  End Sub


#End Region

#Region "イベントプロシージャー"

  Private Sub TxtWeitghtKg_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Validating
    Dim tmpTxtBox As TextBox = DirectCast(sender, TextBox)
    Dim tmpKg As Decimal

    If tmpTxtBox.Text <> "" Then
      If Decimal.TryParse(tmpTxtBox.Text, tmpKg) Then
        ' 小数点第四位で四捨五入し、小数点第三位まで出力
        tmpTxtBox.Text = Math.Round(tmpKg, 3, MidpointRounding.AwayFromZero)
      Else
        e.Cancel = True
      End If
    End If

  End Sub

#End Region


End Class
