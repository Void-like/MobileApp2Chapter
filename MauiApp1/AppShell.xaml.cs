using MauiApp1.Pages;
namespace MauiApp1;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("MainPage", typeof(MainPage));
        Routing.RegisterRoute("NewPage1", typeof(NewPage1));
    }
   
}
