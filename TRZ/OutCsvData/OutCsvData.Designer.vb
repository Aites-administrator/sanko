<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OutCsvData
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OutCsvData))
    Me.Label1 = New System.Windows.Forms.Label()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TxtEdaban1 = New T.R.ZCommonCtrl.TxtEdaban()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.CmbMstItem1 = New T.R.ZCommonCtrl.CmbMstItem()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.TxKotaiNo1 = New T.R.ZCommonCtrl.TxKotaiNo()
    Me.BtnClose = New System.Windows.Forms.Button()
    Me.CmbDateKakouBi1 = New T.R.ZCommonCtrl.CmbDateKakouBiOutCsv()
    Me.CmbDateKakouBi2 = New T.R.ZCommonCtrl.CmbDateKakouBiOutCsv()
    Me.BtnCsv = New T.R.ZCommonCtrl.BtnBase()
    Me.BtnCustomerSelect = New T.R.ZCommonCtrl.BtnBase()
    Me.CmbCustomerSelect1 = New T.R.ZCommonCtrl.CmbCustomerSelect()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.CmbSayu1 = New T.R.ZCommonCtrl.CmbSayu()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.Location = New System.Drawing.Point(28, 24)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(74, 27)
    Me.Label1.TabIndex = 39
    Me.Label1.Text = "加工日"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(12, 122)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.RowTemplate.Height = 21
    Me.DataGridView1.Size = New System.Drawing.Size(1238, 558)
    Me.DataGridView1.TabIndex = 40
    '
    'Label2
    '
    Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.Location = New System.Drawing.Point(247, 23)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(43, 27)
    Me.Label2.TabIndex = 41
    Me.Label2.Text = "～"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label3
    '
    Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.Location = New System.Drawing.Point(12, 76)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(90, 27)
    Me.Label3.TabIndex = 43
    Me.Label3.Text = "枝肉番号"
    Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxtEdaban1
    '
    Me.TxtEdaban1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!)
    Me.TxtEdaban1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtEdaban1.Location = New System.Drawing.Point(110, 76)
    Me.TxtEdaban1.Name = "TxtEdaban1"
    Me.TxtEdaban1.Size = New System.Drawing.Size(116, 27)
    Me.TxtEdaban1.TabIndex = 5
    Me.TxtEdaban1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label4
    '
    Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label4.Location = New System.Drawing.Point(245, 76)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(154, 27)
    Me.Label4.TabIndex = 45
    Me.Label4.Text = "得意先グループ"
    Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label5
    '
    Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label5.Location = New System.Drawing.Point(834, 77)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(107, 27)
    Me.Label5.TabIndex = 47
    Me.Label5.Text = "部位コード"
    Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'CmbMstItem1
    '
    Me.CmbMstItem1.AvailableBlank = False
    Me.CmbMstItem1.DisplayMember = "ItemName"
    Me.CmbMstItem1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!)
    Me.CmbMstItem1.FormattingEnabled = True
    Me.CmbMstItem1.Location = New System.Drawing.Point(947, 76)
    Me.CmbMstItem1.Name = "CmbMstItem1"
    Me.CmbMstItem1.Size = New System.Drawing.Size(222, 27)
    Me.CmbMstItem1.TabIndex = 8
    Me.CmbMstItem1.ValueMember = "ItemCode"
    '
    'Label6
    '
    Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label6.Location = New System.Drawing.Point(518, 24)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(133, 27)
    Me.Label6.TabIndex = 49
    Me.Label6.Text = "個体識別番号"
    Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TxKotaiNo1
    '
    Me.TxKotaiNo1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!)
    Me.TxKotaiNo1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxKotaiNo1.Location = New System.Drawing.Point(657, 24)
    Me.TxKotaiNo1.Name = "TxKotaiNo1"
    Me.TxKotaiNo1.Size = New System.Drawing.Size(143, 27)
    Me.TxKotaiNo1.TabIndex = 3
    Me.TxKotaiNo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'BtnClose
    '
    Me.BtnClose.Location = New System.Drawing.Point(1135, 698)
    Me.BtnClose.Name = "BtnClose"
    Me.BtnClose.Size = New System.Drawing.Size(86, 40)
    Me.BtnClose.TabIndex = 10
    Me.BtnClose.Text = "終了"
    Me.BtnClose.UseVisualStyleBackColor = True
    '
    'CmbDateKakouBi1
    '
    Me.CmbDateKakouBi1.AvailableBlank = False
    Me.CmbDateKakouBi1.DisplayMember = "ItemCode"
    Me.CmbDateKakouBi1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!)
    Me.CmbDateKakouBi1.FormattingEnabled = True
    Me.CmbDateKakouBi1.Location = New System.Drawing.Point(110, 23)
    Me.CmbDateKakouBi1.Name = "CmbDateKakouBi1"
    Me.CmbDateKakouBi1.Size = New System.Drawing.Size(131, 27)
    Me.CmbDateKakouBi1.TabIndex = 1
    Me.CmbDateKakouBi1.ValueMember = "ItemCode"
    '
    'CmbDateKakouBi2
    '
    Me.CmbDateKakouBi2.AvailableBlank = False
    Me.CmbDateKakouBi2.DisplayMember = "ItemCode"
    Me.CmbDateKakouBi2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!)
    Me.CmbDateKakouBi2.FormattingEnabled = True
    Me.CmbDateKakouBi2.Location = New System.Drawing.Point(296, 24)
    Me.CmbDateKakouBi2.Name = "CmbDateKakouBi2"
    Me.CmbDateKakouBi2.Size = New System.Drawing.Size(131, 27)
    Me.CmbDateKakouBi2.TabIndex = 2
    Me.CmbDateKakouBi2.ValueMember = "ItemCode"
    '
    'BtnCsv
    '
    Me.BtnCsv.Location = New System.Drawing.Point(1175, 60)
    Me.BtnCsv.Name = "BtnCsv"
    Me.BtnCsv.Size = New System.Drawing.Size(75, 43)
    Me.BtnCsv.TabIndex = 9
    Me.BtnCsv.Text = "CSV出力"
    Me.BtnCsv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.BtnCsv.UseVisualStyleBackColor = True
    '
    'BtnCustomerSelect
    '
    Me.BtnCustomerSelect.Location = New System.Drawing.Point(733, 70)
    Me.BtnCustomerSelect.Name = "BtnCustomerSelect"
    Me.BtnCustomerSelect.Size = New System.Drawing.Size(95, 43)
    Me.BtnCustomerSelect.TabIndex = 7
    Me.BtnCustomerSelect.Text = "得意先グループ確認"
    Me.BtnCustomerSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.BtnCustomerSelect.UseVisualStyleBackColor = True
    '
    'CmbCustomerSelect1
    '
    Me.CmbCustomerSelect1.AvailableBlank = False
    Me.CmbCustomerSelect1.DisplayMember = "ItemName"
    Me.CmbCustomerSelect1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!)
    Me.CmbCustomerSelect1.FormattingEnabled = True
    Me.CmbCustomerSelect1.Location = New System.Drawing.Point(405, 76)
    Me.CmbCustomerSelect1.Name = "CmbCustomerSelect1"
    Me.CmbCustomerSelect1.Size = New System.Drawing.Size(322, 27)
    Me.CmbCustomerSelect1.TabIndex = 6
    Me.CmbCustomerSelect1.ValueMember = "ItemCode"
    '
    'Label7
    '
    Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label7.Location = New System.Drawing.Point(834, 24)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(107, 27)
    Me.Label7.TabIndex = 50
    Me.Label7.Text = "左右区分"
    Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'CmbSayu1
    '
    Me.CmbSayu1.AvailableBlank = False
    Me.CmbSayu1.DisplayMember = "ItemName"
    Me.CmbSayu1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 14.26415!)
    Me.CmbSayu1.ForeColor = System.Drawing.SystemColors.WindowText
    Me.CmbSayu1.FormattingEnabled = True
    Me.CmbSayu1.Location = New System.Drawing.Point(947, 25)
    Me.CmbSayu1.Name = "CmbSayu1"
    Me.CmbSayu1.Size = New System.Drawing.Size(121, 27)
    Me.CmbSayu1.TabIndex = 4
    Me.CmbSayu1.ValueMember = "ItemCode"
    '
    'OutCsvData
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1262, 775)
    Me.Controls.Add(Me.CmbSayu1)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.CmbCustomerSelect1)
    Me.Controls.Add(Me.BtnCustomerSelect)
    Me.Controls.Add(Me.BtnCsv)
    Me.Controls.Add(Me.CmbDateKakouBi2)
    Me.Controls.Add(Me.CmbDateKakouBi1)
    Me.Controls.Add(Me.BtnClose)
    Me.Controls.Add(Me.TxKotaiNo1)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.CmbMstItem1)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.TxtEdaban1)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.DataGridView1)
    Me.Controls.Add(Me.Label1)
    Me.KeyPreview = True
    Me.Name = "OutCsvData"
    Me.Text = "Form1"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As Label
  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents Label2 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents TxtEdaban1 As T.R.ZCommonCtrl.TxtEdaban
  Friend WithEvents Label4 As Label
  Friend WithEvents Label5 As Label
  Friend WithEvents CmbMstItem1 As T.R.ZCommonCtrl.CmbMstItem
  Friend WithEvents Label6 As Label
  Friend WithEvents TxKotaiNo1 As T.R.ZCommonCtrl.TxKotaiNo
  Friend WithEvents BtnClose As Button
  Friend WithEvents CmbDateKakouBi1 As T.R.ZCommonCtrl.CmbDateKakouBiOutCsv
  Friend WithEvents CmbDateKakouBi2 As T.R.ZCommonCtrl.CmbDateKakouBiOutCsv
  Friend WithEvents BtnCsv As T.R.ZCommonCtrl.BtnBase
  Friend WithEvents BtnCustomerSelect As T.R.ZCommonCtrl.BtnBase
  Friend WithEvents CmbCustomerSelect1 As T.R.ZCommonCtrl.CmbCustomerSelect
  Friend WithEvents Label7 As Label
  Friend WithEvents CmbSayu1 As T.R.ZCommonCtrl.CmbSayu
End Class
