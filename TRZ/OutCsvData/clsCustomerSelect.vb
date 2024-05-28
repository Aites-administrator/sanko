Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class clsCustomerSelect

  Public Function GetSettingCustomerTbl(prmControl As ComboBox) As DataTable
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    Try
      tmpDb.GetResult(tmpDt, GetSettingCustomerTblSql(prmControl))

      If tmpDt.Rows.Count = 0 Then
        Throw New Exception("得意先設定テーブルが取得できませんでした。")
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
    End Try
    Return tmpDt
  End Function


  ''' <summary>
  ''' 項目選択テーブル取得SQL
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   CSV出力用
  ''' </remarks>
  Public Function GetSettingCustomerTblSql(Optional prmControl As ComboBox = Nothing) As String
    Dim sql As String = String.Empty

    sql &= " SELECT "
    sql &= " GROUP_ID " ' グループID
    sql &= " ,	 GROUP_CUSTOMER_NAME " 'グループ得意先名
    sql &= " ,	 MST_CUSTOMER_GROUP.TKCODE AS CUSTOMER_CODE" '得意先コード
    sql &= " ,	 TOKUISAKI.LTKNAME AS CUSTOMER_NAME " ' 得意先名
    sql &= " FROM	MST_CUSTOMER_GROUP "
    sql &= " LEFT JOIN TOKUISAKI "
    sql &= " ON	TOKUISAKI.TKCODE = MST_CUSTOMER_GROUP.TKCODE "
    If Not prmControl Is Nothing _
      AndAlso Not String.IsNullOrWhiteSpace(prmControl.SelectedText) Then
      sql &= " WHERE GROUP_ID = " & prmControl.SelectedText
    End If
    sql &= " ORDER BY GROUP_ID,MST_CUSTOMER_GROUP.TKCODE "

    Console.WriteLine(sql)

    Return sql
  End Function



End Class
