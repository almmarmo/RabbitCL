namespace Rcl.Broker
{
    public class QueueServiceGetOptions
    {
        public QueueServiceGetOptions()
        {
            this.AutoAcknowlodge = false;
        }

        public bool AutoAcknowlodge { get; set; }
    }
}