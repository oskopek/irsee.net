using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class PongHandler
    {
        public static EventHandler<Message> PingAutoResponder
        {
            get
            {
                return async (obj, e) =>
                {
                    var server = obj as RemoteServer;
                    await server.SendMessageAsync(new Message(Command.PONG, e.Parameters[0]));
                };
            }
        }

        public static EventHandler<Message> AutoResponder
        {
            get
            {
                return async (obj, e) =>
                {
                    var server = obj as RemoteServer;
                    await server.SendMessageAsync(new Message(Command.PRIVMSG, e.Parameters[0], "Hello there!"));
                };
            }
        }

        private static async Task TryClassicNickServ(RemoteServer remote)
        {
            // try classic nickserv if SASL failed
            await Console.Error.WriteLineAsync("SASL failed, trying classic NickServ auth.");
            await remote.NickServAuthenticate();
            return;
        }

        public static EventHandler<Message> UniqueNickErrorHandler
        {
            get
            {
                return async (obj, e) =>
                {
                    var server = obj as RemoteServer;
                    server.Configuration.User.Nickname += "-1";
                    await server.SendMessageAsync(new Message(Command.NICK, server.Configuration.User.Nickname));
                };
            }
        }

        public static EventHandler<Message> CapSaslHandler
        {
            get
            {
                return async (obj, e) =>
                {
                    var server = obj as RemoteServer;
                    if (e.Parameters.Count == 3 && e.Parameters[1] == "LS")
                    {
                        string[] available = e.LastParameter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string sasl = (from key in available where key.StartsWith("sasl") select key).FirstOrDefault();
                        if (sasl == default(string))
                        {
                            await TryClassicNickServ(server);
                            return;
                        } else if (sasl.Contains("="))
                        {
                            string[] keyVal = sasl.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                            if (keyVal.Length != 2)
                            {
                                string[] types = keyVal[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if ((from type in types where type == "PLAIN" select type).Any())
                                {
                                    await server.SendMessageAsync(new Message(Command.CAP, "REQ", "sasl"));
                                    return;
                                }
                            }
                            await TryClassicNickServ(server);
                            return;
                        } else
                        {
                            await server.SendMessageAsync(new Message(Command.CAP, "REQ", "sasl"));
                        }
                        return;
                    } else if (e.Parameters.Count == 3 && e.Parameters[1] == "ACK")
                    {
                        if (e.LastParameter.Contains("sasl"))
                        {
                            await server.SendMessageAsync(new Message(Command.AUTHENTICATE, "PLAIN"));
                        }
                        return;
                    } else if (e.Parameters.Count >= 2 && e.Parameters[1] == "NAK")
                    {
                        await TryClassicNickServ(server);
                        return;
                    }

                    await Console.Error.WriteLineAsync($"DEBUG: Unhandled CAP message: \"{e}\""); 
                };
            }
        }

        public static EventHandler<Message> AuthenticateSaslHandler
        {
            get
            {
                return async (obj, e) =>
                {
                    var remote = obj as RemoteServer;
                    if (e.Parameters.Count == 1 && e.Parameters[0] == "+")
                    {
                        IEnumerable<Message> authMessages = Base64AuthEncoder.Encode(
                            $"{remote.Configuration.User.NickServUsername}\0{remote.Configuration.User.NickServUsername}\0{remote.Configuration.User.NickServPassword}");
                        foreach (var msg in authMessages)
                        {
                            await remote.SendMessageAsync(msg);
                        }
                        return;
                    }

                    await Console.Error.WriteLineAsync($"DEBUG: Unhandled AUTHENTICATE message: \"{e}\"");
                };
            }
        }
    }
}
