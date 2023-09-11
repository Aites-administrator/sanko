<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SForm_Tanaorosi
  Inherits T.R.ZCommonCtrl.SFBase

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SForm_Tanaorosi))
    Me.btnClose = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.CmbDateTanaorosi1 = New T.R.ZCommonCtrl.CmbDateTanaorosi()
    Me.SuspendLayout()
    '
    'btnClose
    '
    Me.btnClose.Location = New System.Drawing.Point(259, 161)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(118, 39)
    Me.btnClose.TabIndex = 2
    Me.btnClose.Text = "F12:閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(36, 83)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(73, 21)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "棚卸日"
    '
    'CmbDateTanaorosi1
    '
    Me.CmbDateTanaorosi1.AvailableBlank = False
    Me.CmbDateTanaorosi1.DisplayMember = "ItemCode"
    Me.CmbDateTanaorosi1.FormattingEnabled = True
    Me.CmbDateTanaorosi1.Location = New System.Drawing.Point(130, 80)
    Me.CmbDateTanaorosi1.Name = "CmbDateTanaorosi1"
    Me.CmbDateTanaorosi1.Size = New System.Drawing.Size(247, 29)
    Me.CmbDateTanaorosi1.TabIndex = 3
    Me.CmbDateTanaorosi1.ValueMember = "ItemCode"
    '
    'SForm_Tanaorosi
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
    Me.ClientSize = New System.Drawing.Size(413, 257)
    Me.Controls.Add(Me.CmbDateTanaorosi1)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.Label1)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "SForm_Tanaorosi"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As Label
  Friend WithEvents btnClose As Button
  Friend WithEvents CmbDateTanaorosi1 As T.R.ZCommonCtrl.CmbDateTanaorosi
End Class
