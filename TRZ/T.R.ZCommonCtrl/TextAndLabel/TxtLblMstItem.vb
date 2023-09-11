Public Class TxtLblMstItem

#Region "定数定義"

#Region "プライベート"
  ''' <summary>
  ''' 検索フォームタイトル
  ''' </summary>
  Private Const SERCH_FORM_TITLE As String = "部位マスタ選択"

  ''' <summary>
  ''' フォーカス時インフォメーションメッセージ
  ''' </summary>
  Private Const INF_MSG_TEXT As String = "部位を入力します。入力なしにEnterを押すと選択候補が表示されます。"
#End Region
#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtLblMstLoad(sender As Object, e As EventArgs) Handles MyBase.Load

    ' 動作設定
    Call MyBase.InitCtrl(Me, New TxtMstItem, SERCH_FORM_TITLE, INF_MSG_TEXT)
  End Sub

#End Region

End Class
