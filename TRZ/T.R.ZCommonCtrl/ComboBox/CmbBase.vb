Imports T.R.ZCommonClass

Public Class CmbBase
  Inherits ComboBox

#Region "メンバ"

#Region "プライベート"
  ' メッセージ出力ラベル
  Private _msgLabel As Label

  ' メッセージ出力ラベルテキスト
  Private _msgLabelText As String

  ' 空文字列有効無効フラグ
  Private _AvailableBlank As Boolean
#End Region

#Region "パブリック"

  ''' <summary>
  ''' 選択項目抽出SQL文作成関数コールバック(型)
  ''' </summary>
  ''' <param name="prmCode">絞込項目</param>
  ''' <returns>作成したSQL文</returns>
  Delegate Function CallBackCreateSql(ByVal prmCode As String) As String

  ''' <summary>
  ''' 選択項目抽出SQL文作成関数コールバック（本体）
  ''' </summary>
  Public lcCallBackCreateSql As CallBackCreateSql
#End Region

#End Region

#Region "プロパティー"
#Region "パブリック"

  ''' <summary>
  ''' 空文字列の許可
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks>デフォルト False</remarks>
  Public Property AvailableBlank As Boolean
    Get
      Return _AvailableBlank
    End Get
    Set(value As Boolean)
      _AvailableBlank = value
    End Set
  End Property

#End Region
#End Region

#Region "コンストラクタ"
  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()
    _AvailableBlank = False
  End Sub

#End Region

#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' 選択項目抽出SQL文作成関数
  ''' </summary>
  ''' <param name="prmCode">絞込項目</param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>継承先関数で指定されるlcCallBackCreateSqlを実行する</remarks>
  Private Function CreateSql(Optional prmCode As String = "") As String
    Dim tmpSql As String = String.Empty

    If lcCallBackCreateSql IsNot Nothing Then
      tmpSql = lcCallBackCreateSql(prmCode)
    End If

    Return tmpSql
  End Function

#End Region

#Region "パブリック"

  ''' <summary>
  ''' メッセージラベルの定義
  ''' </summary>
  ''' <param name="msgLabel">メッセージを表示するラベル情報</param>
  Public Sub SetMsgLabel(msgLabel As Label)

    _msgLabel = msgLabel

  End Sub

  ''' <summary>
  ''' メッセージラベルへのメッセージ表示
  ''' </summary>
  ''' <param name="msg">メッセージ</param>
  Public Sub SetMsgLabelText(msg As String)

    _msgLabelText = msg

  End Sub

  ''' <summary>
  ''' コンボボックス初期化
  ''' </summary>
  Public Sub InitCmb()
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    tmpDb.GetResult(tmpDt, CreateSql)

    With Me
      .DataSource = tmpDt
      If tmpDt.Columns.Contains("ItemName") Then
        .DisplayMember = tmpDt.Columns("ItemName").ColumnName
      Else
        .DisplayMember = tmpDt.Columns("ItemCode").ColumnName
      End If
      .ValueMember = tmpDt.Columns("ItemCode").ColumnName
      .DropDownStyle = ComboBoxStyle.DropDown

    End With

  End Sub

#End Region

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' マウスホイールが動くと発生するイベント
  ''' </summary>
  Private Sub CmbMstBase_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseWheel
    ' イベントを処理済みにし、選択値が変わらないようにする
    Dim eventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
    eventArgs.Handled = True
  End Sub

  Private Sub CmbMstBase_OnEnter(sender As Object, e As EventArgs) Handles Me.Enter

    'メッセージラベルの定義が未設定の場合
    If _msgLabel Is Nothing Then
      Exit Sub
    Else
      'メッセージラベルへのメッセージの表示
      _msgLabel.Text = _msgLabelText
    End If

  End Sub

#End Region

End Class
