namespace CharFreq
{
    public interface IAnalyzer
    {
        public AnalyzeResult Analyze(string folder);
    }
}