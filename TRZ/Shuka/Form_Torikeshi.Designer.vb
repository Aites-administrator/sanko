<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_Torikeshi
  Inherits T.R.ZCommonCtrl.SFBase

  'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
  <System.Diagnostics.DebuggerNonUserCode()>
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
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Torikeshi))
        Me.btnHenpin = New System.Windows.Forms.Button()
        Me.Label_BuiCode = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label_No = New System.Windows.Forms.Label()
        Me.Label_EdaNo = New System.Windows.Forms.Label()
        Me.Label_Kakoubi = New System.Windows.Forms.Label()
        Me.Label_Title_No = New System.Windows.Forms.Label()
        Me.Label_Title_EdaNo = New System.Windows.Forms.Label()
        Me.Label_Title_BuiCode = New System.Windows.Forms.Label()
        Me.Label_Title_Kakoubi = New System.Windows.Forms.Label()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnTorikeshi = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnHenpin
        '
        Me.btnHenpin.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnHenpin.Font = New System.Drawing.Font("MS UI Gothic", 8.830189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnHenpin.Location = New System.Drawing.Point(378, 86)
        Me.btnHenpin.Name = "btnHenpin"
        Me.btnHenpin.Size = New System.Drawing.Size(82, 49)
        Me.btnHenpin.TabIndex = 33
        Me.btnHenpin.Text = "返品"
        Me.btnHenpin.UseVisualStyleBackColor = False
        '
        'Label_BuiCode
        '
        Me.Label_BuiCode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_BuiCode.ForeColor = System.Drawing.Color.Black
        Me.Label_BuiCode.Location = New System.Drawing.Point(125, 45)
        Me.Label_BuiCode.Name = "Label_BuiCode"
        Me.Label_BuiCode.Size = New System.Drawing.Size(247, 25)
        Me.Label_BuiCode.TabIndex = 32
        Me.Label_BuiCode.Text = "部位コード"
        Me.Label_BuiCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 241)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(451, 62)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "出荷を取消したいときは「取消」ボタンを押してください。"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label_No
        '
        Me.Label_No.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_No.ForeColor = System.Drawing.Color.Black
        Me.Label_No.Location = New System.Drawing.Point(125, 95)
        Me.Label_No.Name = "Label_No"
        Me.Label_No.Size = New System.Drawing.Size(120, 25)
        Me.Label_No.TabIndex = 30
        Me.Label_No.Text = "小口"
        Me.Label_No.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label_EdaNo
        '
        Me.Label_EdaNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_EdaNo.ForeColor = System.Drawing.Color.Black
        Me.Label_EdaNo.Location = New System.Drawing.Point(125, 70)
        Me.Label_EdaNo.Name = "Label_EdaNo"
        Me.Label_EdaNo.Size = New System.Drawing.Size(120, 25)
        Me.Label_EdaNo.TabIndex = 29
        Me.Label_EdaNo.Text = "枝番"
        Me.Label_EdaNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label_Kakoubi
        '
        Me.Label_Kakoubi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Kakoubi.ForeColor = System.Drawing.Color.Black
        Me.Label_Kakoubi.Location = New System.Drawing.Point(125, 20)
        Me.Label_Kakoubi.Name = "Label_Kakoubi"
        Me.Label_Kakoubi.Size = New System.Drawing.Size(120, 25)
        Me.Label_Kakoubi.TabIndex = 28
        Me.Label_Kakoubi.Text = "加工日"
        Me.Label_Kakoubi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label_Title_No
        '
        Me.Label_Title_No.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Title_No.ForeColor = System.Drawing.Color.Black
        Me.Label_Title_No.Location = New System.Drawing.Point(4, 95)
        Me.Label_Title_No.Name = "Label_Title_No"
        Me.Label_Title_No.Size = New System.Drawing.Size(118, 25)
        Me.Label_Title_No.TabIndex = 27
        Me.Label_Title_No.Text = "小口："
        Me.Label_Title_No.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Title_EdaNo
        '
        Me.Label_Title_EdaNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Title_EdaNo.ForeColor = System.Drawing.Color.Black
        Me.Label_Title_EdaNo.Location = New System.Drawing.Point(4, 70)
        Me.Label_Title_EdaNo.Name = "Label_Title_EdaNo"
        Me.Label_Title_EdaNo.Size = New System.Drawing.Size(118, 25)
        Me.Label_Title_EdaNo.TabIndex = 26
        Me.Label_Title_EdaNo.Text = "枝番："
        Me.Label_Title_EdaNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Title_BuiCode
        '
        Me.Label_Title_BuiCode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Title_BuiCode.ForeColor = System.Drawing.Color.Black
        Me.Label_Title_BuiCode.Location = New System.Drawing.Point(4, 45)
        Me.Label_Title_BuiCode.Name = "Label_Title_BuiCode"
        Me.Label_Title_BuiCode.Size = New System.Drawing.Size(118, 25)
        Me.Label_Title_BuiCode.TabIndex = 25
        Me.Label_Title_BuiCode.Text = "部位コード："
        Me.Label_Title_BuiCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Title_Kakoubi
        '
        Me.Label_Title_Kakoubi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Title_Kakoubi.ForeColor = System.Drawing.Color.Black
        Me.Label_Title_Kakoubi.Location = New System.Drawing.Point(4, 20)
        Me.Label_Title_Kakoubi.Name = "Label_Title_Kakoubi"
        Me.Label_Title_Kakoubi.Size = New System.Drawing.Size(118, 25)
        Me.Label_Title_Kakoubi.TabIndex = 24
        Me.Label_Title_Kakoubi.Text = "加工日："
        Me.Label_Title_Kakoubi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnCancel.Font = New System.Drawing.Font("MS UI Gothic", 8.830189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(378, 149)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(82, 49)
        Me.BtnCancel.TabIndex = 23
        Me.BtnCancel.Text = "キャンセル"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'BtnTorikeshi
        '
        Me.BtnTorikeshi.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnTorikeshi.Font = New System.Drawing.Font("MS UI Gothic", 8.830189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnTorikeshi.Location = New System.Drawing.Point(378, 21)
        Me.BtnTorikeshi.Name = "BtnTorikeshi"
        Me.BtnTorikeshi.Size = New System.Drawing.Size(82, 49)
        Me.BtnTorikeshi.TabIndex = 22
        Me.BtnTorikeshi.Text = "取消"
        Me.BtnTorikeshi.UseVisualStyleBackColor = False
        '
        'Form_Torikeshi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(471, 339)
        Me.Controls.Add(Me.btnHenpin)
        Me.Controls.Add(Me.Label_BuiCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label_No)
        Me.Controls.Add(Me.Label_EdaNo)
        Me.Controls.Add(Me.Label_Kakoubi)
        Me.Controls.Add(Me.Label_Title_No)
        Me.Controls.Add(Me.Label_Title_EdaNo)
        Me.Controls.Add(Me.Label_Title_BuiCode)
        Me.Controls.Add(Me.Label_Title_Kakoubi)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnTorikeshi)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "Form_Torikeshi"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label_No As Label
  Friend WithEvents Label_EdaNo As Label
  Friend WithEvents Label_Kakoubi As Label
  Friend WithEvents Label_Title_No As Label
  Friend WithEvents Label_Title_EdaNo As Label
  Friend WithEvents Label_Title_BuiCode As Label
  Friend WithEvents Label_Title_Kakoubi As Label
  Friend WithEvents BtnCancel As Button
  Friend WithEvents BtnTorikeshi As Button
  Friend WithEvents Label_BuiCode As Label
  Friend WithEvents Label1 As Label
  Friend WithEvents btnHenpin As Button
End Class
