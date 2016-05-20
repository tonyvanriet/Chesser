Imports Cafechess.Chess.Parsers

Public Class PgnParsingHandler
    Implements IGameParserEvents

    Private _Moves As MoveList

    Public Sub New(moves As MoveList)
        _Moves = moves
    End Sub


    Public Sub NewGame(iParser As IGameParser) Implements IGameParserEvents.NewGame

    End Sub

    Public Sub ExitHeader(iParser As IGameParser) Implements IGameParserEvents.ExitHeader

    End Sub

    Public Sub EnterVariation(iParser As IGameParser) Implements IGameParserEvents.EnterVariation

    End Sub

    Public Sub ExitVariation(iParser As IGameParser) Implements IGameParserEvents.ExitVariation

    End Sub

    Public Sub Starting(iParser As IGameParser) Implements IGameParserEvents.Starting

    End Sub

    Public Sub Finished(iParser As IGameParser) Implements IGameParserEvents.Finished

    End Sub

    Public Sub TagParsed(iParser As IGameParser) Implements IGameParserEvents.TagParsed

    End Sub

    Public Sub NagParsed(iParser As IGameParser) Implements IGameParserEvents.NagParsed

    End Sub

    Public Sub MoveParsed(iParser As IGameParser) Implements IGameParserEvents.MoveParsed
        If IsNumeric(iParser.Value) Then

        Else
            _Moves.AddNextPgnMove(iParser.Value)
        End If
    End Sub

    Public Sub CommentParsed(iParser As IGameParser) Implements IGameParserEvents.CommentParsed

    End Sub

    Public Sub EndMarker(iParser As IGameParser) Implements IGameParserEvents.EndMarker

    End Sub


End Class
