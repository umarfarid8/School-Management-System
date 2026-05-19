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
    /// Interaction logic for AddTeacherWindow.xaml
    /// </summary>
    public partial class AddTeacherWindow : Window
    {
        private readonly TeacherRepo _teacherRepo = new TeacherRepo();
        private Teacher _teacherToEdit = null;

        public AddTeacherWindow()
        {
            InitializeComponent();
        }
        public AddTeacherWindow(Teacher teacher)
        {
            InitializeComponent();
            _teacherToEdit = teacher;
            FirstNameTextBox.Text = teacher.FirstName;
            LastNameTextBox.Text = teacher.LastName;
            EmailTextBox.Text = teacher.Email;
            PhoneTextBox.Text = teacher.PhoneNumber;
            SubjectTextBox.Text = teacher.Subject;
            this.Title = "Edit Teacher";
        }
        private void Save_Click(object sender,RoutedEventArgs e)
        {
            if (_teacherToEdit == null)
            {
                var newTeacher = new Teacher()
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Email = EmailTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text,
                    Subject = SubjectTextBox.Text
                };
                _teacherRepo.AddTeacher(newTeacher);
                MessageBox.Show("Teacher added successfully!");
            }
            else
            {
                _teacherToEdit.FirstName = FirstNameTextBox.Text;
                _teacherToEdit.LastName = LastNameTextBox.Text;
                _teacherToEdit.Email = EmailTextBox.Text;
                _teacherToEdit.PhoneNumber = PhoneTextBox.Text;
                _teacherToEdit.Subject = SubjectTextBox.Text;
                _teacherRepo.UpdateTeacher(_teacherToEdit);
                MessageBox.Show("Teacher updated successfully!");
            }
            this.DialogResult = true;
            this.Close();
        }
        private void Cancel_Click(object sender,RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
