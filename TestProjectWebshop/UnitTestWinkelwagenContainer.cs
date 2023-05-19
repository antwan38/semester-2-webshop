using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProjectWebshop;

namespace TestProjectWebshop
{
    [TestClass]
    public class UnitTestWinkelwagenContainer
    {
        [TestMethod]
        public void DeleteGadgetuitWinkelwagen()
        {
            // Arrange
            WinkelwagenStub winkelwagenStub = new WinkelwagenStub();
            WinkelwagenContainer WinkelwagenContainer = new WinkelwagenContainer(1, winkelwagenStub);
            int gadgetID = 2;
            int gadgetID1 = 4;
            int nrRowsDelete;
            int nrRowsDelete1;




            // Act
            nrRowsDelete = WinkelwagenContainer.DeleteGadgetuitWinkelwagen(gadgetID);
            nrRowsDelete1 = WinkelwagenContainer.DeleteGadgetuitWinkelwagen(gadgetID1);



            // Assert
            Assert.AreEqual(1, nrRowsDelete, "er is niks verwijdert");
            Assert.AreEqual(404, nrRowsDelete1, "er is wat verwijdert");
            foreach (int ID in winkelwagenStub.Gebruiker1)
            {
                if (ID == 2)
                {
                    Assert.Fail();
                }
            }

            
        }
        [TestMethod]
        public void AddGadgetInWinkelwagen()
        {
            // Arrange
            WinkelwagenStub winkelwagenStub = new WinkelwagenStub();
            int gebruiker = 1;
            int gebruiker1 = 6;
            WinkelwagenContainer WinkelwagenContainer = new WinkelwagenContainer(gebruiker, winkelwagenStub);
            WinkelwagenContainer WinkelwagenContainer1 = new WinkelwagenContainer(gebruiker1, winkelwagenStub);
            int gadgetID = 2;
            int gadgetID1 = 4;
            int nrRowsSaved;
            int nrRowsSaved1;




            // Act
            nrRowsSaved = WinkelwagenContainer.AddGadgetInWinkelwagen(gadgetID);
            nrRowsSaved1 = WinkelwagenContainer1.AddGadgetInWinkelwagen(gadgetID1);



            // Assert
            Assert.AreEqual(1, nrRowsSaved, "er is niks toegevoegt");
            Assert.AreEqual(404, nrRowsSaved1, "er is wat toegevoegt");
            Assert.AreEqual(gadgetID, winkelwagenStub.ReturnLastAddedItem(gebruiker));

        }
        [TestMethod]
        public void LaadWinkelGadget()
        {
            // Arrange
            WinkelwagenStub winkelwagenStub = new WinkelwagenStub();
            WinkelwagenContainer WinkelwagenContainer = new WinkelwagenContainer(1, winkelwagenStub);
            WinkelwagenContainer WinkelwagenContainer1 = new WinkelwagenContainer(5, winkelwagenStub);
            List<Gadget> gadgets = new List<Gadget>();
            int index = 0;
     
     
            // Act
            gadgets = WinkelwagenContainer.LaadWinkelGadget();



            // Assert
            Assert.IsNull(WinkelwagenContainer1.LaadWinkelGadget());
            foreach (Gadget gadget in gadgets)
            {
                Assert.AreEqual(gadget.GadgetNummer, winkelwagenStub.Gebruiker1[index], "Gadget wordt niet goed op gehaalt uit de database");
                index++;
            }
            
           
        }
    }
}
