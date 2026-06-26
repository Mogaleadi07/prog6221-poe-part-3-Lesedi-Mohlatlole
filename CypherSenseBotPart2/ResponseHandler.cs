using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics.Eventing.Reader;

namespace CypherSenseBotPart2
{
    public class ResponseHandler
    {
        // five private fields
        private Memory memory;
        private Sentiment sentiment;
        private delegate string Chat(string message);
        private Chat botReply;
        private Dictionary<string, List<string>> responses;
        private Random random;
        private TaskAssistant taskAssistant;
        private Quiz cyberQuiz;
        private ActivityLog activityLog;


        // the responsehandler constructor that recieves the memory.
        public ResponseHandler(Memory mem, TaskAssistant taskAssist, Quiz quiz, ActivityLog log)
        {
            memory = mem;
            taskAssistant = taskAssist;
            cyberQuiz= quiz;
            activityLog= log;
            sentiment = new Sentiment();
            random = new Random();
            botReply = GenerateResponse;



            responses = new Dictionary<string, List<string>>()
            {
                {
                    "hello", new List<string>()
                    {
                        "Hello! How can I assist you with cybersecurity today?",
                        "Hi there! What cybersecurity topic would you like to learn about?",
                        "Greetings! I'm here to help you with any cybersecurity questions you may have."
                    }
                },
                {
                    "hi", new List<string>()
                    {
                        "Hi! How can I assist you with cybersecurity today?",
                        "Hello there! What cybersecurity topic would you like to learn about?",
                        "Greetings! I'm here to help you with any cybersecurity questions you may have."
                    }
                },
                {
                    "how are you", new List<string>()
                    {
                        "I'm doing well, thank you for asking! How can I assist you with cybersecurity today?",
                        "I'm here to help you with any cybersecurity questions you may have. What would you like to learn about?"
                    }
                },
                {
                    "who are you", new List<string>()
                    {
                        "I am CypherSense, your cybersecurity awareness chatbot. I'm here to help you learn about various cybersecurity topics and how to stay safe online.",
                        "I'm CypherSense, a chatbot designed to provide information and guidance on cybersecurity topics. How can I assist you today?"
                    }
                },
                {
                    "what is your purpose", new List<string>()
                    {
                        "My purpose is to educate and raise awareness about cybersecurity topics, helping you stay safe online.",
                        "I'm here to provide information and guidance on various cybersecurity topics, so you can make informed decisions and protect yourself online."
                    }
                },
                { "password", new List<string>()
                    {
                        "A strong password should be at least 12 characters long and include a mix of uppercase letters, lowercase letters, numbers, and special characters.",
                        "Avoid using common words or phrases in your passwords, as they can be easily guessed by attackers.",
                        "Consider using a passphrase, which is a sequence of words that is easy for you to remember but difficult for others to guess."
                    }
                },
                { "phishing", new List<string>()
                    {
                        "Phishing is a type of cyber attack where attackers impersonate legitimate organizations to trick individuals into providing sensitive information.",
                        "Be cautious of unsolicited emails or messages that ask for personal information or contain suspicious links.",
                        "Always verify the sender's email address and look for signs of phishing, such as poor grammar or urgent requests."
                    }
                },
                { "scam", new List<string>()
                    {
                        "Never share personal info with unknown people online.",
                        "Be skeptical of offers that seem too good to be true.",
                        "Verify the identity of anyone asking for sensitive information.",
                        "Report suspicious activity to the relevant authorities."
                    }
                },
                { "privacy", new List<string>()
                    {
                        "Protect your privacy  Only share information on trusted platforms.",
                        "Use strong, unique passwords for each of your accounts.",
                        "Regularly review your privacy settings on social media."
                    }
                },
                { "malware", new List<string>()
                    {
                        "Keep your software and operating system up to date to protect against malware.",
                        "Use reputable antivirus software and perform regular scans.",
                        "Be cautious when downloading files or clicking on links from unknown sources."
                    }
                },
                { "DDoS", new List<string>()
                    {
                        "DDoS attacks can overwhelm your network or website with traffic.",
                        "Implement rate limiting to prevent abuse.",
                        "Use a content delivery network (CDN) to distribute traffic."
                    }
                },
                 { "DoS", new List<string>()
                    {
                        "DoS attacks can overwhelm your network or website with traffic.",
                        "Implement rate limiting to prevent abuse.",
                        "Use a content delivery network (CDN) to distribute traffic."
                    }
                },
                  { "espionage", new List<string>()
                    {
                        "Espionage involves the secret gathering of information, often for political or military purposes.",
                        "Be cautious of unusual behavior or communications from individuals who may have access to sensitive information.",
                        "Report any suspicious activities to the appropriate authorities."
                    }
                },
                   { "virus", new List<string>()
                    {
                        "Virus attacks can spread malicious code to your system.",
                        "Use reputable antivirus software and perform regular scans.",
                        "Avoid downloading files or clicking on links from unknown sources."
                    }
                },
                    { "intellectual property", new List<string>()
                    {
                        "Intellectual property theft involves the unauthorized use or distribution of copyrighted material.",
                        "Respect the rights of content creators and only use materials that you have permission to use.",
                        "Report any instances of intellectual property theft to the appropriate authorities."
                    }
                },
                     { "social engineering", new List<string>()
                    {
                        "Social engineering attacks manipulate people into divulging confidential information.",
                        "Be cautious of unsolicited communications and verify the identity of anyone requesting sensitive information.",
                        "Report any suspicious activities to the appropriate authorities."
                    }
                },
                      { "internet threats", new List<string>()
                    {
                        "Internet threats can include various malicious activities targeting your online presence.",
                        "Use reputable security software and keep it updated.",
                        "Be cautious when sharing personal information online."
                    }
                },
            };
        }
        // USING NLP simulation to detect user intent for task management and reminders
        // a method that checks if the user wants to add a task
        private bool IsAddTaskRequest(string userInput)
        {
            string lowerInput = userInput.ToLower();
            return lowerInput.Contains("add task") || lowerInput.Contains("create task")||
             lowerInput.Contains("new task") || lowerInput.Contains("add a task");
        }

