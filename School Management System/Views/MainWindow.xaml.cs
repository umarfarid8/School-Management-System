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
        private readonly AttendanceRepo _attendanceRepo = new AttendanceRepo();
        private readonly NoticeRepo _noticeRepo = new NoticeRepo();
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
                TotalClassesCount.Text = classCount.ToString();

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
                NoticesDataGrid.ItemsSource = _noticeRepo.GetAllNotices();

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
            if(btn?.DataContext is Teacher selectedTeacher)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selectedTeacher.FirstName} {selectedTeacher.LastName}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if(result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var context = new SchoolDbContext())
                        {
                            var teacherInDb = context.Teachers.Find(selectedTeacher.Id);
                            if (teacherInDb != null)
                            {
                                var LinkedStudents = context.Students
                                    .Where(s => s.TeacherId == selectedTeacher.Id)
                                    .ToList();
                                foreach (var student in LinkedStudents)
                                {
                                    student.TeacherId = null;
                                }
                                context.Teachers.Remove(teacherInDb);
                                context.SaveChanges();
                                MessageBox.Show("Teacher is Deleted and Students unassigned successfully");
                                LoadAllData();
                            }
                        }
                    }
                    catch (Exception ex) {
                        string error = ex.InnerException?.Message ?? ex.Message;
                        MessageBox.Show($"Database Error: {error}");
                    }
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
            AttendanceGrid.CommitEdit(DataGridEditingUnit.Row, true);
           
            try
            {
               
                
                    DateTime selectedDate = AttendanceDatePicker.SelectedDate ?? DateTime.Now;
                var recordsToSave = AttendanceGrid.Items.Cast<Student>().Select(student => new AttendanceRecord
                {
                    StudentId = student.Id,
                    Date = selectedDate,
                    Status = student.IsPresent ? "Present" : "Absent"
                }).ToList();
                _attendanceRepo.SaveOrUpdateAttendance(recordsToSave);
                    MessageBox.Show($"Attendance for {selectedDate.ToShortDateString()} saved successfully.");
                    LoadAllData();
                
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
            var history = _attendanceRepo.GetHistoryByDate(selectedDate);

                if (history.Count == 0)
                
                    MessageBox.Show($"No attendance records found for this Date ", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                
                HistoryGrid.ItemsSource = history;

            
        }

        // Notices ka liye 

        public void SaveNotice_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NoticeTitleTxt.Text) || string.IsNullOrWhiteSpace(NoticeContentTxt.Text))
            {
                MessageBox.Show("Please Provide both title and content", "Validation Warning");
                return;
            }
            var notice = new Notice
            {
                Title = NoticeTitleTxt.Text.Trim(),
                Content = NoticeContentTxt.Text.Trim()
            };
            try
            {
                if (string.IsNullOrWhiteSpace(NoticeTitleTxt.Text) && int.TryParse(NoticeIdTxt.Text, out int parsedId))
                {
                    notice.Id = parsedId;
                    _noticeRepo.AddNotice(notice);
                    MessageBox.Show("Notice Published Successfully", "Success");

                }
                else
                {



                    _noticeRepo.AddNotice(notice);


                    MessageBox.Show("Notice Published Successfully", "Success");
                }
                ClearNoticeFields();
                NoticesDataGrid.ItemsSource = _noticeRepo.GetAllNotices();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error : {ex.Message}");
            }
        }
        private void DeleteNotice_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.DataContext is Notice selectedNotice)
            {
                var choice = MessageBox.Show($"Are you sure you want to delete the notice: '{selectedNotice.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (choice == MessageBoxResult.Yes)
                {
                    try
                    {
                        _noticeRepo.DeleteNotice(selectedNotice.Id);
                        LoadAllData();
                        MessageBox.Show("Notice Removed from Database");

                    }
                    catch (Exception ex) {

                        MessageBox.Show($"SQL Deletion Error: {ex.Message}");
                    }
                }
            } 
        }
        private void EditNotice_Click(Object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if(button?.DataContext is Notice selectedNotice)
            {
                NoticeIdTxt.Text = selectedNotice.Id.ToString();
                NoticeTitleTxt.Text = selectedNotice.Title;
                NoticeContentTxt.Text = selectedNotice.Content;
                SaveNoticeBtn.Content = "💾 Update Notice";
            }
        }
        private void ClearNoticeForm_Click(object sender, RoutedEventArgs e)
        {
            ClearNoticeFields();
        }
        private void ClearNoticeFields()
        {
            NoticeIdTxt.Text = string.Empty;
            NoticeTitleTxt.Text = string.Empty;
            NoticeContentTxt.Text = string.Empty;
            SaveNoticeBtn.Content = "🚀 Publish Notice";
        }

        // logout ka liye
        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var confirmation = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation == MessageBoxResult.Yes)
            {
                LoginWindow loginWin = new LoginWindow();
                loginWin.Show();
                this.Close();

            }
        }
        private void NavDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;
        }
        private void NavAttendance_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
        }
        private void NavStudents_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 3;
        }
        private void NavClasses_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 2;
        }
        private void NavTeachers_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 4;
        }
        private void NavNoticeBoard_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 5;
        }

        private void TeacherDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentTabTitle == null) return;
            switch (MainTabControl.SelectedIndex)
            {
                case 0: CurrentTabTitle.Text = "Control Dashboard"; break;
                case 1: CurrentTabTitle.Text = " Attendance Manager"; break;
                case 2: CurrentTabTitle.Text = "Class Logs"; break;
                case 3:CurrentTabTitle.Text = "Student Registry"; break;
                case 4: CurrentTabTitle.Text = "Faculty Profiles"; break;
                case 5: CurrentTabTitle.Text = " School Notice Board"; break;


            }
        }
        
    }
}