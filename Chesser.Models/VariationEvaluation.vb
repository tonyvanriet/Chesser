Public Class VariationEvaluation

    Public Property Depth As Integer
    Public Property SelectiveDepth As Integer
    Public Property Evaluation As Double
    Public Property TimeMs As Integer
    Public Property Nodes As Long
    Public Property NodesPerSecond As Integer
    Public Property MoveInLongAlgebraic As String
    Public Property ContinuationInLongAlgebraic As String

    Public Overrides Function ToString() As String
        Return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", Depth, SelectiveDepth, Evaluation, TimeMs, Nodes, NodesPerSecond, MoveInLongAlgebraic, ContinuationInLongAlgebraic)
    End Function

End Class
