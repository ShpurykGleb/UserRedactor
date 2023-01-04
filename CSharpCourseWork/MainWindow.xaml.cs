using CSharpCourseWork.Controllers;
using System.Windows;

namespace CSharpCourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //MainWindow constructor
        public MainWindow()
        {
            InitializeComponent();

            //Create database if not exist
            FileController.CreateDatabaseIfNotExist();

            if (FileController.IsStreamAdminExist())
            {
                AdminClient adminClient = new AdminClient();
                adminClient.Show();

                Hide();
            }
        }

        //LogInButton action
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            //Get login and password from boxes
            string login = LoginTextBox.Text;
            string password = PasswordPasswordBox.Password;

            //Results of checking values
            string resultsOfCheckingValues = ValuesCheckerController.CheckLoginAndPassword(login, password);

            //Fail
            if (!resultsOfCheckingValues.Equals("Pass"))
            {
                AnimationController.ShowToolTip(TextBlockLogInToolTip, LogInToolTipImage, OpacityProperty, resultsOfCheckingValues, 1);
            }
            else // Success
            {
                AnimationController.HideToolTipFast(TextBlockLogInToolTip, LogInToolTipImage);

                //Admin that log in in the system
                Admin admin = new Admin(login, password);

                //If admin is already registered
                if (EntireController.IsEntireAdminRegisteredAtTheSystem(admin))
                {
                    admin.Theme = EntireController.GetAdminsTheme(admin);

                    //Open the admin client
                    FileController.SerializeStreamAdminWithSHA256(admin);

                    AdminClient adminClient = new AdminClient();
                    adminClient.Show();

                    //Hide main window
                    Hide();
                }
                else //If admin is not already registered
                {
                    string messageNotRegistered = "Sorry, you are not registered at system!";
                    AnimationController.ShowToolTip(TextBlockLogInToolTip, LogInToolTipImage, OpacityProperty, messageNotRegistered, 1);
                }
            }
        }

        //RegistrationButton action
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            //Open the registration window
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();

            //Hide the main window
            Hide();
        }

        //Mask for PasswordTextBlock
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordPasswordBox.Password.Length > 0)
            {
                PasswordTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                PasswordTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
