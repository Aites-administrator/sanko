<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_MstMent
  Inherits T.R.ZCommonCtrl.MFBaseDgv

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_MstMent))
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.btnRegist = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.lblLastUpdate = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(12, 140)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.Size = New System.Drawing.Size(1286, 607)
    Me.DataGridView1.TabIndex = 0
    Me.DataGridView1.TabStop = False
    '
    'btnRegist
    '
    Me.btnRegist.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnRegist.Location = New System.Drawing.Point(952, 773)
    Me.btnRegist.Name = "btnRegist"
    Me.btnRegist.Size = New System.Drawing.Size(96, 33)
    Me.btnRegist.TabIndex = 2
    Me.btnRegist.TabStop = False
    Me.btnRegist.Text = "登録"
    Me.btnRegist.UseVisualStyleBackColor = True
    '
    'btnDelete
    '
    Me.btnDelete.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnDelete.Location = New System.Drawing.Point(1081, 773)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(96, 33)
    Me.btnDelete.TabIndex = 3
    Me.btnDelete.TabStop = False
    Me.btnDelete.Text = "削除"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnClose
    '
    Me.btnClose.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnClose.Location = New System.Drawing.Point(1202, 773)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(96, 33)
    Me.btnClose.TabIndex = 4
    Me.btnClose.TabStop = False
    Me.btnClose.Text = "閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'lblLastUpdate
    '
    Me.lblLastUpdate.AutoSize = True
    Me.lblLastUpdate.Location = New System.Drawing.Point(777, 123)
    Me.lblLastUpdate.Name = "lblLastUpdate"
    Me.lblLastUpdate.Size = New System.Drawing.Size(430, 14)
    Me.lblLastUpdate.TabIndex = 5
    Me.lblLastUpdate.Text = "最終更新日時（KDATE）を保持し、更新・削除時に抽出条件として使用する"
    '
    'lblInformation
    '
    Me.lblInformation.AutoSize = True
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Red
    Me.lblInformation.Location = New System.Drawing.Point(8, 806)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(294, 19)
    Me.lblInformation.TabIndex = 6
    Me.lblInformation.Text = "ここに入力時の説明文が表示されます"
    '
    'Form_MstMent
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(1534, 834)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.lblLastUpdate)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnRegist)
    Me.Controls.Add(Me.DataGridView1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_MstMent"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents btnRegist As Button
  Friend WithEvents btnDelete As Button
  Friend WithEvents btnClose As Button
  Friend WithEvents lblLastUpdate As Label
  Friend WithEvents lblInformation As Label
End Class
