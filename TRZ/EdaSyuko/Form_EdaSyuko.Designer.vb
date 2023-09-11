<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_EdaSyuko
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_EdaSyuko))
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.DataGridView2 = New System.Windows.Forms.DataGridView()
    Me.CmbMstStaff1 = New T.R.ZCommonCtrl.CmbMstStaff()
    Me.TxtDateOutStock = New T.R.ZCommonCtrl.TxtDateBase()
    Me.CmbMstCustomer1 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.TxtDateProcess = New T.R.ZCommonCtrl.TxtDateBase()
    Me.TxtEdaban1 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.TxKotaiNo1 = New T.R.ZCommonCtrl.TxKotaiNo()
    Me.CmbEdaType = New System.Windows.Forms.ComboBox()
    Me.TxtWeitghtKg1 = New T.R.ZCommonCtrl.TxtWeitghtKg()
    Me.TxtUnitPrice = New T.R.ZCommonCtrl.TxTanka()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.lblGridStat = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.btnPrint = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.btnDspStockList = New System.Windows.Forms.Button()
    Me.btnRegist = New System.Windows.Forms.Button()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(12, 210)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.Size = New System.Drawing.Size(1510, 645)
    Me.DataGridView1.TabIndex = 0
    '
    'DataGridView2
    '
    Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView2.Location = New System.Drawing.Point(12, 210)
    Me.DataGridView2.Name = "DataGridView2"
    Me.DataGridView2.Size = New System.Drawing.Size(1510, 645)
    Me.DataGridView2.TabIndex = 1
    '
    'CmbMstStaff1
    '
    Me.CmbMstStaff1.AvailableBlank = False
    Me.CmbMstStaff1.DisplayMember = "ItemName"
    Me.CmbMstStaff1.FormattingEnabled = True
    Me.CmbMstStaff1.Location = New System.Drawing.Point(1041, 12)
    Me.CmbMstStaff1.Name = "CmbMstStaff1"
    Me.CmbMstStaff1.Size = New System.Drawing.Size(262, 29)
    Me.CmbMstStaff1.TabIndex = 2
    Me.CmbMstStaff1.ValueMember = "ItemCode"
    '
    'TxtDateOutStock
    '
    Me.TxtDateOutStock.Location = New System.Drawing.Point(148, 48)
    Me.TxtDateOutStock.Name = "TxtDateOutStock"
    Me.TxtDateOutStock.Size = New System.Drawing.Size(162, 28)
    Me.TxtDateOutStock.TabIndex = 3
    '
    'CmbMstCustomer1
    '
    Me.CmbMstCustomer1.AvailableBlank = False
    Me.CmbMstCustomer1.DisplayMember = "ItemName"
    Me.CmbMstCustomer1.FormattingEnabled = True
    Me.CmbMstCustomer1.Location = New System.Drawing.Point(473, 48)
    Me.CmbMstCustomer1.Name = "CmbMstCustomer1"
    Me.CmbMstCustomer1.Size = New System.Drawing.Size(394, 29)
    Me.CmbMstCustomer1.TabIndex = 4
    Me.CmbMstCustomer1.ValueMember = "ItemCode"
    '
    'TxtDateProcess
    '
    Me.TxtDateProcess.Location = New System.Drawing.Point(148, 92)
    Me.TxtDateProcess.Name = "TxtDateProcess"
    Me.TxtDateProcess.Size = New System.Drawing.Size(162, 28)
    Me.TxtDateProcess.TabIndex = 5
    '
    'TxtEdaban1
    '
    Me.TxtEdaban1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban1.Location = New System.Drawing.Point(473, 92)
    Me.TxtEdaban1.Name = "TxtEdaban1"
    Me.TxtEdaban1.Size = New System.Drawing.Size(100, 28)
    Me.TxtEdaban1.TabIndex = 6
    Me.TxtEdaban1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxKotaiNo1
    '
    Me.TxKotaiNo1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxKotaiNo1.Location = New System.Drawing.Point(782, 92)
    Me.TxKotaiNo1.Name = "TxKotaiNo1"
    Me.TxKotaiNo1.Size = New System.Drawing.Size(181, 28)
    Me.TxKotaiNo1.TabIndex = 7
    Me.TxKotaiNo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'CmbEdaType
    '
    Me.CmbEdaType.FormattingEnabled = True
    Me.CmbEdaType.Location = New System.Drawing.Point(148, 136)
    Me.CmbEdaType.Name = "CmbEdaType"
    Me.CmbEdaType.Size = New System.Drawing.Size(162, 29)
    Me.CmbEdaType.TabIndex = 8
    '
    'TxtWeitghtKg1
    '
    Me.TxtWeitghtKg1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtWeitghtKg1.Location = New System.Drawing.Point(473, 136)
    Me.TxtWeitghtKg1.Name = "TxtWeitghtKg1"
    Me.TxtWeitghtKg1.Size = New System.Drawing.Size(100, 28)
    Me.TxtWeitghtKg1.TabIndex = 9
    Me.TxtWeitghtKg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxtUnitPrice
    '
    Me.TxtUnitPrice.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtUnitPrice.Location = New System.Drawing.Point(782, 136)
    Me.TxtUnitPrice.Name = "TxtUnitPrice"
    Me.TxtUnitPrice.Size = New System.Drawing.Size(100, 28)
    Me.TxtUnitPrice.TabIndex = 10
    Me.TxtUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(940, 12)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(73, 21)
    Me.Label1.TabIndex = 11
    Me.Label1.Text = "担当者"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(36, 52)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(94, 21)
    Me.Label2.TabIndex = 12
    Me.Label2.Text = "出庫日付"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(380, 52)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(73, 21)
    Me.Label3.TabIndex = 13
    Me.Label3.Text = "得意先"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(380, 96)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(52, 21)
    Me.Label4.TabIndex = 14
    Me.Label4.Text = "枝番"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(36, 96)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(73, 21)
    Me.Label5.TabIndex = 15
    Me.Label5.Text = "加工日"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(640, 96)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(136, 21)
    Me.Label6.TabIndex = 16
    Me.Label6.Text = "個体識別番号"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(36, 140)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(58, 21)
    Me.Label7.TabIndex = 17
    Me.Label7.Text = "セット"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(380, 140)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(87, 21)
    Me.Label8.TabIndex = 18
    Me.Label8.Text = "重量(Kg)"
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(651, 140)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(96, 21)
    Me.Label9.TabIndex = 19
    Me.Label9.Text = "Kg売単価"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(24, 9)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(136, 21)
    Me.Label10.TabIndex = 20
    Me.Label10.Text = "枝肉出庫処理"
    '
    'lblGridStat
    '
    Me.lblGridStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblGridStat.Location = New System.Drawing.Point(11, 184)
    Me.lblGridStat.Name = "lblGridStat"
    Me.lblGridStat.Size = New System.Drawing.Size(1511, 26)
    Me.lblGridStat.TabIndex = 39
    Me.lblGridStat.Text = "Label16"
    Me.lblGridStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblInformation
    '
    Me.lblInformation.AutoSize = True
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Red
    Me.lblInformation.Location = New System.Drawing.Point(7, 882)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(294, 19)
    Me.lblInformation.TabIndex = 40
    Me.lblInformation.Text = "ここに入力時の説明文が表示されます"
    '
    'btnClose
    '
    Me.btnClose.Location = New System.Drawing.Point(1392, 865)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(131, 32)
    Me.btnClose.TabIndex = 45
    Me.btnClose.Text = "F12:閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'btnPrint
    '
    Me.btnPrint.Location = New System.Drawing.Point(1252, 865)
    Me.btnPrint.Name = "btnPrint"
    Me.btnPrint.Size = New System.Drawing.Size(131, 32)
    Me.btnPrint.TabIndex = 44
    Me.btnPrint.Text = "F9:印刷"
    Me.btnPrint.UseVisualStyleBackColor = True
    '
    'btnDelete
    '
    Me.btnDelete.Location = New System.Drawing.Point(1110, 865)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(131, 32)
    Me.btnDelete.TabIndex = 43
    Me.btnDelete.Text = "F5:削除"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnDspStockList
    '
    Me.btnDspStockList.Location = New System.Drawing.Point(968, 865)
    Me.btnDspStockList.Name = "btnDspStockList"
    Me.btnDspStockList.Size = New System.Drawing.Size(131, 32)
    Me.btnDspStockList.TabIndex = 42
    Me.btnDspStockList.Text = "F3:在庫表示"
    Me.btnDspStockList.UseVisualStyleBackColor = True
    '
    'btnRegist
    '
    Me.btnRegist.Location = New System.Drawing.Point(828, 865)
    Me.btnRegist.Name = "btnRegist"
    Me.btnRegist.Size = New System.Drawing.Size(131, 32)
    Me.btnRegist.TabIndex = 41
    Me.btnRegist.Text = "F1:登録"
    Me.btnRegist.UseVisualStyleBackColor = True
    '
    'Form_EdaSyuko
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnPrint)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnDspStockList)
    Me.Controls.Add(Me.btnRegist)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.lblGridStat)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TxtUnitPrice)
    Me.Controls.Add(Me.TxtWeitghtKg1)
    Me.Controls.Add(Me.CmbEdaType)
    Me.Controls.Add(Me.TxKotaiNo1)
    Me.Controls.Add(Me.TxtEdaban1)
    Me.Controls.Add(Me.TxtDateProcess)
    Me.Controls.Add(Me.CmbMstCustomer1)
    Me.Controls.Add(Me.TxtDateOutStock)
    Me.Controls.Add(Me.CmbMstStaff1)
    Me.Controls.Add(Me.DataGridView2)
    Me.Controls.Add(Me.DataGridView1)
    Me.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_EdaSyuko"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents DataGridView2 As DataGridView
  Friend WithEvents CmbMstStaff1 As T.R.ZCommonCtrl.CmbMstStaff
  Friend WithEvents TxtDateOutStock As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents CmbMstCustomer1 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents TxtDateProcess As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents TxtEdaban1 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents TxKotaiNo1 As T.R.ZCommonCtrl.TxKotaiNo
  Friend WithEvents CmbEdaType As ComboBox
  Friend WithEvents TxtWeitghtKg1 As T.R.ZCommonCtrl.TxtWeitghtKg
  Friend WithEvents TxtUnitPrice As T.R.ZCommonCtrl.TxTanka
  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents Label4 As Label
  Friend WithEvents Label5 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents Label7 As Label
  Friend WithEvents Label8 As Label
  Friend WithEvents Label9 As Label
  Friend WithEvents Label10 As Label
  Friend WithEvents lblGridStat As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents btnClose As Button
  Friend WithEvents btnPrint As Button
  Friend WithEvents btnDelete As Button
  Friend WithEvents btnDspStockList As Button
  Friend WithEvents btnRegist As Button
End Class
