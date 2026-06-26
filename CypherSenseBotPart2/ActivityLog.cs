using System;
using System.Collections.Generic;
using System.Text;

namespace CypherSenseBotPart2
{
    public class ActivityLogger
    {
        //the activity logger checks the log entries by using the fields:
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }

        // constructor for the ActivityLogger with parameters
        public ActivityLogger(string action, string description, DateTime timeStamp)
        {
            Action = action;
            Description = description;
            TimeStamp = timeStamp;
        }
        // a method that overrides the ToString method 
        public override string ToString()
        {
            return $"[{TimeStamp:yyyy-MM-dd HH:mm:ss}] {Action}: {Description}";
        }
    }
    // class that will log activities.
    public class ActivityLog
    {
        private List<ActivityLogger> logEntries;
        private int maxEntries = 100;
        // constructor for activity logger
        public ActivityLog()
        {
            logEntries = new List<ActivityLogger>();
        }

        // a method that logs an activity
        public void LogAction(string action, string description)
        {
            ActivityLogger entry = new ActivityLogger(action, description, DateTime.Now);
            logEntries.Add(entry);

            if (logEntries.Count > maxEntries)
            {
                logEntries.RemoveAt(0);
            }
        }
        // a method that gets the recent actions
        public List<string> GetRecentActions(int count = 10)
        {
            List<string> recentActions = new List<string>();
            int startIndex = Math.Max(0, logEntries.Count - count);

            for (int i = startIndex; i < logEntries.Count; i++)
            {
                recentActions.Add(logEntries[i].ToString());
            }

            recentActions.Reverse();
            return recentActions;
        }
        // a method that gets all the actions
        public List<string> GetAllActions()
        {
            List<string> allActions = new List<string>();
            foreach (var entry in logEntries)
            {
                allActions.Add(entry.ToString());
            }
            allActions.Reverse();
            return allActions;
        }
        // clearing the log
        public void ClearLog()
        {
            logEntries.Clear();
        }
        //getting the total count
        public int GetActionCount()
        {
            return logEntries.Count;
        }
        //the summary of the log
        public string GetActivitySummary(int count = 10)
        {
            List<string> recentActions = GetRecentActions(count);
            if (recentActions.Count == 0)
                return "No activity has been logged yet.\n";
            string summary = $"Recent Activity ({recentActions.Count} actions):";
            summary += "=================================\n";
            // using a foreach loop to iterate through the recent actions and add them to the summary
            foreach (var action in recentActions)
            {
                summary += action + "\n";
            }

            summary += "═══════════════════════════════════════\n";
            summary += $"Total Logged Actions: {GetActionCount()}\n";

            return summary;
        }
        // a method that gets the type of actions
        public List<string> GetActionsByType(string actionType)
        {
            List<string> filteredActions = new List<string>();

            foreach (var entry in logEntries)
            {
                if (entry.Action.ToLower().Contains(actionType.ToLower()))
                {
                    filteredActions.Add(entry.ToString());
                }
            }
            filteredActions.Reverse();
            return filteredActions;
        }
        // method that gets actions from the last hour
        public List<string> GetActionsFromLastHours(int hours)
        {
            List<string> recentActions = new List<string>();
            DateTime cutoffTime = DateTime.Now.AddHours(-hours);

            foreach (var entry in logEntries)
            {
                if (entry.TimeStamp >= cutoffTime)
                {
                    recentActions.Add(entry.ToString());
                }
            }

            recentActions.Reverse();
            return recentActions;
        }
        // the full log of the actions
        public string ExportLog()
        {
            string export = "===========================\n";
            export += " CYPHERSENSEBOT ACTIVITY LOG\n";
            export += "===========================\n";
            export += $"Total Actions Logged: {GetActionCount()}\n";
            export += $"Export Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n\n ";
            export += "------------------------------------\n";

            List<string> allActions = GetAllActions();
            foreach (var action in allActions)
            {
                export += action + "\n";
            }
export += "------------------------------------\n";
            export += "End of Log\n";
            export += "===========================\n";
            return export;  
        }
    }
}
