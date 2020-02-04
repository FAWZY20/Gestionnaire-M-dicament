using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gsbRapports
{
    public partial class gererMedecin : Form
    {
        private gsbrapports2019Entities donnees; //Création de l'objet "donnees" de type gsbrapports2019Entites ( données de la table connecté )
        
        /// <summary>
        /// Constructeur du gererMedecin qui prend en paramètres l'objet donnees crée précedemment
        /// </summary>
        public gererMedecin(gsbrapports2019Entities donnees)
        {
            InitializeComponent();
            this.donnees = donnees;

            this.bindingSource1.DataSource = this.donnees.medecin; // Connecte l'outil "bindingSource1" aux données de la table "medecin"
            // Création d'une requete Linq "lesDepartements" qui prend tout les départements contenus dans la table "medecin"
            var lesDepartements = (from medecin in donnees.medecin orderby medecin.departement select medecin.departement).Distinct();
            comboBox1.DataSource = lesDepartements; //Connecte l'outil "comboBox1" aux données recueillis par la requete Linq "lesDépartements"
            // Création d'une requete Linq "lesNoms" qui prend tout les noms contenus dans la table "medecin"
            var lesNoms = (from medecin in donnees.medecin orderby medecin.nom select medecin.nom).Distinct();
            comboBox2.DataSource = lesNoms; //Connecte l'outil "comboBox2" aux données recueillis par la requete Linq "lesNoms"
        }

        /// <summary>
        /// Methode qui permet de trier par departement les valeurs affichées dans le "dataGridView1"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true; // Assigne la valeur "true" à la propriété "Visible" du "dataGridView1", de ce fait il sera visible au lancement du déboguage
            button1.Visible = true; // Assigne la valeur "true" à la propriété "Visible" du "button1", de ce fait il sera visible au lancement du déboguage
            int num_departement = Convert.ToInt32(comboBox1.Text); // Converti la chaine de caractere contenue dans le "comboBox1" afin de l'assigner à la variable "num_departement" de type int
            // Création d'une requete Linq "lesMedecins" qui selectionne les medecins situés dans le département voulu"
            var lesMedecins = (from medecin in donnees.medecin where medecin.departement == num_departement select medecin);
            dataGridView1.DataSource = lesMedecins; // Affichage des données souhaitées
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true; // Assigne la valeur "true" à la propriété "Visible" du "dataGridView1", de ce fait il sera visible au lancement du déboguage
            button2.Visible = true; // Assigne la valeur "true" à la propriété "Visible" du "button2", de ce fait il sera visible au lancement du déboguage
            string num_nom = Convert.ToString(comboBox2.Text); // Assigne la chaine de caractere contenue dans le "comboBox2" à la variable "num_nom" de type string
            // Création d'une requete Linq "lesMedecins" qui selectionne les medecins ayant le nom voulu
            var lesMedecins = (from medecin in donnees.medecin where medecin.nom == num_nom select medecin);
            dataGridView1.DataSource = lesMedecins; // Affichage des données souhaitées
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true; // Assigne la valeur "true" à la propriété "Visible" du "dataGridView1", de ce fait il sera visible au lancement du déboguage
            button3.Visible = true;// Assigne la valeur "true" à la propriété "Visible" du "button3", de ce fait il sera visible au lancement du déboguage
            int num_departement = Convert.ToInt32(comboBox1.Text); // Converti la chaine de caractere contenue dans le "comboBox1" afin de l'assigner à la variable "num_departement" de type int
            string num_nom = Convert.ToString(comboBox2.Text); // Assigne la chaine de caractere contenue dans le "comboBox2" à la variable "num_nom" de type string
            // Création d'une requete Linq "lesMedecins" qui selectionne les medecins ayant le nom et le departement voulus
            var lesMedecins = (from medecin in donnees.medecin where medecin.departement == num_departement && medecin.nom == num_nom select medecin);
            dataGridView1.DataSource = lesMedecins; // Affichage des données souhaitées
        }

        /// <summary>
        /// Méthode qui permet d'enregistrer les modifications effectuées
        /// via le boutton "Enregistrer les modifications"
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            donnees.SaveChanges();
            MessageBox.Show("Vos modifications ont été prises en comtpe !");
        }

        /// <summary>
        /// Méthode qui permet de faire le lien entre le formulaire gererMedecin et Le formulaire ajouterMedecin
        /// via le boutton "Ajouter un medecin"
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            ajouterMedecin a = new ajouterMedecin(this.donnees);
            a.Show();
        }

        /// <summary>
        /// Méthode qui permet de faire le lien entre le formulaire gererMedecin et Le formulaire supprimerMedecin
        /// via le boutton "Supprimer un medecin"
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            supprimerMedecin s = new supprimerMedecin(this.donnees);
            s.Show();
        }
    }
}
