namespace PremiumCalculatorLogger
{
    public interface ILogger
    {
        void WriteLog(string message, bool isError);
    }
}
