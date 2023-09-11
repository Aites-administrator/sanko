<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_EdaSeisan
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_EdaSeisan))
    Me.Frame_IN = New System.Windows.Forms.GroupBox()
    Me.Labe_RData7 = New System.Windows.Forms.Label()
    Me.Labe_LData7 = New System.Windows.Forms.Label()
    Me.Labe_Title_LR7 = New System.Windows.Forms.Label()
    Me.Labe_RData6 = New System.Windows.Forms.Label()
    Me.Labe_LData6 = New System.Windows.Forms.Label()
    Me.Labe_Title_LR6 = New System.Windows.Forms.Label()
    Me.Labe_RData5 = New System.Windows.Forms.Label()
    Me.Labe_LData5 = New System.Windows.Forms.Label()
    Me.Labe_Title_LR5 = New System.Windows.Forms.Label()
    Me.Labe_RData4 = New System.Windows.Forms.Label()
    Me.Labe_LData4 = New System.Windows.Forms.Label()
    Me.Labe_Title_LR4 = New System.Windows.Forms.Label()
    Me.Labe_RData3 = New System.Windows.Forms.Label()
    Me.Labe_LData3 = New System.Windows.Forms.Label()
    Me.Labe_Title_LR3 = New System.Windows.Forms.Label()
    Me.Labe_RData2 = New System.Windows.Forms.Label()
    Me.Labe_LData2 = New System.Windows.Forms.Label()
    Me.Labe_Title_LR2 = New System.Windows.Forms.Label()
    Me.Labe_RData1 = New System.Windows.Forms.Label()
    Me.Labe_LData1 = New System.Windows.Forms.Label()
    Me.Labe_Title_LR1 = New System.Windows.Forms.Label()
    Me.Label_Syubetu = New System.Windows.Forms.Label()
    Me.Label_Title_Syubetu = New System.Windows.Forms.Label()
    Me.Label_KotaiNo = New System.Windows.Forms.Label()
    Me.Label_Title_KotaiNo = New System.Windows.Forms.Label()
    Me.Label_Title_Right = New System.Windows.Forms.Label()
    Me.Label_Gensan = New System.Windows.Forms.Label()
    Me.Label_Title_Left = New System.Windows.Forms.Label()
    Me.Label_EdaNo = New System.Windows.Forms.Label()
    Me.Label_Title_Gensan = New System.Windows.Forms.Label()
    Me.Label_Title_EdaNo = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.CmbDateProcessing_01 = New T.R.ZCommonCtrl.CmbDateKakouBi()
    Me.TxtEdaban_01 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.Button_Init = New System.Windows.Forms.Button()
    Me.Label_Title_Eda01 = New System.Windows.Forms.Label()
    Me.Label_Title_Tokuisaki_01 = New System.Windows.Forms.Label()
    Me.Label_GridData = New System.Windows.Forms.Label()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.ButtonEnd = New T.R.ZCommonCtrl.ButtonEnd()
    Me.ButtonGenkaJidou = New T.R.ZCommonCtrl.ButtonUpdate()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.ButtonPrint = New T.R.ZCommonCtrl.ButtonPrint()
    Me.Frame_IN.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Frame_IN
    '
    Me.Frame_IN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Frame_IN.Controls.Add(Me.Labe_RData7)
    Me.Frame_IN.Controls.Add(Me.Labe_LData7)
    Me.Frame_IN.Controls.Add(Me.Labe_Title_LR7)
    Me.Frame_IN.Controls.Add(Me.Labe_RData6)
    Me.Frame_IN.Controls.Add(Me.Labe_LData6)
    Me.Frame_IN.Controls.Add(Me.Labe_Title_LR6)
    Me.Frame_IN.Controls.Add(Me.Labe_RData5)
    Me.Frame_IN.Controls.Add(Me.Labe_LData5)
    Me.Frame_IN.Controls.Add(Me.Labe_Title_LR5)
    Me.Frame_IN.Controls.Add(Me.Labe_RData4)
    Me.Frame_IN.Controls.Add(Me.Labe_LData4)
    Me.Frame_IN.Controls.Add(Me.Labe_Title_LR4)
    Me.Frame_IN.Controls.Add(Me.Labe_RData3)
    Me.Frame_IN.Controls.Add(Me.Labe_LData3)
    Me.Frame_IN.Controls.Add(Me.Labe_Title_LR3)
    Me.Frame_IN.Controls.Add(Me.Labe_RData2)
    Me.Frame_IN.Controls.Add(Me.Labe_LData2)
    Me.Frame_IN.Controls.Add(Me.Labe_Title_LR2)
    Me.Frame_IN.Controls.Add(Me.Labe_RData1)
    Me.Frame_IN.Controls.Add(Me.Labe_LData1)
    Me.Frame_IN.Controls.Add(Me.Labe_Title_LR1)
    Me.Frame_IN.Controls.Add(Me.Label_Syubetu)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Syubetu)
    Me.Frame_IN.Controls.Add(Me.Label_KotaiNo)
    Me.Frame_IN.Controls.Add(Me.Label_Title_KotaiNo)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Right)
    Me.Frame_IN.Controls.Add(Me.Label_Gensan)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Left)
    Me.Frame_IN.Controls.Add(Me.Label_EdaNo)
    Me.Frame_IN.Controls.Add(Me.Label_Title_Gensan)
    Me.Frame_IN.Controls.Add(Me.Label_Title_EdaNo)
    Me.Frame_IN.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Frame_IN.Location = New System.Drawing.Point(0, 742)
    Me.Frame_IN.Name = "Frame_IN"
    Me.Frame_IN.Size = New System.Drawing.Size(1534, 138)
    Me.Frame_IN.TabIndex = 6
    Me.Frame_IN.TabStop = False
    Me.Frame_IN.Text = "枝表示"
    '
    'Labe_RData7
    '
    Me.Labe_RData7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_RData7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_RData7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_RData7.ForeColor = System.Drawing.Color.Black
    Me.Labe_RData7.Location = New System.Drawing.Point(1230, 100)
    Me.Labe_RData7.Name = "Labe_RData7"
    Me.Labe_RData7.Size = New System.Drawing.Size(120, 26)
    Me.Labe_RData7.TabIndex = 49
    Me.Labe_RData7.Text = "123"
    Me.Labe_RData7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_LData7
    '
    Me.Labe_LData7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_LData7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_LData7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_LData7.ForeColor = System.Drawing.Color.Black
    Me.Labe_LData7.Location = New System.Drawing.Point(1230, 40)
    Me.Labe_LData7.Name = "Labe_LData7"
    Me.Labe_LData7.Size = New System.Drawing.Size(120, 26)
    Me.Labe_LData7.TabIndex = 48
    Me.Labe_LData7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_Title_LR7
    '
    Me.Labe_Title_LR7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Labe_Title_LR7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_Title_LR7.ForeColor = System.Drawing.Color.Black
    Me.Labe_Title_LR7.Location = New System.Drawing.Point(1230, 10)
    Me.Labe_Title_LR7.Name = "Labe_Title_LR7"
    Me.Labe_Title_LR7.Size = New System.Drawing.Size(120, 26)
    Me.Labe_Title_LR7.TabIndex = 47
    Me.Labe_Title_LR7.Text = "粗利率"
    Me.Labe_Title_LR7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Labe_RData6
    '
    Me.Labe_RData6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_RData6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_RData6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_RData6.ForeColor = System.Drawing.Color.Black
    Me.Labe_RData6.Location = New System.Drawing.Point(1100, 100)
    Me.Labe_RData6.Name = "Labe_RData6"
    Me.Labe_RData6.Size = New System.Drawing.Size(120, 26)
    Me.Labe_RData6.TabIndex = 46
    Me.Labe_RData6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_LData6
    '
    Me.Labe_LData6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_LData6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_LData6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_LData6.ForeColor = System.Drawing.Color.Black
    Me.Labe_LData6.Location = New System.Drawing.Point(1100, 40)
    Me.Labe_LData6.Name = "Labe_LData6"
    Me.Labe_LData6.Size = New System.Drawing.Size(120, 26)
    Me.Labe_LData6.TabIndex = 45
    Me.Labe_LData6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_Title_LR6
    '
    Me.Labe_Title_LR6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Labe_Title_LR6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_Title_LR6.ForeColor = System.Drawing.Color.Black
    Me.Labe_Title_LR6.Location = New System.Drawing.Point(1100, 10)
    Me.Labe_Title_LR6.Name = "Labe_Title_LR6"
    Me.Labe_Title_LR6.Size = New System.Drawing.Size(120, 26)
    Me.Labe_Title_LR6.TabIndex = 44
    Me.Labe_Title_LR6.Text = "粗利"
    Me.Labe_Title_LR6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Labe_RData5
    '
    Me.Labe_RData5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_RData5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_RData5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_RData5.ForeColor = System.Drawing.Color.Black
    Me.Labe_RData5.Location = New System.Drawing.Point(970, 100)
    Me.Labe_RData5.Name = "Labe_RData5"
    Me.Labe_RData5.Size = New System.Drawing.Size(120, 26)
    Me.Labe_RData5.TabIndex = 43
    Me.Labe_RData5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_LData5
    '
    Me.Labe_LData5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_LData5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_LData5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_LData5.ForeColor = System.Drawing.Color.Black
    Me.Labe_LData5.Location = New System.Drawing.Point(970, 40)
    Me.Labe_LData5.Name = "Labe_LData5"
    Me.Labe_LData5.Size = New System.Drawing.Size(120, 26)
    Me.Labe_LData5.TabIndex = 42
    Me.Labe_LData5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_Title_LR5
    '
    Me.Labe_Title_LR5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Labe_Title_LR5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_Title_LR5.ForeColor = System.Drawing.Color.Black
    Me.Labe_Title_LR5.Location = New System.Drawing.Point(970, 10)
    Me.Labe_Title_LR5.Name = "Labe_Title_LR5"
    Me.Labe_Title_LR5.Size = New System.Drawing.Size(120, 26)
    Me.Labe_Title_LR5.TabIndex = 41
    Me.Labe_Title_LR5.Text = "売金額"
    Me.Labe_Title_LR5.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Labe_RData4
    '
    Me.Labe_RData4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_RData4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_RData4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_RData4.ForeColor = System.Drawing.Color.Black
    Me.Labe_RData4.Location = New System.Drawing.Point(840, 100)
    Me.Labe_RData4.Name = "Labe_RData4"
    Me.Labe_RData4.Size = New System.Drawing.Size(120, 26)
    Me.Labe_RData4.TabIndex = 40
    Me.Labe_RData4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_LData4
    '
    Me.Labe_LData4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_LData4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_LData4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_LData4.ForeColor = System.Drawing.Color.Black
    Me.Labe_LData4.Location = New System.Drawing.Point(840, 40)
    Me.Labe_LData4.Name = "Labe_LData4"
    Me.Labe_LData4.Size = New System.Drawing.Size(120, 26)
    Me.Labe_LData4.TabIndex = 39
    Me.Labe_LData4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_Title_LR4
    '
    Me.Labe_Title_LR4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Labe_Title_LR4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_Title_LR4.ForeColor = System.Drawing.Color.Black
    Me.Labe_Title_LR4.Location = New System.Drawing.Point(840, 10)
    Me.Labe_Title_LR4.Name = "Labe_Title_LR4"
    Me.Labe_Title_LR4.Size = New System.Drawing.Size(120, 26)
    Me.Labe_Title_LR4.TabIndex = 38
    Me.Labe_Title_LR4.Text = "重量計"
    Me.Labe_Title_LR4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Labe_RData3
    '
    Me.Labe_RData3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_RData3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_RData3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_RData3.ForeColor = System.Drawing.Color.Black
    Me.Labe_RData3.Location = New System.Drawing.Point(710, 100)
    Me.Labe_RData3.Name = "Labe_RData3"
    Me.Labe_RData3.Size = New System.Drawing.Size(120, 26)
    Me.Labe_RData3.TabIndex = 37
    Me.Labe_RData3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_LData3
    '
    Me.Labe_LData3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_LData3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_LData3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_LData3.ForeColor = System.Drawing.Color.Black
    Me.Labe_LData3.Location = New System.Drawing.Point(710, 40)
    Me.Labe_LData3.Name = "Labe_LData3"
    Me.Labe_LData3.Size = New System.Drawing.Size(120, 26)
    Me.Labe_LData3.TabIndex = 36
    Me.Labe_LData3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_Title_LR3
    '
    Me.Labe_Title_LR3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Labe_Title_LR3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_Title_LR3.ForeColor = System.Drawing.Color.Black
    Me.Labe_Title_LR3.Location = New System.Drawing.Point(710, 10)
    Me.Labe_Title_LR3.Name = "Labe_Title_LR3"
    Me.Labe_Title_LR3.Size = New System.Drawing.Size(120, 26)
    Me.Labe_Title_LR3.TabIndex = 35
    Me.Labe_Title_LR3.Text = "原価金額"
    Me.Labe_Title_LR3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Labe_RData2
    '
    Me.Labe_RData2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_RData2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_RData2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_RData2.ForeColor = System.Drawing.Color.Black
    Me.Labe_RData2.Location = New System.Drawing.Point(580, 100)
    Me.Labe_RData2.Name = "Labe_RData2"
    Me.Labe_RData2.Size = New System.Drawing.Size(120, 26)
    Me.Labe_RData2.TabIndex = 34
    Me.Labe_RData2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_LData2
    '
    Me.Labe_LData2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_LData2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_LData2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_LData2.ForeColor = System.Drawing.Color.Black
    Me.Labe_LData2.Location = New System.Drawing.Point(580, 40)
    Me.Labe_LData2.Name = "Labe_LData2"
    Me.Labe_LData2.Size = New System.Drawing.Size(120, 26)
    Me.Labe_LData2.TabIndex = 33
    Me.Labe_LData2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_Title_LR2
    '
    Me.Labe_Title_LR2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Labe_Title_LR2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_Title_LR2.ForeColor = System.Drawing.Color.Black
    Me.Labe_Title_LR2.Location = New System.Drawing.Point(580, 10)
    Me.Labe_Title_LR2.Name = "Labe_Title_LR2"
    Me.Labe_Title_LR2.Size = New System.Drawing.Size(120, 26)
    Me.Labe_Title_LR2.TabIndex = 32
    Me.Labe_Title_LR2.Text = "仕入金額"
    Me.Labe_Title_LR2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Labe_RData1
    '
    Me.Labe_RData1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_RData1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_RData1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_RData1.ForeColor = System.Drawing.Color.Black
    Me.Labe_RData1.Location = New System.Drawing.Point(450, 100)
    Me.Labe_RData1.Name = "Labe_RData1"
    Me.Labe_RData1.Size = New System.Drawing.Size(120, 26)
    Me.Labe_RData1.TabIndex = 31
    Me.Labe_RData1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_LData1
    '
    Me.Labe_LData1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Labe_LData1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Labe_LData1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_LData1.ForeColor = System.Drawing.Color.Black
    Me.Labe_LData1.Location = New System.Drawing.Point(450, 40)
    Me.Labe_LData1.Name = "Labe_LData1"
    Me.Labe_LData1.Size = New System.Drawing.Size(120, 26)
    Me.Labe_LData1.TabIndex = 30
    Me.Labe_LData1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Labe_Title_LR1
    '
    Me.Labe_Title_LR1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Labe_Title_LR1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Labe_Title_LR1.ForeColor = System.Drawing.Color.Black
    Me.Labe_Title_LR1.Location = New System.Drawing.Point(450, 10)
    Me.Labe_Title_LR1.Name = "Labe_Title_LR1"
    Me.Labe_Title_LR1.Size = New System.Drawing.Size(120, 26)
    Me.Labe_Title_LR1.TabIndex = 29
    Me.Labe_Title_LR1.Text = "水引重量"
    Me.Labe_Title_LR1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Label_Syubetu
    '
    Me.Label_Syubetu.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Label_Syubetu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label_Syubetu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Syubetu.ForeColor = System.Drawing.Color.Black
    Me.Label_Syubetu.Location = New System.Drawing.Point(200, 105)
    Me.Label_Syubetu.Name = "Label_Syubetu"
    Me.Label_Syubetu.Size = New System.Drawing.Size(180, 26)
    Me.Label_Syubetu.TabIndex = 28
    Me.Label_Syubetu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label_Title_Syubetu
    '
    Me.Label_Title_Syubetu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_Syubetu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Syubetu.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Syubetu.Location = New System.Drawing.Point(200, 75)
    Me.Label_Title_Syubetu.Name = "Label_Title_Syubetu"
    Me.Label_Title_Syubetu.Size = New System.Drawing.Size(180, 26)
    Me.Label_Title_Syubetu.TabIndex = 27
    Me.Label_Title_Syubetu.Text = "種別"
    Me.Label_Title_Syubetu.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Label_KotaiNo
    '
    Me.Label_KotaiNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Label_KotaiNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label_KotaiNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_KotaiNo.ForeColor = System.Drawing.Color.Black
    Me.Label_KotaiNo.Location = New System.Drawing.Point(20, 105)
    Me.Label_KotaiNo.Name = "Label_KotaiNo"
    Me.Label_KotaiNo.Size = New System.Drawing.Size(160, 26)
    Me.Label_KotaiNo.TabIndex = 26
    Me.Label_KotaiNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label_Title_KotaiNo
    '
    Me.Label_Title_KotaiNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_KotaiNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_KotaiNo.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_KotaiNo.Location = New System.Drawing.Point(20, 75)
    Me.Label_Title_KotaiNo.Name = "Label_Title_KotaiNo"
    Me.Label_Title_KotaiNo.Size = New System.Drawing.Size(160, 26)
    Me.Label_Title_KotaiNo.TabIndex = 25
    Me.Label_Title_KotaiNo.Text = "個体識別"
    Me.Label_Title_KotaiNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Label_Title_Right
    '
    Me.Label_Title_Right.AutoSize = True
    Me.Label_Title_Right.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_Right.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Right.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Right.Location = New System.Drawing.Point(400, 100)
    Me.Label_Title_Right.Name = "Label_Title_Right"
    Me.Label_Title_Right.Size = New System.Drawing.Size(32, 21)
    Me.Label_Title_Right.TabIndex = 24
    Me.Label_Title_Right.Text = "右"
    Me.Label_Title_Right.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Label_Gensan
    '
    Me.Label_Gensan.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Label_Gensan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label_Gensan.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.30189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Gensan.ForeColor = System.Drawing.Color.Black
    Me.Label_Gensan.Location = New System.Drawing.Point(200, 40)
    Me.Label_Gensan.Name = "Label_Gensan"
    Me.Label_Gensan.Size = New System.Drawing.Size(180, 26)
    Me.Label_Gensan.TabIndex = 23
    Me.Label_Gensan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label_Title_Left
    '
    Me.Label_Title_Left.AutoSize = True
    Me.Label_Title_Left.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_Left.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Left.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Left.Location = New System.Drawing.Point(400, 48)
    Me.Label_Title_Left.Name = "Label_Title_Left"
    Me.Label_Title_Left.Size = New System.Drawing.Size(32, 21)
    Me.Label_Title_Left.TabIndex = 4
    Me.Label_Title_Left.Text = "左"
    Me.Label_Title_Left.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Label_EdaNo
    '
    Me.Label_EdaNo.BackColor = System.Drawing.Color.Red
    Me.Label_EdaNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label_EdaNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 18.33962!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_EdaNo.ForeColor = System.Drawing.Color.Yellow
    Me.Label_EdaNo.Location = New System.Drawing.Point(20, 40)
    Me.Label_EdaNo.Name = "Label_EdaNo"
    Me.Label_EdaNo.Size = New System.Drawing.Size(160, 29)
    Me.Label_EdaNo.TabIndex = 1
    Me.Label_EdaNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label_Title_Gensan
    '
    Me.Label_Title_Gensan.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_Gensan.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Gensan.ForeColor = System.Drawing.Color.Black
    Me.Label_Title_Gensan.Location = New System.Drawing.Point(200, 10)
    Me.Label_Title_Gensan.Name = "Label_Title_Gensan"
    Me.Label_Title_Gensan.Size = New System.Drawing.Size(180, 26)
    Me.Label_Title_Gensan.TabIndex = 2
    Me.Label_Title_Gensan.Text = "原産地"
    Me.Label_Title_Gensan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'Label_Title_EdaNo
    '
    Me.Label_Title_EdaNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.22642!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_EdaNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Label_Title_EdaNo.Location = New System.Drawing.Point(69, 10)
    Me.Label_Title_EdaNo.Name = "Label_Title_EdaNo"
    Me.Label_Title_EdaNo.Size = New System.Drawing.Size(83, 29)
    Me.Label_Title_EdaNo.TabIndex = 0
    Me.Label_Title_EdaNo.Text = "枝No"
    Me.Label_Title_EdaNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    '
    'GroupBox1
    '
    Me.GroupBox1.BackColor = System.Drawing.Color.Yellow
    Me.GroupBox1.Controls.Add(Me.CmbDateProcessing_01)
    Me.GroupBox1.Controls.Add(Me.TxtEdaban_01)
    Me.GroupBox1.Controls.Add(Me.Button_Init)
    Me.GroupBox1.Controls.Add(Me.Label_Title_Eda01)
    Me.GroupBox1.Controls.Add(Me.Label_Title_Tokuisaki_01)
    Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(5, 1)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1300, 126)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "抽出条件（空白時はALL）"
    '
    'CmbDateProcessing_01
    '
    Me.CmbDateProcessing_01.AvailableBlank = False
    Me.CmbDateProcessing_01.DisplayMember = "ItemCode"
    Me.CmbDateProcessing_01.DropDownWidth = 140
    Me.CmbDateProcessing_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!)
    Me.CmbDateProcessing_01.FormattingEnabled = True
    Me.CmbDateProcessing_01.Location = New System.Drawing.Point(42, 56)
    Me.CmbDateProcessing_01.Name = "CmbDateProcessing_01"
    Me.CmbDateProcessing_01.Size = New System.Drawing.Size(140, 29)
    Me.CmbDateProcessing_01.TabIndex = 1
    Me.CmbDateProcessing_01.TabStop = False
    Me.CmbDateProcessing_01.ValueMember = "ItemCode"
    '
    'TxtEdaban_01
    '
    Me.TxtEdaban_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtEdaban_01.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban_01.Location = New System.Drawing.Point(247, 56)
    Me.TxtEdaban_01.Name = "TxtEdaban_01"
    Me.TxtEdaban_01.Size = New System.Drawing.Size(62, 28)
    Me.TxtEdaban_01.TabIndex = 3
    Me.TxtEdaban_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Button_Init
    '
    Me.Button_Init.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.Button_Init.CausesValidation = False
    Me.Button_Init.Location = New System.Drawing.Point(844, 49)
    Me.Button_Init.Name = "Button_Init"
    Me.Button_Init.Size = New System.Drawing.Size(160, 40)
    Me.Button_Init.TabIndex = 4
    Me.Button_Init.Text = "初期化"
    Me.Button_Init.UseVisualStyleBackColor = False
    '
    'Label_Title_Eda01
    '
    Me.Label_Title_Eda01.AutoSize = True
    Me.Label_Title_Eda01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Eda01.ForeColor = System.Drawing.Color.Blue
    Me.Label_Title_Eda01.Location = New System.Drawing.Point(243, 31)
    Me.Label_Title_Eda01.Name = "Label_Title_Eda01"
    Me.Label_Title_Eda01.Size = New System.Drawing.Size(58, 21)
    Me.Label_Title_Eda01.TabIndex = 2
    Me.Label_Title_Eda01.Text = "枝No"
    '
    'Label_Title_Tokuisaki_01
    '
    Me.Label_Title_Tokuisaki_01.AutoSize = True
    Me.Label_Title_Tokuisaki_01.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label_Title_Tokuisaki_01.ForeColor = System.Drawing.Color.Blue
    Me.Label_Title_Tokuisaki_01.Location = New System.Drawing.Point(48, 31)
    Me.Label_Title_Tokuisaki_01.Name = "Label_Title_Tokuisaki_01"
    Me.Label_Title_Tokuisaki_01.Size = New System.Drawing.Size(76, 21)
    Me.Label_Title_Tokuisaki_01.TabIndex = 0
    Me.Label_Title_Tokuisaki_01.Text = "加工日"
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
    Me.DataGridView1.MultiSelect = False
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.RowTemplate.Height = 21
    Me.DataGridView1.Size = New System.Drawing.Size(1526, 573)
    Me.DataGridView1.TabIndex = 5
    '
    'ButtonEnd
    '
    Me.ButtonEnd.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonEnd.Image = CType(resources.GetObject("ButtonEnd.Image"), System.Drawing.Image)
    Me.ButtonEnd.Location = New System.Drawing.Point(1458, 5)
    Me.ButtonEnd.Name = "ButtonEnd"
    Me.ButtonEnd.Size = New System.Drawing.Size(72, 120)
    Me.ButtonEnd.TabIndex = 3
    Me.ButtonEnd.Text = "ButtonEnd1"
    Me.ButtonEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonEnd.UseVisualStyleBackColor = True
    '
    'ButtonGenkaJidou
    '
    Me.ButtonGenkaJidou.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.ButtonGenkaJidou.Image = CType(resources.GetObject("ButtonGenkaJidou.Image"), System.Drawing.Image)
    Me.ButtonGenkaJidou.Location = New System.Drawing.Point(1386, 5)
    Me.ButtonGenkaJidou.Name = "ButtonGenkaJidou"
    Me.ButtonGenkaJidou.Size = New System.Drawing.Size(72, 120)
    Me.ButtonGenkaJidou.TabIndex = 2
    Me.ButtonGenkaJidou.Text = "ButtonUpdate1"
    Me.ButtonGenkaJidou.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonGenkaJidou.UseVisualStyleBackColor = True
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
    Me.ButtonPrint.Location = New System.Drawing.Point(1314, 5)
    Me.ButtonPrint.Name = "ButtonPrint"
    Me.ButtonPrint.Size = New System.Drawing.Size(72, 120)
    Me.ButtonPrint.TabIndex = 1
    Me.ButtonPrint.Text = "ButtonPrint1"
    Me.ButtonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.ButtonPrint.UseVisualStyleBackColor = True
    '
    'Form_EdaSeisan
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.ButtonGenkaJidou)
    Me.Controls.Add(Me.ButtonPrint)
    Me.Controls.Add(Me.ButtonEnd)
    Me.Controls.Add(Me.Label_GridData)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.Frame_IN)
    Me.Controls.Add(Me.DataGridView1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_EdaSeisan"
    Me.Text = "Form1"
    Me.Frame_IN.ResumeLayout(False)
    Me.Frame_IN.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents Frame_IN As GroupBox
  Friend WithEvents Label_Title_Left As Label
  Friend WithEvents Label_EdaNo As Label
  Friend WithEvents Label_Title_EdaNo As Label
  Friend WithEvents Label_Title_Gensan As Label
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Label_Title_Eda01 As Label
  Friend WithEvents Label_Title_Tokuisaki_01 As Label
  Friend WithEvents Label_GridData As Label
  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents ButtonEnd As T.R.ZCommonCtrl.ButtonEnd
  Friend WithEvents ButtonGenkaJidou As T.R.ZCommonCtrl.ButtonUpdate
  Friend WithEvents Label_Gensan As Label
  Friend WithEvents Labe_RData7 As Label
  Friend WithEvents Labe_LData7 As Label
  Friend WithEvents Labe_Title_LR7 As Label
  Friend WithEvents Labe_RData6 As Label
  Friend WithEvents Labe_LData6 As Label
  Friend WithEvents Labe_Title_LR6 As Label
  Friend WithEvents Labe_RData5 As Label
  Friend WithEvents Labe_LData5 As Label
  Friend WithEvents Labe_Title_LR5 As Label
  Friend WithEvents Labe_RData4 As Label
  Friend WithEvents Labe_LData4 As Label
  Friend WithEvents Labe_Title_LR4 As Label
  Friend WithEvents Labe_RData3 As Label
  Friend WithEvents Labe_LData3 As Label
  Friend WithEvents Labe_Title_LR3 As Label
  Friend WithEvents Labe_RData2 As Label
  Friend WithEvents Labe_LData2 As Label
  Friend WithEvents Labe_Title_LR2 As Label
  Friend WithEvents Labe_RData1 As Label
  Friend WithEvents Labe_LData1 As Label
  Friend WithEvents Labe_Title_LR1 As Label
  Friend WithEvents Label_Syubetu As Label
  Friend WithEvents Label_Title_Syubetu As Label
  Friend WithEvents Label_KotaiNo As Label
  Friend WithEvents Label_Title_KotaiNo As Label
  Friend WithEvents Label_Title_Right As Label
  Friend WithEvents TxtEdaban_01 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents Button_Init As Button
  Friend WithEvents lblInformation As Label
  Friend WithEvents CmbDateProcessing_01 As T.R.ZCommonCtrl.CmbDateKakouBi
  Friend WithEvents ButtonPrint As T.R.ZCommonCtrl.ButtonPrint
End Class
