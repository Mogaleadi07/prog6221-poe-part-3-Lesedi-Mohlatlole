using System;
using System.Collections.Generic;
using System.Text;

namespace CypherSenseBotPart2
{
    public class Sentiment
    {
        private Dictionary<string, string> sentimentKeywords;
        private Dictionary<string, string> sentimentTemplate;
        private string currentUserInput;

        // sentiment constructor
        public Sentiment()
        {
            //a dictionary to store the sentiment keywords.
            sentimentKeywords = new Dictionary<string, string>
            {
               { "worried", "negative" },
                { "scared", "negative" },
                { "afraid", "negative" },
                { "concerned", "negative" },
                { "stressed", "negative" },
                { "excited", "positive" },
                { "happy", "positive" },
                { "interested", "positive" },
                { "curious", "positive" },
                { "keen", "positive" }
            };

            //sentiment template that uses a {issue} placeholder.
            sentimentTemplate = new Dictionary<string, string>
            {
                { "negative", "I understand that you are {emotion} about {issue}. It's important to remember that cybersecurity can be challenging, but I'm here to help you navigate through it." },
                { "positive", "It's great to hear that you're feeling {emotion} about {issue}! Your positive attitude will definitely help you in learning about cybersecurity." },
                { "neutral", " " }
            };
        }

        public void SetUserInput(string input)
        {
            currentUserInput = input;
        }

        // method that detects sentiment from the user
        public string DetectSentiment(string userInput)
        {
            string lowerInput = userInput.ToLower();

            foreach (string keyword in sentimentKeywords.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    return sentimentKeywords[keyword];
                }
            }

            return "neutral";  // returns to neutral if there isno sentiment detected
        }

        // method  that gets the emotional word from user
        public string GetEmotionKeyword(string userInput)
        {
            string lowerInput = userInput.ToLower();

            foreach (string keyword in sentimentKeywords.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    return keyword;
                }
            }

            return "";
        }

        // Getting the sentiment-based prefix for a response
        public string GetSentimentPrefix(string sentiment, string cybersecurityIssue, string userInput)
        {
            if (sentimentTemplate.ContainsKey(sentiment))
            {
                string template = sentimentTemplate[sentiment];
                template = template.Replace("{issue}", cybersecurityIssue);
                string emotionKeyword = GetEmotionKeyword(userInput);
                if (!string.IsNullOrEmpty(emotionKeyword))
                {
                    template = template.Replace("{emotion}", emotionKeyword);
                }
                else
                {
                    template = template.Replace("{emotion}", "feeling");
                }

                return template;  
            }

            return sentimentTemplate["neutral"].Replace("{issue}", cybersecurityIssue);

        }
    }
}
