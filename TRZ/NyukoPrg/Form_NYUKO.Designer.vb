<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_NYUKO
  Inherits T.R.ZCommonCtrl.MFBaseDgv

  'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
  <System.Diagnostics.DebuggerNonUserCode()>
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Windows フォーム デザイナーで必要です。
  Private components As System.ComponentModel.IContainer

  'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
  'Windows フォーム デザイナーを使用して変更できます。  
  'コード エディターを使って変更しないでください。
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_NYUKO))
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.btnRegist = New System.Windows.Forms.Button()
    Me.btnDspStockList = New System.Windows.Forms.Button()
    Me.btnPrint = New System.Windows.Forms.Button()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.Label_GridData = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.CmbMstCustomer1 = New T.R.ZCommonCtrl.CmbMstShiresaki()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.TxtBuiCode1 = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.CmbMstKikaku1 = New T.R.ZCommonCtrl.CmbMstKikaku()
    Me.TxtLblMstItem1 = New T.R.ZCommonCtrl.TxtLblMstItem()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.TxtUnitPrice = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.TxtSyukoCount = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.TxtWeitghtKg1 = New T.R.ZCommonCtrl.TxtWeitghtKg()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.TxtCartonNumber1 = New T.R.ZCommonCtrl.TxtCartonNumber()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.TxtKakouDate = New T.R.ZCommonCtrl.TxtDateBase()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.CmbMstRating1 = New T.R.ZCommonCtrl.CmbMstRating()
    Me.CmbMstOriginPlace1 = New T.R.ZCommonCtrl.CmbMstOriginPlace()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.TxtKotaiNo1 = New T.R.ZCommonCtrl.TxKotaiNo()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.TxtEdaban1 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.CmbMstCattle1 = New T.R.ZCommonCtrl.CmbMstCattle()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.CmbMstStaff1 = New T.R.ZCommonCtrl.CmbMstStaff()
    Me.TxtNyukoDate = New T.R.ZCommonCtrl.TxtDateBase()
    Me.btnDelete = New System.Windows.Forms.Button()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(13, 252)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.Size = New System.Drawing.Size(1510, 607)
    Me.DataGridView1.TabIndex = 1
    '
    'btnRegist
    '
    Me.btnRegist.Location = New System.Drawing.Point(780, 870)
    Me.btnRegist.Name = "btnRegist"
    Me.btnRegist.Size = New System.Drawing.Size(140, 36)
    Me.btnRegist.TabIndex = 3
    Me.btnRegist.Text = "F1：登録"
    Me.btnRegist.UseVisualStyleBackColor = True
    '
    'btnDspStockList
    '
    Me.btnDspStockList.Location = New System.Drawing.Point(930, 870)
    Me.btnDspStockList.Name = "btnDspStockList"
    Me.btnDspStockList.Size = New System.Drawing.Size(140, 36)
    Me.btnDspStockList.TabIndex = 7
    Me.btnDspStockList.Text = "F2：返品"
    Me.btnDspStockList.UseVisualStyleBackColor = True
    '
    'btnPrint
    '
    Me.btnPrint.Location = New System.Drawing.Point(1230, 870)
    Me.btnPrint.Name = "btnPrint"
    Me.btnPrint.Size = New System.Drawing.Size(140, 36)
    Me.btnPrint.TabIndex = 5
    Me.btnPrint.Text = "F9：印刷"
    Me.btnPrint.UseVisualStyleBackColor = True
    '
    'btnClose
    '
    Me.btnClose.Location = New System.Drawing.Point(1380, 870)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(140, 36)
    Me.btnClose.TabIndex = 6
    Me.btnClose.Text = "F12：閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'Label_GridData
    '
    Me.Label_GridData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.Label_GridData.Location = New System.Drawing.Point(13, 226)
    Me.Label_GridData.Name = "Label_GridData"
    Me.Label_GridData.Size = New System.Drawing.Size(1511, 26)
    Me.Label_GridData.TabIndex = 2
    Me.Label_GridData.Text = "Label16"
    Me.Label_GridData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblInformation
    '
    Me.lblInformation.AutoSize = True
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Red
    Me.lblInformation.Location = New System.Drawing.Point(13, 880)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(294, 19)
    Me.lblInformation.TabIndex = 39
    Me.lblInformation.Text = "ここに入力時の説明文が表示されます"
    '
    'GroupBox1
    '
    Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.GroupBox1.Controls.Add(Me.CmbMstCustomer1)
    Me.GroupBox1.Controls.Add(Me.Label17)
    Me.GroupBox1.Controls.Add(Me.TxtBuiCode1)
    Me.GroupBox1.Controls.Add(Me.Label16)
    Me.GroupBox1.Controls.Add(Me.CmbMstKikaku1)
    Me.GroupBox1.Controls.Add(Me.TxtLblMstItem1)
    Me.GroupBox1.Controls.Add(Me.Label14)
    Me.GroupBox1.Controls.Add(Me.TxtUnitPrice)
    Me.GroupBox1.Controls.Add(Me.Label13)
    Me.GroupBox1.Controls.Add(Me.TxtSyukoCount)
    Me.GroupBox1.Controls.Add(Me.Label12)
    Me.GroupBox1.Controls.Add(Me.TxtWeitghtKg1)
    Me.GroupBox1.Controls.Add(Me.Label11)
    Me.GroupBox1.Controls.Add(Me.TxtCartonNumber1)
    Me.GroupBox1.Controls.Add(Me.Label10)
    Me.GroupBox1.Controls.Add(Me.Label9)
    Me.GroupBox1.Controls.Add(Me.TxtKakouDate)
    Me.GroupBox1.Controls.Add(Me.Label8)
    Me.GroupBox1.Controls.Add(Me.Label7)
    Me.GroupBox1.Controls.Add(Me.CmbMstRating1)
    Me.GroupBox1.Controls.Add(Me.CmbMstOriginPlace1)
    Me.GroupBox1.Controls.Add(Me.Label6)
    Me.GroupBox1.Controls.Add(Me.TxtKotaiNo1)
    Me.GroupBox1.Controls.Add(Me.Label5)
    Me.GroupBox1.Controls.Add(Me.TxtEdaban1)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Controls.Add(Me.CmbMstCattle1)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.CmbMstStaff1)
    Me.GroupBox1.Controls.Add(Me.TxtNyukoDate)
    Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1530, 219)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "カット肉入庫入力"
    '
    'CmbMstCustomer1
    '
    Me.CmbMstCustomer1.AvailableBlank = False
    Me.CmbMstCustomer1.DisplayMember = "ItemName"
    Me.CmbMstCustomer1.FormattingEnabled = True
    Me.CmbMstCustomer1.Location = New System.Drawing.Point(470, 48)
    Me.CmbMstCustomer1.Name = "CmbMstCustomer1"
    Me.CmbMstCustomer1.Size = New System.Drawing.Size(472, 29)
    Me.CmbMstCustomer1.TabIndex = 1
    Me.CmbMstCustomer1.ValueMember = "ItemCode"
    '
    'Label17
    '
    Me.Label17.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label17.Location = New System.Drawing.Point(1220, 131)
    Me.Label17.Name = "Label17"
    Me.Label17.Size = New System.Drawing.Size(120, 21)
    Me.Label17.TabIndex = 68
    Me.Label17.Text = "部位コード"
    Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.Label17.Visible = False
    '
    'TxtBuiCode1
    '
    Me.TxtBuiCode1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtBuiCode1.Location = New System.Drawing.Point(1352, 128)
    Me.TxtBuiCode1.Name = "TxtBuiCode1"
    Me.TxtBuiCode1.ReadOnly = True
    Me.TxtBuiCode1.Size = New System.Drawing.Size(123, 28)
    Me.TxtBuiCode1.TabIndex = 10
    Me.TxtBuiCode1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.TxtBuiCode1.Visible = False
    '
    'Label16
    '
    Me.Label16.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label16.Location = New System.Drawing.Point(740, 90)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(130, 21)
    Me.Label16.TabIndex = 64
    Me.Label16.Text = "規格"
    Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'CmbMstKikaku1
    '
    Me.CmbMstKikaku1.AvailableBlank = False
    Me.CmbMstKikaku1.DisplayMember = "ItemName"
    Me.CmbMstKikaku1.FormattingEnabled = True
    Me.CmbMstKikaku1.Location = New System.Drawing.Point(874, 88)
    Me.CmbMstKikaku1.Name = "CmbMstKikaku1"
    Me.CmbMstKikaku1.Size = New System.Drawing.Size(280, 29)
    Me.CmbMstKikaku1.TabIndex = 5
    Me.CmbMstKikaku1.ValueMember = "ItemCode"
    '
    'TxtLblMstItem1
    '
    Me.TxtLblMstItem1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.TxtLblMstItem1.CodeTxt = ""
    Me.TxtLblMstItem1.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstItem1.Location = New System.Drawing.Point(874, 128)
    Me.TxtLblMstItem1.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
    Me.TxtLblMstItem1.Name = "TxtLblMstItem1"
    Me.TxtLblMstItem1.Size = New System.Drawing.Size(601, 31)
    Me.TxtLblMstItem1.TabIndex = 9
    Me.TxtLblMstItem1.TxtPos = False
    '
    'Label14
    '
    Me.Label14.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label14.Location = New System.Drawing.Point(1220, 170)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(120, 21)
    Me.Label14.TabIndex = 61
    Me.Label14.Text = "ｋｇ仕入単価"
    Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxtUnitPrice
    '
    Me.TxtUnitPrice.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtUnitPrice.Location = New System.Drawing.Point(1352, 168)
    Me.TxtUnitPrice.MaxLength = 6
    Me.TxtUnitPrice.Name = "TxtUnitPrice"
    Me.TxtUnitPrice.Size = New System.Drawing.Size(123, 28)
    Me.TxtUnitPrice.TabIndex = 14
    Me.TxtUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label13
    '
    Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label13.Location = New System.Drawing.Point(370, 170)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(90, 21)
    Me.Label13.TabIndex = 60
    Me.Label13.Text = "入庫数"
    Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxtSyukoCount
    '
    Me.TxtSyukoCount.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtSyukoCount.Location = New System.Drawing.Point(470, 168)
    Me.TxtSyukoCount.MaxLength = 2
    Me.TxtSyukoCount.Name = "TxtSyukoCount"
    Me.TxtSyukoCount.Size = New System.Drawing.Size(80, 28)
    Me.TxtSyukoCount.TabIndex = 12
    Me.TxtSyukoCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label12
    '
    Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label12.Location = New System.Drawing.Point(740, 170)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(130, 21)
    Me.Label12.TabIndex = 59
    Me.Label12.Text = "重量(ｋｇ)"
    Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxtWeitghtKg1
    '
    Me.TxtWeitghtKg1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtWeitghtKg1.Location = New System.Drawing.Point(874, 168)
    Me.TxtWeitghtKg1.MaxLength = 0
    Me.TxtWeitghtKg1.Name = "TxtWeitghtKg1"
    Me.TxtWeitghtKg1.Size = New System.Drawing.Size(108, 28)
    Me.TxtWeitghtKg1.TabIndex = 13
    Me.TxtWeitghtKg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label11
    '
    Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label11.Location = New System.Drawing.Point(10, 170)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(140, 21)
    Me.Label11.TabIndex = 58
    Me.Label11.Text = "カートンNo"
    Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxtCartonNumber1
    '
    Me.TxtCartonNumber1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtCartonNumber1.Location = New System.Drawing.Point(160, 168)
    Me.TxtCartonNumber1.MaxLength = 6
    Me.TxtCartonNumber1.Name = "TxtCartonNumber1"
    Me.TxtCartonNumber1.Size = New System.Drawing.Size(100, 28)
    Me.TxtCartonNumber1.TabIndex = 11
    Me.TxtCartonNumber1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label10
    '
    Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label10.Location = New System.Drawing.Point(740, 130)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(130, 21)
    Me.Label10.TabIndex = 57
    Me.Label10.Text = "部位コード(*)"
    Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label9
    '
    Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label9.Location = New System.Drawing.Point(370, 130)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(90, 21)
    Me.Label9.TabIndex = 56
    Me.Label9.Text = "加工日"
    Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxtKakouDate
    '
    Me.TxtKakouDate.Location = New System.Drawing.Point(470, 128)
    Me.TxtKakouDate.Name = "TxtKakouDate"
    Me.TxtKakouDate.Size = New System.Drawing.Size(163, 28)
    Me.TxtKakouDate.TabIndex = 8
    '
    'Label8
    '
    Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label8.Location = New System.Drawing.Point(10, 90)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(140, 21)
    Me.Label8.TabIndex = 55
    Me.Label8.Text = "原産地"
    Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label7
    '
    Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label7.Location = New System.Drawing.Point(370, 90)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(90, 21)
    Me.Label7.TabIndex = 54
    Me.Label7.Text = "格付"
    Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'CmbMstRating1
    '
    Me.CmbMstRating1.AvailableBlank = False
    Me.CmbMstRating1.DisplayMember = "ItemName"
    Me.CmbMstRating1.FormattingEnabled = True
    Me.CmbMstRating1.Location = New System.Drawing.Point(470, 88)
    Me.CmbMstRating1.Name = "CmbMstRating1"
    Me.CmbMstRating1.Size = New System.Drawing.Size(160, 29)
    Me.CmbMstRating1.TabIndex = 4
    Me.CmbMstRating1.ValueMember = "ItemCode"
    '
    'CmbMstOriginPlace1
    '
    Me.CmbMstOriginPlace1.AvailableBlank = False
    Me.CmbMstOriginPlace1.DisplayMember = "ItemName"
    Me.CmbMstOriginPlace1.FormattingEnabled = True
    Me.CmbMstOriginPlace1.Location = New System.Drawing.Point(160, 88)
    Me.CmbMstOriginPlace1.Name = "CmbMstOriginPlace1"
    Me.CmbMstOriginPlace1.Size = New System.Drawing.Size(200, 29)
    Me.CmbMstOriginPlace1.TabIndex = 3
    Me.CmbMstOriginPlace1.ValueMember = "ItemCode"
    '
    'Label6
    '
    Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label6.Location = New System.Drawing.Point(10, 130)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(140, 21)
    Me.Label6.TabIndex = 51
    Me.Label6.Text = "個体識別番号"
    Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxtKotaiNo1
    '
    Me.TxtKotaiNo1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtKotaiNo1.Location = New System.Drawing.Point(160, 128)
    Me.TxtKotaiNo1.MaxLength = 10
    Me.TxtKotaiNo1.Name = "TxtKotaiNo1"
    Me.TxtKotaiNo1.Size = New System.Drawing.Size(150, 28)
    Me.TxtKotaiNo1.TabIndex = 7
    Me.TxtKotaiNo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label5
    '
    Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label5.Location = New System.Drawing.Point(1220, 90)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(120, 21)
    Me.Label5.TabIndex = 48
    Me.Label5.Text = "枝番"
    Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxtEdaban1
    '
    Me.TxtEdaban1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban1.Location = New System.Drawing.Point(1352, 88)
    Me.TxtEdaban1.MaxLength = 6
    Me.TxtEdaban1.Name = "TxtEdaban1"
    Me.TxtEdaban1.Size = New System.Drawing.Size(123, 28)
    Me.TxtEdaban1.TabIndex = 6
    Me.TxtEdaban1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label4
    '
    Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label4.Location = New System.Drawing.Point(1150, 50)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(116, 21)
    Me.Label4.TabIndex = 45
    Me.Label4.Text = "畜種（種別）"
    Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'CmbMstCattle1
    '
    Me.CmbMstCattle1.AvailableBlank = False
    Me.CmbMstCattle1.DisplayMember = "ItemName"
    Me.CmbMstCattle1.FormattingEnabled = True
    Me.CmbMstCattle1.Location = New System.Drawing.Point(1274, 48)
    Me.CmbMstCattle1.Name = "CmbMstCattle1"
    Me.CmbMstCattle1.Size = New System.Drawing.Size(201, 29)
    Me.CmbMstCattle1.TabIndex = 2
    Me.CmbMstCattle1.ValueMember = "ItemCode"
    '
    'Label3
    '
    Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.Location = New System.Drawing.Point(1150, 15)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(73, 21)
    Me.Label3.TabIndex = 42
    Me.Label3.Text = "担当者"
    Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label2
    '
    Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.Location = New System.Drawing.Point(370, 50)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(90, 21)
    Me.Label2.TabIndex = 40
    Me.Label2.Text = "仕入先"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label1
    '
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.Location = New System.Drawing.Point(10, 50)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(140, 21)
    Me.Label1.TabIndex = 38
    Me.Label1.Text = "入庫日付"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'CmbMstStaff1
    '
    Me.CmbMstStaff1.AvailableBlank = False
    Me.CmbMstStaff1.DisplayMember = "ItemName"
    Me.CmbMstStaff1.FormattingEnabled = True
    Me.CmbMstStaff1.Location = New System.Drawing.Point(1235, 13)
    Me.CmbMstStaff1.Name = "CmbMstStaff1"
    Me.CmbMstStaff1.Size = New System.Drawing.Size(240, 29)
    Me.CmbMstStaff1.TabIndex = 15
    Me.CmbMstStaff1.ValueMember = "ItemCode"
    '
    'TxtNyukoDate
    '
    Me.TxtNyukoDate.Location = New System.Drawing.Point(160, 48)
    Me.TxtNyukoDate.Name = "TxtNyukoDate"
    Me.TxtNyukoDate.Size = New System.Drawing.Size(150, 28)
    Me.TxtNyukoDate.TabIndex = 0
    '
    'btnDelete
    '
    Me.btnDelete.Location = New System.Drawing.Point(1080, 870)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(140, 36)
    Me.btnDelete.TabIndex = 4
    Me.btnDelete.Text = "F5：削除"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'Form_NYUKO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.Label_GridData)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnPrint)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnDspStockList)
    Me.Controls.Add(Me.btnRegist)
    Me.Controls.Add(Me.DataGridView1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_NYUKO"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents btnRegist As Button
  Friend WithEvents btnDspStockList As Button
  Friend WithEvents btnPrint As Button
  Friend WithEvents btnClose As Button
  Friend WithEvents Label_GridData As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents TxtLblMstItem1 As T.R.ZCommonCtrl.TxtLblMstItem
  Friend WithEvents Label14 As Label
  Friend WithEvents TxtUnitPrice As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents Label13 As Label
  Friend WithEvents TxtSyukoCount As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents Label12 As Label
  Friend WithEvents TxtWeitghtKg1 As T.R.ZCommonCtrl.TxtWeitghtKg
  Friend WithEvents Label11 As Label
  Friend WithEvents TxtCartonNumber1 As T.R.ZCommonCtrl.TxtCartonNumber
  Friend WithEvents Label10 As Label
  Friend WithEvents Label9 As Label
  Friend WithEvents TxtKakouDate As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents Label8 As Label
  Friend WithEvents Label7 As Label
  Friend WithEvents CmbMstRating1 As T.R.ZCommonCtrl.CmbMstRating
  Friend WithEvents CmbMstOriginPlace1 As T.R.ZCommonCtrl.CmbMstOriginPlace
  Friend WithEvents Label6 As Label
  Friend WithEvents TxtKotaiNo1 As T.R.ZCommonCtrl.TxKotaiNo
  Friend WithEvents Label5 As Label
  Friend WithEvents TxtEdaban1 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents Label4 As Label
  Friend WithEvents CmbMstCattle1 As T.R.ZCommonCtrl.CmbMstCattle
  Friend WithEvents Label3 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Label1 As Label
  Friend WithEvents CmbMstStaff1 As T.R.ZCommonCtrl.CmbMstStaff
  Friend WithEvents Label16 As Label
  Friend WithEvents CmbMstKikaku1 As T.R.ZCommonCtrl.CmbMstKikaku
  Friend WithEvents TxtNyukoDate As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents Label17 As Label
  Friend WithEvents TxtBuiCode1 As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents CmbMstCustomer1 As T.R.ZCommonCtrl.CmbMstShiresaki
  Friend WithEvents btnDelete As Button
End Class
