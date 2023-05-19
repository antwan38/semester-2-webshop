using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using Interfaces;
using System.Collections.Generic;

namespace TestProjectWebshop
{
    [TestClass]
    public class UnitTestBestellingContainer
    {
        [TestMethod]
        public void LaadBestellingen()
        {

            // Arrange
            BestellingStub bestellingStub = new BestellingStub();
            GadgetStub gadgetStub = new GadgetStub();
            BestellingContainer bestellingContainer = new BestellingContainer(2, gadgetStub, bestellingStub);
            List<Bestelling> bestellings = new List<Bestelling>();
            int index = 0;
            int indexGadget = 0;


            // Act
            bestellings = bestellingContainer.LaadBestellingen();


            // Assert
            foreach (Bestelling bestelling in bestellings)
            {
                Assert.AreEqual(bestelling.BestellingNummer, bestellingStub.LaadBestellingen(2)[index].BestellingNummer, "Bestelling wordt niet goed op gehaalt uit de database");
                Assert.AreEqual(bestelling.GebruikerNummer, bestellingStub.LaadBestellingen(2)[index].GebruikerNummer, "Bestelling wordt niet goed op gehaalt uit de database");
                indexGadget = 0;
                foreach (Gadget gadget in bestelling.Gadgets)
                {
                    Assert.AreEqual(gadget.Naam, bestellingStub.LaadBestellingen(2)[index].GadgetDTOs[indexGadget].Naam, "Gadget wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gadget.Aantal, bestellingStub.LaadBestellingen(2)[index].GadgetDTOs[indexGadget].Aantal, "Gadget wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gadget.GadgetNummer, bestellingStub.LaadBestellingen(2)[index].GadgetDTOs[indexGadget].GadgetNummer, "Gadget wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gadget.Categorie, bestellingStub.LaadBestellingen(2)[index].GadgetDTOs[indexGadget].Categorie, "Gadget wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gadget.Beschrijving, bestellingStub.LaadBestellingen(2)[index].GadgetDTOs[indexGadget].Beschrijving, "Gadget wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(gadget.Prijs, bestellingStub.LaadBestellingen(2)[index].GadgetDTOs[indexGadget].Prijs, "Gadget wordt niet goed op gehaalt uit de database");
                    indexGadget++;
                }
                index++;
            }


        }
    }
}