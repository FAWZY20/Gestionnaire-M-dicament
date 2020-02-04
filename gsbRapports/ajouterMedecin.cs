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
    public partial class ajouterMedecin : Form
    {
        private gsbrapports2019Entities donnees; //Création de l'objet "donnees" de type gsbrapports2019Entites ( données de la table connecté )
        
        /// <summary>
        /// Constructeur d'ajouterMedecin qui prend en paramètres l'objet donnees crée précedemment
        /// </summary>
        public ajouterMedecin(gsbrapports2019Entities donnees)
        {
            InitializeComponent();
            this.donnees = donnees;
            this.bindingSource1.DataSource = donnees.medecin; // Connecte l'outil "bindingSource1" aux données de la table "medecin"
        }

        /// <summary>
        /// Methode qui permet d'ajouter un médecin à l'actionnement du boutton
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            {
                bool requette = true; // Creation d'une variable requette de type bool initialisé en true
                // Creations de variables de type string pour pouvoir recuperer les informations saisies dans le formulaire
                string nomAdd = textBox2.Text;
                string prenomAdd = textBox3.Text;
                string adresseAdd = textBox4.Text;
                string telAdd = textBox5.Text;
                string speAdd = textBox6.Text;
                int depAdd = 0; // Creation d'une variable depAdd de type int initialisé à 0

                // Verifie si le nom, le prenom et l'adresse et indiqué 
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    requette = false;
                    string message = "Veuillez saisir des informations svp !"; // Affiche un message d'erreur
                    string caption = "Saisie vide"; // légende du meesagebox
                    MessageBoxButtons buttons = MessageBoxButtons.OK; // Affiche une boite de message avec un boutton OK
                    MessageBox.Show(message, caption, buttons);
                }

                if (textBox5.Text == "") { telAdd = null; } // Si le numero de telephone n'est pas indiqué affecte la valeur null

                if (textBox6.Text == "") { speAdd = null; } // Si la specialite n'est pas indiqué affecte la valeur null

                try
                {
                    depAdd = Int32.Parse(textBox7.Text); // Essaie de convertir la chaine de caractere en un equivalent int 
                }
                catch (FormatException error)
                {
                    // Sinon lui retourne un message d'erreur
                    requette = false;
                    string message = "Saisi d'une valeur incorrect";
                    string caption = error.Message;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }

                // Si la valeur de requette reste true ( ce qui fait qu'il n y a plus d'erreur dans le remplissage du formulaire) ajoute le medecin
                if (requette)
                {
                    string message = "Voulez vous ajouter ?";
                    string caption = "";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel; // Affiche un boite de message avec un boutton OK et CANCEL
                    DialogResult result; // prend la valeur de retour de la boite de dialogue ( boite de message cité precedemment)
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.OK) // Si on appuie sur OK le medecin s'ajoute
                    {
                        medecin ajouter = new medecin { nom = nomAdd, prenom = prenomAdd, adresse = adresseAdd, tel = telAdd, specialiteComplementaire = speAdd, departement = depAdd };
                        donnees.medecin.AddObject(ajouter);
                        donnees.SaveChanges();
                        MessageBox.Show("Le medecin a été ajouté !");
                    }
                }
            }
        }
    }
}
