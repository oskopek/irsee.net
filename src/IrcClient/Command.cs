using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public enum Command
    {
        PASS,
        NICK,
        USER,
        SERVER,
        OPER,
        QUIT,
        SQUIT,
        JOIN,
        PART,
        MODE,
        TOPIC,
        NAMES,
        LIST,
        INVITE,
        KICK,
        VERSION,
        STATS,
        LINKS,
        TIME,
        CONNECT,
        TRACE,
        ADMIN,
        INFO,
        PRIVMSG,
        NOTICE,
        WHO,
        WHOIS,
        WHOWAS,
        KILL,
        PING,
        PONG,
        ERROR,
        AWAY,
        REHASH,
        RESTART,
        SUMMON,
        USERS,
        WALLOPS,
        USERHOST,
        ISON
    }
}
