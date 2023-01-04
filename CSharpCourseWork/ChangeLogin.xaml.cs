using CSharpCourseWork.Controllers;
using System.Windows;
using System.Windows.Media;

namespace CSharpCourseWork
{
    /// <summary>
    /// Логика взаимодействия для ChangeLogin.xaml
    /// </summary>
    public partial class ChangeLogin : Window
    {
        //ChangeLogin constructor
        public ChangeLogin()
        {
            InitializeComponent();

            Admin streamAdmin = FileController.DeserializeStreamAdmin();

            if (streamAdmin.Theme)
            {
                ChangeThemeToBlackTheme();
            }

        }

        //Method that changes theme to black
        private void ChangeThemeToBlackTheme()
        {
            GridBackground.Background = new SolidColorBrush(Color.FromRgb(40, 40, 40));

            BackToProfileButton.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));

            RectangleBackground.Fill = Brushes.Gray;

            NewLoginTextBox.Background = Brushes.Gray;
            NewLoginTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(242, 243, 244));

            ConfirmNewLoginTextBox.Background = Brushes.Gray;
            ConfirmNewLoginTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(242, 243, 244));

            ChangeLoginButton.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));

            TextBlockNewLoginToolTip.Foreground = new SolidColorBrush(Color.FromRgb(242, 243, 244));
            TextBlockConfirmNewLoginToolTip.Foreground = new SolidColorBrush(Color.FromRgb(242, 243, 244));
            TextBlockChangeLoginToolTip.Foreground = new SolidColorBrush(Color.FromRgb(242, 243, 244));
        }

        //Back to profile action
        private void BackToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            AdminClient adminClient = new AdminClient();
            adminClient.Show();

            Hide();
        }


        //Change login action
        private void ChangeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Logins
            string newLogin = NewLoginTextBox.Text;
            string newLoginConfirm = ConfirmNewLoginTextBox.Text;

            //Results of checking logins
            string resultsOfCheckingNewLogin = LoginAndPasswordController.CheckLogin(newLogin);
            bool resultsOfCheckingNewLoginConfirm = ChangeLoginController.AreLoginsEqual(newLogin, newLoginConfirm);

            //new login fail
            if (!resultsOfCheckingNewLogin.Equals("Pass"))
            {
                AnimationController.ShowToolTip(TextBlockNewLoginToolTip, ImageNewLoginToolTip, OpacityProperty, resultsOfCheckingNewLogin, 1);
            }
            else // new login success
            {
                AnimationController.HideToolTipFast(TextBlockNewLoginToolTip, ImageNewLoginToolTip);
            }

            //confirm new login fail
            if (!resultsOfCheckingNewLoginConfirm)
            {
                string newLoginConfirmIsNotEqual = "Confirm login is not equal to new login!";
                AnimationController.ShowToolTip(TextBlockConfirmNewLoginToolTip, ImageConfirmNewLoginToolTip, OpacityProperty, newLoginConfirmIsNotEqual, 1);
            }
            else //confirm new login success
            {
                AnimationController.HideToolTipFast(TextBlockConfirmNewLoginToolTip, ImageConfirmNewLoginToolTip);
            }

            //new login and confirm new login are ok
            if (resultsOfCheckingNewLogin.Equals("Pass") && resultsOfCheckingNewLoginConfirm)
            {
                Admin streamAdmin = FileController.DeserializeStreamAdmin();
                //Login is already set
                if (streamAdmin.Login.Equals(newLogin))
                {
                    string loginIsAlreadySet = "Login is already set!";
                    AnimationController.ShowToolTip(TextBlockChangeLoginToolTip, ImageChangeLoginToolTip, OpacityProperty, loginIsAlreadySet, 1);
                }
                else if (EntireController.IsLoginAlreadyTaken(newLogin)) // login is already taken by another user
                {
                    string loginIsTaken = "Login is already taken by another user!";

                    AnimationController.ShowToolTip(TextBlockChangeLoginToolTip, ImageChangeLoginToolTip, OpacityProperty, loginIsTaken, 1);
                }
                else // new login is ok
                {
                    AnimationController.HideToolTipFast(TextBlockChangeLoginToolTip, ImageChangeLoginToolTip);

                    ChangeLoginController.ChangeLogin(newLogin, streamAdmin);
                    FileController.SerializeStreamAdminWithoutSHA256(streamAdmin);

                    NewLoginTextBox.Text = "";
                    ConfirmNewLoginTextBox.Text = "";

                    string resultsOfChangingLogin = "Login is changed!";
                    AnimationController.ShowToolTip(TextBlockChangeLoginToolTip, ImageChangeLoginToolTip, OpacityProperty, resultsOfChangingLogin, 3);
                    AnimationController.HideToolTipSlow(TextBlockChangeLoginToolTip, ImageChangeLoginToolTip, OpacityProperty, 3);
                }
            }

        }
    }
}
