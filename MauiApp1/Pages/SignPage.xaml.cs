using MauiApp1.DB;
using MauiApp1.Models;
using MauiApp1;

namespace MauiApp1.Pages;

public partial class SignPage : ContentPage
{
    private bool Login;
    public User userNow = new User();
    private List<User> userlist = new List<User>();
    public SignPage()
	{
		InitializeComponent();
        
	}
	public async void  Sign()
	{
        userlist = await (await DBFile.GetDB()).GetUserList();
        Login = false;
        if (String.IsNullOrEmpty(LoginEntry.Text) || String.IsNullOrEmpty(PasswordEntry.Text))
		{
            await DisplayAlert("Îøèáêà", $"Çàïîëíèòå ïîæàëóéñòà âñå äàííûå", "OK");
        }
		else
		{
            
            
            for(int i = 0; i < userlist.Count; i++)
            {
                if (userlist[i].Name == LoginEntry.Text && userlist[i].Password == PasswordEntry.Text)
            {
              userNow = userlist[i];
              Login = true;
              LoginEntry.Text = null;
              PasswordEntry.Text = null;
                  
              break;
            }
              



            }
            if (Login)
            {
                //первая передача
                await Shell.Current.GoToAsync($"//MainPage?Name={userNow.Name}&Email={userNow.Email}");
            }
            else
            {
                await DisplayAlert("Îøèáêà","Íåïðàâèëüíûé ïàðîëü èëè ëîãèí","Îê");
            }
        }
		

	}
    private void SignButton(object sender, EventArgs e)
    {
        Sign();
    }

  
}