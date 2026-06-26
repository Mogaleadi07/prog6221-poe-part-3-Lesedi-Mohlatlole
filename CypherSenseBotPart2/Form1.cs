namespace CypherSenseBotPart2
{
    public partial class Form1 : Form
    {
        // creating an instance of the chatbot class to use its methods in the form.
        private ChatBot chatBot;
        private bool coversationStart=false;
        private TaskAssistant taskAssistant;
        private Quiz quiz;
        private ActivityLog activityLog;
        public Form1()
        {
            InitializeComponent();
        }

        // the form load event.
        private void Form1_Load(object sender, EventArgs e)
        {
            //the voice will greet the user first
            VoiceGreeting.PlayAudio("greetingAudio.wav");

            // displaying the ascii art
            AsciiIArt asciiArt= new AsciiIArt();
            string art = asciiArt.GetAsciiArt();
            richTextBox1.SelectionColor = Color.Yellow;
            richTextBox1.AppendText(art + "\n");
            richTextBox1.SelectionColor = Color.Black;

            taskAssistant = new TaskAssistant();
            quiz = new Quiz();
            activityLog= new ActivityLog();
            

            // displaying the greeting message from the chatbot.
            chatBot = new ChatBot(taskAssistant, quiz,activityLog);
          
            richTextBox1.SelectionColor = Color.DarkCyan;
            richTextBox1.AppendText("CypherSenseBot: " + chatBot.GetGreeting() + "\n");
            richTextBox1.AppendText("---\n");
            richTextBox1.SelectionColor = Color.Black;

            activityLog.LogAction("Bot Startup", "CypherSenseBot initialized");
        }
        // the refresh task lists
        private void RefreshTasksList()
        {
            try
            {
                List<Task1> tasks = taskAssistant.GetAllTasks();

                // Bind to DataGridView
                dataGridView1.DataSource = tasks;

                // Auto-fit columns
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                richTextBox1.SelectionColor = Color.DarkRed;
                richTextBox1.AppendText($"CypherSenseBot: Error loading tasks: {ex.Message}\n");
                richTextBox1.AppendText("---\n");
                richTextBox1.SelectionColor = Color.Black;
            }
        }
        // the send button click event.
        private void sendButton_Click(object sender, EventArgs e)
        {
            string userMessage = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(userMessage))
            {
                richTextBox1.SelectionColor = Color.DarkRed;
                richTextBox1.AppendText("CypherSenseBot: Please enter a name to continue.\n");
                richTextBox1.AppendText("=================================================================================================================================================\n");
                richTextBox1.SelectionColor = Color.Black;
                return;
            }

            richTextBox1.SelectionColor = Color.Magenta;
            string currentUserName = chatBot.GetUserName();
            richTextBox1.AppendText($"{currentUserName}: {userMessage}\n");

            // Get bot response
            string botResponse = chatBot.ProcessMessage(userMessage);

            
            richTextBox1.SelectionColor = Color.DarkCyan;
            richTextBox1.AppendText($"CypherSenseBot: {botResponse}\n");

            
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("---\n");

            textBox1.Clear();
            textBox1.Focus();

            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }
        // the refresh task button 
        private void refreshTasksButton_Click(object sender, EventArgs e)
        {
            RefreshTasksList();

            richTextBox1.SelectionColor = Color.DarkGreen;
            richTextBox1.AppendText("CypherSenseBot: Tasks refreshed!\n");
            richTextBox1.AppendText("---\n");
            richTextBox1.SelectionColor = Color.Black;

            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }

        private void deleteTaskButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                richTextBox1.SelectionColor = Color.DarkRed;
                richTextBox1.AppendText("CypherSenseBot: Please select a task to delete.\n");
                richTextBox1.AppendText("---\n");
                richTextBox1.SelectionColor = Color.Black;
                return;
            }

            try
            {
                // Get selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Get TaskTitle
                string taskTitle = selectedRow.Cells[1].Value.ToString();

                // Delete from database
                taskAssistant.DeleteTask(taskTitle);

                // Refresh grid
                RefreshTasksList();

                richTextBox1.SelectionColor = Color.DarkGreen;
                richTextBox1.AppendText($"CypherSenseBot: Task '{taskTitle}' deleted!\n");
                richTextBox1.AppendText("---\n");
                richTextBox1.SelectionColor = Color.Black;


                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                richTextBox1.SelectionColor = Color.DarkRed;
                richTextBox1.AppendText($"CypherSenseBot: Error deleting task: {ex.Message}\n");
                richTextBox1.AppendText("---\n");
                richTextBox1.SelectionColor = Color.Black;
            }
        }

        // the activity log button click event
        private void activityLogButton_Click(object sender, EventArgs e)
        {
            try
            {
                string logSummary = activityLog.GetActivitySummary(10);

                richTextBox1.SelectionColor = Color.Yellow;
                richTextBox1.AppendText("\n--- ACTIVITY LOG ---\n");
                richTextBox1.SelectionColor = Color.Cyan;
                richTextBox1.AppendText(logSummary);
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.AppendText("---\n");

                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                richTextBox1.SelectionColor = Color.DarkRed;
                richTextBox1.AppendText($"Error loading activity log: {ex.Message}\n");
                richTextBox1.SelectionColor = Color.Black;
            }
        }
    }
}
