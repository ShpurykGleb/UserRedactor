using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CSharpCourseWork.Controllers
{
    //Class that changes login of stream admin
    public class ChangeLoginController
    {
        //Path to database with admins logins and passwords
        private static readonly string _databaseWithAdminsPath = "Database.txt";

        //Static class object of ChangeLoginController
        private static ChangeLoginController _changeLoginController;

        //Private constructor
        private ChangeLoginController() { }

        //Method that gives object of ChangeLoginController
        public ChangeLoginController GetInstance()
        {
            if (_changeLoginController is null)
            {
                _changeLoginController = new ChangeLoginController();
            }

            return _changeLoginController;
        }

        //Method that checks are new login and new login confirm equal or not
        public static bool AreLoginsEqual(string newLogin, string newLoginConfirm) => newLoginConfirm.Equals(newLogin);

        //Method that changes login of stream admin
        public static void ChangeLogin(string newLogin, Admin adminWithOldLogin)
        {
            List<Admin> admins = JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(_databaseWithAdminsPath));
            for (int i = 0; i < admins.Count; i++)
            {
                if (admins[i].Login.Equals(adminWithOldLogin.Login) && admins[i].Password.Equals(adminWithOldLogin.Password))
                {
                    adminWithOldLogin.Login = newLogin;
                    admins[i] = adminWithOldLogin;
                    break;
                }
            }

            FileController.WriteAdmins(admins);
        }
    }
}
