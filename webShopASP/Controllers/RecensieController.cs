using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webShopASP.Models;
using LogicLayer;
using System.Globalization;
using webShopASP.Converter;
using DataAccessLayerWebshop;

namespace webShopASP.Controllers
{
    public class RecensieController : Controller
    {
        private readonly ILogger<RecensieController> _logger;
        public Gebruiker Gebruiker;
        private Interfaces.IGebruikerDAL igebruikerDAL;
        private Interfaces.IRecensieDAL irecensieDAL;

        public RecensieController(ILogger<RecensieController> logger)
        {
            _logger = logger;
            DALFactory factoryDAL = new DALFactory();
            igebruikerDAL = factoryDAL.GetGebruikerDAL();
            irecensieDAL = factoryDAL.GetRecensieDAL();
        }


        public IActionResult VoegToeRecensie(string bericht, int gadgetID)
        {
            try
            {
                if (bericht != null && bericht.Length >= 10)
                {
                    Recensie recensie = new Recensie(irecensieDAL, bericht, (int)HttpContext.Session.GetInt32("ID"), gadgetID);
                    if (recensie.VoegToe() == 1)
                    {
                        return RedirectToAction("GadgetPage", "Gadget", new { ID = gadgetID });
                    }
                    else
                    {
                        return RedirectToAction("GadgetPage", "Gadget", new { ID = gadgetID, bericht = "De recensie is niet toe gevoegt probeer het later opnieuw." });
                    }
                }
                else
                {
                    return RedirectToAction("GadgetPage", "Gadget", new { ID = gadgetID, bericht = "De recensie is niet lang genoeg, probeer het later opnieuw." });
                }
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