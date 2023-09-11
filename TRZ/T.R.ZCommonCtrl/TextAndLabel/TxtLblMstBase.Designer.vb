<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TxtLblMstBase
  Inherits System.Windows.Forms.UserControl

  'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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
    Me.TxtName = New T.R.ZCommonCtrl.TxtBase()
    Me.TxtDummy = New T.R.ZCommonCtrl.TxtMstBase()
    Me.SuspendLayout()
    '
    'TxtName
    '
    Me.TxtName.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtName.Location = New System.Drawing.Point(78, 2)
    Me.TxtName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtName.Name = "TxtName"
    Me.TxtName.Size = New System.Drawing.Size(346, 28)
    Me.TxtName.TabIndex = 5
    '
    'TxtDummy
    '
    Me.TxtDummy.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtDummy.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtDummy.Location = New System.Drawing.Point(0, 0)
    Me.TxtDummy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtDummy.Name = "TxtDummy"
    Me.TxtDummy.Size = New System.Drawing.Size(70, 28)
    Me.TxtDummy.TabIndex = 0
    Me.TxtDummy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxtLblMstBase
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.TxtName)
    Me.Controls.Add(Me.TxtDummy)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.Name = "TxtLblMstBase"
    Me.Size = New System.Drawing.Size(432, 36)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents TxtDummy As TxtMstBase
  Friend WithEvents TxtName As TxtBase
End Class
