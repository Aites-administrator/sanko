Imports T.R.ZCommonClass.clsCommonFnc

Public Class TxtLblMstBase
  Inherits System.Windows.Forms.UserControl

  '----------------------------------------------
  '       マスタ検索＋名称表示 コントロール
  ' コード入力用テキストボックス（TxtMstBaseを継承）と
  ' 名称表示用テキストボックス（TxtBaseを継承）の
  ' 複合コントロールです
  ' コード入力用テキストボックスはマスタ検索機能を有します（TxtMstBaseの機能）
  '----------------------------------------------
#Region "メンバ"

#Region "プライベート"

  ''' <summary>
  ''' コード入力用コントロールを保持
  ''' </summary>
  Private WithEvents _CodeCtrl As Control = Nothing

  ''' <summary>
  ''' 名称表示用テキストボックスの表示位置 True:縦、False:横
  ''' </summary>
  Private _TxtPos As Boolean = False

#End Region

#Region "パブリック"

  ' Enter時イベント
  Delegate Sub CallBackEnter(sender As Object, e As EventArgs)
  Public lcCallBackEnter As CallBackEnter

#End Region
#End Region

#Region "プロパティ"
#Region "パブリック"

  ''' <summary>
  ''' メッセージラベルへのメッセージ表示
  ''' </summary>
  ''' <param name="msg">メッセージ</param>
  Public Sub SetMsgLabelText(msg As String)

    ' コード表示用テキストボックス動作設定
    With DirectCast(_CodeCtrl, TxtMstBase)
      .SetMsgLabelText(msg)   ' フォーカス時メッセージ設定
    End With

  End Sub

  ''' <summary>
  ''' 縦横表示
  ''' </summary>
  ''' <returns></returns>
  Public Property TxtPos As Boolean
    Get
      Return _TxtPos
    End Get

    Set(value As Boolean)
      _TxtPos = value

      If (_TxtPos) Then
        Me.TxtName.Location = New Point(5, 30)
      Else
        Me.TxtName.Location = New Point(78, 0)
      End If

    End Set
  End Property

  ''' <summary>
  ''' コード
  ''' </summary>
  ''' <returns></returns>
  Public Property CodeTxt As String
    Get
      If _CodeCtrl Is Nothing Then
        Return String.Empty
      Else
        Return _CodeCtrl.Text
      End If
    End Get

    Set(value As String)
      If _CodeCtrl IsNot Nothing Then
        DirectCast(_CodeCtrl, TxtMstBase).Text = value
      End If
    End Set
  End Property

  ''' <summary>
  ''' コード入力用コントロール
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property CodeCtrl As TxtMstBase
    Get
      Return _CodeCtrl
    End Get
  End Property

  ''' <summary>
  ''' 名称表示用コントロール
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property NameCtrl As TxtBase
    Get
      Return Me.TxtName
    End Get
  End Property
#End Region
#End Region

#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' コントロール初期化
  ''' </summary>
  ''' <param name="prmTarget">継承先コントロール</param>
  ''' <param name="prmCodeCtrl">コード入力用コントロール</param>
  ''' <param name="prmFormTitle">検索フォームタイトル</param>
  ''' <param name="prmMsgTxt">フォーカス時メッセージ</param>
  Public Sub InitCtrl(prmTarget As Control _
                      , prmCodeCtrl As Control _
                      , prmFormTitle As String _
                      , prmMsgTxt As String)

    ' 背景色を親コントロールと同一にし透過を装う
    prmTarget.BackColor = prmTarget.Parent.BackColor

    ' コード入力用コントロールを配置
    With prmCodeCtrl
      .Location = New Point(0, 0)
      .Size = New System.Drawing.Size(70, 28)
    End With
    Me.Controls.Add(prmCodeCtrl)
    _CodeCtrl = prmCodeCtrl

    ' デザイン確認用ダミーコントロール削除
    Me.Controls.Remove(Me.TxtDummy)

    ' コード表示用テキストボックス動作設定
    With DirectCast(prmCodeCtrl, TxtMstBase)
      .SetFormTitle(prmFormTitle)   ' 検索フォームタイトル表示
      .SetNameCtrl(Me.TxtName)      ' 名称表示用コントロール設定
      .SetMsgLabelText(prmMsgTxt)   ' フォーカス時メッセージ設定
    End With

  End Sub

#End Region
#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' コード入力テキストボックスエンター時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TextBox_Enter(sender As Object, e As EventArgs) Handles _CodeCtrl.Enter
    If lcCallBackEnter IsNot Nothing Then
      Call lcCallBackEnter(sender, e)
    End If
  End Sub

#End Region
End Class
