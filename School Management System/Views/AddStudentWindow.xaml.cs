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
using School_Management_System.DatabaseAccess.EntityFramework;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
using System.Configuration;
namespace School_Management_System.Views
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        private readonly StudentRepo _studentRepo = new StudentRepo();
        public AddStudentWindow()
        {
            InitializeComponent();
        }
        private void SaveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) || string.IsNullOrWhiteSpace(EmailTextBox.Text) || string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;
            string phoneNumber = PhoneTextBox.Text;

            var repository = new StudentRepo();
            repository.AddStudent(firstName, lastName, email, phoneNumber);
            MessageBox.Show("Student added successfully!");
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
