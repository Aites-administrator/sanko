<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_Ryo
  Inherits T.R.ZCommonCtrl.MFBaseDgv

  'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
  <System.Diagnostics.DebuggerNonUserCode()>
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Windows フォーム デザイナーで必要です。
  Private components As System.ComponentModel.IContainer

  'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
  'Windows フォーム デザイナーを使用して変更できます。  
  'コード エディターを使って変更しないでください。
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Ryo))
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.CHK_P = New System.Windows.Forms.CheckBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.CmbMstCustomer_01 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.CmbDateShukaBi_01 = New T.R.ZCommonCtrl.CmbDateShukaBi()
    Me.Label_Title_Tokuisaki_01 = New System.Windows.Forms.Label()
    Me.ButtonReflesh = New T.R.ZCommonCtrl.ButtonReflesh()
    Me.Label_GridData = New System.Windows.Forms.Label()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.ButtonEnd = New T.R.ZCommonCtrl.ButtonEnd()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.ButtonPrint = New T.R.ZCommonCtrl.ButtonPrint()
    Me.GroupBox1.SuspendLayout()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.GroupBox1.Controls.Add(Me.CHK_P)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.CmbMstCustomer_01)
    Me.GroupBox1.Controls.Add(Me.CmbDateShukaBi_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_Tokuisaki_01)
    Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(5, 1)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1217, 126)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "抽出条件（空白時はALL）"
    '
    'CHK_P
    '
    Me.CHK_P.AutoSize = True
    Me.CHK_P.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CHK_P.Location = New System.Drawing.Point(686, 40)
    Me.CHK_P.Name = "CHK_P"
    Me.CHK_P.Size = New System.Drawing.Size(133, 24)
    Me.CHK_P.TabIndex = 5
    Me.CHK_P.Text = "未発行のみ"
    Me.CHK_P.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.White
    Me.Label1.Location = New System.Drawing.Point(200, 40)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(89, 19)
    Me.Label1.TabIndex = 4
    Me.Label1.Text = "得意先名"
    '
    'CmbMstCustomer_01
    '
    Me.CmbMstCustomer_01.AvailableBlank = False
    Me.CmbMstCustomer_01.DisplayMember = "ItemName"
    Me.CmbMstCustomer_01.FormattingEnabled = True
    Me.CmbMstCustomer_01.Location = New System.Drawing.Point(199, 70)
    Me.CmbMstCustomer_01.Name = "CmbMstCustomer_01"
    Me.CmbMstCustomer_01.Size = New System.Drawing.Size(729, 27)
    Me.CmbMstCustomer_01.TabIndex = 3
    Me.CmbMstCustomer_01.ValueMember = "ItemCode"
    '
    'CmbDateShukaBi_01
    '
    Me.CmbDateShukaBi_01.AvailableBlank = False
    Me.CmbDateShukaBi_01.DisplayMember = "ItemCode"
    Me.CmbDateShukaBi_01.FormattingEnabled = True
    Me.CmbDateShukaBi_01.Location = New System.Drawing.Point(19, 70)
    Me.CmbDateShukaBi_01.Name = "CmbDateShukaBi_01"
    Me.CmbDateShukaBi_01.Size = New System.Drawing.Size(169, 27)
    Me.CmbDateShukaBi_01.TabIndex = 2
    Me.CmbDateShukaBi_01.ValueMember = "ItemCode"
    '
    'Label_Title_Tokuisaki_01
    '
    Me.Label_Title_Tokuisaki_01.AutoSize = True
    Me.Label_Title_Tokuisaki_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Tokuisaki_01.ForeColor = System.Drawing.Color.White
    Me.Label_Title_Tokuisaki_01.Location = New System.Drawing.Point(25, 40)
    Me.Label_Title_Tokuisaki_01.Name = "Label_Title_Tokuisaki_01"
    Me.Label_Title_Tokuisaki_01.Size = New System.Drawing.Size(69, 19)
    Me.Label_Title_Tokuisaki_01.TabIndex = 0
    Me.Label_Title_Tokuisaki_01.Text = "出荷日"
    '
    'ButtonReflesh
    '
    Me.ButtonReflesh.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonReflesh.Image = CType(resources.GetObject("ButtonReflesh.Image"), System.Drawing.Image)
    Me.ButtonReflesh.Location = New System.Drawing.Point(1228, 6)
    Me.ButtonReflesh.Name = "ButtonReflesh"
    Me.ButtonReflesh.Size = New System.Drawing.Size(100, 120)
    Me.ButtonReflesh.TabIndex = 6
    Me.ButtonReflesh.Text = "再読込"
    Me.ButtonReflesh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonReflesh.UseVisualStyleBackColor = True
    '
    'Label_GridData
    '
    Me.Label_GridData.BackColor = System.Drawing.SystemColors.Control
    Me.Label_GridData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.Label_GridData.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_GridData.ForeColor = System.Drawing.Color.Black
    Me.Label_GridData.Location = New System.Drawing.Point(5, 131)
    Me.Label_GridData.Name = "Label_GridData"
    Me.Label_GridData.Size = New System.Drawing.Size(1525, 31)
    Me.Label_GridData.TabIndex = 4
    Me.Label_GridData.Text = "Label18"
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(5, 158)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.RowTemplate.Height = 21
    Me.DataGridView1.Size = New System.Drawing.Size(1526, 720)
    Me.DataGridView1.TabIndex = 5
    '
    'ButtonEnd
    '
    Me.ButtonEnd.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonEnd.Image = CType(resources.GetObject("ButtonEnd.Image"), System.Drawing.Image)
    Me.ButtonEnd.Location = New System.Drawing.Point(1430, 5)
    Me.ButtonEnd.Name = "ButtonEnd"
    Me.ButtonEnd.Size = New System.Drawing.Size(100, 120)
    Me.ButtonEnd.TabIndex = 3
    Me.ButtonEnd.Text = "ButtonEnd1"
    Me.ButtonEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonEnd.UseVisualStyleBackColor = True
    '
    'lblInformation
    '
    Me.lblInformation.BackColor = System.Drawing.Color.White
    Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Navy
    Me.lblInformation.Location = New System.Drawing.Point(5, 881)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(1525, 26)
    Me.lblInformation.TabIndex = 7
    Me.lblInformation.Text = "Label18"
    '
    'ButtonPrint
    '
    Me.ButtonPrint.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonPrint.Image = CType(resources.GetObject("ButtonPrint.Image"), System.Drawing.Image)
    Me.ButtonPrint.Location = New System.Drawing.Point(1329, 5)
    Me.ButtonPrint.Name = "ButtonPrint"
    Me.ButtonPrint.Size = New System.Drawing.Size(100, 120)
    Me.ButtonPrint.TabIndex = 1
    Me.ButtonPrint.Text = "ButtonPrint1"
    Me.ButtonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonPrint.UseVisualStyleBackColor = True
    '
    'Form_Ryo
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 19.0!)
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.ButtonReflesh)
    Me.Controls.Add(Me.ButtonPrint)
    Me.Controls.Add(Me.ButtonEnd)
    Me.Controls.Add(Me.Label_GridData)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.DataGridView1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_Ryo"
    Me.Text = "Form1"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Label_Title_Tokuisaki_01 As Label
  Friend WithEvents Label_GridData As Label
  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents ButtonEnd As T.R.ZCommonCtrl.ButtonEnd
  Friend WithEvents lblInformation As Label
  Friend WithEvents ButtonPrint As T.R.ZCommonCtrl.ButtonPrint
  Friend WithEvents CmbDateShukaBi_01 As T.R.ZCommonCtrl.CmbDateShukaBi
  Friend WithEvents CmbMstCustomer_01 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents CHK_P As CheckBox
  Friend WithEvents Label1 As Label
  Friend WithEvents ButtonReflesh As T.R.ZCommonCtrl.ButtonReflesh
End Class
