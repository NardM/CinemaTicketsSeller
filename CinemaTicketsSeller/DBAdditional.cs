namespace CinemaTicketsSeller
{
    using System;
    using System.Linq;

    // Дополняю класс модели, который отвечает за работу с БД
    public partial class Movie
    {
        public override string ToString()
        {
            return Title;
        }
    }

    // Дополняю класс модели, который отвечает за работу с БД
    public partial class Session
    {
        public override string ToString()
        {
            return string.Format("{0} {1}", Movie.Title, BeginTime);
        }
    }

    // Для заполнения БД
    public class Seeder
    {
        public Seeder()
        {
            // Все доступные названия фильмов, стран и студий. Заполнял сам
            MovieTitles =
                new string[] 
                {
                    "Вечное сияние чистого разума", "Бойцовский клуб",
                    "Американская история X", "Побег из Шоушенка",
                    "Криминальное чтиво", "Хороший, плохой, злой"
                };

            CountryNames =
                new string[] { "Россия", "Британия", "США", "Италия", "Франция" };

            StudioNames =
                new string[] { "WB", "Marvel", "TR", "Fun32" };

            MovieGenres =
                new string[] { "Ужасы", "Комедия", "Боевик", "Триллер", "Фантастика" };
        }

        private Random _rand;

        public string[] MovieTitles { get; set; }
        public string[] CountryNames { get; set; }
        public string[] StudioNames { get; set; }
        public string[] MovieGenres { get; set; }

        // Метод, который запоняет БД.
        // Инициализирую экзепляр рандомизатора и запускаю методы, которые запоняют отдельные сущности
        public void SeedDB()
        {
            _rand = new Random();

            SeedMovieGenres();
            SeedMovies();
            SeedSessions();
        }

        // Генерирует записи для фильмов
        private void SeedMovies()
        {
            // Коннектимся к БД
            using (CinemaContext dbContext = new CinemaContext())
            {
                foreach (string movieTitle in MovieTitles)
                {
                    Movie movieToAdd = new Movie()
                    {
                        Title = movieTitle,
                        Duration = TimeSpan.FromMinutes(_rand.Next(60, 150)),
                        StudioName = StudioNames[_rand.Next(StudioNames.Length)],
                        Country = CountryNames[_rand.Next(CountryNames.Length)],
                        AgeRestrictions = GetRandomEnumValue<AgeRestrictionName>(),
                        Description = Faker.Lorem.Paragraph()
                    };

                    dbContext.MovieSet.Add(movieToAdd);
                }

                dbContext.SaveChanges();
            }

            LinkMovieWithGenre();
        }

        private void LinkMovieWithGenre()
        {
            using (CinemaContext dbContext = new CinemaContext())
            {
                foreach (Movie mov in dbContext.MovieSet)
                {
                    MovieGenre movieGenreToLink =
                        dbContext.MovieGenreSet.ToList().ElementAt(_rand.Next(0, dbContext.MovieGenreSet.Count()));

                    movieGenreToLink.Movies.Add(mov);
                }

                dbContext.SaveChanges();
            }
        }

        private void SeedMovieGenres()
        {

            using (CinemaContext dbContext = new CinemaContext())
            {

                foreach (string genreName in MovieGenres)
                {
                    MovieGenre movieGenreToAdd = new MovieGenre();

                    movieGenreToAdd.Name = genreName;
                    movieGenreToAdd.Description = Faker.Lorem.Paragraph();

                    dbContext.MovieGenreSet.Add(movieGenreToAdd);
                }

                dbContext.SaveChanges();
            }
        }

        private void SeedSessions()
        {
            using (CinemaContext dbContext = new CinemaContext())
            {

                for (int i = 0; i < 30; i++)
                {
                    Session sessionToAdd = new Session();

                    Movie movieToLink =
                        dbContext.MovieSet.ToList().ElementAt(_rand.Next(0, dbContext.MovieSet.Count()));

                    sessionToAdd.BeginTime = Faker.Date.Between(DateTime.Now, DateTime.Now.AddMonths(2));
                    sessionToAdd.EndTime = sessionToAdd.BeginTime.Add(movieToLink.Duration);
                    sessionToAdd.TicketPrice = Math.Round(60.0d + _rand.NextDouble() * 200, 2);
                    sessionToAdd.Movie = movieToLink;

                    dbContext.SessionSet.Add(sessionToAdd);
                }

                dbContext.SaveChanges();
            }

            MakeTicketsForSession();
        }

        private void MakeTicketsForSession()
        {
            using (CinemaContext dbContext = new CinemaContext())
            {
                foreach (Session sessionFor in dbContext.SessionSet)
                {
                    for (int column = 1; column < 7; column++)
                    {
                        for (int row = 1; row < 5; row++)
                        {
                            Ticket ticketToAdd = new Ticket()
                            {
                                Status = StatusType.Freely,
                                Seat = new SeatInfo() { Row = row, Column = column },
                                Session = sessionFor
                            };

                            dbContext.TicketSet.Add(ticketToAdd);
                        }
                    }
                }

                dbContext.SaveChanges();
            }
        }

        private T GetRandomEnumValue<T>()
        {
            Array enumValues = Enum.GetValues(typeof(T));

            return (T)enumValues.GetValue(_rand.Next(enumValues.Length));
        }
    }
}
