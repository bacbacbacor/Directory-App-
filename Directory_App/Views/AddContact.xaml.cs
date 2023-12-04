using Directory_App.ViewModel;
using System.Collections.ObjectModel;
using Directory_App.Models;
using System.Text.Json;
using Microsoft.Maui.Storage;
namespace Directory_App.Views;

public partial class AddContact : ContentPage
{
    ContactsViewModel contactViewModel = new ContactsViewModel();
    StudentViewModel studentViewModel = new StudentViewModel();
    ObservableCollection<SchoolProgram> schoolPrograms = new ObservableCollection<SchoolProgram>();
    public ObservableCollection<SchoolProgram> SchoolPrograms { get; set; }


    public class SchoolProgram
    {
        public int iD { get; set; }
        public string SchooloF { get; set; }
    }
    public AddContact()
	{
		InitializeComponent();
        initializeSchoolProgram();
        cboSchoolProg.SelectedIndex = 0;
        BindingContext = contactViewModel;
    }

    private void initializeSchoolProgram()
    {
        schoolPrograms.Add(new SchoolProgram() { SchooloF = " - SELECT - ", iD = 0 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Engineering", iD = 1 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Business and Management", iD = 2 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Computer Studies", iD = 3 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Allied Medical Sciences", iD = 4 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Education", iD = 5 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Arts and Sciences", iD = 6 });
        cboSchoolProg.ItemsSource = schoolPrograms;
        cboCourse.SelectedIndex = 0;
    }

    private void onSelectChange(object sender, EventArgs e)
    {
        SchoolProgram schoolChoice = ((Picker)sender).SelectedItem as SchoolProgram;
        courseUpdate(schoolChoice.iD);
    }

