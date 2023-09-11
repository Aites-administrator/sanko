Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management
Imports T.R.ZCommonClass.clsCommonFnc

Public Class ClassTopMenu

#Region "定数定義"

  Public Const PRG_TITLE As String = "入出庫管理システム（トレ蔵.DX）"

  Public Const PRG_FILENAME As String = "TopMenu.ini"

  Public Const PROCESS_MAX As Integer = 10

#End Region

#Region "構造体"
  ''' <summary>
  ''' レポートワークテーブル構造体
  ''' </summary>
  Public Structure structMenu

    Public Property wSyoriNo As Integer       ' 処理番号
    Public Property wTitle As String          ' タイトル名
    Public Property wColorR As Integer        ' 色RGBのR
    Public Property wColorG As Integer        ' 色RGBのG
    Public Property wColorB As Integer        ' 色RGBのB
    Public Property wExePath As String　      ' EXEファイル
    Public Property wArgument As String　     ' コマンドライン引数
    Public Property wMsg As String　          ' メッセージ

  End Structure

#End Region

#Region "メソッド"

#Region "パブリック"

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

  ''' <summary>
  ''' INIファイルから読み込む
  ''' </summary>
  ''' <param name="no"></param>
  ''' <param name="sMenu"></param>
  Public Shared Sub iniFileRead(no As Integer,
                                ByRef sMenu() As structMenu)

    '自分自身の存在するフォルダ  
    Dim strPath As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
    strPath = System.IO.Path.Combine(strPath, PRG_FILENAME)

    Dim strKey As String = String.Empty
    Dim color As String = String.Empty
    Dim titleName As String = String.Empty
    Dim exeName As String = String.Empty
    Dim argument As String = String.Empty
    Dim msg As String = String.Empty

    ' 1ページ目
    For i = 0 To PROCESS_MAX - 1

      If (no = 1) Then
        strKey = "P01F" & (i + 1).ToString("00")
      Else
        strKey = "P02F" & (i + 1).ToString("00")
      End If

      ' タイトル名の読込
      titleName = GetIniString(strKey, "TITLE", strPath)
      ' EXEパスの読込
      exeName = GetIniString(strKey, "EXE", strPath)
      ' コマンドライン引数の読込
      argument = GetIniString(strKey, "ARG", strPath)
      ' 色の読込
      color = GetIniString(strKey, "COLOR", strPath)
      ' メッセージの取得
      msg = GetIniString(strKey, "MSG", strPath)


      If (String.IsNullOrEmpty(titleName)) Then
        ' クリア
        sMenu(i).wSyoriNo = 0
        sMenu(i).wTitle = ""
        sMenu(i).wColorR = 0
        sMenu(i).wColorG = 0
        sMenu(i).wColorG = 0
        sMenu(i).wExePath = ""
        sMenu(i).wArgument = ""
      Else
        ' iniファイルから読み込んだ値を設定
        sMenu(i).wSyoriNo = i + 1
        sMenu(i).wTitle = titleName
        If (color.Length >= 6) Then
          sMenu(i).wColorR = Convert.ToInt32((color.Substring(0, 2)), 16)
          sMenu(i).wColorG = Convert.ToInt32((color.Substring(2, 2)), 16)
          sMenu(i).wColorB = Convert.ToInt32((color.Substring(4, 2)), 16)
        End If
        sMenu(i).wExePath = exeName
        sMenu(i).wArgument = argument
        sMenu(i).wMsg = msg
      End If

    Next i

  End Sub

  ''' <summary>
  ''' INIファイルから文字列を取得する
  ''' </summary>
  ''' <param name="lpAppName"></param>
  ''' <param name="lpKeyName"></param>
  ''' <param name="strPath"></param>
  ''' <returns></returns>
  Public Shared Function GetIniString(ByVal lpAppName As String, ByVal lpKeyName As String, strPath As String) As String

    Dim sb As StringBuilder = New StringBuilder(256)

    Try
      Call GetPrivateProfileString(lpAppName, lpKeyName, "", sb, Convert.ToUInt32(sb.Capacity), strPath)

      Return sb.ToString
    Catch ex As Exception
      Return sb.ToString
    End Try
  End Function

  ''' <summary>
  ''' 任意のプロセスを起動する
  ''' </summary>
  ''' <param name="prmProc">起動したプロセスを保持</param>
  ''' <param name="prmExeFilePath">対象ファイルパス</param>
  ''' <param name="prmArg">コマンドライン引数</param>
  Public Shared Sub ExecProcess(ByRef prmProc As Process _
                                      , prmExeFilePath As String _
                                      , prmArg As String)

    If (prmProc) Is Nothing _
      OrElse prmProc.HasExited Then
      ' 初回起動時、起動プロセス終了時
      prmProc = ComGetProcessByFilePath(prmExeFilePath, prmArg)
    End If

    Dim psi As New ProcessStartInfo(prmExeFilePath)
    psi.Arguments = prmArg
    Process.Start(psi)

  End Sub

  ''' <summary>
  ''' 任意のプロセスを終了する
  ''' </summary>
  ''' <param name="prmTargetProcessies"></param>
  Public Shared Sub KillProcess(prmTargetProcessies As Process())

    ' 開いているプロセスの終了
    For i = 0 To prmTargetProcessies.Length - 1
      Try
        If Not (prmTargetProcessies(i)) Is Nothing _
          AndAlso prmTargetProcessies(i).HasExited = False Then
          prmTargetProcessies(i).Kill()
        End If
      Catch ex As Exception
        Call ComWriteErrLog(ex)
      End Try
    Next

  End Sub

#End Region

#End Region

End Class
