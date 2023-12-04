using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Directory_App.Models;


namespace Directory_App.ViewModel
{
    public class ContactsViewModel
    {


        private ObservableCollection<ContactDetail> contactCollection = new ObservableCollection<ContactDetail>();
        string mainDir = FileSystem.Current.AppDataDirectory;

        public ContactsViewModel()
        {
            ConvertToContactCollection();
        }

        public ObservableCollection<ContactDetail> ContactCollection
        {
            get { return contactCollection; }
            set { contactCollection = value; }
        }

        public void AddContact(ContactDetail contactDetail)
        {
            contactCollection.Add(contactDetail);
            SaveToFile();
        }

        public void SaveToFile()
        {
            var json = string.Empty;
            json = JsonSerializer.Serialize(contactCollection);
           
            File.WriteAllText(mainDir + "Test.txt", json);

        }

        public void ConvertToContactCollection()
        {
           
            if (File.Exists(mainDir + "Test.txt"))
            {
                string jsonData = File.ReadAllText(mainDir + "Test.txt");
                contactCollection = JsonSerializer.Deserialize<ObservableCollection<ContactDetail>>(jsonData);
            }

        }
    }
}
