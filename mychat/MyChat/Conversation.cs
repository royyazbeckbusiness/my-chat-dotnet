namespace MindLink.Recruitment.MyChat
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the model of a conversation.
    /// </summary>
    public class Conversation
    {


        /// <summary>
        /// The name of the conversation.
        /// </summary>
        /// DateTimeOffset
        private string name;

        /// <summary>
        /// The messages in the conversation.
        /// </summary>
        /// <remarks>
        /// Type List Message.
        /// </remarks>
        private List<Message> messages;


        /// <summary>
        /// Automatic properties for name, to get and set
        /// </summary>
        /// <remarks>
        /// Type string
        /// </remarks>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }


        /// <summary>
        /// Automatic properties for messages, to get and set
        /// </summary>
        /// <remarks>
        /// Type List of Message
        /// </remarks>
        public List<Message> Messages
        {
            get
            {
                return messages;
            }

            set
            {
                messages = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Conversation"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the conversation.
        /// </param>
        /// <param name="messages">
        /// The messages in the conversation.
        /// </param>
        public Conversation(string name, List<Message> messages)
        {
            this.Name = name;
            this.Messages = messages;
        }


        /// <summary>
        /// Add a new message to the list of Message using <paramref name="timeStamp"/>, <paramref name="senderId"/>, <paramref name="content"/>
        /// </summary>
        /// <param name="timeStamp">
        /// An integer for the timeStamp
        /// </param>
        /// <param name="senderId">
        /// A string of the senders id
        /// </param>
        /// <param name="content">
        /// A string of the content of the message
        /// </param>
        public void AddMessage(Int64 timeStamp, string senderId, string content)
        {
            Messages.Add(new Message(DateTimeOffset.FromUnixTimeSeconds(timeStamp), senderId, content));
        }


        /// <summary>
        /// Overload - Add a new message to the list of Message using <paramref name="message"/>
        /// </summary>
        /// <param name="message">
        /// A Message object given
        /// </param>
        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }

    }
}
