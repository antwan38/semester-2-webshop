using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webShopASP.Models;
using LogicLayer;
using webShopASP.Converter;
using DataAccessLayerWebshop;

namespace webShopASP.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public Gebruiker Gebruiker;
        private Interfaces.IGadgetDAL igadgetDAL;
        private Interfaces.IGebruikerDAL igebruikerDAL;
        private Interfaces.IRecensieDAL irecensieDAL;


        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
            DALFactory factoryDAL = new DALFactory();
            igadgetDAL = factoryDAL.GetGadgetDAL();
            igebruikerDAL = factoryDAL.GetGebruikerDAL();
            irecensieDAL = factoryDAL.GetRecensieDAL();
        }
        public IActionResult gotoLogIn()
        {
            if (HttpContext.Session.GetInt32("ID") == null)
            {
                ViewBag.msg = "Voer hier uw gegevens in.";
                return RedirectToAction("LogIn");
            }
            else
            {
                return RedirectToAction("Account");
            }
           
        }

        public IActionResult GoToRegistreer()
        {
            return View("Registreer");
        }
        [HttpPost]
        public IActionResult Registreer(GebruikerViewModel gebruikerViewModel, string Wachtwoord, string ConfirmWachtwoord)
        {
            try
            {
                if ((gebruikerViewModel.Naam != null && gebruikerViewModel.Naam != "") && (gebruikerViewModel.Emailadress != null && gebruikerViewModel.Emailadress != "") && (gebruikerViewModel.Postcode != null && gebruikerViewModel.Postcode != ""))
                {


                    if ((Wachtwoord != ConfirmWachtwoord) || (Wachtwoord == null || Wachtwoord == ""))
                    {
                        ViewBag.msg = "Het wachtwoord en de Confirm Wachtwoord zijn niet het zelfde.";
                        return View("Registreer", gebruikerViewModel);
                    }

                    GebruikerContainer gebruikerContainer = new GebruikerContainer(igebruikerDAL);
                    int status = gebruikerContainer.Registreer(ViewModelConverter.FromViewModel(gebruikerViewModel), Wachtwoord);
                    if (status == 1)
                    {
                        ViewBag.msg = "je account is met succes aangemaakt. Log nu in met je account";
                        return View("LogIn");
                    }
                    else if (status == 2)
                    {
                        ViewBag.msg = "Het ingevulde emailadres is al verbonden met een account. Als je al een account hebt ga naar de inlog pagina.";
                        return View("Registreer", gebruikerViewModel);
                    }
                    else
                    {
                        ViewBag.msg = "Het account kon niet worden aan gemaakt, probeer het later opnieuw.";
                        return View("Registreer", gebruikerViewModel);
                    }
                }
                ViewBag.msg = "Niet alle gegevens zijn correct ingevult.";
                return View("Registreer", gebruikerViewModel);
            }
            catch(TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }
        }

        public IActionResult Account()
        {
            try
            {
                GadgetContainer gadgetContainer = new GadgetContainer(igadgetDAL);
                List<GadgetViewModel> gadgetViewModels = new List<GadgetViewModel>();

                foreach (Gadget gadget in gadgetContainer.LaadGadgets())
                {
                    if (gadget.VerkoperID == HttpContext.Session.GetInt32("ID"))
                    {
                        gadgetViewModels.Add(ViewModelConverter.ToViewModel(gadget, igebruikerDAL));
                    }
                }


                Gebruiker gebruiker = new Gebruiker();
                gebruiker = gebruiker.KrijgGebruiker((int)HttpContext.Session.GetInt32("ID"), igebruikerDAL);



                ViewBag.Gebruiker = ViewModelConverter.ToViewModel(gebruiker);
                return View("Account", gadgetViewModels);
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }


        }

        public IActionResult LogIn()
        {
            return View("LogIn");
        }




        [HttpPost]
        public IActionResult LogIn(string Naam, string Wachtwoord)
        {
            try
            {
                GebruikerContainer user = new GebruikerContainer(igebruikerDAL);
                var gebruiker = user.Login(Naam, Wachtwoord);
                if (gebruiker.GebruikerID == null || gebruiker.GebruikerID == 0)
                {
                    ViewBag.msg = "Login Error, De ingevulde gegevens kloppen niet";
                    return View("LogIn");
                }
                else
                {

                    HttpContext.Session.SetString("Naam", gebruiker.Naam);
                    HttpContext.Session.SetInt32("ID", gebruiker.GebruikerID);
                    return RedirectToAction("Index", "Gadget");
                }
            }
            catch (TemporaryExceptionDAL ex)
            {
                return View("TemporaryError", ex);
            }
        }

        public IActionResult Uitloggen()
        {
            HttpContext.Session.Remove("ID");
            HttpContext.Session.Remove("Naam");
            return RedirectToAction("Index", "Gadget");
        }



        private List<GadgetViewModel> createGadgetViewList()
        {
            GadgetContainer gadgetContainer = new GadgetContainer(igadgetDAL);
            List<GadgetViewModel> gadgets = new List<GadgetViewModel>();
            if (gadgetContainer.LaadGadgets().Count == 0)
            {
                return gadgets;
            }
            
            foreach (Gadget gadget in gadgetContainer.LaadGadgets())
            {
                GadgetViewModel gadgetViewModel = ViewModelConverter.ToViewModel(gadget, igebruikerDAL);
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