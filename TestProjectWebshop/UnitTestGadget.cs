using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using Interfaces;

namespace TestProjectWebshop
{
    [TestClass]
    public class UnitTestGadget
    {
        [TestMethod]
        public void VoegToe()
        {

            // Arrange
            GadgetStub gadgetStub = new GadgetStub();
            string description =
                "Dit is een test Gadget gemaakt door een unittest";
            int aantal = 28;
            string category = "testGadgets";
            string naam = "testGadget";
            Gadget gadget = new Gadget(gadgetStub, 2, description, aantal, naam, category, 78);


            // Act

            int nrRowsSaved = gadget.Voegtoe();
           

            // Assert
            Assert.AreEqual(1, nrRowsSaved, "Unexpected amount of rows saved.");
            Assert.AreEqual(gadget.Naam, gadgetStub.ReturnLastGadget().Naam, "Gadget wordt niet goed op geslagen in de database");
            Assert.AreEqual(gadget.Aantal, gadgetStub.ReturnLastGadget().Aantal, "Gadget wordt niet goed op geslagen in de database");
            Assert.AreEqual(gadget.GadgetNummer, gadgetStub.ReturnLastGadget().GadgetNummer, "Gadget wordt niet goed op geslagen in de database");
            Assert.AreEqual(gadget.Categorie, gadgetStub.ReturnLastGadget().Categorie, "Gadget wordt niet goed op geslagen in de database");
            Assert.AreEqual(gadget.Beschrijving, gadgetStub.ReturnLastGadget().Beschrijving, "Gadget wordt niet goed op geslagen in de database");
            Assert.AreEqual(gadget.Prijs, gadgetStub.ReturnLastGadget().Prijs, "Gadget wordt niet goed op geslagen in de database");

        }
        [TestMethod]
        public void Update()
        {
            
            // Arrange
            GadgetStub gadgetStub = new GadgetStub();
            string description =
                "Dit is een test Gadget gemaakt door een unittest";
            int aantal = 28;
            string category = "testGadgets";
            string naam = "testGadget";
            Gadget gadget = new Gadget(gadgetStub, 2, description, aantal, naam, category, (decimal)78.7, 1);
            Gadget gadget1 = new Gadget(gadgetStub, 2, description, aantal, naam, category, (decimal)78.7, 8);


            // Act
            int nrRowsUpdate = gadget.Update();
            int nrRowsUpdate1 = gadget1.Update();


            // Assert
            Assert.AreEqual(1, nrRowsUpdate, "Er is geen gadget geupdate");
            Assert.AreEqual(0, nrRowsUpdate1, "Er is tegen onze will in iets verandert");
            Assert.AreEqual(gadget.Naam, gadgetStub.ReturnGadgetByID(1).Naam, "Gadget wordt niet goed geupdate in de database");
            Assert.AreEqual(gadget.Aantal, gadgetStub.ReturnGadgetByID(1).Aantal, "Gadget wordt niet goed geupdate in de database");
            Assert.AreEqual(gadget.GadgetNummer, gadgetStub.ReturnGadgetByID(1).GadgetNummer, "Gadget wordt niet goed geupdate in de database");
            Assert.AreEqual(gadget.Categorie, gadgetStub.ReturnGadgetByID(1).Categorie, "Gadget wordt niet goed geupdate in de database");
            Assert.AreEqual(gadget.Beschrijving, gadgetStub.ReturnGadgetByID(1).Beschrijving, "Gadget wordt niet goed geupdate in de database");
            Assert.AreEqual(gadget.Prijs, gadgetStub.ReturnGadgetByID(1).Prijs, "Gadget wordt niet goed geupdate in de database");

        }
    }
}