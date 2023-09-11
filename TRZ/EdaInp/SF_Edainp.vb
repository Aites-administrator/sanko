Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class SF_Edainp

#Region "メソッド"
#Region "プライベート"

#Region "SQL文関連"
  ''' <summary>
  ''' 印刷データ抽出用SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelPrintSrc() As String
    Dim sql As String = String.Empty
    Dim sqlWhere As String = String.Empty

    ' 枝情報取得
    sql &= DirectCast(Owner, Form_Edainp).SqlEdaInfSrcBase

    ' 仕入日
    sqlWhere &= "     EDAB.SIIREBI = '" & Me.TxtDateBase1.Text & "'"

    ' 仕入先コード（From）
    With CmbMstShiresakiFrom
      If .SelectedValue IsNot Nothing Then
        sqlWhere &= " AND EDAB.SRCODE >= " & .SelectedValue
      End If
    End With

    ' 仕入先コード（To）
    With CmbMstShiresakiTo
      If .SelectedValue IsNot Nothing Then
        sqlWhere &= " AND EDAB.SRCODE <= " & .SelectedValue
      End If
    End With

    sqlWhere &= " AND EDAB.KUBUN <> 9 "

    Return ComAddSqlSearchCondition(sql, sqlWhere)
  End Function

  ''' <summary>
  ''' 帳票印刷用ワークテーブル挿入SQL文の作成
  ''' </summary>
  ''' <param name="prmSrc">挿入データ</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsPrintWkTbl(prmSrc As Dictionary(Of String, String)) As String
    Dim sql As String = String.Empty
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty

    For Each tmpKey As String In prmSrc.Keys
      tmpDst &= tmpKey & " ,"
      tmpVal &= prmSrc(tmpKey) & " ,"
    Next

    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= " INSERT INTO WK_EDAINFO(" & tmpDst & ")"
    sql &= " VALUES(" & tmpVal & ")"


    Return sql
  End Function

  ''' <summary>
  ''' 印刷用ワークテーブル削除SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlDelPrintWkTbl() As String
    Dim sql As String = String.Empty

    sql &= " DELETE * FROM WK_EDAINFO "

    Return sql
  End Function

#End Region

#Region "印刷関連"

  ''' <summary>
  ''' 印刷ソース取得
  ''' </summary>
  ''' <returns>印刷ソース</returns>
  Private Function GetPrintSrc() As List(Of Dictionary(Of String, String))
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim ret As New List(Of Dictionary(Of String, String))

    With tmpDb
      Try
        .GetResult(tmpDt, SqlSelPrintSrc())
        If tmpDt.Rows.Count > 0 Then
          For Each tmpDr As DataRow In tmpDt.Rows
            Dim tmpDic As New Dictionary(Of String, String)

            With tmpDic
              .Add("NYUKOBI", "'" & tmpDr("NYUKOBI").ToString() & "'")
              .Add("EBCODE", tmpDr("EBCODE").ToString)
              .Add("EDC", tmpDr("EDC").ToString)
              .Add("KOTAINO", "'" & tmpDr("KOTAINO").ToString() & "'")
              .Add("GNNAME", "'" & tmpDr("GNNAME").ToString() & "'")
              .Add("JYURYO", tmpDr("JYURYO").ToString)
              .Add("KKNAME", "'" & tmpDr("KKNAME").ToString() & "'")
              .Add("KZNAME", "'" & tmpDr("KZNAME").ToString() & "'")
              .Add("SBNAME", "'" & tmpDr("SBNAME").ToString() & "'")
              .Add("LSRNAME", "'" & tmpDr("LSRNAME").ToString() & "'")
              .Add("JYURYO１", tmpDr("JYURYO１").ToString)
              .Add("JYURYO２", tmpDr("JYURYO２").ToString)
              .Add("SIIREGAKU", tmpDr("SIIREGAKU").ToString)
              .Add("SIIREGAKU1", tmpDr("SIIREGAKU1").ToString)
              .Add("SIIREGAKU2", tmpDr("SIIREGAKU2").ToString)
            End With

            ret.Add(tmpDic)
          Next

        End If
      Catch ex As Exception
        Call ComWriteErrLog(ex)
        Throw New Exception("印刷データの取得に失敗しました。")
      Finally
        tmpDb.DbDisconnect()
      End Try
    End With

    Return ret
  End Function
#End Region

#End Region
#End Region

#Region "イベントプロシージャー"

#Region "ボタン"

  ''' <summary>
  ''' 印刷ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    If typMsgBoxResult.RESULT_OK = ComMessageBox("枝入荷情報一覧表を印刷しますか？" _
                                                , Form_Edainp.PRG_TITLE _
                                                , typMsgBox.MSG_NORMAL _
                                                , typMsgBoxButton.BUTTON_OKCANCEL _
                                                , prmDefaultButton:=MessageBoxDefaultButton.Button1) Then

      Dim tmpDb As New clsReport(clsGlobalData.REPORT_FILENAME)

      With tmpDb
        Try
          ' ワークテーブル削除
          .Execute(SqlDelPrintWkTbl())

          ' 印刷データをワークテーブルに挿入
          For Each tmpSrc As Dictionary(Of String, String) In GetPrintSrc()
            .Execute(SqlInsPrintWkTbl(tmpSrc))
          Next

          tmpDb.DbDisconnect()

          ' ACCESSの枝入荷情報一覧表レポートを表示
          ComAccessRun(clsGlobalData.PRINT_PREVIEW, "R_EDAINFO")

        Catch ex As Exception
          Call ComWriteErrLog(ex, False)
        Finally
          .Dispose()
        End Try

      End With

    End If
  End Sub

  ''' <summary>
  ''' 閉じるボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    Me.Close()
  End Sub
#End Region

#Region "テキストボックス"
  ''' <summary>
  ''' 仕入日付チェック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtDateBase1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtDateBase1.Validating
    ' 未入力なら本日日付を設定する
    With Me.TxtDateBase1
      If .Text.Length <= 0 Then
        .Text = ComGetProcDate()
      End If
    End With

  End Sub
#End Region

#Region "フォーム"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub SF_Edainp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.Text = Form_Edainp.PRG_TITLE & "印刷"
    Me.TxtDateBase1.Text = ComGetProcDate()
  End Sub

  ''' <summary>
  ''' ファンクションキー操作
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      ' F1キー押下時
      Case Keys.F1
        ' 印刷ボタン押下処理
        With btnPrint
          .Focus()
          .PerformClick()
        End With
      ' F12キー押下時
      Case Keys.F12
        ' 終了ボタン押下処理
        With btnClose
          .Focus()
          .PerformClick()
        End With
    End Select
  End Sub


#End Region



#End Region
End Class
