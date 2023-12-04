

using Directory_App.Models;
using Directory_App.ViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Xml.Linq;



namespace Directory_App;

public partial class Register : ContentPage
{
    StudentViewModel studentViewModel = new StudentViewModel();

   public class SchoolProgram
   {
        public int iD { get; set; }
        public string SchooloF { get; set; }
    }


    ObservableCollection<SchoolProgram> schoolPrograms = new ObservableCollection<SchoolProgram>();
    public ObservableCollection<SchoolProgram> SchoolPrograms { get; set; }

    public Register()
    {
        InitializeComponent();
        initializeSchoolProgram();
        BindingContext = studentViewModel;
        Shell.Current.Title = "Registration Page";
        cboSchoolProg.SelectedIndex = 0;
        cboYearLvl.SelectedIndex = 0; 
    }

    private void initializeSchoolProgram()
    {
        schoolPrograms.Add(new SchoolProgram() { SchooloF = " - SELECT - ", iD = 0 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Engineering", iD = 1 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Business and Management", iD = 2 });
        schoolPrograms.Add(new SchoolProgram() { SchooloF = "School of Computer Studies", iD =  3});
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
            courses.Add(" - SELECT - " );
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

    private void ResetForm(Object sender, EventArgs e)
    {
        ClearForm();
    }

    public void ClearForm() 
    {
        StudentFLD.Text = string.Empty;
        FirstNameFLD.Text = string.Empty;
        LastNameFLD.Text = string.Empty;
        MobileNoFLD.Text = string.Empty;
        EmailFLD.Text = string.Empty;
        CityFLD.Text = string.Empty;
        PasswordFLD.Text = string.Empty;
        ConfirmPasswordFLD.Text = string.Empty;
        cboSchoolProg.SelectedIndex = 0;
        cboCourse.SelectedIndex = 0;
        cboYearLvl.SelectedIndex = 0;
        rdoFemale.IsChecked = false;
        rdoMale.IsChecked = false;
        cboDate.Date = DateTime.Today;

        
    }

    private async void ValidateForm(Object sender, EventArgs e)
    {
        bool isAnyRadioButtonSelected = IsAnyRadioButtonSelected();
        StudentModel student = new StudentModel();
        student.studentID = StudentFLD.Text;
        student.firstName = FirstNameFLD.Text;
        student.lastName = LastNameFLD.Text;
        student.email = EmailFLD.Text;
        student.password = PasswordFLD.Text;
        student.mobileNum = MobileNoFLD.Text;

        if (rdoFemale.IsChecked)
        {
            student.gender = "Female";
        }
        else if (rdoMale.IsChecked)
        {
            student.gender = "Male";
        }

        student.city = CityFLD.Text; ;
        student.birthday = cboDate.Date.ToString("dd/MM/yyyy");
        string selectedCourse = cboCourse.SelectedItem as string;
        string selectedYear = cboYearLvl.SelectedItem as string;
        int num = cboSchoolProg.SelectedIndex;
        switch (num)
        {
            case 1:
                student.schoolName = "School of Engineering";
                break;
            case 2:
                student.schoolName = "School of Business and Management";
                break;
            case 3:
                student.schoolName = "School of Computer Science";
                break;
            case 4:
                student.schoolName = "School of Allied Medical Services";
                break;
            case 5:
                student.schoolName = "School of Education";
                break;
            case 6:
                student.schoolName = "School of Business and Management";
                break;
        }

        if (String.IsNullOrEmpty(student.studentID))
        {
            await DisplayAlert("Error", "Must contain your Student ID", "continue");
            
        }
        else if(student.studentID.Length != 5 || !IsNumeric(student.studentID))
        {
            await DisplayAlert("Student ID", "Student ID must be a 5-digit number", "continue");
        }
        else if(String.IsNullOrEmpty(student.firstName))
        {
            await DisplayAlert("Firstname", "Must contain your Firstname", "continue");
        }
        else if (String.IsNullOrEmpty(student.lastName))
        {
            await DisplayAlert("Lastname", "Must contain your Lastname", "continue");
        }
        else if (String.IsNullOrEmpty(student.email))
        {
            await DisplayAlert("Email", "Must contain your Email", "continue");
        }
        else if (!IsNumeric(student.mobileNum) || String.IsNullOrEmpty(student.mobileNum))
        {
            await DisplayAlert("Moblie Number", "Numeric Numbers only", "continue");
        }
        else if (String.IsNullOrEmpty(student.password))
        {
            await DisplayAlert("Password", "Must contain your Password", "continue");
        }
        else if (String.IsNullOrEmpty(ConfirmPasswordFLD.Text))
        {
            await DisplayAlert("Password", "Must contain your Password", "continue");
        }
        else if(!IsAnyRadioButtonSelected())
        {
            await DisplayAlert("Gender", "Please select a gender.", "continue");
            return;
        }
        else if(cboSchoolProg.SelectedIndex == 0)
        {
            await DisplayAlert("School Program", "Please select a School from the list.", "continue");
            return;
        }
        else if (cboCourse.SelectedIndex == 0)
        {
            await DisplayAlert("Course", "Please select a Course from the list.", "continue");
            return;
        }
        else if (cboYearLvl.SelectedIndex == 0)
        {
            await DisplayAlert("Year Level", "Please select a Year Level from the list.", "continue");
            return;
        }
        else if (ConfirmPasswordFLD.Text  != student.password)
        {
            await DisplayAlert("Password", "Passwords do not match. Please try again.", "continue");
            return;
        }

        else
        {
            if (studentViewModel.StudentsCollection.Any(existingStudent => existingStudent.studentID == student.studentID))
            {
                await DisplayAlert("ERROR", "ID exists", "OK");
                StudentFLD.Text = string.Empty;
            }
            else
            {
                //DateTime birtdate = cboDate.Date;
                string summaryMessage = $"Student: {student.studentID}\nGender: {student.gender}\nFirstname: {student.firstName}\nLastname: {student.lastName}\nSchool Program: {student.schoolName}\nCourse: {selectedCourse}\nYear Level: {selectedYear}\nBirth Date: {student.birthday}\nMobile no.: {student.mobileNum}\nCity: {student.city}\nEmail: {student.email}\nPassword: {student.password}";
                student.yearLvl = selectedYear;
                student.courseName = selectedCourse;

                // Create a unique file for each student
                string fileName = $"S{student.studentID}.txt";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                // Save student data to the file
                var json = JsonSerializer.Serialize(student);
                File.WriteAllText(filePath, json);

                await DisplayAlert("Registration Summary", summaryMessage, "OK");

                // Add the student to the ViewModel and clear the form
                studentViewModel.AddStudent(student);
                ClearForm();
            }  
        }
    }

    private bool IsNumeric(string input)
    {
        return int.TryParse(input, out _);
    }

    private bool IsAnyRadioButtonSelected()
    {
        return rdoMale.IsChecked || rdoFemale.IsChecked;
    }

}

