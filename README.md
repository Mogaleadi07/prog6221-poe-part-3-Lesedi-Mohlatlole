CypherSenseBot

PROJECT NAME
CypherSenseBot - An AI-Powered Chatbot with Integrated Task Management and Cybersecurity Learning

BRIEF DESCRIPTION
CypherSenseBot is an intelligent Windows-based chatbot application designed to assist users with conversational AI, task management, cybersecurity knowledge, and learning through interactive quizzes. The bot combines Natural Language Processing (NLP) simulation, sentiment analysis, and a user-friendly interface to provide an engaging and productive experience.

Key Features:
- Conversational AI Interface with natural language processing
- Task Assistant for creating, managing, and tracking tasks
- Cybersecurity Quiz/Mini-Game for educational learning
- Activity Logging to track all interactions
- Sentiment Analysis to understand user emotions
- Voice Greeting with audio welcome message
- ASCII Art Display for themed presentation
- SQL Database Integration for persistent storage

HOW TO OPEN AND RUN THE PROJECT

Prerequisites:
- Visual Studio 2022 or later
- .NET 10.0 (or compatible framework)
- SQL Server (Local or Express Edition)
- Windows OS (Windows 10/11 recommended)

Opening the Project:
1. Navigate to the folder containing CypherSenseBotPart2.slnx or CypherSenseBotPart2.csproj
2. Launch Visual Studio
3. Click "Open a project or solution"
4. Browse to the project directory
5. Select CypherSenseBotPart2.slnx file and click Open
6. Go to Build > Build Solution (or press Ctrl+Shift+B)
7. Press F5 or click Debug > Start Debugging
8. The application window will open with ASCII art and greeting message

Alternative - Run from Command Line:
cd CypherSenseBotPart2
dotnet run

SOFTWARE REQUIRED

Core Requirements:
- Visual Studio 2022 or Visual Studio Code with C# extensions
- .NET 10.0 SDK
- SQL Server 2019 or SQL Express/LocalDB
- Windows Forms (included with .NET)

NuGet Package Dependencies:
- Microsoft.Data.SqlClient v7.0.1 (for SQL Server connectivity)

Additional Software:
- SQL Server Management Studio (SSMS) - Optional, for database administration
- Audio Output - Required for voice greeting feature

DATABASE SETUP INSTRUCTIONS

Step 1: Create the Database
1. Open SQL Server Management Studio (SSMS)
2. Connect to your local SQL Server instance
3. Right-click on "Databases" and select "New Database"
4. Database Name: Task1 (or your preferred name)
5. Click OK

Step 2: Create the TaskHelper Table
1. Open a New Query Window in SSMS
2. Execute the following SQL script:

CREATE TABLE [dbo].[TaskHelper]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(250) NOT NULL, 
    [Description] NVARCHAR(500) NOT NULL, 
    [ReminderDate] DATETIME NULL, 
    [IsCompleted] BIT NOT NULL DEFAULT 0
)

Step 3: Create the ActivityLog Table
1. Execute the following SQL script:

CREATE TABLE [dbo].[ActivityLog]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Action] NVARCHAR(250) NOT NULL,
    [Details] NVARCHAR(500) NOT NULL,
    [Timestamp] DATETIME NOT NULL DEFAULT GETDATE()
)

Step 4: Update Connection String
1. Locate the ChatBot.cs file in the CypherSenseBotPart2 project
2. Update the connection string if needed:

string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=Task1;Integrated Security=true;Encrypt=false;";

Replace YOUR_SERVER_NAME with:
- (LocalDB)\MSSQLLocalDB for LocalDB
- . or localhost for local SQL Server
- Your actual server name for remote servers

3. Save and rebuild the solution

HOW TO USE THE TASK ASSISTANT

Accessing Tasks:
1. Click the "Refresh Tasks" button to load all existing tasks from the database
2. Tasks will appear in the DataGridView on the right side of the application

Creating a New Task:
1. Type a message like: "Create task: [Task Title]"
2. The bot will prompt for a description and reminder date
3. The task will be added to the database automatically

Viewing Tasks:
1. Click "Refresh Tasks" button to reload the task list from the database
2. Tasks display with columns: Id, Title, Description, ReminderDate, IsCompleted

Deleting Tasks:
1. Select a task from the DataGridView (click on any row)
2. Click the "Delete Task" button
3. The task will be removed from the database and the list will refresh

Marking Tasks as Complete:
1. Use the task management interface or send a message like: "Mark [Task Title] as complete"

HOW TO ACCESS THE QUIZ/MINI-GAME

Starting the Quiz:
1. Type a message containing keywords like: "Quiz", "Test", "Challenge", or "Game"
2. Example messages:
   - "Let's start a quiz"
   - "I want to take a cybersecurity test"
   - "Can you challenge me with a game?"

