Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management

Public Class ClassMentMenu

#Region "定数定義"

  Public Const PRG_TITLE As String = "マスターメンテナンス"

  Public Const PRG_FILENAME As String = "MentMenu.ini"

#End Region

#Region "構造体"
  ''' <summary>
  ''' レポートワークテーブル構造体
  ''' </summary>
  Public Structure structMenu

    Public Property wSyoriNo As Integer       ' 処理番号
    Public Property wTitle As String          ' タイトル名
    Public Property wColorR As Integer        ' 色RGBのR
    Public Property wColorG As Integer        ' 色RGBのR
    Public Property wColorB As Integer        ' 色RGBのR
    Public Property wExePath As String　      ' EXEファイル
    Public Property wArgument As String　     ' コマンドライン引数
    Public Property wMsg As String　          ' メッセージ

  End Structure

#End Region

  ' ------------------------------------------------------
  ' ini ファイル読み込み
  ' ------------------------------------------------------
  '指定のIniファイルの指定キーの値を取得(文字列)
  ' AUTO版 GetPrivateProfileString
  Public Declare Auto Function GetPrivateProfileString Lib "kernel32" _
        Alias "GetPrivateProfileString" (
        <MarshalAs(UnmanagedType.LPTStr)> ByVal lpApplicationName As String,
        <MarshalAs(UnmanagedType.LPTStr)> ByVal lpKeyName As String,
        <MarshalAs(UnmanagedType.LPTStr)> ByVal lpDefault As String,
        <MarshalAs(UnmanagedType.LPTStr)> ByVal lpReturnedString As StringBuilder,
        ByVal nSize As UInt32,
        <MarshalAs(UnmanagedType.LPTStr)> ByVal lpFileName As String) As UInt32


    Public Shared Function GetIniString(ByVal lpAppName As String, ByVal lpKeyName As String, strPath As String) As String

    Dim sb As StringBuilder = New StringBuilder(256)

    Try
      Call GetPrivateProfileString(lpAppName, lpKeyName, "", sb, Convert.ToUInt32(sb.Capacity), strPath)

      Return sb.ToString
    Catch ex As Exception
      Return sb.ToString
    End Try
  End Function


End Class
