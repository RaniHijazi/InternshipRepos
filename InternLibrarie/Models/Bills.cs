namespace InternshipLibrarie.Models
{
    public class Bills
    {
        public int BillsId { get; set; } 
        public int borrow_id { get; set; }  
        public BorrowedBooks BorrowedBook { get; set; }
    }
}
