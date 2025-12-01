
using MauiApp1.DB;
using MauiApp1.Models;
using MauiApp1.Pages;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;





namespace MauiApp1.Pages
{
    [QueryProperty(nameof(Login), "Name")]
    [QueryProperty(nameof(Email), "Email")]

    public partial class MainPage : ContentPage
    {

        string login;
        string email;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Movie> MovieTablichkaUpdate { get; set; } = new ObservableCollection<Movie>();
        public List<Movie> MovieTablichka { get; set; } = new List<Movie>();
        public List<string> Genres { get; set; } = new List<string> { "Хоррор", "Комедия", "Романтика", "Боевик" };


        public Movie SelectedMovies { get; set; } = new Movie();


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

                await (await DBFile.GetDB()).AddMovies(TitleText.Text, DiscriptionText.Text, DiscriptionDate.Date, StepperSelect.Value, GenreList.SelectedItem.ToString(), SliderMinutes.Value, imageL);
                await DisplayAlert("Успех", "Фильм сохранен", "Ок");
                Tablichka();
            }
        }

        public async void Tablichka()
        {
            MovieTablichkaUpdate.Clear();
            MovieTablichka = await (await DBFile.GetDB()).GetMovieList();
            for (int i = 0; i < MovieTablichka.Count; i++)
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
                        await (await DBFile.GetDB()).ChangeMovie(SelectedMovies.Id, TitleText.Text, DiscriptionText.Text, DiscriptionDate.Date, StepperSelect.Value, GenreList.SelectedItem.ToString(), SliderMinutes.Value, imageL);
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
                List<MoviesAuthors> list = await (await DBFile.GetDB()).GetMovieAuthorList();
                foreach (MoviesAuthors auth in list)
                {
                    if (auth.IdMovie == SelectedMovies.Id)
                    {

                        inList = false;

                    }


                }
                if (inList)
                {
                    await (await DBFile.GetDB()).DelMovie(SelectedMovies.Id);
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
            await Shell.Current.GoToAsync("NewPage2");
        }
        public async void Button_Clicked_To_Page3(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("NewPage3");
        }

        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            numberStepper.Text = StepperSelect.Value.ToString();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            SliderNumber.Text = SliderMinutes.Value.ToString();
        }

        private async void LoadImage(object sender, EventArgs e)
        {
            await FilePicker.Default.PickAsync();
            var dictionary = new Dictionary<DevicePlatform, IEnumerable<string>>();
            dictionary[DevicePlatform.Android] = new List<string> { "jpg", "png", "jpeg" };
            dictionary[DevicePlatform.WinUI] = new List<string> { "jpg", "png", "jpeg" };
            PickOptions pickOptions = new PickOptions();
            pickOptions.FileTypes = new FilePickerFileType(dictionary);
            FileResult fileResult = await FilePicker.Default.PickAsync(pickOptions);
            if (fileResult != null)
            {
                Stream inputStream = await fileResult.OpenReadAsync();
                imageL.Source = ImageSource.FromStream(() => inputStream);



            }

        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool GOGOGO = false;
            Movie movie = e.CurrentSelection.FirstOrDefault() as Movie;
            var navigationParameter = new ShellNavigationQueryParameters
    {
        { "Movie", movie }
    };
            GOGOGO = await DisplayAlert("тпаемся", "пошли на другую страницу", "давай","не");
            if (GOGOGO) 
            {
                await Shell.Current.GoToAsync($"//NewPage2", navigationParameter);
            }
        }

    }
}

    

