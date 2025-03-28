namespace Aufgabenverwalter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
            this.Load += Form1_Load;
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbPriority.Items.Add("Hoch");
            cmbPriority.Items.Add("Mittel");
            cmbPriority.Items.Add("Niedrig");

           
            if (cmbPriority.Items.Count > 0)
                cmbPriority.SelectedIndex = 0;
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            string taskDescription = txtTaskDescription.Text.Trim();
            if (string.IsNullOrEmpty(taskDescription))
            {
                MessageBox.Show("Bitte eine gültige Aufgabe eingeben.");
                return;
            }

            string priority = cmbPriority.SelectedItem?.ToString() ?? "Mittel";
            DateTime dueDate = dtpDueDate.Value;

            string taskEntry = $"{taskDescription} | Priorität: {priority} | Fällig: {dueDate.ToShortDateString()}";

            lstTasks.Items.Add(taskEntry);

            txtTaskDescription.Clear();
            if (cmbPriority.Items.Count > 0)
                cmbPriority.SelectedIndex = 0;
        }

        private void btnShowTasks_Click(object sender, EventArgs e)
        {
            if (lstTasks.Items.Count == 0)
            {
                MessageBox.Show("Keine Aufgaben vorhanden.");
                return;
            }

            string allTasks = string.Join(Environment.NewLine, lstTasks.Items.Cast<string>());
            MessageBox.Show(allTasks, "Alle Aufgaben");
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex >= 0)
            {
                lstTasks.Items.RemoveAt(lstTasks.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Aufgabe zum Löschen aus.");
            }
        }

        private void btnEditTask_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex >= 0)
            {
                string currentTask = lstTasks.SelectedItem.ToString();
                string newTask = Microsoft.VisualBasic.Interaction.InputBox("Aufgabenbeschreibung bearbeiten:", "Aufgabe bearbeiten", currentTask);
                if (!string.IsNullOrEmpty(newTask))
                {
                    lstTasks.Items[lstTasks.SelectedIndex] = newTask;
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Aufgabe zum Bearbeiten aus.");
            }
        }

        private void lstTasks_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string taskEntry = lstTasks.Items[e.Index].ToString();

            Color color = Color.Black;
            if (taskEntry.Contains("Hoch"))
                color = Color.Red;
            else if (taskEntry.Contains("Niedrig"))
                color = Color.Green;

            e.DrawBackground();
            TextRenderer.DrawText(e.Graphics, taskEntry, e.Font, e.Bounds, color);
            e.DrawFocusRectangle();
        }
    }
}


