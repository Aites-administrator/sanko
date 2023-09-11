<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_ReportPrnRange
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_ReportPrnRange))
    Me.Cmd_Buton01 = New System.Windows.Forms.Button()
    Me.Cmd_Buton12 = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cbRangeEnd = New System.Windows.Forms.ComboBox()
    Me.cbRangeStart = New System.Windows.Forms.ComboBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'Cmd_Buton01
    '
    Me.Cmd_Buton01.Font = New System.Drawing.Font("ＭＳ ゴシック", 18.33962!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Cmd_Buton01.Location = New System.Drawing.Point(157, 362)
    Me.Cmd_Buton01.Name = "Cmd_Buton01"
    Me.Cmd_Buton01.Size = New System.Drawing.Size(180, 55)
    Me.Cmd_Buton01.TabIndex = 20
    Me.Cmd_Buton01.Text = "F1：印刷"
    Me.Cmd_Buton01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.Cmd_Buton01.UseVisualStyleBackColor = True
    '
    'Cmd_Buton12
    '
    Me.Cmd_Buton12.Font = New System.Drawing.Font("ＭＳ ゴシック", 18.33962!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Cmd_Buton12.Location = New System.Drawing.Point(811, 362)
    Me.Cmd_Buton12.Name = "Cmd_Buton12"
    Me.Cmd_Buton12.Size = New System.Drawing.Size(180, 55)
    Me.Cmd_Buton12.TabIndex = 31
    Me.Cmd_Buton12.Text = "F12：閉じる"
    Me.Cmd_Buton12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.Cmd_Buton12.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 23.77358!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.Black
    Me.Label1.Location = New System.Drawing.Point(415, 70)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(261, 35)
    Me.Label1.TabIndex = 32
    Me.Label1.Text = "印刷範囲の入力"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 23.77358!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.Black
    Me.Label2.Location = New System.Drawing.Point(523, 187)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(51, 35)
    Me.Label2.TabIndex = 33
    Me.Label2.Text = "～"
    '
    'cbRangeEnd
    '
    Me.cbRangeEnd.Font = New System.Drawing.Font("MS UI Gothic", 18.33962!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.cbRangeEnd.FormattingEnabled = True
    Me.cbRangeEnd.IntegralHeight = False
    Me.cbRangeEnd.Location = New System.Drawing.Point(646, 187)
    Me.cbRangeEnd.Name = "cbRangeEnd"
    Me.cbRangeEnd.Size = New System.Drawing.Size(360, 35)
    Me.cbRangeEnd.TabIndex = 35
    '
    'cbRangeStart
    '
    Me.cbRangeStart.Font = New System.Drawing.Font("MS UI Gothic", 18.33962!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.cbRangeStart.FormattingEnabled = True
    Me.cbRangeStart.IntegralHeight = False
    Me.cbRangeStart.Location = New System.Drawing.Point(89, 187)
    Me.cbRangeStart.Name = "cbRangeStart"
    Me.cbRangeStart.Size = New System.Drawing.Size(360, 35)
    Me.cbRangeStart.TabIndex = 34
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.ForeColor = System.Drawing.Color.Black
    Me.Label3.Location = New System.Drawing.Point(94, 157)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(104, 21)
    Me.Label3.TabIndex = 36
    Me.Label3.Text = "開始コード"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label4.ForeColor = System.Drawing.Color.Black
    Me.Label4.Location = New System.Drawing.Point(651, 157)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(104, 21)
    Me.Label4.TabIndex = 37
    Me.Label4.Text = "終了コード"
    '
    'Form_ReportPrnRange
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ClientSize = New System.Drawing.Size(1111, 483)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.cbRangeEnd)
    Me.Controls.Add(Me.cbRangeStart)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Cmd_Buton12)
    Me.Controls.Add(Me.Cmd_Buton01)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Location = New System.Drawing.Point(0, 50)
    Me.Name = "Form_ReportPrnRange"
    Me.Text = "マスター登録リスト印刷"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Cmd_Buton01 As Button
  Friend WithEvents Cmd_Buton12 As Button
  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents cbRangeEnd As ComboBox
  Friend WithEvents cbRangeStart As ComboBox
  Friend WithEvents Label3 As Label
  Friend WithEvents Label4 As Label
End Class
