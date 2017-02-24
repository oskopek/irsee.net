using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class PongHandler
    {
        public EventHandler<Message> Delegate()
        {
            return async (obj, e) => {
                var client = obj as IrcClient;
                await client.Servers[0].SendMessageAsync(new Message(Command.PONG, e.Parameters[0]));
            };
        }

        public EventHandler<Message> AutoResponder
        {
            get
            {
                return async (obj, e) =>
                {
                    var client = obj as IrcClient;
                    await client.Servers[0].SendMessageAsync(new Message(Command.PRIVMSG, e.Parameters[0], "Hello there!"));
                };
            }
        }

        public EventHandler<Message> CapSaslHandler
        {
            get
            {
                return async (obj, e) =>
                {
                    var remote = obj as RemoteServer;
                    if (e.Parameters.Count == 3 && e.Parameters[1] == "LS")
                    {

                    } else if (e.Parameters.Count == 3 && e.Parameters[1] == "ACK")
                    {
                        if (e.LastParameter.Contains("sasl"))
                        {
                            await remote.SendMessageAsync(new Message(Command.AUTHENTICATE, "PLAIN"));
                        }
                    } else if (e.Parameters.Count >= 2 && e.Parameters[1] == "NAK")
                    {
                        // try classic nickserv if SASL failed
                        await Console.Error.WriteLineAsync("SASL failed, trying classic NickServ auth.");
                        await remote.NickServAuthenticate();
                        return;
                    }

                    await Console.Error.WriteLineAsync($"DEBUG: Unhandled CAP message: \"{e}\""); 
                };
            }
        }

        public EventHandler<Message> AuthenticateSaslHandler
        {
            get
            {
                return async (obj, e) =>
                {
                    var remote = obj as RemoteServer;
                    if (e.Parameters.Count == 1 && e.Parameters[0] == "+")
                    {
                        IEnumerable<Message> authMessages = Base64AuthEncoder.Encode(
                            $":IDENTIFY {remote.Configuration.User.NickServUsername} {remote.Configuration.User.NickServPassword}");
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
