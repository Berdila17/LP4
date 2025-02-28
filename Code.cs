using System;
using System.Windows.Forms;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace LP4
{
    public partial class txtBearbeiten : Form
    {
        private List<string> aufgabenListe;
        public txtBearbeiten()
        {
            InitializeComponent();
            aufgabenListe = new List<string>();
        }

        private void augabenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void aufgabenHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aufgabe = txtAufgabe.Text.Trim();

            if (!string.IsNullOrEmpty(aufgabe))
            {
                aufgabenListe.Add(aufgabe);
                MessageBox.Show("Aufgabe hinzugefügt!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAufgabe.Clear();
            }
            else
            {
                MessageBox.Show("Bitte eine Aufgabe eingeben!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAnzeigen_Click(object sender, EventArgs e)
        {
            lstAufgaben.Items.Clear();

            if (aufgabenListe.Count > 0)
            {
                foreach (string aufgabe in aufgabenListe)
                {
                    lstAufgaben.Items.Add(aufgabe);
                }
            }
            else
            {
                MessageBox.Show("Keine Aufgaben vorhanden.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLöschen_Click(object sender, EventArgs e)
        {
            if (lstAufgaben.SelectedIndex != -1)
            {
                aufgabenListe.RemoveAt(lstAufgaben.SelectedIndex);
                lstAufgaben.Items.RemoveAt(lstAufgaben.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Bitte eine Aufgabe auswählen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBearbeiten_Click(object sender, EventArgs e)
        {
            if (lstAufgaben.SelectedIndex != -1) // Prüfen, ob eine Aufgabe ausgewählt wurde
            {
                string neueBeschreibung = txtedit.Text.Trim();

                if (!string.IsNullOrEmpty(neueBeschreibung))
                {
                    int index = lstAufgaben.SelectedIndex;
                    aufgabenListe[index] = neueBeschreibung; // Liste aktualisieren
                    lstAufgaben.Items[index] = neueBeschreibung; // ListBox aktualisieren
                    txtedit.Clear(); // Eingabefeld leeren
                }
                else
                {
                    MessageBox.Show("Bitte eine neue Beschreibung eingeben!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Bitte eine Aufgabe auswählen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
