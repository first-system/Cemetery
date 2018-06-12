using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;
using System.Data.Entity;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        string fileName; // переменная путь файла
                         // GET: Home

        public ActionResult Index()
        {
            return View();
        }//главная страница

        public ActionResult Map()
        {
            return View();
        }//Страница карта

        public ActionResult Contact()
        {
            return View();
        }//Страница контакт

        public ActionResult Article()
        {
            return View();
        }//Страница статьи

        public ActionResult Search()
        {
            List<Deceased> deceaseds;
            using (DataContext DB = new DataContext())
            {
                deceaseds = new List<Deceased>(DB.Deceaseds.Include(c=>c.Category).Include(a=>a.BurialPlace));
            }
            return View(deceaseds);
        }//страница поиска

        public ActionResult History()
        {
            return View();
        }//Страница истории 

        [HttpGet]
        public ActionResult Form(int item_id) //метод отображения процесса
        {
            ViewBag.item = item_id;
            return PartialView();
        }

        [HttpPost]//Принимаюший метод
        public ActionResult Form(string LastName, string FirstName, string Parcicle, DateTime Datebith, DateTime DateDead, int NumUch, int NumMog, string opis, string category, HttpPostedFileBase upload)//обработка данных страницы
        {
            //получаем файл
            if (upload != null)
            {
                fileName = System.IO.Path.GetFileName(upload.FileName);//получам путь файла               
                upload.SaveAs(Server.MapPath("~/Content/images/Photo/" + fileName));// сохраняем файл в папку Files в проекте
            }
            else
            {
                fileName = "notphoto.jpg";// Если файл отсутсвует, загрузить картинку, нет фото.
            }

            using (DataContext DB = new DataContext())
            {
                //Ищем могилу в базе,если существует - пусть будет она. Если нет - копаем
                var burplace = DB.BurialPlaces.FirstOrDefault(x => x.NArea == NumUch && x.NBurial == NumMog);
                if (burplace == null)
                    burplace = new BurialPlace { NArea = NumUch, NBurial = NumMog };

                Category categ = DB.Categories.FirstOrDefault(d => d.CategoryName == category);

                Deceased man = new Deceased // Объявляем класс
                {
                    FName = FirstName,
                    LName = LastName,
                    SName = Parcicle,
                    DOB = Datebith,
                    DateDeath = DateDead,
                    // Присобачил метод проверки: если место существует - то хороним
                    // там, или же создаем новое.
                    BurialPlace = burplace,
                    Description = opis,
                    //Надо присобачить выбор из раскрывающегося списка
                    Category = categ,
                    Photo = "~/Content/images/Photo/" + fileName,
                    Search = FirstName + " " + LastName + " " + Parcicle + " " + Datebith + " " + DateDead + " " + NumUch + " " + NumMog + " " + opis + " " + category
                };
                DB.Deceaseds.Add(man);//Запись в бд
                DB.SaveChanges();//Сохранение
            }
            return RedirectToAction("Search");//Возврат на страницу
        }

        [HttpGet]//Отдающий метод
        public ActionResult Find(string findtext)//метод поиска по бд
        {
            using (DataContext DB = new DataContext())
            {
                var men = from s in DB.Deceaseds//прописываем запрос
                          select s;
                if (!String.IsNullOrEmpty(findtext))
                {
                    men = men.Where(s => s.Search.Contains(findtext)).Include(c => c.Category).Include(a => a.BurialPlace);//Запрос поиска
                }
                return View(men.ToList());// вывод содержимого
            }
        }
    }
}