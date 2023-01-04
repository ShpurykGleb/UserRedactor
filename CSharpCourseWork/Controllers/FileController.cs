using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CSharpCourseWork.Controllers
{
    //Class that controls database in the program
    internal class FileController
    {
        //Path to database with admins logins and passwords
        private static readonly string _databaseWithAdminsPath = "DatabaseWithAdmins.txt";

        //Path to database with stream admin login and password
        private static readonly string _streamAdminPath = "StreamAdmin.txt";

        //Static class object of FileController
        private static FileController _fileController;

        //Private constructor
        private FileController() { }

        //Method that gives object of FileController
        public FileController GetInstance()
        {
            if (_fileController is null)
            {
                _fileController = new FileController();
            }

            return _fileController;
        }

        //Method that serialize list of admins and write this list to file(Path -> _databaseWithADminsPath)
        public static void WriteAdmins(List<Admin> admins) => File.WriteAllText(_databaseWithAdminsPath, JsonConvert.SerializeObject(admins));

        //Method that checks is database create
        //if yes - do nothing
        //else create file(Path -> _databaseWithADminsPath) with a serialized list of admins in which there is one admin(login - Admin, password Admin111)
        public static void CreateDatabaseIfNotExist()
        {
            if (!File.Exists(_databaseWithAdminsPath))
            {
                List<Admin> admins = new List<Admin>();

                string standartAdminLogin = "Admin";
                string standartAdminPassword = CryptController.SHA256Crypt("Admin111");

                admins.Add(new Admin(standartAdminLogin, standartAdminPassword));

                WriteAdmins(admins);
            }
        }

        //Method that rewrite database with admins if in program registered new admin
        public static void AddNewAdminToDatabase(Admin newAdmin)
        {
            newAdmin.Password = CryptController.SHA256Crypt(newAdmin.Password);

            List<Admin> adminsBeforeWithAddedNewAdmin = JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(_databaseWithAdminsPath));
            adminsBeforeWithAddedNewAdmin.Add(newAdmin);

            WriteAdmins(adminsBeforeWithAddedNewAdmin);
        }

        //Method that serializes stream admin and write it to file(Path -> _streamAdminPath) with SHA256 algorithm
        public static void SerializeStreamAdminWithSHA256(Admin admin)
        {
            admin.Password = CryptController.SHA256Crypt(admin.Password);

            File.WriteAllText(_streamAdminPath, JsonConvert.SerializeObject(admin));
        }

        //Method that serializes stream admin and write it to file(Path -> _streamAdminPath)
        public static void SerializeStreamAdminWithoutSHA256(Admin admin) => File.WriteAllText(_streamAdminPath, JsonConvert.SerializeObject(admin));

        //Method that gives deserialized stream admin from file(Path -> _streamAdminPath)
        public static Admin DeserializeStreamAdmin() => JsonConvert.DeserializeObject<Admin>(File.ReadAllText(_streamAdminPath));

        //Method that saves admins profile changes in database
        public static void SaveAdminChangesInDatabase(Admin admin)
        {
            List<Admin> admins = JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(_databaseWithAdminsPath));


            for (int i = 0; i < admins.Count; i++)
            {
                if (admin.Login.Equals(admins[i].Login) && admin.Password.Equals(admins[i].Password))
                {
                    admins[i] = admin;
                    break;
                }
            }

            WriteAdmins(admins);
        }

        //Method that deletes file(Path -> _streamAdminPath) with stream admin
        public static void DeleteStreamAdmin() => File.Delete(_streamAdminPath);

        //Method that checks is file(Path->StreamAdmin.txt) exists or not
        public static bool IsStreamAdminExist() => File.Exists(_streamAdminPath);

    }
}
