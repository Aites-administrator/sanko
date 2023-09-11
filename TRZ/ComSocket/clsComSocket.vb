Imports System.Net
Imports System.Net.Sockets
Imports T.R.ZCommonClass.clsCommonFnc

Public Class clsComSocket
  Implements IDisposable
  '----------------------------------
  '     ソケット通信管理クラス
  '----------------------------------

#Region "メンバ"

#Region "プライベート"

  ''' <summary>
  ''' 受信待ちスレッド
  ''' </summary>
  Private _threadServer As Threading.Thread

  ''' <summary>
  ''' クライアント通信管理
  ''' </summary>
  Private _listener As TcpListener

  ''' <summary>
  ''' Socketリスト
  ''' </summary>
  Private _ClientHandlers As New List(Of clsClientHandler)

  Private _StopFlg As Boolean
#End Region

#Region "パブリック"

  ''' <summary>
  ''' 読込完了時イベント
  ''' </summary>
  Public Delegate Sub CallBackDataReceive(sender As clsClientHandler, tmpBuffer() As Byte)
  Public lcDataReceive As CallBackDataReceive

#End Region

#End Region

#Region "コンストラクタ"
  Public Sub New()
    lcDataReceive = Nothing
    _StopFlg = False
  End Sub
#End Region

#Region "メソッド"

#Region "パブリック"
  Public Sub StartSocket()
    _threadServer = New Threading.Thread(New Threading.ThreadStart(AddressOf StartServerListen))
    _threadServer.Start()
  End Sub
#End Region

#Region "プライベート"

  ''' <summary>
  ''' 通信処理開始
  ''' </summary>
  ''' <remarks>別スレッドで処理</remarks>
  Private Sub StartServerListen()
    Try
      'PORT番号設定
      '      _listener = New TcpListener(IPAddress.Any, 20002)
      '_listener = New TcpListener(IPAddress.Any, 20224)
      _listener = New TcpListener(IPAddress.Any, 20004)
      _listener.Start()

      While (1)
        If _StopFlg Then
          Exit While
        End If

        Dim tmpSocketForClient As Socket = _listener.AcceptSocket()
        Dim tmpClientHandler = New MsAcroConnection(tmpSocketForClient, AddressOf DataReceive)
        'Dim tmpClientHandler = New clsClientHandler(tmpSocketForClient, AddressOf DataReceive)

        Call AddClientHandlerList(tmpClientHandler)

        tmpClientHandler.StarRead()

      End While
    Catch ex As System.Threading.ThreadAbortException
    Catch ex As System.Net.Sockets.SocketException
    Catch ex As Exception

    End Try
  End Sub

  ''' <summary>
  ''' Socketオブジェクト保持
  ''' </summary>
  ''' <param name="prmClientHandler">保持対象のSocketオブジェクト</param>
  Private Sub AddClientHandlerList(prmClientHandler As clsClientHandler)
    _ClientHandlers.Add(prmClientHandler)
  End Sub

  ''' <summary>
  ''' Socketオブジェクト解放
  ''' </summary>
  ''' <param name="prmClientHandler">解放対象のSocketオブジェクト</param>
  Private Sub RemoveClientHandlerList(prmClientHandler As clsClientHandler)
    Try
      For idx As Integer = 0 To _ClientHandlers.Count - 1
        If _ClientHandlers(idx).Equals(prmClientHandler) Then
          _ClientHandlers.RemoveAt(idx)
        End If
      Next
    Catch ex As Exception
      Call ComWriteErrLog(ex)
    End Try
  End Sub

#End Region

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' データ受信完了時処理
  ''' </summary>
  ''' <param name="tmpBuffer"></param>
  Private Sub DataReceive(sender As clsClientHandler, tmpBuffer() As Byte)
    If lcDataReceive IsNot Nothing Then
      Call lcDataReceive(sender, tmpBuffer)
      If tmpBuffer Is Nothing Then
        Call RemoveClientHandlerList(sender)
      End If
    End If
  End Sub

#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
      End If


      If _ClientHandlers IsNot Nothing _
        AndAlso _ClientHandlers.Count > 0 Then

        For Each tmpCh As clsClientHandler In _ClientHandlers
          tmpCh.Dispose()
        Next
        _ClientHandlers = Nothing

      End If

      If lcDataReceive IsNot Nothing Then
        lcDataReceive = Nothing
      End If

      If _listener IsNot Nothing Then
        _listener.Stop()
        Threading.Thread.Sleep(20)
        _listener = Nothing
      End If

      If _threadServer IsNot Nothing Then
        _StopFlg = True
        _threadServer.Abort()
        _threadServer.Join()
      End If

    End If
    disposedValue = True
  End Sub

  ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
  Protected Overrides Sub Finalize()
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(False)
    MyBase.Finalize()
  End Sub

  ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
  Public Sub Dispose() Implements IDisposable.Dispose
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(True)
    ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
    GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class
