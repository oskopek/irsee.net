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
                string rawMessage = (Prefix == null ? "" : ":" + Prefix + " ") + Command
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

        public Command Command { get; }

        public Message(Command Command, IList<string> Parameters, string Prefix = null)
        {
            this.Prefix = Prefix;
            this.Command = Command;
            if (Parameters == null)
            {
                throw new ArgumentException("Parameters cannot be null");
            }
            this.Parameters = Parameters;
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
            Command.TryParse(commandStr, true, out command);
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
            return (Prefix ?? "").GetHashCode() + Command.GetHashCode() + (from p in Parameters select p.GetHashCode()).Sum();
        }

    }
}
