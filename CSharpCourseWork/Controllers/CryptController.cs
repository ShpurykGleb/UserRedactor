using System.Security.Cryptography;
using System.Text;

namespace CSharpCourseWork.Controllers
{
    //Class that crypt passwords
    public class CryptController
    {
        //Static class object of CryptController
        private static CryptController _cryptController;

        //Private constructor
        private CryptController() { }

        //Method that gives object of CryptController
        public static CryptController GetInstance()
        {
            if(_cryptController is null)
            {
                _cryptController = new CryptController();
            }

            return _cryptController;
        }

        //Nested method that gives crypt password with help of SHA256 algorithm
        private static string Crypt(byte[] bytes)
        {
            SHA256Managed crypt = new SHA256Managed();
            byte[] hash = crypt.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var item in hash)
            {
                stringBuilder.Append(item);
            }

            return stringBuilder.ToString();
        }

        //Method that gives crypt password with help of SHA256 algorithm
        public static string SHA256Crypt(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            return Crypt(bytes);         
        }
    }
}