    private void courseUpdate(int school_ID)
    {
        List<String> courses = new List<string>(); ;
        courses.Add("-SELECT-");
        if (school_ID == 1)
        {
            courses.Add(" - SELECT - ");
            courses.Add("BS Civil Engineering");
            courses.Add("BS Computer Engineering");
            courses.Add("BS Electronics and Communication Engineering");
            courses.Add("BS Electrical Engineering");
            courses.Add("BS Industrial Engineering");
            courses.Add("BS Mechanical Engineering");
        }
        else if (school_ID == 2)
        {
            courses.Add(" - SELECT - ");
            courses.Add("BS Science in Business Administration");
            courses.Add("BS Human Resource Development Management");
            courses.Add("BS Business Administration");
            courses.Add("BS Entrepreneurship");
            courses.Add("BS Accountancy");
            courses.Add("BS Management Accounting");
        }
        else if (school_ID == 3)
        {
            courses.Add(" - SELECT - ");
            courses.Add("BS Computer Science");
            courses.Add("BS Entertainment and Multimedia Computing");
            courses.Add("BS Information Systems");
            courses.Add("BS Information Technology");
        }
        else if (school_ID == 4)
        {
            courses.Add(" - SELECT - ");
            courses.Add("BS Nursing");
        }
        else if (school_ID == 5)
        {
            courses.Add(" - SELECT - ");
            courses.Add("Bachelor of Elementary Education");
            courses.Add("Bachelor of Early Childhood Education");
            courses.Add("Bachelor of Physical Education");
            courses.Add("Bachelor of Secondary Education Major in English");
            courses.Add("Bachelor of Secondary Education Major in Filipino");
            courses.Add("Bachelor of Secondary Education Major in Mathematics");
            courses.Add("Bachelor of Secondary Education Major in Science");
            courses.Add("Bachelor of Special Needs Education-Generalist");
            courses.Add("Bachelor of Special Needs Education major in Early Childhood Education");
            courses.Add("Bachelor of Special Needs Education major in Elementary School Teaching");
        }
        else if (school_ID == 6)
        {
            courses.Add(" - SELECT - ");
            courses.Add("BA Communication");
            courses.Add("BA Marketing Communication");
            courses.Add("BA Journalism");
            courses.Add("BA Arts in English Language Studies");
            courses.Add("BA Biology major in Medical Biology");
            courses.Add("BA Biology major in Microbiology");
            courses.Add("BA Psychology");
            courses.Add("BA Library and Information Science");
            courses.Add("BA Arts in International Studies");
            courses.Add("BA Political Science");

        }
        else if (school_ID == 7)
        {
            courses.Add(" - SELECT - ");
            courses.Add("Bachelor of Laws");
        }

       cboCourse.ItemsSource = courses;
       cboCourse.SelectedIndex = 0;
    }

    
   
 
    private async void ValidateForm(object sender, EventArgs e)
    {
        //string id, string lastname, string firstname, string school, string course, string email, string phone, string type
        string SelectedCourse = cboCourse.SelectedItem as string;
        string selectedSchool = cboSchoolProg.SelectedItem as string;
        string type = rdoFaculty.IsChecked ? "Faculty" : "Student";
        ContactDetail contactDetail = new ContactDetail(txtID.Text, txtLastname.Text, txtFirstname.Text, selectedSchool, SelectedCourse, txtEmail.Text, txtMobile.Text, type);
        StudentModel student = new StudentModel();
        bool isAnyRadioButtonSelected = IsAnyRadioButtonSelected();
        contactDetail.ID = txtID.Text;
        contactDetail.LastName = txtLastname.Text;
        contactDetail.FirstName = txtFirstname.Text;
        contactDetail.Phone = txtMobile.Text;
        contactDetail.Email = txtEmail.Text;

        if (rdoFaculty.IsChecked)
        {
            contactDetail.Type = "Faculty";
        }
        else if (rdoStudent.IsChecked)
        {
            contactDetail.Type = "Student";
        }


        int num = cboSchoolProg.SelectedIndex;
        switch (num)
        {
            case 1:
                contactDetail.School = "School of Engineering";
                break;
            case 2:
                contactDetail.School = "School of Business and Management";
                break;
            case 3:
                contactDetail.School = "School of Computer Science";
                break;
            case 4:
                contactDetail.School = "School of Allied Medical Services";
                break;
            case 5:
                contactDetail.School = "School of Education";
                break;
            case 6:
                contactDetail.School = "School of Business and Management";
                break;
        }

        if(String.IsNullOrEmpty(contactDetail.ID))
        {
            await DisplayAlert("Error", "Please Enter Student ID", "continue");
        }
        else if(contactDetail.ID.Length != 5 || !IsNumeric(contactDetail.ID))
        {
            await DisplayAlert("Student ID", "Student ID must be a 5-digit number", "continue");

        }
        else if (String.IsNullOrEmpty(contactDetail.FirstName))
        {
            await DisplayAlert("Firstname", "Must contain your Firstname", "continue");
        }
        else if (String.IsNullOrEmpty(contactDetail.LastName))
        {
            await DisplayAlert("Firstname", "Must contain your Firstname", "continue");
        }
        else if (!IsNumeric(contactDetail.Phone) || String.IsNullOrEmpty(contactDetail.Phone))
        {
            await DisplayAlert("Moblie Number", "Numeric Numbers only", "continue");
        }
        else if (String.IsNullOrEmpty(contactDetail.Email))
        {
            await DisplayAlert("Email", "Must contain your Email", "continue");
        }
        else if (!IsAnyRadioButtonSelected())
        {
            await DisplayAlert("Gender", "Please select a gender.", "continue");
            return;
        }
        else if (cboSchoolProg.SelectedIndex == 0)
        {
            await DisplayAlert("School Program", "Please select a School from the list.", "continue");
            return;
        }
        else if (cboCourse.SelectedIndex == 0)
        {
            await DisplayAlert("Course", "Please select a Course from the list.", "continue");
            return;
        }

        else
        {
            string enteredStudentID = txtID.Text;
            bool studentExists = studentViewModel.StudentsCollection.Any(existingStudent => existingStudent.studentID == enteredStudentID);
            bool IdExists = contactViewModel.ContactCollection.Any(existingId => existingId.ID == enteredStudentID);

            if (studentExists && IdExists)
            {
                await DisplayAlert("ERROR", "ID exists", "OK");
               contactDetail.ID = string.Empty;
            }
            else
            {
                contactDetail.Course = SelectedCourse;
                string fileName = $"U{contactDetail.ID}.txt";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                var json = JsonSerializer.Serialize(contactDetail);
                File.WriteAllText(filePath, json);
                contactViewModel.AddContact(contactDetail);
                ClearForm();
                await DisplayAlert("Registration Summary", "Success", "Ok");
            }
        }
    }
    private void ResetForm(Object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    { 
        txtID.Text = string.Empty;
        txtLastname.Text = string.Empty;
        txtFirstname.Text = string.Empty;
        cboSchoolProg.SelectedIndex = 0;
        cboCourse.SelectedIndex = 0;
        txtEmail.Text = string.Empty;
        txtMobile.Text = string.Empty;
        rdoFaculty.IsChecked = false;
        rdoStudent.IsChecked = false;
    }

    private void OnAddContact_Clicked(object sender, EventArgs e)
    {
        string SelectedCourse = cboCourse.SelectedItem as string;
        string selectedSchool = cboSchoolProg.SelectedItem as string;
        string type = rdoFaculty.IsChecked ? "Faculty" : "Student";
        Navigation.PushAsync(new AddContact());
        ContactDetail contactInfo = new ContactDetail(txtID.Text, txtLastname.Text, txtFirstname.Text, selectedSchool, SelectedCourse, txtEmail.Text, txtMobile.Text, type);
        contactViewModel.AddContact(contactInfo);
    }
    private bool IsNumeric(string input)
    {
        return int.TryParse(input, out _);
    }

    private bool IsAnyRadioButtonSelected()
    {
        return rdoFaculty.IsChecked || rdoStudent.IsChecked;
    }


}