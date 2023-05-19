using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;

namespace TestProjectWebshop
{
    [TestClass]
    public class UnitTestGebruiker
    {
        
        
        [TestMethod]
        public void KrijgGebruiker()
        {
            // Arrange
            GebruikerStub gebruikerStub = new GebruikerStub();
            Gebruiker gebruiker = new Gebruiker();
            Gebruiker gebruiker1 = new Gebruiker();
            int GebruikerID = 1;


            // Act
            gebruiker = gebruiker.KrijgGebruiker(GebruikerID, gebruikerStub);
            gebruiker1 = gebruiker.KrijgGebruiker(33, gebruikerStub);



            // Assert
            Assert.IsNull(gebruiker1.Naam);
            Assert.IsNull(gebruiker1.Emailadress);
            Assert.IsNull(gebruiker1.Postcode);
            Assert.IsFalse(gebruiker1.IsVerkoper);
            Assert.AreEqual(0, gebruiker1.GebruikerID);
            foreach (GebruikerDTO gebruikerDTO in gebruikerStub.gebruikerDTOs)
            {
                if (gebruikerDTO.GebruikerID == GebruikerID)
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
