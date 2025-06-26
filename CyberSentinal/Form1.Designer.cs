namespace CyberSentinal
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage chatTab;
        private System.Windows.Forms.TabPage quizTab;
        private System.Windows.Forms.TabPage taskTab;
        private System.Windows.Forms.TabPage reminderTab;

        private System.Windows.Forms.TextBox chatbotTextBox;
        private System.Windows.Forms.RichTextBox chatbotOutput;
        private System.Windows.Forms.Button chatbotSendButton;

        private System.Windows.Forms.Label quizLabel;
        private System.Windows.Forms.RadioButton quizOption1;
        private System.Windows.Forms.RadioButton quizOption2;
        private System.Windows.Forms.RadioButton quizOption3;
        private System.Windows.Forms.RadioButton quizOption4;
        private System.Windows.Forms.Button submitQuizButton;
        private System.Windows.Forms.ListBox questionSummaryListBox;

        private System.Windows.Forms.TextBox taskNameTextBox;
        private System.Windows.Forms.TextBox taskDescTextBox;
        private System.Windows.Forms.TextBox taskTimeTextBox;
        private System.Windows.Forms.ComboBox taskStatusComboBox;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.Button addTaskButton;
        private System.Windows.Forms.Button editTaskButton;
        private System.Windows.Forms.Button deleteTaskButton;

        private System.Windows.Forms.TextBox reminderTextBox;
        private System.Windows.Forms.Button setReminderButton;
        private System.Windows.Forms.Timer reminderTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.chatTab = new System.Windows.Forms.TabPage();
            this.quizTab = new System.Windows.Forms.TabPage();
            this.taskTab = new System.Windows.Forms.TabPage();
            this.reminderTab = new System.Windows.Forms.TabPage();

            // Tabs Setup
            this.tabControl.Controls.Add(this.chatTab);
            this.tabControl.Controls.Add(this.quizTab);
            this.tabControl.Controls.Add(this.taskTab);
            this.tabControl.Controls.Add(this.reminderTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;

            // CHAT TAB
            this.chatbotOutput = new System.Windows.Forms.RichTextBox { Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(640, 250) };
            this.chatbotTextBox = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(10, 270), Width = 500 };
            this.chatbotSendButton = new System.Windows.Forms.Button { Text = "Send", Location = new System.Drawing.Point(520, 270) };
            this.chatbotSendButton.Click += new System.EventHandler(this.chatbotSendButton_Click);
            this.chatTab.Controls.AddRange(new System.Windows.Forms.Control[] { chatbotOutput, chatbotTextBox, chatbotSendButton });

            // QUIZ TAB
            this.quizLabel = new System.Windows.Forms.Label { Text = "Question", Location = new System.Drawing.Point(10, 10), Width = 600 };
            this.quizOption1 = new System.Windows.Forms.RadioButton { Location = new System.Drawing.Point(30, 40), Width = 600 };
            this.quizOption2 = new System.Windows.Forms.RadioButton { Location = new System.Drawing.Point(30, 70), Width = 600 };
            this.quizOption3 = new System.Windows.Forms.RadioButton { Location = new System.Drawing.Point(30, 100), Width = 600 };
            this.quizOption4 = new System.Windows.Forms.RadioButton { Location = new System.Drawing.Point(30, 130), Width = 600 };
            this.submitQuizButton = new System.Windows.Forms.Button { Text = "Submit", Location = new System.Drawing.Point(30, 170) };
            this.submitQuizButton.Click += new System.EventHandler(this.submitQuizButton_Click);
            this.questionSummaryListBox = new System.Windows.Forms.ListBox { Location = new System.Drawing.Point(400, 10), Size = new System.Drawing.Size(240, 150) };
            this.quizTab.Controls.AddRange(new System.Windows.Forms.Control[] {
                quizLabel, quizOption1, quizOption2, quizOption3, quizOption4, submitQuizButton, questionSummaryListBox
            });

            // TASK TAB
            this.taskNameTextBox = new System.Windows.Forms.TextBox { PlaceholderText = "Task Name", Location = new System.Drawing.Point(10, 10) };
            this.taskDescTextBox = new System.Windows.Forms.TextBox { PlaceholderText = "Task Description", Location = new System.Drawing.Point(10, 40) };
            this.taskTimeTextBox = new System.Windows.Forms.TextBox { PlaceholderText = "Time", Location = new System.Drawing.Point(10, 70) };
            this.taskStatusComboBox = new System.Windows.Forms.ComboBox { Location = new System.Drawing.Point(10, 100), Width = 200 };
            this.taskStatusComboBox.Items.AddRange(new string[] { "Pending", "In Progress", "Completed" });
            this.taskListBox = new System.Windows.Forms.ListBox { Location = new System.Drawing.Point(250, 10), Size = new System.Drawing.Size(300, 150) };
            this.addTaskButton = new System.Windows.Forms.Button { Text = "Add", Location = new System.Drawing.Point(10, 130) };
            this.editTaskButton = new System.Windows.Forms.Button { Text = "Edit", Location = new System.Drawing.Point(90, 130) };
            this.deleteTaskButton = new System.Windows.Forms.Button { Text = "Delete", Location = new System.Drawing.Point(170, 130) };

            this.addTaskButton.Click += new System.EventHandler(this.addTaskButton_Click);
            this.editTaskButton.Click += new System.EventHandler(this.editTaskButton_Click);
            this.deleteTaskButton.Click += new System.EventHandler(this.deleteTaskButton_Click);
            this.taskListBox.SelectedIndexChanged += new System.EventHandler(this.taskListBox_SelectedIndexChanged);

            this.taskTab.Controls.AddRange(new System.Windows.Forms.Control[] {
                taskNameTextBox, taskDescTextBox, taskTimeTextBox, taskStatusComboBox, taskListBox,
                addTaskButton, editTaskButton, deleteTaskButton
            });

            // REMINDER TAB
            this.reminderTextBox = new System.Windows.Forms.TextBox { PlaceholderText = "Enter reminder (e.g., Drink water at 15:30)", Location = new System.Drawing.Point(10, 10), Width = 400 };
            this.setReminderButton = new System.Windows.Forms.Button { Text = "Set Reminder", Location = new System.Drawing.Point(420, 10) };
            this.setReminderButton.Click += new System.EventHandler(this.SetReminderButton_Click);
            this.reminderTimer = new System.Windows.Forms.Timer(this.components);
            this.reminderTimer.Interval = 60000; // check every minute
            this.reminderTimer.Tick += new System.EventHandler(this.ReminderTimer_Tick);
            this.reminderTab.Controls.AddRange(new System.Windows.Forms.Control[] { reminderTextBox, setReminderButton });

            // Finalize
            this.Controls.Add(this.tabControl);
            this.Text = "CyberSentinal";
            this.ClientSize = new System.Drawing.Size(700, 400);
        }
    }
}