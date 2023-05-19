using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webShopASP.Models;
using LogicLayer;
using System.Globalization;
using webShopASP.Converter;
using DataAccessLayerWebshop;

namespace webShopASP.Controllers
{
    public class WinkelwagenController : Controller
    {
        private readonly ILogger<WinkelwagenController> _logger;
        public Gebruiker Gebruiker;
        private Interfaces.IGadgetDAL iGadgetDAL;
        private Interfaces.IBestellingDAL iBestellingDAL;
        private Interfaces.IGebruikerDAL iGebruikerDAL;
        private Interfaces.IWinkelwagenDAL iIWinkelwagenDAL;



        public WinkelwagenController(ILogger<WinkelwagenController> logger)
        {
            _logger = logger;
            DALFactory factoryDAL = new DALFactory();
            iGadgetDAL = factoryDAL.GetGadgetDAL();
            iBestellingDAL = factoryDAL.GetBestellingDAL();
            iGebruikerDAL = factoryDAL.GetGebruikerDAL();
            iIWinkelwagenDAL = factoryDAL.GetWinkelwagenDAL();
        }

        public IActionResult AddToWinkelwagen(int GadgetID)
        {
            try
            {
                if (HttpContext.Session.GetInt32("ID") != null)
                {
                    WinkelwagenContainer winkelwagenContainer = new WinkelwagenContainer((int)HttpContext.Session.GetInt32("ID"), iIWinkelwagenDAL);
                    winkelwagenContainer.AddGadgetInWinkelwagen(GadgetID);
                    return RedirectToAction("Winkelwagen");
                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
                }
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }

        }

        public IActionResult BestelGadgetuitWinkelwagen(int[] ids)
        {
            try
            {
                WinkelwagenContainer winkelwagenContainer = new WinkelwagenContainer((int)HttpContext.Session.GetInt32("ID"), iIWinkelwagenDAL);
                Bestelling bestelling = new Bestelling(iBestellingDAL);
                List<Gadget> gadgets = new List<Gadget>();
                List<Gadget> alleGadgets = new List<Gadget>();
                List<Gadget> newGadgets = new List<Gadget>();
                int index = 0;
                foreach (Gadget gadget in winkelwagenContainer.LaadWinkelGadget())
                {
                    gadgets.Add(gadget.VeranderAantal(ids[index]));
                    index++;
                }
                GadgetContainer gadgetContainer = new GadgetContainer(iGadgetDAL);
                alleGadgets = gadgetContainer.LaadGadgets();
                int aantalGadgetsOver = 0;
                foreach (Gadget gadget in gadgets)
                {
                    foreach(Gadget alGadget in alleGadgets)
                    {
                        if(gadget.GadgetNummer == alGadget.GadgetNummer)
                        {
                            aantalGadgetsOver = alGadget.Aantal - gadget.Aantal;
                            if (aantalGadgetsOver < 0)
                            {
                                return RedirectToAction("Winkelwagen", new { bericht = $"Je bestelling is mislukt, Omdat er niet genoeg op voorraad is van:{gadget.Naam}, probeer het later opnieuw" });
                            }
                            
                            newGadgets.Add(gadget.VeranderAantal(aantalGadgetsOver));
                        }
                    }
                }

                if (bestelling.VoegToe(gadgets, (int)HttpContext.Session.GetInt32("ID")) == true)
                {
                    foreach(Gadget gadget in newGadgets)
                    {
                        if (gadget.Update(iGadgetDAL) != 1)
                        {
                            return RedirectToAction("Winkelwagen", new { bericht = "Je bestelling is mislukt, probeer het later opnieuw" });
                        }
                    }
                    return RedirectToAction("Bestellingen", "Gebruiker");
                }
                else
                {
                    return RedirectToAction("Winkelwagen", new { bericht = "Je bestelling is mislukt, probeer het later opnieuw" });
                }
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }

        }
        public IActionResult DeleteGadgetuitWinkelwagen(int GadgetID)
        {
            try
            {
                WinkelwagenContainer winkelwagenContainer = new WinkelwagenContainer((int)HttpContext.Session.GetInt32("ID"), iIWinkelwagenDAL);
                if (winkelwagenContainer.DeleteGadgetuitWinkelwagen(GadgetID) == 1)
                {
                    return RedirectToAction("Winkelwagen");
                }
                else
                {
                    return RedirectToAction("Winkelwagen", new { bericht = "Het verwijderen van de gadget uit je winkelwagen is mislukt, probeer het late opnieuw" });
                }
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }

        }
        public IActionResult Winkelwagen(string bericht)
        {
            try
            {
                if (HttpContext.Session.GetInt32("ID") != null)
                {
                    List<GadgetViewModel> gadgets = new List<GadgetViewModel>();
                    WinkelwagenContainer winkelwagenContainer = new WinkelwagenContainer((int)HttpContext.Session.GetInt32("ID"), iIWinkelwagenDAL);
                    foreach (Gadget gadget in winkelwagenContainer.LaadWinkelGadget())
                    {
                        GadgetViewModel gadgetViewModel = ViewModelConverter.ToViewModel(gadget, iGebruikerDAL);
                        gadgets.Add(gadgetViewModel);

                    }
                    ViewBag.msg = bericht;
                    return View("WinkelWagen", gadgets);
                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
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