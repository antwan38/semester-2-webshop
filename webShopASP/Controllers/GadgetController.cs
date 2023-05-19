using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webShopASP.Models;
using LogicLayer;
using webShopASP.Converter;
using System.Globalization;
using DataAccessLayerWebshop;

namespace webShopASP.Controllers
{
    public class GadgetController : Controller
    {
        private readonly ILogger<GadgetController> _logger;
        public Gebruiker Gebruiker;
        private Interfaces.IGadgetDAL iGadgetDAL;
        private Interfaces.IBestellingDAL iBestellingDAL;
        private Interfaces.IGebruikerDAL iGebruikerDAL;
        private Interfaces.IWinkelwagenDAL iIWinkelwagenDAL;
        private Interfaces.IRecensieDAL iRecensieDAL;

        public GadgetController(ILogger<GadgetController> logger)
        {
            _logger = logger;
            DALFactory factoryDAL = new DALFactory();
            iGadgetDAL = factoryDAL.GetGadgetDAL();
            iBestellingDAL = factoryDAL.GetBestellingDAL();
            iGebruikerDAL = factoryDAL.GetGebruikerDAL();
            iIWinkelwagenDAL = factoryDAL.GetWinkelwagenDAL();
            iRecensieDAL = factoryDAL.GetRecensieDAL();

        }
        public IActionResult GadgetVerwijder(int GadgetID)
        {
            try
            {
                if (GadgetID != null)
                {


                    GadgetContainer gadgetContainer = new GadgetContainer(iGadgetDAL);
                    if (1 == gadgetContainer.Verwijder(GadgetID))
                    {
                        return RedirectToAction("gotoLogIn", "Account");
                    }
                    else
                    {
                        ViewBag.Info = "De gadget kon niet worden verwijdert, probeer het later opnieuw";
                        return View("NewGadget");
                    }

                }
                else
                {
                    ViewBag.Info = "Voer alle informatie juist in";
                    return View("NewGadget");
                }
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }
        }

        [HttpPost]
        public IActionResult InfoGadget(webShopASP.Models.GadgetViewModel gadgetViewModel)
        {
            try
            {
                if (gadgetViewModel.Beschrijving != null && gadgetViewModel.Aantal != null && gadgetViewModel.Naam != null && gadgetViewModel.Aantal != null && gadgetViewModel.Categorie != null)
                {
                    Gadget gadget = new Gadget(iGadgetDAL, (int)HttpContext.Session.GetInt32("ID"), gadgetViewModel.Beschrijving, gadgetViewModel.Aantal, gadgetViewModel.Naam, gadgetViewModel.Categorie, Convert.ToDecimal(gadgetViewModel.Prijs, new CultureInfo("en-US")), gadgetViewModel.GadgetNummer);
                    if (1 == gadget.Update())
                    {
                        return RedirectToAction("gotoLogIn", "Account");
                    }
                    else
                    {
                        ViewBag.Info = "De gadget kon niet worden verandert, probeer het later opnieuw";
                        return View("NewGadget");
                    }

                }
                else
                {
                    ViewBag.Info = "Voer alle informatie juist in";
                    return View("NewGadget");
                }
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }
        }

        [HttpPost]
        public IActionResult NewGadget(webShopASP.Models.GadgetViewModel gadgetViewModel)
        {
            try
            {
                if (gadgetViewModel.Beschrijving != null && gadgetViewModel.Aantal != null && gadgetViewModel.Naam != null && gadgetViewModel.Aantal != null && gadgetViewModel.Categorie != null)
                {
                    Gadget gadget = new Gadget(iGadgetDAL, (int)HttpContext.Session.GetInt32("ID"), gadgetViewModel.Beschrijving, gadgetViewModel.Aantal, gadgetViewModel.Naam, gadgetViewModel.Categorie, Convert.ToDecimal(gadgetViewModel.Prijs, new CultureInfo("en-US")));
                    if (1 == gadget.Voegtoe())
                    {
                        return RedirectToAction("gotoLogIn", "Account");
                    }
                    else
                    {
                        ViewBag.Info = "De gadget kon niet worden toegevoegd, probeer het later opnieuw";
                        return View("NewGadget");
                    }

                }
                else
                {
                    ViewBag.Info = "Voer alle informatie juist in";
                    return View("NewGadget");
                }
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }
        }

        public IActionResult GadgetInfo(int ID)
        {
            try
            {
                List<GadgetViewModel> gadgets = CreateGadgetViewList();
                foreach (GadgetViewModel gadget in gadgets)
                {
                    if (gadget.GadgetNummer == ID)
                    {
                        return View("GadgetInfo", gadget);
                    }
                }
                return RedirectToAction("Account", "Account");
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }

        }

        public IActionResult GadgetPage(int ID, string bericht)
        {
            try
            {
                List<GadgetViewModel> gadgets = CreateGadgetViewList();
                foreach (GadgetViewModel gadget in gadgets)
                {
                    if (gadget.GadgetNummer == ID)
                    {
                        RecensieContainer recensies = new RecensieContainer(ID, iRecensieDAL);
                        List<RecensieViewModel> recensiesList = new List<RecensieViewModel>();
                        foreach (Recensie recensie1 in recensies.LaadRecensies())
                        {
                            recensiesList.Add(ViewModelConverter.ToViewModel(recensie1, iGebruikerDAL));
                        }
                        ViewBag.Recensies = recensiesList;
                        ViewBag.msg = bericht;
                        return View("GadgetPage", gadget);
                    }
                }
                RecensieContainer recensie = new RecensieContainer(ID, iRecensieDAL);
                List<RecensieViewModel> recensieList = new List<RecensieViewModel>();
                foreach (Recensie recensie1 in recensie.LaadRecensies())
                {
                    recensieList.Add(ViewModelConverter.ToViewModel(recensie1, iGebruikerDAL));
                }
                ViewBag.Recensies = recensieList;
                return View("GadgetPage");
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }

        }

        public IActionResult VoegGadgetToe()
        {
            return View("NewGadget");
        }

        public IActionResult Index()
        {
            try
            {
                return View("Index", CreateGadgetViewList());
            }
            catch(TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }
        }

        private List<GadgetViewModel> CreateGadgetViewList()
        {
            GadgetContainer gadgetContainer = new GadgetContainer(iGadgetDAL);
            List<GadgetViewModel> gadgets = new List<GadgetViewModel>();
            if (gadgetContainer.LaadGadgets().Count == 0)
            {
                return gadgets;
            }

            foreach (Gadget gadget in gadgetContainer.LaadGadgets())
            {
                GadgetViewModel gadgetViewModel = ViewModelConverter.ToViewModel(gadget, iGebruikerDAL);
                gadgets.Add(gadgetViewModel);

            }
            return gadgets;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}