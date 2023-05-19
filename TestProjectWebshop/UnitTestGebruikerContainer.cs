using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProjectWebshop
{
    [TestClass]
    public class UnitTestGebruikerContainer
    {
        [TestMethod]
        public void Login()
        {

            // Arrange
            GebruikerStub gebruikerStub = new GebruikerStub();
            GebruikerContainer gebruikerContainer = new GebruikerContainer(gebruikerStub);
            Gebruiker gebruiker = new Gebruiker();
            Gebruiker gebruiker1 = new Gebruiker();
            string naam = "Antwan Bakker";
            string wachtwoord = "test";
            string naam1 = "IK besta niet";
            string wachtwoord1 = "dit klopt niet";



            // Act
            gebruiker = gebruikerContainer.Login(naam, wachtwoord);
            gebruiker1 = gebruikerContainer.Login(naam1, wachtwoord1);



            // Assert
            Assert.IsNull(gebruiker1.Naam);
            Assert.IsNull(gebruiker1.Emailadress);
            Assert.IsNull(gebruiker1.Postcode);
            Assert.IsFalse(gebruiker1.IsVerkoper);
            Assert.AreEqual(0, gebruiker1.GebruikerID);
            foreach (GebruikerDTO gebruikerDTO in gebruikerStub.gebruikerDTOs)
            {
                if (gebruikerDTO.Naam == naam)
                {

                    Assert.AreEqual(gebruiker.Naam, gebruikerDTO.Naam, "Gebruiker wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gebruiker.Emailadress, gebruikerDTO.Emailadress, "Gebruiker wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gebruiker.GebruikerID, gebruikerDTO.GebruikerID, "Gebruiker wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gebruiker.Postcode, gebruikerDTO.Postcode, "Gebruiker wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gebruiker.IsVerkoper, gebruikerDTO.IsVerkoper, "Gebruiker wordt niet goed op gehaalt uit de database");

                }
            }


        }
        [TestMethod]
        public void Registreer()
        {
            // Arrange
            GebruikerStub gebruikerStub = new GebruikerStub();
            GebruikerContainer gebruikerContainer = new GebruikerContainer(gebruikerStub);
            Gebruiker gebruiker = new Gebruiker("TestGebruiker", "test@gmail.com", "7421AJ", 0, false);
            Gebruiker gebruiker1 = new Gebruiker("TestGebruikerfout", "error@gmail.com", "74AJ", 0, false);
            int nrRowsSaved;
            int nrRowsSaved1;           
            string wachtwoord = "test";
            string wachtwoord1 = "";



            // Act
            nrRowsSaved = gebruikerContainer.Registreer(gebruiker, wachtwoord);
            nrRowsSaved1 = gebruikerContainer.Registreer(gebruiker1, wachtwoord1);



            // Assert
            Assert.AreEqual(1, nrRowsSaved);
            Assert.AreEqual(0, nrRowsSaved1);
            foreach (GebruikerDTO gebruikerDTO in gebruikerStub.gebruikerDTOs)
            {
                if (gebruikerDTO.Naam == gebruiker.Naam)
                {

                    Assert.AreEqual(gebruiker.Naam, gebruikerDTO.Naam, "Gebruiker wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gebruiker.Emailadress, gebruikerDTO.Emailadress, "Gebruiker wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gebruiker.GebruikerID, gebruikerDTO.GebruikerID, "Gebruiker wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gebruiker.Postcode, gebruikerDTO.Postcode, "Gebruiker wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gebruiker.IsVerkoper, gebruikerDTO.IsVerkoper, "Gebruiker wordt niet goed op gehaalt uit de database");

                }
            }
        }
    }
}
