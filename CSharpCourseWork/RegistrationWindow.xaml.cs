using CSharpCourseWork.Controllers;
using System.Windows;

namespace CSharpCourseWork
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        //RegistrationWindow constructor
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        //BackToMainMenuButton action
        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            //Open the main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            //Hide the registration window
            Hide();
        }

        //RegistrationButton action
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            //Get login and password from boxes
            string login = RegisterLoginTextBox.Text;
            string password = RegisterPasswordPasswordBox.Password;

            //Future admin
            Admin newAdmin = new Admin(login, password);

            //Results of checking login and password
            string resultsOfCheckingLogin = LoginAndPasswordController.CheckLogin(login);
            string resultsOfCheckingPassword = LoginAndPasswordController.CheckPassword(password);

            //Fail login
            if (!resultsOfCheckingLogin.Equals("Pass"))
            {
                AnimationController.ShowToolTip(TextBlockLoginToolTip, LoginToolTipImage, OpacityProperty, resultsOfCheckingLogin, 1);
            }
            else // Success login
            {
                AnimationController.HideToolTipFast(TextBlockLoginToolTip, LoginToolTipImage);
            }

            //Fail password
            if (!resultsOfCheckingPassword.Equals("Pass"))
            {
                AnimationController.ShowToolTip(TextBlockPasswordToolTip, PasswordToolTipImage, OpacityProperty, resultsOfCheckingPassword, 1);
            }
            else // Success password
            {
                AnimationController.HideToolTipFast(TextBlockPasswordToolTip, PasswordToolTipImage);
            }

            //Login and password is ok
            if (resultsOfCheckingLogin.Equals("Pass") && resultsOfCheckingPassword.Equals("Pass"))
            {

                //If future admin is already registered in program
                if (EntireController.IsEntireAdminRegisteredAtTheSystem(newAdmin))
                {
                    string textAlreadyRegistered = "You are already registered in the program!";

                    AnimationController.ShowToolTip(TextBlockRegisterToolTip, RegisterToolTipImage, OpacityProperty, textAlreadyRegistered, 1);
                }
                else if (EntireController.IsLoginAlreadyTaken(newAdmin.Login)) // if future admin login is already taken
                {
                    string textLoginIsTaken = "Login is already taken by another user!";

                    AnimationController.ShowToolTip(TextBlockRegisterToolTip, RegisterToolTipImage, OpacityProperty, textLoginIsTaken, 1);
                }
                else // success registration
                {
                    string successfullRegistration = "You are registered at the program!";

                    AnimationController.ShowToolTip(TextBlockRegisterToolTip, RegisterToolTipImage, OpacityProperty, successfullRegistration, 3);

                    FileController.AddNewAdminToDatabase(newAdmin);

                    RegisterLoginTextBox.Text = "";
                    RegisterPasswordPasswordBox.Password = "";

                    AnimationController.HideToolTipSlow(TextBlockRegisterToolTip, RegisterToolTipImage,OpacityProperty, 3);
                }

            }
        }

        //Mask for RegisterPasswordTextBlock
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RegisterPasswordPasswordBox.Password.Length > 0)
            {
                RegisterPasswordTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                RegisterPasswordTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
