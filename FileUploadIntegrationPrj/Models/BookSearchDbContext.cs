using Microsoft.EntityFrameworkCore;

namespace FileUploadIntegrationPrj.Models
{
    public class BookSearchDbContext : DbContext
    {
        public BookSearchDbContext(DbContextOptions<BookSearchDbContext> opt) : base(opt)
        {

        }

        public DbSet<Book>? Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>().HasData(
                new Book() { BookId = 1, BookTitle = "Principles for Dealing with the Changing World Order", BookLocationName = "Principles_for_Dealing_World_Order_Ray_Dalio.jpg", BookLocationPath = "", DatePublished = new DateTime(2021, 11, 30), BookAuthor = "Ray Dalio", BookGenre = "Business & Money" },
                new Book() { BookId = 2, BookTitle = "Business Secrets from the Bible", BookLocationName = "Business_Secrets_from_the_Bible_Rabbi_Daniel_Lapin.jpg", BookLocationPath = "", DatePublished = new DateTime(2014, 3, 3), BookAuthor = "Rabbi Daniel Lapin", BookGenre = "Self-help" },
                new Book() { BookId = 3, BookTitle = "How to Avoid a Climate Disaster", BookLocationName = "How_to_Avoid_a_Climate_Disaster_Bill_Gates.jpg", BookLocationPath = "", DatePublished = new DateTime(2021, 2, 16), BookAuthor = "Bill Gates", BookGenre = "History" },
                new Book() { BookId = 4, BookTitle = "Start with Why", BookLocationName = "Start_with_Why_Simon_Sinek.jpg", BookLocationPath = "", DatePublished = new DateTime(2009, 10, 29), BookAuthor = "Simon Sinek", BookGenre = "Business & Money" },
                new Book() { BookId = 5, BookTitle = "Think Again", BookLocationName = "Think_Again_Adam_Grant.jpg", BookLocationPath = "", DatePublished = new DateTime(2021, 2, 2), BookAuthor = "Adam Grant", BookGenre = "Business & Money" },
                new Book() { BookId = 6, BookTitle = "Atomic Habits", BookLocationName = "Atomic_Habits_James_Clear.jpg", BookLocationPath = "", DatePublished = new DateTime(2018, 10, 16), BookAuthor = "James Clear", BookGenre = "Health, Fitness & Dieting" },
                new Book() { BookId = 7, BookTitle = "A Promised Land", BookLocationName = "A_Promised_Land_Barack_Obama.jpg", BookLocationPath = "", DatePublished = new DateTime(2020, 11, 17), BookAuthor = "Barack Obama", BookGenre = "Biographies & Memoirs" },
                new Book() { BookId = 8, BookTitle = "Upstream", BookLocationName = "Upstream_Dan_Heath.jpg", BookLocationPath = "", DatePublished = new DateTime(2020, 11, 17), BookAuthor = "Dan Heath", BookGenre = "Business & Money" },
                new Book() { BookId = 9, BookTitle = "The Phoenix Project", BookLocationName = "The_Phoenix_Project_Gene_Kim.jpg", BookLocationPath = "", DatePublished = new DateTime(2013, 1, 10), BookAuthor = "Gene Kim", BookGenre = "Business & Money" },
                new Book() { BookId = 10, BookTitle = "Hit Refresh", BookLocationName = "Hit_Refresh_Satya_Nadella.jpg", BookLocationPath = "", DatePublished = new DateTime(2017, 9, 26), BookAuthor = "Satya Nadella", BookGenre = "Biographies & Memoirs" },
                new Book() { BookId = 11, BookTitle = "The Little Red Book of Selling", BookLocationName = "The_Little_Red_Book_of_Selling_Jeffrey_Gitomer.jpg", BookLocationPath = "", DatePublished = new DateTime(2004, 9, 25), BookAuthor = "Jeffrey Gitomer", BookGenre = "Business & Money" },
                new Book() { BookId = 12, BookTitle = "Principles: Life and Work", BookLocationName = "Principles_Life_and_Work_Ray_Dalio.jpg", BookLocationPath = "", DatePublished = new DateTime(2017, 9, 19), BookAuthor = "Ray Dalio", BookGenre = "Business & Money" },
                new Book() { BookId = 13, BookTitle = "Signature in the Cell", BookLocationName = "Signature_in_the_Cell_Stephen_Meyer.jpg", BookLocationPath = "", DatePublished = new DateTime(2009, 6, 23), BookAuthor = "Stephen_Meyer", BookGenre = "Christian Books & Bibles" },
                new Book() { BookId = 14, BookTitle = "Unlimited Memory", BookLocationName = "Unlimited_Memory_Kevin_Horsley.jpg",BookLocationPath = "", DatePublished = new DateTime(2021, 8, 13), BookAuthor = "Kevin Horsley", BookGenre = "Business & Money" },
                new Book() { BookId = 15, BookTitle = "Outliers", BookLocationName = "Outliers_The_Story_of_Success_Malcolm_Gladwell.jpg", BookLocationPath = "", DatePublished = new DateTime(2008, 11, 18), BookAuthor = "Malcolm Gladwell", BookGenre = "Business & Money" },
                new Book() { BookId = 16, BookTitle = "David and Goliath", BookLocationName = "David_and_Goliath_Malcolm_Gladwell.jpg", BookLocationPath = "", DatePublished = new DateTime(2013, 10, 15), BookAuthor = "Malcolm Gladwell", BookGenre = "Business & Money" },
                new Book() { BookId = 17, BookTitle = "Mindset", BookLocationName = "Mindset_The_New_Psychology_of_Success_Carol_S._Dweck.jpg", BookLocationPath = "", DatePublished = new DateTime(2006, 2, 28), BookAuthor = "Carol S. Dweck", BookGenre = "Health, Fitness & Dieting" },
                new Book() { BookId = 18, BookTitle = "Incognito", BookLocationName = "Incognito_The_Secret_Lives_of_the_Brain_David_Eagleman.jpg", BookLocationPath = "", DatePublished = new DateTime(2017, 9, 19), BookAuthor = "David Eagleman", BookGenre = "Health, Fitness & Dieting" },
                new Book() { BookId = 19, BookTitle = "Unshakeable", BookLocationName = "Unshakeable_Your_Financial_Freedom_Tony_Robbins.jpg", BookLocationPath = "", DatePublished = new DateTime(2017, 2, 28), BookAuthor = "Tony Robbins", BookGenre = "Business & Money" },
                new Book() { BookId = 20, BookTitle = "Life Force", BookLocationName = "Life_Force_Tony_Robbins.jpg", BookLocationPath = "", DatePublished = new DateTime(2022, 2, 8), BookAuthor = "Tony Robbins", BookGenre = "Health, Fitness & Dieting" }
                );
        }

        
    }
}
       