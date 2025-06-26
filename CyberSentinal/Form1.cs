using System;
using System.Collections.Generic;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CyberSentinal
{
    public partial class Form1 : Form
    {
        private string userName = "User";
        private int currentQuestion = 0;
        private int score = 0;
        private List<(string time, string message)> reminders = new();
        private List<string> activityLog = new();
        private Random rnd = new();

        private readonly (string Question, string[] Options, int CorrectIndex)[] quizData = new[]
        {
            ("What is phishing?", new[] { "A fishing method", "Scam via email to steal info", "A virus", "A network tool" }, 1),
            ("What does 2FA stand for?", new[] { "Two-Factor Authentication", "Fast Access", "Fake Account", "Firewall Alert" }, 0),
            ("Why use a VPN?", new[] { "To watch Netflix", "For faster internet", "For secure internet connection", "To boost battery" }, 2),
            ("Which is a strong password?", new[] { "123456", "password", "Pa$$w0rd123!", "abcdefg" }, 2),
            ("What's the first thing to do when getting a suspicious email?", new[] { "Open it", "Click links", "Report or delete it", "Forward it" }, 2),
            ("What is malware?", new[] { "A type of drink", "Malfunctioning hardware", "Malicious software", "An antivirus" }, 2),
            ("What’s a good way to secure your account?", new[] { "Using your pet's name", "Writing your password on paper", "Enabling 2FA", "Never logging out" }, 2),
            ("What is a firewall?", new[] { "Antivirus", "A password", "A security system", "A browser" }, 2),
            ("What is the safest type of network?", new[] { "Public Wi-Fi", "Unsecured Home Network", "Secured Home Network", "Open Hotspot" }, 2),
            ("What does HTTPS mean?", new[] { "It’s a virus", "It’s secure", "It’s a hacker tool", "It’s a browser" }, 1),
        };

        private static readonly Dictionary<string, List<string>> KeywordTips = new()
        {
            { "phishing", new List<string> {
                "Beware of emails asking for personal information. Verify sender addresses.",
                "Don't click suspicious links—hover to preview URLs.",
                "Check for spelling errors and urgent messages—they're red flags!"
            }},
            { "password", new List<string> {
                "Use a password manager to store complex, unique passwords.",
                "Never reuse passwords across sites.",
                "Use a mix of letters, numbers, and symbols."
            }},
            { "privacy", new List<string> {
                "Limit app permissions to only what's necessary.",
                "Use privacy-focused browsers or extensions.",
                "Review your social media privacy settings regularly."
            }},
            { "malware", new List<string> {
                "Keep your antivirus software updated.",
                "Avoid downloading software from untrusted sources.",
                "Regularly scan your devices for malware."
            }},
            { "data breach", new List<string> {
                "Change your passwords immediately if you suspect a breach.",
                "Enable two-factor authentication wherever possible.",
                "Monitor your accounts for unusual activity."
            }},
            { "two-factor authentication", new List<string> {
                "Always enable 2FA for an extra layer of security.",
                "Use an authenticator app instead of SMS for better security.",
                "Backup your 2FA codes in a secure location."
            }},
            { "social engineering", new List<string> {
                "Be cautious of unsolicited requests for information.",
                "Verify identities before sharing sensitive data.",
                "Educate yourself about common social engineering tactics."
            }},
            { "encryption", new List<string> {
                "Use end-to-end encryption for sensitive communications.",
                "Encrypt your files and backups to protect against unauthorized access.",
                "Understand the difference between symmetric and asymmetric encryption."
            }},
            { "vpn", new List<string> {
                "Use a VPN to secure your internet connection on public Wi-Fi.",
                "Choose a reputable VPN provider with a no-logs policy.",
                "Understand the limitations of VPNs—they don't make you invincible."
            }},
            { "iot security", new List<string> {
                "Change default passwords on IoT devices immediately.",
                "Keep your IoT devices updated with the latest firmware.",
                "Isolate IoT devices on a separate network if possible."
            }},
            { "ransomware", new List<string> {
                "Regularly back up your data to recover from ransomware attacks.",
                "Be cautious of email attachments and links from unknown sources.",
                "Use reputable security software to detect and block ransomware."
            }},
            { "cybersecurity", new List<string> {
                "Stay informed about the latest cybersecurity threats and trends.",
                "Participate in cybersecurity training and awareness programs.",
                "Report suspicious activities to your IT department or local authorities."
            }},
            { "identity theft", new List<string> {
                "Monitor your credit report regularly for unusual activity.",
                "Use identity theft protection services if necessary.",
                "Be cautious about sharing personal information online."
            }},
            { "online safety", new List<string> {
                "Always log out of accounts when using public computers.",
                "Be wary of oversharing personal information on social media.",
                "Use strong, unique passwords for all your online accounts."
            }}
        };

        public Form1()
        {
            InitializeComponent();
            this.Text = "Cyber Sentinel";
            AskUserName();
            PlayWelcomeSound();
            ShowAsciiArt();
            reminderTimer.Start();
        }

        private void AskUserName()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Welcome to Cyber Sentinel!\nPlease enter your name:", "Cyber Sentinel");
            if (!string.IsNullOrWhiteSpace(input)) userName = input;
        }

        private void PlayWelcomeSound()
        {
            try
            {
                string filepath = @"C:\Users\lab_services_student\OneDrive - ADvTECH Ltd\Desktop\CyberSentinal\CyberSentinal\Files\Welcome.wav.wav";
                SoundPlayer player = new SoundPlayer(filepath);
                player.Play();
            }
            catch (Exception)
            {
                Console.WriteLine("Voice file missing or not supported");
            }
        }

        private void ShowAsciiArt()
        {
            string asciiArt = @"
  ____       _               _____             _        _       
 / ___|  ___| |_ _   _ _ __ | ____|_ __   __ _| |_ __ _| | ___  
 \___ \ / _ \ __| | | | '_ \|  _| | '_ \ / _` | __/ _` | |/ _ \ 
  ___) |  __/ |_| |_| | |_) | |___| | | | (_| | || (_| | |  __/ 
 |____/ \___|\__|\__,_| .__/|_____|_| |_|\__,_|\__\__,_|_|\___| 
                      |_|                                       
";
            MessageBox.Show(asciiArt, "Cyber Sentinel - Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LogActivity(string description)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            activityLog.Add($"[{timestamp}] {description}");
            if (activityLog.Count > 50) activityLog.RemoveAt(0);
        }

        private void chatbotSendButton_Click(object sender, EventArgs e)
        {
            string input = chatbotTextBox.Text.Trim();
            chatbotTextBox.Clear();
            chatbotOutput.AppendText($"> {userName}: {input}\n");

            string lowerInput = input.ToLower();

            if (lowerInput.Contains("show activity log"))
            {
                chatbotOutput.AppendText("Bot 📜: Here's your recent activity:\n");
                foreach (var log in activityLog)
                    chatbotOutput.AppendText("- " + log + "\n");
                return;
            }

            if (lowerInput.Contains("quiz"))
            {
                tabControl.SelectedTab = quizTab;
                chatbotOutput.AppendText("Bot 🤖: Switched to Quiz tab.\n");
                LogActivity("Started quiz.");
                LoadQuizQuestion();
                return;
            }

            if (lowerInput.Contains("task"))
            {
                tabControl.SelectedTab = taskTab;
                chatbotOutput.AppendText("Bot 📝: Opened Task Manager.\n");
                return;
            }

            if (lowerInput.Contains("remind"))
            {
                tabControl.SelectedTab = reminderTab;
                chatbotOutput.AppendText("Bot ⏰: Let's set a reminder.\n");
                return;
            }

            bool positive = Regex.IsMatch(lowerInput, @"\b(happy|confident|secure|good|love|great)\b");
            bool negative = Regex.IsMatch(lowerInput, @"\b(worried|scared|afraid|nervous|anxious|bad|unsafe)\b");

            foreach (var keyword in KeywordTips.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    var tips = KeywordTips[keyword];
                    string tip = tips[rnd.Next(tips.Count)];
                    string sentiment = positive ? "Glad to hear you're feeling positive! " : negative ? "Thanks for sharing—here’s a tip: " : "";
                    chatbotOutput.AppendText($"Bot 💡: {sentiment}{tip}\n");
                    LogActivity($"Asked about '{keyword}'");
                    return;
                }
            }

            chatbotOutput.AppendText("Bot 🤖: I didn't understand that. Try keywords like 'password', 'VPN', 'phishing', or ask to 'show activity log'.\n");
        }

        private void LoadQuizQuestion()
        {
            if (currentQuestion >= quizData.Length)
            {
                MessageBox.Show($"🎉 Quiz complete! You scored {score}/{quizData.Length}.", "Quiz Results");
                currentQuestion = 0;
                score = 0;
                return;
            }

            var question = quizData[currentQuestion];
            quizLabel.Text = question.Question;
            quizOption1.Text = question.Options[0];
            quizOption2.Text = question.Options[1];
            quizOption3.Text = question.Options[2];
            quizOption4.Text = question.Options[3];

            quizOption1.Checked = quizOption2.Checked = quizOption3.Checked = quizOption4.Checked = false;
        }

        private void submitQuizButton_Click(object sender, EventArgs e)
        {
            int selected = quizOption1.Checked ? 0 :
                           quizOption2.Checked ? 1 :
                           quizOption3.Checked ? 2 :
                           quizOption4.Checked ? 3 : -1;

            if (selected == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            string correctAnswer = quizData[currentQuestion].Options[quizData[currentQuestion].CorrectIndex];
            if (selected == quizData[currentQuestion].CorrectIndex)
            {
                MessageBox.Show("✅ Correct!");
                score++;
            }
            else
            {
                MessageBox.Show($"❌ Incorrect. Correct answer: {correctAnswer}");
            }

            questionSummaryListBox.Items.Add($"{quizData[currentQuestion].Question} — Answer: {correctAnswer}");
            LogActivity($"Answered quiz question: {quizData[currentQuestion].Question}");
            currentQuestion++;
            LoadQuizQuestion();
        }

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            string task = $"Name: {taskNameTextBox.Text} | Desc: {taskDescTextBox.Text} | Time: {taskTimeTextBox.Text} | Status: {taskStatusComboBox.Text}";
            taskListBox.Items.Add(task);
            LogActivity($"Added task: {taskNameTextBox.Text}");
            ClearTaskFields();
        }

        private void editTaskButton_Click(object sender, EventArgs e)
        {
            int index = taskListBox.SelectedIndex;
            if (index != -1)
            {
                string task = $"Name: {taskNameTextBox.Text} | Desc: {taskDescTextBox.Text} | Time: {taskTimeTextBox.Text} | Status: {taskStatusComboBox.Text}";
                taskListBox.Items[index] = task;
                LogActivity($"Edited task: {taskNameTextBox.Text}");
                ClearTaskFields();
            }
            else
            {
                MessageBox.Show("Please select a task to edit.");
            }
        }

        private void deleteTaskButton_Click(object sender, EventArgs e)
        {
            int index = taskListBox.SelectedIndex;
            if (index != -1)
            {
                string task = taskListBox.Items[index].ToString();
                taskListBox.Items.RemoveAt(index);
                LogActivity($"Deleted task: {task}");
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }

        private void taskListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (taskListBox.SelectedIndex != -1)
            {
                string[] parts = taskListBox.SelectedItem.ToString().Split('|');
                taskNameTextBox.Text = GetValue(parts, "Name:");
                taskDescTextBox.Text = GetValue(parts, "Desc:");
                taskTimeTextBox.Text = GetValue(parts, "Time:");
                taskStatusComboBox.Text = GetValue(parts, "Status:");
            }
        }

        private string GetValue(string[] parts, string key)
        {
            foreach (var part in parts)
                if (part.Trim().StartsWith(key))
                    return part.Substring(part.IndexOf(":") + 1).Trim();
            return "";
        }

        private void ClearTaskFields()
        {
            taskNameTextBox.Clear();
            taskDescTextBox.Clear();
            taskTimeTextBox.Clear();
            taskStatusComboBox.SelectedIndex = -1;
        }

        private void SetReminderButton_Click(object sender, EventArgs e)
        {
            string input = reminderTextBox.Text;
            var match = Regex.Match(input, @"(.+)\s+at\s+(\d{2}:\d{2})");
            if (match.Success)
            {
                string message = match.Groups[1].Value.Trim();
                string time = match.Groups[2].Value.Trim();
                reminders.Add((time, message));
                MessageBox.Show($"Reminder set for {time}: {message}");
                LogActivity($"Reminder set: '{message}' at {time}");
            }
            else
            {
                MessageBox.Show("Invalid format. Use: 'Drink water at 14:30'");
            }
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            string now = DateTime.Now.ToString("HH:mm");
            foreach (var (time, message) in reminders)
            {
                if (now == time)
                {
                    MessageBox.Show($"🔔 Reminder: {message}");
                }
            }
        }
    }
}
