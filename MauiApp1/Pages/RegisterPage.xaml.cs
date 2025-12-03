using MauiApp1.DB;
using MauiApp1.Models;
namespace MauiApp1.Pages;

public partial class RegisterPage : ContentPage
{
    private List<User> userlist = new List<User>();
    public RegisterPage()
	{
		InitializeComponent();
	}
    public async void Registration()
    {
        if (String.IsNullOrEmpty(LoginEntry.Text) || String.IsNullOrEmpty(PasswordEntry.Text)|| String.IsNullOrEmpty(SecondPasswordEntry.Text)|| String.IsNullOrEmpty(Mail.Text))
        {
            await DisplayAlert("Îøèáêà", $"Çàïîëíèòå ïîæàëóéñòà âñå äàííûå", "OK");
        }
        else
        {
            if (PasswordEntry.Text == SecondPasswordEntry.Text)
            {
                if(Mail.Text.Contains("@")&& Mail.Text.Contains("."))
                {
                    List<User> userlist = await (await DBFile.GetDB()).GetUserList();
                    bool manchik = false;
                    for (int i = 0; i < userlist.Count;i++)
                    {
                        if (userlist[i].Name == LoginEntry.Text)
                        {
                            manchik = true;
                        }
                       
                    }
                    if (manchik)
                    {
                        await DisplayAlert("Ошибка", "Такой логин уже есть", "OK");
                    }
                    else
                    {
                        await (await DBFile.GetDB()).AddUser(LoginEntry.Text, PasswordEntry.Text, Mail.Text);
                        await DisplayAlert("Óñïåõ", "âû çàðåãàëèñü", "OK");
                        LoginEntry.Text = null;
                        PasswordEntry.Text = null;
                        SecondPasswordEntry.Text = null;
                        Mail.Text = null;

                    }
                }
                else
                {
                    await DisplayAlert("Îøèáêà", "Íàïèøèòå êîðåêòííóþ ïî÷òó", "OK");
                }

            }
            else
            {
                await DisplayAlert("Îøèáêà", $"Ïîâòîðèòå ïàðîëü", "OK");
            }



        }


    }

    private void RegButton(object sender, EventArgs e)
    {
        Registration();
    }

   
}