namespace MindLink.Recruitment.MyChat
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the configuration for the exporter.
    /// </summary>
    public class ConversationExporterConfiguration
    {
        /// <summary>
        /// The input file path.
        /// </summary>
        /// <remarks>
        /// Type string.
        /// </remarks>
        private string inputFilePath;

        /// <summary>
        /// The output file path.
        /// </summary>
        /// <remarks>
        /// Type string.
        /// </remarks>
        private string outputFilePath;

        /// <summary>
        /// Which user to filter message to be shown.
        /// </summary>
        /// <remarks>
        /// Type string.
        /// </remarks>
        private string filterUser = "";

        /// <summary>
        /// Which word to filter message to be shown.
        /// </summary>
        /// <remarks>
        /// Type string.
        /// </remarks>
        private string filterWord = "";

        /// <summary>
        /// A list containing the black list words
        /// </summary>
        /// <remarks>
        /// Type List string.
        /// </remarks>
        private List<string> blackList;

        /// <summary>
        /// Automatic properties for inputFilePath, to get and set
        /// </summary>
        /// <remarks>
        /// Type string
        /// </remarks>
        public string InputFilePath
        {
            get
            {
                return inputFilePath;
            }

            set
            {
                inputFilePath = value;
            }
        }

        /// <summary>
        /// Automatic properties for outputFilePath, to get and set
        /// </summary>
        /// <remarks>
        /// Type string
        /// </remarks>
        public string OutputFilePath
        {
            get
            {
                return outputFilePath;
            }

            set
            {
                outputFilePath = value;
            }
        }

        /// <summary>
        /// Automatic properties for filterUser, to get and set
        /// </summary>
        /// <remarks>
        /// Type string
        /// </remarks>
        public string FilterUser
        {
            get
            {
                return filterUser;
            }

            set
            {
                filterUser = value;
            }
        }

        /// <summary>
        /// Automatic properties for filterWord, to get and set
        /// </summary>
        /// <remarks>
        /// Type string
        /// </remarks>
        public string FilterWord
        {
            get
            {
                return filterWord;
            }

            set
            {
                filterWord = value;
            }
        }

        /// <summary>
        /// Automatic properties for blackList, to get and set
        /// </summary>
        /// <remarks>
        /// Type List string.
        /// </remarks>
        public List<string> BlackList
        {
            get
            {
                return blackList;
            }

            set
            {
                blackList = value;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationExporterConfiguration"/> class.
        /// </summary>
        /// <param name="arg">
        /// A array string which is expected for [0] to be input file, [1] to be output file, filter user/word/ messsages to be shown and black list words from message starting from arg[2]
        /// </param>
        /// <exception cref="IndexOutOfRangeException">
        /// Accessing an array data that is not there.
        /// </exception>
        public ConversationExporterConfiguration(string[] arg)
        {
            try
            {
                this.InputFilePath = arg[0];
                this.OutputFilePath = arg[1];
                if (arg.Length > 2)
                {
                    for (int i = 2; i < arg.Length; ++i)
                    {
                        if (arg[i].StartsWith("filterUser-", StringComparison.InvariantCultureIgnoreCase))
                        {
                            FilterUser = arg[i].Substring(11);
                        }
                        else if (arg[i].StartsWith("filterWord-", StringComparison.InvariantCultureIgnoreCase))
                        {
                            FilterWord = arg[i].Substring(11);
                        }
                        else if (arg[i].StartsWith("blackList-", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string[] words = arg[i].Substring(10).Split(',');
                            blackList = new List<string>(words);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException("One of arguments was empty. Please check your arguments.", ex);
            }

        }

        /// <summary>
        /// Returns true or false, checking if filter user has been set by checking if it is not null or empty
        /// </summary>
        /// <remarks>
        /// Type bool.
        /// </remarks>
        public bool IsFilterUserSet()
        {
            return !String.IsNullOrEmpty(FilterUser);
        }

        /// <summary>
        /// Returns true or false, checking if filter word has been set by checking if it is not null or empty
        /// </summary>
        /// <remarks>
        /// Type bool.
        /// </remarks>
        public bool IsFilterWordSet()
        {
            return !String.IsNullOrEmpty(FilterWord);
        }

        /// <summary>
        /// Returns true or false, checking if black list has been set by checking if the list not null or empty
        /// </summary>
        /// <remarks>
        /// Type bool.
        /// </remarks>
        public bool IsBlackListHasEntry()
        {
            return blackList != null && blackList.Count != 0;
        }
    }
}
