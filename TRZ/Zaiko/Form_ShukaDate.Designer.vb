<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_ShukaDateInput
  Inherits T.R.ZCommonCtrl.SFBase

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_ShukaDateInput))
    Me.LabelMsg = New System.Windows.Forms.Label()
    Me.BtnOK = New System.Windows.Forms.Button()
    Me.BtnCancel = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TxtSyukaDate = New T.R.ZCommonCtrl.TxtDateBase()
    Me.SuspendLayout()
    '
    'LabelMsg
    '
    Me.LabelMsg.AutoSize = True
    Me.LabelMsg.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.LabelMsg.Location = New System.Drawing.Point(42, 43)
    Me.LabelMsg.Name = "LabelMsg"
    Me.LabelMsg.Size = New System.Drawing.Size(67, 21)
    Me.LabelMsg.TabIndex = 0
    Me.LabelMsg.Text = "Label1"
    '
    'BtnOK
    '
    Me.BtnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.BtnOK.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.BtnOK.Location = New System.Drawing.Point(84, 193)
    Me.BtnOK.Name = "BtnOK"
    Me.BtnOK.Size = New System.Drawing.Size(126, 40)
    Me.BtnOK.TabIndex = 3
    Me.BtnOK.Text = "OK"
    Me.BtnOK.UseVisualStyleBackColor = False
    '
    'BtnCancel
    '
    Me.BtnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.BtnCancel.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.BtnCancel.Location = New System.Drawing.Point(304, 193)
    Me.BtnCancel.Name = "BtnCancel"
    Me.BtnCancel.Size = New System.Drawing.Size(126, 40)
    Me.BtnCancel.TabIndex = 4
    Me.BtnCancel.Text = "キャンセル"
    Me.BtnCancel.UseVisualStyleBackColor = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.Location = New System.Drawing.Point(42, 124)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(94, 21)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "出荷日付"
    '
    'TxtSyukaDate
    '
    Me.TxtSyukaDate.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtSyukaDate.Location = New System.Drawing.Point(155, 121)
    Me.TxtSyukaDate.Name = "TxtSyukaDate"
    Me.TxtSyukaDate.Size = New System.Drawing.Size(163, 28)
    Me.TxtSyukaDate.TabIndex = 2
    '
    'Form_ShukaDateInput
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
    Me.ClientSize = New System.Drawing.Size(549, 254)
    Me.Controls.Add(Me.TxtSyukaDate)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.BtnCancel)
    Me.Controls.Add(Me.BtnOK)
    Me.Controls.Add(Me.LabelMsg)
    Me.DoubleBuffered = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_ShukaDateInput"
    Me.Text = ""
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents LabelMsg As Label
  Friend WithEvents BtnOK As Button
  Friend WithEvents BtnCancel As Button
  Friend WithEvents Label1 As Label
  Friend WithEvents TxtSyukaDate As T.R.ZCommonCtrl.TxtDateBase
End Class
