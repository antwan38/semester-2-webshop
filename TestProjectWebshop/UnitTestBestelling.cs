using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using Interfaces;
using System.Collections.Generic;

namespace TestProjectWebshop
{
    [TestClass]
    public class UnitTestBestelling
    {
        [TestMethod]
        public void VoegToe()
        {

            // Arrange
            BestellingStub bestellingStub = new BestellingStub();
            GadgetStub gadgetStub = new GadgetStub();
            List<Gadget> gadgets = new List<Gadget>();
            int gebruikerNr = 2;
            Bestelling bestelling = new Bestelling(bestellingStub);
            Gadget gadget = new Gadget(gadgetStub, 2, "bestelling unit test Gadget", 66, "Unittest", "Categorie", (decimal)4.6);
            Gadget gadget1 = new Gadget(gadgetStub, 2, "bestelling unit test Gadget nr 2", 99, "Unittest nr 2", "Categorie de tweede", (decimal)20.6);
            gadgets.Add(gadget);
            gadgets.Add(gadget1);
            bool nrRowsSaved;
            int index = 0;


            // Act
            nrRowsSaved = bestelling.VoegToe(gadgets, gebruikerNr);


            // Assert
            Assert.AreEqual(true, nrRowsSaved, "Bestelling wordt niet goed op geslagen in de database");
            Assert.AreEqual(bestelling.GebruikerNummer, bestellingStub.ReturnLastBestellingDTO().GebruikerNummer, "Bestelling wordt niet goed op geslagen in de database");
            foreach (GadgetDTO gadgetDTO in bestellingStub.ReturnLastBestellingDTO().GadgetDTOs)
            {
                Assert.AreEqual(gadgets[index].Aantal, gadgetDTO.Aantal, "Gadget wordt niet goed op geslagen in de database");
                Assert.AreEqual(gadgets[index].GadgetNummer, gadgetDTO.GadgetNummer, "Gadget wordt niet goed op geslagen in de database");
                Assert.AreEqual(gadgets[index].Categorie, gadgetDTO.Categorie, "Gadget wordt niet goed op geslagen in de database");
                Assert.AreEqual(gadgets[index].Beschrijving, gadgetDTO.Beschrijving, "Gadget wordt niet goed op geslagen in de database");
                Assert.AreEqual(gadgets[index].Prijs, gadgetDTO.Prijs, "Gadget wordt niet goed op geslagen in de database");
                index++;
            }


        }
    }
}