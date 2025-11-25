using MauiApp1.DB;

namespace MauiApp1.Pages;

public partial class RegisterPage : ContentPage
{
    public DBFile db = DBFile.GetDB();
    public RegisterPage()
	{
		InitializeComponent();
	}
    public async void Registration()
    {
        if (String.IsNullOrEmpty(LoginEntry.Text) || String.IsNullOrEmpty(PasswordEntry.Text)|| String.IsNullOrEmpty(SecondPasswordEntry.Text)|| String.IsNullOrEmpty(Mail.Text))
        {
            await DisplayAlert("Ошибка", $"Заполните пожалуйста все данные", "OK");
        }
        else
        {
            if (PasswordEntry.Text == SecondPasswordEntry.Text)
            {
                if(Mail.Text.Contains("@")&& Mail.Text.Contains("."))
                {
                    await db.AddUser(LoginEntry.Text,PasswordEntry.Text,Mail.Text);
                    await DisplayAlert("Успех", "вы зарегались", "OK");
                }
                else
                {
                    await DisplayAlert("Ошибка", "Напишите коректнную почту", "OK");
                }

            }
            else
            {
                await DisplayAlert("Ошибка", $"Повторите пароль", "OK");
            }



        }


    }

    private void RegButton(object sender, EventArgs e)
    {
        Registration();
    }

    private void SignButton(object sender, EventArgs e)
    {
        //переход
    }
}