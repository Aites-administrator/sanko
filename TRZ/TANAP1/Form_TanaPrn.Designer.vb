<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_TanaPrn
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_TanaPrn))
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.Button_End = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.CmbDateTanaorosiBi1 = New T.R.ZCommonCtrl.CmbDateTanaorosi()
    Me.TxtLblMstItem2 = New T.R.ZCommonCtrl.TxtLblMstItem()
    Me.TxtLblMstSyubetsu2 = New T.R.ZCommonCtrl.TxtLblMstSyubetsu()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label_Title_4 = New System.Windows.Forms.Label()
    Me.Label_Title_3 = New System.Windows.Forms.Label()
    Me.Label_Title_2 = New System.Windows.Forms.Label()
    Me.TxtLblMstGB1 = New T.R.ZCommonCtrl.TxtLblMstGB()
    Me.TxtLblMstGB2 = New T.R.ZCommonCtrl.TxtLblMstGB()
    Me.TxtLblMstSyubetsu1 = New T.R.ZCommonCtrl.TxtLblMstSyubetsu()
    Me.TxtLblMstItem1 = New T.R.ZCommonCtrl.TxtLblMstItem()
    Me.PrgBar = New System.Windows.Forms.ProgressBar()
    Me.Label_Title_1 = New System.Windows.Forms.Label()
    Me.Button_Hyouji = New System.Windows.Forms.Button()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'lblInformation
    '
    Me.lblInformation.BackColor = System.Drawing.Color.White
    Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Navy
    Me.lblInformation.Location = New System.Drawing.Point(0, 681)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(977, 31)
    Me.lblInformation.TabIndex = 7
    Me.lblInformation.Text = "Label18"
    '
    'Button_End
    '
    Me.Button_End.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.Button_End.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Button_End.Location = New System.Drawing.Point(654, 601)
    Me.Button_End.Name = "Button_End"
    Me.Button_End.Size = New System.Drawing.Size(160, 40)
    Me.Button_End.TabIndex = 2
    Me.Button_End.Text = "Ｆ１２：終了"
    Me.Button_End.UseVisualStyleBackColor = False
    '
    'GroupBox1
    '
    Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.GroupBox1.Controls.Add(Me.CmbDateTanaorosiBi1)
    Me.GroupBox1.Controls.Add(Me.TxtLblMstItem2)
    Me.GroupBox1.Controls.Add(Me.TxtLblMstSyubetsu2)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.Label_Title_4)
    Me.GroupBox1.Controls.Add(Me.Label_Title_3)
    Me.GroupBox1.Controls.Add(Me.Label_Title_2)
    Me.GroupBox1.Controls.Add(Me.TxtLblMstGB1)
    Me.GroupBox1.Controls.Add(Me.TxtLblMstGB2)
    Me.GroupBox1.Controls.Add(Me.TxtLblMstSyubetsu1)
    Me.GroupBox1.Controls.Add(Me.TxtLblMstItem1)
    Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(23, 43)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(920, 474)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "棚卸一覧印刷条件"
    '
    'CmbDateTanaorosiBi1
    '
    Me.CmbDateTanaorosiBi1.AvailableBlank = False
    Me.CmbDateTanaorosiBi1.DisplayMember = "ItemCode"
    Me.CmbDateTanaorosiBi1.FormattingEnabled = True
    Me.CmbDateTanaorosiBi1.Location = New System.Drawing.Point(210, 79)
    Me.CmbDateTanaorosiBi1.Name = "CmbDateTanaorosiBi1"
    Me.CmbDateTanaorosiBi1.Size = New System.Drawing.Size(203, 29)
    Me.CmbDateTanaorosiBi1.TabIndex = 0
    Me.CmbDateTanaorosiBi1.ValueMember = "ItemCode"
    '
    'TxtLblMstItem2
    '
    Me.TxtLblMstItem2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.TxtLblMstItem2.CodeTxt = ""
    Me.TxtLblMstItem2.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstItem2.Location = New System.Drawing.Point(540, 360)
    Me.TxtLblMstItem2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtLblMstItem2.Name = "TxtLblMstItem2"
    Me.TxtLblMstItem2.Size = New System.Drawing.Size(300, 60)
    Me.TxtLblMstItem2.TabIndex = 6
    Me.TxtLblMstItem2.TxtPos = False
    '
    'TxtLblMstSyubetsu2
    '
    Me.TxtLblMstSyubetsu2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.TxtLblMstSyubetsu2.CodeTxt = ""
    Me.TxtLblMstSyubetsu2.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstSyubetsu2.Location = New System.Drawing.Point(540, 260)
    Me.TxtLblMstSyubetsu2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtLblMstSyubetsu2.Name = "TxtLblMstSyubetsu2"
    Me.TxtLblMstSyubetsu2.Size = New System.Drawing.Size(300, 60)
    Me.TxtLblMstSyubetsu2.TabIndex = 4
    Me.TxtLblMstSyubetsu2.TxtPos = False
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.ForeColor = System.Drawing.Color.Black
    Me.Label3.Location = New System.Drawing.Point(460, 360)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(37, 25)
    Me.Label3.TabIndex = 12
    Me.Label3.Text = "～"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.Black
    Me.Label2.Location = New System.Drawing.Point(460, 260)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(37, 25)
    Me.Label2.TabIndex = 8
    Me.Label2.Text = "～"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.Black
    Me.Label1.Location = New System.Drawing.Point(460, 160)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(37, 25)
    Me.Label1.TabIndex = 4
    Me.Label1.Text = "～"
    '
    'Label_Title_4
    '
    Me.Label_Title_4.AutoSize = True
    Me.Label_Title_4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_4.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_4.Location = New System.Drawing.Point(50, 360)
    Me.Label_Title_4.Name = "Label_Title_4"
    Me.Label_Title_4.Size = New System.Drawing.Size(127, 21)
    Me.Label_Title_4.TabIndex = 10
    Me.Label_Title_4.Text = "部位コード(*)"
    '
    'Label_Title_3
    '
    Me.Label_Title_3.AutoSize = True
    Me.Label_Title_3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_3.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_3.Location = New System.Drawing.Point(50, 260)
    Me.Label_Title_3.Name = "Label_Title_3"
    Me.Label_Title_3.Size = New System.Drawing.Size(127, 21)
    Me.Label_Title_3.TabIndex = 6
    Me.Label_Title_3.Text = "畜種コード(*)"
    '
    'Label_Title_2
    '
    Me.Label_Title_2.AutoSize = True
    Me.Label_Title_2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_2.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_2.Location = New System.Drawing.Point(50, 160)
    Me.Label_Title_2.Name = "Label_Title_2"
    Me.Label_Title_2.Size = New System.Drawing.Size(127, 21)
    Me.Label_Title_2.TabIndex = 2
    Me.Label_Title_2.Text = "部門コード(*)"
    '
    'TxtLblMstGB1
    '
    Me.TxtLblMstGB1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.TxtLblMstGB1.CodeTxt = ""
    Me.TxtLblMstGB1.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstGB1.Location = New System.Drawing.Point(210, 160)
    Me.TxtLblMstGB1.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
    Me.TxtLblMstGB1.Name = "TxtLblMstGB1"
    Me.TxtLblMstGB1.Size = New System.Drawing.Size(300, 60)
    Me.TxtLblMstGB1.TabIndex = 1
    Me.TxtLblMstGB1.TxtPos = False
    '
    'TxtLblMstGB2
    '
    Me.TxtLblMstGB2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.TxtLblMstGB2.CodeTxt = ""
    Me.TxtLblMstGB2.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstGB2.Location = New System.Drawing.Point(540, 160)
    Me.TxtLblMstGB2.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
    Me.TxtLblMstGB2.Name = "TxtLblMstGB2"
    Me.TxtLblMstGB2.Size = New System.Drawing.Size(300, 60)
    Me.TxtLblMstGB2.TabIndex = 2
    Me.TxtLblMstGB2.TxtPos = False
    '
    'TxtLblMstSyubetsu1
    '
    Me.TxtLblMstSyubetsu1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.TxtLblMstSyubetsu1.CodeTxt = ""
    Me.TxtLblMstSyubetsu1.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstSyubetsu1.Location = New System.Drawing.Point(210, 260)
    Me.TxtLblMstSyubetsu1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtLblMstSyubetsu1.Name = "TxtLblMstSyubetsu1"
    Me.TxtLblMstSyubetsu1.Size = New System.Drawing.Size(300, 60)
    Me.TxtLblMstSyubetsu1.TabIndex = 3
    Me.TxtLblMstSyubetsu1.TxtPos = False
    '
    'TxtLblMstItem1
    '
    Me.TxtLblMstItem1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.TxtLblMstItem1.CodeTxt = ""
    Me.TxtLblMstItem1.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstItem1.Location = New System.Drawing.Point(210, 360)
    Me.TxtLblMstItem1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtLblMstItem1.Name = "TxtLblMstItem1"
    Me.TxtLblMstItem1.Size = New System.Drawing.Size(300, 60)
    Me.TxtLblMstItem1.TabIndex = 5
    Me.TxtLblMstItem1.TxtPos = False
    '
    'PrgBar
    '
    Me.PrgBar.BackColor = System.Drawing.Color.LightGray
    Me.PrgBar.Location = New System.Drawing.Point(0, 683)
    Me.PrgBar.Name = "PrgBar"
    Me.PrgBar.Size = New System.Drawing.Size(977, 29)
    Me.PrgBar.TabIndex = 19
    '
    'Label_Title_1
    '
    Me.Label_Title_1.AutoSize = True
    Me.Label_Title_1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_1.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_1.Location = New System.Drawing.Point(97, 130)
    Me.Label_Title_1.Name = "Label_Title_1"
    Me.Label_Title_1.Size = New System.Drawing.Size(73, 21)
    Me.Label_Title_1.TabIndex = 20
    Me.Label_Title_1.Text = "棚卸日"
    '
    'Button_Hyouji
    '
    Me.Button_Hyouji.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.Button_Hyouji.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Button_Hyouji.Location = New System.Drawing.Point(196, 601)
    Me.Button_Hyouji.Name = "Button_Hyouji"
    Me.Button_Hyouji.Size = New System.Drawing.Size(160, 40)
    Me.Button_Hyouji.TabIndex = 1
    Me.Button_Hyouji.Text = "Ｆ１：表示"
    Me.Button_Hyouji.UseVisualStyleBackColor = False
    '
    'Form_TanaPrn
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.ClientSize = New System.Drawing.Size(976, 710)
    Me.Controls.Add(Me.Button_Hyouji)
    Me.Controls.Add(Me.Label_Title_1)
    Me.Controls.Add(Me.Button_End)
    Me.Controls.Add(Me.PrgBar)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.lblInformation)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Location = New System.Drawing.Point(0, 50)
    Me.Name = "Form_TanaPrn"
    Me.Text = "棚卸一覧印刷条件(明細)"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lblInformation As Label
  Friend WithEvents Button_End As Button
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents PrgBar As ProgressBar
  Friend WithEvents Label_Title_1 As Label
  Friend WithEvents CmbDateTanaorosiBi1 As T.R.ZCommonCtrl.CmbDateTanaorosi
  Friend WithEvents TxtLblMstItem2 As T.R.ZCommonCtrl.TxtLblMstItem
  Friend WithEvents TxtLblMstSyubetsu2 As T.R.ZCommonCtrl.TxtLblMstSyubetsu
  Friend WithEvents Label3 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Label1 As Label
  Friend WithEvents Label_Title_4 As Label
  Friend WithEvents Label_Title_3 As Label
  Friend WithEvents Label_Title_2 As Label
  Friend WithEvents TxtLblMstGB1 As T.R.ZCommonCtrl.TxtLblMstGB
  Friend WithEvents TxtLblMstGB2 As T.R.ZCommonCtrl.TxtLblMstGB
  Friend WithEvents TxtLblMstSyubetsu1 As T.R.ZCommonCtrl.TxtLblMstSyubetsu
  Friend WithEvents TxtLblMstItem1 As T.R.ZCommonCtrl.TxtLblMstItem
  Friend WithEvents Button_Hyouji As Button
End Class
