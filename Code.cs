using System;
using System.Windows.Forms;
using System.Collections.Generic;


namespace LP4
{
    public partial class Form1 : Form
    {
        private List<string> aufgabenListe;
        public Form1()
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
    }
}
