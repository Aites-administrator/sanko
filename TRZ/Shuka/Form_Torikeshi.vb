Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData

Public Class Form_Torikeshi

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
    _bTotalByEda = prmTargetData.ContainsKey("RCount")
    Me.btnHenpin.Visible = Not _bTotalByEda
    Me.btnHenpin.Enabled = Not _bTotalByEda
    If _bTotalByEda Then
      ' 枝別合計
      Me.Label_EdaNo.Text = prmTargetData("EBCODE")
      Me.Label_Title_Kakoubi.Text = "出荷日"
      Me.Label_Kakoubi.Text = DateTime.Parse(prmTargetData("SYUKKABI")).ToString("yyyy/MM/dd")
      Me.Label_Title_BuiCode.Text = "得意先"
      Me.Label_BuiCode.Text = prmTargetData("TOKUISAKI_NAME")
      Me.Label_No.Text = prmTargetData("RCount")
    Else
      ' 通常
      Dim dt As DateTime = DateTime.Parse(prmTargetData("KAKOUBI"))

      Me.Label_Kakoubi.Text = dt.ToString("yyyy/MM/dd")
      Me.Label_BuiCode.Text = prmTargetData("BICODE")
      Me.Label_EdaNo.Text = prmTargetData("EBCODE")
      Me.Label_No.Text = prmTargetData("TOOSINO")
    End If


  End Sub
#End Region

