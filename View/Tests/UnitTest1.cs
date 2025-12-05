using Model;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using ViewModel;

namespace Tests
{
    public class Tests
    {
        private CatalogueViewModel catalogueVM = new();
        private AjouterLivreViewModel ajouterVM = new();
        private SupprimerLivreViewModel supprimerVM = new();

        // Variables attendues
        private string stringAttendu { get; set; }
        private Livre livreAttendu { get; set; }

        private readonly string cheminBiblio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bibliotheque.xml");



        [SetUp]
        public void Setup()
        {
            stringAttendu = "<Livre>\r\n  <Titre>titre_test</Titre>\r\n  <Auteur>auteur_test</Auteur>\r\n  <ISBN>isbn_test</ISBN>\r\n  <MaisonEdition>msnedtn_test</MaisonEdition>\r\n  <DatePublication>1001 - 01 - 01</DatePublication>\r\n  <Description>desc_test</Description>\r\n  <MoyenneEvaluation>0</MoyenneEvaluation>\r\n  <NombreEvaluations>0</NombreEvaluations>\r\n</Livre>";
            livreAttendu = new Livre("titre_test", "auteur_test", "isbn_test", "msnedtn_test", new DateOnly(1001, 01, 01), "desc_test");
        }

        [Test]
        public void AjouterLivre()
        {
            ajouterVM.Titre = "titre_test";
            ajouterVM.Auteur = "auteur_test";
            ajouterVM.ISBN = "isbn_test";
            ajouterVM.MaisonEdition = "msnedtn_test";
            ajouterVM.Description = "desc_test";

            ajouterVM.AjouterLivreCommand();

            var doc = XDocument.Load(cheminBiblio);

            var dernierLivre = doc.Root.Element("Livres").Elements("Livre").Last();

            // Si lors de la correction vous testez un autre livre, sachez que
            // .LastNode.ToString() inclut les espaces et charactères spéciaux et s'ils en sont
            // pas inclus dans stringAttendu, le test ne passera pas.
            string stringRetourne = dernierLivre.ToString();

            Assert.That(stringAttendu.Equals(stringRetourne));
        }

        [Test]
        public void LectureXML()
        {
            catalogueVM.ChargerLivres();

            Livre livreRecu = catalogueVM.Livres.Last();
            bool equivalent = true;
            for (int i = 0; i < 1; i++)
            {
                if (livreAttendu.Titre != livreRecu.Titre)
                {
                    equivalent = false;
                    break;
                }
                if (livreAttendu.Auteur != livreRecu.Auteur)
                {
                    equivalent = false;
                    break;
                }
                if (livreAttendu.ISBN != livreRecu.ISBN)
                {
                    equivalent = false;
                    break;
                }
                if (livreAttendu.MaisonEdition != livreRecu.MaisonEdition)
                {
                    equivalent = false;
                    break;
                }
                if (livreAttendu.DatePublication != livreRecu.DatePublication)
                {
                    equivalent = false;
                    break;
                }
                if (livreAttendu.Description != livreRecu.Description)
                {
                    equivalent = false;
                    break;
                }
                if (livreAttendu.MoyenneEvaluation != livreRecu.MoyenneEvaluation)
                {
                    equivalent = false;
                    break;
                }
                if (livreAttendu.NmbEvaluation != livreRecu.NmbEvaluation)
                {
                    equivalent = false;
                    break;
                }
            }

            Assert.That(equivalent.Equals(true));
        }

        [Test]
        public void SupprimerLivre()
        {
            supprimerVM.ISBN = "isbn_test";
            supprimerVM.SupprimerLivreCommand();
            var doc = XDocument.Load(cheminBiblio);

            var dernierLivre = doc.Root.Element("Livres").Elements("Livre").Last();
            var stringRetourne = dernierLivre.ToString();
            Assert.That(stringRetourne, Is.Not.EqualTo(stringAttendu));
        }

    }
}
