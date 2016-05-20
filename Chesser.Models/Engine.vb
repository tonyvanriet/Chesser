Imports Cafechess.Chess.Engines
Imports Cafechess.Threading

Public Class Engine
    Implements IThreadedReaderEvents
    Implements IDisposable


    Private _FileName As String

    Private _EngineProcess As Process
    Private _EngineProcessStartInfo As ProcessStartInfo
    Private _ConsoleReader As Cafechess.Chess.Engines.ConsoleReader
    Private WithEvents _ThreadedReader As ThreadedReader

    Public Sub New(fileName As String)
        _FileName = fileName
    End Sub

    Public Sub Start()

        _EngineProcess = New Process
        _EngineProcessStartInfo = New ProcessStartInfo
        _ConsoleReader = New ConsoleReader(_EngineProcess)
        _ThreadedReader = New ThreadedReader(_ConsoleReader)
        _ThreadedReader.addEvents(Me)

        _EngineProcessStartInfo.FileName = _FileName
        _EngineProcessStartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(_FileName)
        _EngineProcessStartInfo.RedirectStandardInput = True
        _EngineProcessStartInfo.RedirectStandardOutput = True
        _EngineProcessStartInfo.UseShellExecute = False
        _EngineProcessStartInfo.CreateNoWindow = True

        _EngineProcess.StartInfo = _EngineProcessStartInfo
        _EngineProcess.Start()
        _ThreadedReader.start()

    End Sub

    Public Sub Shutdown()
        Try
            If IsRunning Then

                CommandInputLine("quit")

                ' Apparently I need to give the engine some time to quit before continuing. Otherwise, I get an exception from the ThreadedReader producer logic.
                System.Threading.Thread.Sleep(100)
                _ConsoleReader.Close()

                If Not _EngineProcess.HasExited Then
                    _EngineProcess.Kill()
                End If
                _EngineProcess.Close()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            _EngineProcess = Nothing
            _EngineProcessStartInfo = Nothing
            _ConsoleReader = Nothing
            _ThreadedReader = Nothing
        End Try
    End Sub


    Public ReadOnly Property IsRunning As Boolean
        Get
            Return _EngineProcess IsNot Nothing AndAlso Not _EngineProcess.HasExited
        End Get
    End Property


    Public Sub CommandInputLine(input As String, Optional flush As Boolean = True)
        _EngineProcess.StandardInput.WriteLine(input)
        If flush Then _EngineProcess.StandardInput.Flush()
    End Sub

    Private Function _ThreadedReader_EventProcessRecord(oColumns As IList, oValues As IList) As Boolean Handles _ThreadedReader.EventProcessRecord
        Return True
    End Function

    Private Sub _ThreadedReader_EventBeginProducing() Handles _ThreadedReader.EventBeginProducing

    End Sub

    Private Sub _ThreadedReader_EventEndProducing(count As Integer) Handles _ThreadedReader.EventEndProducing

    End Sub

    Private Sub _ThreadedReader_EventBeginConsuming() Handles _ThreadedReader.EventBeginConsuming

    End Sub

    Private Sub _ThreadedReader_EventEndConsuming(count As Integer) Handles _ThreadedReader.EventEndConsuming

    End Sub


#Region " IThreadedReaderEvents Implementation "

    Public Function processRow(oColumnNames As IList, oValues As IList) As Boolean Implements IThreadedReaderEvents.processRow
        Debug.Assert(oValues.Count = 1, "seeing " & oValues.Count & " oValues")
        Dim data As String = oValues(0)
        'Debug.Write(data)
        RaiseOutputLine(data)
        Return True
    End Function

    Public Sub beginConsumer() Implements IThreadedReaderEvents.beginConsumer

    End Sub

    Public Sub beginProducer() Implements IThreadedReaderEvents.beginProducer

    End Sub

    Public Sub endConsumer(count As Integer) Implements IThreadedReaderEvents.endConsumer

    End Sub

    Public Sub endProducer(count As Integer) Implements IThreadedReaderEvents.endProducer

    End Sub

    Public Sub finished() Implements IThreadedReaderEvents.finished

    End Sub


#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region


    Private Sub RaiseOutputLine(line As String)
        RaiseEvent OutputLine(line)
    End Sub

    Public Event OutputLine(line As String)

End Class