#Region "メンバ"
#Region "プライベート"
  Private _targetData As Dictionary(Of String, String)

  ''' <summary>
  ''' 枝別合計表示フラグ（True:枝別合計表示 False:通常表示）
  ''' </summary>
  Private _bTotalByEda As Boolean
#End Region
#End Region

#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' 返金額入力画面表示
  ''' </summary>
  Private Sub ShowHenkinForm()
    Dim tmpFormHenkin As New Form_HenkinInput

    Try
      tmpFormHenkin.ShowSubForm(_targetData)
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("返品処理に失敗しました。")
    Finally
      tmpFormHenkin.Dispose()
    End Try

  End Sub


  ''' <summary>
  ''' 取消変更時イベント
  ''' </summary>
  ''' <returns>
  '''  True   -   成功
  '''  False  -   失敗
  ''' </returns>
  Public Function UpDateDb() As Boolean

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
        sql = SqlUpd()
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

  ''' <summary>
  ''' 取消変更時イベント(枝別合計)
  ''' </summary>
  ''' <returns></returns>
  Private Function UpDateDb2() As Boolean
    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty
    Dim ret As Boolean = False
    Dim tmpRCout As Double = Double.Parse(_targetData("RCount"))
    Dim mainDatabase As New clsSqlServer
    Dim tmpDt As New DataTable

    ' 実行
    With tmpDb
      Try
        ' SQL文の作成
        sql = SqlSelect()

        Call mainDatabase.GetResult(tmpDt, sql)

        Dim getHenpinBi As String = ""
        If (tmpRCout = tmpDt.Rows.Count) Then


          For Each tmpRow As DataRow In tmpDt.Rows
            getHenpinBi = tmpRow("HENPINBI").ToString
            If (IsDate(getHenpinBi)) Then

              clsCommonFnc.ComMessageBox("出荷データは返品済みで、取り消しできません。",
                                     "出荷削除変更",
                                     typMsgBox.MSG_NORMAL,
                                     typMsgBoxButton.BUTTON_OK)
              Return ret
            End If
          Next

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
        sql = SqlUpd()
        ' SQL実行結果が対象件数と同じか？
        If .Execute(sql) = tmpRCout Then
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
      Finally
        tmpDt.Dispose()
        tmpDb.Dispose()
        mainDatabase.Dispose()
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

    Me.Text = "出荷処理確認画面"

    ' ロード時にフォーカスを設定する
    Me.ActiveControl = Me.BtnCancel

  End Sub

  ''' <summary>
  ''' 取消ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnTorikeshi_Click(sender As Object, e As EventArgs) Handles BtnTorikeshi.Click

    MyBase.SfResult = typSfResult.SF_OK

    If _bTotalByEda Then
      ' 枝別合計表示
      UpDateDb2()
    Else
      ' 通常表示
      UpDateDb()
    End If

    Me.Close()

  End Sub

  ''' <summary>
  ''' 返品ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnHenpin_Click(sender As Object, e As EventArgs) Handles btnHenpin.Click

    Try

      If IsDate(_targetData("HENPINBI")) Then
        '----------------------------
        '       返品済データ
        '----------------------------
        ComMessageBox("出荷データは返品済みです。", "出荷返品処理", typMsgBox.MSG_WARNING, typMsgBoxButton.BUTTON_OK)

      ElseIf _targetData("DENNO") = "0" AndAlso _targetData("BICODE") = EDANIKU_CODE.ToString Then
        '----------------------------
        '   売上変換前枝肉返品処理
        '----------------------------
        Dim tmpRet As typMsgBoxResult = ComMessageBox("出荷データは伝票作成していませんので、取り消しになります。" _
                                                    , "出荷返品処理" _
                                                    , typMsgBox.MSG_WARNING _
                                                    , typMsgBoxButton.BUTTON_OKCANCEL)
        If tmpRet = typMsgBoxResult.RESULT_OK Then
          ' 取消処理
          UpDateDb()
        End If
      ElseIf _targetData("DENNO") = "0" Then
        '----------------------------
        ' 売上変換前枝肉"以外"返品
        '----------------------------
        Dim tmpRet As typMsgBoxResult = ComMessageBox("出荷データは伝票作成していませんので、取り消しになります。" _
                                                        & vbCrLf & "どうしても返品にしたいときと買取は'いいえ'を押します" _
                                                    , "出荷返品処理" _
                                                    , typMsgBox.MSG_WARNING _
                                                    , typMsgBoxButton.BUTTON_YESNOCANCEL)
        If tmpRet = typMsgBoxResult.RESULT_YES Then
          ' 取消処理
          UpDateDb()
        ElseIf tmpRet = typMsgBoxResult.RESULT_NO Then
          ' 返品処理
          Call ShowHenkinForm()
        End If

      Else
        ' 返品処理
        Call ShowHenkinForm()
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    Finally
      MyBase.SfResult = typSfResult.SF_OK
      Me.Close()
    End Try

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
    sql &= " WHERE  UTKCODE  =  " & _targetData("UTKCODE")
    sql &= "    AND EBCODE   =  " & _targetData("EBCODE")
    sql &= "    AND SYUKKABI = '" & _targetData("SYUKKABI") & "'"
    sql &= "    AND KAKOUBI  = '" & _targetData("KAKOUBI") & "'"

    If _bTotalByEda Then
      ' 枝別合計
      sql &= SqlWhereEdaConditions()
    Else
      ' 通常
      sql &= "  AND BICODE   =  " & _targetData("BICODE")
      sql &= "  AND TOOSINO  =  " & _targetData("TOOSINO")
    End If

    sql &= "    AND NSZFLG   = 2 "
    sql &= "    AND DKUBUN   = 0 "
    sql &= "    AND KYOKUFLG = 0 "
    sql &= "    AND JYURYO   > 0 "

    Return sql

  End Function

  ''' <summary>
  ''' 取消更新用SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlUpd() As String

    Dim sql As String = String.Empty

    sql &= " UPDATE CUTJ "
    sql &= " SET NSZFLG     = 0 "
    sql &= "   , SYUKKABI   = Null "
    sql &= "   , GYONO      = 0 "
    sql &= "   , DENNO      = 0 "
    sql &= "   , KDATE      = '" & ComGetProcTime() & "'"
    sql &= " WHERE EBCODE   =  " & _targetData("EBCODE")
    sql &= "   AND NSZFLG   = 2 "
    sql &= "   AND DKUBUN   = 0 "
    sql &= "   AND KYOKUFLG = 0 "


    If _bTotalByEda Then
      ' 枝別合計
      sql &= SqlWhereEdaConditions()
    Else
      ' 通常
      sql &= "   AND TDATE    = '" & _targetData("TDATE") & "'"
      sql &= "   AND KDATE    = '" & _targetData("KDATE") & "'"
      sql &= "   AND KUBUN    =  " & _targetData("KUBUN")
      sql &= "   AND BICODE   =  " & _targetData("BICODE")
      sql &= "   AND KIKAINO  =  " & _targetData("KIKAINO")
      sql &= "   AND SIRIALNO =  " & _targetData("SIRIALNO")
      sql &= "   AND TOOSINO  =  " & _targetData("TOOSINO")
    End If

    Return sql

  End Function

  ''' <summary>
  ''' 枝別合計時のCutJ抽出条件作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlWhereEdaConditions() As String
    Dim sql As String = String.Empty

    sql &= "  AND CUTJ.SETCD     = " & _targetData("SETCD")
    sql &= "  AND CUTJ.TANKA     = " & _targetData("TANKA")
    sql &= "  AND CUTJ.GENKA     = " & _targetData("GENKA")
    sql &= "  AND CUTJ.KOTAINO   = " & _targetData("KOTAINO")
    sql &= "  AND CUTJ.KAKUC     = " & _targetData("KAKUC")
    sql &= "  AND CUTJ.SAYUKUBUN = " & _targetData("SAYUKUBUN")

    Return sql
  End Function

#End Region

End Class
