using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.Data.SqlClient.Internal.SqlClientEventSource;

namespace CypherSenseBotPart2
{
    // method for the task1 database
    public class Task1
    {
        // the task properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        //the task1 constructor
        public Task1()
        { 
        }
        //the task1 constructor with parameters
        public Task1(int id, string title, string description, DateTime reminderDate, bool isCompleted)
        {
            Id = id;
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
            IsCompleted = isCompleted;
        }

        //overriding the ToString method to display the task information
        public override string ToString()
        {
            string status = IsCompleted ? "Completed" : "Pending";
            string reminder = ReminderDate.ToString("MM/dd/yyyy HH:mm");
            return $"[{status}] {Title} (Reminder: {reminder})";
        }
    }
    
    public class TaskAssistant
    {
        // a connection string to the database
        private string connectionString = @"server=(localdb)\MSSQLLocalDB; Database=Task1;Trusted_Connection=True;";
    

        // the task assistant constructor
        public TaskAssistant()
        {
            IntializeDatabase();
        }

        private void IntializeDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string createTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaskHelper]') AND type in (N'U'))
                    BEGIN
                        CREATE TABLE [dbo].[TaskHelper] (
                            [Id] INT PRIMARY KEY IDENTITY(1,1),
                            [Title] NVARCHAR(270) NOT NULL,
                            [Description] NVARCHAR(500) NOT NULL,
                            [ReminderDate] DATETIME NULL,
                            [IsCompleted] BIT NOT NULL DEFAULT 0
                        );
                    END";

                    using (SqlCommand cmd = new SqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database initialization error: {ex.Message}");
            }
        }

        // method to get all tasks from the database
        public List<Task1> GetAllTasks()
        {
            List<Task1> tasks = new List<Task1>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Id, Title, Description, ReminderDate, IsCompleted FROM [dbo].[TaskHelper]";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Task1 task = new Task1(
                                    (int)reader["Id"],
                                    (string)reader["Title"],
                                    (string)reader["Description"],
                                    (DateTime)reader["ReminderDate"],
                                    (bool)reader["IsCompleted"]
                                );
                                tasks.Add(task);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving tasks: {ex.Message}");
                }
            }
            return tasks;
        }

        // method to add a new task to the database
        public bool AddTask(string title, string description, DateTime reminderDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO [dbo].[TaskHelper] (Title, Description, ReminderDate, IsCompleted) VALUES (@title, @description, @reminderDate, 0)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@reminderDate", reminderDate);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding task: {ex.Message}");
                    return false;
                }
            }
        }

        // method to mark a task as completed
        public bool CompleteTask(int taskId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE [dbo].[TaskHelper] SET IsCompleted = 1 WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", taskId);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error completing task: {ex.Message}");
                    return false;
                }
            }
        }

        // method to delete a task from the database
        public bool DeleteTask(string titleTask)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM [dbo].[TaskHelper] WHERE Title = @TaskTitle";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TaskTitle", titleTask);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting task: {ex.Message}");
                    return false;
                }
            }
        }

        // method to update a task in the database
        public bool UpdateTask(int id, string title, string description, DateTime reminderDate, bool isCompleted)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE [dbo].[TaskHelper] SET Title = @title, Description = @description, ReminderDate = @reminderDate, IsCompleted = @isCompleted WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@reminderDate", reminderDate);
                        cmd.Parameters.AddWithValue("@isCompleted", isCompleted);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating task: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
        
    

