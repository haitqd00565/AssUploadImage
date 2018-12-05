using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssUploadImage.Models;
using Image = AssUploadImage.Models.Image;

namespace AssUploadImage.Controllers
{
    public class AccountController : Controller
    {
        public OrderImageEntities db = new OrderImageEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            //var password = user.Password;
            var account = db.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (account == null)
            {
                ViewBag.DuplicateMessage = "Tài khoản không tồn tại  hoặc đã bị xóa.";
                return View("Login");
            }
            else
            {
                ViewBag.SuccessMessage = "Đăng nhập thành công.";
                return View("UploadImage");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (db.Users.Any(x => x.Email == user.Email))
            {
                ViewBag.DuplicateMessage = "Tài khoản này đã tồn tại.";
                return View("Register");
            }

            db.Users.Add(user);
            db.SaveChanges();
            ViewBag.SuccessMessage = "Đăng kí thành công.";
            return View("Login");
        }
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            Image image = new Image();
            User user = new User();
            var path = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    //for checking uploaded file is image  or not
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                    || Path.GetExtension(file.FileName).ToLower() == ".png"
                    || Path.GetExtension(file.FileName).ToLower() == ".gif"
                    || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                         path = Path.Combine(Server.MapPath("~/Content/" + file.FileName));
                        image.Url = path;
                        
                        db.Images.Add(image);
                        file.SaveAs(path);
                        ViewBag.UploadSuccess = true;
                    }
                    db.SaveChanges();
                }
                

            }

            return View();
        }
        
        
    }
}