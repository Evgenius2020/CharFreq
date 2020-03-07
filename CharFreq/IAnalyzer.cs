namespace CharFreq
{
    public interface IAnalyzer
    {
        AnalyzeResult Analyze(string folder);
    }
}