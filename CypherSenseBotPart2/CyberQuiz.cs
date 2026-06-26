using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CypherSenseBotPart2
{
    public class CyberQuiz
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }
        public bool isMultipleChoice { get; set; }

        // the constructor for the CyberQuiz class with parameters.
        public CyberQuiz(int id, string question, List<string> options, int correctAnswerIndex, string explanation, bool isMultipleChoice)
        {
            ID = id;
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
            Explanation = explanation;
            this.isMultipleChoice = isMultipleChoice;
        }
    }
    public class Quiz
    {
        private List<CyberQuiz> questions;
        private int currentQuestionIndex;
        private int score;
        private bool isQuizCompleted;
        private List<int> answeredQuestions;

        // the constructor for the Quiz class.
        public Quiz()
        {
            questions = IntializeQuestions();
            currentQuestionIndex = 0;
            score = 0;
            isQuizCompleted = false;
            answeredQuestions = new List<int>();
        }
        // a method that initializes the quiz questions.
        private List<CyberQuiz> IntializeQuestions()
        {
            List<CyberQuiz> quizQuestions = new List<CyberQuiz>
            {
                new CyberQuiz(1, "What is the primary purpose of a firewall in network security?", new List<string> { "To encrypt data", "To monitor network traffic", "To block unauthorized access", "To store passwords" }, 2, "A firewall is designed to block unauthorized access while permitting outward communication.", true),
                new CyberQuiz(2, "Which of the following is a common method used in phishing attacks?", new List<string> { "Sending malicious emails", "Installing antivirus software", "Using strong passwords", "Regularly updating software" }, 0, "Phishing attacks often involve sending fraudulent emails to trick individuals into revealing sensitive information.", true),
                new CyberQuiz(3, "What does the term 'malware' refer to?", new List<string> { "A type of hardware", "Malicious software designed to harm or exploit systems", "A secure network protocol", "A programming language" }, 1, "'Malware' is short for malicious software and refers to any software intentionally designed to cause damage to a computer, server, client, or computer network.", true),
                new CyberQuiz(4, "Which of the following is an example of two-factor authentication (2FA)?", new List<string> { "Using a password and a fingerprint scan", "Using a password only", "Using a username only", "Using a password and a security question" }, 0, "Two-factor authentication requires two different forms of identification: something you know (password) and something you have (fingerprint scan).", true),
                new CyberQuiz(5, "What is the main goal of social engineering attacks?", new List<string> { "To exploit software vulnerabilities", "To manipulate individuals into divulging confidential information", "To create strong encryption algorithms", "To develop secure coding practices" }, 1, "Social engineering attacks aim to manipulate people into giving up confidential information rather than exploiting technical vulnerabilities.", true),
                new CyberQuiz(6, "What is the primary advantage of using a password manager?", new List<string> {"It slows down login", "Stores complex passwords securely","Eliminates need for passwords","Makes hacking easier"}, 1, "Password managers securely store and generate complex passwords for different accounts.", true),
                new CyberQuiz(7, "Which of the following is a sign of a secure website?", new List<string> {"HTTP protocol", "Padlock icon and HTTPS","No password is required","Bright Colours"}, 1, "Look for the padlock icon and HTTPS in the URL bar to verify a secure connection.", true),
                new CyberQuiz(8, "What would you do if you suspect a data breach?", new List<string> {"Ignore it","Change your password immediately","Post it on social media", "Wait fot a notification"}, 1, "Change your passwords immediately and montitor your account for suspecious activity", true),
                new CyberQuiz(9, "What is encryption?", new List<string> {"Deleting files","Converting data into unreadable format","Backing up data", "Scrambling data"}, 1, "Encryption is the process of converting data into a code to prevent unauthorized access.", true),
                new CyberQuiz(10,"What is the purpose of a VPN?", new List<string> {"To speed up internet","To create a secure connection over the internet","To block ads", "To hide your IP address"}, 1, "A VPN (Virtual Private Network) creates a secure connection over the internet, protecting your data and privacy.", true),
                new CyberQuiz(11,"Which of the following is a strong password?", new List<string> {"123456", "password", "P@ssw0rd!", "qwertyuiop"}, 2, "A strong password should include a mix of uppercase and lowercase letters, numbers, and special characters.", true),
                new CyberQuiz(12,"What is the purpose of a security audit?", new List<string> {"To check for vulnerabilities and ensure compliance with security policies","To install antivirus software","To create a backup of data","To improve network performance"}, 0, "A security audit is conducted to identify vulnerabilities and ensure that security policies are being followed.", true),
                new CyberQuiz(13,"What is the difference between a virus and a worm?", new List<string> {"A virus requires user action to spread, while a worm can spread automatically","A worm requires user action to spread, while a virus can spread automatically","There is no difference","They are the same thing"}, 0, "A virus typically requires user action (like opening an infected file) to spread, while a worm can propagate itself across networks without user intervention.", true),
                new CyberQuiz(14,"What is the purpose of a honeypot in cybersecurity?", new List<string> {"To attract and trap potential attackers","To store sensitive data","To encrypt files", "To monitor network traffic"}, 0, "A honeypot is a decoy system designed to attract and trap potential attackers, allowing security teams to study their methods.", true),
                new CyberQuiz(15,"What is the main purpose of a penetration test?", new List<string> {"To identify security weaknesses by simulating an attack","To install antivirus software","To create a backup of data", "To improve network performance"}, 0, "A penetration test (pen test) is conducted to identify security weaknesses by simulating an attack on the system.", true)
            };
            return quizQuestions;
        }
        // starting the quiz
        public string StartQuiz()
        {
            currentQuestionIndex = 0;
            score = 0;
            isQuizCompleted = false;
            answeredQuestions.Clear();
            string introMessage = "Welcome to the Cybersecurity Quiz! You will be presented with multiple-choice questions. Please select the correct answer from the options provided. Good luck!\n\n";
            return introMessage + GetCurrentQuestion();
        }

        // method to get the current question
        public string GetCurrentQuestion()
        {
            if (isQuizCompleted || currentQuestionIndex >= questions.Count)
                return "Quiz has ended";

            CyberQuiz quizQuestions = questions[currentQuestionIndex];
            string progressBar = CreateProgressBar(currentQuestionIndex, questions.Count);

            string output = $"\n{progressBar}";
            output += $"\nQuestion {currentQuestionIndex + 1}/{questions.Count}\n";
            output += $"{quizQuestions.Question}\n";

            if (quizQuestions.isMultipleChoice)
            {
                output += "A) " + quizQuestions.Options[0] + "\n";
                output += "B) " + quizQuestions.Options[1] + "\n";
                output += "C) " + quizQuestions.Options[2] + "\n";
                output += "D) " + quizQuestions.Options[3] + "\n";
            }
            else
            {
                output += "A) " + quizQuestions.Options[0] + "\n";
                output += "B) " + quizQuestions.Options[1] + "\n";
            }
            return output;
        }

        // the visual progress bar for the quiz
        private string CreateProgressBar(int current, int total)
        {
            int filled = (current * 10) / total;
            string bar = "[";
            for (int i = 0; i < 10; i++)
            {
                if (i < filled)
                    bar += "█";
                else
                    bar += "░";
            }
            bar += "]";
            return bar;
        }
        // method that submits and checks if the answer is correct
        public string SubmitAnswer(string userAnswer)
        {
            if (isQuizCompleted)
                return "Quiz is not completed yet. Please answer the current question.";
            if (currentQuestionIndex >= questions.Count)
                return GetQuizResult();

            CyberQuiz quizQuestions = questions[currentQuestionIndex];
            string output = "";
            userAnswer = userAnswer.Trim().ToUpper();
            int userAnswerIndex = -1;

            if (userAnswer == "A") userAnswerIndex = 0;
            else if (userAnswer == "B") userAnswerIndex = 1;
            else if (userAnswer == "C") userAnswerIndex = 2;
            else if (userAnswer == "D") userAnswerIndex = 3;
            else
            {
                return "Invalid answer. Please select A, B, C, or D.";
            }

            answeredQuestions.Add(currentQuestionIndex);
            // checking the answer
            if (userAnswerIndex == quizQuestions.CorrectAnswerIndex)
            {
                score++;
                output += "CORRECT !";
            }
            else
            {
                output += "INCORRECT !";
            }
            output += quizQuestions.Explanation + "\n";
            output += $"(score : {score}/ {currentQuestionIndex + 1})\n";
            currentQuestionIndex++; // incrementing current question
            if (currentQuestionIndex >= questions.Count)
            {
                output += GetQuizResult();
                isQuizCompleted = false;
            }
            else
            {
                output += "\n" + GetCurrentQuestion();
            }
            return output;
        }
        private string GetQuizResult()
        {
            int total = questions.Count;
            double percentage = (score * 100.0) / total;
            int correctCount = score;
            int wrongCount = total - score;

            string feedback = "";
            string performanceSummary = "";
            string recommendation = "";
            // adding a personalized feedback based on the score
            if (percentage >= 100)
            {
                feedback = "Outstanding ! You have an exceptional understanding of cybersecurity concepts.";
                performanceSummary = $"Outstanding Preformance! You answered {correctCount}/ {total} correctly.";
                recommendation = "You have an exceptional understanding of cybersecurity concepts. Consider exploring advanced topics and staying updated with the latest trends in cybersecurity.";
            }

            if (percentage >= 95)
            {
                feedback = "Excellent ! You have a strong understanding of cybersecurity concepts.";
                performanceSummary = $"Great Preformance! You answered {correctCount}/ {total} correctly.";
                recommendation = "You have excellent knowledge of cybersecurity concepts.";
            }
            else if (percentage >= 80)
            {
                feedback = "Good job! You have a solid understanding of cybersecurity concepts.";
                performanceSummary = $"Good Preformance! You answered {correctCount}/ {total} correctly.";
                recommendation = "You have a good understanding of cybersecurity concepts. Keep learning and practicing to improve your skills.";
            }
            else if (percentage >= 60)
            {
                feedback = "Fair! You have a basic understanding of cybersecurity concepts.";
                performanceSummary = $"Fair Preformance! You answered {correctCount}/ {total} correctly.";
                recommendation = "You have a basic understanding of cybersecurity concepts. Consider reviewing key topics and practicing more to enhance your knowledge.";
            }
            else
            {
                feedback = "Needs Improvement! You may need to review cybersecurity concepts.";
                performanceSummary = $"Needs Improvement! You answered {correctCount}/ {total} correctly.";
                recommendation = "You may need to review cybersecurity concepts and practice more to improve your knowledge and skills.";
            }
            string result = $" FINAL SCORE: {score}/{total} ({percentage:F1}%)\n\n" +
                            $"Feedback: {feedback}\n" +
                            $"Performance Summary: {performanceSummary}\n" +
                            $"Recommendation: {recommendation}\n" +
            $"Correct Answers: {correctCount}\n" +
            $"Incorrect Answers: {wrongCount} \n" +
            $"Topics Covered: Phishing, Malware, Network Security etc.\n\n";
            return result;
        }
        public bool IsQuizCompleted()
        {
            return isQuizCompleted;
        }
        public int GetCurrentScore()
        {
            return score;
        }
        public int GetCurrentQuestionNumber()
        {
            return currentQuestionIndex + 1;
        }
        public int GetTotalQuestions()
        {
            return questions.Count;
        }
    }
}
   
       

    

