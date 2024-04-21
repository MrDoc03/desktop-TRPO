using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
namespace Десктоп_РПМ
{
    public partial class MainWindow : Window
    {
        UserContext db = new UserContext();
        public MainWindow()
        {
            InitializeComponent();           
            db.Database.Initialize(true);
            db.SaveChanges();
            var imagesql = db.Books.SqlQuery("select * from Books");

            foreach (var url in imagesql)
            {
                Image image = new Image();
                string coverImagePath = System.IO.Path.Combine("MangaImages",url.PagesFolder, "1.jpg"); // C:\Users\fidan\OneDrive\Рабочий стол\ТРПО\desktop-TRPO\MangaImages\1\1.jpg
                
                image.Source = new BitmapImage(new Uri(coverImagePath, UriKind.Relative));
                
                switch (url.BookId)
                {
                    case 1:
                        
                        image1.Source = image.Source;
                        text1.Text = url.Title + " " + url.Author;
                        button1.Name = "id"+Convert.ToString(url.BookId);
                        break;
                    case 2:
                        image2.Source = image.Source;
                        text2.Text = url.Title + " " + url.Author;
                        button2.Name = "id"+Convert.ToString(url.BookId);
                        break;
                    case 3:
                        image3.Source = image.Source;
                        text3.Text = url.Title + " " + url.Author;
                        button3.Name = "id" + Convert.ToString(url.BookId);
                        break;
                    case 4:
                        image4.Source = image.Source;
                        text4.Text = url.Title + " " + url.Author;
                        button4.Name = "id" + Convert.ToString(url.BookId);
                        break;
                    case 5:
                        image5.Source = image.Source;
                        text5.Text = url.Title + " " + url.Author;
                        button5.Name = "id" + Convert.ToString(url.BookId);
                        break;
                    case 6:
                        image6.Source = image.Source;
                        text6.Text = url.Title + " " + url.Author;
                        button6.Name = "id" + Convert.ToString(url.BookId);
                        break;
                    case 7:
                        image7.Source = image.Source;
                        text7.Text = url.Title + " " + url.Author;
                        button7.Name = "id" + Convert.ToString(url.BookId);
                        break;
                    case 8:
                        image8.Source = image.Source;
                        text8.Text = url.Title + " " + url.Author;
                        button8.Name = "id" + Convert.ToString(url.BookId);
                        break;
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                navigate.Text = "> Любимые книги";
                maincontent.Visibility = Visibility.Hidden;
                favoritecontent.Visibility = Visibility.Visible;
                favorite.Children.Clear();
                using (UserContext db = new UserContext())
                {
                    var favoriteBookIds = db.FavoriteBooks.Select(f => f.BookId).ToList();
                    var favoriteBooks = db.Books.Where(b => favoriteBookIds.Contains(b.BookId)).ToList();

                    foreach (var book in favoriteBooks)
                    {
                        Button button = new Button
                        {
                            Name = "id" + Convert.ToString(book.BookId),
                            Content = book.Title,
                            Tag = book.BookId,
                            Width = 900,
                            
                            Margin = new Thickness(10, 5, 10, 5),
                            Padding = new Thickness(10),
                            Background = Brushes.Orange,
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A")),
                            FontFamily = new FontFamily("Comic Sans MS"),
                            FontSize = 12,
                            FontWeight = FontWeights.Bold,
                            BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A")),
                            BorderThickness = new Thickness(1)
                        };

                        button.Click += Click_Book;
                        favorite.Children.Add(button);
                    }
                    Rectangle rectangle = new Rectangle
                    {
                        Height = 30
                    };
                    favorite.Children.Add(rectangle);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке избранных книг: {ex.Message}");
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Профиль taskWindow = new Профиль();
            taskWindow.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            navigate.Text = "> Главная";
            maincontent.Visibility = Visibility.Visible;
            favoritecontent.Visibility = Visibility.Hidden;
        }
        private void ShowGenreBooks_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string genre = menuItem.Header.ToString();

            LoadGenreBooks(genre);
        }
        private void LoadGenreBooks(string genre)
        {
            try
            {
                navigate.Text = "> "+ genre;
                maincontent.Visibility = Visibility.Hidden;
                favoritecontent.Visibility = Visibility.Visible;
                using (UserContext db = new UserContext())
                {
                    var genreBooks = db.Books.Where(b => b.Genre.ToLower() == genre.ToLower()).ToList();
                    favorite.Children.Clear();

                    foreach (var book in genreBooks)
                    {
                        Button button = new Button
                        {
                            Name = "id" + Convert.ToString(book.BookId),
                            Content = book.Title,
                            Tag = book.BookId,
                            Width = 900,
                            Margin = new Thickness(10, 5, 10, 5),
                            Padding = new Thickness(10),
                            Background = Brushes.Orange,
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A")),
                            FontFamily = new FontFamily("Comic Sans MS"),
                            FontSize = 12,
                            FontWeight = FontWeights.Bold,
                            BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A")),
                            BorderThickness = new Thickness(1),
                        };
                        button.Click += Click_Book;
                        favorite.Children.Add(button);
                    }
                    Rectangle rectangle = new Rectangle
                    {
                        Height = 30
                    };
                    favorite.Children.Add(rectangle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке книг: {ex.Message}");
            }
        }
        private void Click_Book(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            name = name.Remove(0, 2);
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@name", Convert.ToInt64(name));
            var book = db.Books.SqlQuery("select * from Books where BookId=@name", param);
            
            foreach (var books in book)
            {

                Книга taskWindow = new Книга(books.BookId, books.Title, books.Author, books.Genre, books.Year, books.Description, books.PagesFolder);
                taskWindow.Show();
            }
            this.Close();          
        }
        private void LoadBooks(string searchQuery = "")
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    var books = db.Books
                        .Where(b => string.IsNullOrEmpty(searchQuery) ||
                                    b.Title.ToLower().Contains(searchQuery) ||
                                    b.Author.ToLower().Contains(searchQuery) ||
                                    b.Genre.ToLower().Contains(searchQuery))
                        .ToList();
                    favorite.Children.Clear();
                    foreach (var book in books)
                    {
                        Button button = new Button
                        {
                            Name = "id" + Convert.ToString(book.BookId),
                            Content = book.Title,
                            Tag = book.BookId,
                            Width = 900,
                            Margin = new Thickness(10, 5, 10, 5),
                            Padding = new Thickness(10),
                            Background = Brushes.Orange,
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A")),
                            FontFamily = new FontFamily("Comic Sans MS"),
                            FontSize = 12,
                            FontWeight = FontWeights.Bold,
                            BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00680A")),
                            BorderThickness = new Thickness(1),
                        };
                        button.Click += Click_Book;
                        favorite.Children.Add(button);
                    }
                    Rectangle rectangle = new Rectangle
                    {
                        Height = 30
                    };
                    favorite.Children.Add(rectangle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке книг: {ex.Message}");
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks(searchTextbox.Text.ToLower());
            maincontent.Visibility = Visibility.Hidden;
            favoritecontent.Visibility = Visibility.Visible;
            navigate.Text = "> Поиск";
        }    
        private void ToTheSource(object sender, RoutedEventArgs e)
        {           
            Vk_Tg taskWindow = new Vk_Tg();
            taskWindow.Owner = this;
            taskWindow.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
