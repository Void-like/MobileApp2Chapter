namespace MauiApp1.Pages;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute("NewPage1", typeof(NewPage1));
        Routing.RegisterRoute("NewPage2", typeof(NewPage2));
        Routing.RegisterRoute("NewPage3", typeof(NewPage3));
    }

}