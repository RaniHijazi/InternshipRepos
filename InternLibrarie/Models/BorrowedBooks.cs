namespace InternshipLibrarie.Models
{
    public class BorrowedBooks
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int book_id { get; set; }
        public int overdue_fees { get; set; }
        public DateTime borrow_date{get;set;}
        public DateTime return_date { get; set; }
        public Users User { get; set; }
        public Books Book { get; set; }
    }
}
