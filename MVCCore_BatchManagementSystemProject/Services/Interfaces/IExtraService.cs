namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface IExtraService
    {
        string Encrypt(string clearText);
        string Decrypt(string clearText);
        string GetRandomPassword(int length);
        void Send_Email(string to, string subject, string body);
        void Send_Gmail_Email(string to, string subject, string body);
        string ConvertAmount(double amount);
        string ConvertAmountInWord(long amount);
    }
}
