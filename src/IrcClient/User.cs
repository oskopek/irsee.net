namespace Irsee.IrcClient
{
    public class User
    {
        public string Nickname { get; set; }
        public string Realname { get; set; }
        public string Username { get; set; }
        public string NickServUsername { get; set; }
        public string NickServPassword { get; set; }

        public User(string nickname, string nickServUsername = null,
            string nickServPassword = null, string username = null, string realname = null)  {
            if (username == null) {
                username = nickname;
            }
            if (realname == null) {
                realname = nickname;
            }
            Nickname = nickname;
            Username = username;
            Realname = realname;
            if (nickServUsername == null)
            {
                nickServUsername = nickname;
            }
            NickServUsername = nickServUsername;
            NickServPassword = nickServPassword;
        }
    }   
}
