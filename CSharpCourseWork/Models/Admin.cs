namespace CSharpCourseWork
{
    //Class that uses for work with program
    public class Admin
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Theme { get; set; } // standart - false, black - true

        //Private constructor
        private Admin() { }

        //Parametric constructor
        public Admin(string login, string password)
        {
            Login = login;
            Password = password;
            Theme = false;
        }
    }
}
