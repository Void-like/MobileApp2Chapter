using MauiApp1.Models;
using MauiApp1.DB;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.Pages;

public partial class NewPage1 : ContentPage
{
    public ObservableCollection<Author> AuthorTablichkaUpdate { get; set; } = new ObservableCollection<Author>();
    public List<Author> AuthorTablichka { get; set; } = new List<Author>();
    public List<string> Genres { get; set; } = new List<string> { "Мужчина", "Девушка" };
   
    public Author SelectedAuthor { get; set; }
    public NewPage1()
	{
		InitializeComponent();
        BindingContext = this;     
        Tablichka();

    }
    public  async void SaveAuthor()
    {
        if (String.IsNullOrWhiteSpace(Name.Text)&& String.IsNullOrWhiteSpace(SecondName.Text) && String.IsNullOrWhiteSpace(ThirtyName.Text) && String.IsNullOrWhiteSpace(gender.SelectedItem.ToString()))
        {
            await DisplayAlert("Ошибка", "Автор не добавлен", "Ок");

        }
        else
        {
            await (await DBFile.GetDB()).AddAuthor(Name.Text, SecondName.Text, ThirtyName.Text, BirthDayText.Date, gender.SelectedItem.ToString(), StepperSelect.Value, LiveOrDie.IsToggled);
            await DisplayAlert("Успех", "Автор добавлен", "Ок");
        }
        Tablichka();
    }
    public async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool inList = true;
     
        if (SelectedAuthor != null)
        {
            List<MoviesAuthors> list = await (await DBFile.GetDB()).GetMovieAuthorList();
            foreach (MoviesAuthors auth in list)
            {
                if (auth.IdAuthor == SelectedAuthor.Id)
                {

                    inList = false;

                }


            }
            if (inList)
            {
                await (await DBFile.GetDB()).DelAuthor(SelectedAuthor.Id);
                await DisplayAlert("Успех", $"автор {SelectedAuthor.SecondName} {SelectedAuthor.Name} удален", "Ок");
                Tablichka();
            }
            else
            {
                await DisplayAlert("Ошибка", "Автор находится в связи ", "OK");
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Не выбран автор для удаления", "OK");
        }
     
    }
    public async void OnChangeClicked(object sender, EventArgs e)
    {
        if (SelectedAuthor != null)
        {
            bool result = await DisplayAlert("Изменение",
          $"Вы уверены, что хотите изменить  автора {SelectedAuthor.Name}?", "Да", "Нет");
            if (result)
            {
                if (String.IsNullOrEmpty(Name.Text) || String.IsNullOrEmpty(SecondName.Text) || String.IsNullOrEmpty(ThirtyName.Text) || String.IsNullOrEmpty(gender.SelectedItem.ToString()))
                {


                    await DisplayAlert("Ошибка", "Не все данные заполнены", "ок");
                }
                else
                {
                    await (await DBFile.GetDB()).ChangeAuthor(SelectedAuthor.Id, Name.Text, SecondName.Text, ThirtyName.Text, BirthDayText.Date, gender.SelectedItem.ToString(), StepperSelect.Value, LiveOrDie.IsToggled);
                    await DisplayAlert("Успех", "Данные автора изменены", "ок");
                    Tablichka();
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Вы отменили изменения", "ок");
            }
        }
        else
        {
            await DisplayAlert("ОШИБКА МОЛОДОСТИ", "Не выбран айтем", "Емае");
        }
        Tablichka();
    }
    public async void Tablichka()
    {
        AuthorTablichkaUpdate.Clear();
        AuthorTablichka = await (await DBFile.GetDB()).GetAuthorList();
        for (int i = 0; i < AuthorTablichka.Count; i++) 
        {
            AuthorTablichkaUpdate.Add(AuthorTablichka[i]);
        }

    }

    public void Button_Clicked_Author(object sender, EventArgs e)
    {
         SaveAuthor();
    }
   
    private async void Button_Clicked_Home(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MainPage");
    }

    private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        numberStepper.Text = StepperSelect.Value.ToString();
    }
}