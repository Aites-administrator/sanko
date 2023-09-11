Imports CommonPcaDx
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form1

  Private _PcaSYK As New clsPcaSYK("aites", "495344", "108", "pcaSam.exe", "P20V01C001KON0001")

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    Me.Close()
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    Try
      Call PostUriageDenpyo()
    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
    End Try
  End Sub

  Private Sub PostUriageDenpyo()

    ' ヘッダー作成
    With _PcaSYK.Header
      .売上日 = "20210101"
      .請求日 = "20210101"
      .伝票No = "0"
      .伝区 = "0"
      .得意先コード = "12"
    End With

    ' 明細追加
    _PcaSYK.AddDetail(CreateUriageDetail("1", "1"))
    _PcaSYK.AddDetail(CreateUriageDetail("2", "2"))
    _PcaSYK.AddDetail(CreateUriageDetail("3", "33"))

    ' 伝票登録
    Try
      _PcaSYK.Create()
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("売上伝票の作成に失敗しました")
    End Try

  End Sub

  Private Function CreateUriageDetail(prmItemCode As String, prmCount As String) As clsPcaSYKD
    Dim ret As New clsPcaSYKD

    With ret
      .商品コード = prmItemCode
      .数量 = prmCount
    End With

    Return ret
  End Function
End Class
