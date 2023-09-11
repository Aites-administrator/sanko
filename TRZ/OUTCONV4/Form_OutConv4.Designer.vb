<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_OutConv4
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_OutConv4))
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.DataGridView2 = New System.Windows.Forms.DataGridView()
    Me.CmbMstShiresaki1 = New T.R.ZCommonCtrl.CmbMstShiresaki()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.CmbDateShiirebi1 = New T.R.ZCommonCtrl.CmbDateShiirebi()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.lblItemDetailListStat = New System.Windows.Forms.Label()
    Me.lblSupplierListStat = New System.Windows.Forms.Label()
    Me.lblUnSendCount = New System.Windows.Forms.Label()
    Me.lblPostPlanedCount = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.btnEnd = New T.R.ZCommonCtrl.ButtonEnd()
    Me.btnPostPca = New T.R.ZCommonCtrl.ButtonUpdate()
    Me.btnRefresh = New System.Windows.Forms.Button()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(20, 125)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.RowTemplate.Height = 21
    Me.DataGridView1.Size = New System.Drawing.Size(1495, 245)
    Me.DataGridView1.TabIndex = 2
    '
    'DataGridView2
    '
    Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView2.Location = New System.Drawing.Point(20, 422)
    Me.DataGridView2.Name = "DataGridView2"
    Me.DataGridView2.RowTemplate.Height = 21
    Me.DataGridView2.Size = New System.Drawing.Size(1495, 423)
    Me.DataGridView2.TabIndex = 109
    Me.DataGridView2.TabStop = False
    '
    'CmbMstShiresaki1
    '
    Me.CmbMstShiresaki1.AvailableBlank = False
    Me.CmbMstShiresaki1.DisplayMember = "ItemName"
    Me.CmbMstShiresaki1.FormattingEnabled = True
    Me.CmbMstShiresaki1.Location = New System.Drawing.Point(402, 55)
    Me.CmbMstShiresaki1.Name = "CmbMstShiresaki1"
    Me.CmbMstShiresaki1.Size = New System.Drawing.Size(382, 29)
    Me.CmbMstShiresaki1.TabIndex = 1
    Me.CmbMstShiresaki1.ValueMember = "ItemCode"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.Magenta
    Me.Label1.Location = New System.Drawing.Point(298, 58)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(106, 24)
    Me.Label1.TabIndex = 3
    Me.Label1.Text = "仕入先名"
    '
    'CmbDateShiirebi1
    '
    Me.CmbDateShiirebi1.AvailableBlank = False
    Me.CmbDateShiirebi1.DisplayMember = "ItemCode"
    Me.CmbDateShiirebi1.FormattingEnabled = True
    Me.CmbDateShiirebi1.Location = New System.Drawing.Point(88, 55)
    Me.CmbDateShiirebi1.Name = "CmbDateShiirebi1"
    Me.CmbDateShiirebi1.Size = New System.Drawing.Size(138, 29)
    Me.CmbDateShiirebi1.TabIndex = 0
    Me.CmbDateShiirebi1.ValueMember = "ItemCode"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.Magenta
    Me.Label2.Location = New System.Drawing.Point(16, 58)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(82, 24)
    Me.Label2.TabIndex = 5
    Me.Label2.Text = "仕入日"
    '
    'lblInformation
    '
    Me.lblInformation.BackColor = System.Drawing.Color.White
    Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Navy
    Me.lblInformation.Location = New System.Drawing.Point(0, 882)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(1535, 29)
    Me.lblInformation.TabIndex = 19
    Me.lblInformation.Text = "Label18"
    Me.lblInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblItemDetailListStat
    '
    Me.lblItemDetailListStat.BackColor = System.Drawing.SystemColors.Control
    Me.lblItemDetailListStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblItemDetailListStat.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblItemDetailListStat.ForeColor = System.Drawing.Color.Black
    Me.lblItemDetailListStat.Location = New System.Drawing.Point(20, 392)
    Me.lblItemDetailListStat.Name = "lblItemDetailListStat"
    Me.lblItemDetailListStat.Size = New System.Drawing.Size(1495, 31)
    Me.lblItemDetailListStat.TabIndex = 20
    Me.lblItemDetailListStat.Text = "Label18"
    Me.lblItemDetailListStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblSupplierListStat
    '
    Me.lblSupplierListStat.BackColor = System.Drawing.SystemColors.Control
    Me.lblSupplierListStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblSupplierListStat.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblSupplierListStat.ForeColor = System.Drawing.Color.Black
    Me.lblSupplierListStat.Location = New System.Drawing.Point(20, 95)
    Me.lblSupplierListStat.Name = "lblSupplierListStat"
    Me.lblSupplierListStat.Size = New System.Drawing.Size(1495, 31)
    Me.lblSupplierListStat.TabIndex = 21
    Me.lblSupplierListStat.Text = "Label18"
    Me.lblSupplierListStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblUnSendCount
    '
    Me.lblUnSendCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(208, Byte), Integer))
    Me.lblUnSendCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblUnSendCount.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblUnSendCount.ForeColor = System.Drawing.Color.Black
    Me.lblUnSendCount.Location = New System.Drawing.Point(1252, 844)
    Me.lblUnSendCount.Name = "lblUnSendCount"
    Me.lblUnSendCount.Size = New System.Drawing.Size(263, 31)
    Me.lblUnSendCount.TabIndex = 22
    Me.lblUnSendCount.Text = "Label18"
    Me.lblUnSendCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblPostPlanedCount
    '
    Me.lblPostPlanedCount.AutoSize = True
    Me.lblPostPlanedCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblPostPlanedCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(208, Byte), Integer))
    Me.lblPostPlanedCount.Location = New System.Drawing.Point(16, 850)
    Me.lblPostPlanedCount.Name = "lblPostPlanedCount"
    Me.lblPostPlanedCount.Size = New System.Drawing.Size(367, 24)
    Me.lblPostPlanedCount.TabIndex = 25
    Me.lblPostPlanedCount.Text = "ここに送信予定件数が表示されます"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Label3.Location = New System.Drawing.Point(16, 9)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(321, 33)
    Me.Label3.TabIndex = 110
    Me.Label3.Text = "仕入データ  変換画面"
    '
    'btnEnd
    '
    Me.btnEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.btnEnd.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnEnd.Image = CType(resources.GetObject("btnEnd.Image"), System.Drawing.Image)
    Me.btnEnd.Location = New System.Drawing.Point(1421, 6)
    Me.btnEnd.Name = "btnEnd"
    Me.btnEnd.Size = New System.Drawing.Size(94, 84)
    Me.btnEnd.TabIndex = 111
    Me.btnEnd.TabStop = False
    Me.btnEnd.Text = "F12:終了"
    Me.btnEnd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btnEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnEnd.UseVisualStyleBackColor = False
    '
    'btnPostPca
    '
    Me.btnPostPca.BackColor = System.Drawing.SystemColors.Control
    Me.btnPostPca.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnPostPca.Image = CType(resources.GetObject("btnPostPca.Image"), System.Drawing.Image)
    Me.btnPostPca.Location = New System.Drawing.Point(1250, 6)
    Me.btnPostPca.Name = "btnPostPca"
    Me.btnPostPca.Size = New System.Drawing.Size(94, 84)
    Me.btnPostPca.TabIndex = 112
    Me.btnPostPca.TabStop = False
    Me.btnPostPca.Text = "F1:仕入明細データ作成"
    Me.btnPostPca.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnPostPca.UseVisualStyleBackColor = False
    '
    'btnRefresh
    '
    Me.btnRefresh.BackColor = System.Drawing.SystemColors.Control
    Me.btnRefresh.Location = New System.Drawing.Point(817, 55)
    Me.btnRefresh.Name = "btnRefresh"
    Me.btnRefresh.Size = New System.Drawing.Size(125, 27)
    Me.btnRefresh.TabIndex = 113
    Me.btnRefresh.TabStop = False
    Me.btnRefresh.Text = "F5:最新表示"
    Me.btnRefresh.UseVisualStyleBackColor = False
    '
    'Form_OutConv4
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.BackColor = System.Drawing.Color.Green
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.btnRefresh)
    Me.Controls.Add(Me.btnPostPca)
    Me.Controls.Add(Me.btnEnd)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.lblPostPlanedCount)
    Me.Controls.Add(Me.lblUnSendCount)
    Me.Controls.Add(Me.lblSupplierListStat)
    Me.Controls.Add(Me.lblItemDetailListStat)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.CmbDateShiirebi1)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.CmbMstShiresaki1)
    Me.Controls.Add(Me.DataGridView2)
    Me.Controls.Add(Me.DataGridView1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_OutConv4"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents DataGridView2 As DataGridView
  Friend WithEvents CmbMstShiresaki1 As T.R.ZCommonCtrl.CmbMstShiresaki
  Friend WithEvents Label1 As Label
  Friend WithEvents CmbDateShiirebi1 As T.R.ZCommonCtrl.CmbDateShiirebi
  Friend WithEvents Label2 As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents lblItemDetailListStat As Label
  Friend WithEvents lblSupplierListStat As Label
  Friend WithEvents lblUnSendCount As Label
  Friend WithEvents lblPostPlanedCount As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents btnEnd As T.R.ZCommonCtrl.ButtonEnd
  Friend WithEvents btnPostPca As T.R.ZCommonCtrl.ButtonUpdate
  Friend WithEvents btnRefresh As Button
End Class
