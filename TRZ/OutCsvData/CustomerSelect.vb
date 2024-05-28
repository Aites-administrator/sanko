Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.DgvForm01
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass.clsDGVColumnSetting


Public Class CustomerSelect
  Implements IDgvForm01

#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "CustSelect"
  Private Const PRG_TITLE As String = "得意先選択画面"

  ''' <summary>
  ''' テーブル名定数
  ''' </summary>
  Private Const ITEM_CODE As String = "ITEM_CODE"
  Private Const JURYO As String = "JYURYO"
  Private Const SEIZOUBI As String = "SEIZOUBI"
  Private Const CARTONID As String = "CARTONID"
  Private Const KOTAINO As String = "KOTAINO"
  Private Const EDABAN As String = "EDABAN"
  Private Const LOTNO As String = "LOTNO"
  Private Const CUTKIKAKUNO As String = "CUTKIKAKUNO"
  Public Const MOJI_CODE As String = "Shift-JIS"


#End Region
#End Region


#Region "データグリッドビュー２つの画面用操作関連共通"
  '１つ目のDataGridViewオブジェクト格納先
  Private DG2V1 As DataGridView

  Private Sub InitForm() Implements IDgvForm01.InitForm

    '入庫明細オブジェクトの設定
    DG2V1 = Me.DataGridView1

    ' グリッド初期化
    Call InitGrid(DG2V1, CreateGridSrc(), CreateGridlayout())
    ' 入庫明細設定
    With DG2V1

      '表示する
      .Visible = True

      ' グリッド動作設定
      With Controlz(.Name)
        ' 固定列設定
        .FixedRow = 0

        ' 検索コントロール設定
        '.AddSearchControl(Me.CmbMstItem1, "BICODE", typExtraction.EX_EQ, typColumnKind.CK_Text)
        '.AddSearchControl(Me.CmbDateKakouBi1, "KAKOUBI", typExtraction.EX_GTE, typColumnKind.CK_Date)
        '.AddSearchControl(Me.CmbDateKakouBi2, "KAKOUBI", typExtraction.EX_LTE, typColumnKind.CK_Date)
        '.AddSearchControl(Me.TxKotaiNo1, "CUTJ.KOTAINO", typExtraction.EX_EQ, typColumnKind.CK_Text)
        '.AddSearchControl(Me.TxtEdaban1, "CUTJ.EBCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)
        '.AddSearchControl(Me.CmbMstCustomer1, "CUTJ.TKCODE", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' 編集可能列設定
        .EditColumnList = CreateGridEditCol()

      End With
    End With
  End Sub

  Public Function CreateGridSrc() As String Implements IDgvForm01.CreateGridSrc
    Dim sql As String = String.Empty
    Dim clsCustomerSelect As New clsCustomerSelect

    sql &= clsCustomerSelect.GetSettingCustomerTblSql()

    Return sql
  End Function

  Public Function CreateGridlayout() As List(Of clsDGVColumnSetting) Implements IDgvForm01.CreateGridlayout
    Dim ret As New List(Of clsDGVColumnSetting)
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    With ret
      .Add(New clsDGVColumnSetting("グループID", "GROUP_ID", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("グループ名", "GROUP_CUSTOMER_NAME", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=220))
      .Add(New clsDGVColumnSetting("得意先コード", "CUSTOMER_CODE", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=130))
      .Add(New clsDGVColumnSetting("得意先名", "CUSTOMER_NAME", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=420))

    End With

    For Each retrow In ret
      ComWriteLog(retrow.TitleCaption & " " & retrow.DataSrc, "D:\sanko\TRZ\OutCsvData\bin\Debug\log.log")
    Next

    Return ret
  End Function

  Public Function CreateGridEditCol() As List(Of clsDataGridEditTextBox) Implements IDgvForm01.CreateGridEditCol
    Dim ret As New List(Of clsDataGridEditTextBox)

    Return ret
  End Function

#End Region

  Private Sub CustomerSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.Text = PRG_TITLE

    Call InitForm()
    Controlz(DG2V1.Name).AutoSearch = True

  End Sub

End Class