using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Karnel_Travels.Models;
using System.Data.Entity;
using System.Web.Security;
using System.Net.Mail;

namespace Karnel_Travels.Controllers
{
    public class KarnelTravelsController : Controller
    {
        database dbcon = new database();
        // GET: KarnelTravels
        public ActionResult Index()
        {
            var accco = dbcon.Accomadtion;
            var tour = dbcon.Tour.Take(1);
            int id = 2;
            var tour2 = dbcon.Tour.Where(m=>m.Tour_Id == id);

            TourViewModel tvm = new TourViewModel();
            tvm.Accomodations = accco.ToList();
            tvm.Tours = tour.ToList();
            tvm.Tours2 = tour2.ToList();

            var data = dbcon.Users.Where(m=>m.User_Type == "Owner");
            if (data.Count() == 1)
            {
                return View(tvm);
            }
            else
            {
                return RedirectToAction("Signupadmin");
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contactus(Contact cont)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                cont.FromEmail = "shayansameer07@gmail.com";


                mail.From = new MailAddress(cont.FromEmail);

                mail.Subject = cont.Subject;
                mail.Body = cont.Body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(cont.FromEmail, "aptech.123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return View();
            }
            else
            {
                return View("Contact", cont);
            }
        }


        //create system
        public ActionResult SignUp()
        {
            return View();
        }
        User usr = new User();

        [HttpPost]
        public ActionResult CreateCustomer(User cus)
        {
            if (ModelState.IsValid)
            {
                cus.User_Type = "Customer";
                dbcon.Users.Add(cus);
                dbcon.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return View("SignUp", cus);
            }
        }


        //logi system/////
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login lg)
        {
            if (ModelState.IsValid)
            {
                var data = dbcon.Users.SingleOrDefault(m => m.User_Email == lg.Email && m.User_Password == lg.Password);
                if (data != null)
                {
                    if (data.User_Type == "Account")
                    {
                        FormsAuthentication.SetAuthCookie(data.User_Id.ToString(), true);
                        return RedirectToAction("Account");
                    }
                    else if (data.User_Type == "Admin" || data.User_Type == "Owner")
                    {
                        FormsAuthentication.SetAuthCookie(data.User_Id.ToString(), true);
                        return RedirectToAction("Account");
                    }
                    else
                    {
                        return new HttpStatusCodeResult(404, "Error Cant Find Your Type Please Login Again");
                    }
                }
                else
                {
                    return RedirectToAction("SignUpadmin");
                }
            }
            else
            {
                return View(lg);
            }
        }
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }

