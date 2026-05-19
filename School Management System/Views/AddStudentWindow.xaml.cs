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
        private Student _studentToEdit = null;
        public AddStudentWindow()
        {
            InitializeComponent();
            LoadDropdownData();
        }
        //logic for save button click event handler
        private void SaveStudent_Click(object sender, RoutedEventArgs e)
        {
            //Edit ka liye logic
            int? selectedClassId = (int?)ClassComboBox.SelectedValue;
            int? selectedTeacherId = (int?)TeacherComboBox.SelectedValue;

            if (_studentToEdit == null)
            {
                var newStudent = new Student
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Email = EmailTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text,
                    AssignedClassId = selectedClassId,
                    TeacherId = selectedTeacherId
                };
                _studentRepo.AddStudentObject(newStudent);

               // _studentRepo.AddStudent(newStudent.FirstName, newStudent.LastName, newStudent.Email, newStudent.PhoneNumber);
                MessageBox.Show("New Student Record added successfully!");
            }
            else
            {
                _studentToEdit.FirstName = FirstNameTextBox.Text;
                _studentToEdit.LastName = LastNameTextBox.Text;
                _studentToEdit.Email = EmailTextBox.Text;
                _studentToEdit.PhoneNumber = PhoneTextBox.Text;
                _studentToEdit.AssignedClassId = (int?)ClassComboBox.SelectedValue;
                _studentToEdit.AssignedClass = null;
                _studentToEdit.TeacherId = (int?)TeacherComboBox.SelectedValue;
                _studentToEdit.AssignedTeacher = null;
                _studentRepo.UpdateStudent(_studentToEdit);
                MessageBox.Show("Student Record updated successfully!");
            }
            this.DialogResult = true;
            this.Close();




            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) || string.IsNullOrWhiteSpace(EmailTextBox.Text) || string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

        }
        //logic for cancel button click event handler
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //logic for edit button click event handler
        public AddStudentWindow(Student student) : this()
        {
            InitializeComponent();
            _studentToEdit = student;

            FirstNameTextBox.Text = student.FirstName;
            LastNameTextBox.Text = student.LastName;
            EmailTextBox.Text = student.Email;
            PhoneTextBox.Text = student.PhoneNumber;
            ClassComboBox.SelectedValue = student.AssignedClassId;
            TeacherComboBox.SelectedValue = student.TeacherId;
            this.Title = "Edit Student";
        }

        private void LoadDropdownData()
        {
            using (var db = new SchoolDbContext())
            {
                ClassComboBox.ItemsSource = db.Classes.ToList();
                TeacherComboBox.ItemsSource = db.Teachers.ToList();
            }
        }
    }
}