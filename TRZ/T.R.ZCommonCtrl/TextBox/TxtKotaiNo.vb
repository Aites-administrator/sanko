Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class TxKotaiNo
  Inherits TxtNumericBase

  ' 個体識別番号入力テキストボックス
#Region "メンバ"

#Region "パブリック"
  ' 個体識別番号エラー時処理コールバック
  Delegate Sub CallBackEnterInvalidKotaiNo(KotaiNo As String)
  Public lcCallBackEnterInvalidKotaiNo As CallBackEnterInvalidKotaiNo

  ' 個体識別番号有効時処理コールバック
  Delegate Sub CallBackEnterEnableKotaiNo(KotaiNo As String)
  Public lcCallBackEnterEnableKotaiNo As CallBackEnterEnableKotaiNo

#End Region

#End Region

#Region "コンストラクタ"

  Public Sub New()
    ' 数値10桁のみ入力可
    MyBase.New(10)
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("個体識別を入力してください。")

  End Sub

  Private Sub InitializeComponent()
    Me.SuspendLayout()
    '
    'TxKotaiNo
    '
    Me.ResumeLayout(False)

  End Sub


#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' チェック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxKotaiNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Validating
    Dim tmpTxt As TextBox = DirectCast(sender, TextBox)

    If tmpTxt.Text <> "" Then
      ' 個体識別番号として正しいか？
      If ComChkKotaiNo(tmpTxt.Text) Then
        ' 個体識別番号有効
        If lcCallBackEnterEnableKotaiNo IsNot Nothing Then
          Call lcCallBackEnterEnableKotaiNo(tmpTxt.Text)
        End If
      Else
        ' 個体識別番号無効

        If lcCallBackEnterInvalidKotaiNo Is Nothing Then
          ' コールバック未設定ならキャンセル
          e.Cancel = True
        Else
          'コールバック実行
          Call lcCallBackEnterInvalidKotaiNo(tmpTxt.Text)
        End If
      End If
    End If
  End Sub

#End Region

End Class
