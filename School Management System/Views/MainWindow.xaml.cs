using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using School_Management_System.DatabaseAccess.EntityFramework;
using School_Management_System.DatabaseAccess.Repository;
using System.Collections.Generic;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;
using School_Management_System.Views;
namespace School_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentRepo _studentRepo = new StudentRepo();
        private readonly ClassRecordRepo _classRecordRepo = new ClassRecordRepo();

        public List<ClassRecord> ItemsSource { get; private set; } = new List<ClassRecord>();

        public MainWindow()
        {
            InitializeComponent();
            LoadAllData();
        }

        //Student DataGrid
        private void LoadAllData()
        {
            StudentDataGrid.ItemsSource = _studentRepo.GetAllStudentRecords();

        }

        // student ka record add karne ke liye button click event handler
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddStudentWindow();
            win.ShowDialog();
            LoadAllData(); // Refresh data after adding a student
        }
        private void RefreshStudents_Click(object sender, RoutedEventArgs e)
        {
            LoadAllData();
        }
        // student record edit karne ke liye double click event handler
        private void StudentDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StudentDataGrid.SelectedItem is Student selectedStudent)
            {
                OpenEditForm(selectedStudent);
            }
        }
        // student record edit karne ke liye button click event handler
        private void EditStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if(button.DataContext is Student selectedStudent)
            {
                OpenEditForm(selectedStudent);
            }
        }

        // student record edit karne ke liye form open karne ka method
        private void OpenEditForm(Student student)
        {
            var editWin = new AddStudentWindow(student);
            if (editWin.ShowDialog() == true)
            {
                LoadAllData(); // Refresh data after editing a student
            }
        }

        // student record delete karne ke liye button click event handler

        public void DeleteStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button?.DataContext is Student selectedStudent)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {selectedStudent.FirstName} {selectedStudent.LastName}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _studentRepo.DeleteStudent(selectedStudent);
                        LoadAllData(); // Refresh data after deletion
                        MessageBox.Show("Student deleted successfully.");
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the student: {ex.Message}");
                    }
                }
            }
        }
        //Class DataGrid
        private void OpenFormButton_Click(object sender, RoutedEventArgs e)
        {
            AddClassWindow form = new AddClassWindow();
            form.ShowDialog();
            LoadAllData(); // Refresh data after adding a class
        }
        private void ShowRecordsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    var allClasses = db.Classes.ToList();
                    ClassesData.ItemsSource = allClasses;

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void OpenAddStudent_Click(object sender, RoutedEventArgs e)
        {
            var studentWindow = new AddStudentWindow();
            bool? result = studentWindow.ShowDialog();
            if (result == true)
            {
                LoadAllData();
            }
        }
        // Class record edit karne ke liye double click event handler
        private void ClassesData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(ClassesData.SelectedItem is ClassRecord selectedClass)
            {
                OpenEditClassForm(selectedClass);
            }
        }
        // Class record edit karne ke liye button click event handler
        private void EditClassBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.DataContext is ClassRecord selectedClass)
            {
                OpenEditClassForm(selectedClass);
            }
        }

        // Class record edit karne ke liye form open karne ka method
        private void OpenEditClassForm(ClassRecord classRecord)
        {
            var editWin = new AddClassWindow(classRecord);
            if (editWin.ShowDialog() == true)
            {
                LoadAllData(); // Refresh data after editing a class
            }
        }

        // Class record delete karne ke liye button click event handler
        private void DeleteClassBtn_Click(object sender, RoutedEventArgs e)
        {
           var button = sender as System.Windows.Controls.Button;
            if (button != null && button.DataContext is ClassRecord selectedClass)
            {
                var result = MessageBox.Show($"Delete {selectedClass.ClassName}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _classRecordRepo.DeleteClass(selectedClass);
                        LoadAllData(); // Refresh data after deletion
                        MessageBox.Show("Class deleted successfully.");
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the class: {ex.Message}");
                    }
                }
            }
        }
        private void NavDashboard_Click(object sender, RoutedEventArgs e)
        {

        }
        private void NavStudents_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void NavClasses_Click(object sender, RoutedEventArgs e)
        {
            
        }
       
    }
}