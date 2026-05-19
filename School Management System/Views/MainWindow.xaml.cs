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
using Microsoft.EntityFrameworkCore;

namespace School_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentRepo _studentRepo = new StudentRepo();
        private readonly ClassRecordRepo _classRecordRepo = new ClassRecordRepo();
        private readonly TeacherRepo _teacherRepo = new TeacherRepo();
        public List<ClassRecord> ItemsSource { get; private set; } = new List<ClassRecord>();

        public MainWindow()
        {
            InitializeComponent();
            LoadAllData();
        }

        //Student DataGrid
        private void LoadAllData()
        {
            using (var context = new SchoolDbContext())
            {
                int studentCount = context.Students.Count();
                int teacherCount = context.Teachers.Count();
                int classCount = context.Classes.Count();

                TotalStudentsCount.Text = studentCount.ToString();
                TotalTeachersCount.Text = teacherCount.ToString();
                TotalTeachersCount.Text = classCount.ToString();

                var today = DateTime.Today;
                int presentToday = context.AttendanceRecords.
                    Count(a => a.Date.Date == today && a.Status == "Present");
                if (studentCount > 0)
                {
                    double rate = ((double)presentToday / studentCount) * 100;
                    AttendanceRateText.Text = $"{rate:F0}%";
                }
                else
                {
                    AttendanceRateText.Text ="0%";

                }


                    StudentDataGrid.ItemsSource = context.Students.Include(s => s.AssignedClass).ToList();
                TeacherDataGrid.ItemsSource = context.Teachers.ToList();
                ClassesData.ItemsSource = context.Classes.ToList();

                ActivityLog.Items.Insert(0, $"Dashboard refereshed at {DateTime.Now:HH:mm:ss}");

            }
        }

        // student ka record add karne ke liye button click event handler
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddStudentWindow();
            win.ShowDialog();
            LoadAllData();
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
                LoadAllData(); 
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
                        LoadAllData(); 
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
            LoadAllData(); 
        }
        private void ShowRecordsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadAllData();
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
                LoadAllData(); 
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

        // teacher data grid
       
        private void OpenAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddTeacherWindow();
            if (win.ShowDialog() == true)
                LoadAllData();
        }
        private void TeacherDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(TeacherDataGrid.SelectedItem is Teacher selectedTeacher)
            {
                OpenEditTeacherForm(selectedTeacher);
            }
        }
        private void EditTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null && btn.DataContext is Teacher selectedTeacher)
            {
                OpenEditTeacherForm(selectedTeacher);
            }
        }
        private void OpenEditTeacherForm(Teacher teacher)
        {
            var editWin = new AddTeacherWindow(teacher);
            if (editWin.ShowDialog() == true)
            {
                LoadAllData(); 
            }
        }
        private void DeleteTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if(btn != null && btn.DataContext is Teacher selectedTeacher)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selectedTeacher.FirstName} {selectedTeacher.LastName}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if(result == MessageBoxResult.Yes)
                {
                    _teacherRepo.DeleteTeacher(selectedTeacher);
                    LoadAllData();
                    MessageBox.Show("Teacher deleted successfully.");

                }
            }
        }
        private void RefreshTeachers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadAllData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred while refreshing teachers: {ex.Message}");
            }
        }


        // Attendance Tab
        private void LoadAttendanceList_Click(object sender, RoutedEventArgs e)
        {
            var students = _studentRepo.GetAllStudentRecords();
            AttendanceGrid.ItemsSource = students;

        }

        private void SaveAttendance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new SchoolDbContext())
                {
                    DateTime selectedDate = AttendanceDatePicker.SelectedDate ?? DateTime.Now;
                    foreach (var student in AttendanceGrid.Items.Cast<Student>())
                    {
                        var existingRecord = context.AttendanceRecords.FirstOrDefault(a => a.StudentId
                        == student.Id && a.Date.Date == selectedDate.Date);
                        if (existingRecord != null)
                        {
                            existingRecord.Status = "Present";
                        }
                        else
                        {
                            var newRecord = new AttendanceRecord
                            {
                                StudentId = student.Id,
                                Date = selectedDate,
                                Status ="Present"
                            };
                            context.AttendanceRecords.Add(newRecord);
                        }
                    }
                    context.SaveChanges();
                    MessageBox.Show($"Attendance for {selectedDate.ToShortDateString()} saved successfully.");
                    LoadAllData();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while saving attendance: {ex.Message}");
            }
        }

        // history view ka liye ligic
        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = HistoryDatePicker.SelectedDate ?? DateTime.Now;
            using (var context = new SchoolDbContext())
            {
                var records = context.AttendanceRecords.
                    Include(a => a.Student)
                    .Where(a => a.Date.Date == selectedDate.Date)
                    .ToList();
                if (records.Count == 0)
                {
                    MessageBox.Show($"No attendance records found for this Date ", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                HistoryGrid.ItemsSource = records;

            }
        }
        private void NavDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;
        }
        private void NavStudents_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;
        }
        private void NavClasses_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
        }
        private void NavTeachers_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 2;
        }

        private void TeacherDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}