using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections;
using Newtonsoft.Json;
using System.Xml;

namespace LP4
{
    public partial class txtBearbeiten : Form
    {
        private List<TaskItem> aufgabenListe;
        private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "tasks.json");

        public txtBearbeiten()
        {
            InitializeComponent();
            aufgabenListe = new List<TaskItem>();
            LoadTasks(); // Beim Starten die Aufgaben laden
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            string aufgabe = txtAufgabe.Text.Trim();
            string priorität = cmbPriorität.SelectedItem?.ToString(); // ComboBox für Priorität (hoch, mittel, niedrig)
            DateTime datum = dateTimePicker1.Value; // DateTimePicker für das Datum der Aufgabe

            if (string.IsNullOrEmpty(aufgabe) || string.IsNullOrEmpty(priorität))
            {
                MessageBox.Show("Bitte eine Aufgabe und Priorität eingeben!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TaskItem task = new TaskItem
            {
                Beschreibung = aufgabe,
                Priorität = priorität,
                Datum = datum
            };

            aufgabenListe.Add(task);
            MessageBox.Show("Aufgabe hinzugefügt!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtAufgabe.Clear();
            cmbPriorität.SelectedIndex = -1; // ComboBox zurücksetzen
            dateTimePicker1.Value = DateTime.Now; // DateTimePicker zurücksetzen
            SaveTasks(); // Aufgaben speichern
        }

        
        private void btnAnzeigen_Click(object sender, EventArgs e)
        {
            lstAufgaben.Items.Clear();

            if (aufgabenListe.Count > 0)
            {
                foreach (TaskItem aufgabe in aufgabenListe)
                {
                    ListViewItem item = new ListViewItem(aufgabe.Beschreibung);
                    item.SubItems.Add(aufgabe.Datum.ToString("dd.MM.yyyy"));
                    item.SubItems.Add(aufgabe.Priorität);
                    SetPriorityColor(item, aufgabe.Priorität);
                    lstAufgaben.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Keine Aufgaben vorhanden.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void SetPriorityColor(ListViewItem item, string priority)
        {
            switch (priority.ToLower())
            {
                case "hoch":
                    item.BackColor = Color.Red;
                    break;
                case "mittel":
                    item.BackColor = Color.Yellow;
                    break;
                case "niedrig":
                    item.BackColor = Color.Green;
                    break;
                default:
                    item.BackColor = Color.White;
                    break;
            }
        }

       
        private void SortByDate()
        {
            lstAufgaben.ListViewItemSorter = new ListViewItemComparer(1); // Spalte 1 = Datum
            lstAufgaben.Sort();
        }

        
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer(int column)
            {
                col = column;
            }

            public int Compare(object x, object y)
            {
                DateTime dateX = DateTime.Parse(((ListViewItem)x).SubItems[col].Text);
                DateTime dateY = DateTime.Parse(((ListViewItem)y).SubItems[col].Text);
                return DateTime.Compare(dateX, dateY);
            }
        }

       
        private void btnLöschen_Click(object sender, EventArgs e)
        {
            if (lstAufgaben.SelectedItems.Count > 0)
            {
                var selectedItem = lstAufgaben.SelectedItems[0];
                var taskToRemove = aufgabenListe.FirstOrDefault(t => t.Beschreibung == selectedItem.Text);
                if (taskToRemove != null)
                {
                    aufgabenListe.Remove(taskToRemove);
                    lstAufgaben.Items.Remove(selectedItem);
                    SaveTasks(); // Aufgaben speichern nach Löschen
                }
            }
            else
            {
                MessageBox.Show("Bitte eine Aufgabe auswählen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
        private void btnBearbeiten_Click(object sender, EventArgs e)
        {
            if (lstAufgaben.SelectedItems.Count > 0)
            {
                var selectedItem = lstAufgaben.SelectedItems[0];
                string neueBeschreibung = txtedit.Text.Trim();

                if (string.IsNullOrEmpty(neueBeschreibung))
                {
                    MessageBox.Show("Bitte eine neue Beschreibung eingeben!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var taskToUpdate = aufgabenListe.FirstOrDefault(t => t.Beschreibung == selectedItem.Text);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Beschreibung = neueBeschreibung; // Beschreibung aktualisieren
                    selectedItem.Text = neueBeschreibung; // ListView aktualisieren
                    txtedit.Clear();
                    SaveTasks(); // Aufgaben speichern nach Bearbeiten
                }
            }
            else
            {
                MessageBox.Show("Bitte eine Aufgabe auswählen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
        private void SaveTasks()
        {
            try
            {
                var tasks = aufgabenListe.Select(task => new
                {
                    task.Beschreibung,
                    task.Priorität,
                    task.Datum
                }).ToList();

                File.WriteAllText(filePath, JsonConvert.SerializeObject(tasks, Formatting.Indented));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Speichern der Aufgaben: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void LoadTasks()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var tasks = JsonConvert.DeserializeObject<List<TaskItem>>(File.ReadAllText(filePath));
                    aufgabenListe = tasks ?? new List<TaskItem>(); // Falls null, neue Liste anlegen
                    btnAnzeigen_Click(null, null); // Aufgaben anzeigen
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Aufgaben: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    
    public class TaskItem
    {
        public string Beschreibung { get; set; }
        public string Priorität { get; set; }
        public DateTime Datum { get; set; }
    }
}
