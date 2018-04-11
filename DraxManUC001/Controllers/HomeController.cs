using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraxManUC001.Models;

namespace DraxManUC001.Controllers
{
	public class HomeController : Controller
	{
		DomainModel dm=new DomainModel();
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
        public ActionResult ListaProdotti(){ 
            return View();    
        }
        public ActionResult Dettaglio(){ 
            return View();        
        }
		public ActionResult Ricerca()
		{
			return View();
		}
        [HttpPost]
        public ActionResult AddToCar()
		{
			return View();
		}
        public ActionResult Detail()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Ricerca(string id,string descrizione)
		{
			int codice;
			if (id !="" && int.TryParse(id,out codice)) {
				Prodotto prodotto = dm.Search(codice);
				if (prodotto == null) {
					ViewBag.Message=$"La ricerca con il seguente codice {codice} non ha prodotto alcun risultato";
					return View();
				}
				ViewBag.Prodotto = prodotto;
				return View("Dettaglio");
			}else if (descrizione !="" ){ 
				List<Prodotto> prodotti = dm.Search(descrizione);
				if(prodotti == null) {
					ViewBag.Message ="Non è stato trovato alcun prodotto che corrisponda alla descrizione";
					return View();
				}
				ViewBag.Prodotti = prodotti;
				return View("ListaProdotti");
			} else {
				ViewBag.Message="Inserire almeno un parametro di ricerca";
				return View();
			}
		}
		public ActionResult AddToCar(int id,int? quantita) {
			Prodotto prodotto = dm.Search(id);
			if (prodotto != null) {
				if (quantita == null || quantita <= 0) {
					ViewBag.Message = $"Inserire la quantita deve essere maggiore di 1 ";
					ViewBag.Prodotto = prodotto;
					return View("OrderProduct");
				}
				List<Prodotto> prodotti = Session["products"] as List<Prodotto>;
				if (prodotti == null) {
					prodotti = new List<Prodotto>();
				}
				if (prodotti.Contains(prodotto)) {
					prodotti[prodotti.IndexOf(prodotto)].QtaOrdine += (int)quantita;
				} else {
					Prodotto aggiunto = new Prodotto { Id = id, Descrizione = prodotto.Descrizione, QtaOrdine = (int)quantita };
					prodotti.Add(aggiunto);
				}
				Session["products"] = prodotti;
				ViewBag.Message = "Elemento aggiunto al carrello";
			} else
				ViewBag.Message = "Prodotto non è stato trovato ";
			return View("Ricerca");
		}
}