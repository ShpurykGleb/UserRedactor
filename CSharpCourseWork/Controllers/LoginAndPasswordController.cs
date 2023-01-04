namespace CSharpCourseWork.Controllers
{
    //Class that controls registration process in the program
    public class LoginAndPasswordController
    {
        //The length that must password must be
        private static readonly int _passwordNeededLength = 8;

        //The min length that must be in login
        private static readonly int _loginNeededLength = 5;

        //Static class object of LoginAndPasswordController
        private static LoginAndPasswordController _loginAndPasswordController;

        //Private constructor
        private LoginAndPasswordController() { }

        //Method that gives object of RegistrationController
        public static LoginAndPasswordController GetInstance()
        {
            if (_loginAndPasswordController is null)
            {
                _loginAndPasswordController = new LoginAndPasswordController();
            }

            return _loginAndPasswordController;
        }

        //Method that checks password for correct input
        public static string CheckPassword(string password)
        {
            //Password is null?
            if (password is null)
            {
                return "Password is null!\nEnter the password!";
            }
            else if (password.Equals("")) // Password equals ""?
            {
                return "Password is empty!\nEnter the password!";
            }
            else if (password.Length < _passwordNeededLength) // Password length  is less than needed?
            {
                return $"Password length is less then {_passwordNeededLength} characters!\nEnter the password, that not less then {_passwordNeededLength} characters";
            }
            else // Password contains one big letter and one digit?
            {
                //Flag for big letter
                bool bigLetterFlag = false;

                //Flag for digit
                bool numberFlag = false;

                //Loop that checks contains of letter 
                for (int i = 0; i < password.Length; i++)
                {
                    if (char.IsDigit(password[i])) // if digit in password -> numberFlag = true
                    {
                        numberFlag = true;
                    }
                    else if (char.IsUpper(password[i])) // if big letter in password -> bigLetterFlag = true
                    {
                        bigLetterFlag = true;
                    }
                }

                //Results
                if (!bigLetterFlag)
                {
                    return "Password must contains one big letter!";
                }
                else if (!numberFlag)
                {
                    return "Password must contains one digit!";
                }
            }

            //Password is ok
            return "Pass";
        }

        //Method that checks login for correct input
        public static string CheckLogin(string login)
        {
            //Login is null?
            if (login is null)
            {
                return "Login is null!\nEnter the login!";
            }
            else if (login.Equals("")) // Login equals ""?
            {
                return "Login is empty!\nEnter the login!";
            }
            else if (login.Length < _loginNeededLength) // Login length  is less than needed?
            {
                return $"Login length is less then {_loginNeededLength} characters!\nEnter the login, that not less then {_loginNeededLength} characters";
            }
            else // Login contains one letter?
            {
                //Flag for letter
                bool letterFlag = false;

                //Loop that checks contains of letter 
                for (int i = 0; i < login.Length; i++)
                {
                    if (char.IsLetter(login[i]))
                    {
                        letterFlag = true;
                        break;
                    }
                }

                // Results
                if (!letterFlag)
                {
                    return "Login must contains one letter!";
                }
            }

            //Login is ok
            return "Pass";
        }
    }
}
