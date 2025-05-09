

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aufgabenverwalter; 

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

            LadeAufgabenAusDatenbank();
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

           
            DbHelper.InsertTask(taskDescription, priority, dueDate);

            MessageBox.Show("✅ Aufgabe gespeichert!");
            txtTaskDescription.Clear();
            cmbPriority.SelectedIndex = 0;

            LadeAufgabenAusDatenbank();
        }

        private void btnShowTasks_Click(object sender, EventArgs e)
        {
            LadeAufgabenAusDatenbank();
        }

        private void LadeAufgabenAusDatenbank()
        {
            lstTasks.Items.Clear();
            var aufgaben = DbHelper.LadeAlleAufgaben();

            foreach (var aufgabe in aufgaben)
            {
                lstTasks.Items.Add(new ListItem { Id = aufgabe.Id, Text = aufgabe.Anzeige });
            }
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem is ListItem selected)
            {
                DbHelper.DeleteTask(selected.Id);
                MessageBox.Show("❌ Aufgabe gelöscht");
                LadeAufgabenAusDatenbank();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Aufgabe zum Löschen aus.");
            }
        }

        private void btnEditTask_Click(object sender, EventArgs e)
        {
           // Muss noch überarbeitet werden das nächste mal
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

        private class ListItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }
    }
}
