using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace CypherSenseBotPart2
{
    public class Memory
    {
        private string userName;
        private List<string> topics;
        private Dictionary<string, int> topicsTracker;

        // the memory constructor
        public Memory()
        {
            userName = "";
            topics = new List<string>();
            topicsTracker = new Dictionary<string, int>();
        }
        //the set automatic property for username
        public void setUserName(string name)
        {
            userName = name.Trim();
        }

        // the get automatic property for username
        public string getUserName()
        {
            return userName;
        }

        // a method that stores topics the user discussed
        public void storeTopics(string topic)
        {
            if (!topics.Contains(topic))
            {
                topics.Add(topic);
                topicsTracker[topic] = 1;
            }
            else
            {
                topicsTracker[topic]++;
            }
        }

        // using a list to get all the topics discussed
        public List<string> getTopics()
        {
            return topics;
        }

        // a method that gets the most discussed topic
        public string getFavoriteTopic()
        {
            string favoriteTopic = ""; 
            int maxCount = 0;
            foreach (string topic in topicsTracker.Keys)
            {
                if (topicsTracker[topic] > maxCount)
                {
                    maxCount = topicsTracker[topic];
                    favoriteTopic = topic;
                }
            }
            return favoriteTopic;
        }

        public int GetTopicsTracker(string topic)
        {
            if (topicsTracker.ContainsKey(topic))
            {
                return topicsTracker[topic];
            }
            return 0;
        }
    }
}
    