        // a method that checks if the user wants to delete a task
        private bool IsDeleteTaskRequest(string userInput)
        {
            string lowerInput = userInput.ToLower();
            return lowerInput.Contains("delete task") || lowerInput.Contains("remove task") ||
                   lowerInput.Contains("delete the task") || lowerInput.Contains("remove the task");
        }

        // a method that extracts the task title from user input
        private string ExtractTaskTitle(string userInput)
        {
            string lower= userInput.ToLower();
            if (lower.Contains("add task"))
            {
                int index = lower.IndexOf("add task") + 8;
                if (index < userInput.Length)
                {
                    return userInput.Substring(index).Trim();
                }
                return string.Empty;
            }
            if (lower.Contains("task"))
            {
                int index = lower.IndexOf("task") + 4;
                if (index < userInput.Length)
                {
                    return userInput.Substring(index).Trim();
                }
                return string.Empty;
            }
            return null;
        }

        // a method that extracts the task title for deletion
        private string ExtractTaskTitleForDelete(string userInput)
        {
            string lower = userInput.ToLower();
            if (lower.Contains("delete task"))
            {
                int index = lower.IndexOf("delete task") + 11;
                if (index < userInput.Length)
                {
                    return userInput.Substring(index).Trim();
                }
                return string.Empty;
            }
            if (lower.Contains("remove task"))
            {
                int index = lower.IndexOf("remove task") + 11;
                if (index < userInput.Length)
                {
                    return userInput.Substring(index).Trim();
                }
                return string.Empty;
            }
            if (lower.Contains("delete the task"))
            {
                int index = lower.IndexOf("delete the task") + 15;
                if (index < userInput.Length)
                {
                    return userInput.Substring(index).Trim();
                }
                return string.Empty;
            }
            if (lower.Contains("remove the task"))
            {
                int index = lower.IndexOf("remove the task") + 15;
                if (index < userInput.Length)
                {
                    return userInput.Substring(index).Trim();
                }
                return string.Empty;
            }
            return null;
        }
        // method that handles addition of tasks
        private string HandleAddTask(string userInput)
        {
            string taskTitle = ExtractTaskTitle(userInput);
            if (string.IsNullOrEmpty(taskTitle))
            {
                return "Please provide a title for the task you want to add.";
            }
           try
            {
                bool success = taskAssistant.AddTask(taskTitle,$"Task: {taskTitle}", DateTime.Now);
                if (success)
                {
                    activityLog.LogAction("Task Added", $"'{taskTitle}' - Saved to database");
                    return $"Task '{taskTitle}' has been added successfully!";
                }
                else
                {
                    return "Failed to add the task. Please try again.";
                }
            }
            catch (Exception ex)
            {
                return "An error occurred while adding the task. Please try again.";
            }
        }

