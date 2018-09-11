namespace Rcl.Broker
{
    public class QueueMessage
    {
        public QueueMessage(ulong id, string payload)
        {
            Id = id;
            Payload = payload;
        }

        public ulong Id { get; private set; }
        public string Payload { get; private set; }
    }
}