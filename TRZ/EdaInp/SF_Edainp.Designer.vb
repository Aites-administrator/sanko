<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SF_Edainp
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SF_Edainp))
    Me.TxtDateBase1 = New T.R.ZCommonCtrl.TxtDateBase()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.btnPrint = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.CmbMstShiresakiFrom = New T.R.ZCommonCtrl.CmbMstShiresaki()
    Me.CmbMstShiresakiTo = New T.R.ZCommonCtrl.CmbMstShiresaki()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'TxtDateBase1
    '
    Me.TxtDateBase1.Location = New System.Drawing.Point(137, 60)
    Me.TxtDateBase1.Name = "TxtDateBase1"
    Me.TxtDateBase1.Size = New System.Drawing.Size(160, 28)
    Me.TxtDateBase1.TabIndex = 0
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(23, 63)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(94, 21)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "仕入日付"
    '
    'btnClose
    '
    Me.btnClose.Location = New System.Drawing.Point(481, 299)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(112, 33)
    Me.btnClose.TabIndex = 70
    Me.btnClose.TabStop = False
    Me.btnClose.Text = "F12:閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'btnPrint
    '
    Me.btnPrint.Location = New System.Drawing.Point(332, 299)
    Me.btnPrint.Name = "btnPrint"
    Me.btnPrint.Size = New System.Drawing.Size(112, 33)
    Me.btnPrint.TabIndex = 69
    Me.btnPrint.TabStop = False
    Me.btnPrint.Text = "F1:実行"
    Me.btnPrint.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(23, 146)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(73, 21)
    Me.Label2.TabIndex = 71
    Me.Label2.Text = "仕入先"
    '
    'CmbMstShiresakiFrom
    '
    Me.CmbMstShiresakiFrom.AvailableBlank = False
    Me.CmbMstShiresakiFrom.DisplayMember = "ItemName"
    Me.CmbMstShiresakiFrom.FormattingEnabled = True
    Me.CmbMstShiresakiFrom.Location = New System.Drawing.Point(137, 146)
    Me.CmbMstShiresakiFrom.Name = "CmbMstShiresakiFrom"
    Me.CmbMstShiresakiFrom.Size = New System.Drawing.Size(328, 29)
    Me.CmbMstShiresakiFrom.TabIndex = 72
    Me.CmbMstShiresakiFrom.ValueMember = "ItemCode"
    '
    'CmbMstShiresakiTo
    '
    Me.CmbMstShiresakiTo.AvailableBlank = False
    Me.CmbMstShiresakiTo.DisplayMember = "ItemName"
    Me.CmbMstShiresakiTo.FormattingEnabled = True
    Me.CmbMstShiresakiTo.Location = New System.Drawing.Point(265, 201)
    Me.CmbMstShiresakiTo.Name = "CmbMstShiresakiTo"
    Me.CmbMstShiresakiTo.Size = New System.Drawing.Size(328, 29)
    Me.CmbMstShiresakiTo.TabIndex = 73
    Me.CmbMstShiresakiTo.ValueMember = "ItemCode"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(187, 206)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(31, 21)
    Me.Label3.TabIndex = 74
    Me.Label3.Text = "～"
    '
    'SF_Edainp
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.ClientSize = New System.Drawing.Size(629, 357)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.CmbMstShiresakiTo)
    Me.Controls.Add(Me.CmbMstShiresakiFrom)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnPrint)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TxtDateBase1)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "SF_Edainp"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents TxtDateBase1 As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents Label1 As Label
  Friend WithEvents btnClose As Button
  Friend WithEvents btnPrint As Button
  Friend WithEvents Label2 As Label
  Friend WithEvents CmbMstShiresakiFrom As T.R.ZCommonCtrl.CmbMstShiresaki
  Friend WithEvents CmbMstShiresakiTo As T.R.ZCommonCtrl.CmbMstShiresaki
  Friend WithEvents Label3 As Label
End Class
