using Directory_App.Models;
using Directory_App.ViewModel;
namespace Directory_App.Views;



public partial class Home : ContentPage
{
	ContactsViewModel conViewModel = new ContactsViewModel();
    public Home()
    {
        InitializeComponent();

        BindingContext = conViewModel;
    }

   
    
private void OnAddContact_Clicked(object sender, EventArgs e)
    {


        Navigation.PushAsync(new AddContact());
       
      
       
    }
	
}