        // method that handles deletion of tasks
        private string HandleDeleteTask(string userInput)
        {
            string taskTitle = ExtractTaskTitleForDelete(userInput);
            if (string.IsNullOrEmpty(taskTitle))
            {
                return "Please provide a title for the task you want to delete.";
            }
            try
            {
                bool success = taskAssistant.DeleteTask(taskTitle);
                if (success)
                {
                    activityLog.LogAction("Task Deleted", $"'{taskTitle}' - Removed from database");
                    return $"Task '{taskTitle}' has been deleted successfully!";
                }
                else
                {
                    return "Failed to delete the task. Please try again.";
                }
            }
            catch (Exception ex)
            {
                return "An error occurred while deleting the task. Please try again.";
            }
        }

        // reminder commands
        private bool IsReminderRequest(string userInput)
        {
            string lowerInput = userInput.ToLower();
            return lowerInput.Contains("remind me") || lowerInput.Contains("set a reminder") ||
                   lowerInput.Contains("reminder") || lowerInput.Contains("set reminder");
        }

        // handle reminder requests
        private string HandleReminder(string userInput)
        { 
            string lower = userInput.ToLower();
            DateTime reminderDate = DateTime.MinValue;
            if (lower.Contains("tomorrow"))
                reminderDate = DateTime.Now.AddDays(1);
            else if (lower.Contains("today"))
                reminderDate = DateTime.Now;
            else if (lower.Contains("3 days") || lower.Contains("three days"))
                reminderDate = DateTime.Now.AddDays(3);
            else if (lower.Contains("week") || lower.Contains("seven days"))
                reminderDate = DateTime.Now.AddDays(7);
            else
                return "I can set reminders for : today, tomorrow, in 3 days, next week.";

            activityLog.LogAction("Reminder Set", $"Set for {reminderDate:MMM dd, yyyy}");
            return $"Reminder set for {reminderDate:dddd, MMM dd, yyyy}";
        }
        private bool IsQuizRequest(string userInput)
        {
            string lower = userInput.ToLower();
            return lower.Contains("quiz") || lower.Contains("play") ||
                   lower.Contains("game") || lower.Contains("test");
        }

