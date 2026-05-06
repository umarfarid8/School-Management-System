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
        private void LoadAllData()
        {
            StudentDataGrid.ItemsSource = _studentRepo.GetAllStudentRecords();
        }
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
        private void OpenFormButton_Click(object sender, RoutedEventArgs e)
        {
            AddClassWindow form = new AddClassWindow();
            form.ShowDialog();
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
        private void NavDashboard_Click(object sender, RoutedEventArgs e)
        {

        }
        private void NavStudents_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void NavClasses_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void OpenAddClass_Click(object sender, RoutedEventArgs e)
        {
            var addClassWin = new AddClassWindow();
            addClassWin.ShowDialog();
            LoadAllData();
        }
    }
}