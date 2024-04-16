using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Десктоп_РПМ
{
    public partial class Книга : Window
    {
        public int Id;
        public string Text;
        public string Title;
        public string Author;
        public string Genre;
        public int Year;
        public bool h;
        public Книга()
        {
            InitializeComponent();
        }
        public Книга(int Id, string Text, string Title, string Author, string Genre, int Year)
        {
            this.Id = Id;
            this.Text = Text;
            this.Title = Title;
            this.Author = Author;
            this.Genre = Genre;
            this.Year = Year;
            InitializeComponent();
            StreamReader reader = new StreamReader(this.Text, System.Text.Encoding.UTF8);
            textbox.Content = reader.ReadToEnd();
            label.Content = this.Title;
            UserContext db = new UserContext();
            var count = db.Database.SqlQuery<int>("select count(*) from FavoriteBooks where BookId = @id", new System.Data.SqlClient.SqlParameter("@id", this.Id)).SingleOrDefault();

            if (count > 0)
            {
                heartbutton.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A"));
            }
            else
            {
                heartbutton.Fill = Brushes.Black;
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
            MessageBox.Show(Author + " " + Genre + " " + Convert.ToString(Year));
        }
        private void heart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    var existingFavorite = db.FavoriteBooks.SingleOrDefault(f => f.BookId == this.Id);

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
                            Email = "zverev@example.com"
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
