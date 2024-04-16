using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Десктоп_РПМ;

public static class DbInitializer
{
    public static void Initialize(UserContext context) //разобраться с YourDbContext объект YourDbContext, который представляет контекст базы данных Entity Framework
    {
        // Проверка, пуста ли база данных
        if (context.Books.Any())
        {
            return; // База данных уже заполнена
        }

        // Пример данных для манги
        var books = new Book[]
        {
            new Book
            {
                Title = "Атака Титанов",
                Author = "Хадзимэ Исаяма",
                Year = 2009,
                Genre = "Фэнтези, боевик",
                Description = "История о борьбе человечества с гигантскими титанами",
                CoverImage = "/MangaImages/1/cover.jpg" // Путь к обложке
            }
        };

        foreach (var book in books)
        {
            context.Books.Add(book);

            // Пример добавления страниц (замените путями к вашим изображениям)
            var pages = new Page[]
            {
                new Page { BookId = book.BookId, PageNumber = 1, ImagePath = "/MangaImages/1/page1.jpg" },
                new Page { BookId = book.BookId, PageNumber = 2, ImagePath = "/MangaImages/1/page2.jpg" },
                // ... Добавьте остальные страницы
            };

            context.Pages.AddRange(pages);
        }

        context.SaveChanges();
    }
}

public class Book
{
    [Key]
    public int BookId { get; set; }

    [Required]
    public string Title { get; set; }

    public string CoverImage { get; set; } // Путь к обложке

    [Required]
    public string Author { get; set; }

    [Required]
    public int Year { get; set; }

    [Required]
    public string Genre { get; set; }

    public string Description { get; set; }

    public virtual List<Page> Pages { get; set; } // Список страниц
    public virtual List<FavoriteBooks> FavoriteBooks { get; set; } // Список избранных
}

public class User
{
    [Key]
    public string Email { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }

    public virtual List<FavoriteBooks> FavoriteBooks { get; set; } // Список избранных
}

public class Page
{
    [Key]
    public int PageId { get; set; }

    [Required]
    public int BookId { get; set; }

    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }

    [Required]
    public int PageNumber { get; set; }

    [Required]
    public string ImagePath { get; set; } // Путь к изображению страницы
}

public class FavoriteBooks
{
    [Key, Column(Order = 0)]
    public int BookId { get; set; }

    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }

    [Key, Column(Order = 1)]
    public string Email { get; set; }

    [ForeignKey("Email")]
    public virtual User User { get; set; }
}


/*using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

public class Book
{
    [Key]
    public int BookId { get; set; }

    [Required]
    public string Title { get; set; }

    public string ImageUrl { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public int Year { get; set; }

    [Required]
    public string Genre { get; set; }

    public string Description { get; set; }

    public string Text { get; set; }
}

public class User
{
    [Key]
    public string Email { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }

    *//* public int BookId { get; set; }
     [ForeignKey("BookId")]
     public virtual Book Book { get; set; }*//*

}

public class ReadingHistory
{
    [Key, Column(Order = 0)]
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }

    [Key, Column(Order = 1)]
    public string Email { get; set; }
    [ForeignKey("Email")]
    public virtual User User { get; set; }


    public int LastReadPage { get; set; }
}

public class FavoriteBooks
{

    [Key, Column(Order = 0)]
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }

    [Key, Column(Order = 1)]
    public string Email { get; set; }
    [ForeignKey("Email")]
    public virtual User User { get; set; }
}*/