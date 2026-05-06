using School_Management_System.DatabasAccess.Repository;
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

namespace School_Management_System
{
    /// <summary>
    /// Interaction logic for AddClassWindow.xaml
    /// </summary>
    public partial class AddClassWindow : Window
    {
        public AddClassWindow()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredClassName = ClassNameTextBox.Text;
            string enteredRoomNumber = RoomNumberTextBox.Text;
            
            var repository = new ClassRecordRepo();
            repository.AddClassRecord(enteredClassName, enteredRoomNumber);
            MessageBox.Show("Class added successfully!");
            this.Close();
        }
    }
}
