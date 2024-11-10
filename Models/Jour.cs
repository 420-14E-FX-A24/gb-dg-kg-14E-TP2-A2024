﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static Automate.Models.Jour;
using System.Windows.Media;
using Microsoft.VisualBasic;

namespace Automate.Models
{
	// Personellement, j'appellerais le model Tache. Avec Id, Date, Type(enum), Comment, CreatedAt, UpdatedAt
    //
	// Si on veut être plus précis on pourrait même les afficher avec une heure.
	// Et les ajouter avec une heure aussi. (Ca change rien pour la bd, parce que c'est du DateTime)
	//
	// Il faut rester concis avec la langue des variables, soit tout anglais ou tout francais, pas de mélange
	//
	// Quand on clique sur une date ca affiche les tâches.
	//
	// J'afficherais les tâches en ordre décroissant selon la date d'ajout ou de modification.
	public class Jour
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("TimeCreated")]
        public DateTime TimeCreated { get; set; }

        [BsonElement("TimeUpdated")]
        [BsonIgnoreIfNull]
        public DateTime TimeUpdated { get; set; }

        [BsonElement("Taches")]
        [BsonIgnoreIfNull]
        public List<Tache> Taches { get; set; }

        [BsonElement("CommentaireTaches")]
        [BsonIgnoreIfNull]
        public List<string> CommentaireTaches { get; set; }

        [BsonElement("NombreAlertes")]
        public int NombreAlertes { get; set; }

        [BsonElement("Numero")]
        public int Numero {  get; set; } // Que représente numéro ?

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        public enum Tache
        {
            Semis,
            Rempotage,
            Entretien,
            Arrosage,
            Recolte,
            Commandes,
            Evenements
        }

        public Jour()
        {
            DateTime DateAujourdhui = DateTime.Now;
            TimeCreated = DateAujourdhui;
            Taches = new List<Tache>();
            CommentaireTaches = new List<string>();
            NombreAlertes = 0;
            Numero = DateAujourdhui.Day;
            Date = DateAujourdhui.ToLocalTime();
        }

        public Jour(Jour jour)
        {
            TimeCreated = jour.Date;
            Taches = new List<Tache>(jour.Taches);
            CommentaireTaches = new List<string>(jour.CommentaireTaches);
            NombreAlertes = jour.NombreAlertes;
            Numero = jour.Numero;
            Date = jour.Date;
        }


        public Jour(DateTime date)
        {
            DateTime DateAujourdhui = DateTime.Now;
            TimeCreated = DateAujourdhui;
            Taches = new List<Tache>();
            CommentaireTaches = new List<string>();
            NombreAlertes = 0;
            Numero = date.Day;
            Date = date.ToLocalTime();
        }

        public Jour(List<Tache> taches, List<string> commentairesTaches, DateTime date)
        {
            DateTime DateAujourdhui = DateTime.Now;
            TimeCreated = DateAujourdhui;
            Taches = taches;
            CommentaireTaches = commentairesTaches;
            List<Tache> traitementTacheAlertes = taches.FindAll(tache =>
            {
                int numeroTache = (int)tache;
                return numeroTache == 1 || numeroTache == 6;
            });
            NombreAlertes = traitementTacheAlertes.Count;
            Numero = date.Day;
            Date = date.ToLocalTime();
        }

    }
}
