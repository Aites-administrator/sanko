<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Shuka
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Shuka))
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.txtSyukkabiFrom = New T.R.ZCommonCtrl.TxtDateBase()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtSyukkabi = New T.R.ZCommonCtrl.TxtDateBase()
    Me.CmbMstCustomer_01 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.CmbDateProcessing_01 = New T.R.ZCommonCtrl.CmbDateProcessing()
    Me.CheckBox_EdaBetu = New System.Windows.Forms.CheckBox()
    Me.TxKotaiNo_01 = New T.R.ZCommonCtrl.TxKotaiNo()
    Me.TxtEdaban_01 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.CmbMstSetType_01 = New T.R.ZCommonCtrl.CmbMstSetType()
    Me.CmbMstItem_01 = New T.R.ZCommonCtrl.CmbMstItem()
    Me.Label_Title_EdaNo_01 = New System.Windows.Forms.Label()
    Me.Label_Title_SyouhinMei = New System.Windows.Forms.Label()
    Me.Label_Title_SetCd_01 = New System.Windows.Forms.Label()
    Me.Label_Title_Tokuisaki_01 = New System.Windows.Forms.Label()
    Me.Label_Title_TKotaiNo = New System.Windows.Forms.Label()
    Me.Label_Title_Syukabi = New System.Windows.Forms.Label()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.Frame_IN = New System.Windows.Forms.GroupBox()
    Me.Label_KotaiNo = New System.Windows.Forms.Label()
    Me.ButtonUpdate = New T.R.ZCommonCtrl.ButtonUpdate()
    Me.Label_Title_Jyuryo = New System.Windows.Forms.Label()
    Me.Label_Jyuryo = New System.Windows.Forms.Label()
    Me.Label_Sayu = New System.Windows.Forms.Label()
    Me.Label_EdaNo_02 = New System.Windows.Forms.Label()
    Me.CmbMstCustomer_02 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.TxBaika_02 = New T.R.ZCommonCtrl.TxBaika()
    Me.TxTanka_02 = New T.R.ZCommonCtrl.TxTanka()
    Me.Label_Title_Tanka = New System.Windows.Forms.Label()
    Me.Label_Title_Baika = New System.Windows.Forms.Label()
    Me.Label_Title_SetCd_02 = New System.Windows.Forms.Label()
    Me.Label_SeizouBi = New System.Windows.Forms.Label()
    Me.Label_Title_SeizouBi = New System.Windows.Forms.Label()
    Me.Label1_Title_Tokuisaki_02 = New System.Windows.Forms.Label()
    Me.CmbMstSetType_02 = New T.R.ZCommonCtrl.CmbMstSetType()
    Me.Label_Title_EdaNo_02 = New System.Windows.Forms.Label()
    Me.DataGridView2 = New System.Windows.Forms.DataGridView()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.ButtonEnd = New T.R.ZCommonCtrl.ButtonEnd()
    Me.ButtonTorikeshi = New T.R.ZCommonCtrl.ButtonCancel()
    Me.ButtonPrint = New T.R.ZCommonCtrl.ButtonPrint()
    Me.Label_GridData = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TxtJyouJyouNo = New T.R.ZCommonCtrl.TxtJyouJyouNo()
    Me.GroupBox1.SuspendLayout()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Frame_IN.SuspendLayout()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
    Me.GroupBox1.Controls.Add(Me.TxtJyouJyouNo)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.txtSyukkabiFrom)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.txtSyukkabi)
    Me.GroupBox1.Controls.Add(Me.CmbMstCustomer_01)
    Me.GroupBox1.Controls.Add(Me.CmbDateProcessing_01)
    Me.GroupBox1.Controls.Add(Me.CheckBox_EdaBetu)
    Me.GroupBox1.Controls.Add(Me.TxKotaiNo_01)
    Me.GroupBox1.Controls.Add(Me.TxtEdaban_01)
    Me.GroupBox1.Controls.Add(Me.CmbMstSetType_01)
    Me.GroupBox1.Controls.Add(Me.CmbMstItem_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_EdaNo_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_SyouhinMei)
    Me.GroupBox1.Controls.Add(Me.Label_Title_SetCd_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_Tokuisaki_01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_TKotaiNo)
    Me.GroupBox1.Controls.Add(Me.Label_Title_Syukabi)
    Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(10, 0)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1375, 126)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "抽出条件（空白時はALL）"
    '
    'txtSyukkabiFrom
    '
    Me.txtSyukkabiFrom.Location = New System.Drawing.Point(796, 11)
    Me.txtSyukkabiFrom.Name = "txtSyukkabiFrom"
    Me.txtSyukkabiFrom.Size = New System.Drawing.Size(116, 19)
    Me.txtSyukkabiFrom.TabIndex = 17
    Me.txtSyukkabiFrom.Visible = False
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.White
    Me.Label2.Location = New System.Drawing.Point(592, 11)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(180, 19)
    Me.Label2.TabIndex = 16
    Me.Label2.Text = "未選択時は7か月前"
    Me.Label2.Visible = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.White
    Me.Label1.Location = New System.Drawing.Point(293, 11)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(149, 19)
    Me.Label1.TabIndex = 15
    Me.Label1.Text = "出荷日の検索値"
    Me.Label1.Visible = False
    '
    'txtSyukkabi
    '
    Me.txtSyukkabi.Location = New System.Drawing.Point(461, 11)
    Me.txtSyukkabi.Name = "txtSyukkabi"
    Me.txtSyukkabi.Size = New System.Drawing.Size(116, 19)
    Me.txtSyukkabi.TabIndex = 14
    Me.txtSyukkabi.Visible = False
    '
    'CmbMstCustomer_01
    '
    Me.CmbMstCustomer_01.AvailableBlank = False
    Me.CmbMstCustomer_01.DisplayMember = "ItemName"
    Me.CmbMstCustomer_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstCustomer_01.FormattingEnabled = True
    Me.CmbMstCustomer_01.Location = New System.Drawing.Point(384, 40)
    Me.CmbMstCustomer_01.Name = "CmbMstCustomer_01"
    Me.CmbMstCustomer_01.Size = New System.Drawing.Size(419, 27)
    Me.CmbMstCustomer_01.TabIndex = 3
    Me.CmbMstCustomer_01.ValueMember = "ItemCode"
    '
    'CmbDateProcessing_01
    '
    Me.CmbDateProcessing_01.AvailableBlank = False
    Me.CmbDateProcessing_01.DisplayMember = "ItemCode"
    Me.CmbDateProcessing_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbDateProcessing_01.FormattingEnabled = True
    Me.CmbDateProcessing_01.Location = New System.Drawing.Point(124, 40)
    Me.CmbDateProcessing_01.Name = "CmbDateProcessing_01"
    Me.CmbDateProcessing_01.Size = New System.Drawing.Size(140, 27)
    Me.CmbDateProcessing_01.TabIndex = 1
    Me.CmbDateProcessing_01.ValueMember = "ItemCode"
    '
    'CheckBox_EdaBetu
    '
    Me.CheckBox_EdaBetu.AutoSize = True
    Me.CheckBox_EdaBetu.BackColor = System.Drawing.Color.Transparent
    Me.CheckBox_EdaBetu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CheckBox_EdaBetu.Location = New System.Drawing.Point(1254, 11)
    Me.CheckBox_EdaBetu.Name = "CheckBox_EdaBetu"
    Me.CheckBox_EdaBetu.Size = New System.Drawing.Size(116, 19)
    Me.CheckBox_EdaBetu.TabIndex = 12
    Me.CheckBox_EdaBetu.Text = "枝別合計表示"
    Me.CheckBox_EdaBetu.UseVisualStyleBackColor = False
    '
    'TxKotaiNo_01
    '
    Me.TxKotaiNo_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxKotaiNo_01.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxKotaiNo_01.Location = New System.Drawing.Point(1231, 40)
    Me.TxKotaiNo_01.Name = "TxKotaiNo_01"
    Me.TxKotaiNo_01.Size = New System.Drawing.Size(120, 27)
    Me.TxKotaiNo_01.TabIndex = 7
    Me.TxKotaiNo_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxtEdaban_01
    '
    Me.TxtEdaban_01.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.TxtEdaban_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtEdaban_01.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban_01.Location = New System.Drawing.Point(864, 40)
    Me.TxtEdaban_01.Name = "TxtEdaban_01"
    Me.TxtEdaban_01.Size = New System.Drawing.Size(62, 27)
    Me.TxtEdaban_01.TabIndex = 5
    Me.TxtEdaban_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'CmbMstSetType_01
    '
    Me.CmbMstSetType_01.AvailableBlank = False
    Me.CmbMstSetType_01.DisplayMember = "ItemName"
    Me.CmbMstSetType_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstSetType_01.FormattingEnabled = True
    Me.CmbMstSetType_01.Location = New System.Drawing.Point(124, 83)
    Me.CmbMstSetType_01.Name = "CmbMstSetType_01"
    Me.CmbMstSetType_01.Size = New System.Drawing.Size(300, 27)
    Me.CmbMstSetType_01.TabIndex = 9
    Me.CmbMstSetType_01.ValueMember = "ItemCode"
    '
    'CmbMstItem_01
    '
    Me.CmbMstItem_01.AvailableBlank = False
    Me.CmbMstItem_01.DisplayMember = "ItemName"
    Me.CmbMstItem_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstItem_01.FormattingEnabled = True
    Me.CmbMstItem_01.Location = New System.Drawing.Point(671, 84)
    Me.CmbMstItem_01.Name = "CmbMstItem_01"
    Me.CmbMstItem_01.Size = New System.Drawing.Size(330, 27)
    Me.CmbMstItem_01.TabIndex = 11
    Me.CmbMstItem_01.ValueMember = "ItemCode"
    '
    'Label_Title_EdaNo_01
    '
    Me.Label_Title_EdaNo_01.AutoSize = True
    Me.Label_Title_EdaNo_01.BackColor = System.Drawing.Color.Transparent
    Me.Label_Title_EdaNo_01.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_EdaNo_01.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_EdaNo_01.Location = New System.Drawing.Point(814, 43)
    Me.Label_Title_EdaNo_01.Name = "Label_Title_EdaNo_01"
    Me.Label_Title_EdaNo_01.Size = New System.Drawing.Size(49, 19)
    Me.Label_Title_EdaNo_01.TabIndex = 4
    Me.Label_Title_EdaNo_01.Text = "枝番"
    '
    'Label_Title_SyouhinMei
    '
    Me.Label_Title_SyouhinMei.AutoSize = True
    Me.Label_Title_SyouhinMei.BackColor = System.Drawing.Color.Transparent
    Me.Label_Title_SyouhinMei.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_SyouhinMei.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_SyouhinMei.Location = New System.Drawing.Point(579, 87)
    Me.Label_Title_SyouhinMei.Name = "Label_Title_SyouhinMei"
    Me.Label_Title_SyouhinMei.Size = New System.Drawing.Size(69, 19)
    Me.Label_Title_SyouhinMei.TabIndex = 10
    Me.Label_Title_SyouhinMei.Text = "商品名"
    '
    'Label_Title_SetCd_01
    '
    Me.Label_Title_SetCd_01.AutoSize = True
    Me.Label_Title_SetCd_01.BackColor = System.Drawing.Color.Transparent
    Me.Label_Title_SetCd_01.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_SetCd_01.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_SetCd_01.Location = New System.Drawing.Point(12, 87)
    Me.Label_Title_SetCd_01.Name = "Label_Title_SetCd_01"
    Me.Label_Title_SetCd_01.Size = New System.Drawing.Size(52, 19)
    Me.Label_Title_SetCd_01.TabIndex = 8
    Me.Label_Title_SetCd_01.Text = "セット"
    '
    'Label_Title_Tokuisaki_01
    '
    Me.Label_Title_Tokuisaki_01.AutoSize = True
    Me.Label_Title_Tokuisaki_01.BackColor = System.Drawing.Color.Transparent
    Me.Label_Title_Tokuisaki_01.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Tokuisaki_01.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Tokuisaki_01.Location = New System.Drawing.Point(283, 43)
    Me.Label_Title_Tokuisaki_01.Name = "Label_Title_Tokuisaki_01"
    Me.Label_Title_Tokuisaki_01.Size = New System.Drawing.Size(89, 19)
    Me.Label_Title_Tokuisaki_01.TabIndex = 2
    Me.Label_Title_Tokuisaki_01.Text = "得意先名"
    '
    'Label_Title_TKotaiNo
    '
    Me.Label_Title_TKotaiNo.AutoSize = True
    Me.Label_Title_TKotaiNo.BackColor = System.Drawing.Color.Transparent
    Me.Label_Title_TKotaiNo.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_TKotaiNo.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_TKotaiNo.Location = New System.Drawing.Point(1136, 43)
    Me.Label_Title_TKotaiNo.Name = "Label_Title_TKotaiNo"
    Me.Label_Title_TKotaiNo.Size = New System.Drawing.Size(89, 19)
    Me.Label_Title_TKotaiNo.TabIndex = 6
    Me.Label_Title_TKotaiNo.Text = "個体識別"
    '
    'Label_Title_Syukabi
    '
    Me.Label_Title_Syukabi.AutoSize = True
    Me.Label_Title_Syukabi.BackColor = System.Drawing.Color.Transparent
    Me.Label_Title_Syukabi.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Syukabi.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Syukabi.Location = New System.Drawing.Point(12, 43)
    Me.Label_Title_Syukabi.Name = "Label_Title_Syukabi"
    Me.Label_Title_Syukabi.Size = New System.Drawing.Size(69, 19)
    Me.Label_Title_Syukabi.TabIndex = 0
    Me.Label_Title_Syukabi.Text = "出荷日"
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(61, 178)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.RowTemplate.Height = 21
    Me.DataGridView1.Size = New System.Drawing.Size(1525, 695)
    Me.DataGridView1.TabIndex = 4
    '
    'Frame_IN
    '
    Me.Frame_IN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Frame_IN.Controls.Add(Me.Label_KotaiNo)
    Me.Frame_IN.Controls.Add(Me.ButtonUpdate)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Jyuryo)
    Me.Frame_IN.Controls.Add(Me.Label_Jyuryo)
    Me.Frame_IN.Controls.Add(Me.Label_Sayu)
    Me.Frame_IN.Controls.Add(Me.Label_EdaNo_02)
    Me.Frame_IN.Controls.Add(Me.CmbMstCustomer_02)
    Me.Frame_IN.Controls.Add(Me.TxBaika_02)
    Me.Frame_IN.Controls.Add(Me.TxTanka_02)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Tanka)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Baika)
    Me.Frame_IN.Controls.Add(Me.Label_Title_SetCd_02)
    Me.Frame_IN.Controls.Add(Me.Label_SeizouBi)
    Me.Frame_IN.Controls.Add(Me.Label_Title_SeizouBi)
    Me.Frame_IN.Controls.Add(Me.Label1_Title_Tokuisaki_02)
    Me.Frame_IN.Controls.Add(Me.CmbMstSetType_02)
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
    Me.Label_KotaiNo.Location = New System.Drawing.Point(535, 13)
    Me.Label_KotaiNo.Name = "Label_KotaiNo"
    Me.Label_KotaiNo.Size = New System.Drawing.Size(146, 21)
    Me.Label_KotaiNo.TabIndex = 21
    Me.Label_KotaiNo.Text = "1234 "
    Me.Label_KotaiNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'ButtonUpdate
    '
    Me.ButtonUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonUpdate.Image = CType(resources.GetObject("ButtonUpdate.Image"), System.Drawing.Image)
    Me.ButtonUpdate.Location = New System.Drawing.Point(1438, 21)
    Me.ButtonUpdate.Name = "ButtonUpdate"
    Me.ButtonUpdate.Size = New System.Drawing.Size(72, 81)
    Me.ButtonUpdate.TabIndex = 14
    Me.ButtonUpdate.Text = "ButtonUpdate1"
    Me.ButtonUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonUpdate.UseVisualStyleBackColor = False
    '
    'Label_Title_Jyuryo
    '
    Me.Label_Title_Jyuryo.AutoSize = True
    Me.Label_Title_Jyuryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_Jyuryo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Jyuryo.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Jyuryo.Location = New System.Drawing.Point(687, 13)
    Me.Label_Title_Jyuryo.Name = "Label_Title_Jyuryo"
    Me.Label_Title_Jyuryo.Size = New System.Drawing.Size(49, 19)
    Me.Label_Title_Jyuryo.TabIndex = 6
    Me.Label_Title_Jyuryo.Text = "重量"
    Me.Label_Title_Jyuryo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label_Jyuryo
    '
    Me.Label_Jyuryo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Jyuryo.ForeColor = System.Drawing.Color.Black
    Me.Label_Jyuryo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.Label_Jyuryo.Location = New System.Drawing.Point(743, 13)
    Me.Label_Jyuryo.Name = "Label_Jyuryo"
    Me.Label_Jyuryo.Size = New System.Drawing.Size(80, 21)
    Me.Label_Jyuryo.TabIndex = 7
    Me.Label_Jyuryo.Text = "1234 "
    Me.Label_Jyuryo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label_Sayu
    '
    Me.Label_Sayu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Sayu.ForeColor = System.Drawing.Color.Black
    Me.Label_Sayu.Location = New System.Drawing.Point(467, 13)
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
    'CmbMstCustomer_02
    '
    Me.CmbMstCustomer_02.AvailableBlank = False
    Me.CmbMstCustomer_02.DisplayMember = "ItemName"
    Me.CmbMstCustomer_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstCustomer_02.FormattingEnabled = True
    Me.CmbMstCustomer_02.Location = New System.Drawing.Point(25, 80)
    Me.CmbMstCustomer_02.Name = "CmbMstCustomer_02"
    Me.CmbMstCustomer_02.Size = New System.Drawing.Size(480, 27)
    Me.CmbMstCustomer_02.TabIndex = 9
    Me.CmbMstCustomer_02.ValueMember = "ItemCode"
    '
    'TxBaika_02
    '
    Me.TxBaika_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxBaika_02.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxBaika_02.Location = New System.Drawing.Point(914, 80)
    Me.TxBaika_02.Name = "TxBaika_02"
    Me.TxBaika_02.Size = New System.Drawing.Size(120, 27)
    Me.TxBaika_02.TabIndex = 12
    Me.TxBaika_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxTanka_02
    '
    Me.TxTanka_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxTanka_02.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxTanka_02.Location = New System.Drawing.Point(1096, 80)
    Me.TxTanka_02.Name = "TxTanka_02"
    Me.TxTanka_02.Size = New System.Drawing.Size(120, 27)
    Me.TxTanka_02.TabIndex = 13
    Me.TxTanka_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label_Title_Tanka
    '
    Me.Label_Title_Tanka.AutoSize = True
    Me.Label_Title_Tanka.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Tanka.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Tanka.Location = New System.Drawing.Point(1096, 51)
    Me.Label_Title_Tanka.Name = "Label_Title_Tanka"
    Me.Label_Title_Tanka.Size = New System.Drawing.Size(69, 19)
    Me.Label_Title_Tanka.TabIndex = 12
    Me.Label_Title_Tanka.Text = "原単価"
    '
    'Label_Title_Baika
    '
    Me.Label_Title_Baika.AutoSize = True
    Me.Label_Title_Baika.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Baika.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Baika.Location = New System.Drawing.Point(914, 51)
    Me.Label_Title_Baika.Name = "Label_Title_Baika"
    Me.Label_Title_Baika.Size = New System.Drawing.Size(49, 19)
    Me.Label_Title_Baika.TabIndex = 14
    Me.Label_Title_Baika.Text = "単価"
    '
    'Label_Title_SetCd_02
    '
    Me.Label_Title_SetCd_02.AutoSize = True
    Me.Label_Title_SetCd_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_SetCd_02.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_SetCd_02.Location = New System.Drawing.Point(530, 51)
    Me.Label_Title_SetCd_02.Name = "Label_Title_SetCd_02"
    Me.Label_Title_SetCd_02.Size = New System.Drawing.Size(95, 19)
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
    Me.Label_Title_SeizouBi.Size = New System.Drawing.Size(69, 19)
    Me.Label_Title_SeizouBi.TabIndex = 0
    Me.Label_Title_SeizouBi.Text = "製造日"
    Me.Label_Title_SeizouBi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label1_Title_Tokuisaki_02
    '
    Me.Label1_Title_Tokuisaki_02.AutoSize = True
    Me.Label1_Title_Tokuisaki_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1_Title_Tokuisaki_02.ForeColor = System.Drawing.Color.Black
    Me.Label1_Title_Tokuisaki_02.Location = New System.Drawing.Point(25, 51)
    Me.Label1_Title_Tokuisaki_02.Name = "Label1_Title_Tokuisaki_02"
    Me.Label1_Title_Tokuisaki_02.Size = New System.Drawing.Size(89, 19)
    Me.Label1_Title_Tokuisaki_02.TabIndex = 8
    Me.Label1_Title_Tokuisaki_02.Text = "得意先名"
    '
    'CmbMstSetType_02
    '
    Me.CmbMstSetType_02.AvailableBlank = False
    Me.CmbMstSetType_02.DisplayMember = "ItemName"
    Me.CmbMstSetType_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstSetType_02.FormattingEnabled = True
    Me.CmbMstSetType_02.Location = New System.Drawing.Point(530, 80)
    Me.CmbMstSetType_02.Name = "CmbMstSetType_02"
    Me.CmbMstSetType_02.Size = New System.Drawing.Size(300, 27)
    Me.CmbMstSetType_02.TabIndex = 11
    Me.CmbMstSetType_02.ValueMember = "ItemCode"
    '
    'Label_Title_EdaNo_02
    '
    Me.Label_Title_EdaNo_02.AutoSize = True
    Me.Label_Title_EdaNo_02.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_EdaNo_02.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_EdaNo_02.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_EdaNo_02.Location = New System.Drawing.Point(250, 13)
    Me.Label_Title_EdaNo_02.Name = "Label_Title_EdaNo_02"
    Me.Label_Title_EdaNo_02.Size = New System.Drawing.Size(65, 19)
    Me.Label_Title_EdaNo_02.TabIndex = 2
    Me.Label_Title_EdaNo_02.Text = "枝 No."
    Me.Label_Title_EdaNo_02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'DataGridView2
    '
    Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView2.Location = New System.Drawing.Point(5, 160)
    Me.DataGridView2.Name = "DataGridView2"
    Me.DataGridView2.RowTemplate.Height = 21
    Me.DataGridView2.Size = New System.Drawing.Size(1526, 715)
    Me.DataGridView2.TabIndex = 2
    '
    'lblInformation
    '
    Me.lblInformation.BackColor = System.Drawing.Color.WhiteSmoke
    Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 18.33962!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Navy
    Me.lblInformation.Location = New System.Drawing.Point(0, 880)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(1535, 29)
    Me.lblInformation.TabIndex = 7
    '
    'ButtonEnd
    '
    Me.ButtonEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonEnd.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonEnd.Image = CType(resources.GetObject("ButtonEnd.Image"), System.Drawing.Image)
    Me.ButtonEnd.Location = New System.Drawing.Point(1458, 2)
    Me.ButtonEnd.Name = "ButtonEnd"
    Me.ButtonEnd.Size = New System.Drawing.Size(72, 63)
    Me.ButtonEnd.TabIndex = 4
    Me.ButtonEnd.Text = "123"
    Me.ButtonEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonEnd.UseVisualStyleBackColor = False
    '
    'ButtonTorikeshi
    '
    Me.ButtonTorikeshi.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonTorikeshi.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonTorikeshi.Image = CType(resources.GetObject("ButtonTorikeshi.Image"), System.Drawing.Image)
    Me.ButtonTorikeshi.Location = New System.Drawing.Point(1458, 65)
    Me.ButtonTorikeshi.Name = "ButtonTorikeshi"
    Me.ButtonTorikeshi.Size = New System.Drawing.Size(72, 63)
    Me.ButtonTorikeshi.TabIndex = 5
    Me.ButtonTorikeshi.Text = "123"
    Me.ButtonTorikeshi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonTorikeshi.UseVisualStyleBackColor = False
    '
    'ButtonPrint
    '
    Me.ButtonPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ButtonPrint.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonPrint.Image = CType(resources.GetObject("ButtonPrint.Image"), System.Drawing.Image)
    Me.ButtonPrint.Location = New System.Drawing.Point(1387, 2)
    Me.ButtonPrint.Name = "ButtonPrint"
    Me.ButtonPrint.Size = New System.Drawing.Size(72, 126)
    Me.ButtonPrint.TabIndex = 3
    Me.ButtonPrint.Text = "ButtonPrint1"
    Me.ButtonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonPrint.UseVisualStyleBackColor = False
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
    Me.Label_GridData.TabIndex = 13
    Me.Label_GridData.Text = "Label18"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.BackColor = System.Drawing.Color.Transparent
    Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.ForeColor = System.Drawing.Color.Black
    Me.Label3.Location = New System.Drawing.Point(934, 43)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(95, 19)
    Me.Label3.TabIndex = 18
    Me.Label3.Text = "上場コード"
    '
    'TxtJyouJyouNo
    '
    Me.TxtJyouJyouNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtJyouJyouNo.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtJyouJyouNo.Location = New System.Drawing.Point(1031, 40)
    Me.TxtJyouJyouNo.Name = "TxtJyouJyouNo"
    Me.TxtJyouJyouNo.Size = New System.Drawing.Size(82, 26)
    Me.TxtJyouJyouNo.TabIndex = 19
    Me.TxtJyouJyouNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Form_Shuka
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 19.0!)
    Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(128, Byte), Integer))
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.ButtonTorikeshi)
    Me.Controls.Add(Me.Label_GridData)
    Me.Controls.Add(Me.ButtonPrint)
    Me.Controls.Add(Me.ButtonEnd)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.Frame_IN)
    Me.Controls.Add(Me.DataGridView1)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.DataGridView2)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Margin = New System.Windows.Forms.Padding(5)
    Me.Name = "Form_Shuka"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Frame_IN.ResumeLayout(False)
    Me.Frame_IN.PerformLayout()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents TxtEdaban_01 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents CmbMstSetType_01 As T.R.ZCommonCtrl.CmbMstSetType
  Friend WithEvents CmbMstItem_01 As T.R.ZCommonCtrl.CmbMstItem
  Friend WithEvents Label_Title_EdaNo_01 As Label
  Friend WithEvents Label_Title_SyouhinMei As Label
  Friend WithEvents Label_Title_SetCd_01 As Label
  Friend WithEvents Label_Title_Tokuisaki_01 As Label
  Friend WithEvents Label_Title_TKotaiNo As Label
  Friend WithEvents Label_Title_Syukabi As Label
  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents Frame_IN As GroupBox
  Friend WithEvents Label_Title_Tanka As Label
  Friend WithEvents Label_Title_Baika As Label
  Friend WithEvents Label_Title_SetCd_02 As Label
  Friend WithEvents Label_SeizouBi As Label
  Friend WithEvents Label_Title_SeizouBi As Label
  Friend WithEvents Label1_Title_Tokuisaki_02 As Label
  Friend WithEvents CmbMstSetType_02 As T.R.ZCommonCtrl.CmbMstSetType
  Friend WithEvents Label_Title_EdaNo_02 As Label
  Friend WithEvents TxKotaiNo_01 As T.R.ZCommonCtrl.TxKotaiNo
  Friend WithEvents TxBaika_02 As T.R.ZCommonCtrl.TxBaika
  Friend WithEvents TxTanka_02 As T.R.ZCommonCtrl.TxTanka
  Friend WithEvents DataGridView2 As DataGridView
  Public WithEvents CheckBox_EdaBetu As CheckBox
  Friend WithEvents lblInformation As Label
  Friend WithEvents CmbMstCustomer_01 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents CmbDateProcessing_01 As T.R.ZCommonCtrl.CmbDateProcessing
  Friend WithEvents CmbMstCustomer_02 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents Label_Title_Jyuryo As Label
  Friend WithEvents Label_Jyuryo As Label
  Friend WithEvents Label_Sayu As Label
  Friend WithEvents Label_EdaNo_02 As Label
  Friend WithEvents ButtonEnd As T.R.ZCommonCtrl.ButtonEnd
  Friend WithEvents ButtonUpdate As T.R.ZCommonCtrl.ButtonUpdate
  Friend WithEvents ButtonTorikeshi As T.R.ZCommonCtrl.ButtonCancel
  Friend WithEvents ButtonPrint As T.R.ZCommonCtrl.ButtonPrint
  Friend WithEvents Label_GridData As Label
  Friend WithEvents Label_KotaiNo As Label
  Friend WithEvents Label1 As Label
  Friend WithEvents txtSyukkabi As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents txtSyukkabiFrom As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents Label2 As Label
  Friend WithEvents TxtJyouJyouNo As T.R.ZCommonCtrl.TxtJyouJyouNo
  Friend WithEvents Label3 As Label
End Class