        private string HandleQuiz(string userInput)
        {
            if (IsQuizRequest(userInput))
            {
                if (!cyberQuiz.IsQuizCompleted())  
                {
                    activityLog.LogAction("Quiz Started", "User began the 15-question cybersecurity quiz");
                    return " Let's start the Cybersecurity Quiz!\n" + cyberQuiz.StartQuiz();
                }
            }

            // Otherwise, user is answering a question
            return cyberQuiz.SubmitAnswer(userInput);
        }
        // a method for the completation of tasks
        private bool IsCompleteTaskRequest(string userInput)
        {
            string lowerInput = userInput.ToLower();
            return lowerInput.Contains("complete task") || lowerInput.Contains("mark complete") ||
                   lowerInput.Contains("finished task") || lowerInput.Contains("done with");
        }
        //method that handles complete tasks
        private string HandleCompleteTask(string userInput)
        {
            string lower = userInput.ToLower();
            string taskTitle = null;

            if (lower.Contains("complete task"))
            {
                int index = lower.IndexOf("complete task") + 13;
                if (index < userInput.Length)
                {
                    taskTitle = userInput.Substring(index).Trim();
                }
            }
            else if (lower.Contains("done with"))
            {
                int index = lower.IndexOf("done with") + 9;
                if (index < userInput.Length)
                {
                    taskTitle = userInput.Substring(index).Trim();
                }
            }

            if (string.IsNullOrEmpty(taskTitle))
            {
                return "Please provide a task name to mark as complete.";
            }

            try
            {
                // Get all tasks and find the one matching the title
                List<Task1> allTasks = taskAssistant.GetAllTasks();
                Task1 targetTask = allTasks.FirstOrDefault(t => t.Title.ToLower().Contains(taskTitle.ToLower()) ||taskTitle.ToLower().Contains(t.Title.ToLower().Trim('-').Trim()));

                if (targetTask == null)
                {
                    return $"Task '{taskTitle}' not found. Please check the title.";
                }

                taskAssistant.CompleteTask(targetTask.Id);  // ← Pass the ID
                activityLog.LogAction("Task Completed", $"'{taskTitle}' - Marked as complete");
                return $"Task '{taskTitle}' has been marked as complete!";
            }
            catch (Exception ex)
            {
                return "Error completing task. Please try again.";
            }
        }
        // a method for the activity log
        private bool IsActivityLogRequest(string userInput)
        {
            string lower = userInput.ToLower();
            return lower.Contains("activity log") || lower.Contains("show log") ||
                   lower.Contains("log") || lower.Contains("history");
        }
        // method that handles the activity log
        private string HandleActivityLog()
        {
            activityLog.LogAction("Activity Log Viewed", "User requested activity log display");
            return activityLog.GetActivitySummary(10);
        }

        // a method that generates a response based on the user input and the topics in the responses dictionary.
        public string GenerateResponse(string userInput)
        {
            string lowerInput = userInput.ToLower();
            string userName = memory.getUserName();

            if (!cyberQuiz.IsQuizCompleted()) 
            {
                // Any single letter answer (A, B, C, D) = quiz answer
                if (userInput.Trim().Length == 1 && "ABCD".Contains(userInput.ToUpper()))
                {
                    return cyberQuiz.SubmitAnswer(userInput);
                }
            }
            if (IsQuizRequest(userInput))
            {
                return HandleQuiz(userInput);
            }

            if (IsActivityLogRequest(userInput))
            {
                return HandleActivityLog();
            }

            // Check for task commands FIRST
            if (IsAddTaskRequest(userInput))
            {
                return HandleAddTask(userInput);
            }

            if (IsDeleteTaskRequest(userInput))
            {
                return HandleDeleteTask(userInput);
            }
            if (IsCompleteTaskRequest(userInput))
            {
                return HandleCompleteTask(userInput);
            }

            if (IsReminderRequest(userInput))
            {
                return HandleReminder(userInput);
            }

            // Detect sentiment from user input
            string detectedSentiment = sentiment.DetectSentiment(userInput);

            var sortedTopics = responses.Keys.OrderByDescending(k => k.Length);
            // check if the user input contains any of the topics in the responses dictionary
            foreach (var topic in sortedTopics)
            {
                if (lowerInput.Contains(topic))
                {
                    memory.storeTopics(topic);
                    List<string> topicResponses = responses[topic];

                    int randomIndex = random.Next(topicResponses.Count);
                    string baseResponse = topicResponses[randomIndex];

                    string sentimentPrefix = sentiment.GetSentimentPrefix(detectedSentiment, topic, userInput);
                    string personalizedResponse = sentimentPrefix + "\n" + baseResponse;

                    // Get favorite topic from memory 
                    string favoriteTopic = memory.getFavoriteTopic();
                    if (!string.IsNullOrEmpty(favoriteTopic) && favoriteTopic == topic)
                    {
                        int topicCount = memory.GetTopicsTracker(topic);
                        if (topicCount >= 3)
                        {
                            personalizedResponse += $"\n\nI see this is your favorite topic, {userName}! You've asked about {topic} quite a bit!";
                        }
                    }
                    return personalizedResponse;
                }
            }
                return "I'm sorry, I don't have information on that topic. Please ask about another cybersecurity topic.";
            }

        // A public method that uses the delegate to process messages
        public string ProcessMessage(string userInput)
        {
            return botReply(userInput);
        }
    }
}


