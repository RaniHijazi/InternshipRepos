namespace InternshipLibrarie.Models
{
    public class Subscriptions
    {
        public int SubscriptionsId { get; set; }   
        public string subscription_name { get; set; } 
        public DateTime subscription_expiry_date { get; set; }
        public int user_id { get; set; }
        public Users User { get; set; }

        

    }
}
