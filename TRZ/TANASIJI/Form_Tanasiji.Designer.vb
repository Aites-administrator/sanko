<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Tanasiji
  Inherits T.R.ZCommonCtrl.FormBase

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Tanasiji))
    Me.txtTanaorosiDate = New T.R.ZCommonCtrl.TxtDateBase()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.btnPreparation = New System.Windows.Forms.Button()
    Me.btnReflect = New System.Windows.Forms.Button()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'txtTanaorosiDate
    '
    Me.txtTanaorosiDate.Location = New System.Drawing.Point(282, 147)
    Me.txtTanaorosiDate.Name = "txtTanaorosiDate"
    Me.txtTanaorosiDate.Size = New System.Drawing.Size(139, 28)
    Me.txtTanaorosiDate.TabIndex = 0
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(104, 150)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(115, 21)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "棚卸日入力"
    '
    'btnPreparation
    '
    Me.btnPreparation.Location = New System.Drawing.Point(19, 365)
    Me.btnPreparation.Name = "btnPreparation"
    Me.btnPreparation.Size = New System.Drawing.Size(139, 32)
    Me.btnPreparation.TabIndex = 69
    Me.btnPreparation.Text = "F1:準備実行"
    Me.btnPreparation.UseVisualStyleBackColor = True
    '
    'btnReflect
    '
    Me.btnReflect.Location = New System.Drawing.Point(193, 365)
    Me.btnReflect.Name = "btnReflect"
    Me.btnReflect.Size = New System.Drawing.Size(139, 32)
    Me.btnReflect.TabIndex = 70
    Me.btnReflect.Text = "F5:確定実行"
    Me.btnReflect.UseVisualStyleBackColor = True
    Me.btnReflect.Visible = False
    '
    'btnClose
    '
    Me.btnClose.Location = New System.Drawing.Point(367, 365)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(139, 32)
    Me.btnClose.TabIndex = 71
    Me.btnClose.Text = "F12:閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'Form_Tanasiji
    '
    Me.ClientSize = New System.Drawing.Size(524, 409)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnReflect)
    Me.Controls.Add(Me.btnPreparation)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.txtTanaorosiDate)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_Tanasiji"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents txtTanaorosiDate As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents Label1 As Label
  Friend WithEvents btnPreparation As Button
  Friend WithEvents btnReflect As Button
  Friend WithEvents btnClose As Button
End Class
