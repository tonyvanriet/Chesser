Imports System.ComponentModel

Imports Chesser.Models


Public Class ExcelDumpViewModel
    Implements INotifyPropertyChanged

    Public Property ExcelDumper As ExcelDumpModel

    Public Sub New()
        ExcelDumper = New ExcelDumpModel
    End Sub


    Private _SelectedMove As Move
    Public Property SelectedMove As Move
        Get
            Return _SelectedMove
        End Get
        Set(value As Move)
            _SelectedMove = value
            OnSelectedMoveChanged()
            OnPropertyChanged("SelectedMove")
        End Set
    End Property


    Private Sub OnSelectedMoveChanged()
        Dim selectedMoveIndex As Integer = ExcelDumper.GetMoveIndex(SelectedMove)
        ExcelDumper.SetCurrentMoveIndex(selectedMoveIndex)
    End Sub


    Public Sub NextMove()
        ExcelDumper.NextMove()
        SelectedMove = ExcelDumper.GetCurrentMove
    End Sub

    Public Sub PreviousMove()
        ExcelDumper.PreviousMove()
        SelectedMove = ExcelDumper.GetCurrentMove
    End Sub

    Public Sub Go(Optional moveTimeMs As Integer? = Nothing)
        ExcelDumper.Go(moveTimeMs)
    End Sub

    Public Sub GoDepth(Optional depth As Integer? = Nothing)
        ExcelDumper.GoDepth(depth)
    End Sub

    Private _Is960 As Boolean = False
    Public Property Is960 As Boolean
        Get
            Return _Is960
        End Get
        Set(value As Boolean)
            If value <> _Is960 Then
                _Is960 = value
                OnPropertyChanged("Is960")
                OnIs960Changed()
            End If
        End Set
    End Property

    Private Sub OnIs960Changed()
        ExcelDumper.Set960(Me.Is960)
    End Sub


    Sub Close()
        ExcelDumper.Close()
    End Sub



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

End Class


