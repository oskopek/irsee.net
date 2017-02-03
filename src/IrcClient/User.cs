namespace Irsee.IrcClient
{
    public class User
    {
        public User(string nickname, string username = null, string realname = null)  {
            if (username == null) {
                username = nickname;
            }
            if (realname == null) {
                realname = nickname;
            }
            this.Nickname = nickname;
            this.Username = username;
            this.Realname = realname;
        }

        public string Nickname { get; private set; }
        public string Realname { get; private set; }
        public string Username { get; private set; }

    }   
}
