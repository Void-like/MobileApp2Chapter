using MauiApp1.Pages;
namespace MauiApp1;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("MainPage", typeof(MainPage));
        Routing.RegisterRoute("NewPage1", typeof(NewPage1));
        Routing.RegisterRoute("SignPage", typeof(SignPage));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private async void Clouse(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SignPage");
    }
}
