


namespace webShopASP.Models
{
    using System.ComponentModel.DataAnnotations;
    public class GadgetViewModel
    {
        
        public int Aantal { get; set; }
        
        public string Prijs { get; set; }
        public string Beschrijving { get; set; }
        public string Categorie { get; set; }
        public string Naam { get; set; }
        public int GadgetNummer { get; set; }
        public GebruikerViewModel Verkoper { get; set; }
    }
}
