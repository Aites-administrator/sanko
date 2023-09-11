<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KotaiTrace
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KotaiTrace))
    Me.TxKotaiNo1 = New T.R.ZCommonCtrl.TxKotaiNo()
    Me.TxtEdaban1 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.TxtDateShiirebi = New T.R.ZCommonCtrl.TxtDateBase()
    Me.CmbMstShiresaki1 = New T.R.ZCommonCtrl.CmbMstShiresaki()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.btnSearch = New System.Windows.Forms.Button()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.txtDummy = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.txtSearchValGTE = New T.R.ZCommonCtrl.TxtDateBase()
    Me.txtSearchValEQ = New T.R.ZCommonCtrl.TxtDateBase()
    Me.TabControl1 = New System.Windows.Forms.TabControl()
    Me.TabPage1 = New System.Windows.Forms.TabPage()
    Me.lblPage = New System.Windows.Forms.Label()
    Me.btnNext = New System.Windows.Forms.Button()
    Me.btnPrevious = New System.Windows.Forms.Button()
    Me.lblDLSRNAME = New System.Windows.Forms.Label()
    Me.lblDSBNAME = New System.Windows.Forms.Label()
    Me.lblDGNNAME = New System.Windows.Forms.Label()
    Me.lblEBCODE = New System.Windows.Forms.Label()
    Me.lblEDC = New System.Windows.Forms.Label()
    Me.lblDKKNAME = New System.Windows.Forms.Label()
    Me.lblDKZNAME = New System.Windows.Forms.Label()
    Me.lblKOTAINO = New System.Windows.Forms.Label()
    Me.lblDTJNAME = New System.Windows.Forms.Label()
    Me.lblSIIREBI = New System.Windows.Forms.Label()
    Me.Label15 = New System.Windows.Forms.Label()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.TabPage2 = New System.Windows.Forms.TabPage()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.TabPage3 = New System.Windows.Forms.TabPage()
    Me.DataGridView2 = New System.Windows.Forms.DataGridView()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.TxtJyouJyouNo = New T.R.ZCommonCtrl.TxtJyouJyouNo()
    Me.TabControl1.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.TabPage2.SuspendLayout()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage3.SuspendLayout()
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TxKotaiNo1
    '
    Me.TxKotaiNo1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxKotaiNo1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxKotaiNo1.Location = New System.Drawing.Point(155, 15)
    Me.TxKotaiNo1.Name = "TxKotaiNo1"
    Me.TxKotaiNo1.Size = New System.Drawing.Size(138, 27)
    Me.TxKotaiNo1.TabIndex = 1
    Me.TxKotaiNo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxtEdaban1
    '
    Me.TxtEdaban1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtEdaban1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban1.Location = New System.Drawing.Point(397, 15)
    Me.TxtEdaban1.Name = "TxtEdaban1"
    Me.TxtEdaban1.Size = New System.Drawing.Size(86, 27)
    Me.TxtEdaban1.TabIndex = 2
    Me.TxtEdaban1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxtDateShiirebi
    '
    Me.TxtDateShiirebi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtDateShiirebi.Location = New System.Drawing.Point(841, 15)
    Me.TxtDateShiirebi.Name = "TxtDateShiirebi"
    Me.TxtDateShiirebi.Size = New System.Drawing.Size(114, 27)
    Me.TxtDateShiirebi.TabIndex = 4
    '
    'CmbMstShiresaki1
    '
    Me.CmbMstShiresaki1.AvailableBlank = False
    Me.CmbMstShiresaki1.DisplayMember = "ItemName"
    Me.CmbMstShiresaki1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.CmbMstShiresaki1.FormattingEnabled = True
    Me.CmbMstShiresaki1.Location = New System.Drawing.Point(1069, 15)
    Me.CmbMstShiresaki1.Name = "CmbMstShiresaki1"
    Me.CmbMstShiresaki1.Size = New System.Drawing.Size(295, 27)
    Me.CmbMstShiresaki1.TabIndex = 5
    Me.CmbMstShiresaki1.ValueMember = "ItemCode"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.Location = New System.Drawing.Point(20, 18)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(129, 20)
    Me.Label1.TabIndex = 5
    Me.Label1.Text = "個体識別番号"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.Location = New System.Drawing.Point(323, 18)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(69, 20)
    Me.Label2.TabIndex = 6
    Me.Label2.Text = "枝番号"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.Location = New System.Drawing.Point(997, 18)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(69, 20)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "仕入先"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label4.Location = New System.Drawing.Point(747, 18)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(89, 20)
    Me.Label4.TabIndex = 8
    Me.Label4.Text = "仕入日付"
    '
    'btnSearch
    '
    Me.btnSearch.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnSearch.Location = New System.Drawing.Point(1253, 874)
    Me.btnSearch.Name = "btnSearch"
    Me.btnSearch.Size = New System.Drawing.Size(118, 30)
    Me.btnSearch.TabIndex = 6
    Me.btnSearch.Text = "F1:実行"
    Me.btnSearch.UseVisualStyleBackColor = True
    '
    'btnClose
    '
    Me.btnClose.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnClose.Location = New System.Drawing.Point(1401, 874)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(118, 30)
    Me.btnClose.TabIndex = 7
    Me.btnClose.Text = "F12:閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'txtDummy
    '
    Me.txtDummy.Location = New System.Drawing.Point(1156, 52)
    Me.txtDummy.Name = "txtDummy"
    Me.txtDummy.Size = New System.Drawing.Size(84, 27)
    Me.txtDummy.TabIndex = 71
    Me.txtDummy.Visible = False
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label5.Location = New System.Drawing.Point(728, 52)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(396, 20)
    Me.Label5.TabIndex = 72
    Me.Label5.Text = "起動時に0件で表示させるためのダミー検索値"
    Me.Label5.Visible = False
    '
    'lblInformation
    '
    Me.lblInformation.AutoSize = True
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Red
    Me.lblInformation.Location = New System.Drawing.Point(10, 886)
    Me.lblInformation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(263, 17)
    Me.lblInformation.TabIndex = 73
    Me.lblInformation.Text = "ここに入力時の説明文が表示されます"
    '
    'txtSearchValGTE
    '
    Me.txtSearchValGTE.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.txtSearchValGTE.Location = New System.Drawing.Point(1401, 8)
    Me.txtSearchValGTE.Name = "txtSearchValGTE"
    Me.txtSearchValGTE.Size = New System.Drawing.Size(114, 27)
    Me.txtSearchValGTE.TabIndex = 74
    Me.txtSearchValGTE.Visible = False
    '
    'txtSearchValEQ
    '
    Me.txtSearchValEQ.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.txtSearchValEQ.Location = New System.Drawing.Point(1401, 41)
    Me.txtSearchValEQ.Name = "txtSearchValEQ"
    Me.txtSearchValEQ.Size = New System.Drawing.Size(114, 27)
    Me.txtSearchValEQ.TabIndex = 75
    Me.txtSearchValEQ.Visible = False
    '
    'TabControl1
    '
    Me.TabControl1.Controls.Add(Me.TabPage1)
    Me.TabControl1.Controls.Add(Me.TabPage2)
    Me.TabControl1.Controls.Add(Me.TabPage3)
    Me.TabControl1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TabControl1.Location = New System.Drawing.Point(0, 0)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(1512, 777)
    Me.TabControl1.TabIndex = 1
    Me.TabControl1.TabStop = False
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.lblPage)
    Me.TabPage1.Controls.Add(Me.btnNext)
    Me.TabPage1.Controls.Add(Me.btnPrevious)
    Me.TabPage1.Controls.Add(Me.lblDLSRNAME)
    Me.TabPage1.Controls.Add(Me.lblDSBNAME)
    Me.TabPage1.Controls.Add(Me.lblDGNNAME)
    Me.TabPage1.Controls.Add(Me.lblEBCODE)
    Me.TabPage1.Controls.Add(Me.lblEDC)
    Me.TabPage1.Controls.Add(Me.lblDKKNAME)
    Me.TabPage1.Controls.Add(Me.lblDKZNAME)
    Me.TabPage1.Controls.Add(Me.lblKOTAINO)
    Me.TabPage1.Controls.Add(Me.lblDTJNAME)
    Me.TabPage1.Controls.Add(Me.lblSIIREBI)
    Me.TabPage1.Controls.Add(Me.Label15)
    Me.TabPage1.Controls.Add(Me.Label14)
    Me.TabPage1.Controls.Add(Me.Label13)
    Me.TabPage1.Controls.Add(Me.Label12)
    Me.TabPage1.Controls.Add(Me.Label11)
    Me.TabPage1.Controls.Add(Me.Label10)
    Me.TabPage1.Controls.Add(Me.Label9)
    Me.TabPage1.Controls.Add(Me.Label8)
    Me.TabPage1.Controls.Add(Me.Label7)
    Me.TabPage1.Controls.Add(Me.Label6)
    Me.TabPage1.Location = New System.Drawing.Point(4, 29)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(1504, 744)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "TabPage1"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'lblPage
    '
    Me.lblPage.AutoSize = True
    Me.lblPage.Location = New System.Drawing.Point(1334, 704)
    Me.lblPage.Name = "lblPage"
    Me.lblPage.Size = New System.Drawing.Size(39, 20)
    Me.lblPage.TabIndex = 29
    Me.lblPage.Text = "0/0"
    '
    'btnNext
    '
    Me.btnNext.Location = New System.Drawing.Point(1434, 704)
    Me.btnNext.Name = "btnNext"
    Me.btnNext.Size = New System.Drawing.Size(64, 25)
    Me.btnNext.TabIndex = 28
    Me.btnNext.Text = ">>"
    Me.btnNext.UseVisualStyleBackColor = True
    '
    'btnPrevious
    '
    Me.btnPrevious.Location = New System.Drawing.Point(1208, 704)
    Me.btnPrevious.Name = "btnPrevious"
    Me.btnPrevious.Size = New System.Drawing.Size(64, 25)
    Me.btnPrevious.TabIndex = 27
    Me.btnPrevious.Text = "<<"
    Me.btnPrevious.UseVisualStyleBackColor = True
    '
    'lblDLSRNAME
    '
    Me.lblDLSRNAME.AutoSize = True
    Me.lblDLSRNAME.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblDLSRNAME.Location = New System.Drawing.Point(742, 108)
    Me.lblDLSRNAME.Name = "lblDLSRNAME"
    Me.lblDLSRNAME.Size = New System.Drawing.Size(0, 22)
    Me.lblDLSRNAME.TabIndex = 26
    '
    'lblDSBNAME
    '
    Me.lblDSBNAME.AutoSize = True
    Me.lblDSBNAME.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblDSBNAME.Location = New System.Drawing.Point(742, 216)
    Me.lblDSBNAME.Name = "lblDSBNAME"
    Me.lblDSBNAME.Size = New System.Drawing.Size(0, 22)
    Me.lblDSBNAME.TabIndex = 25
    '
    'lblDGNNAME
    '
    Me.lblDGNNAME.AutoSize = True
    Me.lblDGNNAME.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblDGNNAME.Location = New System.Drawing.Point(742, 324)
    Me.lblDGNNAME.Name = "lblDGNNAME"
    Me.lblDGNNAME.Size = New System.Drawing.Size(0, 22)
    Me.lblDGNNAME.TabIndex = 24
    '
    'lblEBCODE
    '
    Me.lblEBCODE.AutoSize = True
    Me.lblEBCODE.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblEBCODE.Location = New System.Drawing.Point(742, 432)
    Me.lblEBCODE.Name = "lblEBCODE"
    Me.lblEBCODE.Size = New System.Drawing.Size(0, 22)
    Me.lblEBCODE.TabIndex = 23
    '
    'lblEDC
    '
    Me.lblEDC.AutoSize = True
    Me.lblEDC.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblEDC.Location = New System.Drawing.Point(742, 540)
    Me.lblEDC.Name = "lblEDC"
    Me.lblEDC.Size = New System.Drawing.Size(0, 22)
    Me.lblEDC.TabIndex = 22
    '
    'lblDKKNAME
    '
    Me.lblDKKNAME.AutoSize = True
    Me.lblDKKNAME.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblDKKNAME.Location = New System.Drawing.Point(231, 216)
    Me.lblDKKNAME.Name = "lblDKKNAME"
    Me.lblDKKNAME.Size = New System.Drawing.Size(0, 22)
    Me.lblDKKNAME.TabIndex = 21
    '
    'lblDKZNAME
    '
    Me.lblDKZNAME.AutoSize = True
    Me.lblDKZNAME.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblDKZNAME.Location = New System.Drawing.Point(231, 324)
    Me.lblDKZNAME.Name = "lblDKZNAME"
    Me.lblDKZNAME.Size = New System.Drawing.Size(0, 22)
    Me.lblDKZNAME.TabIndex = 20
    '
    'lblKOTAINO
    '
    Me.lblKOTAINO.AutoSize = True
    Me.lblKOTAINO.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblKOTAINO.Location = New System.Drawing.Point(231, 432)
    Me.lblKOTAINO.Name = "lblKOTAINO"
    Me.lblKOTAINO.Size = New System.Drawing.Size(0, 22)
    Me.lblKOTAINO.TabIndex = 19
    '
    'lblDTJNAME
    '
    Me.lblDTJNAME.AutoSize = True
    Me.lblDTJNAME.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblDTJNAME.Location = New System.Drawing.Point(231, 540)
    Me.lblDTJNAME.Name = "lblDTJNAME"
    Me.lblDTJNAME.Size = New System.Drawing.Size(0, 22)
    Me.lblDTJNAME.TabIndex = 18
    '
    'lblSIIREBI
    '
    Me.lblSIIREBI.AutoSize = True
    Me.lblSIIREBI.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblSIIREBI.Location = New System.Drawing.Point(231, 108)
    Me.lblSIIREBI.Name = "lblSIIREBI"
    Me.lblSIIREBI.Size = New System.Drawing.Size(0, 22)
    Me.lblSIIREBI.TabIndex = 17
    '
    'Label15
    '
    Me.Label15.AutoSize = True
    Me.Label15.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label15.Location = New System.Drawing.Point(96, 216)
    Me.Label15.Name = "Label15"
    Me.Label15.Size = New System.Drawing.Size(49, 20)
    Me.Label15.TabIndex = 16
    Me.Label15.Text = "規格"
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label14.Location = New System.Drawing.Point(96, 324)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(49, 20)
    Me.Label14.TabIndex = 15
    Me.Label14.Text = "格付"
    '
    'Label13
    '
    Me.Label13.AutoSize = True
    Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label13.Location = New System.Drawing.Point(96, 432)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(89, 20)
    Me.Label13.TabIndex = 14
    Me.Label13.Text = "個体識別"
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label12.Location = New System.Drawing.Point(96, 540)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(49, 20)
    Me.Label12.TabIndex = 13
    Me.Label12.Text = "屠場"
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label11.Location = New System.Drawing.Point(603, 108)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(69, 20)
    Me.Label11.TabIndex = 12
    Me.Label11.Text = "仕入先"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label10.Location = New System.Drawing.Point(603, 216)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(49, 20)
    Me.Label10.TabIndex = 11
    Me.Label10.Text = "種別"
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label9.Location = New System.Drawing.Point(603, 324)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(69, 20)
    Me.Label9.TabIndex = 10
    Me.Label9.Text = "原産地"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label8.Location = New System.Drawing.Point(603, 432)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(69, 20)
    Me.Label8.TabIndex = 9
    Me.Label8.Text = "枝番号"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label7.Location = New System.Drawing.Point(603, 540)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(98, 20)
    Me.Label7.TabIndex = 8
    Me.Label7.Text = "上場コード"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label6.Location = New System.Drawing.Point(96, 108)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(89, 20)
    Me.Label6.TabIndex = 7
    Me.Label6.Text = "仕入日付"
    '
    'TabPage2
    '
    Me.TabPage2.Controls.Add(Me.DataGridView1)
    Me.TabPage2.Location = New System.Drawing.Point(4, 29)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(1504, 744)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "TabPage2"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(5, 6)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.Size = New System.Drawing.Size(1493, 720)
    Me.DataGridView1.TabIndex = 0
    Me.DataGridView1.TabStop = False
    '
    'TabPage3
    '
    Me.TabPage3.Controls.Add(Me.DataGridView2)
    Me.TabPage3.Location = New System.Drawing.Point(4, 29)
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage3.Size = New System.Drawing.Size(1504, 744)
    Me.TabPage3.TabIndex = 2
    Me.TabPage3.Text = "TabPage3"
    Me.TabPage3.UseVisualStyleBackColor = True
    '
    'DataGridView2
    '
    Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView2.Location = New System.Drawing.Point(5, 6)
    Me.DataGridView2.Name = "DataGridView2"
    Me.DataGridView2.Size = New System.Drawing.Size(1493, 719)
    Me.DataGridView2.TabIndex = 0
    Me.DataGridView2.TabStop = False
    '
    'Panel1
    '
    Me.Panel1.BackColor = System.Drawing.Color.Black
    Me.Panel1.Controls.Add(Me.TabControl1)
    Me.Panel1.Location = New System.Drawing.Point(5, 83)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(1516, 779)
    Me.Panel1.TabIndex = 76
    '
    'Label16
    '
    Me.Label16.AutoSize = True
    Me.Label16.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label16.Location = New System.Drawing.Point(512, 18)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(98, 20)
    Me.Label16.TabIndex = 77
    Me.Label16.Text = "上場コード"
    '
    'TxtJyouJyouNo
    '
    Me.TxtJyouJyouNo.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtJyouJyouNo.Location = New System.Drawing.Point(610, 15)
    Me.TxtJyouJyouNo.Name = "TxtJyouJyouNo"
    Me.TxtJyouJyouNo.Size = New System.Drawing.Size(100, 27)
    Me.TxtJyouJyouNo.TabIndex = 3
    Me.TxtJyouJyouNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Form_KotaiTrace
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 19.0!)
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.TxtJyouJyouNo)
    Me.Controls.Add(Me.Label16)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.txtSearchValEQ)
    Me.Controls.Add(Me.txtSearchValGTE)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.txtDummy)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnSearch)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.CmbMstShiresaki1)
    Me.Controls.Add(Me.TxtDateShiirebi)
    Me.Controls.Add(Me.TxtEdaban1)
    Me.Controls.Add(Me.TxKotaiNo1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_KotaiTrace"
    Me.TabControl1.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.TabPage1.PerformLayout()
    Me.TabPage2.ResumeLayout(False)
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage3.ResumeLayout(False)
    CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TxKotaiNo1 As T.R.ZCommonCtrl.TxKotaiNo
  Friend WithEvents TxtEdaban1 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents TxtDateShiirebi As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents CmbMstShiresaki1 As T.R.ZCommonCtrl.CmbMstShiresaki
  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents Label4 As Label
  Friend WithEvents btnSearch As Button
  Friend WithEvents btnClose As Button
  Friend WithEvents txtDummy As TextBox
  Friend WithEvents Label5 As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents txtSearchValGTE As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents txtSearchValEQ As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents TabControl1 As TabControl
  Friend WithEvents TabPage1 As TabPage
  Friend WithEvents lblPage As Label
  Friend WithEvents btnNext As Button
  Friend WithEvents btnPrevious As Button
  Friend WithEvents lblDLSRNAME As Label
  Friend WithEvents lblDSBNAME As Label
  Friend WithEvents lblDGNNAME As Label
  Friend WithEvents lblEBCODE As Label
  Friend WithEvents lblEDC As Label
  Friend WithEvents lblDKKNAME As Label
  Friend WithEvents lblDKZNAME As Label
  Friend WithEvents lblKOTAINO As Label
  Friend WithEvents lblDTJNAME As Label
  Friend WithEvents lblSIIREBI As Label
  Friend WithEvents Label15 As Label
  Friend WithEvents Label14 As Label
  Friend WithEvents Label13 As Label
  Friend WithEvents Label12 As Label
  Friend WithEvents Label11 As Label
  Friend WithEvents Label10 As Label
  Friend WithEvents Label9 As Label
  Friend WithEvents Label8 As Label
  Friend WithEvents Label7 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents TabPage2 As TabPage
  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents TabPage3 As TabPage
  Friend WithEvents DataGridView2 As DataGridView
  Friend WithEvents Panel1 As Panel
  Friend WithEvents Label16 As Label
  Friend WithEvents TxtJyouJyouNo As T.R.ZCommonCtrl.TxtJyouJyouNo
End Class
