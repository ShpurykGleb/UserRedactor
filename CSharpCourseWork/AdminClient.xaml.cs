using CSharpCourseWork.Controllers;
using CSharpCourseWork.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CSharpCourseWork
{
    /// <summary>
    /// Логика взаимодействия для AdminClient.xaml
    /// </summary>
    public partial class AdminClient : Window
    {
        //List with users
        private BindingList<User> _users;

        //AdminClient constructor
        public AdminClient()
        {
            InitializeComponent();

            Admin admin = FileController.DeserializeStreamAdmin();

            if (admin.Theme)
            {
                ChangeThemeToBlackTheme();
            }

            TextBlockWithHello.Text = $"Hello, {admin.Login}!";

            _users = new BindingList<User>();
        }

        //List with user changed action
        private void Users_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                DatabaseController.RewriteDatabaseWithUsers(DatabaseController.DatabaseWithUsersName, _users);
            }
        }


        //Change login action
        private void ChangeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeLogin changeLogin = new ChangeLogin();
            changeLogin.Show();

            Hide();
        }

        //Change password action
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.Show();

            Hide();
        }

        //Log out action
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            FileController.DeleteStreamAdmin();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Hide();
        }

        //Method that changes theme to standart
        private void ChangeThemeToStandartTheme()
        {
            GridBackground.Background = new SolidColorBrush(Color.FromRgb(0, 167, 189));
            RectangleProfile.Fill = new SolidColorBrush(Color.FromRgb(242, 243, 244));

            ChangeLoginButton.Background = new SolidColorBrush(Color.FromRgb(104, 220, 43));
            ChangePasswordButton.Background = new SolidColorBrush(Color.FromRgb(104, 220, 43));
            LogOutButton.Background = new SolidColorBrush(Color.FromRgb(104, 220, 43));
            ChangeThemeButton.Background = new SolidColorBrush(Color.FromRgb(104, 220, 43));
            ButtonChooseDatabase.Background = new SolidColorBrush(Color.FromRgb(104, 220, 43));

            ChangeThemeButtonImage.Source = new BitmapImage(new Uri("/Images/7.png", UriKind.RelativeOrAbsolute));
        }

        //Method that changes theme to black
        private void ChangeThemeToBlackTheme()
        {
            GridBackground.Background = new SolidColorBrush(Color.FromRgb(40, 40, 40));
            RectangleProfile.Fill = Brushes.Gray;

            ChangeLoginButton.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            ChangePasswordButton.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            LogOutButton.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            ChangeThemeButton.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            ButtonChooseDatabase.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));

            ChangeThemeButtonImage.Source = new BitmapImage(new Uri("/Images/10.png", UriKind.RelativeOrAbsolute));
        }

        //Change theme action
        private void ChangeThemeButton_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = FileController.DeserializeStreamAdmin();

            // black -> standart
            if (admin.Theme)
            {
                admin.Theme = false;

                ChangeThemeToStandartTheme();
            }
            else // standart -> black
            {
                admin.Theme = true;

                ChangeThemeToBlackTheme();
            }

            FileController.SaveAdminChangesInDatabase(admin);

            FileController.SerializeStreamAdminWithoutSHA256(admin);
        }

        //Choose database button action
        private void ButtonChooseDatabase_Click(object sender, RoutedEventArgs e)
        {
            _users = DatabaseController.GetListWithUsersFromDatabase();
            DataGridWithUsers.ItemsSource = _users;

            _users.ListChanged += Users_ListChanged;
        }

        //Delete row in datagrid action
        private void DataGridWithUsers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key && _users.Count != 0 && DataGridWithUsers.SelectedIndex != _users.Count)
            {
                _users.RemoveAt(DataGridWithUsers.SelectedIndex);
                User._id--;

                for (int i = 0; i < _users.Count; i++)
                {
                    _users[i].ThisId = i + 1;
                }

                DatabaseController.RewriteDatabaseWithUsers(DatabaseController.DatabaseWithUsersName, _users);
            }
        }
    }
}
