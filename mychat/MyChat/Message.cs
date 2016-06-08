namespace MindLink.Recruitment.MyChat
{
    using System;

    /// <summary>
    /// Represents a chat message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The message content.
        /// </summary>
        /// <remarks>
        /// Type string.
        /// </remarks>
        private string content;

        /// <summary>
        /// The message timestamp.
        /// </summary>
        /// <remarks>
        /// Type DateTimeOffset
        /// </remarks>
        private DateTimeOffset timestamp;

        /// <summary>
        /// The message sender.
        /// </summary>
        /// <remarks>
        /// Type string.
        /// </remarks>
        private string senderId;

        /// <summary>
        /// Automatic properties for content, to get and set
        /// </summary>
        /// <remarks>
        /// Type string
        /// </remarks>
        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        /// <summary>
        /// Automatic properties for TimeStamp, to get and set
        /// </summary>
        /// <remarks>
        /// Type DateTimeOffset
        /// </remarks>
        public DateTimeOffset Timestamp
        {
            get
            {
                return timestamp;
            }

            set
            {
                timestamp = value;
            }
        }

        /// <summary>
        /// Automatic properties for SenderId, to get and set
        /// </summary>
        /// <remarks>
        /// Type string
        /// </remarks>
        public string SenderId
        {
            get
            {
                return senderId;
            }

            set
            {
                senderId = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="timestamp">
        /// The message timestamp.
        /// </param>
        /// <param name="senderId">
        /// The ID of the sender.
        /// </param>
        /// <param name="content">
        /// The message content.
        /// </param>
        public Message(DateTimeOffset timestamp, string senderId, string content)
        {
            this.Timestamp = timestamp;
            this.SenderId = senderId;
            this.Content = content;
        }

    }
}
