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
using School_Management_System.DatabasAccess.EntityFramework;
namespace School_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
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
                ClassesDataGrid.ItemsSource = allClasses;
            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}