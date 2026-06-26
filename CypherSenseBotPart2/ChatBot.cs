using System;
using System.Collections.Generic;
using System.Text;

namespace CypherSenseBotPart2
{
    public class ChatBot
    {
        private Memory memory;
        private ResponseHandler responseHandler;
        private string currentUserName = ""; 
        private TaskAssistant taskAssitant;
        private Quiz quiz;
        private ActivityLog activityLog;



        // chatbot constructor
        public ChatBot(TaskAssistant taskAssistant,Quiz quizInstance, ActivityLog log)
        {
            taskAssistant = taskAssistant;
            quiz = quizInstance;
            memory = new Memory();
            responseHandler = new ResponseHandler(memory, taskAssistant, quiz, log);
        }

        //the chatbot method that greets the user
        public string GetGreeting()
        {
            return "Hello! I am CypherSense, your cybersecurity awareness chatbot. What is your name?";
        }

        // a method that checks if name is empty
        private bool isEmpty(string name)
        {
            return string.IsNullOrEmpty(name) ||string.IsNullOrWhiteSpace(name);
        }

        // method that processes the user message and returns a response
        public string ProcessMessage(string userInput)
        {
            // checks if the user entered their name.
            if (string.IsNullOrEmpty(currentUserName))
            {
                string trimmedInput = userInput.Trim();

                // validating the name 
                if (isEmpty(trimmedInput))
                {
                    return "Please enter a valid name.";
                }
                
                currentUserName = userInput.Trim();
                memory.setUserName(currentUserName);
                return $"Nice to meet you, {currentUserName}! Welcome to the cybersecurity awareness chatbot.\nYou can ask me questions about cybersecurity topics like passwords, phishing, scams, privacy, malware, and more. Type 'exit' to end the conversation.";
            }

            // If user chooses to exit the conversation
            if (userInput.ToLower() == "exit")
            {
                return "Goodbye! Stay safe online!";
            }

            // using ResponseHandler to get the responses.
            string response = responseHandler.ProcessMessage(userInput);
            return response;
        }

        public string GetUserName()
        {
            return currentUserName;
        }

        // Checks if the conversation must continue
        public bool IsConversationActive()
        {
            return !currentUserName.ToLower().Equals("exit");
        }
    }
}
