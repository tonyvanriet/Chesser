Imports System.IO

Public Class EngineController

    Private WithEvents _Engine As Engine

    Private _Moves As New MoveList
    Public ReadOnly Property Moves As MoveList
        Get
            Return _Moves
        End Get
    End Property

    Private _CurrentDepth As Integer? = Nothing


    Public Sub SetCurrentMoveIndex(moveIndex As Integer)
        _CurrentMoveIndex = moveIndex
        _Engine.CommandInputLine("position fen " & _Moves(_CurrentMoveIndex).FenNotation)
    End Sub


    Private _CurrentMoveIndex As Integer

    Public Function GetCurrentMove() As Move
        Return Moves(_CurrentMoveIndex)
    End Function


    Public Function GetMoveIndex(move As Move)
        Return Moves.GetMoveIndex(move)
    End Function

    Private Const FileNameBase As String = "Higgsfield_vs_tvchumack_2013_06_23"

    Private Const GamesFullPath As String = "C:\Users\Tony van Riet\Dropbox\Development\Chess\Chesser\Games\"

    Public Sub New()


        LoadPgn(String.Format("{0}{1}.pgn", GamesFullPath, FileNameBase))

        LoadFen(String.Format("{0}{1}.fen", GamesFullPath, FileNameBase))

        _Engine = New HoudiniEngine("C:\Program Files\Houdini 3 Chess\Houdini_3_x64.exe")
        _Engine.Start()
        _Engine.CommandInputLine("hash=1024")
        _Engine.CommandInputLine("threads=4")
        _Engine.CommandInputLine("multipv=3")
        _Engine.CommandInputLine("ucinewgame")

        _CurrentMoveIndex = 0

        _Engine.CommandInputLine("position fen " & _Moves(_CurrentMoveIndex).FenNotation)

    End Sub


    Private _PgnParsingHandler As PgnParsingHandler

    Private _PositionFenNotation As New Cafechess.Chess.Parsers.FenNotation

    Private _FenParser As FenParser


    Private Sub LoadPgn(pgnFileName As String)

        _PgnParsingHandler = New PgnParsingHandler(_Moves)

        Try

            Dim pgnParser As New Cafechess.Chess.Parsers.PGNparser
            pgnParser.Filename = pgnFileName
            pgnParser.AddEvents(_PgnParsingHandler)
            pgnParser.Parse()

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub


    Private Sub LoadFen(fenFileName As String)
        Try
            Dim sr As StreamReader = New StreamReader(fenFileName)
            Dim line As String

            Do

                line = sr.ReadLine()
                _Moves.AddNextFenMove(line)

            Loop Until line Is Nothing
            sr.Close()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Public Sub Go(Optional moveTimeMs As Integer? = Nothing)
        _CurrentDepth = Nothing

        If moveTimeMs Is Nothing Then
            _Engine.CommandInputLine("go infinite")
        Else
            _Engine.CommandInputLine("go movetime " & moveTimeMs)
        End If
    End Sub

    Public Sub GoDepth(Optional depth As Integer? = Nothing)
        _CurrentDepth = Nothing

        If depth Is Nothing Then
            _Engine.CommandInputLine("go infinite")
        Else
            '_Engine.CommandInputLine("setoption name clear hash")
            _CurrentDepth = depth
            _Engine.CommandInputLine("go depth " & depth)
        End If
    End Sub


    Sub PreviousMove()
        If _CurrentMoveIndex > 0 Then
            SetCurrentMoveIndex(_CurrentMoveIndex - 1)
        End If
    End Sub

    Sub NextMove()
        If _CurrentMoveIndex < _Moves.Count - 1 Then
            SetCurrentMoveIndex(_CurrentMoveIndex + 1)
        End If
    End Sub


    Sub Set960(is960 As Boolean)
        _Engine.CommandInputLine("setoption name UCI_Chess960 value " & is960.ToString)
    End Sub


    Public Sub [Stop]()
        _CurrentDepth = Nothing
        _Engine.CommandInputLine("stop")
    End Sub

    Public Sub Close()
        _Engine.Shutdown()
        _Engine = Nothing
    End Sub


    Private Sub _Engine_OutputLine(line As String) Handles _Engine.OutputLine
        Dim words As New List(Of String)(line.Split(" "))

        If words.Count >= 2 AndAlso words(0) = "info" AndAlso words(1) = "multipv" Then
            Dim variationEvaluation As New VariationEvaluation
            With variationEvaluation
                .Depth = words(4)
                .SelectiveDepth = words(6)

                Select Case words(8)
                    Case "cp"
                        .Evaluation = words(9) / 100
                    Case "mate"
                        .Evaluation = 40 * Math.Sign(CType(words(9), Integer))
                    Case Else
                        Throw New Exception
                End Select

                .TimeMs = words(11)
                .Nodes = words(13)
                .NodesPerSecond = words(15)
                .MoveInLongAlgebraic = words(21)

                Dim sb As New System.Text.StringBuilder
                For i = 22 To words.Count - 1
                    sb.Append(words(i) & " ")
                Next

                .ContinuationInLongAlgebraic = sb.ToString

            End With

            Debug.WriteLine(variationEvaluation.ToString)

            RaiseEvent VariationEvaluationReturned(variationEvaluation)


        ElseIf words.Count > 0 And words(0) = "bestmove" Then
            RaiseEvent GoFinished()


        Else
            Debug.Write(line)
        End If


    End Sub


    Public Event VariationEvaluationReturned(ve As VariationEvaluation)

    Public Event GoFinished()

End Class
