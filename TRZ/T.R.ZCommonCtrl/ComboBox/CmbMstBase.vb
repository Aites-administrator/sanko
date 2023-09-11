
Imports T.R.ZCommonClass

Public Class CmbMstBase
  Inherits CmbBase

#Region "メンバ"

#Region "プライベート"
  Private _CodeFormat As String
#End Region

#End Region

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ(ダミー)
  ''' </summary>
  ''' <remarks>ユーザーコントロールには引数なしのコンストラクタが必要です</remarks>
  Public Sub New()
  End Sub

  Public Sub New(prmCodeFormat As String)
    _CodeFormat = prmCodeFormat
  End Sub

#End Region

#Region "メソッド"

#Region "プライベート"

  ' コード存在確認
  Private Function ChkCode(prmCode As String) As Boolean
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    tmpDb.GetResult(tmpDt, lcCallBackCreateSql(prmCode))
    Return (1 <= tmpDt.Rows.Count)
  End Function

  Private Sub CmbMstBase_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
    With Me
      If .Text.Length <= 0 Then
        .SelectedIndex = -1
      Else
        If .Text.Length <= Len(_CodeFormat) Then
          ' マスタ検索
          If ChkCode(.Text) Then
            ' 表示内容設定
            .SelectedValue = .Text
            .Text = .Text
          Else
            ' 全選択
            Me.Select(.Text.Length, 0)
            e.Cancel = True
          End If
        End If
      End If
    End With

  End Sub

#End Region

#End Region


End Class
