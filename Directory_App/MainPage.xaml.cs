
using System.Windows.Input;
using Directory_App.ViewModel;
using Directory_App.Models;
using Directory_App.Views;

namespace Directory_App
{
    public partial class MainPage : ContentPage
    {
        StudentViewModel studentViewModel = new StudentViewModel();
        StudentModel student = new StudentModel();
        public MainPage()
        {
            InitializeComponent();
            Shell.Current.Title = "Login Page";
        }

         private void OnClicked(object sender, EventArgs e)
        {
            string enteredStudentID = StudentIDFLD.Text;

            if (string.IsNullOrWhiteSpace(enteredStudentID))
            {
                DisplayAlert("ERROR", "Student ID should not be empty. Please enter a Student ID.", "OK");
            }
            else
            {
                // Check if the entered student ID exists in the Users.txt file
                bool studentExists = studentViewModel.StudentsCollection.Any(existingStudent => existingStudent.studentID == enteredStudentID);

                if (studentExists)
                {
                    // User exists, show a success message
                    DisplayAlert("Congrats!", "Student ID is registered.", "OK");
                    //Redirect to homepage
                    Navigation.PushAsync(new Home());
                }
                else
                {
                    // User does not exist, show an error message
                    DisplayAlert("ERROR", "User does not exist. Please register.", "OK");
                    StudentIDFLD.Text = string.Empty; // Clear the Entry control
                    PasswordFLD.Text = string.Empty;
                }
            }
        }

        private async void OnHyperLink(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

       
    }
}