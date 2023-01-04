using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CSharpCourseWork.Controllers
{
    //Class that changes password of stream admin
    public class ChangePasswordController
    {
        //Path to database with admins logins and passwords
        private static readonly string _databaseWithAdminsPath = "DatabaseWithAdmins.txt";

        //Static class object of ChangePasswordController
        private static ChangePasswordController _changePasswordController;

        //Private constructor
        private ChangePasswordController() { }

        //Method that gives object of ChangePasswordController
        public static ChangePasswordController GetInstance()
        {
            if (_changePasswordController is null)
            {
                _changePasswordController = new ChangePasswordController();
            }

            return _changePasswordController;
        }

        //Method thath checks is old password correct
        public static bool IsOldPasswordCorrect(string oldPassword, Admin admin) => admin.Password.Equals(CryptController.SHA256Crypt(oldPassword));

        //Method that checks are new password and new password confirm equal or not
        public static bool IsTheNewPasswordEqualToItsConfirmation(string newPassword, string newPasswordConfirm) => newPassword.Equals(newPasswordConfirm);

        //Method that check is new password already set or not
        public static bool IsPasswordAlreadySet(string newPassword, Admin admin) => admin.Password.Equals(CryptController.SHA256Crypt(newPassword));

        //Method that changes password of stream admin
        public static void ChangePassword(string newPassword, Admin adminWithOldPassword)
        {
            List<Admin> admins = JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(_databaseWithAdminsPath));
            for (int i = 0; i < admins.Count; i++)
            {
                if (admins[i].Login.Equals(adminWithOldPassword.Login) && admins[i].Password.Equals(adminWithOldPassword.Password))
                {
                    adminWithOldPassword.Password = CryptController.SHA256Crypt(newPassword);
                    admins[i] = adminWithOldPassword;
                    break;
                }
            }

            FileController.WriteAdmins(admins);
        }
    }
}
