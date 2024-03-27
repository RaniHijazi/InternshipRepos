namespace InternshipLibrarie.Models
{
    public class Books
    {
        public int BooksId { get; set; } 
        public string title { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public int total_copies { get; set; }


        public ICollection<BorrowedBooks> BorrowedBooks { get; set; }


    }
}
