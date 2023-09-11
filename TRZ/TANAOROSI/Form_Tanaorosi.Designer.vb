<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Tanaorosi
  Inherits T.R.ZCommonCtrl.MFBaseDgv

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Tanaorosi))
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.txtNyukobi = New T.R.ZCommonCtrl.TxtDateBase()
    Me.TxtLblMstShiresaki1 = New T.R.ZCommonCtrl.TxtLblMstShiresaki()
    Me.TxKotaiNo1 = New T.R.ZCommonCtrl.TxKotaiNo()
    Me.txtKakoubi = New T.R.ZCommonCtrl.TxtDateBase()
    Me.TxtLblMstItem1 = New T.R.ZCommonCtrl.TxtLblMstItem()
    Me.txtCarton = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.txtStockCount = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.TxtWeitghtKg1 = New T.R.ZCommonCtrl.TxtWeitghtKg()
    Me.txtCostKg = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.lblListStat = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.btnEntry = New System.Windows.Forms.Button()
    Me.btnMoveNext = New System.Windows.Forms.Button()
    Me.btnShowStockList = New System.Windows.Forms.Button()
    Me.btnShowUntreated = New System.Windows.Forms.Button()
    Me.btnDelete = New System.Windows.Forms.Button()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.TxtLblMstStaff1 = New T.R.ZCommonCtrl.TxtLblMstStaff()
    Me.txtTanaorosibi = New T.R.ZCommonCtrl.TxtDateBase()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.txtUntreatedFlg = New T.R.ZCommonCtrl.TxtNumericBase()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.btnPrint = New System.Windows.Forms.Button()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(48, 57)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(94, 21)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "入庫日付"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(632, 57)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(120, 21)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "仕入先コード"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(48, 102)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(136, 21)
    Me.Label3.TabIndex = 2
    Me.Label3.Text = "個体識別番号"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(632, 102)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(73, 21)
    Me.Label4.TabIndex = 3
    Me.Label4.Text = "加工日"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(48, 147)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(99, 21)
    Me.Label5.TabIndex = 4
    Me.Label5.Text = "部位コード"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(632, 147)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(97, 21)
    Me.Label6.TabIndex = 5
    Me.Label6.Text = "カートンNo"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(48, 192)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(73, 21)
    Me.Label7.TabIndex = 6
    Me.Label7.Text = "在庫数"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(632, 192)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(87, 21)
    Me.Label8.TabIndex = 7
    Me.Label8.Text = "重量(Kg)"
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(48, 237)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(117, 21)
    Me.Label9.TabIndex = 8
    Me.Label9.Text = "Kg仕入単価"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(48, 12)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(73, 21)
    Me.Label10.TabIndex = 9
    Me.Label10.Text = "担当者"
    '
    'txtNyukobi
    '
    Me.txtNyukobi.Location = New System.Drawing.Point(217, 57)
    Me.txtNyukobi.Name = "txtNyukobi"
    Me.txtNyukobi.Size = New System.Drawing.Size(147, 28)
    Me.txtNyukobi.TabIndex = 1
    '
    'TxtLblMstShiresaki1
    '
    Me.TxtLblMstShiresaki1.BackColor = System.Drawing.SystemColors.Control
    Me.TxtLblMstShiresaki1.CodeTxt = ""
    Me.TxtLblMstShiresaki1.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstShiresaki1.Location = New System.Drawing.Point(778, 57)
    Me.TxtLblMstShiresaki1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtLblMstShiresaki1.Name = "TxtLblMstShiresaki1"
    Me.TxtLblMstShiresaki1.Size = New System.Drawing.Size(432, 36)
    Me.TxtLblMstShiresaki1.TabIndex = 2
    Me.TxtLblMstShiresaki1.TxtPos = False
    '
    'TxKotaiNo1
    '
    Me.TxKotaiNo1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxKotaiNo1.Location = New System.Drawing.Point(217, 102)
    Me.TxKotaiNo1.Name = "TxKotaiNo1"
    Me.TxKotaiNo1.Size = New System.Drawing.Size(147, 28)
    Me.TxKotaiNo1.TabIndex = 3
    Me.TxKotaiNo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'txtKakoubi
    '
    Me.txtKakoubi.Location = New System.Drawing.Point(778, 102)
    Me.txtKakoubi.Name = "txtKakoubi"
    Me.txtKakoubi.Size = New System.Drawing.Size(147, 28)
    Me.txtKakoubi.TabIndex = 4
    '
    'TxtLblMstItem1
    '
    Me.TxtLblMstItem1.BackColor = System.Drawing.SystemColors.Control
    Me.TxtLblMstItem1.CodeTxt = ""
    Me.TxtLblMstItem1.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstItem1.Location = New System.Drawing.Point(217, 147)
    Me.TxtLblMstItem1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtLblMstItem1.Name = "TxtLblMstItem1"
    Me.TxtLblMstItem1.Size = New System.Drawing.Size(371, 36)
    Me.TxtLblMstItem1.TabIndex = 5
    Me.TxtLblMstItem1.TxtPos = False
    '
    'txtCarton
    '
    Me.txtCarton.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.txtCarton.Location = New System.Drawing.Point(778, 147)
    Me.txtCarton.Name = "txtCarton"
    Me.txtCarton.Size = New System.Drawing.Size(100, 28)
    Me.txtCarton.TabIndex = 6
    Me.txtCarton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'txtStockCount
    '
    Me.txtStockCount.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.txtStockCount.Location = New System.Drawing.Point(217, 192)
    Me.txtStockCount.Name = "txtStockCount"
    Me.txtStockCount.Size = New System.Drawing.Size(100, 28)
    Me.txtStockCount.TabIndex = 7
    Me.txtStockCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxtWeitghtKg1
    '
    Me.TxtWeitghtKg1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtWeitghtKg1.Location = New System.Drawing.Point(778, 192)
    Me.TxtWeitghtKg1.Name = "TxtWeitghtKg1"
    Me.TxtWeitghtKg1.Size = New System.Drawing.Size(100, 28)
    Me.TxtWeitghtKg1.TabIndex = 8
    Me.TxtWeitghtKg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'txtCostKg
    '
    Me.txtCostKg.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.txtCostKg.Location = New System.Drawing.Point(217, 237)
    Me.txtCostKg.Name = "txtCostKg"
    Me.txtCostKg.Size = New System.Drawing.Size(100, 28)
    Me.txtCostKg.TabIndex = 9
    Me.txtCostKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(52, 303)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.RowTemplate.Height = 21
    Me.DataGridView1.Size = New System.Drawing.Size(1470, 532)
    Me.DataGridView1.TabIndex = 19
    '
    'lblListStat
    '
    Me.lblListStat.BackColor = System.Drawing.SystemColors.Control
    Me.lblListStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblListStat.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblListStat.ForeColor = System.Drawing.Color.Black
    Me.lblListStat.Location = New System.Drawing.Point(52, 274)
    Me.lblListStat.Name = "lblListStat"
    Me.lblListStat.Size = New System.Drawing.Size(1470, 31)
    Me.lblListStat.TabIndex = 20
    Me.lblListStat.Text = "Label18"
    Me.lblListStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblInformation
    '
    Me.lblInformation.BackColor = System.Drawing.Color.White
    Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Navy
    Me.lblInformation.Location = New System.Drawing.Point(0, 880)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(1535, 29)
    Me.lblInformation.TabIndex = 21
    Me.lblInformation.Text = "Label18"
    Me.lblInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'btnEntry
    '
    Me.btnEntry.Location = New System.Drawing.Point(373, 842)
    Me.btnEntry.Name = "btnEntry"
    Me.btnEntry.Size = New System.Drawing.Size(148, 30)
    Me.btnEntry.TabIndex = 10
    Me.btnEntry.Text = "F1:登録"
    Me.btnEntry.UseVisualStyleBackColor = True
    '
    'btnMoveNext
    '
    Me.btnMoveNext.Location = New System.Drawing.Point(540, 842)
    Me.btnMoveNext.Name = "btnMoveNext"
    Me.btnMoveNext.Size = New System.Drawing.Size(148, 30)
    Me.btnMoveNext.TabIndex = 11
    Me.btnMoveNext.Text = "F2:次表示"
    Me.btnMoveNext.UseVisualStyleBackColor = True
    '
    'btnShowStockList
    '
    Me.btnShowStockList.Location = New System.Drawing.Point(707, 842)
    Me.btnShowStockList.Name = "btnShowStockList"
    Me.btnShowStockList.Size = New System.Drawing.Size(148, 30)
    Me.btnShowStockList.TabIndex = 12
    Me.btnShowStockList.Text = "F3:在庫表示"
    Me.btnShowStockList.UseVisualStyleBackColor = True
    '
    'btnShowUntreated
    '
    Me.btnShowUntreated.Location = New System.Drawing.Point(874, 842)
    Me.btnShowUntreated.Name = "btnShowUntreated"
    Me.btnShowUntreated.Size = New System.Drawing.Size(148, 30)
    Me.btnShowUntreated.TabIndex = 13
    Me.btnShowUntreated.Text = "F5:未処理表示"
    Me.btnShowUntreated.UseVisualStyleBackColor = True
    '
    'btnDelete
    '
    Me.btnDelete.Location = New System.Drawing.Point(1041, 842)
    Me.btnDelete.Name = "btnDelete"
    Me.btnDelete.Size = New System.Drawing.Size(148, 30)
    Me.btnDelete.TabIndex = 14
    Me.btnDelete.Text = "F7:削除"
    Me.btnDelete.UseVisualStyleBackColor = True
    '
    'btnClose
    '
    Me.btnClose.Location = New System.Drawing.Point(1375, 842)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(148, 30)
    Me.btnClose.TabIndex = 15
    Me.btnClose.Text = "F12:閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'TxtLblMstStaff1
    '
    Me.TxtLblMstStaff1.BackColor = System.Drawing.SystemColors.Control
    Me.TxtLblMstStaff1.CodeTxt = ""
    Me.TxtLblMstStaff1.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.TxtLblMstStaff1.Location = New System.Drawing.Point(217, 12)
    Me.TxtLblMstStaff1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
    Me.TxtLblMstStaff1.Name = "TxtLblMstStaff1"
    Me.TxtLblMstStaff1.Size = New System.Drawing.Size(432, 36)
    Me.TxtLblMstStaff1.TabIndex = 0
    Me.TxtLblMstStaff1.TxtPos = False
    '
    'txtTanaorosibi
    '
    Me.txtTanaorosibi.Location = New System.Drawing.Point(1140, 12)
    Me.txtTanaorosibi.Name = "txtTanaorosibi"
    Me.txtTanaorosibi.Size = New System.Drawing.Size(100, 28)
    Me.txtTanaorosibi.TabIndex = 29
    Me.txtTanaorosibi.Visible = False
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Location = New System.Drawing.Point(1068, 15)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(73, 21)
    Me.Label11.TabIndex = 30
    Me.Label11.Text = "棚卸日"
    Me.Label11.Visible = False
    '
    'txtUntreatedFlg
    '
    Me.txtUntreatedFlg.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.txtUntreatedFlg.Location = New System.Drawing.Point(1140, 57)
    Me.txtUntreatedFlg.Name = "txtUntreatedFlg"
    Me.txtUntreatedFlg.Size = New System.Drawing.Size(100, 28)
    Me.txtUntreatedFlg.TabIndex = 31
    Me.txtUntreatedFlg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.txtUntreatedFlg.Visible = False
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Location = New System.Drawing.Point(1027, 60)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(118, 21)
    Me.Label12.TabIndex = 32
    Me.Label12.Text = "未処理フラグ"
    Me.Label12.Visible = False
    '
    'btnPrint
    '
    Me.btnPrint.Location = New System.Drawing.Point(1208, 842)
    Me.btnPrint.Name = "btnPrint"
    Me.btnPrint.Size = New System.Drawing.Size(148, 30)
    Me.btnPrint.TabIndex = 33
    Me.btnPrint.Text = "F9:印刷"
    Me.btnPrint.UseVisualStyleBackColor = True
    '
    'Form_Tanaorosi
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.btnPrint)
    Me.Controls.Add(Me.Label12)
    Me.Controls.Add(Me.txtUntreatedFlg)
    Me.Controls.Add(Me.Label11)
    Me.Controls.Add(Me.txtTanaorosibi)
    Me.Controls.Add(Me.TxtLblMstStaff1)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnDelete)
    Me.Controls.Add(Me.btnShowUntreated)
    Me.Controls.Add(Me.btnShowStockList)
    Me.Controls.Add(Me.btnMoveNext)
    Me.Controls.Add(Me.btnEntry)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.lblListStat)
    Me.Controls.Add(Me.DataGridView1)
    Me.Controls.Add(Me.txtCostKg)
    Me.Controls.Add(Me.TxtWeitghtKg1)
    Me.Controls.Add(Me.txtStockCount)
    Me.Controls.Add(Me.txtCarton)
    Me.Controls.Add(Me.TxtLblMstItem1)
    Me.Controls.Add(Me.txtKakoubi)
    Me.Controls.Add(Me.TxKotaiNo1)
    Me.Controls.Add(Me.TxtLblMstShiresaki1)
    Me.Controls.Add(Me.txtNyukobi)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "Form_Tanaorosi"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents Label4 As Label
  Friend WithEvents Label5 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents Label7 As Label
  Friend WithEvents Label8 As Label
  Friend WithEvents Label9 As Label
  Friend WithEvents Label10 As Label
  Friend WithEvents txtNyukobi As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents TxtLblMstShiresaki1 As T.R.ZCommonCtrl.TxtLblMstShiresaki
  Friend WithEvents TxKotaiNo1 As T.R.ZCommonCtrl.TxKotaiNo
  Friend WithEvents txtKakoubi As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents TxtLblMstItem1 As T.R.ZCommonCtrl.TxtLblMstItem
  Friend WithEvents txtCarton As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents txtStockCount As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents TxtWeitghtKg1 As T.R.ZCommonCtrl.TxtWeitghtKg
  Friend WithEvents txtCostKg As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents lblListStat As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents btnEntry As Button
  Friend WithEvents btnMoveNext As Button
  Friend WithEvents btnShowStockList As Button
  Friend WithEvents btnShowUntreated As Button
  Friend WithEvents btnDelete As Button
  Friend WithEvents btnClose As Button
  Friend WithEvents TxtLblMstStaff1 As T.R.ZCommonCtrl.TxtLblMstStaff
  Friend WithEvents txtTanaorosibi As T.R.ZCommonCtrl.TxtDateBase
  Friend WithEvents Label11 As Label
  Friend WithEvents txtUntreatedFlg As T.R.ZCommonCtrl.TxtNumericBase
  Friend WithEvents Label12 As Label
  Friend WithEvents btnPrint As Button
End Class
