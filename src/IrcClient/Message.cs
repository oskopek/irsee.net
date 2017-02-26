using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class Message : IMessage
    {
        public string RawMessage {
            get {
                string rawMessage = (Prefix == null ? "" : ":" + Prefix + " ") + Command.Show()
                    + Parameters.Take(Parameters.Count-1).Aggregate("", (acc, x) => acc + " " + x);
                if (Parameters.Count > 0)
                {
                    rawMessage += " :" + Parameters[Parameters.Count - 1];
                }
                return rawMessage;
            }
        }
        public string Prefix { get; }
        public IList<string> Parameters { get; }

        public string LastParameter {
            get
            {
                if (Parameters.Count <= 0)
                {
                    return null;
                } else
                {
                    return Parameters[Parameters.Count - 1];
                }
                
            }
        }

        public Command Command { get; }

        private Message(Command command, IList<string> parameters, string prefix = null)
        {
            Prefix = prefix;
            Command = command;
            if (parameters == null)
            {
                throw new ArgumentException("Parameters cannot be null");
            }
            Parameters = parameters;
        }

        public Message(string prefix, Command command, params string[] parameters)
            : this(command, new List<string>(parameters), prefix)
        {
            // intentionally empty
        }

        public Message(Command command, params string[] parameters) : this(null, command, parameters)
        {
            // intentionally empty
        }

        public static Message From(string rawMessage)
        {
            if (rawMessage == null)
            {
                throw new ArgumentException("Cannot parse null message.");
            } else if (rawMessage.Length == 0)
            {
                throw new ArgumentException("Cannot parse empty message.");
            }

            string prefix = null;
            if (rawMessage[0] == ':') // has prefix
            {
                int endPrefix = rawMessage.IndexOf(' ');
                prefix = rawMessage.Substring(1, endPrefix-1);
                rawMessage = rawMessage.Substring(endPrefix + 1).TrimStart(' ');
            }

            int endCommand = rawMessage.IndexOf(' ');
            if (endCommand < 0)
            {
                throw new ArgumentException("Cannot parse message without a command.");
            }
            string commandStr = rawMessage.Substring(0, endCommand);
            Command command;
            Enum.TryParse(commandStr, true, out command);
            rawMessage = rawMessage.Substring(endCommand).TrimStart(' ');

            int lastParamIndex = rawMessage.IndexOf(":");
            string lastParam = null;
            if (lastParamIndex >= 0)
            {
                lastParam = rawMessage.Substring(lastParamIndex + 1);
                rawMessage = rawMessage.Substring(0, lastParamIndex).TrimEnd(' ');
            }
            IList<string> parameters = new List<string>(rawMessage.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            if (lastParam != null)
            {
                parameters.Add(lastParam);
            }
            return new Message(command, parameters, prefix);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = obj as Message;
            return Prefix == other.Prefix && Command == other.Command && Parameters.SequenceEqual(other.Parameters);
        }

        public static bool operator ==(Message a, Message b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Message a, Message b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            int n = 1337;
            return (Prefix ?? "").GetHashCode() + Command.GetHashCode() + (from p in Parameters select p.GetHashCode() % n).Sum();
        }

    }
}
