using CSharpCourseWork.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;

namespace CSharpCourseWork.Controllers
{
    //Class that control users in database
    internal class DatabaseController
    {
        //Database with users path
        public static string DatabaseWithUsersName { get; set; }

        //Database with users path reserve
        private static readonly string _databaseWithUsersNameReserve = "DatabaseWithUsers.txt";

        //List with users
        public static BindingList<User> Users { get; set; }

        //Static class object of DatabaseController
        private static DatabaseController _databaseController;

        //Private constructor
        private DatabaseController() { }

        //Method that gives object of DatabaseController
        public static DatabaseController GetInstance()
        {
            if (_databaseController is null)
            {
                _databaseController = new DatabaseController();
            }

            return _databaseController;
        }

        //Nested method that get database with users path
        private static string GetDatabaseFileName()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Text documents (*.txt)|*.txt",
                FilterIndex = 2
            };

            if (dialog.ShowDialog() is true)
            {
                return dialog.FileName;
            }

            return null;
        }

        //Method that gives list with users from database
        public static BindingList<User> GetListWithUsersFromDatabase()
        {
            DatabaseWithUsersName = GetDatabaseFileName();

            Users = new BindingList<User>();
            User._id = Users.Count;

            if (DatabaseWithUsersName is null || DatabaseWithUsersName.Equals(""))
            {
                DatabaseWithUsersName = _databaseWithUsersNameReserve;
                return Users;
            }
            else if (File.ReadAllText(DatabaseWithUsersName).Length == 0)
            {
                return Users;
            }

            try
            {
                Users = JsonConvert.DeserializeObject<BindingList<User>>(File.ReadAllText(DatabaseWithUsersName));
                User._id = Users.Count;
            }
            catch (Exception)
            {
                DatabaseWithUsersName = _databaseWithUsersNameReserve;
                return Users;
            }

            return Users;
        }

        //Method that rewrites users to database
        public static void RewriteDatabaseWithUsers(string databasePath, BindingList<User> users) => File.WriteAllText(databasePath, JsonConvert.SerializeObject(users));

    }
}
