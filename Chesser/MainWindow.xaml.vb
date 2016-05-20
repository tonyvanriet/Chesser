Class MainWindow

    Private Sub MainWindow_Closed(sender As Object, e As EventArgs) Handles Me.Closed
    End Sub

    Private Sub MainWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        ChesserTabShell.Close()
    End Sub

    Private Sub MainWindow_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded

    End Sub
End Class
