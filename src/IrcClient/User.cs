namespace Irsee.IrcClient
{
    public class User : Entity
    {
        public string Nickname { get; set; }
        public string Realname { get; set; }
        public string Username { get; set; }
        public string NickServUsername { get; set; }
        public string NickServPassword { get; set; }

        public User(string nickname, string username = null, string realname = null,
            string nickServUsername = null, string nickServPassword = null) : base(nickname) {
            Nickname = nickname;
            if (username == null) {
                username = nickname;
            }
            Username = username;
            if (realname == null) {
                realname = nickname;
            }
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
