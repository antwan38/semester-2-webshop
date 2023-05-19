using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webShopASP.Models;
using LogicLayer;
using System.Globalization;
using webShopASP.Converter;
using DataAccessLayerWebshop;

namespace webShopASP.Controllers
{
    public class GebruikerController : Controller
    {
        private readonly ILogger<GebruikerController> _logger;
        public Gebruiker Gebruiker;
        private Interfaces.IGadgetDAL iGadgetDAL;
        private Interfaces.IBestellingDAL iBestellingDAL;
        private Interfaces.IGebruikerDAL iGebruikerDAL;

        public GebruikerController(ILogger<GebruikerController> logger)
        {
            _logger = logger;
            DALFactory factoryDAL = new DALFactory();
            iGadgetDAL = factoryDAL.GetGadgetDAL();
            iBestellingDAL = factoryDAL.GetBestellingDAL();
            iGebruikerDAL = factoryDAL.GetGebruikerDAL();
        }

       
        public IActionResult Bestellingen()
        {
            try
            {
                List<GadgetViewModel> gadgets = new List<GadgetViewModel>();
                BestellingContainer bestellingContainer = new BestellingContainer((int)HttpContext.Session.GetInt32("ID"), iGadgetDAL, iBestellingDAL);
                foreach (Bestelling bestelling in bestellingContainer.LaadBestellingen())
                {

                    foreach (Gadget gadget in bestelling.Gadgets)
                    {
                        gadgets.Add(ViewModelConverter.ToViewModel(gadget, iGebruikerDAL));
                    }

                }

                return View("Bestellingen", gadgets);
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }
        }
       
       
      

       

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}