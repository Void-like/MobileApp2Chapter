

using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace MauiApp1.DB
{
    
    public class DBFile
    {
        private List<Author> authorList = new List<Author>();
        private List<Movie> moviesList = new List<Movie>();
        private List<MoviesAuthors> listMovies = new List<MoviesAuthors>();
        private List<User> userList = new List<User>();
        private List<int> ints = new List<int> { 0, 0, 0, 0 };
        private static DBFile db;
        public static async Task<DBFile> GetDB()
        {
            if(db == null)
            {
                db = new DBFile();
                await db.LoadDis();

            }
            return db;

        }
       



       
        //Меняем
        public async Task ListMoviesChange(int id, int idAuthor, int idMovies)
        {
            MoviesAuthors movies = new MoviesAuthors();
            movies.Id = id;
            movies.IdAuthor = idAuthor;
            movies.IdMovie = idMovies;

            int a = 0;
            int b = 0;
            foreach (MoviesAuthors author in listMovies)
            {

                if (author.Id == id)
                {

                    b = a;
                    break;
                }
                a++;

            }

            listMovies[b] = movies;
            await SaveFileListMovie();
            await SaveFileDiscriminant();

        }
       
        public async Task ChangeMovie(int id, string name, string description, DateTime date, double ocenochka, string genre, double minutes,string image)
        {
            Movie movies = new Movie();
            movies.Id = id;
            movies.Name = name;
            movies.Description = description;
            movies.Date = date;
            movies.Genre = genre;
            movies.Minutes = minutes;
            movies.Ocenochka = ocenochka;
            movies.Image = image;   

            int a = 0;
            int b = 0;
            foreach (Movie author in moviesList)
            {

                if (author.Id == id)
                {

                    b = a;
                    break;
                }
                a++;

            }
            moviesList[b] = movies;
            await SaveFileMovie();

        }
        public async Task ChangeAuthor(int id, string name, string secondName, string thrityName, DateTime birthDay, string gender, double ocenochka,bool isalive)
        {
            int a = 0;
            int b = 0;
            Author authors = new Author();
            authors.Id = id;
            authors.Name = name;
            authors.SecondName = secondName;
            authors.BirthDay = birthDay;
            authors.ThrityName = thrityName;
            authors.Gender = gender;
            authors.Ocenochka = ocenochka;
            authors.IsAlive = isalive;
            foreach (Author author in authorList)
            {

                if (author.Id == id)
                {
                    b = a;
                    break;
                }
                a++;

            }
            authorList[b] = authors;
            await SaveFileAuthor();
        }
      

      
        //Получаем
        public async Task<List<MoviesAuthors>> GetMovieAuthorList()
        {
            await Task.Delay(1000);
            return listMovies.ToList();
        }
        public async Task<List<Author>> GetAuthorList()
        {
            await Task.Delay(1000);
            return authorList.ToList();
        }

        public async Task<List<Movie>> GetMovieList()
        {
            await Task.Delay(1000);
            return moviesList;
        }
        public async Task<List<User>> GetUserList()
        {
            await Task.Delay(1000);
            return userList;
        }




        //Удаление
        public async Task DelAuthor(int id)
        { 
                Author author = new Author();
                for (int i = 0; i < authorList.Count; i++)
                {
                    if (authorList[i].Id == id)
                    {
                        authorList[i] = author;
                        break;
                    }
                }     
                await Task.Delay(1000);
                authorList.Remove(author);
                await SaveFileAuthor();
            
        }

        public async Task DelMovie(int id)
        {
          
            Movie movie = new Movie();
            for (int i = 0; i < moviesList.Count; i++)
            {
                if (moviesList[i].Id == id)
                {
                    moviesList[i] = movie;
                    break;
                }
            }
                await Task.Delay(1000);
                moviesList.Remove(movie);
                await SaveFileMovie();
            
        }
        public async Task ListMoviesDel(int id)
        {
            MoviesAuthors moviesauthors = new MoviesAuthors();
            foreach (MoviesAuthors moviesauthor in listMovies)
            {

                if (moviesauthor.Id == id)
                {

                    moviesauthors = moviesauthor;
                }


            }
            listMovies.Remove(moviesauthors);
            await SaveFileListMovie();
        }





        //Добавление
        public async Task AddUser(string username, string password, string email)
        {
            User user = new User();
            user.Id = ints[3];
            user.Email = email;
            user.Name = username;
            user.Password = password;
          
            userList.Add(user);
            ints[3] = ints[3] + 1;
            await SaveFileDiscriminant();
            await SaveUser();
        }
        public async Task AddAuthor(string name, string secondName, string thrityName, DateTime birthDay , string gender ,double ocenochka,bool isalive)
        {

            Author author = new Author();
            author.Id = ints[0];
            author.Name = name;
            author.SecondName = secondName;
            author.ThrityName = thrityName;
            author.BirthDay = birthDay;
            author.Gender = gender;
            author.Ocenochka = ocenochka;
            author.IsAlive = isalive;
            authorList.Add(author);
            ints[0] = ints[0] + 1;
            await SaveFileDiscriminant();
            await SaveFileAuthor();
        }
        public async Task AddMovies(string name, string description, DateTime date, double ocenochka, string genre,double minutes, string image )
        {
            Movie movies = new Movie();
            movies.Id = ints[1];
            movies.Name = name;
            movies.Description = description;
            movies.Date = date;
            movies.Ocenochka = ocenochka;
            movies.Genre = genre;
            movies.Minutes = minutes;
            movies.Image = image;
            moviesList.Add(movies);
            ints[1] = ints[1] + 1;
            await SaveFileDiscriminant();
            await SaveFileMovie();
        }
        public async Task ListMoviesAdd(int idAuthor, int idMovies)
        {
            MoviesAuthors movies = new MoviesAuthors();
            movies.Id = ints[2];
            movies.IdAuthor = idAuthor;
            movies.IdMovie = idMovies;

            listMovies.Add(movies);

            ints[2] = ints[2] + 1;

            await SaveFileListMovie();
            await SaveFileDiscriminant();

        }







        //Сохраняю и загружаю ы
        public async Task SaveFileMovie()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "movie2.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, moviesList);
            }
        }

        public async Task LoadFileMovie()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "movie2.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);
                moviesList = JsonSerializer.Deserialize<List<Movie>>(a);

            }

        }
        public async Task SaveFileListMovie()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "listmovie2.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, listMovies);
            }
        }

        public async Task LoadFileListMovie()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "listmovie2.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);
                listMovies = JsonSerializer.Deserialize<List<MoviesAuthors>>(a);

            }

        }
        public async Task SaveFileAuthor()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "author3.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, authorList);
            }
            LoadDis();
        }
        public async Task LoadFileAuthor()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "author3.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);

                authorList = JsonSerializer.Deserialize<List<Author>>(a);

            }

        }
        public async Task SaveFileDiscriminant()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "discriminant3.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, ints);
            }
        }
        public async Task LoadDiscriminant()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "discriminant3.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);
                ints = JsonSerializer.Deserialize<List<int>>(a);

            }

        }
        public async Task SaveUser()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "user1.db");
            using (FileStream outputStream = File.Create(targetFile))
            {
                await JsonSerializer.SerializeAsync(outputStream, userList);
            }
        }
        public async Task LoadUser()
        {

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "user1.db");
            if (File.Exists(targetFile))
            {
                string a = await File.ReadAllTextAsync(targetFile);
                userList = JsonSerializer.Deserialize<List<User>>(a);

            }

        }
        //все загружаем и я не мопс
        public async Task LoadDis()
        {
            await LoadDiscriminant();
            await LoadFileAuthor();
            await LoadFileMovie();
            await LoadFileListMovie();
            await LoadUser();
        }

    }
}


//Задание делать в копии своего приложения с простой навигацией.
//1. Перестроить всю навигацию в приложении на Shell.☑  
//1.1. Использовать все три элемента для навигации в Shell: FlyoutItem, Tab, TabBar☑
//1.2. Переходы на страницы с передачей данных сделать разными способами. ☑
//2. Стилизовать FlyoutItem, Tab, TabBar (цвет, шрифт и т.д.).☑
//3. Создать свои ContentView и найти им применение в приложении.☑
//4. У всплывающего меню сделать в AppShell свои Header и Footer.☑
//5. Сделать страницы авторизации и регистрации.☑
//5.1. При запуске приложения первой страницей открывается авторизация.☑
//5.2. Находясь на этих страницах не должны отображаться всплывающее меню.☑
//6. Сделать загрузку пользователем файлов (изображений, документов и т.д.).☑