Quiz Features:
- Multiple-Choice Questions to test cybersecurity knowledge
- Instant Feedback with correct/incorrect answers and explanations
- Score Tracking to record your quiz performance
- Difficulty Levels ranging from basic to advanced

Sample Quiz Questions:
- Basic cybersecurity concepts
- Common threats and vulnerabilities
- Security best practices
- Password protection strategies
- Data encryption fundamentals
- Network security basics

Viewing Quiz Results:
1. Your quiz scores are logged in the Activity Log
2. Check the Activity Log to review your performance history

HOW TO TEST THE NLP SIMULATION

Natural Language Processing Features:

CypherSenseBot includes simulated NLP capabilities that allow it to understand and respond to user inputs naturally.

Testing NLP Capabilities:

Example 1: Task Creation (NLP recognizes intent)
User: "I need to create a new task"
User: "Can you help me add a reminder?"
User: "Let's add something to my to-do list"

Example 2: Greetings and Casual Chat
User: "Hello"
User: "How are you?"
User: "What's your name?"

Example 3: Quiz Requests
User: "Let's play a game"
User: "I want to learn about cybersecurity"
User: "Can you quiz me?"

Example 4: Help Requests
User: "Help"
User: "What can you do?"
User: "Show me available commands"

NLP Processing Steps:
1. Input Tokenization - Bot parses user messages
2. Intent Recognition - Identifies what the user wants
3. Context Understanding - Considers conversation history
4. Response Generation - Generates appropriate replies
5. Action Execution - Performs requested tasks

Sentiment Analysis:
The bot also analyzes the sentiment of user messages:
- Positive Sentiment - Responds encouragingly
- Negative Sentiment - Provides supportive responses
- Neutral Sentiment - Provides factual information

Test Example:
User: "This is amazing!" 
Bot recognizes positive sentiment, responds enthusiastically

User: "I'm frustrated"
Bot recognizes negative sentiment, offers assistance

HOW TO VIEW THE ACTIVITY LOG

Accessing the Activity Log:
1. Click the "Activity Log" Button located on the main interface
2. Shows the last 10 activities by default
3. The log appears in the main chat area
4. Displays Action, Details, and Timestamp

What Gets Logged:
- Bot Initialization when the chatbot starts
- User Messages for all user inputs
- Task Actions for task creation, deletion, and updates
- Quiz Activities for quiz start, completion, and scores
- Errors for any system errors or exceptions
- User Interactions for all significant user actions

Sample Activity Log Output:
[Timestamp] User Login: User connected to CypherSenseBot
[Timestamp] Task Created: "Project Report" scheduled for 2024-12-31
[Timestamp] Quiz Started: Cybersecurity Basics quiz initiated
[Timestamp] Quiz Completed: Score 85/100
[Timestamp] Task Deleted: "Old reminder" removed from system

Log Analysis:
- Use logs to track your interaction history
- Monitor task completion rates
- Review quiz performance trends
- Troubleshoot any issues that occurred

LOGIN DETAILS AND IMPORTANT NOTES

User Authentication:
- Current Version: No login required
- Default Username: Automatically prompted on first interaction
- Enter Your Name: When prompted, type your name to personalize the experience

Important Configuration Notes:

1. Connection String
   - Update in ChatBot.cs if using non-local SQL Server
   - Ensure SQL Server is running before launching the application

2. Database Permissions
   - Ensure your SQL login has CREATE, READ, UPDATE, DELETE permissions
   - If using Windows Authentication, ensure your Windows account has database access

3. Audio File
   - Place greetingAudio.wav in the application's bin/Debug folder
   - If audio file is missing, the app will continue without voice greeting

4. File Paths
   - Ensure all relative paths are from the application executable location
   - For portability, use relative paths instead of absolute paths

5. Performance Considerations
   - Large task lists (500+) may take time to load
   - Activity logs grow over time; consider archiving old logs
   - Quiz questions are stored in ResponseHandler.cs



VIDEO PRESENTATION



PROJECT STRUCTURE

CypherSenseBotPart2/
├── CypherSenseBotPart2/
│   ├── Form1.cs                 # Main UI form
│   ├── ChatBot.cs               # Core chatbot logic
│   ├── TaskAssistant.cs         # Task management
│   ├── CyberQuiz.cs             # Quiz functionality
│   ├── ActivityLog.cs           # Activity logging
│   ├── ResponseHandler.cs       # NLP response generation
│   ├── Sentiment.cs             # Sentiment analysis
│   ├── Memory.cs                # Bot memory/context
│   ├── VoiceGreeting.cs         # Audio greeting
│   ├── AsciiIArt.cs             # ASCII art display
│   ├── Program.cs               # Application entry point
│   └── CypherSenseBotPart2.csproj
├── Task1/                       # SQL Server project
│   ├── TaskHelper.sql           # Task table schema
│   └── Task1.sqlproj
└── CypherSenseBotPart2.slnx    # Solution file




