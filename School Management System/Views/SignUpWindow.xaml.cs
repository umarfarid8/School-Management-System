using School_Management_System.DatabaseAccess.EntityFramework.Entities;
using School_Management_System.DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace School_Management_System.Views
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private readonly UserRepo _userRepo = new UserRepo();
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void GoToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWin = new LoginWindow();
            loginWin.Show();
            this.Close();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTxt.Text) ||
                string.IsNullOrWhiteSpace(EmailTxt.Text) ||
                string.IsNullOrWhiteSpace(PasswordTxt.Password))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            var newUser = new User
            {
                FullName = FullNameTxt.Text.Trim(),
                EmailAddress = EmailTxt.Text.Trim(),
                Password = PasswordTxt.Password
            };
            try
            {
                if (_userRepo.RegisterUser(newUser))
                {
                    MessageBox.Show("Registration Successful! Please login.", "Success");
                    GoToLogin_Click(sender, e);
                }
            }
            catch (Exception ex) {

                MessageBox.Show($"Error during registration: {ex.Message}");
            }

        }
    }
}
