using Cemetery.Models;
using Cemetery.Models.ViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication9.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // поиск пользователя в бд
                    User user = null;
                    using (DataContext db = new DataContext())
                    {
                        
                        Guid password = EncoderGuid.Encoder.GetHashString(model.Password);
                        user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == password);
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Login, true);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                        }
                    }
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                return new HttpNotFoundResult(Ex.Message + "\n" + Ex.TargetSite + "\n" + Ex.InnerException);
            }
        }

        //==============================Регистрация (пока закрыта)=======================================

        //[Authorize(Roles = "admin")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DataContext db = new DataContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login);

                    if (user == null)
                    {
                        // создаем нового пользователя
                        db.Users.Add(new User { Login = model.Login, Password = GetHashString(model.Password), RoleId = 2 });
                        db.SaveChanges();

                        Guid password = GetHashString(model.Password);
                        user = db.Users.Where(u => u.Login == model.Login && u.Password == password).FirstOrDefault();

                        // если пользователь удачно добавлен в бд
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Login, true);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        static Guid GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return new Guid(hash);
        }
    }
}