Imports System.Text.Encoding
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class FormComSocket


#Region "定数定義"

#Region "プライベート"
  Private Const PRG_ID As String = "ComSocket"
#End Region

#End Region

#Region "スタートアップ"
  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, FormComSocket)
  End Sub
#End Region


#Region "メンバ"
#Region "プライベート"

  ''' <summary>
  ''' ソケット通信制御オブジェクト
  ''' </summary>
  Private _SocketMgr As clsComSocket

#End Region
#End Region

#Region "メソッド"
#Region "プライベート"

#Region "データ送受信処理"

  ''' <summary>
  ''' データ受信後処理
  ''' </summary>
  ''' <param name="prmCh">ソケットクライアントハンドル</param>
  ''' <param name="prmBuff">受信データ</param>
  Private Sub ReadComp(prmCh As Object, prmBuff() As Byte)
    Dim tmpMsAcroData As MsAcroConnection = DirectCast(prmCh, MsAcroConnection)


    If prmBuff Is Nothing Then
      Me.lblReceiveMsg.Text = Now().ToString("yyyy/MM/dd HH:mm:ss") & "<>" & "切断しました。"
    Else
      ' 応答データ作成
      Dim tmpHeader As New MsAcroData(prmBuff)    ' 受信電文をコピー

      Select Case tmpMsAcroData.ReceivedData.GetHeaderData(MsAcroData.DataName.処理識別番号)
        Case "9501"
          '-----------------------------'
          '     モリタ屋特別仕様
          '-----------------------------'
          ' ハンディ出荷
          Dim tmpReturnText As String = String.Empty ' 応答メッセージ

          Me.lblReceiveMsg.Text = Now().ToString("yyyy/MM/dd HH:mm:ss") & "<>ハンディ出荷データ受信！"
          Try
            ' 実績更新
            tmpReturnText = UpdateDb4HT(tmpMsAcroData)
          Catch ex As Exception
            ' 例外エラー
            tmpReturnText = 3
          Finally
            prmCh.StartSend(GetEncoding("UTF-8").GetBytes(tmpReturnText)）
          End Try

        Case "1101"
          ' 通信用ｼｰｹﾝｽ番号取得
          With tmpHeader

            .SetHeaderData(MsAcroData.DataName.明細部バイト数 _
                           , (.DetailSectionLength _
                           + .GetHeaderData(MsAcroData.DataName.シーケンス番号).Length).ToString())

            .SetHeaderData(MsAcroData.DataName.総バイト数 _
                           , (.SubHeaderLength _
                           + .DetailSectionLength _
                           + .GetHeaderData(MsAcroData.DataName.シーケンス番号).Length).ToString())

            prmCh.StartSend(GetEncoding("UTF-8").GetBytes(.GetHeaderText() & .GetHeaderData(MsAcroData.DataName.シーケンス番号)))
          End With

        Case "1102"
          ' サーバー時刻取得
        Case "1501"
          ' 実績データ受信
          Me.lblReceiveMsg.Text = Now().ToString("yyyy/MM/dd HH:mm:ss") & "<>実績データ受信！"

          Try
            ' 実績更新
            Call UpdateDb(tmpMsAcroData)
          Catch ex As Exception

          Finally
            With tmpHeader
              .SetHeaderData(MsAcroData.DataName.アンサー電文要求, "0")
              .SetHeaderData(MsAcroData.DataName.機械番号, "0")
              .SetHeaderData(MsAcroData.DataName.送信レコード件数, "0")
              .SetHeaderData(MsAcroData.DataName.総バイト数, (.SubHeaderLength + .DetailSectionLength).ToString())
              .SetHeaderData(MsAcroData.DataName.明細部バイト数, (.DetailSectionLength).ToString())
            End With

            prmCh.StartSend(GetEncoding("UTF-8").GetBytes(tmpHeader.GetHeaderText()))
          End Try
        Case Is >= "1502"
          ' マスタ受信要求
          Dim tmpRecordCnt As Long = Long.MinValue    ' 送信レコード件数
          Dim tmpDataLength As Long = Long.MinValue   ' データ部バイト数
          Dim tmpSendData As String = String.Empty    ' 送信データ

          ' 送信データ取得
          tmpSendData = CreateMstData(tmpMsAcroData, tmpRecordCnt, tmpDataLength)

          With tmpHeader
            .SetHeaderData(MsAcroData.DataName.アンサー電文要求, "0")
            .SetHeaderData(MsAcroData.DataName.機械番号, "0")
            .SetHeaderData(MsAcroData.DataName.要求レコード件数, "0")
            .SetHeaderData(MsAcroData.DataName.送信レコード件数, tmpRecordCnt.ToString())
            .SetHeaderData(MsAcroData.DataName.総バイト数, (.SubHeaderLength + .DetailSectionLength + tmpDataLength).ToString())
            .SetHeaderData(MsAcroData.DataName.明細部バイト数, (.DetailSectionLength + tmpDataLength).ToString())
          End With

          'prmCh.StartSend(GetEncoding("UTF-8").GetBytes(tmpHeader.GetHeaderText() & tmpSendData))
          prmCh.StartSend(GetEncoding("SHIFT-JIS").GetBytes(tmpHeader.GetHeaderText() & tmpSendData))

          Me.lblReceiveMsg.Text = Now().ToString("yyyy/MM/dd HH:mm:ss") & "<>マスタ要求受信！"
        Case Else

      End Select

      'prmCh.StartSend(System.Text.Encoding.GetEncoding("SHIFT-JIS").GetBytes(tmpMsAcroData.ReceivedData.GetHeaderData(MsAcroData.DataName.処理識別番号)))
    End If
  End Sub

  ''' <summary>
  ''' マスタデータ作成
  ''' </summary>
  ''' <param name="prmMsAcroData">計量器よりの要求データ</param>
  ''' <param name="prmRecordCnt">取得レコード件数</param>
  ''' <param name="prmDataLength">取得データバイト数</param>
  ''' <returns>マスタデータ文字列</returns>
  ''' <remarks>DBアクセス時のエラーは呼出し元関数でキャッチします。</remarks>
  Private Function CreateMstData(prmMsAcroData As MsAcroConnection _
                                 , ByRef prmRecordCnt As Long _
                                 , ByRef prmDataLength As Long) As String
    Dim ret As String = String.Empty
    Dim tmpLastNumber As String = String.Empty  ' 最終送信コード（この番号以降のデータを送信）
    Dim tmpSendCnt As Long = Long.MinValue      ' 送信可能件数（この値以下の件数を送信）


    With prmMsAcroData.ReceivedData
      tmpLastNumber = .GetMstRequestStatus(MsAcroData.MstItemRequestStatus.部位番号)
      tmpSendCnt = .GetHeaderData(MsAcroData.DataName.要求レコード件数)

      Select Case .GetHeaderData(MsAcroData.DataName.処理識別番号)
        Case "1502"
          ' 部位マスタ
          ret = CreateBuim(tmpLastNumber, tmpSendCnt, prmRecordCnt)
        Case "1503"
          ' 得意先マスタ
          ret = CreateTokuisaki(tmpLastNumber, tmpSendCnt, prmRecordCnt)
      End Select

      ' バイト数取得
      'prmDataLength = GetEncoding("UTF-8").GetBytes(ret).Length
      prmDataLength = GetEncoding("SHIFT-JIS").GetBytes(ret).LongLength

    End With

    Return ret
  End Function

  ''' <summary>
  ''' 部位マスタ文字列作成
  ''' </summary>
  ''' <param name="prmLastNumber">最終送信コード</param>
  ''' <param name="prmSendCnt">送信可能件数</param>
  ''' <param name="prmRecordCnt">取得件数</param>
  ''' <returns>部位マスタ文字列</returns>
  ''' <remarks>
  ''' コードが[prmLastNumber]より大きいレコードを[prmSendCnt]件取得する
  ''' 取得した件数は[prmRecordCnt]に設定する（[prmSendCnt]未満になる場合もあり）
  ''' </remarks>
  Private Function CreateBuim(prmLastNumber As String _
                              , prmSendCnt As Long _
                              , ByRef prmRecordCnt As Long) As String

    Dim tmpDb As New clsSqlServer()
    Dim tmpDt As New DataTable
    Dim ret As String = String.Empty
        '    Dim tmpDataHeader As String = "制御区分:1100,部位コード:1102,部位名称:1152" & vbCrLf
        '20220404 suyama 
        'Dim tmpDataHeader As String = "1:1100,2:1102,3:1103,4:1104,5:1105,6:1107,7:1108,8:1109,9:1110,10:1112,11:1122,12:1128,13:1132,14:1135,15:1141,16:1152" & vbCrLf
        Dim tmpDataHeader As String = "1:1100,2:1102,3:1103,4:1104,5:1105,6:1107,7:1108,8:1109,9:1110,10:1112,11:1123,12:1128,13:1132,14:1135,15:1141,16:1150,17:1152" & vbCrLf


        Try
      tmpDb.GetResult(tmpDt, SqlSelMstItem(prmLastNumber, prmSendCnt))
      prmRecordCnt = tmpDt.Rows.Count

      If prmRecordCnt > 0 Then
        For Each tmpDr As DataRow In tmpDt.Rows
          If tmpDr("KUBUN").ToString().Equals("0") Then
            ret &= "1,"
          Else
            ' 使用停止もしくは削除
            ret &= "9,"
          End If
          ret &= tmpDr("BICODE").ToString() & ","   ' 呼出コード

                    ',3:1103  ' 単価
                    ',4:1104  ’定重量
                    ',5:1105  ’入り本数
                    ',6:1107  ’牛豚区分
                    ',7:1108  ’上限重量
                    ',8:1109  ’下限重量

                    ',9:1110  ’保存温度番号

                    ',10:1112 ’風袋重量０
                    ',11:1122 ’商品コード１⇒1123　コード2
                    ',12:1128 ’加工日印字有無
                    ',13:1132 ’賞味期限印字有無
                    ',14:1135 ’賞味日数
                    ',15:1141 ’ブランド番号
                    '20220404 suyama　↓
                    'ret &= "0,0,1,2,0,0,1,10,91010,1,1,35,0,"
                    ret &= "0,0,1,2,0,0," '上下限
                    ret &= tmpDr("ONDO").ToString() & ","  '温度番号
                    ret &= "0," '風袋重量
                    ret &= tmpDr("HINCD").ToString() & "," '商品コード２（ミートエース品番）
                    ret &= "1,1,"   '加工日印字,賞味期限印字　あり 
                    ret &= tmpDr("KIGEN").ToString() & "," '期限日数
                    ret &= "0,"   'ブランド
                    ret &= tmpDr("FMT").ToString() & "," 'フォーマット1
                    '20220404 suyama　↑
                    '        ret &= tmpDr("HIN1").ToString() & vbLf   ' 商品名
                    ret &= tmpDr("HINBAN").ToString().Replace(","c, "")  ' 商品名
                    ret &= vbCrLf
        Next
        ret = ret.Substring(0, ret.Length - 2)
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("部位マスタの作成に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

    Return tmpDataHeader & ret
  End Function

  ''' <summary>
  ''' 得意先マスタ作成
  ''' </summary>
  ''' <param name="prmLastNumber">最終送信コード</param>
  ''' <param name="prmSendCnt">送信可能件数</param>
  ''' <param name="prmRecordCnt">取得件数</param>
  ''' <returns>得意先マスタ文字列</returns>
  ''' <remarks>
  ''' コードが[prmLastNumber]より大きいレコードを[prmSendCnt]件取得する
  ''' 取得した件数は[prmRecordCnt]に設定する（[prmSendCnt]未満になる場合もあり）
  ''' </remarks>
  Private Function CreateTokuisaki(prmLastNumber As String _
                              , prmSendCnt As Long _
                              , ByRef prmRecordCnt As Long) As String

    Dim tmpDb As New clsSqlServer()
    Dim tmpDt As New DataTable
    Dim ret As String = String.Empty
    Dim tmpDataHeader As String = "制御区分:1200,得意先番号:1202,得意先名称:1220" & vbCrLf

    Try
      tmpDb.GetResult(tmpDt, SqlSelMstCustomer(prmLastNumber, prmSendCnt))
      prmRecordCnt = tmpDt.Rows.Count

      If prmRecordCnt > 0 Then
        For Each tmpDr As DataRow In tmpDt.Rows
          If tmpDr("KUBUN").ToString().Equals("0") Then
            ret &= "1,"
          Else
            ' 使用停止もしくは削除
            ret &= "9,"
          End If
          ret &= tmpDr("TKCODE").ToString() & ","
          ret &= DelHonor(tmpDr("TNAME").ToString())
          ret &= vbCrLf
        Next
        ret = ret.Substring(0, ret.Length - 1)
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("得意先マスタの作成に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

    Return tmpDataHeader & ret
  End Function

  ''' <summary>
  ''' 敬称削除
  ''' </summary>
  ''' <param name="prmCustomerName">得意先名称</param>
  ''' <returns>敬称を削除した得意先名称</returns>
  ''' <remarks>
  ''' 指定された得意先名称の終端より"様"を削除する
  ''' </remarks>
  Private Function DelHonor(prmCustomerName As String) As String
    Dim ret As String = prmCustomerName
    Dim tmpHonor As String = "様"

    If Not String.IsNullOrEmpty(ret) _
       AndAlso Strings.Right(ret, 1).Equals(tmpHonor) Then
      ret = ret.Substring(0, ret.Length - 1)
    End If

    Return ret
  End Function
#End Region

#Region "DB更新"

  ''' <summary>
  ''' 計量実績更新（HTデータより）
  ''' </summary>
  ''' <param name="prmMsAcroData">計量実績（HT送信データ）</param>
  ''' <remarks>
  ''' ハンディターミナルでの原料出荷処理データを計量データとして保存する
  ''' </remarks>
  Private Function UpdateDb4HT(prmMsAcroData As MsAcroConnection) As Integer
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim ret As Integer = 0

    Try
      ' 原料在庫確認
      tmpDb.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelTrnStock(prmMsAcroData), " ITEM_STATUS = 0 "))

      If tmpDt.Rows.Count <= 0 Then
        tmpDt.Clear()
        tmpDb.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelTrnStock(prmMsAcroData), " ITEM_STATUS = 1 "))
        If tmpDt.Rows.Count <= 0 Then
          ' 原料在庫無し
          ret = 1
        Else
          ' 出庫済
          ret = 2
        End If

        GoTo Exit_Fnc
      End If

      tmpDb.TrnStart()

      ' 計量実績更新
      If tmpDb.Execute(SqlInsTRN_WEIGHING_RESULTS4HT(prmMsAcroData, tmpDt.Rows(0))) <> 1 Then
        Throw New Exception("出荷データの更新に失敗しました。")
      End If

      ' 原料在庫更新
      If tmpDb.Execute(SqlUpdTrnStock(tmpDt.Rows(0))) <> 1 Then
        Throw New Exception("原料在庫の更新に失敗しました。")
      End If

      tmpDb.TrnCommit()
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
      Throw New Exception("原料出荷に失敗しました。")
    Finally
      tmpDb.Dispose()
    End Try

Exit_Fnc:

    Return ret
  End Function

  ''' <summary>
  ''' 計量実績更新
  ''' </summary>
  ''' <param name="prmMsAcroData"></param>
  Private Sub UpdateDb(prmMsAcroData As MsAcroConnection)
    Dim tmpDb As New clsSqlServer

    Try
      ' 加減算フラグ=1を直前取り消しと見做す
      If prmMsAcroData.ReceivedData.GetWeighingResults(MsAcroData.WeighingResults.加減算フラグ) = "1" Then
        ' 直前取り消し
        tmpDb.Execute(SqlUpdTRN_WEIGHING_RESULTS(prmMsAcroData))
      Else
        ' 通常実績
        tmpDb.Execute(SqlInsTRN_WEIGHING_RESULTS(prmMsAcroData))
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("計量実績の保存に失敗しました。")
    Finally
      tmpDb.Dispose()
    End Try

  End Sub

#End Region

#Region "SQL文作成関連"

  ''' <summary>
  ''' 計量器用部位マスタ取得SQL文の作成
  ''' </summary>
  ''' <param name="prmLastNumber">最終コード（これよりコードが大きいデータを取得）</param>
  ''' <param name="prmSendCnt">最大取得件数</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelMstItem(prmLastNumber As String _
                                  , prmSendCnt As Long) As String

    Dim sql As String = String.Empty

        '20220404 suyama
        sql = " SELECT TOP(" & prmSendCnt & ") "
        sql &= " KUBUN AS KUBUN "
        sql &= " ,BICODE AS BICODE "
        sql &= " ,isnull(TEMP_TYPE,2) AS ONDO "
        sql &= " ,ITEM_CODE AS HINCD "
        sql &= " ,isnull(BESTBEFORE_DAYS,3) AS KIGEN " 
        sql &= " ,isnull(FORMAT_NO,67) AS FMT "
        sql &= " ,CONCAT([LBNAME1],'<BR/>',left([LBNAME2],18)) AS hinban "
        'sql &= "       ,TEMP_TYPE AS ONDO "
        'sql &= "       ,ITEM_CODE AS HINCD "
        'sql &= "       ,BESTBEFORE_DAYS AS KIGEN "
        'sql &= "       ,FORMAT_NO AS FMT "
        'sql &= "       ,CONCAT([LBNAME1],'<BR/>',left([LBNAME2],18)) AS hinban  "
        sql &= " FROM MST_ITEM "
        '20220727 追加
        sql &= " INNER Join SHENKAN ON MST_ITEM.ITEM_CODE = SHENKAN.SCODE "
        sql &= " WHERE BICODE > " & prmLastNumber
        sql &= " AND ITEM_CODE > 1000000 "
        sql &= " And Not(ITEM_CODE between 82000000 And 82999999 ) "
        sql &= " And KUBUN <> -1 "
        sql &= " ORDER BY BICODE "

        'sql &= " WHERE BICODE > " & prmLastNumber
        'sql &= "       And BICODE <1000000 "
        'sql &= " ORDER BY BICODE "

        'sql &= " Select TOP(" & prmSendCnt & ")  "
        'sql &= "         MST_ITEM.KUBUN "
        'sql &= "       , MST_ITEM.BICODE "
        'sql &= "       , SHENKAN.HINMEI "
        'sql &= " FROM MST_ITEM "
        'sql &= "  INNER JOIN SHENKAN On MST_ITEM.ITEM_CODE = SHENKAN.SCODE "
        'sql &= " WHERE BICODE > " & prmLastNumber
        'sql &= " ORDER BY BICODE "

        Return sql

  End Function

  ''' <summary>
  ''' 計量器用得意先マスタ取得SQL文の作成
  ''' </summary>
  ''' <param name="prmLastNumber">最終コード（これよりコードが大きいデータを取得）</param>
  ''' <param name="prmSendCnt">最大取得件数</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelMstCustomer(prmLastNumber As String _
                                  , prmSendCnt As Long) As String
    Dim sql As String = String.Empty

        '20220405 suyama
        sql &= " Select TOP(" & prmSendCnt & ") "
        sql &= "    A.CUSTOMER_CODE As TKCODE  "
        sql &= "    , CONCAT(isnull(LEFT(A.TKNAME1,25), C.TNAME) ,'<BR/>',LEFT(A.TKNAME2,10)) AS TNAME "
        sql &= "    , A.KUBUN  "
        sql &= " FROM MST_CUSTOMER A "
        sql &= "    INNER JOIN THENKAN B ON CAST(A.CUSTOMER_CODE as numeric) = B.TKCODE "
        sql &= "    INNER JOIN TOKUISAKI C on CAST(A.CUSTOMER_CODE as numeric) = C.TKCODE "
        sql &= " WHERE A.CUSTOMER_CODE > " & prmLastNumber
        sql &= " ORDER BY A.CUSTOMER_CODE  "


        'sql &= "  SELECT TOKUISAKI.TKCODE "
        'sql &= "       , TOKUISAKI.TNAME  "
        'sql &= "       , MST_CUSTOMER.KUBUN "
        'sql &= "  FROM MST_CUSTOMER "
        'sql &= "       INNER JOIN TOKUISAKI ON CAST(MST_CUSTOMER.CUSTOMER_CODE as numeric) = TOKUISAKI.TKCODE "
        'sql &= "       LEFT JOIN MST_TANTO ON MST_CUSTOMER.TANTO_CODE = MST_TANTO.TANTO_CODE "

        'sql &= "  UNION "

        '    sql &= "  SELECT THENKAN.HENKAN AS TKCODE  "

        '    sql &= "  SELECT MST_CUSTOMER.CUSTOMER_CODE AS TKCODE  "
        '    sql &= "       , TOKUISAKI.TNAME  "
        'sql &= "       , MST_CUSTOMER.KUBUN "
        'sql &= "  FROM MST_CUSTOMER "
        'sql &= "       INNER JOIN THENKAN ON CAST(MST_CUSTOMER.CUSTOMER_CODE as numeric) = THENKAN.TKCODE "
        'sql &= "       INNER JOIN TOKUISAKI ON THENKAN.TKCODE = TOKUISAKI.TKCODE "
        'sql &= " ) AS CUSTMST "
        'sql &= " WHERE TKCODE > " & prmLastNumber
        'sql &= " ORDER BY TKCODE "

        Return sql

  End Function

  ''' <summary>
  ''' 在庫一覧更新SQL文
  ''' </summary>
  ''' <param name="prmDr">更新条件</param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' HT原料出荷時に出荷した原料を在庫一覧から削除（フラグ変更）する
  ''' </remarks>
  Private Function SqlUpdTrnStock(prmDr As DataRow) As String
    Dim sql As String = String.Empty

    sql &= " UPDATE TRN_STOCK "
    sql &= " SET ITEM_STATUS = 1 "
    sql &= "   , LASTUPDATE = ' " & ComGetProcTime() & "'"
    sql &= " WHERE IN_STOCK_DATE = '" & Date.Parse(prmDr("IN_STOCK_DATE").ToString()).ToString("yyyy/MM/dd") & "'"
    sql &= " AND SUPPLIER_CODE = " & prmDr("SUPPLIER_CODE").ToString()
    sql &= " AND EDABAN = " & prmDr("EDABAN").ToString()
    sql &= " AND PROCESSING_DATE = '" & Date.Parse(prmDr("PROCESSING_DATE").ToString()).ToString("yyyy/MM/dd") & "'"
    sql &= " AND BUICODE = " & prmDr("BUICODE").ToString()
    sql &= " AND KOTAINO = " & prmDr("KOTAINO").ToString()
    sql &= " AND ITEM_WEIGHT = " & prmDr("ITEM_WEIGHT").ToString()
    sql &= " AND CARTON_NUMBER = " & prmDr("CARTON_NUMBER").ToString()
    sql &= " AND SIDE_TYPE = " & prmDr("SIDE_TYPE").ToString()
    sql &= " AND ENTRY_DATE = '" & Date.Parse(prmDr("ENTRY_DATE").ToString()).ToString("yyyy/MM/dd HH:mm:ss.fff") & "'"

    Return sql
  End Function

  ''' <summary>
  ''' 原料在庫取得SQL文作成
  ''' </summary>
  ''' <param name="prmMsAcroData">計量実績データ(HTデータ)</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelTrnStock(prmMsAcroData As MsAcroConnection) As String
    Dim sql As String = String.Empty
    Dim tmpDate As String = String.Empty


    With prmMsAcroData.ReceivedData
      tmpDate = .GetWeighingResults(MsAcroData.WeighingResults.加工日)

      sql &= " SELECT * "
      sql &= " FROM TRN_STOCK "
      sql &= " WHERE PROCESSING_DATE = '" & Date.Parse(tmpDate.Substring(0, 2) _
                                                    & "/" & tmpDate.Substring(2, 2) _
                                                    & "/" & tmpDate.Substring(4, 2)).ToString("yyyy/MM/dd") & "'"
      sql &= "   AND SHOHINC         = " & .GetWeighingResults(MsAcroData.WeighingResults.商品コード)
      sql &= "   AND KOTAINO         = " & .GetWeighingResults(MsAcroData.WeighingResults.個体識別番号)
      sql &= "   AND CARTON_NUMBER   = " & .GetWeighingResults(MsAcroData.WeighingResults.通し番号)
      sql &= "   AND ITEM_WEIGHT     = " & .GetWeighingResults(MsAcroData.WeighingResults.重量) & "0"
    End With

    Return sql
  End Function

  ''' <summary>
  ''' 計量実績テーブル更新SQL文の作成
  ''' </summary>
  ''' <param name="prmMsAcroData">計量実績データ（HTデータ）</param>
  ''' <param name="prmDr">原材料データ</param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>ハンディ原料出荷用</remarks>
  Private Function SqlInsTRN_WEIGHING_RESULTS4HT(prmMsAcroData As MsAcroConnection _
                                                , prmDr As DataRow) As String
    Dim sql As String = String.Empty
    Dim tmpKeyValue As New Dictionary(Of String, String)
    Dim tmpEntryDate As String = ComGetProcTime()
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty
    Dim tmpLotNumber As String = ""
    Dim tmpOrderNumber As String = ""
    Dim tmpKotaiNo As Long = Long.MinValue

    With prmMsAcroData.ReceivedData
      '--------------------------
      '   HT送信データより設定
      '--------------------------
      tmpLotNumber = .GetWeighingResults(MsAcroData.WeighingResults.ロット番号)
      tmpKeyValue.Add("ORDER_NUMBER", tmpLotNumber.Substring(0, 7))
      tmpKeyValue.Add("ORDER_SUB_NUMBER", tmpLotNumber.Substring(7, 3))
      If Long.TryParse(.GetWeighingResults(MsAcroData.WeighingResults.個体識別番号), tmpKotaiNo) Then
        tmpKeyValue.Add("KOTAINO", tmpKotaiNo.ToString())
      Else
        tmpKeyValue.Add("KOTAINO", "0")
      End If
      tmpKeyValue.Add("CARTON_NUMBER", .GetWeighingResults(MsAcroData.WeighingResults.通し番号))
      tmpKeyValue.Add("WEIGHING_VALUE", G2Kg(.GetWeighingResults(MsAcroData.WeighingResults.重量), 100))

      tmpKeyValue.Add("MODIFIED_WEIGHING", G2Kg(.GetWeighingResults(MsAcroData.WeighingResults.重量), 100))
      tmpKeyValue.Add("IN_COUNT", .GetWeighingResults(MsAcroData.WeighingResults.入本数))


      '--------------------------
      '   在庫データより設定
      '--------------------------
      tmpKeyValue.Add("ORIGIN_PLACE_CODE", prmDr("ORIGIN_PLACE_CODE").ToString())
      tmpKeyValue.Add("KIND_CODE", prmDr("KIND_CODE").ToString())
      tmpKeyValue.Add("EDABAN", prmDr("EDABAN").ToString())
      tmpKeyValue.Add("SAYU", prmDr("SIDE_TYPE").ToString())
      tmpKeyValue.Add("BUICODE", prmDr("BUICODE").ToString())

      '--------------------------
      '         固定値
      '--------------------------
      tmpKeyValue.Add("MACHINE_NUMBER", "999")
      tmpKeyValue.Add("SEQUENCE_NUMBER", "0")

      '--------------------------
      '         SystemDate
      '--------------------------
      tmpKeyValue.Add("ENTRY_DATE", "'" & tmpEntryDate & "'")
      tmpKeyValue.Add("LASTUPDATE", "'" & tmpEntryDate & "'")

    End With

    For Each tmpKey As String In tmpKeyValue.Keys
      tmpDst &= tmpKey & ","
      tmpVal &= tmpKeyValue(tmpKey) & ","
    Next

    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= " INSERT INTO TRN_WEIGHING_RESULTS( " & tmpDst & ")"
    sql &= "             VALUES(" & tmpVal & ")"

    Return sql


    Return sql
  End Function

  ''' <summary>
  ''' 計量実績テーブル更新SQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlInsTRN_WEIGHING_RESULTS(prmMsAcroData As MsAcroConnection) As String
    Dim sql As String = String.Empty
    Dim tmpKeyValue As New Dictionary(Of String, String)
    Dim tmpEntryDate As String = ComGetProcTime()
    Dim tmpDst As String = String.Empty
    Dim tmpVal As String = String.Empty
    Dim tmpLotNumber As String = ""
    Dim tmpOrderNumber As String = ""
    Dim tmpKotaiNo As Long = Long.MinValue

    With prmMsAcroData.ReceivedData
      tmpLotNumber = .GetWeighingResults(MsAcroData.WeighingResults.ロット番号)
      tmpKeyValue.Add("ORDER_NUMBER", tmpLotNumber.Substring(0, 7))
      tmpKeyValue.Add("ORDER_SUB_NUMBER", tmpLotNumber.Substring(7, 3))

      tmpKeyValue.Add("MACHINE_NUMBER", .GetWeighingResults(MsAcroData.WeighingResults.機械番号))

      If Long.TryParse(.GetWeighingResults(MsAcroData.WeighingResults.個体識別番号), tmpKotaiNo) Then
        tmpKeyValue.Add("KOTAINO", tmpKotaiNo.ToString())
      Else
        tmpKeyValue.Add("KOTAINO", "0")
      End If

      tmpKeyValue.Add("EDABAN", .GetWeighingResults(MsAcroData.WeighingResults.枝肉番号))
      tmpKeyValue.Add("SAYU", .GetWeighingResults(MsAcroData.WeighingResults.左右))

      tmpKeyValue.Add("CARTON_NUMBER", .GetWeighingResults(MsAcroData.WeighingResults.通し番号))
      tmpKeyValue.Add("SEQUENCE_NUMBER", .GetWeighingResults(MsAcroData.WeighingResults.シーケンス番号))
      tmpKeyValue.Add("BUICODE", .GetWeighingResults(MsAcroData.WeighingResults.部位番号))

      tmpKeyValue.Add("WEIGHING_VALUE", G2Kg(.GetWeighingResults(MsAcroData.WeighingResults.重量) _
                                              , GetCoefficient(prmMsAcroData.ReceivedData)))

      tmpKeyValue.Add("MODIFIED_WEIGHING", G2Kg(.GetWeighingResults(MsAcroData.WeighingResults.重量) _
                                              , GetCoefficient(prmMsAcroData.ReceivedData)))

      tmpKeyValue.Add("ORIGIN_PLACE_CODE", .GetWeighingResults(MsAcroData.WeighingResults.原産地番号))
      tmpKeyValue.Add("KIND_CODE", .GetWeighingResults(MsAcroData.WeighingResults.品種番号))
      tmpKeyValue.Add("IN_COUNT", .GetWeighingResults(MsAcroData.WeighingResults.入本数))

      tmpKeyValue.Add("ENTRY_DATE", "'" & tmpEntryDate & "'")
      tmpKeyValue.Add("LASTUPDATE", "'" & tmpEntryDate & "'")
    End With

    For Each tmpKey As String In tmpKeyValue.Keys
      tmpDst &= tmpKey & ","
      tmpVal &= tmpKeyValue(tmpKey) & ","
    Next

    tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    tmpVal = tmpVal.Substring(0, tmpVal.Length - 1)

    sql &= " INSERT INTO TRN_WEIGHING_RESULTS( " & tmpDst & ")"
    sql &= "             VALUES(" & tmpVal & ")"

    Return sql
  End Function

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlUpdTRN_WEIGHING_RESULTS(prmMsAcroData As MsAcroConnection) As String
    Dim sql As String = String.Empty
    Dim tmpLotNumber As String = ""
    Dim tmpKotaiNo As Long = Long.MinValue

    sql &= " UPDATE TRN_WEIGHING_RESULTS "
    sql &= " SET KUBUN = 9 "
    sql &= "    ,LASTUPDATE = '" & ComGetProcTime() & "'"

    With prmMsAcroData.ReceivedData
      tmpLotNumber = .GetWeighingResults(MsAcroData.WeighingResults.ロット番号)

      sql &= " WHERE ORDER_NUMBER = " & tmpLotNumber.Substring(0, 7)
      sql &= "   AND ORDER_SUB_NUMBER = " & tmpLotNumber.Substring(7, 3)
      sql &= "   AND MACHINE_NUMBER = " & .GetWeighingResults(MsAcroData.WeighingResults.機械番号)
      sql &= "   AND CARTON_NUMBER = " & .GetWeighingResults(MsAcroData.WeighingResults.通し番号)
      sql &= "   AND SEQUENCE_NUMBER = " & .GetWeighingResults(MsAcroData.WeighingResults.シーケンス番号)
      sql &= "   AND BUICODE = " & .GetWeighingResults(MsAcroData.WeighingResults.部位番号)
      sql &= "   AND WEIGHING_VALUE = " & G2Kg(.GetWeighingResults(MsAcroData.WeighingResults.重量) _
                                                                 , GetCoefficient(prmMsAcroData.ReceivedData))
      sql &= "   AND MODIFIED_WEIGHING = " & G2Kg(.GetWeighingResults(MsAcroData.WeighingResults.重量) _
                                                                 , GetCoefficient(prmMsAcroData.ReceivedData))
      sql &= "   AND IN_COUNT = " & .GetWeighingResults(MsAcroData.WeighingResults.入本数)

      If Long.TryParse(.GetWeighingResults(MsAcroData.WeighingResults.個体識別番号), tmpKotaiNo) Then
        sql &= "   AND KOTAINO = " & .GetWeighingResults(MsAcroData.WeighingResults.個体識別番号)
      Else
        sql &= "   AND KOTAINO = 0 "
      End If
    End With

    Return sql
  End Function

#End Region

#Region ""

  ''' <summary>
  ''' 計量重量の係数を取得する
  ''' </summary>
  ''' <param name="prmMsAcroData">計量データ</param>
  ''' <returns>
  ''' 計量重量をg単位に変換する際の係数
  ''' </returns>
  Private Function GetCoefficient(prmMsAcroData As MsAcroData) As Long
    Dim ret As Long = 1000

    If prmMsAcroData.GetWeighingResults(MsAcroData.WeighingResults.機械番号).Length >= 2 Then
      ' 計量器番号が2桁以上の場合は大容量の計量器とみなし、10g単位で重量が送信されてくると判断
      ' ※ 1キロ計量された場合は重量として100が送信されてくる
      ret = 100
    End If

    Return ret
  End Function

  ''' <summary>
  ''' グラム文字列をKg文字列に変換する
  ''' </summary>
  ''' <param name="prmGText"></param>
  ''' <returns></returns>
  Private Function G2Kg(prmGText As String _
                      , Optional prmRate As Long = 1000) As String
    Dim ret As String = String.Empty
    Dim tmpGValue As Decimal = Decimal.MinValue

    If Decimal.TryParse(prmGText, tmpGValue) Then
      ret = (tmpGValue / prmRate).ToString
    End If

    Return ret
  End Function

#End Region
#End Region
#End Region

#Region "イベントプロシージャー"

#Region "フォーム関連"
  ''' <summary>
  ''' フォーム初期起動時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
    ' ソケット通信開始
    _SocketMgr = New clsComSocket
    _SocketMgr.lcDataReceive = AddressOf ReadComp
    _SocketMgr.StartSocket()

  End Sub

  ''' <summary>
  ''' フォーム終了時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    Me._SocketMgr.Dispose()
  End Sub
#End Region

#End Region

End Class
