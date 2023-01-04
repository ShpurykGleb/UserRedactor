namespace CSharpCourseWork
{
    //Class that checks login and password for correct input
    internal class ValuesCheckerController
    {
        //Static class object of CheckerController
        private static ValuesCheckerController _valuescheckerController;

        //The min length that must be in login
        private static readonly int _loginNeededLength = 5;

        //Password length that should be
        private static readonly int _passwordNeededLength = 8;

        //Private constructor
        private ValuesCheckerController() { }

        //Method that gives object of CheckerController
        public static ValuesCheckerController GetInstance()
        {
            if (_valuescheckerController is null)
            {
                _valuescheckerController = new ValuesCheckerController();
            }

            return _valuescheckerController;
        }

        //Method that check login and password for correct input
        public static string CheckLoginAndPassword(string login, string password)
        {
            //Login or password is null?
            if (login is null || password is null)
            {
                if (login is null)
                {
                    return "Login is null!\nEnter the login!";
                }
                else
                {
                    return "Password is null!\nEnter the password!";
                }
            }
            else if (login.Equals("") && password.Equals("")) // Login and password equals ""?
            {
                return "Enter the values!";
            }
            else if (login.Equals("") || password.Equals("")) // Login or password equals ""?
            {
                if (login.Equals(""))
                {
                    return "Login is empty!\nEnter the login!";
                }
                else
                {
                    return "Password is empty!\nEnter the password!";
                }
            }
            else if(login.Length < _loginNeededLength || password.Length < _passwordNeededLength) // Login length or password length is less than needed?
            {
                if (login.Length < _loginNeededLength)
                {
                    return $"Login length is less then {_loginNeededLength} characters!\nEnter the login, that not less than {_loginNeededLength} characters";
                }
                else
                {
                    return $"Password length is less then {_passwordNeededLength} characters!\nEnter the password, that not less than {_passwordNeededLength} characters";
                }
            }
            
            //Login and password is ok
            return "Pass";
        }
    }
}
