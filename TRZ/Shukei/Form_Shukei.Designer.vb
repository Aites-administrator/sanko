<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_Shukei
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Shukei))
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.Button_Hyouji = New System.Windows.Forms.Button()
    Me.Button_End = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.RadioButton6 = New System.Windows.Forms.RadioButton()
    Me.RadioButton5 = New System.Windows.Forms.RadioButton()
    Me.RadioButton4 = New System.Windows.Forms.RadioButton()
    Me.RadioButton3 = New System.Windows.Forms.RadioButton()
    Me.RadioButton2 = New System.Windows.Forms.RadioButton()
    Me.RadioButton1 = New System.Windows.Forms.RadioButton()
    Me.Label_Title_4 = New System.Windows.Forms.Label()
    Me.Label_Title_3 = New System.Windows.Forms.Label()
    Me.Label_Title_2 = New System.Windows.Forms.Label()
    Me.CmbDateProcessing_01 = New T.R.ZCommonCtrl.CmbDateKakouHani()
    Me.TxtEdaban_01 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.Label_Title_1 = New System.Windows.Forms.Label()
    Me.Label_Title_5 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TxtEdaban_02 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.CmbDateProcessing_02 = New T.R.ZCommonCtrl.CmbDateKakouHani()
    Me.CmbMstCustomer_01 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.CmbMstItem_01 = New T.R.ZCommonCtrl.CmbMstItem()
    Me.CmbMstCustomer_02 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.CmbMstItem_02 = New T.R.ZCommonCtrl.CmbMstItem()
    Me.PrgBar = New System.Windows.Forms.ProgressBar()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'lblInformation
    '
    Me.lblInformation.BackColor = System.Drawing.Color.White
    Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Navy
    Me.lblInformation.Location = New System.Drawing.Point(0, 475)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(1100, 31)
    Me.lblInformation.TabIndex = 7
    Me.lblInformation.Text = "Label18"
    '
    'Button_Hyouji
    '
    Me.Button_Hyouji.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.Button_Hyouji.CausesValidation = False
    Me.Button_Hyouji.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Button_Hyouji.Location = New System.Drawing.Point(98, 415)
    Me.Button_Hyouji.Name = "Button_Hyouji"
    Me.Button_Hyouji.Size = New System.Drawing.Size(160, 40)
    Me.Button_Hyouji.TabIndex = 9
    Me.Button_Hyouji.Text = "Ｆ１：表示"
    Me.Button_Hyouji.UseVisualStyleBackColor = False
    '
    'Button_End
    '
    Me.Button_End.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.Button_End.CausesValidation = False
    Me.Button_End.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Button_End.Location = New System.Drawing.Point(805, 415)
    Me.Button_End.Name = "Button_End"
    Me.Button_End.Size = New System.Drawing.Size(160, 40)
    Me.Button_End.TabIndex = 10
    Me.Button_End.Text = "Ｆ１２：終了"
    Me.Button_End.UseVisualStyleBackColor = False
    '
    'GroupBox1
    '
    Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.GroupBox1.Controls.Add(Me.RadioButton6)
    Me.GroupBox1.Controls.Add(Me.RadioButton5)
    Me.GroupBox1.Controls.Add(Me.RadioButton4)
    Me.GroupBox1.Controls.Add(Me.RadioButton3)
    Me.GroupBox1.Controls.Add(Me.RadioButton2)
    Me.GroupBox1.Controls.Add(Me.RadioButton1)
    Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(25, 283)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1035, 87)
    Me.GroupBox1.TabIndex = 8
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "出力帳票選択"
    '
    'RadioButton6
    '
    Me.RadioButton6.AutoSize = True
    Me.RadioButton6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.RadioButton6.Location = New System.Drawing.Point(314, 48)
    Me.RadioButton6.Name = "RadioButton6"
    Me.RadioButton6.Size = New System.Drawing.Size(210, 25)
    Me.RadioButton6.TabIndex = 1
    Me.RadioButton6.TabStop = True
    Me.RadioButton6.Text = "加工日報データ出力"
    Me.RadioButton6.UseVisualStyleBackColor = True
    Me.RadioButton6.Visible = False
    '
    'RadioButton5
    '
    Me.RadioButton5.AutoSize = True
    Me.RadioButton5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.RadioButton5.Location = New System.Drawing.Point(280, 140)
    Me.RadioButton5.Name = "RadioButton5"
    Me.RadioButton5.Size = New System.Drawing.Size(244, 25)
    Me.RadioButton5.TabIndex = 4
    Me.RadioButton5.TabStop = True
    Me.RadioButton5.Text = "枝別セット処理横計明細"
    Me.RadioButton5.UseVisualStyleBackColor = True
    Me.RadioButton5.Visible = False
    '
    'RadioButton4
    '
    Me.RadioButton4.AutoSize = True
    Me.RadioButton4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.RadioButton4.Location = New System.Drawing.Point(314, 109)
    Me.RadioButton4.Name = "RadioButton4"
    Me.RadioButton4.Size = New System.Drawing.Size(217, 25)
    Me.RadioButton4.TabIndex = 3
    Me.RadioButton4.TabStop = True
    Me.RadioButton4.Text = "日別得意先別集計表"
    Me.RadioButton4.UseVisualStyleBackColor = True
    Me.RadioButton4.Visible = False
    '
    'RadioButton3
    '
    Me.RadioButton3.AutoSize = True
    Me.RadioButton3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.RadioButton3.Location = New System.Drawing.Point(31, 109)
    Me.RadioButton3.Name = "RadioButton3"
    Me.RadioButton3.Size = New System.Drawing.Size(175, 25)
    Me.RadioButton3.TabIndex = 2
    Me.RadioButton3.TabStop = True
    Me.RadioButton3.Text = "得意先別集計表"
    Me.RadioButton3.UseVisualStyleBackColor = True
    Me.RadioButton3.Visible = False
    '
    'RadioButton2
    '
    Me.RadioButton2.AutoSize = True
    Me.RadioButton2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.RadioButton2.Location = New System.Drawing.Point(314, 48)
    Me.RadioButton2.Name = "RadioButton2"
    Me.RadioButton2.Size = New System.Drawing.Size(213, 25)
    Me.RadioButton2.TabIndex = 1
    Me.RadioButton2.TabStop = True
    Me.RadioButton2.Text = "パーツ処理横計明細"
    Me.RadioButton2.UseVisualStyleBackColor = True
    Me.RadioButton2.Visible = False
    '
    'RadioButton1
    '
    Me.RadioButton1.AutoSize = True
    Me.RadioButton1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.RadioButton1.Location = New System.Drawing.Point(31, 48)
    Me.RadioButton1.Name = "RadioButton1"
    Me.RadioButton1.Size = New System.Drawing.Size(202, 25)
    Me.RadioButton1.TabIndex = 0
    Me.RadioButton1.TabStop = True
    Me.RadioButton1.Text = "セット処理横計明細"
    Me.RadioButton1.UseVisualStyleBackColor = True
    '
    'Label_Title_4
    '
    Me.Label_Title_4.AutoSize = True
    Me.Label_Title_4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_4.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_4.Location = New System.Drawing.Point(20, 200)
    Me.Label_Title_4.Name = "Label_Title_4"
    Me.Label_Title_4.Size = New System.Drawing.Size(98, 21)
    Me.Label_Title_4.TabIndex = 14
    Me.Label_Title_4.Text = "部位範囲"
    '
    'Label_Title_3
    '
    Me.Label_Title_3.AutoSize = True
    Me.Label_Title_3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_3.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_3.Location = New System.Drawing.Point(20, 150)
    Me.Label_Title_3.Name = "Label_Title_3"
    Me.Label_Title_3.Size = New System.Drawing.Size(98, 21)
    Me.Label_Title_3.TabIndex = 13
    Me.Label_Title_3.Text = "枝番範囲"
    '
    'Label_Title_2
    '
    Me.Label_Title_2.AutoSize = True
    Me.Label_Title_2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_2.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_2.Location = New System.Drawing.Point(20, 100)
    Me.Label_Title_2.Name = "Label_Title_2"
    Me.Label_Title_2.Size = New System.Drawing.Size(120, 21)
    Me.Label_Title_2.TabIndex = 12
    Me.Label_Title_2.Text = "得意先範囲"
    '
    'CmbDateProcessing_01
    '
    Me.CmbDateProcessing_01.AvailableBlank = False
    Me.CmbDateProcessing_01.DisplayMember = "ItemCode"
    Me.CmbDateProcessing_01.DropDownWidth = 140
    Me.CmbDateProcessing_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbDateProcessing_01.FormattingEnabled = True
    Me.CmbDateProcessing_01.Location = New System.Drawing.Point(144, 50)
    Me.CmbDateProcessing_01.Name = "CmbDateProcessing_01"
    Me.CmbDateProcessing_01.Size = New System.Drawing.Size(200, 29)
    Me.CmbDateProcessing_01.TabIndex = 0
    Me.CmbDateProcessing_01.ValueMember = "ItemCode"
    '
    'TxtEdaban_01
    '
    Me.TxtEdaban_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtEdaban_01.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban_01.Location = New System.Drawing.Point(144, 150)
    Me.TxtEdaban_01.Name = "TxtEdaban_01"
    Me.TxtEdaban_01.Size = New System.Drawing.Size(150, 28)
    Me.TxtEdaban_01.TabIndex = 4
    Me.TxtEdaban_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label_Title_1
    '
    Me.Label_Title_1.AutoSize = True
    Me.Label_Title_1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_1.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_1.Location = New System.Drawing.Point(20, 50)
    Me.Label_Title_1.Name = "Label_Title_1"
    Me.Label_Title_1.Size = New System.Drawing.Size(98, 21)
    Me.Label_Title_1.TabIndex = 9
    Me.Label_Title_1.Text = "日付範囲"
    '
    'Label_Title_5
    '
    Me.Label_Title_5.AutoSize = True
    Me.Label_Title_5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_5.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_5.Location = New System.Drawing.Point(580, 50)
    Me.Label_Title_5.Name = "Label_Title_5"
    Me.Label_Title_5.Size = New System.Drawing.Size(38, 25)
    Me.Label_Title_5.TabIndex = 15
    Me.Label_Title_5.Text = "～"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.Black
    Me.Label1.Location = New System.Drawing.Point(580, 100)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(38, 25)
    Me.Label1.TabIndex = 16
    Me.Label1.Text = "～"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.Black
    Me.Label2.Location = New System.Drawing.Point(580, 150)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(38, 25)
    Me.Label2.TabIndex = 17
    Me.Label2.Text = "～"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.ForeColor = System.Drawing.Color.Black
    Me.Label3.Location = New System.Drawing.Point(580, 200)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(38, 25)
    Me.Label3.TabIndex = 18
    Me.Label3.Text = "～"
    '
    'TxtEdaban_02
    '
    Me.TxtEdaban_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtEdaban_02.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban_02.Location = New System.Drawing.Point(640, 150)
    Me.TxtEdaban_02.Name = "TxtEdaban_02"
    Me.TxtEdaban_02.Size = New System.Drawing.Size(150, 28)
    Me.TxtEdaban_02.TabIndex = 5
    Me.TxtEdaban_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'CmbDateProcessing_02
    '
    Me.CmbDateProcessing_02.AvailableBlank = False
    Me.CmbDateProcessing_02.DisplayMember = "ItemCode"
    Me.CmbDateProcessing_02.DropDownWidth = 140
    Me.CmbDateProcessing_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbDateProcessing_02.FormattingEnabled = True
    Me.CmbDateProcessing_02.Location = New System.Drawing.Point(640, 50)
    Me.CmbDateProcessing_02.Name = "CmbDateProcessing_02"
    Me.CmbDateProcessing_02.Size = New System.Drawing.Size(200, 29)
    Me.CmbDateProcessing_02.TabIndex = 1
    Me.CmbDateProcessing_02.ValueMember = "ItemCode"
    '
    'CmbMstCustomer_01
    '
    Me.CmbMstCustomer_01.AvailableBlank = False
    Me.CmbMstCustomer_01.DisplayMember = "ItemName"
    Me.CmbMstCustomer_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstCustomer_01.FormattingEnabled = True
    Me.CmbMstCustomer_01.Location = New System.Drawing.Point(144, 100)
    Me.CmbMstCustomer_01.Name = "CmbMstCustomer_01"
    Me.CmbMstCustomer_01.Size = New System.Drawing.Size(420, 29)
    Me.CmbMstCustomer_01.TabIndex = 2
    Me.CmbMstCustomer_01.ValueMember = "ItemCode"
    '
    'CmbMstItem_01
    '
    Me.CmbMstItem_01.AvailableBlank = False
    Me.CmbMstItem_01.DisplayMember = "ItemName"
    Me.CmbMstItem_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstItem_01.FormattingEnabled = True
    Me.CmbMstItem_01.Location = New System.Drawing.Point(144, 200)
    Me.CmbMstItem_01.Name = "CmbMstItem_01"
    Me.CmbMstItem_01.Size = New System.Drawing.Size(420, 29)
    Me.CmbMstItem_01.TabIndex = 6
    Me.CmbMstItem_01.ValueMember = "ItemCode"
    '
    'CmbMstCustomer_02
    '
    Me.CmbMstCustomer_02.AvailableBlank = False
    Me.CmbMstCustomer_02.DisplayMember = "ItemName"
    Me.CmbMstCustomer_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstCustomer_02.FormattingEnabled = True
    Me.CmbMstCustomer_02.Location = New System.Drawing.Point(640, 100)
    Me.CmbMstCustomer_02.Name = "CmbMstCustomer_02"
    Me.CmbMstCustomer_02.Size = New System.Drawing.Size(420, 29)
    Me.CmbMstCustomer_02.TabIndex = 3
    Me.CmbMstCustomer_02.ValueMember = "ItemCode"
    '
    'CmbMstItem_02
    '
    Me.CmbMstItem_02.AvailableBlank = False
    Me.CmbMstItem_02.DisplayMember = "ItemName"
    Me.CmbMstItem_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstItem_02.FormattingEnabled = True
    Me.CmbMstItem_02.Location = New System.Drawing.Point(640, 200)
    Me.CmbMstItem_02.Name = "CmbMstItem_02"
    Me.CmbMstItem_02.Size = New System.Drawing.Size(420, 29)
    Me.CmbMstItem_02.TabIndex = 7
    Me.CmbMstItem_02.ValueMember = "ItemCode"
    '
    'PrgBar
    '
    Me.PrgBar.BackColor = System.Drawing.Color.LightGray
    Me.PrgBar.Location = New System.Drawing.Point(0, 477)
    Me.PrgBar.Name = "PrgBar"
    Me.PrgBar.Size = New System.Drawing.Size(1100, 29)
    Me.PrgBar.TabIndex = 19
    '
    'Form_Shukei
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.ClientSize = New System.Drawing.Size(1084, 507)
    Me.Controls.Add(Me.PrgBar)
    Me.Controls.Add(Me.CmbMstItem_02)
    Me.Controls.Add(Me.CmbMstCustomer_02)
    Me.Controls.Add(Me.CmbMstItem_01)
    Me.Controls.Add(Me.CmbMstCustomer_01)
    Me.Controls.Add(Me.CmbDateProcessing_02)
    Me.Controls.Add(Me.TxtEdaban_02)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Label_Title_5)
    Me.Controls.Add(Me.Label_Title_4)
    Me.Controls.Add(Me.Label_Title_3)
    Me.Controls.Add(Me.Label_Title_2)
    Me.Controls.Add(Me.CmbDateProcessing_01)
    Me.Controls.Add(Me.TxtEdaban_01)
    Me.Controls.Add(Me.Label_Title_1)
    Me.Controls.Add(Me.Button_End)
    Me.Controls.Add(Me.Button_Hyouji)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.GroupBox1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Location = New System.Drawing.Point(0, 50)
    Me.Name = "Form_Shukei"
    Me.Text = "加工実績集計表"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lblInformation As Label
  Friend WithEvents Button_Hyouji As Button
  Friend WithEvents Button_End As Button
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Label_Title_4 As Label
  Friend WithEvents Label_Title_3 As Label
  Friend WithEvents Label_Title_2 As Label
  Friend WithEvents CmbDateProcessing_01 As T.R.ZCommonCtrl.CmbDateKakouHani
  Friend WithEvents TxtEdaban_01 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents Label_Title_1 As Label
  Friend WithEvents Label_Title_5 As Label
  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents TxtEdaban_02 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents CmbDateProcessing_02 As T.R.ZCommonCtrl.CmbDateKakouHani
  Friend WithEvents CmbMstCustomer_01 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents CmbMstItem_01 As T.R.ZCommonCtrl.CmbMstItem
  Friend WithEvents RadioButton6 As RadioButton
  Friend WithEvents RadioButton5 As RadioButton
  Friend WithEvents RadioButton4 As RadioButton
  Friend WithEvents RadioButton3 As RadioButton
  Friend WithEvents RadioButton2 As RadioButton
  Friend WithEvents RadioButton1 As RadioButton
  Friend WithEvents CmbMstCustomer_02 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents CmbMstItem_02 As T.R.ZCommonCtrl.CmbMstItem
  Friend WithEvents PrgBar As ProgressBar
End Class
