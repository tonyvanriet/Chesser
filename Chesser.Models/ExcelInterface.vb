Imports Microsoft.Office.Interop


Public Class ExcelInterface

    Private _ExcelApplication As Excel.Application
    Private _Workbook As Excel.Workbook

    Private _DataSheet As Excel.Worksheet
    Private _PivotTableSheet As Excel.Worksheet
    Private _PivotChartSheet As Excel.Worksheet

    Private _DataSheetWriteRowIndex As Integer = 1

    Public Sub Initialize()
        If _ExcelApplication Is Nothing Then
            _ExcelApplication = CreateObject("Excel.Application")
        End If


        _ExcelApplication.Visible = True

        '_Workbook = _ExcelApplication.ActiveWorkbook
        'If _Workbook Is Nothing Then
        '    _Workbook = _ExcelApplication.Workbooks.Add
        'End If


        _Workbook = OpenAnalysisWorkbookFromTemplate()

        If _Workbook IsNot Nothing Then
            _DataSheet = _Workbook.ActiveSheet

        End If


    End Sub


    Private Function OpenAnalysisWorkbookFromTemplate() As Excel.Workbook
        Dim path = "C:\Users\Tony van Riet\Dropbox\Development\Chess\Chesser\Engines\"
        Dim fileName = "Analysis Template.xlsx"

        Dim workbook = _ExcelApplication.Workbooks.Open(path & fileName)

        path = "C:\Users\Tony van Riet\Dropbox\Development\Chess\Chesser\Engines\Analysis Results\"
        fileName = "Analysis " & Now.ToString("yyyyMMdd-HHmmss")
        workbook.SaveAs(path & fileName)

        Return workbook
    End Function


    Public Sub WriteVariationEvaluationHeaders()

        ' Add table headers going cell by cell.
        _DataSheet.Cells(1, 1).Value = "Depth"
        _DataSheet.Cells(1, 2).Value = "Selective Depth"
        _DataSheet.Cells(1, 3).Value = "Evaluation"
        _DataSheet.Cells(1, 4).Value = "Time (ms)"
        _DataSheet.Cells(1, 5).Value = "Nodes"
        _DataSheet.Cells(1, 6).Value = "Nodes / s"
        _DataSheet.Cells(1, 7).Value = "Move"

        _DataSheetWriteRowIndex = 2
    End Sub

    Sub WriteVariationEvaluation(variationEvaluation As VariationEvaluation)
        WriteToDataSheet(variationEvaluation)
        ' write to some other sheet
    End Sub


    Private Sub WriteToDataSheet(variationEvaluation As VariationEvaluation)
        _DataSheet.Cells(_DataSheetWriteRowIndex, 1).Value = variationEvaluation.Depth
        _DataSheet.Cells(_DataSheetWriteRowIndex, 2).Value = variationEvaluation.SelectiveDepth
        _DataSheet.Cells(_DataSheetWriteRowIndex, 3).Value = variationEvaluation.Evaluation
        _DataSheet.Cells(_DataSheetWriteRowIndex, 4).Value = variationEvaluation.TimeMs
        _DataSheet.Cells(_DataSheetWriteRowIndex, 5).Value = variationEvaluation.Nodes
        _DataSheet.Cells(_DataSheetWriteRowIndex, 6).Value = variationEvaluation.NodesPerSecond
        _DataSheet.Cells(_DataSheetWriteRowIndex, 7).Value = variationEvaluation.MoveInLongAlgebraic
        _DataSheetWriteRowIndex += 1

        'For Each chart In _Workbook.Charts
        '    _PivotChart = chart
        'Next

        'If _PivotChart IsNot Nothing Then
        '    _PivotChart.Refresh()
        'End If

        _Workbook.RefreshAll()
    End Sub


    Private _PivotChart As Excel.Chart

    Sub SetupPivotChart()
        _PivotChart = _DataSheet.Parent.Charts.Add



        'oChart = oWS.Parent.Charts.Add
        'With oChart
        '    .ChartWizard(oResizeRange, Excel.XlChartType.xl3DColumn, , Excel.XlRowCol.xlColumns)
        '    oSeries = .SeriesCollection(1)
        '    oSeries.XValues = oWS.Range("A2", "A6")
        '    For iRet = 1 To iNumQtrs
        '        .SeriesCollection(iRet).Name = "=""Q" & Str(iRet) & """"
        '    Next iRet
        '    .Location(Excel.XlChartLocation.xlLocationAsObject, oWS.Name)
        'End With

    End Sub

    '    Public Sub DoTheDump()


    '        ' Format A1:D1 as bold, vertical alignment = center.
    '        With _DataSheet.Range("A1", "D1")
    '            .Font.Bold = True
    '            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
    '        End With

    '        ' Create an array to set multiple values at once.
    '        Dim saNames(5, 2) As String
    '        saNames(0, 0) = "John"
    '        saNames(0, 1) = "Smith"
    '        saNames(1, 0) = "Tom"
    '        saNames(1, 1) = "Brown"
    '        saNames(2, 0) = "Sue"
    '        saNames(2, 1) = "Thomas"
    '        saNames(3, 0) = "Jane"

    '        saNames(3, 1) = "Jones"
    '        saNames(4, 0) = "Adam"
    '        saNames(4, 1) = "Johnson"

    '        ' Fill C2:C6 with a relative formula (=A2 & " " & B2).
    '        oRng = _DataSheet.Range("C2", "C6")
    '        oRng.Formula = "=A2 & "" "" & B2"

    '        ' Fill D2:D6 with a formula(=RAND()*100000) and apply format.
    '        oRng = _DataSheet.Range("D2", "D6")
    '        oRng.Formula = "=RAND()*100000"
    '        oRng.NumberFormat = "$0.00"

    '        ' AutoFit columns A:D.
    '        oRng = _DataSheet.Range("A1", "D1")
    '        oRng.EntireColumn.AutoFit()

    '        ' Manipulate a variable number of columns for Quarterly Sales Data.
    '        Call DisplayQuarterlySales(_DataSheet)

    '        ' Make sure Excel is visible and give the user control
    '        ' of Excel's lifetime.
    '        oXL.Visible = True
    '        oXL.UserControl = True

    '        ' Make sure that you release object references.
    '        oRng = Nothing
    '        _DataSheet = Nothing
    '        oWB = Nothing
    '        oXL.Quit()
    '        oXL = Nothing

    '        Exit Sub
    'Err_Handler:
    '        MsgBox(Err.Description, vbCritical, "Error: " & Err.Number)
    '    End Sub


    'Private Sub DisplayQuarterlySales(ByVal oWS As Excel.Worksheet)
    '    Dim oResizeRange As Excel.Range
    '    Dim oChart As Excel.Chart
    '    Dim oSeries As Excel.Series
    '    Dim iNumQtrs As Integer
    '    Dim sMsg As String
    '    Dim iRet As Integer


    '    ' Determine how many quarters to display data for.
    '    For iNumQtrs = 4 To 2 Step -1
    '        sMsg = "Enter sales data for" & Str(iNumQtrs) & " quarter(s)?"
    '        iRet = MsgBox(sMsg, vbYesNo Or vbQuestion _
    '           Or vbMsgBoxSetForeground, "Quarterly Sales")
    '        If iRet = vbYes Then Exit For
    '    Next iNumQtrs

    '    ' Starting at E1, fill headers for the number of columns selected.
    '    oResizeRange = oWS.Range("E1", "E1").Resize(ColumnSize:=iNumQtrs)
    '    oResizeRange.Formula = "=""Q"" & COLUMN()-4 & CHAR(10) & ""Sales"""

    '    ' Change the Orientation and WrapText properties for the headers.
    '    oResizeRange.Orientation = 38
    '    oResizeRange.WrapText = True

    '    ' Fill the interior color of the headers.
    '    oResizeRange.Interior.ColorIndex = 36

    '    ' Fill the columns with a formula and apply a number format.
    '    oResizeRange = oWS.Range("E2", "E6").Resize(ColumnSize:=iNumQtrs)
    '    oResizeRange.Formula = "=RAND()*100"
    '    oResizeRange.NumberFormat = "$0.00"

    '    ' Apply borders to the Sales data and headers.
    '    oResizeRange = oWS.Range("E1", "E6").Resize(ColumnSize:=iNumQtrs)
    '    oResizeRange.Borders.Weight = Excel.XlBorderWeight.xlThin

    '    ' Add a Totals formula for the sales data and apply a border.
    '    oResizeRange = oWS.Range("E8", "E8").Resize(ColumnSize:=iNumQtrs)
    '    oResizeRange.Formula = "=SUM(E2:E6)"
    '    With oResizeRange.Borders(Excel.XlBordersIndex.xlEdgeBottom)
    '        .LineStyle = Excel.XlLineStyle.xlDouble
    '        .Weight = Excel.XlBorderWeight.xlThick
    '    End With

    '    ' Add a Chart for the selected data.
    '    oResizeRange = oWS.Range("E2:E6").Resize(ColumnSize:=iNumQtrs)
    '    oChart = oWS.Parent.Charts.Add
    '    With oChart
    '        .ChartWizard(oResizeRange, Excel.XlChartType.xl3DColumn, , Excel.XlRowCol.xlColumns)
    '        oSeries = .SeriesCollection(1)
    '        oSeries.XValues = oWS.Range("A2", "A6")
    '        For iRet = 1 To iNumQtrs
    '            .SeriesCollection(iRet).Name = "=""Q" & Str(iRet) & """"
    '        Next iRet
    '        .Location(Excel.XlChartLocation.xlLocationAsObject, oWS.Name)
    '    End With

    '    ' Move the chart so as not to cover your data.
    '    With oWS.Shapes.Item("Chart 1")
    '        .Top = oWS.Rows(10).Top
    '        .Left = oWS.Columns(2).Left
    '    End With

    '    ' Free any references.
    '    oChart = Nothing
    '    oResizeRange = Nothing
    'End Sub


    Public Sub RefreshAll()
        _Workbook.RefreshAll()
    End Sub


    Public Sub Close()
        '_ExcelApplication.Visible = True
        '_ExcelApplication.UserControl = True

        'Dim path = "C:\Users\Tony van Riet\Dropbox\Development\Chess\Chesser\Engines\Analysis Results\"
        'Dim fileName = "Analysis " & Now.ToString("yyyyMMdd-HHmmss")
        '_Workbook.SaveAs(path & fileName)
        _Workbook.Save()
        _Workbook.Close()

        _ExcelApplication.Quit()

        _DataSheet = Nothing
        _Workbook = Nothing
        _ExcelApplication = Nothing
    End Sub

    Sub ClearDataSheet()
        _DataSheet.Cells.Clear()
    End Sub

End Class
