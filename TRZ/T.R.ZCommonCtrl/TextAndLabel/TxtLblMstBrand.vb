Public Class TxtLblMstBrand

  '------------------------------------------
  '   BLOCK_TBLをブランドマスタとして使用
  '------------------------------------------

#Region "定数定義"

#Region "プライベート"
  ''' <summary>
  ''' 検索フォームタイトル
  ''' </summary>
  Private Const SERCH_FORM_TITLE As String = "ブランド選択"

  ''' <summary>
  ''' フォーカス時インフォメーションメッセージ
  ''' </summary>
  Private Const INF_MSG_TEXT As String = "ブランドコードを入力して下さい。"
#End Region
#End Region

  ''' <summary>
  ''' 編集コントロール保持
  ''' </summary>
  Private _control As Control

#Region "イベントプロシージャー"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtLblMstLoad(sender As Object, e As EventArgs) Handles MyBase.Load

    ' 動作設定
    Call MyBase.InitCtrl(Me, New TxtMstBlockCode, SERCH_FORM_TITLE, INF_MSG_TEXT)

  End Sub

#End Region

End Class
