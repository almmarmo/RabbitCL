namespace rcl.Configurations
{
    public static class UsageConfiguration
    {
        public static string USAGE { get => @"
RabbitCL
Usage:
    rcl configuration --host=<host> --port=<port> --user=<user> --pass=<pass> --ssl=<ssl>
    rcl consume       --queue=<queue> --ack=<ack> --out=<destinationFolder>
    rcl               (-h | --help)
    rcl --version
    rcl --config

Options:
    -h --help         Show this screen.
    --version         Show version.
    --config          Get current configuration settings.
    --host=HOST       Broker host connection
    --port=PORT       Broker port connection
    --user=USER       Broker username
    --pass=PASS       Broker password
    --ssl=SSL         Broker enable SSL
    --queue=QUEUE     Broker queue name
    --ack=ACK         Acknowledge message
    --out=OUTPUT      Output folder"; }
    }
}
