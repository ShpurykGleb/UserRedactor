using CSharpCourseWork.Controllers;
using System.Windows;
using System.Windows.Media;

namespace CSharpCourseWork
{
    /// <summary>
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        //ChangePassword constructor
        public ChangePassword()
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

            ChangePasswordButton.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));

            PasswordBoxOldPassword.Background = Brushes.Gray;
            PasswordBoxOldPassword.BorderBrush = new SolidColorBrush(Color.FromRgb(242, 243, 244));

            PasswordBoxNewPassword.Background = Brushes.Gray;
            PasswordBoxNewPassword.BorderBrush = new SolidColorBrush(Color.FromRgb(242, 243, 244));

            PasswordBoxNewPasswordConfirm.Background = Brushes.Gray;
            PasswordBoxNewPasswordConfirm.BorderBrush = new SolidColorBrush(Color.FromRgb(242, 243, 244));

            TextBlockOldPasswordtoolTip.Foreground = new SolidColorBrush(Color.FromRgb(242, 243, 244));
            TextBlockNewPasswordToolTip.Foreground = new SolidColorBrush(Color.FromRgb(242, 243, 244));
            TextBlockNewPasswordConfirmToolTip.Foreground = new SolidColorBrush(Color.FromRgb(242, 243, 244));
            TextBlockChangePasswordToolTip.Foreground = new SolidColorBrush(Color.FromRgb(242, 243, 244));
        }

        //Back to profile action
        private void BackToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            AdminClient adminClient = new AdminClient();
            adminClient.Show();

            Hide();
        }

        //Change password action
        private void ChangeButtonPassword_Click(object sender, RoutedEventArgs e)
        {
            //Passwords
            string oldPassword = PasswordBoxOldPassword.Password;
            string newPassword = PasswordBoxNewPassword.Password;
            string newPasswordConfirm = PasswordBoxNewPasswordConfirm.Password;

            //Stream admin
            Admin streamAdmin = FileController.DeserializeStreamAdmin();

            //Old password is incorrect
            if (!ChangePasswordController.IsOldPasswordCorrect(oldPassword, streamAdmin))
            {
                string messageOldPassword = "This is not old password!";
                AnimationController.ShowToolTip(TextBlockOldPasswordtoolTip, ImageOldPasswordToolTip, OpacityProperty, messageOldPassword, 1);
            }
            else // correct
            {
                AnimationController.HideToolTipFast(TextBlockOldPasswordtoolTip, ImageOldPasswordToolTip);
            }

            //New password is incorrect
            string resultsOfCheckingNewPassword = LoginAndPasswordController.CheckPassword(newPassword);
            if (!resultsOfCheckingNewPassword.Equals("Pass"))
            {
                AnimationController.ShowToolTip(TextBlockNewPasswordToolTip, ImageNewPasswordToolTip, OpacityProperty, resultsOfCheckingNewPassword, 1);
            }
            else // correct
            {
                AnimationController.HideToolTipFast(TextBlockNewPasswordToolTip, ImageNewPasswordToolTip);
            }

            //New password and new password confirm are not equal
            if (!ChangePasswordController.IsTheNewPasswordEqualToItsConfirmation(newPassword, newPasswordConfirm))
            {
                string messageConfirmPassword = "Confirm password is not equal to new password!";
                AnimationController.ShowToolTip(TextBlockNewPasswordConfirmToolTip, ImageNewPasswordConfirmToolTip, OpacityProperty, messageConfirmPassword, 1);
            }
            else // equal
            {
                AnimationController.HideToolTipFast(TextBlockNewPasswordConfirmToolTip, ImageNewPasswordConfirmToolTip);
            }

            //All is ok
            if (ChangePasswordController.IsOldPasswordCorrect(oldPassword, streamAdmin)
                && resultsOfCheckingNewPassword.Equals("Pass")
                && ChangePasswordController.IsTheNewPasswordEqualToItsConfirmation(newPassword, newPasswordConfirm))
            {
                //New password is already set
                if (ChangePasswordController.IsPasswordAlreadySet(newPassword, streamAdmin))
                {
                    string messagePasswordIsAlreadySet = "Password is already set!";
                    AnimationController.ShowToolTip(TextBlockChangePasswordToolTip, ImageChangePasswordToolTip, OpacityProperty, messagePasswordIsAlreadySet, 1);
                }
                else // change password
                {
                    AnimationController.HideToolTipFast(TextBlockChangePasswordToolTip, ImageChangePasswordToolTip);

                    ChangePasswordController.ChangePassword(newPassword, streamAdmin);

                    PasswordBoxOldPassword.Clear();
                    PasswordBoxNewPassword.Clear();
                    PasswordBoxNewPasswordConfirm.Clear();

                    string messagePasswordChanged = "Password changed!";
                    AnimationController.ShowToolTip(TextBlockChangePasswordToolTip, ImageChangePasswordToolTip, OpacityProperty, messagePasswordChanged, 1);
                    AnimationController.HideToolTipSlow(TextBlockChangePasswordToolTip, ImageChangePasswordToolTip, OpacityProperty, 3);
                }
            }
        }

        //Mask for PasswordBoxOldPassword
        private void OldPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxOldPassword.Password.Length > 0)
            {
                TextBlockOldPassword.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlockOldPassword.Visibility = Visibility.Visible;
            }
        }

        //Mask for PasswordBoxNewPassword
        private void NewPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxNewPassword.Password.Length > 0)
            {
                TextBlockNewPassword.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlockNewPassword.Visibility = Visibility.Visible;
            }
        }

        //Mask for PasswordBoxNewPasswordConfirm
        private void NewPasswordChangedConfirm(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxNewPasswordConfirm.Password.Length > 0)
            {
                TextBlockNewPasswordConfirm.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlockNewPasswordConfirm.Visibility = Visibility.Visible;
            }
        }
    }
}

