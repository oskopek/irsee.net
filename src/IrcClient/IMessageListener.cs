namespace Irsee.IrcClient
{
    interface IMessageListener
    {
        void Accept(IMessage message);
    }   
}
