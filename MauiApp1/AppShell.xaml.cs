using MauiApp1.Pages;
namespace MauiApp1;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
     
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private async void Clouse(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TydaNado");
    }
}
