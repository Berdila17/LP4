using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LP4
{
    public partial class txtBearbeiten : Form
    {
        
        private List<Aufgabe> aufgabenListe;

        public txtBearbeiten()
        {
            InitializeComponent();
            aufgabenListe = new List<Aufgabe>();
        }

       
        private void txtBearbeiten_Load(object sender, EventArgs e)
        {
           
            cmbPriorität.Items.Clear();
            cmbPriorität.Items.Add("Hoch");
            cmbPriorität.Items.Add("Mittel");
            cmbPriorität.Items.Add("Niedrig");
            cmbPriorität.SelectedIndex = 1; 
        }

        
        private void btnHinzufügen_Click(object sender, EventArgs e)
        {
            string beschreibung = txtAufgabe.Text.Trim();
            if (string.IsNullOrEmpty(beschreibung))
            {
                MessageBox.Show("Bitte eine Aufgabe eingeben!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

         
            Aufgabe neueAufgabe = new Aufgabe
            {
                Beschreibung = beschreibung,
                Prioritaet = cmbPriorität.SelectedItem.ToString(),
                Faelligkeitsdatum = dateTimePicker1.Value,
                Erledigt = false
            };

           
            aufgabenListe.Add(neueAufgabe);
            MessageBox.Show("Aufgabe hinzugefügt!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

            
            txtAufgabe.Clear();
            cmbPriorität.SelectedIndex = 1;
        }

       
        private void btnAnzeigen_Click(object sender, EventArgs e)
        {
            AktualisiereListe();
        }

      
        private void AktualisiereListe()
        {
            lstAufgaben.Items.Clear();
            foreach (Aufgabe a in aufgabenListe)
            {
                lstAufgaben.Items.Add(a);
            }
        }

     
        private void btnLöschen_Click(object sender, EventArgs e)
        {
            if (lstAufgaben.SelectedIndex >= 0)
            {
                aufgabenListe.RemoveAt(lstAufgaben.SelectedIndex);
                AktualisiereListe();
            }
            else
            {
                MessageBox.Show("Bitte eine Aufgabe auswählen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBearbeiten_Click(object sender, EventArgs e)
        {
            if (lstAufgaben.SelectedIndex >= 0)
            {
                string neueBeschreibung = txtedit.Text.Trim();
                if (string.IsNullOrEmpty(neueBeschreibung))
                {
                    MessageBox.Show("Bitte eine neue Beschreibung eingeben!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

              
                Aufgabe ausgewaehlteAufgabe = (Aufgabe)lstAufgaben.SelectedItem;
                ausgewaehlteAufgabe.Beschreibung = neueBeschreibung;

               
                AktualisiereListe();
                txtedit.Clear();
            }
            else
            {
                MessageBox.Show("Bitte eine Aufgabe auswählen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
        private void btnSortieren_Click(object sender, EventArgs e)
        {
            aufgabenListe.Sort((a, b) =>
            {
                int prioA = PriorityToNumber(a.Prioritaet);
                int prioB = PriorityToNumber(b.Prioritaet);
                int result = prioA.CompareTo(prioB);
                if (result == 0)
                {
                    return a.Faelligkeitsdatum.CompareTo(b.Faelligkeitsdatum);
                }
                return result;
            });

            AktualisiereListe();
        }

       
        private int PriorityToNumber(string prio)
        {
            switch (prio.ToLower())
            {
                case "hoch":
                    return 1;
                case "mittel":
                    return 2;
                case "niedrig":
                    return 3;
                default:
                    return 4;
            }
        }
    }
}
