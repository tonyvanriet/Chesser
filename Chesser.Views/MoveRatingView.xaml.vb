Public Class MoveRatingView

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MoveRatingViewModel = New MoveRatingViewModel(Dispatcher)
        Me.DataContext = MoveRatingViewModel

    End Sub


    Public Property MoveRatingViewModel As MoveRatingViewModel


    Private Sub GoButton_Click(sender As Object, e As RoutedEventArgs) Handles GoButton.Click
        MoveRatingViewModel.Go()        
    End Sub

    Private Sub MoveRatingView_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded

    End Sub

    Sub Close()
        MoveRatingViewModel.Close()
    End Sub

End Class
