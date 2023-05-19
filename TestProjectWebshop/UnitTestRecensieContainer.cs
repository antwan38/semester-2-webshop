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
    public class UnitTestRecensieContainer
    {
        
        [TestMethod]
        public void LaadRecensies()
        {
            // Arrange
            RecensieStub recensiestub = new RecensieStub();
            RecensieContainer recensieContainer = new RecensieContainer(1, recensiestub);
            RecensieContainer recensieContainer1 = new RecensieContainer(7, recensiestub);
            List<RecensieContainer> recensieContainer2 = new List<RecensieContainer>();
            List<Recensie> recensies = new List<Recensie>();
            List<Recensie> recensies1 = new List<Recensie>();
            int index = 0;


            // Act
            recensies = recensieContainer.LaadRecensies();
            recensies1 = recensieContainer1.LaadRecensies();



            // Assert
            Assert.AreEqual(0, recensies1.Count);
            foreach (RecensieDTO recensieDTO in recensiestub.RecensieDTOs)
            {
                if (recensieDTO.GadgetID == 1)
                {
                    Assert.AreEqual(recensies[index].Bericht, recensieDTO.recensie, "Recensie wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(recensies[index].Gebruiker, recensieDTO.GebruikerID, "Recensie wordt niet goed op gehaalt uit de database");
                    Assert.AreEqual(recensies[index].GadgetID, recensieDTO.GadgetID, "Recensie wordt niet goed op gehaalt uit de database");
                    index++;
                }
                
            }
        }

        
        

    }
}
