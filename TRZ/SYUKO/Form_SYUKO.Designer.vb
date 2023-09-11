<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_SYUKO
  Inherits T.R.ZCommonCtrl.MFBaseDgv

  'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
  <System.Diagnostics.DebuggerNonUserCode()> _
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
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_SYUKO))
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.DataGridView2 = New System.Windows.Forms.DataGridView()
    Me.TxtSyukoDate = New T.R.ZCommonCtrl.TxtDateBase()
    Me.CmbMstStaff1 = New T.R.ZCommonCtrl.CmbMstStaff()
    Me.CmbMstCustomer1 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.CmbMstCattle1 = New T.R.ZCommonCtrl.CmbMstCattle()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.TxtEdaban1 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.TxtKotaiNo1 = New T.R.ZCommonCtrl.TxKotaiNo()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.CmbMstOriginPlace1 = New T.R.ZCommonCtrl.CmbMstOriginPlace()
    Me.CmbMstRating1 = New T.R.ZCommonCtrl.CmbMstRating()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.TxtKakouDate = New T.R.ZCommonCtrl.TxtDateBase()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.TxtCartonNumber1 = New T.R.ZCommonCtrl.TxtCartonNumber()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.TxtWeitghtKg1 = New T.R.ZCommonCtrl.TxtWeitghtKg()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.txtSyukoCount = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.TxtUnitPrice = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.CmbMstSetType1 = New T.R.ZCommonCtrl.CmbMstSetType()
    Me.Label15 = New System.Windows.Forms.Label()
    Me.btnRegist = New System.Windows.Forms.Button()
    Me.btnDspStockList = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.btnPrint = New System.Windows.Forms.Button()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.lblGridStat = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.TxtLblMstItem1 = New T.R.ZCommonCtrl.TxtLblMstItem()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(13, 252)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.Size = New System.Drawing.Size(1510, 607)
    Me.DataGridView1.TabIndex = 15
    '
    'DataGridView2
    '
    Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView2.Location = New System.Drawing.Point(13, 252)
    Me.DataGridView2.Name = "DataGridView2"
    Me.DataGridView2.Size = New System.Drawing.Size(1510, 607)
    Me.DataGridView2.TabIndex = 15
    '
    'TxtSyukoDate
    '
    Me.TxtSyukoDate.Location = New System.Drawing.Point(108, 53)
    Me.TxtSyukoDate.Name = "TxtSyukoDate"
    Me.TxtSyukoDate.Size = New System.Drawing.Size(163, 28)
    Me.TxtSyukoDate.TabIndex = 1
    '
    'CmbMstStaff1
    '
    Me.CmbMstStaff1.AvailableBlank = False
    Me.CmbMstStaff1.DisplayMember = "ItemName"
    Me.CmbMstStaff1.FormattingEnabled = True
    Me.CmbMstStaff1.Location = New System.Drawing.Point(1282, 18)
    Me.CmbMstStaff1.Name = "CmbMstStaff1"
    Me.CmbMstStaff1.Size = New System.Drawing.Size(240, 29)
    Me.CmbMstStaff1.TabIndex = 3
    Me.CmbMstStaff1.ValueMember = "ItemCode"
    '
    'CmbMstCustomer1
    '
    Me.CmbMstCustomer1.AvailableBlank = False
    Me.CmbMstCustomer1.DisplayMember = "ItemName"
    Me.CmbMstCustomer1.FormattingEnabled = True
    Me.CmbMstCustomer1.Location = New System.Drawing.Point(435, 53)
    Me.CmbMstCustomer1.Name = "CmbMstCustomer1"
    Me.CmbMstCustomer1.Size = New System.Drawing.Size(472, 29)
    Me.CmbMstCustomer1.TabIndex = 2
    Me.CmbMstCustomer1.ValueMember = "ItemCode"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.Location = New System.Drawing.Point(9, 57)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(94, 21)
    Me.Label1.TabIndex = 5
    Me.Label1.Text = "出庫日付"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.Location = New System.Drawing.Point(357, 57)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(73, 21)
    Me.Label2.TabIndex = 6
    Me.Label2.Text = "得意先"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.Location = New System.Drawing.Point(1204, 19)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(73, 21)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "担当者"
    '
    'CmbMstCattle1
    '
    Me.CmbMstCattle1.AvailableBlank = False
    Me.CmbMstCattle1.DisplayMember = "ItemName"
    Me.CmbMstCattle1.FormattingEnabled = True
    Me.CmbMstCattle1.Location = New System.Drawing.Point(1150, 57)
    Me.CmbMstCattle1.Name = "CmbMstCattle1"
    Me.CmbMstCattle1.Size = New System.Drawing.Size(120, 29)
    Me.CmbMstCattle1.TabIndex = 3
    Me.CmbMstCattle1.ValueMember = "ItemCode"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label4.Location = New System.Drawing.Point(1006, 61)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(116, 21)
    Me.Label4.TabIndex = 9
    Me.Label4.Text = "畜種（種別）"
    '
    'TxtEdaban1
    '
    Me.TxtEdaban1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban1.Location = New System.Drawing.Point(784, 95)
    Me.TxtEdaban1.Name = "TxtEdaban1"
    Me.TxtEdaban1.Size = New System.Drawing.Size(123, 28)
    Me.TxtEdaban1.TabIndex = 6
    Me.TxtEdaban1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label5.Location = New System.Drawing.Point(726, 99)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(52, 21)
    Me.Label5.TabIndex = 11
    Me.Label5.Text = "枝番"
    '
    'TxtKotaiNo1
    '
    Me.TxtKotaiNo1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtKotaiNo1.Location = New System.Drawing.Point(1150, 99)
    Me.TxtKotaiNo1.Name = "TxtKotaiNo1"
    Me.TxtKotaiNo1.Size = New System.Drawing.Size(188, 28)
    Me.TxtKotaiNo1.TabIndex = 7
    Me.TxtKotaiNo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label6.Location = New System.Drawing.Point(1006, 102)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(136, 21)
    Me.Label6.TabIndex = 13
    Me.Label6.Text = "個体識別番号"
    '
    'CmbMstOriginPlace1
    '
    Me.CmbMstOriginPlace1.AvailableBlank = False
    Me.CmbMstOriginPlace1.DisplayMember = "ItemName"
    Me.CmbMstOriginPlace1.FormattingEnabled = True
    Me.CmbMstOriginPlace1.Location = New System.Drawing.Point(108, 99)
    Me.CmbMstOriginPlace1.Name = "CmbMstOriginPlace1"
    Me.CmbMstOriginPlace1.Size = New System.Drawing.Size(163, 29)
    Me.CmbMstOriginPlace1.TabIndex = 4
    Me.CmbMstOriginPlace1.ValueMember = "ItemCode"
    '
    'CmbMstRating1
    '
    Me.CmbMstRating1.AvailableBlank = False
    Me.CmbMstRating1.DisplayMember = "ItemName"
    Me.CmbMstRating1.FormattingEnabled = True
    Me.CmbMstRating1.Location = New System.Drawing.Point(435, 99)
    Me.CmbMstRating1.Name = "CmbMstRating1"
    Me.CmbMstRating1.Size = New System.Drawing.Size(160, 29)
    Me.CmbMstRating1.TabIndex = 5
    Me.CmbMstRating1.ValueMember = "ItemCode"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label7.Location = New System.Drawing.Point(357, 102)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(52, 21)
    Me.Label7.TabIndex = 17
    Me.Label7.Text = "格付"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label8.Location = New System.Drawing.Point(9, 102)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(73, 21)
    Me.Label8.TabIndex = 18
    Me.Label8.Text = "原産地"
    '
    'TxtKakouDate
    '
    Me.TxtKakouDate.Location = New System.Drawing.Point(108, 145)
    Me.TxtKakouDate.Name = "TxtKakouDate"
    Me.TxtKakouDate.Size = New System.Drawing.Size(163, 28)
    Me.TxtKakouDate.TabIndex = 8
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label9.Location = New System.Drawing.Point(9, 149)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(73, 21)
    Me.Label9.TabIndex = 20
    Me.Label9.Text = "加工日"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label10.Location = New System.Drawing.Point(302, 149)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(127, 21)
    Me.Label10.TabIndex = 22
    Me.Label10.Text = "部位コード(*)"
    '
    'TxtCartonNumber1
    '
    Me.TxtCartonNumber1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtCartonNumber1.Location = New System.Drawing.Point(1150, 149)
    Me.TxtCartonNumber1.Name = "TxtCartonNumber1"
    Me.TxtCartonNumber1.Size = New System.Drawing.Size(100, 28)
    Me.TxtCartonNumber1.TabIndex = 10
    Me.TxtCartonNumber1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label11.Location = New System.Drawing.Point(1006, 150)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(102, 21)
    Me.Label11.TabIndex = 24
    Me.Label11.Text = "カートンNo"
    '
    'TxtWeitghtKg1
    '
    Me.TxtWeitghtKg1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtWeitghtKg1.Location = New System.Drawing.Point(435, 191)
    Me.TxtWeitghtKg1.Name = "TxtWeitghtKg1"
    Me.TxtWeitghtKg1.Size = New System.Drawing.Size(108, 28)
    Me.TxtWeitghtKg1.TabIndex = 12
    Me.TxtWeitghtKg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label12.Location = New System.Drawing.Point(302, 192)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(87, 21)
    Me.Label12.TabIndex = 26
    Me.Label12.Text = "重量(Kg)"
    '
    'txtSyukoCount
    '
    Me.txtSyukoCount.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.txtSyukoCount.Location = New System.Drawing.Point(108, 191)
    Me.txtSyukoCount.Name = "txtSyukoCount"
    Me.txtSyukoCount.Size = New System.Drawing.Size(163, 28)
    Me.txtSyukoCount.TabIndex = 11
    Me.txtSyukoCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label13
    '
    Me.Label13.AutoSize = True
    Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label13.Location = New System.Drawing.Point(13, 192)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(73, 21)
    Me.Label13.TabIndex = 28
    Me.Label13.Text = "出庫数"
    '
    'TxtUnitPrice
    '
    Me.TxtUnitPrice.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtUnitPrice.Location = New System.Drawing.Point(784, 191)
    Me.TxtUnitPrice.Name = "TxtUnitPrice"
    Me.TxtUnitPrice.Size = New System.Drawing.Size(123, 28)
    Me.TxtUnitPrice.TabIndex = 13
    Me.TxtUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label14.Location = New System.Drawing.Point(680, 192)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(96, 21)
    Me.Label14.TabIndex = 30
    Me.Label14.Text = "Kg売単価"
    '
    'CmbMstSetType1
    '
    Me.CmbMstSetType1.AvailableBlank = False
    Me.CmbMstSetType1.DisplayMember = "ItemName"
    Me.CmbMstSetType1.FormattingEnabled = True
    Me.CmbMstSetType1.Location = New System.Drawing.Point(1150, 191)
    Me.CmbMstSetType1.Name = "CmbMstSetType1"
    Me.CmbMstSetType1.Size = New System.Drawing.Size(372, 29)
    Me.CmbMstSetType1.TabIndex = 14
    Me.CmbMstSetType1.ValueMember = "ItemCode"
    '
    'Label15
    '
    Me.Label15.AutoSize = True
    Me.Label15.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label15.Location = New System.Drawing.Point(1006, 192)
    Me.Label15.Name = "Label15"
    Me.Label15.Size = New System.Drawing.Size(58, 21)
    Me.Label15.TabIndex = 32
    Me.Label15.Text = "セット"
    '
    'btnRegist
    '
    Me.btnRegist.Location = New System.Drawing.Point(878, 864)
    Me.btnRegist.Name = "btnRegist"
    Me.btnRegist.Size = New System.Drawing.Size(112, 32)
    Me.btnRegist.TabIndex = 33
    Me.btnRegist.Text = "登録"
    Me.btnRegist.UseVisualStyleBackColor = True
    '
    'btnDspStockList
    '
    Me.btnDspStockList.Location = New System.Drawing.Point(1010, 864)
    Me.btnDspStockList.Name = "btnDspStockList"
    Me.btnDspStockList.Size = New System.Drawing.Size(112, 32)
    Me.btnDspStockList.TabIndex = 34
    Me.btnDspStockList.Text = "在庫表示"
    Me.btnDspStockList.UseVisualStyleBackColor = True
    '
    'btnDelete
    '
    Me.btnDelete.Location = New System.Drawing.Point(1144, 864)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(112, 32)
    Me.btnDelete.TabIndex = 35
    Me.btnDelete.Text = "削除"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnPrint
    '
    Me.btnPrint.Location = New System.Drawing.Point(1278, 864)
    Me.btnPrint.Name = "btnPrint"
    Me.btnPrint.Size = New System.Drawing.Size(112, 32)
    Me.btnPrint.TabIndex = 36
    Me.btnPrint.Text = "印刷"
    Me.btnPrint.UseVisualStyleBackColor = True
    '
    'btnClose
    '
    Me.btnClose.Location = New System.Drawing.Point(1410, 864)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(112, 32)
    Me.btnClose.TabIndex = 37
    Me.btnClose.Text = "閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'lblGridStat
    '
    Me.lblGridStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblGridStat.Location = New System.Drawing.Point(13, 226)
    Me.lblGridStat.Name = "lblGridStat"
    Me.lblGridStat.Size = New System.Drawing.Size(1511, 26)
    Me.lblGridStat.TabIndex = 38
    Me.lblGridStat.Text = "Label16"
    Me.lblGridStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
    'TxtLblMstItem1
    '
    Me.TxtLblMstItem1.BackColor = System.Drawing.SystemColors.Control
    Me.TxtLblMstItem1.CodeTxt = ""
    Me.TxtLblMstItem1.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstItem1.Location = New System.Drawing.Point(435, 145)
    Me.TxtLblMstItem1.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
    Me.TxtLblMstItem1.Name = "TxtLblMstItem1"
    Me.TxtLblMstItem1.Size = New System.Drawing.Size(393, 31)
    Me.TxtLblMstItem1.TabIndex = 9
    Me.TxtLblMstItem1.TxtPos = False
    '
    'Form_SYUKO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.TxtLblMstItem1)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.lblGridStat)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnPrint)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnDspStockList)
    Me.Controls.Add(Me.btnRegist)
    Me.Controls.Add(Me.Label15)
    Me.Controls.Add(Me.CmbMstSetType1)
    Me.Controls.Add(Me.Label14)
    Me.Controls.Add(Me.TxtUnitPrice)
    Me.Controls.Add(Me.Label13)
    Me.Controls.Add(Me.txtSyukoCount)
    Me.Controls.Add(Me.Label12)
    Me.Controls.Add(Me.TxtWeitghtKg1)
    Me.Controls.Add(Me.Label11)
    Me.Controls.Add(Me.TxtCartonNumber1)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.TxtKakouDate)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.CmbMstRating1)
    Me.Controls.Add(Me.CmbMstOriginPlace1)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.TxtKotaiNo1)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.TxtEdaban1)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.CmbMstCattle1)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.CmbMstCustomer1)
    Me.Controls.Add(Me.CmbMstStaff1)
    Me.Controls.Add(Me.TxtSyukoDate)
    Me.Controls.Add(Me.DataGridView2)
    Me.Controls.Add(Me.DataGridView1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_SYUKO"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents DataGridView2 As DataGridView
  Friend WithEvents TxtSyukoDate As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents CmbMstStaff1 As T.R.ZCommonCtrl.CmbMstStaff
  Friend WithEvents CmbMstCustomer1 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents CmbMstCattle1 As T.R.ZCommonCtrl.CmbMstCattle
  Friend WithEvents Label4 As Label
  Friend WithEvents TxtEdaban1 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents Label5 As Label
  Friend WithEvents TxtKotaiNo1 As T.R.ZCommonCtrl.TxKotaiNo
  Friend WithEvents Label6 As Label
  Friend WithEvents CmbMstOriginPlace1 As T.R.ZCommonCtrl.CmbMstOriginPlace
  Friend WithEvents CmbMstRating1 As T.R.ZCommonCtrl.CmbMstRating
  Friend WithEvents Label7 As Label
  Friend WithEvents Label8 As Label
  Friend WithEvents TxtKakouDate As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents Label9 As Label
  Friend WithEvents Label10 As Label
  Friend WithEvents TxtCartonNumber1 As T.R.ZCommonCtrl.TxtCartonNumber
  Friend WithEvents Label11 As Label
  Friend WithEvents TxtWeitghtKg1 As T.R.ZCommonCtrl.TxtWeitghtKg
  Friend WithEvents Label12 As Label
  Friend WithEvents txtSyukoCount As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents Label13 As Label
  Friend WithEvents TxtUnitPrice As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents Label14 As Label
  Friend WithEvents CmbMstSetType1 As T.R.ZCommonCtrl.CmbMstSetType
  Friend WithEvents Label15 As Label
  Friend WithEvents btnRegist As Button
  Friend WithEvents btnDspStockList As Button
  Friend WithEvents btnDelete As Button
  Friend WithEvents btnPrint As Button
  Friend WithEvents btnClose As Button
  Friend WithEvents lblGridStat As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents TxtLblMstItem1 As T.R.ZCommonCtrl.TxtLblMstItem
End Class
