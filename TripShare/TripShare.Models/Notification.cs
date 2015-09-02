namespace TripShare.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string RecieverId { get; set; }

        public virtual User Reciever { get; set; }

        public NotificationType Type { get; set; }
    }
}
