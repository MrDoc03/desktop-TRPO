using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Десктоп_РПМ
{
    public partial class Книга : Window
    {
        public int Id;
        public string Title;
        public string Author;
        public string Genre;
        public int Year;
        public string Description;
        public string PagesHolder;

        public bool h;
        public Книга()
        {
            InitializeComponent();
        }
        public Книга(int Id, string Title, string Author, string Genre, int Year, string Description, string PagesFolder)
        {
            this.Id = Id;
            this.Title = Title;
            this.Author = Author;
            this.Genre = Genre;
            this.Year = Year;
            this.Description = Description;
            this.PagesHolder = PagesFolder;
            Console.WriteLine(this.Id);
            InitializeComponent();

            label.Content = this.Title;
            UserContext db = new UserContext();

            using (UserContext context = new UserContext())
            {
                var count = context.FavoriteBooks.Where(f => f.BookId == this.Id && f.Email == Account.Email).ToList().Count();

                for (int i = 1; i < 100; i++)
                /*int i = 1;               
                while (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), (new Uri("MangaImages/" + PagesFolder + "/" + Convert.ToString(i) + ".jpg", UriKind.Relative)).OriginalString)))
                */
                {
                    try
                    {

                        Image myImage = new Image();
                        myImage.Source = new BitmapImage(new Uri("MangaImages/" + PagesFolder + "/" + Convert.ToString(i) + ".jpg", UriKind.Relative));
                        myImage.Width = 450;

                        myImage.Margin = new Thickness(1, 1, 1, 1);
                        PageHolder.Children.Add(myImage);
                        Console.WriteLine(myImage.Source.ToString());
                        //i++;
                    }
                    catch (Exception ex) { break; }

                }

                if (count > 0)
                {
                    heartbutton.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A"));
                }
                else
                {
                    heartbutton.Fill = Brushes.Black;
                }



            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow taskWindow = new MainWindow();
            taskWindow.Show();
            this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Genre + " " + Description + " " + Convert.ToString(Year));
        }
        private void heart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    var existingFavorite = db.FavoriteBooks.FirstOrDefault(f => f.BookId == this.Id && f.Email == Account.Email);


                    if (existingFavorite != null)
                    {
                        db.FavoriteBooks.Remove(existingFavorite);
                        db.SaveChanges();

                        heartbutton.Fill = Brushes.Black;
                        MessageBox.Show("Книга удалена из избранного!");
                    }
                    else
                    {
                        FavoriteBooks newFavorite = new FavoriteBooks
                        {
                            BookId = this.Id,
                            Email = Account.Email
                        };

                        db.FavoriteBooks.Add(newFavorite);
                        db.SaveChanges();

                        heartbutton.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A"));
                        MessageBox.Show("Книга добавлена в избранное!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении избранного: {ex.Message}");
            }
        }
    }
}
