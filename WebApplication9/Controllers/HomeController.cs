using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        string fileName; // переменная путь файла
        // GET: Home
        private Models.ManDBEntities5 db = new Models.ManDBEntities5();//Модель Бд
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
            var items = db.Man;
            return View(items);
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
        public ActionResult Form(string LastName,string FirstName, string Parcicle, string Datebith,string DateDead,string NumUch,string NumMog,string opis,string category, HttpPostedFileBase upload)//обработка данных страницы
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
         
            Man man = new Man // Объявлем класс
            {
                Имя = FirstName,
                Фамилия = LastName,
                Отчество = Parcicle,
                Дата_рождения = Datebith,
                Дата_смерти = DateDead,
                Номер_участка = NumUch,
                Номер_могилы = NumMog,
                Описание = opis,
                Категория = category,
                Фотография = "/Content/images/Photo/" + fileName,
                search = FirstName + " " + LastName + " " + Parcicle + " " + Datebith + " " + DateDead + " " + NumUch + " " + NumMog + " " + opis + " "+category
            }; 

            db.Man.Add(man);//Запись в бд
            db.SaveChanges();//Сохранение
            return RedirectToAction("Search");//Возврат на страницу
        }

        [HttpGet]//Отдающий метод
        public ActionResult Find(string findtext)//метод поиска по бд
        {
            var men = from s in db.Man//прописываем запрос
                      select s;
            if (!String.IsNullOrEmpty(findtext))
            {
                men = men.Where(s => s.search.Contains(findtext));//Запрос поиска
            }
            return View(men.ToList());// вывод содержимого
        }
    }
}