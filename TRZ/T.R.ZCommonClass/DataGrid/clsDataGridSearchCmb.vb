Public Class clsDataGridSearchCmb
  Inherits clsDataGridSearchControl

#Region "メンバ"

#Region "プライベート"
  Private WithEvents mCmb As ComboBox
#End Region

#End Region

#Region "コンストラクタ"

  Public Sub New()
    lcCallBackSetTargetControl = Sub() SetTargetCtrl()
  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  Public Sub SetTargetCtrl()
    mCmb = DirectCast(mTargetControl, ComboBox)
  End Sub

#End Region

#Region "プライベート"
  Private Sub TextChanged(sender As Object, e As EventArgs) Handles mCmb.Validated, mCmb.SelectedIndexChanged

    If mCallBack IsNot Nothing Then
      mCallBack()
    End If
  End Sub
#End Region

#End Region

End Class
