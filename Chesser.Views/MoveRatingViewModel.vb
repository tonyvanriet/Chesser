Imports System.ComponentModel

Imports Chesser.Models
Imports System.Collections.ObjectModel


Public Class MoveRatingViewModel

    Private WithEvents _EngineController As EngineController

    Private _UiDispatcher As System.Windows.Threading.Dispatcher


    Public Sub New(uiDispatcher As System.Windows.Threading.Dispatcher)
        _EngineController = New EngineController
        _UiDispatcher = uiDispatcher
    End Sub

    Public Sub Go(Optional moveTimeMs As Integer? = 30000)


        Dim numHalfMoves = _EngineController.Moves.Count

        _EngineController.SetCurrentMoveIndex(6)
        _BestVariations.Clear()
        _LatestVariationTimeMs = 0

        _EngineController.Go(moveTimeMs)

    End Sub


    Private Sub _EngineController_VariationEvaluationReturned(ve As VariationEvaluation) Handles _EngineController.VariationEvaluationReturned
        _UiDispatcher.BeginInvoke(Sub() UpdateBestVariations(ve))
    End Sub


    Private Sub UpdateBestVariations(ve As VariationEvaluation)

        If ve.TimeMs > _LatestVariationTimeMs Then
            _LatestVariationTimeMs = ve.TimeMs
            _BestVariations.Clear()
        End If

        Debug.Assert(ve.TimeMs = _LatestVariationTimeMs)

        _BestVariations.Add(ve)

    End Sub


    Private _LatestVariationTimeMs As Integer = 0


    Private _BestVariations As New ObservableCollection(Of VariationEvaluation)
    Public ReadOnly Property BestVariations As ObservableCollection(Of VariationEvaluation)
        Get
            Return _BestVariations
        End Get
    End Property



    Private Sub _EngineController_GoFinished() Handles _EngineController.GoFinished

    End Sub



    Public Sub Close()
        _EngineController.Close()
    End Sub

End Class


