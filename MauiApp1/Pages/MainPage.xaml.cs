using MauiApp1.DB;
using MauiApp1.Models;
using MauiApp1.Pages;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;


namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Movie> MovieTablichkaUpdate { get; set; } = new ObservableCollection<Movie>();
        public List<Movie> MovieTablichka { get; set; } = new List<Movie>();
        public List<string> Genres { get; set; } = new List<string> { "Хоррор","Комедия","Романтика","Боевик"};
         
      

       public DBFile db = new DBFile();
       public Movie SelectedMovies { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            Tablichka();
        }
        public async void SaveMovie()
        {
            if (String.IsNullOrEmpty(TitleText.Text) && String.IsNullOrEmpty(DiscriptionText.Text))
            {
                await DisplayAlert("Ошибка", "Фильм  не сохранен", "Ок");    
            }
            else
            {
                await db.AddMovies(TitleText.Text, DiscriptionText.Text, DiscriptionDate.Date, StepperSelect.Value, GenreList.SelectedItem.ToString(), SliderMinutes.Value);
                await DisplayAlert("Успех", "Фильм сохранен", "Ок");
                Tablichka();
            }    
        }
        
        public async void Tablichka()
        {
            MovieTablichkaUpdate.Clear();
            MovieTablichka = await db.GetMovieList();
            for (int i = 0;i< MovieTablichka.Count; i++)
            {
                MovieTablichkaUpdate.Add(MovieTablichka[i]);
            }
        }
        public void Button_Clicked_Movie(object sender, EventArgs e)
        {
          SaveMovie();
        }

        public async void OnChangeClicked(object sender, EventArgs e)
        {

            if (SelectedMovies != null)
            {
                bool result = await DisplayAlert("Изменение",
              $"Вы уверены, что хотите изменить  автора {SelectedMovies.Name}?", "Да", "Нет");
                if (result)
                {
                    if (String.IsNullOrEmpty(TitleText.Text) || String.IsNullOrEmpty(DiscriptionText.Text) || String.IsNullOrEmpty(GenreList.SelectedItem.ToString())) 
                    {


                        await DisplayAlert("Ошибка", "Не все данные заполнены", "ок");
                    }
                    else
                    {
                        await db.ChangeMovie(SelectedMovies.Id, TitleText.Text, DiscriptionText.Text, DiscriptionDate.Date, StepperSelect.Value, GenreList.SelectedItem.ToString(), SliderMinutes.Value);
                        await DisplayAlert("Успех", "Киношка поменялась", "Емае");
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
            
        }
        public async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool inList = true;

            if (SelectedMovies != null)
            {
                List<MoviesAuthors> list = await db.GetMovieAuthorList();
                foreach (MoviesAuthors auth in list)
                {
                    if (auth.IdMovie == SelectedMovies.Id)
                    {

                        inList = false;

                    }


                }
                if (inList)
                {
                    await db.DelMovie(SelectedMovies.Id);
                    await DisplayAlert("Успех", $"Фильм {SelectedMovies.Name} удален", "Ок");
                    Tablichka();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Фильм находится в связи ", "OK");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Не выбран фильм для удаления", "OK");
            }

        }
        public async void Button_Clicked_To_Page2(object sender, EventArgs e)
        {   
            await Navigation.PushModalAsync(new NewPage1(db));
        }
        public async void Button_Clicked_To_Page3(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewPage2(db));
        }

        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            numberStepper.Text = StepperSelect.Value.ToString();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            SliderNumber.Text = SliderMinutes.Value.ToString();
        }
    }

    }

