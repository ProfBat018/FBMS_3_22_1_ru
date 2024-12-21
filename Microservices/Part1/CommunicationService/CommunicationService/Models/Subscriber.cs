namespace CommunicationService.Models
{
    public class Subscriber
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string OwnerUsername { get; set; } 
        public string SubscriberUsername { get; set; } 
    }
}
