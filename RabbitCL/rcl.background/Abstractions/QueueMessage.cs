namespace rcl.Abstractions
{
    public class QueueMessage<T>
    {
        public QueueMessage(ulong deliveryTag, T payload)
        {
            DeliveryTag = deliveryTag;
            Payload = payload;
        }

        public ulong DeliveryTag { get; private set; }
        public T Payload { get; private set; }
    }
}
