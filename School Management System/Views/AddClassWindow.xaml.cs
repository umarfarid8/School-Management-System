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

namespace School_Management_System
{
    /// <summary>
    /// Interaction logic for AddClassWindow.xaml
    /// </summary>
    public partial class AddClassWindow : Window
    {
        private readonly ClassRecordRepo _classRepo = new ClassRecordRepo();
        private ClassRecord _classToEdit = null;
        public AddClassWindow()
        
        {
            InitializeComponent();
        }

        public AddClassWindow(ClassRecord classToEdit)
        {
            InitializeComponent();
            _classToEdit = classToEdit;
            ClassNameTextBox.Text = classToEdit.ClassName;
            RoomNumberTextBox.Text = classToEdit.ClassRoom;

            this.Title = "Edit Class Record";
        }
        // logic for save button click event handler
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if(_classToEdit == null)
            {
                string enteredClassName = ClassNameTextBox.Text;
                string enteredRoomNumber = RoomNumberTextBox.Text;

                var repository = new ClassRecordRepo();
                repository.AddClassRecord(enteredClassName, enteredRoomNumber);
                MessageBox.Show("Class added successfully!");
                this.Close();

            }
            else
            {
                _classToEdit.ClassName = ClassNameTextBox.Text;
                _classToEdit.ClassRoom = RoomNumberTextBox.Text;
                _classRepo.UpdateClass(_classToEdit);
                MessageBox.Show("Class updated successfully!");
             
            }
            this.DialogResult = true;
            this.Close();



        }
        
    }
}
