Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_ShukaDateInput

#Region "メンバ"
#Region "プライベート"
  Private _targetData As Dictionary(Of String, String)

  'メインフォームオブジェクト
  Private mfrmMain As Form_Zaiko
#End Region
#End Region

#Region "コンストラクタ"
  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()

    ' この呼び出しはデザイナーで必要です。
    InitializeComponent()

    ' InitializeComponent() 呼び出しの後で初期化を追加します。

    ' 画面初期化処理追加
    MyBase.lcCallBackInitForm = AddressOf DspData
  End Sub

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  ''' <param name="afrm">メインフォームオブジェクト</param>
  Public Sub New(ByVal afrm As Form_Zaiko)

    ' この呼び出しはデザイナーで必要です。
    InitializeComponent()

    ' InitializeComponent() 呼び出しの後で初期化を追加します。

    ' 画面初期化処理追加
    MyBase.lcCallBackInitForm = AddressOf DspData

    'メインフォームオブジェクトの退避
    Me.mfrmMain = afrm

  End Sub

  ''' <summary>
  ''' パラメータ画面表示
  ''' </summary>
  ''' <remarks>
  ''' TestCode
  ''' </remarks>
  Private Sub DspData(prmTargetData As Dictionary(Of String, String))

    _targetData = prmTargetData

  End Sub
#End Region

#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' 一覧変更時イベント
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function UpDateShukaDb() As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = True
    Dim sqlKensu As Integer = 0

    ' 実行
    With tmpDb
      Try
        ' トランザクション開始
        .TrnStart()

        ' 明細をダブルクリックし、得意先、セット、入荷単価、売上単価を修正
        ' 得意先の選択テキストを取得
        Dim setTokuisakiCmbText As String = mfrmMain.CmbMstCustomer_02.Text
        ' 得意先が未入力もしくは未選択の場合
        If String.IsNullOrWhiteSpace(setTokuisakiCmbText) Then
          setTokuisakiCmbText = ""
        Else
          ' 得意先コードを取得
          setTokuisakiCmbText = setTokuisakiCmbText.Substring(0, 4)
        End If

        ' セットの選択テキストを取得
        Dim setCdCmbText As String = mfrmMain.CmbMstSetType_02.Text
        ' セットが未入力もしくは未選択の場合
        If String.IsNullOrWhiteSpace(setCdCmbText) Then
          setCdCmbText = ""
        Else
          ' セットコードを取得
          setCdCmbText = setCdCmbText.Substring(0, 4)
        End If

        ' CutJ更新SQL文作成
        ' 枝別合計表示チェックボックス判定
        If (mfrmMain.CheckBox_EdaBetu.Checked) Then
          sql = SqlUpdEdaCutJ(setTokuisakiCmbText _
                            , setCdCmbText _
                            , mfrmMain.TxTanka_02.Text _
                            , mfrmMain.TxBaika_02.Text _
                            , TxtSyukaDate.Text _
                            , mfrmMain.Controlz(mfrmMain.GetDataGridName()).SelectedRow)

          ' 実行対象の件数を取得
          sqlKensu = mfrmMain.GetDataGridKensu()
          If sqlKensu = 0 Then
            ' 更新失敗
            Throw New Exception("件数の取得に失敗しました。")
          End If

        Else
          sql = SqlUpdCutJ(setTokuisakiCmbText _
                       , setCdCmbText _
                       , mfrmMain.TxTanka_02.Text _
                       , mfrmMain.TxBaika_02.Text _
                       , TxtSyukaDate.Text _
                       , mfrmMain.Controlz(mfrmMain.GetDataGridName()).SelectedRow)

          ' 実行対象の件数を1件に設定
          sqlKensu = 1

        End If

        ' SQL実行結果が指定した件数か？
        If .Execute(sql) = sqlKensu Then
          ' 更新成功
          .TrnCommit()
        Else
          ' 更新失敗
          Throw New Exception("CutJの更新に失敗しました。他のユーザーによって修正されています。")
        End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
        .TrnRollBack()                   ' RollBack
        ret = False
      End Try

    End With

    Return ret

  End Function

#End Region

#End Region

#Region "イベントプロシージャ"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_SyukaDateInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = "在庫変更処理"

    Dim kakoubi As String = _targetData("KAKOUBI")
    ' 初期表示メッセージ表示
    LabelMsg.Text = "製造日：" & kakoubi.Substring(0, 10) _
                  & "　枝No：" & _targetData("EBCODE") _
                  & "　のデータを出荷します。" _
                  & vbCrLf _
                  & "出荷日付を入力し「ＯＫ」を押してください。" _
                  & vbCrLf _
                  & "やり直すときは「キャンセル」を押してください"

    ' 出荷日付の初期値に本日日付を設定
    TxtSyukaDate.Text = Now().ToString("yyyy/MM/dd")

    ' キャンセルボタンにフォーカス設定
    Me.ActiveControl = Me.BtnCancel

  End Sub

  ''' <summary>
  ''' ＯＫボタン押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

    ' 出荷日付が日付データかどうか判定
    If IsDate(TxtSyukaDate.Text) Then
      '出荷更新用SQL実行
      UpDateShukaDb()
    End If

    MyBase.SfResult = typSfResult.SF_OK
    Me.Close()

  End Sub

  ''' <summary>
  ''' キャンセルボタン押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

    MyBase.SfResult = typSfResult.SF_CANCEL
    Me.Close()

  End Sub

#End Region

#Region "SQL関連"

  ''' <summary>
  ''' CutJ更新SQL文作成（出荷）
  ''' </summary>
  ''' <param name="prmTokuisaki">得意先コード</param>
  ''' <param name="prmSetCd">セットコード</param>
  ''' <param name="prmTanka">原価</param>
  ''' <param name="prmBaika">売価</param>
  ''' <param name="prmShukaDate">出荷日</param>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdCutJ(prmTokuisaki As String _
                            , prmSetCd As String _
                            , prmTanka As String _
                            , prmBaika As String _
                            , prmShukaDate As String _
                            , prmSelectedRow As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty
    sql &= " UPDATE CUTJ "
    sql &= " SET "

    Dim updateList As New List(Of String)

    updateList.Add(" NSZFLG = 2 ")

    ' 得意先が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTokuisaki) = False Then
      ' 得意先が空白以外の場合
      updateList.Add(" UTKCODE = " & prmTokuisaki)
    End If

    ' セットが空白かどうか判定
    If String.IsNullOrWhiteSpace(prmSetCd) = False Then
      ' セットが空白以外の場合
      updateList.Add(" SETCD = " & prmSetCd)
    End If

    ' 原価が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTanka) = False Then
      ' 原価が空白以外の場合
      updateList.Add(" GENKA = " & prmTanka)
    Else
      ' 原価が空白の場合は0を代入
      updateList.Add(" GENKA = 0 ")
    End If

    ' 売原価が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmBaika) = False Then
      ' 売原価が空白以外の場合
      updateList.Add(" TANKA = " & prmBaika)
      updateList.Add(" BAIKA = " & prmBaika)
    Else
      ' 売原価が空白の場合は0を代入
      updateList.Add(" TANKA = 0 ")
      updateList.Add(" BAIKA = 0 ")
    End If

    ' SP区分を設定
    If Val(prmSetCd) = 0 Then
      updateList.Add(" SPKUBUN = 0 ")
    Else
      updateList.Add(" SPKUBUN = 1 ")
    End If

    ' 出荷日付
    updateList.Add(" SYUKKABI = '" & prmShukaDate & "'")

    ' 担当者
    Dim tantoCd As String
    Dim setStaffCmbText As String = mfrmMain.CmbMstStaff1.Text
    ' セットが未入力もしくは未選択の場合
    If String.IsNullOrWhiteSpace(setStaffCmbText) = False Then
      tantoCd = setStaffCmbText.Substring(0, 4)
      If Val(tantoCd) <> 0 Then
        updateList.Add(" TANTO = " & Val(tantoCd))
      End If
    End If

    sql &= String.Join(",", updateList.ToArray)
    sql &= ", KDATE = '" & ComGetProcTime() & "'"
    sql &= " WHERE EBCODE =    " & prmSelectedRow("EBCODE")

    sql &= "   AND KDATE =   ' " & prmSelectedRow("KDATE") & "'"
    sql &= "   AND BICODE =    " & prmSelectedRow("BICODE")
    sql &= "   AND TOOSINO =   " & prmSelectedRow("TOOSINO")
    sql &= "   AND KAKOUBI = ' " & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG <= 1 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KYOKUFLG = 0 "
    sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")
    sql &= "   AND TKCODE =    " & prmSelectedRow("TKCODE")

    Console.WriteLine(sql)

    Return sql

  End Function

  ''' <summary>
  ''' CutJ更新SQL文作成（（枝別集計出荷））
  ''' </summary>
  ''' <param name="prmTokuisaki">得意先コード</param>
  ''' <param name="prmSetCd">セットコード</param>
  ''' <param name="prmTanka">原価</param>
  ''' <param name="prmBaika">売価</param>
  ''' <param name="prmShukaDate">出荷日</param>
  ''' <param name="prmSelectedRow">編集前の値</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpdEdaCutJ(prmTokuisaki As String _
                            , prmSetCd As String _
                            , prmTanka As String _
                            , prmBaika As String _
                            , prmShukaDate As String _
                            , prmSelectedRow As Dictionary(Of String, String)) As String

    Dim sql As String = String.Empty
    sql &= " UPDATE CUTJ "
    sql &= " SET "

    Dim updateList As New List(Of String)

    updateList.Add(" NSZFLG = 2 ")

    ' 得意先が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTokuisaki) = False Then
      ' 得意先が空白以外の場合
      updateList.Add(" UTKCODE = " & prmTokuisaki)
    End If

    ' セットが空白かどうか判定
    If String.IsNullOrWhiteSpace(prmSetCd) = False Then
      ' セットが空白以外の場合
      updateList.Add(" SETCD = " & prmSetCd)
    End If

    ' 原価が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmTanka) = False Then
      ' 原価が空白以外の場合
      updateList.Add(" GENKA = " & prmTanka)
    Else
      ' 原価が空白の場合は0を代入
      updateList.Add(" GENKA = 0 ")
    End If

    ' 売原価が空白かどうか判定
    If String.IsNullOrWhiteSpace(prmBaika) = False Then
      ' 売原価が空白以外の場合
      updateList.Add(" TANKA = " & prmBaika)
      updateList.Add(" BAIKA = " & prmBaika)
    Else
      ' 売原価が空白の場合は0を代入
      updateList.Add(" TANKA = 0 ")
      updateList.Add(" BAIKA = 0 ")
    End If

    ' SP区分を設定
    If Val(prmSetCd) = 0 Then
      updateList.Add(" SPKUBUN = 0 ")
    Else
      updateList.Add(" SPKUBUN = 1 ")
    End If

    ' 出荷日付
    updateList.Add(" SYUKKABI = '" & prmShukaDate & "'")

    ' 担当者
    Dim tantoCd As String
    Dim setStaffCmbText As String = mfrmMain.CmbMstStaff1.Text
    ' セットが未入力もしくは未選択の場合
    If String.IsNullOrWhiteSpace(setStaffCmbText) = False Then
      tantoCd = setStaffCmbText.Substring(0, 4)
      If Val(tantoCd) <> 0 Then
        updateList.Add(" TANTO = " & Val(tantoCd))
      End If
    End If

    sql &= String.Join(",", updateList.ToArray)
    sql &= ", KDATE = '" & ComGetProcTime() & "'"
    sql &= " WHERE EBCODE =    " & prmSelectedRow("EBCODE")

    sql &= "   AND KDATE <=  ' " & prmSelectedRow("KDATE_MAX") & "'"
    sql &= "   AND KAKOUBI = ' " & prmSelectedRow("KAKOUBI") & "'"
    sql &= "   AND NSZFLG <= 1 "
    sql &= "   AND DKUBUN = 0 "
    sql &= "   AND KYOKUFLG = 0 "
    sql &= "   AND TKCODE =    " & prmSelectedRow("TKCODE")

    '<---------- 2021/05/10 shiomitsu.T
    ' 現行システムでは枝出庫時は以下の項目を更新対象の抽出条件にしていません
    ' が、選択したデータと出荷されるデータの乖離が激しくなるので今回のリリースでは抽出条件に含んでおきます。
    sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")  ' ← 左右が設定されている場合のみ対象に含む
    ' 現行システムに合わせるなら 
    '          →  If prmSelectedRow("SAYUKUBUN") <> String.Empty then  
    '                 sql &= "   AND SAYUKUBUN = " & prmSelectedRow("SAYUKUBUN")
    '              End If
    sql &= "   AND SETCD =     " & prmSelectedRow("SETCD")
    sql &= "   AND KAKUC =     " & prmSelectedRow("KAKUC")
    sql &= "   AND TANKA =     " & prmSelectedRow("TANKA")
    sql &= "   AND GENKA =     " & prmSelectedRow("GENKA")
    sql &= "   AND KOTAINO =   " & prmSelectedRow("KOTAINO")
    ' 2021/05/10 shiomitsu.T  ---------->

    Console.WriteLine(sql)

    Return sql

  End Function

#End Region


End Class