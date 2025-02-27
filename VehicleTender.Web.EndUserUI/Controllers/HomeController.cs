﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VehicleTender.Web.EndUserUI.ApiService.Concrete;
using VehicleTender.WEB.EndUser.Validation;
using VehicleTender.WEB.UserDTO.Concrete;
using VehicleTender.WEB.UserDTO.VM.Account;
using VehicleTender.WEB.UserDTO.VM.Contact;
using VehicleTender.WEB.UserDTO.VM.Expertise;
using VehicleTender.WEB.UserDTO.VM.HomeModel;

namespace VehicleTender.Web.EndUserUI.Controllers
{
    public class HomeController : Controller
    {
        Token token = new Token();

        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //Dikkat admin tarafından kaydedilen araçların resimleri hem enduserdan hem admin uı dan görüntülenmek üzere erişilmeli 

            List<HomeAdvertViewModel> list = new List<HomeAdvertViewModel>();
            HomeAdvertViewModel viewModel1 = new HomeAdvertViewModel();
            HomeAdvertViewModel viewModel2 = new HomeAdvertViewModel();
            HomeAdvertViewModel viewModel3 = new HomeAdvertViewModel();
            HomeAdvertViewModel viewModel4 = new HomeAdvertViewModel();
            viewModel1.AdvertHeader = "Doktordan Temiz Tempra Slx";
            viewModel1.KM = "1000";
            viewModel1.TransmissionType = "Düz";
            viewModel1.Photograph = "1.jpg";
            viewModel1.Model = "2000";

            viewModel2.AdvertHeader = "Öğretmenden Çiziksiz Bakımlı Tofaş";
            viewModel2.KM = "1324240";
            viewModel2.TransmissionType = "Otamatik";
            viewModel2.Photograph = @"2.jpg";
            viewModel2.Model = "2020";

            viewModel3.AdvertHeader = "Değişen Yok Hata Yook";
            viewModel3.KM = "231230";
            viewModel3.TransmissionType = "Düz";
            viewModel3.Photograph = @"3.jpg";
            viewModel3.Model = "1990";

            viewModel4.AdvertHeader = "Temiz Araba";
            viewModel4.KM = "23424";
            viewModel4.TransmissionType = "Düz";
            viewModel4.Photograph = @"4.jpg";
            viewModel4.Model = "1889";


            list.Add(viewModel1);
            list.Add(viewModel2);
            list.Add(viewModel4);
            list.Add(viewModel3);
            return View(list);
        }

        public ActionResult About()
        {
            if (HttpContext.Request.Cookies["token"]==null)
            {
                return RedirectToAction("Login","Home");
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Token token, ContactVM contactVM)
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ContactValidation contactValidation = new ContactValidation();
            ValidationResult result = contactValidation.Validate(contactVM);
            if (result.IsValid)
            {
              //  await contactService.Contact(token, contactVM);
                return RedirectToAction("Index");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }
        

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginVM loginVM)
        {
            LoginValidation loginValidation = new LoginValidation();
            ValidationResult result = loginValidation.Validate(loginVM);

            if (result.IsValid)
            {
                AccountService accountService = new AccountService();

                var token = await accountService.GetToken(loginVM);
                HttpCookie httpCookie = new HttpCookie("token");
                httpCookie.Expires = DateTime.Now.AddDays(1);
                httpCookie.Value = token.AccessToken;

                if (token == null)
                {
                    return View();
                }
                return RedirectToAction("/Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Token token, RegisterVM registerVM)
        {
            RegisterValidation registerValidation = new RegisterValidation();
            ValidationResult result = registerValidation.Validate(registerVM);

            if (result.IsValid)
            {
              //  await accountService.Register(token, registerVM);
                return RedirectToAction("Login");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Expertise(string city,string district)
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<ExpertiseVM> model = new List<ExpertiseVM>();
            //ExpertiseService expertiseService = new ExpertiseService();

            if (city!=null || district!=null)
            {
                //model= await expertiseService.GetAllExpertise(token, city + "," + district);
                ExpertiseVM expertiseVM = new ExpertiseVM()
                {
                    Address = "asddasd",
                    City = "sadasd",
                    District = "asdasd",
                    ExpertiseCompanyName = "searchoptionsisnotnull",
                    Id = 1,
                    Telephone = "asdasd"
                };
                model.Add(expertiseVM);
                
            }
            else
            {
                //model = await expertiseService.GetAllExpertise(token);
                ExpertiseVM expertiseVM = new ExpertiseVM()
                {
                    Address = "asddasd",
                    City = "sadasd",
                    District = "asdasd",
                    ExpertiseCompanyName = "searchoptionsisnull",
                    Id = 1,
                    Telephone = "asdasd"
                };
                model.Add(expertiseVM);

            }
            return View(model);
        }


        [HttpGet]
        public ActionResult CorporateRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CorporateRegister(Token token, CorporateRegisterVM corporateRegisterVM)
        {
            CorporateRegisterValidation corporateRegisterValidation = new CorporateRegisterValidation();
            ValidationResult result = corporateRegisterValidation.Validate(corporateRegisterVM);
            if (result.IsValid)
            {
             //   await accountService.CorporateRegister(token, corporateRegisterVM);
                return RedirectToAction("Login");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Token token, ResetPasswordVM resetPasswordVM)
        {
            ResetPasswordValidation resetPasswordValidation = new ResetPasswordValidation();
            ValidationResult result = resetPasswordValidation.Validate(resetPasswordVM);
            if (result.IsValid)
            {
            //    await accountService.ResetPassword(token, resetPasswordVM);
                return RedirectToAction("Login");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }


        [HandleError]
        public ActionResult ErrorPage404()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HandleError]
        public ActionResult ErrorPage500()
        {
            if (HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}