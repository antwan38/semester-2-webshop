using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using Interfaces;
using System.Collections.Generic;

namespace TestProjectWebshop
{
    [TestClass]
    public class UnitTestGadgetContainer
    {
        [TestMethod]
        public void LaadGadgets()
        {

            // Arrange
            GadgetStub gadgetStub = new GadgetStub();
            GadgetContainer gadgetContainer = new GadgetContainer(gadgetStub);
            List<Gadget> Gadgets = new List<Gadget>();
            int index = 0;



            // Act
            Gadgets = gadgetContainer.LaadGadgets();
           
           

            // Assert
            foreach (Gadget gadget in Gadgets)
            {
                Assert.AreEqual(gadget.Naam, gadgetStub.gadgetDTOs[index].Naam, "Gadget wordt niet goed op gehaalt uit de database");
                Assert.AreEqual(gadget.Aantal, gadgetStub.gadgetDTOs[index].Aantal, "Gadget wordt niet goed op gehaalt uit de database");
                Assert.AreEqual(gadget.GadgetNummer, gadgetStub.gadgetDTOs[index].GadgetNummer, "Gadget wordt niet goed op gehaalt uit de database");
                Assert.AreEqual(gadget.Categorie, gadgetStub.gadgetDTOs[index].Categorie, "Gadget wordt niet goed op gehaalt uit de database");
                Assert.AreEqual(gadget.Beschrijving, gadgetStub.gadgetDTOs[index].Beschrijving, "Gadget wordt niet goed op gehaalt uit de database");
                Assert.AreEqual(gadget.Prijs, gadgetStub.gadgetDTOs[index].Prijs, "Gadget wordt niet goed op gehaalt uit de database");
                index++; 
            }
           
            
        }
        [TestMethod]
        public void Verwijder()
        {
            // Arrange
            GadgetStub gadgetStub = new GadgetStub();
            GadgetContainer gadgetContainer = new GadgetContainer(gadgetStub);
            int nrRowsDelete = 0;
            int nrRowsDelete1 = 0;




            // Act
            nrRowsDelete = gadgetContainer.Verwijder(2);
            nrRowsDelete1 = gadgetContainer.Verwijder(20);


            // Assert
            Assert.AreEqual(1, nrRowsDelete, "Er is geen gadget verwijderd");
            Assert.AreEqual(0, nrRowsDelete1, "Er is een gadget tegen onze wil verwijderd");
            foreach (GadgetDTO gadgetDTO in gadgetStub.gadgetDTOs)
            {
                if (gadgetDTO.GadgetNummer == 2)
                {
                    Assert.Fail();
                }
            }
            

        }
    }
}