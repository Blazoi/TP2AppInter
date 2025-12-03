namespace Model
{
    public class Livre
    {
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public string ISBN { get; set; }
        public string MaisonEdition { get; set; }
        public DateOnly? DatePublication { get; set; }
        public string Description { get; set; }
        public double MoyenneEvaluation { get; set; }
        public int NmbEvaluation { get; set; }

        public Livre(string titre, string auteur, string iSBN, string maisonEdition, DateOnly? datePublication, string description, double moyenneEvaluation = 0, int nmbEvaluation = 0)
        {
            Titre = titre;
            Auteur = auteur;
            ISBN = iSBN;
            MaisonEdition = maisonEdition;
            DatePublication = datePublication;
            Description = description;
            MoyenneEvaluation = moyenneEvaluation;
            NmbEvaluation = nmbEvaluation;
        }
    }
}