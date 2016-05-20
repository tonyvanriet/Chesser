
Public Class MoveList
    Inherits Dictionary(Of Integer, Move)

    Private _CurrentFenMoveIndex As Integer = 0

    Public Sub AddNextFenMove(fenMove As String)
        If Me.ContainsKey(_CurrentFenMoveIndex) = False Then Me.Add(_CurrentFenMoveIndex, New Move())
        Me(_CurrentFenMoveIndex).FenNotation = fenMove
        _CurrentFenMoveIndex += 1
    End Sub


    'Public Sub SetFenMoves(fenMoves As IEnumerable(Of String))

    '    Dim moveIndex As Integer = 0

    '    For Each m In Me
    '        m.FenNotation = fenMoves(moveIndex)
    '        moveIndex += 1
    '    Next

    '    For i = moveIndex To fenMoves.Count - 1
    '        Me.Add(New Move)
    '        Me(i).FenNotation = fenMoves(moveIndex)
    '    Next


    '    For moveIndex As Integer = 0 To fenMoves.Count - 1
    '        If moveIndex >= Me.Count Then Me.Add()
    '        If Me.ContainsKey(moveIndex) = False Then Me.Add(moveIndex, New Move)
    '        Me(moveIndex).FenNotation = fenMoves(moveIndex)
    '    Next

    'End Sub


    Private _CurrentPgnMoveIndex As Integer = 0


    Sub AddNextPgnMove(pgnMove As String)
        If Me.ContainsKey(_CurrentPgnMoveIndex) = False Then Me.Add(_CurrentPgnMoveIndex, New Move())
        Me(_CurrentPgnMoveIndex).PgnNotation = pgnMove
        _CurrentPgnMoveIndex += 1
    End Sub


    Public Function GetMoveIndex(move As Move) As Integer
        For i = 0 To Me.Values.Count - 1
            If Me(i) Is move Then Return i
        Next
        Throw New Exception("GetMoveIndex couldn't find move")
    End Function


End Class
