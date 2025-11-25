using MauiApp1.DB;
using MauiApp1.Models;
namespace MauiApp1.Pages;

public partial class SignPage : ContentPage
{
    private bool Login;
    private DBFile db;
    public User userNow;
    private List<User> userlist = new List<User>();
    public SignPage()
	{
		InitializeComponent();
	}
	public async void  Sign()
	{
        Login = false;
        if (String.IsNullOrEmpty(LoginEntry.Text) || String.IsNullOrEmpty(PasswordEntry.Text))
		{
            await DisplayAlert("Ошибка", $"Заполните пожалуйста все данные", "OK");
        }
		else
		{
            
            userlist = await db.GetUserList();
            foreach (User user in userlist) 
            { 
            if(user.Name == LoginEntry.Text && user.Password == PasswordEntry.Text)
            {
              userNow = user;
              Login = true;
              break;
            }
            
            
            
            }
            if (Login)
            {
                //потом переходим

            }
            else
            {
                await DisplayAlert("Ошибка","Неправильный пароль или логин","Ок");
            }
        }
		

	}

    private void SignButton(object sender, EventArgs e)
    {
        Sign();
    }

    private void RegButton(object sender, EventArgs e)
    {
        //потом переходим

    }
}