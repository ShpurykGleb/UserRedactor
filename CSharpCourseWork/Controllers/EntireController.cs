using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpCourseWork.Controllers;

namespace CSharpCourseWork
{
    //Class that control entire process in the program
    public class EntireController
    {
        //Path to database with admins logins and passwords
        private static readonly string _databaseWithAdminsPath = "DatabaseWithAdmins.txt";

        //Static class object of EntireController
        private static EntireController _entireController;

        //Private constructor
        private EntireController() { }

        //Method that gives object of EntireController
        public static EntireController GetInstance()
        {
            if (_entireController is null)
            {
                return _entireController = new EntireController();
            }

            return _entireController;
        }

        //Method that gives deserialized list with admins from file(Path -> DatabaseWithAdminsPath)
        private static List<Admin> GetListWithAdminsFromFile() => JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(_databaseWithAdminsPath));

        //Nested method that gives true if admin is registered in the system, and false if is not 
        public static bool IsEntireAdminRegisteredAtTheSystem(Admin admin) => GetListWithAdminsFromFile().Any(x => x.Login.Equals(admin.Login) && x.Password.Equals(CryptController.SHA256Crypt(admin.Password)));

        //Method that gives admins theme
        public static bool GetAdminsTheme(Admin admin) => GetListWithAdminsFromFile().Where(x => x.Login.Equals(admin.Login) && x.Password.Equals(CryptController.SHA256Crypt(admin.Password))).ToList()[0].Theme;

        //Method that gives true if newAdmin login is already taken, and false if is not
        public static bool IsLoginAlreadyTaken(string login) => GetListWithAdminsFromFile().Any(x => x.Login.Equals(login));
    }
}
