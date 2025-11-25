using MauiApp1.DB;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace MauiApp1.Pages;
public partial class NewPage2 : ContentPage
{
    public ObservableCollection<MoviesAuthors> MainTablichka { get; set; } = new ObservableCollection<MoviesAuthors>();
   
    private MoviesAuthors _selectedMovieAuthor;
    private Author _selectedAuthor;
    private Movie _selectedMovie;

    public List<MoviesAuthors> ListMoviess { get; set; }
    public List<Author> AuthorList { get; set; }
    public List<Movie> MovieList { get; set; }
    public DBFile db = DBFile.GetDB();
    public NewPage2()
    {
        InitializeComponent();   
        Tablichka();
        BindingContext = this;
    }


   
    public void CraftTablichka()
    {
        MainTablichka.Clear();
        MoviesAuthors listMovies = new MoviesAuthors();
        if (ListMoviess == null || ListMoviess.Count == 0)
        {
            return;
        }
       for(int i = 0; i < ListMoviess.Count; i++)
        {
            listMovies.Id = ListMoviess[i].Id;


            foreach (var author in AuthorList)
            {
                if (author.Id == ListMoviess[i].IdAuthor)
                {
                    listMovies.Author = author;
                }
            }
            foreach (var movie in MovieList)
            {
                if (movie.Id == ListMoviess[i].IdMovie)
                {
                    listMovies.Movie = movie;
                }
            }
            MainTablichka.Add(listMovies);

        }
        OnPropertyChanged(nameof(MainTablichka));     

    }

    public MoviesAuthors SelectedMovieAuthor
    {
        get => _selectedMovieAuthor;
        set
        {
            _selectedMovieAuthor = value;
            OnPropertyChanged();
        }
    }

    public Author SelectedAuthor
    {
        get => _selectedAuthor;
        set
        {
            if (_selectedAuthor != value)
            {
                _selectedAuthor = value;
                OnPropertyChanged(nameof(SelectedAuthor));
            }
        }
    }

    public Movie SelectedMovie
    {
        get => _selectedMovie;
        set
        {
            if (_selectedMovie != value)
            {
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }
    }

    public async void SaveAuthor()
    {
        if (SelectedAuthor == null || SelectedMovie == null)
        {
            await DisplayAlert("Ошибка", "Выберите автора и фильм", "OK");
            
        }
        else
        {
            await db.ListMoviesAdd(SelectedAuthor.Id, SelectedMovie.Id);
            await DisplayAlert("Успех", "Связь добавлена", "OK");
        }
        SelectedAuthor = null;
        SelectedMovie = null;
        Tablichka();
    }

    public async void Tablichka()
    {
        try
        {
            var getMovewAuthorListTask = db.GetMovieAuthorList();
            var getMoviesTask = db.GetMovieList();
            var getAuthorsTask = db.GetAuthorList();
            AuthorList = await getAuthorsTask;
            MovieList = await getMoviesTask;
            ListMoviess = await getMovewAuthorListTask;
            PickerAuthor.ItemsSource = AuthorList;
            PickerMovie.ItemsSource = MovieList;
            CraftTablichka();
            OnPropertyChanged(nameof(AuthorList));
            OnPropertyChanged(nameof(MovieList));
            OnPropertyChanged(nameof(ListMoviess));
            OnPropertyChanged(nameof(MainTablichka));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка загрузки данных: {ex.Message}", "OK");
        }
    }

    private void Button_Clicked_Save(object sender, EventArgs e)
    {
        SaveAuthor();
    }

    private async void OnChangeClicked(object sender, EventArgs e)
    {
        
        if (SelectedAuthor == null || SelectedMovie == null|| SelectedMovieAuthor == null)
        {
            await DisplayAlert("Ошибка", "Не выбран элемент или не выбраны фильмы или автора", "OK");
    
        }
        else
        {
            bool result = await DisplayAlert("Изменение",$"Вы уверены, что хотите изменить связь  {SelectedMovieAuthor.Id}?", "Да", "Нет");
            if (result)
            {
                await db.ListMoviesChange(SelectedMovieAuthor.Id, SelectedAuthor.Id, SelectedMovie.Id);
                await DisplayAlert("Успех", "Связь изменена", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", "Вы отменили изменения", "OK");
            }
        }
        Tablichka();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        
        if (SelectedMovieAuthor == null)
        {
            await DisplayAlert("Ошибка", "Не выбран элемент", "OK");
        }
        else
        {
            
          await  db.ListMoviesDel(SelectedMovieAuthor.Id);
          await DisplayAlert("Успех", "Связь удалена", "OK");
        }
        Tablichka();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MainPage());
    }
}