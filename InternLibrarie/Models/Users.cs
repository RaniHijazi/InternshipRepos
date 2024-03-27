namespace InternshipLibrarie.Models
{
    public class Users
    {
        public int id { get; set; }
        public string username { get; set;}
        public string password { get; set; }
       
        public decimal total_fees_due { get; set; }
        public decimal total_books_borrowed { get; set; }

        public string Email { get; set; }
        public Boolean is_admin { get; set; }

       


        public ICollection<BorrowedBooks> BorrowedBooks { get; set; }

        public Subscriptions subscription {get;set;}

        
        
    }
}
