Public Class ExcelDumpView

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ExcelDumpViewModel = New ExcelDumpViewModel
        Me.DataContext = ExcelDumpViewModel

    End Sub


    Public Property ExcelDumpViewModel As ExcelDumpViewModel


    Private Sub GoTimeButton_Click(sender As Object, e As RoutedEventArgs) Handles GoTimeButton.Click
        If String.IsNullOrWhiteSpace(GoMoveTimeTextBox.Text) Then
            ExcelDumpViewModel.Go()
        Else
            ExcelDumpViewModel.Go(GoMoveTimeTextBox.Text)
        End If
    End Sub


    Private Sub GoDepthButton_Click(sender As Object, e As RoutedEventArgs) Handles GoDepthButton.Click
        If String.IsNullOrWhiteSpace(GoDepthTextBox.Text) Then
            ExcelDumpViewModel.GoDepth()
        Else
            ExcelDumpViewModel.GoDepth(GoDepthTextBox.Text)
            GoDepthTextBox.Text += 1
        End If
    End Sub


    Private Sub PreviousMoveButton_Click(sender As Object, e As RoutedEventArgs) Handles PreviosMoveButton.Click
        ExcelDumpViewModel.PreviousMove()
    End Sub

    Private Sub NextMoveButton_Click(sender As Object, e As RoutedEventArgs) Handles NextMoveButton.Click
        ExcelDumpViewModel.NextMove()
    End Sub

    Private Sub StopButton_Click(sender As Object, e As RoutedEventArgs) Handles StopButton.Click
        ExcelDumpViewModel.ExcelDumper.Stop()
    End Sub


    Private Sub ExcelDumpView_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded

    End Sub

    Sub Close()
        ExcelDumpViewModel.Close()
    End Sub


    '  Private Sub CafeChessBoardFormsHost_Loaded(sender As Object, e As RoutedEventArgs) Handles CafeChessBoardFormsHost.Loaded
    '      Dim formsHost As Forms.Integration.WindowsFormsHost = sender
    '      Dim cafeChessBoardControl As Cafechess.Chess.Controls.ChessBoard = formsHost.Child

    '      Dim chessBoardGuiValidationHandler = i

    '      cafeChessBoardControl.ChessBoardGui.Validation = New Cafechess.Chess.Validation.StandardValidation
    '      cafeChessBoardControl.ChessBoardGui.Validation.AddEvents(chessBoardGuiValidationHandler)
    '///       NewChessboard.ChessBoardGui.Validation.AddEvents(this);      
    '///       IChessboardGui igui = NewChessboard.ChessBoardGui;          
    '///       igui.ActiveLayer = cafechess.control.chessboard.Layers.DrawTop;      
    '
    'End Sub

End Class
