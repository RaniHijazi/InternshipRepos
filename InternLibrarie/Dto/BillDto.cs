namespace InternshipLibrarie.Dto
{
    public class BillDto
    {
        public int BorrowId { get; set; }
        public string UserName { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int TotalFeesDue { get; set; }

    }
}