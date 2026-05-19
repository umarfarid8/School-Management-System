using School_Management_System.DatabaseAccess.EntityFramework;
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
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
using School_Management_System.DatabaseAccess.Repository;

namespace School_Management_System.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserRepo _userRepo = new UserRepo();
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTxt.Text) || string.IsNullOrWhiteSpace(PasswordTxt.Password))
            {
                MessageBox.Show("Please enter both Email and Password.", "Entry Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var user = _userRepo.Authenticate(EmailTxt.Text, PasswordTxt.Password);
                if (user != null)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("The email or password you entered is incorrect.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    PasswordTxt.Clear();
                    PasswordTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A system error occurred: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void GoToSignUp_Click(object sender, RoutedEventArgs e)
        {
            var signUpWin = new SignUpWindow();
            signUpWin.Show();
            this.Close();
        }
    }
}
