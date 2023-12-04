using Directory_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Directory_App.ViewModel
{
    public class StudentViewModel
    {
        private ObservableCollection<StudentModel> studentsCollection = new ObservableCollection<StudentModel>();
        string mainDir = FileSystem.Current.AppDataDirectory;

        public StudentViewModel()
        {
            ConvertToStudentCollection();
        }

        public void AddStudent(StudentModel student)
        {
            studentsCollection.Add(student);
            SaveToFile();
        }

        public ObservableCollection<StudentModel> StudentsCollection
        {
            get { return studentsCollection; }
            set { studentsCollection = value;}
        }

        public void SaveToFile()
        {
            var json = string.Empty;
            json = JsonSerializer.Serialize(studentsCollection);
            File.WriteAllText(mainDir + "Users.txt", json);        
        }

        public void ConvertToStudentCollection()
        {
            if (File.Exists(mainDir + "Users.txt"))
            {
                string jasonData = File.ReadAllText(mainDir + "Users.txt");
                studentsCollection = JsonSerializer.Deserialize<ObservableCollection<StudentModel>>(jasonData);
            }
        }

    }
}
