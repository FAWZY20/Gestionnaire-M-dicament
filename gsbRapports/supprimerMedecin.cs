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
    public partial class supprimerMedecin : Form
    {
        private gsbrapports2019Entities donnees; //Création de l'objet "donnees" de type gsbrapports2019Entites ( données de la table connecté )

        /// <summary>
        /// Constructeur de supprimerMedecin qui prend en paramètres l'objet donnees crée précedemment
        /// </summary>
        public supprimerMedecin(gsbrapports2019Entities donnees) 
        {
            InitializeComponent();
            this.donnees = donnees;
            this.bindingSource1.DataSource = donnees.medecin; // Connecte l'outil "bindingSource1" aux données de la table "medecin"
        }

        private void supprimerMedecin_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Methode qui permet de supprimer un médecin à l'actionnement du boutton
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            int idAdd = 0; // Creation d'une variable idAdd de type int initialisé à 0
            // Creations de variables de type string pour pouvoir recuperer les informations saisies dans le formulaire
            string nomAdd = textBox2.Text;
            string prenomAdd = textBox3.Text;

            try
            {
                idAdd = Int32.Parse(textBox1.Text); // Essaie de convertir la chaine de caractere en un equivalent int 
            }
            catch (FormatException error)
            {
                // Sinon lui retourne un message d'erreur
                string message = "Saisi d'une valeur incorrect";
                string caption = error.Message;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }

            // Création d'un requete Linq qui permet de voir si le medecin indiqué existe dans la table medecin
            var unMedecin = (from medecin in donnees.medecin where medecin.id == idAdd && medecin.nom == nomAdd && medecin.prenom == prenomAdd select medecin).First();
            
            medecin SUPPRIMER = unMedecin;
            
            // Creation d'une requete Linq qui permet de voir si le medecin voulu possede des rapports
            var lesRapports = (from rapport in donnees.rapport where rapport.idMedecin == idAdd select rapport).Count();
            
            // Dans le cas ou le medecin possede un rapport un message d'erreur s'affiche et empeche la suppresion
            if (lesRapports > 0)
            { MessageBox.Show("Impossible de supprimer le médecin car il possède des rapports."); }
            // Sinon le medecin est supprimer
            else 
            {
                string message = "Voulez vous supprimer ?";
                string caption = "";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.OK) // Si on appuie sur OK alors le medecin se supprime de la base
                {
                    medecin supprimer = SUPPRIMER;
                    donnees.medecin.DeleteObject(supprimer);
                    donnees.SaveChanges();
                    MessageBox.Show("Le medecin a été supprimer !");
                }
            }
            
        }

    }
}
