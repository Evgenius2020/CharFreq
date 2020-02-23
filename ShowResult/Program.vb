Imports CharFreq.Implementation

Module ShowResult
    Private Sub TabWrite(s as String)
        Console.Write(s)
        Console.Write(vbTab)
    End Sub
    
    Private Sub TabWrite(d as Double)
        Console.Write("{0:F}", d)
        Console.Write(vbTab)
    End Sub
    
    Sub Main()
        Dim analyzer = new Analyzer()
        Dim analyzeResult = analyzer.Analyze("Files/")
        
        TabWrite("Symb")
        For Each filename in analyzeResult.FileNames
            TabWrite(filename)
        Next
        TabWrite("Avg")
        Console.WriteLine("Disp")
        
        For Each occurencesInfo in analyzeResult.OccurrencesInfos
            TabWrite(occurencesInfo.Symbol)
            For Each occurencesInFile in occurencesInfo.Occurrences
                TabWrite(occurencesInFile)
            Next
            TabWrite(occurencesInfo.OccurrencesAverage)
            Console.WriteLine("{0:F}", occurencesInfo.OccurrencesDispersion)
        Next
    End Sub
End Module
