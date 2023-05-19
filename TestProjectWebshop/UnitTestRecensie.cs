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
    public class UnitTestRecensie
    {

        [TestMethod]
        public void VoegToe()
        {
            // Arrange
            RecensieStub recensieStub = new RecensieStub();
            Recensie recensie = new Recensie(recensieStub, "Hallo werkt het pls", 1, 1);
            int nrRowsSaved;


            // Act
            nrRowsSaved = recensie.VoegToe();

            


            // Assert
            Assert.AreEqual(1, nrRowsSaved);
            foreach (RecensieDTO recensieDTO in recensieStub.RecensieDTOs)
            {
                if (recensieDTO.recensie == recensie.Bericht)
                {
                    Assert.AreEqual(recensie.Bericht, recensieDTO.recensie, "Recensie wordt niet goed op geslagen in de database");
                    Assert.AreEqual(recensie.GadgetID, recensieDTO.GadgetID, "Recensie wordt niet goed op geslagen in de database");
                    Assert.AreEqual(recensie.Gebruiker, recensieDTO.GebruikerID, "Recensie wordt niet goed op geslagen in de database");
                }
            }
           


        }

       
    }
}
