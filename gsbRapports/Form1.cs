using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gsbRapports
{
    public partial class Form1 : Form
    {
        private gsbrapports2019Entities donnees; //Création de l'objet "donnees" de type gsbrapports2019Entites ( données de la table connecté )

        /// <summary>
        /// Constructeur du Form1
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.donnees = new gsbrapports2019Entities();
        }

        private void fichierToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void médecinToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Méthode qui permet de faire le lien entre le formulaire "Form1" et Le formulaire "gererMedecin"
        /// Via l'onglet "Gérer" qui se trouve dans l'onglet "Medecin" du formulaire "Form1"
        /// </summary>
        private void ajouterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gererMedecin f = new gererMedecin(this.donnees);
            f.Show();
        }

        /// <summary>
        /// Méthode qui permet de faire le lien entre le formulaire Form1 et Le formulaire dernierRapport
        /// via l'onglet "Dernier rapport" qui se trouve dans l'onglet "Medecin" du formulaire "Form1"
        /// </summary>
        private void majToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dernierRapport g = new dernierRapport(this.donnees);
            g.Show();
        }

        private void ajouterToolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }
    }
}
