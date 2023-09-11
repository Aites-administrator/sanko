<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_Zaiko
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Zaiko))
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.DataGridView2 = New System.Windows.Forms.DataGridView()
    Me.Label_GridData = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.TxtKakoubi_04 = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TxtKakoubi_03 = New System.Windows.Forms.TextBox()
    Me.TxtKakoubi_02 = New System.Windows.Forms.TextBox()
    Me.TxtKakoubi_01 = New System.Windows.Forms.TextBox()
    Me.CmbMstItem_01 = New T.R.ZCommonCtrl.CmbMstItem()
    Me.CheckBox_Sample02 = New System.Windows.Forms.CheckBox()
    Me.CheckBox_Sample01 = New System.Windows.Forms.CheckBox()
    Me.CheckBox_EdaBetu = New System.Windows.Forms.CheckBox()
    Me.CmbMstSetType_01 = New T.R.ZCommonCtrl.CmbMstSetType()
    Me.CmbMstStaff1 = New T.R.ZCommonCtrl.CmbMstStaff()
    Me.TxtEdaban_01 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.CmbMstCustomer_01 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.Label_Title_EdaNo_01 = New System.Windows.Forms.Label()
    Me.Label_Title_SyouhinMei = New System.Windows.Forms.Label()
    Me.Label_Title_SetCd_01 = New System.Windows.Forms.Label()
    Me.Label_Title_Tokuisaki_01 = New System.Windows.Forms.Label()
    Me.Label_Title_TANTOU = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.Frame_IN = New System.Windows.Forms.GroupBox()
    Me.Label_KotaiNo = New System.Windows.Forms.Label()
    Me.ButtonSyuka = New T.R.ZCommonCtrl.ButtonSyuka()
    Me.ButtonUpdate = New T.R.ZCommonCtrl.ButtonUpdate()
    Me.TxBaika_02 = New T.R.ZCommonCtrl.TxBaika()
    Me.TxTanka_02 = New T.R.ZCommonCtrl.TxTanka()
    Me.CmbMstSetType_02 = New T.R.ZCommonCtrl.CmbMstSetType()
    Me.CmbMstCustomer_02 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.Label_Title_Jyuryo = New System.Windows.Forms.Label()
    Me.Label_Jyuryo = New System.Windows.Forms.Label()
    Me.Label_Sayu = New System.Windows.Forms.Label()
    Me.Label_EdaNo_02 = New System.Windows.Forms.Label()
    Me.Label_Title_Tanka = New System.Windows.Forms.Label()
    Me.Label_Title_Baika = New System.Windows.Forms.Label()
    Me.Label_Title_SetCd_02 = New System.Windows.Forms.Label()
    Me.Label_SeizouBi = New System.Windows.Forms.Label()
    Me.Label_Title_SeizouBi = New System.Windows.Forms.Label()
    Me.Label1_Title_Tokuisaki_02 = New System.Windows.Forms.Label()
    Me.Label_Title_EdaNo_02 = New System.Windows.Forms.Label()
    Me.ButtonEnd = New T.R.ZCommonCtrl.ButtonEnd()
    Me.ButtonPrint = New T.R.ZCommonCtrl.ButtonPrint()
    Me.ButtonTorikeshi = New T.R.ZCommonCtrl.ButtonCancel()
    Me.Panel_Msg = New System.Windows.Forms.Panel()
    Me.Label_Kensu = New System.Windows.Forms.Label()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.Frame_IN.SuspendLayout()
    Me.Panel_Msg.SuspendLayout()
    Me.SuspendLayout()
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(164, 230)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.RowTemplate.Height = 21
    Me.DataGridView1.Size = New System.Drawing.Size(1526, 695)
    Me.DataGridView1.TabIndex = 2
    '
    'DataGridView2
    '
    Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView2.Location = New System.Drawing.Point(5, 165)
    Me.DataGridView2.Name = "DataGridView2"
    Me.DataGridView2.RowTemplate.Height = 21
    Me.DataGridView2.Size = New System.Drawing.Size(1526, 685)
    Me.DataGridView2.TabIndex = 1
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
    Me.Label_GridData.TabIndex = 14
    Me.Label_GridData.Text = "Label18"
    '
    'GroupBox1
    '
    Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.TxtKakoubi_04)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.TxtKakoubi_03)
    Me.GroupBox1.Controls.Add(Me.TxtKakoubi_02)
    Me.GroupBox1.Controls.Add(Me.TxtKakoubi_01)
    Me.GroupBox1.Controls.Add(Me.CmbMstItem_01)
    Me.GroupBox1.Controls.Add(Me.CheckBox_Sample02)
    Me.GroupBox1.Controls.Add(Me.CheckBox_Sample01)
    Me.GroupBox1.Controls.Add(Me.CheckBox_EdaBetu)
    Me.GroupBox1.Controls.Add(Me.CmbMstSetType_01)
    Me.GroupBox1.Controls.Add(Me.CmbMstStaff1)
    Me.GroupBox1.Controls.Add(Me.TxtEdaban_01)
    Me.GroupBox1.Controls.Add(Me.CmbMstCustomer_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_EdaNo_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_SyouhinMei)
    Me.GroupBox1.Controls.Add(Me.Label_Title_SetCd_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_Tokuisaki_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_TANTOU)
    Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(9, 1)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1300, 126)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "抽出条件（空白時はALL）"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.White
    Me.Label2.Location = New System.Drawing.Point(271, 46)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(168, 15)
    Me.Label2.TabIndex = 20
    Me.Label2.Text = "△✖共にチェック時条件"
    Me.Label2.Visible = False
    '
    'TxtKakoubi_04
    '
    Me.TxtKakoubi_04.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 10.86792!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtKakoubi_04.Location = New System.Drawing.Point(337, 64)
    Me.TxtKakoubi_04.Name = "TxtKakoubi_04"
    Me.TxtKakoubi_04.ReadOnly = True
    Me.TxtKakoubi_04.Size = New System.Drawing.Size(104, 23)
    Me.TxtKakoubi_04.TabIndex = 19
    Me.TxtKakoubi_04.Visible = False
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label4.ForeColor = System.Drawing.Color.White
    Me.Label4.Location = New System.Drawing.Point(1211, 82)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(35, 21)
    Me.Label4.TabIndex = 17
    Me.Label4.Text = "LT"
    Me.Label4.Visible = False
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.ForeColor = System.Drawing.Color.White
    Me.Label3.Location = New System.Drawing.Point(1211, 50)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(51, 21)
    Me.Label3.TabIndex = 15
    Me.Label3.Text = "GTE"
    Me.Label3.Visible = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.White
    Me.Label1.Location = New System.Drawing.Point(1211, 16)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(48, 21)
    Me.Label1.TabIndex = 13
    Me.Label1.Text = "LTE"
    Me.Label1.Visible = False
    '
    'TxtKakoubi_03
    '
    Me.TxtKakoubi_03.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 10.86792!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtKakoubi_03.Location = New System.Drawing.Point(1267, 81)
    Me.TxtKakoubi_03.Name = "TxtKakoubi_03"
    Me.TxtKakoubi_03.ReadOnly = True
    Me.TxtKakoubi_03.Size = New System.Drawing.Size(104, 23)
    Me.TxtKakoubi_03.TabIndex = 18
    Me.TxtKakoubi_03.Visible = False
    '
    'TxtKakoubi_02
    '
    Me.TxtKakoubi_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 10.86792!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtKakoubi_02.Location = New System.Drawing.Point(1267, 48)
    Me.TxtKakoubi_02.Name = "TxtKakoubi_02"
    Me.TxtKakoubi_02.ReadOnly = True
    Me.TxtKakoubi_02.Size = New System.Drawing.Size(104, 23)
    Me.TxtKakoubi_02.TabIndex = 16
    Me.TxtKakoubi_02.Visible = False
    '
    'TxtKakoubi_01
    '
    Me.TxtKakoubi_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 10.86792!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtKakoubi_01.Location = New System.Drawing.Point(1267, 16)
    Me.TxtKakoubi_01.Name = "TxtKakoubi_01"
    Me.TxtKakoubi_01.ReadOnly = True
    Me.TxtKakoubi_01.Size = New System.Drawing.Size(104, 23)
    Me.TxtKakoubi_01.TabIndex = 14
    Me.TxtKakoubi_01.Visible = False
    '
    'CmbMstItem_01
    '
    Me.CmbMstItem_01.AvailableBlank = False
    Me.CmbMstItem_01.DisplayMember = "ItemName"
    Me.CmbMstItem_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstItem_01.FormattingEnabled = True
    Me.CmbMstItem_01.Location = New System.Drawing.Point(875, 93)
    Me.CmbMstItem_01.Name = "CmbMstItem_01"
    Me.CmbMstItem_01.Size = New System.Drawing.Size(330, 29)
    Me.CmbMstItem_01.TabIndex = 12
    Me.CmbMstItem_01.ValueMember = "ItemCode"
    '
    'CheckBox_Sample02
    '
    Me.CheckBox_Sample02.AutoSize = True
    Me.CheckBox_Sample02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CheckBox_Sample02.ForeColor = System.Drawing.Color.Red
    Me.CheckBox_Sample02.Location = New System.Drawing.Point(31, 44)
    Me.CheckBox_Sample02.Name = "CheckBox_Sample02"
    Me.CheckBox_Sample02.Size = New System.Drawing.Size(224, 21)
    Me.CheckBox_Sample02.TabIndex = 1
    Me.CheckBox_Sample02.Text = "×抽出（製造後2週間経過）"
    Me.CheckBox_Sample02.UseVisualStyleBackColor = True
    '
    'CheckBox_Sample01
    '
    Me.CheckBox_Sample01.AutoSize = True
    Me.CheckBox_Sample01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CheckBox_Sample01.ForeColor = System.Drawing.Color.Yellow
    Me.CheckBox_Sample01.Location = New System.Drawing.Point(31, 18)
    Me.CheckBox_Sample01.Name = "CheckBox_Sample01"
    Me.CheckBox_Sample01.Size = New System.Drawing.Size(280, 21)
    Me.CheckBox_Sample01.TabIndex = 0
    Me.CheckBox_Sample01.Text = "△抽出（製造後1週間～2週間まで）"
    Me.CheckBox_Sample01.UseVisualStyleBackColor = True
    '
    'CheckBox_EdaBetu
    '
    Me.CheckBox_EdaBetu.AutoSize = True
    Me.CheckBox_EdaBetu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CheckBox_EdaBetu.ForeColor = System.Drawing.Color.Lime
    Me.CheckBox_EdaBetu.Location = New System.Drawing.Point(325, 18)
    Me.CheckBox_EdaBetu.Name = "CheckBox_EdaBetu"
    Me.CheckBox_EdaBetu.Size = New System.Drawing.Size(129, 21)
    Me.CheckBox_EdaBetu.TabIndex = 2
    Me.CheckBox_EdaBetu.Text = "枝別合計表示"
    Me.CheckBox_EdaBetu.UseVisualStyleBackColor = True
    '
    'CmbMstSetType_01
    '
    Me.CmbMstSetType_01.AvailableBlank = False
    Me.CmbMstSetType_01.DisplayMember = "ItemName"
    Me.CmbMstSetType_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstSetType_01.FormattingEnabled = True
    Me.CmbMstSetType_01.Location = New System.Drawing.Point(569, 93)
    Me.CmbMstSetType_01.Name = "CmbMstSetType_01"
    Me.CmbMstSetType_01.Size = New System.Drawing.Size(300, 29)
    Me.CmbMstSetType_01.TabIndex = 10
    Me.CmbMstSetType_01.ValueMember = "ItemCode"
    '
    'CmbMstStaff1
    '
    Me.CmbMstStaff1.AvailableBlank = False
    Me.CmbMstStaff1.DisplayMember = "ItemName"
    Me.CmbMstStaff1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstStaff1.FormattingEnabled = True
    Me.CmbMstStaff1.Location = New System.Drawing.Point(875, 20)
    Me.CmbMstStaff1.Name = "CmbMstStaff1"
    Me.CmbMstStaff1.Size = New System.Drawing.Size(330, 29)
    Me.CmbMstStaff1.TabIndex = 4
    Me.CmbMstStaff1.ValueMember = "ItemCode"
    '
    'TxtEdaban_01
    '
    Me.TxtEdaban_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtEdaban_01.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban_01.Location = New System.Drawing.Point(499, 93)
    Me.TxtEdaban_01.Name = "TxtEdaban_01"
    Me.TxtEdaban_01.Size = New System.Drawing.Size(62, 28)
    Me.TxtEdaban_01.TabIndex = 8
    Me.TxtEdaban_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'CmbMstCustomer_01
    '
    Me.CmbMstCustomer_01.AvailableBlank = False
    Me.CmbMstCustomer_01.DisplayMember = "ItemName"
    Me.CmbMstCustomer_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstCustomer_01.FormattingEnabled = True
    Me.CmbMstCustomer_01.Location = New System.Drawing.Point(12, 93)
    Me.CmbMstCustomer_01.Name = "CmbMstCustomer_01"
    Me.CmbMstCustomer_01.Size = New System.Drawing.Size(480, 29)
    Me.CmbMstCustomer_01.TabIndex = 6
    Me.CmbMstCustomer_01.ValueMember = "ItemCode"
    '
    'Label_Title_EdaNo_01
    '
    Me.Label_Title_EdaNo_01.AutoSize = True
    Me.Label_Title_EdaNo_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_EdaNo_01.ForeColor = System.Drawing.Color.White
    Me.Label_Title_EdaNo_01.Location = New System.Drawing.Point(495, 69)
    Me.Label_Title_EdaNo_01.Name = "Label_Title_EdaNo_01"
    Me.Label_Title_EdaNo_01.Size = New System.Drawing.Size(32, 21)
    Me.Label_Title_EdaNo_01.TabIndex = 7
    Me.Label_Title_EdaNo_01.Text = "枝"
    '
    'Label_Title_SyouhinMei
    '
    Me.Label_Title_SyouhinMei.AutoSize = True
    Me.Label_Title_SyouhinMei.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_SyouhinMei.ForeColor = System.Drawing.Color.White
    Me.Label_Title_SyouhinMei.Location = New System.Drawing.Point(877, 69)
    Me.Label_Title_SyouhinMei.Name = "Label_Title_SyouhinMei"
    Me.Label_Title_SyouhinMei.Size = New System.Drawing.Size(76, 21)
    Me.Label_Title_SyouhinMei.TabIndex = 11
    Me.Label_Title_SyouhinMei.Text = "商品名"
    '
    'Label_Title_SetCd_01
    '
    Me.Label_Title_SetCd_01.AutoSize = True
    Me.Label_Title_SetCd_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_SetCd_01.ForeColor = System.Drawing.Color.White
    Me.Label_Title_SetCd_01.Location = New System.Drawing.Point(565, 69)
    Me.Label_Title_SetCd_01.Name = "Label_Title_SetCd_01"
    Me.Label_Title_SetCd_01.Size = New System.Drawing.Size(61, 21)
    Me.Label_Title_SetCd_01.TabIndex = 9
    Me.Label_Title_SetCd_01.Text = "セット"
    '
    'Label_Title_Tokuisaki_01
    '
    Me.Label_Title_Tokuisaki_01.AutoSize = True
    Me.Label_Title_Tokuisaki_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Tokuisaki_01.ForeColor = System.Drawing.Color.White
    Me.Label_Title_Tokuisaki_01.Location = New System.Drawing.Point(12, 69)
    Me.Label_Title_Tokuisaki_01.Name = "Label_Title_Tokuisaki_01"
    Me.Label_Title_Tokuisaki_01.Size = New System.Drawing.Size(98, 21)
    Me.Label_Title_Tokuisaki_01.TabIndex = 5
    Me.Label_Title_Tokuisaki_01.Text = "得意先名"
    '
    'Label_Title_TANTOU
    '
    Me.Label_Title_TANTOU.AutoSize = True
    Me.Label_Title_TANTOU.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_TANTOU.ForeColor = System.Drawing.Color.White
    Me.Label_Title_TANTOU.Location = New System.Drawing.Point(781, 23)
    Me.Label_Title_TANTOU.Name = "Label_Title_TANTOU"
    Me.Label_Title_TANTOU.Size = New System.Drawing.Size(76, 21)
    Me.Label_Title_TANTOU.TabIndex = 3
    Me.Label_Title_TANTOU.Text = "担当者"
    '
    'lblInformation
    '
    Me.lblInformation.BackColor = System.Drawing.Color.White
    Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Navy
    Me.lblInformation.Location = New System.Drawing.Point(0, 880)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(1535, 29)
    Me.lblInformation.TabIndex = 17
    Me.lblInformation.Text = "Label18"
    '
    'Frame_IN
    '
    Me.Frame_IN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Frame_IN.Controls.Add(Me.Label_KotaiNo)
    Me.Frame_IN.Controls.Add(Me.ButtonSyuka)
    Me.Frame_IN.Controls.Add(Me.ButtonUpdate)
    Me.Frame_IN.Controls.Add(Me.TxBaika_02)
    Me.Frame_IN.Controls.Add(Me.TxTanka_02)
    Me.Frame_IN.Controls.Add(Me.CmbMstSetType_02)
    Me.Frame_IN.Controls.Add(Me.CmbMstCustomer_02)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Jyuryo)
    Me.Frame_IN.Controls.Add(Me.Label_Jyuryo)
    Me.Frame_IN.Controls.Add(Me.Label_Sayu)
    Me.Frame_IN.Controls.Add(Me.Label_EdaNo_02)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Tanka)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Baika)
    Me.Frame_IN.Controls.Add(Me.Label_Title_SetCd_02)
    Me.Frame_IN.Controls.Add(Me.Label_SeizouBi)
    Me.Frame_IN.Controls.Add(Me.Label_Title_SeizouBi)
    Me.Frame_IN.Controls.Add(Me.Label1_Title_Tokuisaki_02)
    Me.Frame_IN.Controls.Add(Me.Label_Title_EdaNo_02)
    Me.Frame_IN.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Frame_IN.Location = New System.Drawing.Point(0, 765)
    Me.Frame_IN.Name = "Frame_IN"
    Me.Frame_IN.Size = New System.Drawing.Size(1534, 114)
    Me.Frame_IN.TabIndex = 3
    Me.Frame_IN.TabStop = False
    '
    'Label_KotaiNo
    '
    Me.Label_KotaiNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_KotaiNo.ForeColor = System.Drawing.Color.Black
    Me.Label_KotaiNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.Label_KotaiNo.Location = New System.Drawing.Point(684, 13)
    Me.Label_KotaiNo.Name = "Label_KotaiNo"
    Me.Label_KotaiNo.Size = New System.Drawing.Size(146, 21)
    Me.Label_KotaiNo.TabIndex = 20
    Me.Label_KotaiNo.Text = "1234 "
    Me.Label_KotaiNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'ButtonSyuka
    '
    Me.ButtonSyuka.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonSyuka.Image = CType(resources.GetObject("ButtonSyuka.Image"), System.Drawing.Image)
    Me.ButtonSyuka.Location = New System.Drawing.Point(1453, 13)
    Me.ButtonSyuka.Name = "ButtonSyuka"
    Me.ButtonSyuka.Size = New System.Drawing.Size(75, 95)
    Me.ButtonSyuka.TabIndex = 17
    Me.ButtonSyuka.Text = "ButtonSyuka1"
    Me.ButtonSyuka.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonSyuka.UseVisualStyleBackColor = False
    '
    'ButtonUpdate
    '
    Me.ButtonUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonUpdate.Image = CType(resources.GetObject("ButtonUpdate.Image"), System.Drawing.Image)
    Me.ButtonUpdate.Location = New System.Drawing.Point(1372, 14)
    Me.ButtonUpdate.Name = "ButtonUpdate"
    Me.ButtonUpdate.Size = New System.Drawing.Size(75, 95)
    Me.ButtonUpdate.TabIndex = 16
    Me.ButtonUpdate.Text = "ButtonUpdate1"
    Me.ButtonUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonUpdate.UseVisualStyleBackColor = False
    '
    'TxBaika_02
    '
    Me.TxBaika_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxBaika_02.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxBaika_02.Location = New System.Drawing.Point(853, 80)
    Me.TxBaika_02.Name = "TxBaika_02"
    Me.TxBaika_02.Size = New System.Drawing.Size(120, 28)
    Me.TxBaika_02.TabIndex = 13
    Me.TxBaika_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxTanka_02
    '
    Me.TxTanka_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxTanka_02.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxTanka_02.Location = New System.Drawing.Point(1003, 80)
    Me.TxTanka_02.Name = "TxTanka_02"
    Me.TxTanka_02.Size = New System.Drawing.Size(120, 28)
    Me.TxTanka_02.TabIndex = 15
    Me.TxTanka_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'CmbMstSetType_02
    '
    Me.CmbMstSetType_02.AvailableBlank = False
    Me.CmbMstSetType_02.DisplayMember = "ItemName"
    Me.CmbMstSetType_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstSetType_02.FormattingEnabled = True
    Me.CmbMstSetType_02.Location = New System.Drawing.Point(530, 80)
    Me.CmbMstSetType_02.Name = "CmbMstSetType_02"
    Me.CmbMstSetType_02.Size = New System.Drawing.Size(300, 29)
    Me.CmbMstSetType_02.TabIndex = 11
    Me.CmbMstSetType_02.ValueMember = "ItemCode"
    '
    'CmbMstCustomer_02
    '
    Me.CmbMstCustomer_02.AvailableBlank = False
    Me.CmbMstCustomer_02.DisplayMember = "ItemName"
    Me.CmbMstCustomer_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstCustomer_02.FormattingEnabled = True
    Me.CmbMstCustomer_02.Location = New System.Drawing.Point(25, 80)
    Me.CmbMstCustomer_02.Name = "CmbMstCustomer_02"
    Me.CmbMstCustomer_02.Size = New System.Drawing.Size(480, 29)
    Me.CmbMstCustomer_02.TabIndex = 9
    Me.CmbMstCustomer_02.ValueMember = "ItemCode"
    '
    'Label_Title_Jyuryo
    '
    Me.Label_Title_Jyuryo.AutoSize = True
    Me.Label_Title_Jyuryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_Jyuryo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Jyuryo.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Jyuryo.Location = New System.Drawing.Point(542, 13)
    Me.Label_Title_Jyuryo.Name = "Label_Title_Jyuryo"
    Me.Label_Title_Jyuryo.Size = New System.Drawing.Size(54, 21)
    Me.Label_Title_Jyuryo.TabIndex = 6
    Me.Label_Title_Jyuryo.Text = "重量"
    Me.Label_Title_Jyuryo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.Label_Title_Jyuryo.Visible = False
    '
    'Label_Jyuryo
    '
    Me.Label_Jyuryo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Jyuryo.ForeColor = System.Drawing.Color.Black
    Me.Label_Jyuryo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.Label_Jyuryo.Location = New System.Drawing.Point(598, 13)
    Me.Label_Jyuryo.Name = "Label_Jyuryo"
    Me.Label_Jyuryo.Size = New System.Drawing.Size(80, 21)
    Me.Label_Jyuryo.TabIndex = 7
    Me.Label_Jyuryo.Text = "1234 "
    Me.Label_Jyuryo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.Label_Jyuryo.Visible = False
    '
    'Label_Sayu
    '
    Me.Label_Sayu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Sayu.ForeColor = System.Drawing.Color.Black
    Me.Label_Sayu.Location = New System.Drawing.Point(410, 13)
    Me.Label_Sayu.Name = "Label_Sayu"
    Me.Label_Sayu.Size = New System.Drawing.Size(69, 21)
    Me.Label_Sayu.TabIndex = 5
    Me.Label_Sayu.Text = "右"
    Me.Label_Sayu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label_EdaNo_02
    '
    Me.Label_EdaNo_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_EdaNo_02.ForeColor = System.Drawing.Color.Black
    Me.Label_EdaNo_02.Location = New System.Drawing.Point(326, 13)
    Me.Label_EdaNo_02.Name = "Label_EdaNo_02"
    Me.Label_EdaNo_02.Size = New System.Drawing.Size(69, 21)
    Me.Label_EdaNo_02.TabIndex = 3
    Me.Label_EdaNo_02.Text = "1234"
    Me.Label_EdaNo_02.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label_Title_Tanka
    '
    Me.Label_Title_Tanka.AutoSize = True
    Me.Label_Title_Tanka.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Tanka.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Tanka.Location = New System.Drawing.Point(1003, 54)
    Me.Label_Title_Tanka.Name = "Label_Title_Tanka"
    Me.Label_Title_Tanka.Size = New System.Drawing.Size(98, 21)
    Me.Label_Title_Tanka.TabIndex = 14
    Me.Label_Title_Tanka.Text = "入荷単価"
    '
    'Label_Title_Baika
    '
    Me.Label_Title_Baika.AutoSize = True
    Me.Label_Title_Baika.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Baika.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Baika.Location = New System.Drawing.Point(853, 54)
    Me.Label_Title_Baika.Name = "Label_Title_Baika"
    Me.Label_Title_Baika.Size = New System.Drawing.Size(98, 21)
    Me.Label_Title_Baika.TabIndex = 12
    Me.Label_Title_Baika.Text = "売上単価"
    '
    'Label_Title_SetCd_02
    '
    Me.Label_Title_SetCd_02.AutoSize = True
    Me.Label_Title_SetCd_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_SetCd_02.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_SetCd_02.Location = New System.Drawing.Point(530, 54)
    Me.Label_Title_SetCd_02.Name = "Label_Title_SetCd_02"
    Me.Label_Title_SetCd_02.Size = New System.Drawing.Size(105, 21)
    Me.Label_Title_SetCd_02.TabIndex = 10
    Me.Label_Title_SetCd_02.Text = "セット区分"
    '
    'Label_SeizouBi
    '
    Me.Label_SeizouBi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_SeizouBi.ForeColor = System.Drawing.Color.Black
    Me.Label_SeizouBi.Location = New System.Drawing.Point(111, 13)
    Me.Label_SeizouBi.Name = "Label_SeizouBi"
    Me.Label_SeizouBi.Size = New System.Drawing.Size(133, 21)
    Me.Label_SeizouBi.TabIndex = 1
    Me.Label_SeizouBi.Text = "2001/12/31"
    Me.Label_SeizouBi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label_Title_SeizouBi
    '
    Me.Label_Title_SeizouBi.AutoSize = True
    Me.Label_Title_SeizouBi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_SeizouBi.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_SeizouBi.Location = New System.Drawing.Point(23, 13)
    Me.Label_Title_SeizouBi.Name = "Label_Title_SeizouBi"
    Me.Label_Title_SeizouBi.Size = New System.Drawing.Size(76, 21)
    Me.Label_Title_SeizouBi.TabIndex = 0
    Me.Label_Title_SeizouBi.Text = "製造日"
    Me.Label_Title_SeizouBi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label1_Title_Tokuisaki_02
    '
    Me.Label1_Title_Tokuisaki_02.AutoSize = True
    Me.Label1_Title_Tokuisaki_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1_Title_Tokuisaki_02.ForeColor = System.Drawing.Color.Black
    Me.Label1_Title_Tokuisaki_02.Location = New System.Drawing.Point(25, 54)
    Me.Label1_Title_Tokuisaki_02.Name = "Label1_Title_Tokuisaki_02"
    Me.Label1_Title_Tokuisaki_02.Size = New System.Drawing.Size(98, 21)
    Me.Label1_Title_Tokuisaki_02.TabIndex = 8
    Me.Label1_Title_Tokuisaki_02.Text = "得意先名"
    '
    'Label_Title_EdaNo_02
    '
    Me.Label_Title_EdaNo_02.AutoSize = True
    Me.Label_Title_EdaNo_02.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_EdaNo_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_EdaNo_02.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_EdaNo_02.Location = New System.Drawing.Point(250, 13)
    Me.Label_Title_EdaNo_02.Name = "Label_Title_EdaNo_02"
    Me.Label_Title_EdaNo_02.Size = New System.Drawing.Size(70, 21)
    Me.Label_Title_EdaNo_02.TabIndex = 2
    Me.Label_Title_EdaNo_02.Text = "枝 No."
    Me.Label_Title_EdaNo_02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'ButtonEnd
    '
    Me.ButtonEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonEnd.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonEnd.Image = CType(resources.GetObject("ButtonEnd.Image"), System.Drawing.Image)
    Me.ButtonEnd.Location = New System.Drawing.Point(1458, 5)
    Me.ButtonEnd.Name = "ButtonEnd"
    Me.ButtonEnd.Size = New System.Drawing.Size(72, 120)
    Me.ButtonEnd.TabIndex = 18
    Me.ButtonEnd.Text = "ButtonEnd1"
    Me.ButtonEnd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ButtonEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonEnd.UseVisualStyleBackColor = False
    '
    'ButtonPrint
    '
    Me.ButtonPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonPrint.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonPrint.Image = CType(resources.GetObject("ButtonPrint.Image"), System.Drawing.Image)
    Me.ButtonPrint.Location = New System.Drawing.Point(1386, 5)
    Me.ButtonPrint.Name = "ButtonPrint"
    Me.ButtonPrint.Size = New System.Drawing.Size(72, 120)
    Me.ButtonPrint.TabIndex = 19
    Me.ButtonPrint.Text = "ButtonPrint1"
    Me.ButtonPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ButtonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonPrint.UseVisualStyleBackColor = False
    '
    'ButtonTorikeshi
    '
    Me.ButtonTorikeshi.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonTorikeshi.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonTorikeshi.Image = CType(resources.GetObject("ButtonTorikeshi.Image"), System.Drawing.Image)
    Me.ButtonTorikeshi.Location = New System.Drawing.Point(1314, 5)
    Me.ButtonTorikeshi.Name = "ButtonTorikeshi"
    Me.ButtonTorikeshi.Size = New System.Drawing.Size(72, 120)
    Me.ButtonTorikeshi.TabIndex = 20
    Me.ButtonTorikeshi.Text = "ButtonCancel1"
    Me.ButtonTorikeshi.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.ButtonTorikeshi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonTorikeshi.UseVisualStyleBackColor = False
    '
    'Panel_Msg
    '
    Me.Panel_Msg.Controls.Add(Me.Label_Kensu)
    Me.Panel_Msg.Controls.Add(Me.Label13)
    Me.Panel_Msg.Controls.Add(Me.Label12)
    Me.Panel_Msg.Location = New System.Drawing.Point(0, 825)
    Me.Panel_Msg.Name = "Panel_Msg"
    Me.Panel_Msg.Size = New System.Drawing.Size(1534, 50)
    Me.Panel_Msg.TabIndex = 26
    '
    'Label_Kensu
    '
    Me.Label_Kensu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 18.33962!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Kensu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Label_Kensu.Location = New System.Drawing.Point(838, 16)
    Me.Label_Kensu.Name = "Label_Kensu"
    Me.Label_Kensu.Size = New System.Drawing.Size(688, 32)
    Me.Label_Kensu.TabIndex = 15
    Me.Label_Kensu.Text = "売上単価"
    Me.Label_Kensu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label13
    '
    Me.Label13.AutoSize = True
    Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 10.86792!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label13.ForeColor = System.Drawing.Color.Red
    Me.Label13.Location = New System.Drawing.Point(0, 31)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(262, 16)
    Me.Label13.TabIndex = 9
    Me.Label13.Text = """×""　←　製造日から２週間以上経過"
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 10.86792!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label12.ForeColor = System.Drawing.Color.Yellow
    Me.Label12.Location = New System.Drawing.Point(0, 8)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(289, 16)
    Me.Label12.TabIndex = 1
    Me.Label12.Text = """△""　←　製造日から１週間以降２週間迄"
    Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Form_Zaiko
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.Frame_IN)
    Me.Controls.Add(Me.ButtonPrint)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.Label_GridData)
    Me.Controls.Add(Me.ButtonTorikeshi)
    Me.Controls.Add(Me.DataGridView1)
    Me.Controls.Add(Me.DataGridView2)
    Me.Controls.Add(Me.Panel_Msg)
    Me.Controls.Add(Me.ButtonEnd)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Margin = New System.Windows.Forms.Padding(5)
    Me.Name = "Form_Zaiko"
    Me.Text = "Form1"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.Frame_IN.ResumeLayout(False)
    Me.Frame_IN.PerformLayout()
    Me.Panel_Msg.ResumeLayout(False)
    Me.Panel_Msg.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents DataGridView2 As DataGridView
  Friend WithEvents Label_GridData As Label
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Label_Title_EdaNo_01 As Label
  Friend WithEvents Label_Title_SyouhinMei As Label
  Friend WithEvents Label_Title_Tokuisaki_01 As Label
  Friend WithEvents Label_Title_TANTOU As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents Frame_IN As GroupBox
  Friend WithEvents Label_Title_Jyuryo As Label
  Friend WithEvents Label_Jyuryo As Label
  Friend WithEvents Label_Sayu As Label
  Friend WithEvents Label_EdaNo_02 As Label
  Friend WithEvents Label_Title_Tanka As Label
  Friend WithEvents Label_Title_Baika As Label
  Friend WithEvents Label_Title_SetCd_02 As Label
  Friend WithEvents Label_SeizouBi As Label
  Friend WithEvents Label_Title_SeizouBi As Label
  Friend WithEvents Label1_Title_Tokuisaki_02 As Label
  Friend WithEvents Label_Title_EdaNo_02 As Label
  Friend WithEvents CmbMstStaff1 As T.R.ZCommonCtrl.CmbMstStaff
  Friend WithEvents TxtEdaban_01 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents CmbMstCustomer_01 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents ButtonUpdate As T.R.ZCommonCtrl.ButtonUpdate
  Friend WithEvents TxBaika_02 As T.R.ZCommonCtrl.TxBaika
  Friend WithEvents TxTanka_02 As T.R.ZCommonCtrl.TxTanka
  Friend WithEvents CmbMstSetType_02 As T.R.ZCommonCtrl.CmbMstSetType
  Friend WithEvents CmbMstCustomer_02 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents ButtonPrint As T.R.ZCommonCtrl.ButtonPrint
  Friend WithEvents ButtonTorikeshi As T.R.ZCommonCtrl.ButtonCancel
  Friend WithEvents CmbMstSetType_01 As T.R.ZCommonCtrl.CmbMstSetType
  Public WithEvents CheckBox_Sample02 As CheckBox
  Public WithEvents CheckBox_Sample01 As CheckBox
  Public WithEvents CheckBox_EdaBetu As CheckBox
  Friend WithEvents ButtonSyuka As T.R.ZCommonCtrl.ButtonSyuka
  Friend WithEvents CmbMstItem_01 As T.R.ZCommonCtrl.CmbMstItem
  Friend WithEvents TxtKakoubi_01 As TextBox
  Friend WithEvents TxtKakoubi_03 As TextBox
  Friend WithEvents TxtKakoubi_02 As TextBox
  Friend WithEvents Panel_Msg As Panel
  Friend WithEvents Label_Kensu As Label
  Friend WithEvents Label13 As Label
  Friend WithEvents Label12 As Label
  Friend WithEvents Label1 As Label
  Friend WithEvents Label4 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents Label_Title_SetCd_01 As Label
  Friend WithEvents ButtonEnd As T.R.ZCommonCtrl.ButtonEnd
  Friend WithEvents Label_KotaiNo As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents TxtKakoubi_04 As TextBox
End Class
