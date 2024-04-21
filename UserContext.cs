using System.Data.Entity;

namespace Десктоп_РПМ
{
    class UserContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        //public DbSet<ReadingHistory> ReadingHistory { get; set; }
        public DbSet<FavoriteBooks> FavoriteBooks { get; set; }

        private int? language;

        public int Language
        {
            get
            {
                return language.HasValue ? language.Value : 1049; //1049 - русский язык
            }
            set
            {
                language = value;
            }
        }
    }
}