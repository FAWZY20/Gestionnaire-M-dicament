using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace gsbRapports
{
    public partial class dernierRapport : Form
    {
        private gsbrapports2019Entities donnees; //Création de l'objet "donnees" de type gsbrapports2019Entites ( données de la table connecté )

        /// <summary>
        /// Constructeur de dernierRapport qui prend en paramètres l'objet donnees crée précedemment
        /// </summary>
        public dernierRapport(gsbrapports2019Entities donnees)
        {
            InitializeComponent();
            this.donnees = donnees;
            this.bindingSource1.DataSource = this.donnees.medecin; // Connecte l'outil "bindingSource1" aux données de la table "medecin"
            this.bindingSource2.DataSource = this.donnees.rapport; // Connecte l'outil "bindingSource2" aux données de la table "rapport"
            this.bindingSource3.DataSource = this.donnees.offrir; // Connecte l'outil "bindingSource3" aux données de la table "offrir"
            // Creation d'une requete Linq permettant de slectionner tout les idMedecin contenue dans la table rapport
            var lesidMedecin = (from rapport in donnees.rapport orderby rapport.idMedecin select rapport.idMedecin).Distinct();
            comboBox1.DataSource = lesidMedecin; //Connecte l'outil "comboBox1" aux données recueillis par la requete Linq "lesidMedecins"
        }

        private void dernierRapport_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Methode qui permet d'afficher le dernier rapporr d'un medecin à l'actionnement du boutton
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // Creation d'une requete Linq qui va permettre de recuperer les valeurs voulus dans les labels selon le nom et prenom du medecin
                var lesRapports = (from rapport in donnees.rapport
                                   join medecin in donnees.medecin on rapport.idMedecin equals medecin.id
                                   join visiteur in donnees.visiteur on rapport.idVisiteur equals visiteur.id
                                   orderby rapport.date descending
                                   where medecin.nom == textBox1.Text && medecin.prenom == textBox2.Text
                                   select new
                                   {
                                       idRapport = rapport.id,
                                       NomMedecin = medecin.nom,
                                       DateRapport = rapport.date,
                                       Motif = rapport.motif,
                                       Bilan = rapport.bilan,
                                       NomVisiteur = visiteur.nom,
                                   }).First();
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label16.Visible = true;
                label7.Text = lesRapports.NomVisiteur.ToString();
                label8.Text = lesRapports.Motif.ToString();
                label9.Text = lesRapports.Bilan.ToString();
                label10.Text = lesRapports.DateRapport.ToString();
                label16.Text = lesRapports.idRapport.ToString();
                button2.Visible = true;
                button3.Visible = true;

            var ideRapport = label16.Text; // Recupere la chaine de caractere du label16 ( idRapport )
            int idRapport_int = Convert.ToInt32(ideRapport); // Convertie la valeur recuperer en entier ( int )

            try // Essaie de voir si le medecin a prescrit des medicaments dans son rapport
            {
                // Creation d'une requete Linq qui va permettre de recuperer l'id du medicament et la quantité prescrit
                var lesQuantites = (from offrir in donnees.offrir
                                    join rapport in donnees.rapport on offrir.idRapport equals rapport.id
                                    where offrir.idRapport == lesRapports.idRapport
                                    select new
                                    {
                                        idMedicament = offrir.idMedicament,
                                        quantiteMedicament = offrir.quantite,
                                    }).First();
                label17.Visible = true;
                label15.Visible = true;
                label17.Text = lesQuantites.idMedicament.ToString();
                label15.Text = lesQuantites.quantiteMedicament.ToString();
            }
            catch // Dans le cas ou le medecin n'a prescrit aucun medicament, indique par un message l'etat et remplie les champs par aucun
            {
                MessageBox.Show("Le médecin n'a pas prescrit de médicament !"); 
                label17.Visible = true;
                label15.Visible = true;
                label17.Text = "aucun";
                label15.Text = "aucun";

            }

            string idMedicament_string = label17.Text; // Recupere la chaine de caractere du label17 ( idMedicament )

            try // Essaie de voir le nom du medicament prescrit par le medecin dans son rapport
            {
                // Creation d'une requete Linq qui va permettre de recuperer le nom du medicament prescrit
                var lesMedicaments = (from medicament in donnees.medicament
                                      join offrir in donnees.offrir on medicament.id equals offrir.idMedicament
                                      join rapport in donnees.rapport on offrir.idRapport equals rapport.id
                                      where medicament.id == idMedicament_string
                                      select new
                                      {
                                          nomMedicament = medicament.nomCommercial,
                                      }).First();
                label14.Visible = true;
                label14.Text = lesMedicaments.nomMedicament.ToString();
            }
            catch // Dans le cas ou le medecin n'a prescrit aucun medicament, indique par un message l'etat et remplie les champs par aucun
            {
                MessageBox.Show("Aucun médicament a été prescrit !");
                label14.Visible = true;
                label14.Text = "aucun";
            }
        }

        /// <summary>
        /// Methode qui permet d'exporter en XML le dernier rapport d'un médecin à l'actionnement du boutton
        /// les using necessaires ont été ajouté en haut au prealable
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            // Affecte chaque valeur obtenue pour le dernier rapport à un objet de type XElement
            XElement idMedecin = new XElement("idMedecin", comboBox1.Text);
            XElement nomMedecin = new XElement("NomMedecin",textBox1.Text);
            XElement prenomMedecin = new XElement("prenomMedecin", textBox2.Text);
            XElement nomVisiteur = new XElement("NomVisiteur",label7.Text);
            XElement motif = new XElement("Motif",label8.Text);
            XElement bilan = new XElement("Bilan",label9.Text);
            XElement date = new XElement("DateRapport",label10.Text);
            XElement idMedicament = new XElement("idMedicament",label17.Text);
            XElement quantiteMedicament = new XElement("QuantiteMedicament",label15.Text);
            XElement nomMedicament = new XElement("NomMedicament", label14.Text);
            XElement n = new XElement("Rapport");

            // Ajoute les informations collectés sous format XML à l'objet n de type XElement
            n.Add(idMedecin);
            n.Add(nomMedecin);
            n.Add(prenomMedecin);
            n.Add(nomVisiteur);
            n.Add(motif);
            n.Add(bilan);
            n.Add(date);
            n.Add(idMedicament);
            n.Add(nomMedicament);
            n.Add(quantiteMedicament);

            XmlSerializer xs = new XmlSerializer(typeof(XElement)); // Permet de serialiser ( convertir des proprietes et des champs publics dans un format série ( XML )) les donnees de objet de type XElement
            using (StreamWriter wr = new StreamWriter("rapport.xml")) // Creation du ficheir rapport.xml ( par default dans le bin du projet visualstudio)
            {
                xs.Serialize(wr, n);
            }
            MessageBox.Show("Contenu du fichier enregistré :" + n.ToString());
        }

        /// <summary>
        /// Methode qui permet de preremplir les noms et prenoms du medecin par rapport au id choisie à l'actionnement du boutton
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            var idMedecin = comboBox1.Text;
            int idMedecin_int = Convert.ToInt32(idMedecin);

            // Creation d'une requete Linq qui retourne le nom et prenom du medecin selon l'id indiqué afin de remplir les labels
            var lesMedecinsdansrapports = ( from rapport in donnees.rapport
                                            join medecin in donnees.medecin on rapport.idMedecin equals medecin.id
                                            where medecin.id == idMedecin_int
                                            select new
                                            {
                                              NomMedecin = medecin.nom,
                                              PrenomMedecin = medecin.prenom,
                                            }).First();
            label11.Visible = true;
            label12.Visible = true;
            textBox1.Text = lesMedecinsdansrapports.NomMedecin.ToString();
            textBox2.Text = lesMedecinsdansrapports.PrenomMedecin.ToString();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
