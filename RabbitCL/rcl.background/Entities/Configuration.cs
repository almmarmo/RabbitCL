using Newtonsoft.Json;
using System;

namespace rcl.Entities
{
    public class Configuration
    {
        protected Configuration() { }
        public Configuration(string host, string port, string username, string password, string ssl)
        {
            SetHost(host);
            SetPort(port);
            SetUsername(username);
            SetPassword(password);
            SetSsl(ssl);
        }

        public string Host { get; private set; }
        public int Port { get; set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool Ssl { get; private set; }

        public void SetHost(string _host)
        {
            if (string.IsNullOrEmpty(_host))
                throw new Exception("HOST IS REQUIRED");

            Host = _host;
        }

        public void SetPort(string _port)
        {
            if (string.IsNullOrEmpty(_port))
                throw new Exception("PORT IS REQUIRED");

            Port = int.Parse(_port);
        }

        public void SetUsername(string _user)
        {
            if (string.IsNullOrEmpty(_user))
                throw new Exception("USERNAME IS REQUIRED");

            Username = _user;
        }

        public void SetPassword(string _pass)
        {
            if (string.IsNullOrEmpty(_pass))
                throw new Exception("PASSWORD IS REQUIRED");

            Password = _pass;
        }

        public void SetSsl(string _ssl)
        {
            bool result;
            if (string.IsNullOrEmpty(_ssl) && !bool.TryParse(_ssl, out result))
                throw new Exception("SSL INFORMATION IS REQUIRED");

            Ssl = bool.Parse(_ssl);
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
