namespace MyChat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;
    using System.Text;
    using MindLink.Recruitment.MyChat;
    using Newtonsoft.Json;
    using System.Text.RegularExpressions;
    using System.Linq;

    /// <summary>
    /// Represents a conversation exporter that can read a conversation and write it out in JSON.
    /// </summary>
    public class ConversationExporter //: IConversationExporter
    {

        /// <summary>
        /// Represents the configuration for the exporter.
        /// </summary>
        /// <remarks>
        /// Type ConversationExporterConfiguration.
        /// </remarks>
        private ConversationExporterConfiguration configuration;


        /// <summary>
        /// The application entry point. Using <paramref name="args"/> to create a ConversationExporter
        /// </summary>
        /// <param name="args">
        /// The command line arguments.
        /// </param>
        static void Main(string[] args)
        {
            //var n = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            ConversationExporter conv = new ConversationExporter(args);
        }


        /// <summary>
        /// The class constructor taking arguments and calling configuration.
        /// </summary>
        /// <param name="args">
        /// The command line arguments.
        /// </param>
        /// <exception cref="Exception">
        /// Catches the exception caught from other methods and displays the message to user.
        /// </exception>
        public ConversationExporter(string[] args)
        {
            try
            {
                Console.WriteLine("The following arguments can be used export a conversion. Argument 1 is the input file path, argument 2 output file path."
                    + "\nArguments past the second do these. To filter by user name to see those messages only. example to show bobs messages: \"filterUser-bob\""
                    + "\nTo filter by word to see those messages only. Example to show messages with word pie: \"filterWord-pie\""
                    + "\nTo black list words and replace those words with redacted. Example to blacklist words by comma delimiter: \"blackList-fuck,shit\""
                    + "\n\nPress any key to continue.");
                Console.ReadKey();

                configuration = new ConversationExporterConfiguration(args);
                this.ExportConversation(configuration.InputFilePath, configuration.OutputFilePath);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();

        }

        /// <summary>
        /// The class constructor for testing purpose
        /// </summary>
        public ConversationExporter()
        {

        }

        /// <summary>
        /// Exports the conversation at <paramref name="inputFilePath"/> as JSON to <paramref name="outputFilePath"/>.
        /// </summary>
        /// <param name="inputFilePath">
        /// The input file path.
        /// </param>
        /// <param name="outputFilePath">
        /// The output file path.
        /// </param>
        public void ExportConversation(string inputFilePath, string outputFilePath)
        {
            Conversation conversation = this.ReadConversation(inputFilePath);

            this.WriteConversation(conversation, outputFilePath);

            Console.WriteLine("Conversation exported from '{0}' to '{1}'", inputFilePath, outputFilePath);
        }

        /// <summary>
        /// Helper method to read the conversation from <paramref name="inputFilePath"/>. 
        /// Checks each line if filter for user is set and user id not in message then ignore.
        /// Checks each line if filter for word is set and word not in message then ignore.
        /// Checks each line if filter for black list word is set and replace message content with redacted for those words.
        /// If it gets to the end, it adds the messages and returns a converstation.
        /// </summary>
        /// <param name="inputFilePath">
        /// The input file path.
        /// </param>
        /// <returns>
        /// A <see cref="Conversation"/> model representing the conversation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the file was not found.
        /// </exception>
        /// <exception cref="IOException">
        /// Thrown when I/O error occurred.
        /// </exception>
        public Conversation ReadConversation(string inputFilePath)
        {
            try
            {
                List<Message> messages = new List<Message>();

                string conversationName = "";
                using (var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open, FileAccess.Read), Encoding.ASCII))
                {
                    conversationName = reader.ReadLine();
                    string line;

                    char[] fieldDelimiters = new char[] { ' ' };

                    Regex blacklistRegularExpression = null;
                    if (configuration.IsBlackListHasEntry())
                    {
                        blacklistRegularExpression = new Regex(String.Join("|", configuration.BlackList.Select(k => Regex.Escape(k))));
                    }

                    while ((line = reader.ReadLine()) != null)
                    {
                        Message message = ParseMessageLine(line, fieldDelimiters);

                        if (configuration.IsFilterUserSet() && !configuration.FilterUser.Equals(message.SenderId, StringComparison.InvariantCultureIgnoreCase))
                        {
                            continue;
                        }

                        if (configuration.IsFilterWordSet() && !message.Content.ToLower().Contains(configuration.FilterWord.ToLower()))
                        {
                            continue;
                        }

                        if (configuration.IsBlackListHasEntry())
                        {
                            message.Content = blacklistRegularExpression.Replace(message.Content, "*redacted*");
                        }

                        messages.Add(message);
                    }
                }

                return new Conversation(conversationName, messages);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("The file was not found.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("I/O error occurred.", ex);
            }
        }


        /// <summary>
        /// Helper method to read the line and split, convert timestamp to number and then create and return message.
        /// </summary>
        /// <param name="line">
        /// The line from intput file.
        /// </param>
        /// <param name="fieldDelimiters">
        /// The space delimiters.
        /// </param>
        /// /// <returns>
        /// A <see cref="Message"/> model representing the message.
        /// </returns>
        /// /// <exception cref="IndexOutOfRangeException">
        /// Thrown when one of arguments was empty.
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown when TimeStamp not in correct format.
        /// </exception>
        public Message ParseMessageLine(string line, char[] fieldDelimiters)
        {
            string[] split = line.Split(fieldDelimiters, 3);
            if (split.Length != 3)
            {
                throw new IndexOutOfRangeException("One of arguments was empty");
            }
            string timeStamp = split[0];
            string senderId = split[1];
            string content = split[2];

            Int64 parsedTimeStamp;
            if (!Int64.TryParse(timeStamp, out parsedTimeStamp))
            {
                throw new FormatException("TimeStamp not in correct format");
            }

            return new Message(DateTimeOffset.FromUnixTimeSeconds(parsedTimeStamp), senderId, content);
        }

        /// <summary>
        /// Helper method to write the <paramref name="conversation"/> as JSON to <paramref name="outputFilePath"/>.
        /// </summary>
        /// <param name="conversation">
        /// The conversation.
        /// </param>
        /// <param name="outputFilePath">
        /// The output file path.
        /// </param>
        /// <exception cref="SecurityException">
        /// Thrown when there is no permission to file <paramref name="outputFilePath"/>.
        /// </exception>
        public void WriteConversation(Conversation conversation, string outputFilePath)
        {
            try
            {
                using (var writer = new StreamWriter(new FileStream(outputFilePath, FileMode.Create, FileAccess.ReadWrite)))
                {

                    var serialized = JsonConvert.SerializeObject(conversation, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore

                    });

                    writer.Write(serialized);

                    writer.Flush();

                    writer.Close();
                }

            }
            catch (SecurityException ex)
            {
                throw new SecurityException("No permission to file.", ex);
            }
        }

    }
}
