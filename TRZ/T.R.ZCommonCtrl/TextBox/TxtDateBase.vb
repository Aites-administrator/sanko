Imports T.R.ZCommonClass.clsCommonFnc

Public Class TxtDateBase
  Inherits TxtBase

  ' 日付入力テキストボックス

#Region "メンバ"
#Region "プライベート"
  Private _HasError As Boolean
#End Region
#End Region

#Region "プロパティー"
#Region "パブリック"

  ''' <summary>
  ''' 最後に入力された文字列は変換エラーが発生したか？
  ''' </summary>
  ''' <returns>
  '''   True  : 最後に入力された文字列はエラー
  '''   False : 最後に入力された文字列はエラーではない
  ''' </returns>
  Public ReadOnly Property HasError As Boolean
    Get
      Return _HasError
    End Get
  End Property
#End Region
#End Region

#Region "コンストラクタ"
  Public Sub New()
    MyBase.New("yyyy/MM/dd".Length)
    _HasError = False
  End Sub
#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' キー入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' 数字8桁とバックスペースのみ入力可
  ''' </remarks>
  Private Sub TxtDateBase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) _
      AndAlso e.KeyChar <> "/"c _
      AndAlso e.KeyChar <> ControlChars.Back Then
      e.Handled = True
    End If

  End Sub

  Private Sub TxtDateBase_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
    Dim tmpDateText As String = String.Empty
    With Me
      If .Text.Length > 0 Then
        Try
          ' 入力された値を日付形式に変換
          tmpDateText = ComCreateDateText(.Text)
          .Text = ComCreateDateText(.Text)
          _HasError = False
        Catch ex As Exception
          e.Cancel = True
          _HasError = True
        End Try
      End If
    End With

  End Sub

#End Region

End Class
