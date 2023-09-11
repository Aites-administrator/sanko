<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_HenkinInput
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_HenkinInput))
    Me.Label1 = New System.Windows.Forms.Label()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnOk = New System.Windows.Forms.Button()
    Me.txtHenkingaku = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.Location = New System.Drawing.Point(44, 39)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(329, 21)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "返品時の単価を入力して下さい！！"
    '
    'btnCancel
    '
    Me.btnCancel.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnCancel.Location = New System.Drawing.Point(321, 191)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(126, 40)
    Me.btnCancel.TabIndex = 5
    Me.btnCancel.Text = "キャンセル"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnOk
    '
    Me.btnOk.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnOk.Location = New System.Drawing.Point(101, 191)
    Me.btnOk.Name = "btnOk"
    Me.btnOk.Size = New System.Drawing.Size(126, 40)
    Me.btnOk.TabIndex = 4
    Me.btnOk.Text = "OK"
    Me.btnOk.UseVisualStyleBackColor = True
    '
    'txtHenkingaku
    '
    Me.txtHenkingaku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.txtHenkingaku.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.txtHenkingaku.Location = New System.Drawing.Point(101, 114)
    Me.txtHenkingaku.Name = "txtHenkingaku"
    Me.txtHenkingaku.Size = New System.Drawing.Size(148, 28)
    Me.txtHenkingaku.TabIndex = 6
    Me.txtHenkingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Form_HenkinInput
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(549, 254)
    Me.Controls.Add(Me.txtHenkingaku)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnOk)
    Me.Controls.Add(Me.Label1)
    Me.DoubleBuffered = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_HenkinInput"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents Label1 As Label
  Friend WithEvents btnCancel As Button
  Friend WithEvents btnOk As Button
  Friend WithEvents txtHenkingaku As T.R.ZCommonCtrl.TxtNumericBase
End Class