        public ActionResult Signupadmin()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name);
                var data = dbcon.Users.SingleOrDefault(m => m.User_Id == id);
                if (data != null)
                {
                    if (data.User_Type == "Owner")
                    {
                        return View();
                    }
                    else
                    {
                        return new HttpStatusCodeResult(404, "Error : You Cant Access This Page");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateAdmin(User adm)
        {
            if (ModelState.IsValid)
            {
                var data = dbcon.Users.Where(m => m.User_Type == "Owner");
                if (data.Count() == 0)
                {
                    adm.User_Type = "Owner";
                    dbcon.Users.Add(adm);
                    dbcon.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    adm.User_Type = "Admin";
                    dbcon.Users.Add(adm);
                    dbcon.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View("Signupadmin", adm);
        }

        public ActionResult Account()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name);
                if (id != 0)
                {
                    var user = dbcon.Users.Where(m => m.User_Id == id);
                    var accompodations = dbcon.Accomadtion_Booking.Include(m => m.Accomadtions).Where(m => m.User_Id == id);
                    var travels = dbcon.Travels_Booking.Include(m => m.Travels).Where(m => m.User_Id == id);
                    var tours = dbcon.Tour_Bookings.Include(m => m.Tours).Where(m => m.User_Id == id);
                    AccountViewModel tvm = new AccountViewModel();
                    tvm.Accomadtion_Book = accompodations.ToList();
                    tvm.Tour_Book = tours.ToList();
                    tvm.Travel_Books = travels.ToList();
                    tvm.Users = user.ToList();
                    return View(tvm);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }



        //tour system Coding
        public ActionResult Tours()
        {
            var data = dbcon.Tour;
            if (data != null || data.Count() > 0)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("AddTour");
            }
        }

        public ActionResult TourDetails(int? id)
        {
            string login = User.Identity.Name;
            if (login != null)
            {
                var tourdata = dbcon.Tour.Where(m => m.Tour_Id == id);
                var data = dbcon.Tour;

                TourDetailModel tvm = new TourDetailModel();
                tvm.Tours = data.ToList();
                tvm.Toursdata = tourdata.ToList();
                return View(tvm);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult AddTour()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var data = dbcon.Users.SingleOrDefault(m => m.User_Id == id);
            if (data.User_Id != 0)
            {
                if (data.User_Type == "Admin" || data.User_Type == "Owner")
                {
                    return View();
                }
                else
                {
                    return new HttpStatusCodeResult(404, "Error You Cant Access This Page");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
        public ActionResult getTourstByCity(string city)
        {
            if (city != "" && city!= null)
            {
                var tours = dbcon.Tour.Where(m => (m.Tour_City.StartsWith(city) || m.Tour_City.EndsWith(city)) && m.Tour_City.Contains(city));

                if (tours != null)
                {
                    return PartialView("_TourPartial", tours);
                }
            }
           
            return PartialView("_TourPartial", dbcon.Tour.ToList());
        }
        [HttpPost]
        public ActionResult addtour(Tour tr, HttpPostedFileBase ImagePath)
        {
            if (ImagePath != null)
            {
                string physicalpath = "~/Content/images/" + ImagePath.FileName;
                tr.Tour_Picture = physicalpath;
            }
            if (ModelState.IsValid && ImagePath != null)
            {
                ImagePath.SaveAs(Server.MapPath("~/Content/images/" + ImagePath.FileName));
                dbcon.Tour.Add(tr);
                dbcon.SaveChanges();
                return RedirectToAction("Tours");
            }
            else
            {
                return View("AddTour", tr);
            }
        }
        public ActionResult DeleteTour(int? id, Tour tr)
        {
            var tvm = dbcon.Tour.Find(id);
            dbcon.Tour.Remove(tvm);
            dbcon.SaveChanges();
            return RedirectToAction("Tours");
        }
        public ActionResult BookTour(string id , string adults , string kid , string date ,string price,Tour_Booking tb)
        {
            if (adults != null && kid != null && date != null && price != null)
            {

                tb.Date_Book = date;
                tb.No_Adults = Convert.ToInt32(kid);
                tb.No_Child = Convert.ToInt32(adults);
                tb.Price = price;
                int identity = Convert.ToInt32(id);
                tb.Tour_Id = Convert.ToInt32(id);
                tb.User_Id = Convert.ToInt32(User.Identity.Name);
                tb.Status = true;
                if(ModelState.IsValid)
                {
                    dbcon.Tour_Bookings.Add(tb);
                    dbcon.SaveChanges();
                    return RedirectToAction("Tours");
                }
                else
                {
                    return RedirectToAction("TourDetails",id);
                }
            }
            else
            {
                return RedirectToAction("TourDetails");
            }
        }








        //Accomadtion system Coding
        public ActionResult Accomadtion()
        {
            
            var data = dbcon.Accomadtion;
            if (data != null || dbcon.Accomadtion.Count() > 0)
            {
                return View("Accomadtion", data);
            }
            else
            {
                return RedirectToAction("AddAccomadtion");
            }
        }
        public ActionResult BookAccomadtion(string id, string adults, string kid, string date, string price, Accomadtion_Booking tb)
        {
            if (adults != null && kid != null && date != null && price != null)
            {

                tb.Date_Book = date;
                tb.No_Adults = Convert.ToInt32(kid);
                tb.No_Child = Convert.ToInt32(adults);
                tb.Price = price;
                int identity = Convert.ToInt32(id);
                tb.Accomadtion_Id = Convert.ToInt32(id);
                tb.User_Id = Convert.ToInt32(User.Identity.Name);
                tb.Status = true;
                if (ModelState.IsValid)
                {
                    dbcon.Accomadtion_Booking.Add(tb);
                    dbcon.SaveChanges();
                    return RedirectToAction("Accomadtion");
                }
                else
                {
                    return RedirectToAction("AccomadtionDetails", id);
                }
            }
            else
            {
                return RedirectToAction("AccomadtionDetails");
            }
        }

        public ActionResult AccomadtionDetails(int? id)
        {
            var tourdata = dbcon.Accomadtion.Where(m => m.Accomadtion_Id == id);
            var data = dbcon.Accomadtion;

            TourDetailModel tvm = new TourDetailModel();
            tvm.Accomadtions = data.ToList();
            tvm.Accomadtionsdata = tourdata.ToList();
            return View(tvm);
        }
        public ActionResult getAccomadtiontByCity(string city)
        {
            if (city != "" && city != null)
            {
                var accomadtions = dbcon.Accomadtion.Where(m => (m.Accomadtion_City.StartsWith(city) || m.Accomadtion_City.EndsWith(city)) && m.Accomadtion_City.Contains(city));

                if (accomadtions != null)
                {
                    return PartialView("_AccomadtionPartial", accomadtions);
                }
            }

            return PartialView("_AccomadtionPartial", dbcon.Accomadtion.ToList());
        }
        public ActionResult AddAccomadtion()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var data = dbcon.Users.SingleOrDefault(m => m.User_Id == id);
            if (data.User_Id != 0)
            {
                if (data.User_Type == "Admin" || data.User_Type == "Owner")
                {
                    return View();
                }
                else
                {
                    return new HttpStatusCodeResult(404, "Error You Cant Access This Page");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult DeleteAccomadtion(int? id, Tour tr)
        {
            var tvm = dbcon.Accomadtion.Find(id);
            dbcon.Accomadtion.Remove(tvm);
            dbcon.SaveChanges();
            return RedirectToAction("Accomadtion");
        }
        [HttpPost]
        public ActionResult AddAccomadtion(Accomadtion tr, HttpPostedFileBase picture)
        {
            if (picture != null)
            {
                string physicalpath = "~/Content/images/" + picture.FileName;
                tr.Accomadtion_Picture = physicalpath;
            }
            if (ModelState.IsValid && picture != null)
            {
                picture.SaveAs(Server.MapPath("~/Content/images/" + picture.FileName));
                dbcon.Accomadtion.Add(tr);
                dbcon.SaveChanges();
                return RedirectToAction("Accomadtion");
            }
            else
            {
                return View("AddAccomadtion", tr);
            }
        }




        //travel system Coding

        public ActionResult Travel()
        {
            var data = dbcon.Travels;
            if(data.Count() > 0)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("AddTravel");
            }
        }
        public ActionResult BookTravel(string id, string adults, string kid, string date, string price, Travel_Booking tb)
        {
            if (adults != null && kid != null && date != null && price != null)
            {

                tb.Date_Book = date;
                tb.No_Adults = Convert.ToInt32(kid);
                tb.No_Child = Convert.ToInt32(adults);
                tb.Price = price;
                int identity = Convert.ToInt32(id);
                tb.Travel_Id = Convert.ToInt32(id);
                tb.User_Id = Convert.ToInt32(User.Identity.Name);
                tb.Status = true;
                if (ModelState.IsValid)
                {
                    dbcon.Travels_Booking.Add(tb);
                    dbcon.SaveChanges();
                    return RedirectToAction("Travels");
                }
                else
                {
                    return RedirectToAction("TravelDetails", id);
                }
            }
            else
            {
                return RedirectToAction("TravelDetails");
            }
        }
        public ActionResult TravelDetails(int? id)
        {
            var tourdata = dbcon.Travels.Where(m => m.Travel_Id == id);
            var data = dbcon.Travels;

            TourDetailModel tvm = new TourDetailModel();
            tvm.Travel = data.ToList();
            tvm.Travelsdata = tourdata.ToList();
            return View(tvm);
        }
        public ActionResult DeleteTravel(int? id, Travel tr)
        {
            var tvm = dbcon.Travels.Find(id);
            dbcon.Travels.Remove(tvm);
            dbcon.SaveChanges();
            return RedirectToAction("Travel");
        }
        public ActionResult AddTravel()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name);
                var data = dbcon.Users.SingleOrDefault(m => m.User_Id == id);
                if (data != null)
                {
                    if (data.User_Type == "Admin" || data.User_Type == "Owner")
                    {
                        return View();
                    }
                    else
                    {
                        return new HttpStatusCodeResult(404, "Error You Cant Access This Page");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult AddTravel(Travel tr, HttpPostedFileBase picture)
        {

            if (picture != null)
            {
                string physicalpath = "~/Content/images/" + picture.FileName;
                tr.Travel_Picture = physicalpath;
            }
            if (ModelState.IsValid && picture != null)
            {
                picture.SaveAs(Server.MapPath("~/Content/images/" + picture.FileName));
                dbcon.Travels.Add(tr);
                dbcon.SaveChanges();
                return RedirectToAction("Travel");
            }
            else
            {
                return View(tr);
            }
        }
        public ActionResult getTravelByCity(string city)
        {
            if (city != "" && city != null)
            {
                var travel = dbcon.Travels.Where(m => (m.Travel_City.StartsWith(city) || m.Travel_City.EndsWith(city)) && m.Travel_City.Contains(city));

                if (travel != null)
                {
                    return PartialView("_TravelPartial", travel);
                }
            }

            return PartialView("_TravelPartial", dbcon.Travels.ToList());
        }

    }
}