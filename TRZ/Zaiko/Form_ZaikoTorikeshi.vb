Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_ZaikoTorikeshi

#Region "列挙体"

  ''' <summary>
  ''' 処理
  ''' </summary>
  Public Enum typSyori
    ''' <summary>廃棄</summary>
    SYORI_HAIKI = 1
    ''' <summary>取消</summary>
    SYORI_TORIKESHI

  End Enum

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
  ''' パラメータ画面表示
  ''' </summary>
  ''' <remarks>
  ''' TestCode
  ''' </remarks>
  Private Sub DspData(prmTargetData As Dictionary(Of String, String))

    _targetData = prmTargetData

    Dim dt As DateTime = DateTime.Parse(prmTargetData("KAKOUBI"))

    Me.Label_Kakoubi.Text = dt.ToString("yyyy/MM/dd")
    Me.Label_BuiCode.Text = prmTargetData("BICODE")
    Me.Label_EdaNo.Text = prmTargetData("EBCODE")
    Me.Label_No.Text = prmTargetData("TOOSINO")

  End Sub
#End Region

#Region "メンバ"
#Region "プライベート"
  Private _targetData As Dictionary(Of String, String)
#End Region
#End Region

#Region "メソッド"

#Region "プライベート"
  ''' <summary>
  ''' 廃棄・取消変更時イベント
  ''' </summary>
  ''' <param name="syoriNo">1：廃棄、2：取消</param>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Private Function UpDateDb(syoriNo As Integer) As Boolean

    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = False

    ' 実行
    With tmpDb
      Try
        ' SQL文の作成
        sql = SqlSelect()

        Dim mainDatabase As New clsSqlServer
        Dim tmpDt As New DataTable
        Call mainDatabase.GetResult(tmpDt, sql)

        Dim getHenpinBi As String = ""
        If (1 <= tmpDt.Rows.Count) Then

          Dim dtRow As DataRow
          dtRow = tmpDt.Rows(0)

          getHenpinBi = dtRow("HENPINBI").ToString
          If (IsDate(getHenpinBi)) Then

            clsCommonFnc.ComMessageBox("出荷データは返品済みで、取り消しできません。",
                                     "出荷削除変更",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OK)
            Return ret
          End If

        Else

          clsCommonFnc.ComMessageBox("出荷データを在庫にできませんでした。",
                                     "出荷削除変更",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OK)
          Return ret

        End If

        ' トランザクション開始
        .TrnStart()

        ' SQL文の作成
        If (syoriNo = typSyori.SYORI_HAIKI) Then
          sql = SqlHaikiUpd()
        Else
          sql = SqlTorikeshiUpd()
        End If

        ' SQL実行結果が1件か？
        If .Execute(sql) = 1 Then
          ' 更新成功
          .TrnCommit()
          ret = True
        Else
          ' 更新失敗
          Throw New Exception("CutJの更新に失敗しました。他のユーザーによって修正されています。")
        End If

      Catch ex As Exception
        ' Error
        .TrnRollBack()                   ' RollBack
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
      End Try

    End With

    Return ret

  End Function
#End Region

#End Region

#Region "イベントプロシージャー"
  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Torikeshi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = "在庫処理確認画面"

    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Me.BtnCancel

  End Sub

  ''' <summary>
  ''' 取消ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnTorikeshi_Click(sender As Object, e As EventArgs) Handles BtnTorikeshi.Click

    UpDateDb(typSyori.SYORI_TORIKESHI)

    MyBase.SfResult = typSfResult.SF_OK
    Me.Close()

  End Sub

  ''' <summary>
  ''' 廃棄ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnHaiki_Click(sender As Object, e As EventArgs) Handles BtnHaiki.Click

    UpDateDb(typSyori.SYORI_HAIKI)

    MyBase.SfResult = typSfResult.SF_OK
    Me.Close()

  End Sub

  ''' <summary>
  ''' Cancelボタン押下時 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

    MyBase.SfResult = typSfResult.SF_CANCEL
    Me.Close()

  End Sub
#End Region

#Region "SQL関連"

  ''' <summary>
  ''' 取消可能有無判定の検索用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelect() As String

    Dim sql As String = String.Empty

    sql = ""
    sql &= " SELECT * FROM CUTJ "
    sql &= " WHERE  UTKCODE =  " & _targetData("UTKCODE")
    sql &= "    AND BICODE =   " & _targetData("BICODE")
    sql &= "    AND EBCODE  =  " & _targetData("EBCODE")
    sql &= "    AND TOOSINO  = " & _targetData("TOOSINO")
    sql &= "    AND KAKOUBI = '" & _targetData("KAKOUBI") & "'"
    sql &= "    AND NSZFLG <= 2 "
    sql &= "    AND DKUBUN = 0 "
    sql &= "    AND KYOKUFLG = 0 "

    Return sql

  End Function

  ''' <summary>
  ''' 廃棄取消更新用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlHaikiUpd() As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET NSZFLG     = 3 "
    sql &= " 　 ,KDATE      = '" & ComGetProcTime() & "'"
    sql &= "    ,SYUKKABI   = '" & ComGetProcDate() & "'"
    sql &= " WHERE TDATE    = '" & _targetData("TDATE") & "'"
    sql &= "   AND KDATE    = '" & _targetData("KDATE") & "'"
    sql &= "   AND TKCODE   = " & _targetData("TKCODE")
    sql &= "   AND BICODE   = " & _targetData("BICODE")
    sql &= "   AND TOOSINO  = " & _targetData("TOOSINO")
    sql &= "   AND EBCODE   = " & _targetData("EBCODE")
    sql &= "   AND NSZFLG   = " & _targetData("NSZFLG")
    sql &= "   AND DKUBUN   = " & _targetData("DKUBUN")
    sql &= "   AND KYOKUFLG = " & _targetData("KYOKUFLG")

    Return sql

  End Function

  ''' <summary>
  ''' 取消更新用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlTorikeshiUpd() As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET NSZFLG = 5 "
    sql &= " 　 ,KYOKUFLG = 2 "
    sql &= " 　 ,KDATE = '" & ComGetProcTime() & "'"
    sql &= "    ,SYUKKABI = '" & ComGetProcDate() & "'"
    sql &= " WHERE TDATE    = '" & _targetData("TDATE") & "'"
    sql &= "   AND KDATE    = '" & _targetData("KDATE") & "'"
    sql &= "   AND TKCODE   = " & _targetData("TKCODE")
    sql &= "   AND BICODE   = " & _targetData("BICODE")
    sql &= "   AND TOOSINO  = " & _targetData("TOOSINO")
    sql &= "   AND EBCODE   = " & _targetData("EBCODE")
    sql &= "   AND NSZFLG   = " & _targetData("NSZFLG")
    sql &= "   AND DKUBUN   = " & _targetData("DKUBUN")
    sql &= "   AND KYOKUFLG = " & _targetData("KYOKUFLG")

    Return sql

  End Function

#End Region

End Class
