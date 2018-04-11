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
		public ActionResult Ricerca()
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
	}
}