Public Class CmbMstSyukkaBi
  Inherits CmbMstBase

  Private Const CODE_FORMAT As String = "yyyy/MM/dd"

#Region "コンストラクタ"

  Public Sub New()

    MyBase.New(CODE_FORMAT, 1)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT Format(SYUKKABI,'" & CODE_FORMAT & "') AS SHUKAYMD2  "
    sql &= " FROM CUTJ "
    sql &= " WHERE (NSZFLG = 2 AND DKUBUN = 0) "
    sql &= " GROUP BY Format(SYUKKABI,'" & CODE_FORMAT & "') "
    sql &= " ORDER BY Format(SYUKKABI,'" & CODE_FORMAT & "') DESC"


    Return sql
  End Function

#End Region

#End Region

End Class
