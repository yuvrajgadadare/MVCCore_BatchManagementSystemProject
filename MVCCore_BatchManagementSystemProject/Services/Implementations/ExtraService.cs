using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class ExtraService:IExtraService
    {
        public string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }
        public  string GetRandomPassword(int length)
        {
             
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        public void Send_Email(string to, string subject, string body)
        {

            var fromAddress = new MailAddress("hr@ciitinstitute.com", "CIIT Training Institute");
            var toAddress = new MailAddress(to, to);
            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            var smtp = new SmtpClient
            {
                Host = "relay-hosting.secureserver.net",
                Port = 25,
                Timeout = 2000000,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("hr@ciitinstitute.com", "Ciit#0908")
            };
            System.Net.ServicePointManager.Expect100Continue = false;
            smtp.Send(message);
           // string msg = "Sent Successfully";
           
        }

        public void Send_Gmail_Email(string to, string subject, string body)
        {
            var fromAddress = new MailAddress("yuvraj.gadadare@gmail.com", "CIIT Training Institute");
            var toAddress = new MailAddress(to, to);
            // const string fromPassword = ac.accountPassword;
            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Timeout = 2000000,

                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("yuvraj.gadadare@gmail.com", "fbsdaunsegxejlfa")
            };
            smtp.Send(message);





        }

        private static String[] units = { "Zero", "One", "Two", "Three",
    "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
    "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "", "", "Twenty", "Thirty", "Forty",
    "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public  String ConvertAmount(double amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return ConvertAmountInWord(amount_int) + " Only.";
                }
                else
                {
                    return ConvertAmountInWord(amount_int) + " Point " + ConvertAmountInWord(amount_dec) + " Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public  String ConvertAmountInWord(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " " + ConvertAmountInWord(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " And " + ConvertAmountInWord(i % 100) : "");
            }
            if (i < 100000)
            {
                return ConvertAmountInWord(i / 1000) + " Thousand "
                        + ((i % 1000 > 0) ? " " + ConvertAmountInWord(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return ConvertAmountInWord(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + ConvertAmountInWord(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return ConvertAmountInWord(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + ConvertAmountInWord(i % 10000000) : "");
            }
            return ConvertAmountInWord(i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + ConvertAmountInWord(i % 1000000000) : "");
        }

    }
}