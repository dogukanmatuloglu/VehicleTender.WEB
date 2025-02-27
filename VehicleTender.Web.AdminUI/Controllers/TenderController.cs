﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection;
using VehicleTender.Web.AdminUI.ApiServices.Services;
using VehicleTender.Web.AdminUI.Models.Car;
using VehicleTender.Web.AdminUI.Models.Car.CarFeatures.Body;
using VehicleTender.Web.AdminUI.Models.PageModel;
using VehicleTender.Web.AdminUI.Models.Stock;
using VehicleTender.Web.AdminUI.Models.Tender;
using VehicleTender.Web.AdminUI.Models.Token;

namespace VehicleTender.Web.AdminUI.Controllers
{
    public class TenderController : Controller
    {
        Token token = new Token();
        IHttpContextAccessor _httpContextAccessor;
        TenderService tenderService = new TenderService();
        public TenderController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Tender()
        {
            // return View(await tenderService.TenderList());
            List<GetTenderDTO> tenderListVM = new List<GetTenderDTO>() {
            new GetTenderDTO
                {
                    CreatedBy=3,
                    TenderId=3,
                    CreatedDate=DateTime.Now,
                    IndivudualOrCorparate="Kurumsal",
                    Statu="Başladı",
                    TenderName="Ford İhalesi"
                },
            new GetTenderDTO
                {
              CreatedBy=3,
                    TenderId=3,
                    CreatedDate=DateTime.Now,
                    IndivudualOrCorparate="Kurumsal",
                    Statu="Başladı",
                    TenderName="Ford İhalesi"
                }
            };
            return View(tenderListVM);
        }

        [HttpPost]
        public async Task<IActionResult> Tender(string? tenderName, string? isIndividual, string? statu)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            List<GetTenderDTO> testlikTenderList = new List<GetTenderDTO>();
            TenderDTO tenderDTO = new TenderDTO();
            if (tenderName != null || isIndividual != null || statu != null)
            {
                GetTenderDTO tender = new GetTenderDTO();
                tender.TenderStart = DateTime.Now;
                tender.Statu = "Başladı";
                tender.TenderId = 1;
                tender.IndivudualOrCorparate = "Bireysel";
                tender.TenderStart = DateTime.Now;
                tender.CreatedBy = 1;
                tender.CreatedDate = DateTime.Now;
                tender.TenderName = "İhaleAdi";
                testlikTenderList.Add(tender);
            }
            return View(testlikTenderList);
        }
        [HttpGet]
        public IActionResult NewTenderCreate()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            AddNewTenderDTO addNewTender = new AddNewTenderDTO();
            TenderCarPriceAndTenderCar tenderCarPriceAndTenderCar = new TenderCarPriceAndTenderCar();
            List<TenderCar> tenderCarList = new List<TenderCar>();
            tenderCarList.Add(new TenderCar()
            {
                CarId = 1,
                CarBrand = "Mercedes",
                CarModel = "CLA",
                CreatedBy = "Okan",
                CreatedDate = DateTime.Now,
                Statu = "Satışta",
                TenderStartCarPrice = 100,
                TenderMinumumCarPrice = 1500
            });
            tenderCarList.Add(new TenderCar()
            {
                CarId = 1,
                CarBrand = "Mercedes",
                CarModel = "CLA",
                CreatedBy = "Okan",
                CreatedDate = DateTime.Now,
                Statu = "Satışta",
                TenderStartCarPrice = 100,
                TenderMinumumCarPrice = 1500
            });
            //await tenderService.AddNewTender(token, addNewTender);
            addNewTender.GetAllCars = tenderCarList;
            return View(addNewTender);
        }

        [HttpPost]
        public async Task<IActionResult> NewTenderCreate(AddNewTenderDTO addNewTenderDTO)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            await tenderService.AddNewTender(token, addNewTenderDTO);
            return RedirectToAction("Tender");
        }

        [HttpGet]
        public IActionResult UpdateTender()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            UpdateTenderDTO updateTender = new UpdateTenderDTO();
            updateTender.CompanyName = "BilgeAdam";
            updateTender.TenderName = "Mercedes İhalesi";
            updateTender.IndividualOrCorparate = "Kurumsal";
            updateTender.Statu = "Başladı";
            updateTender.TenderStartDate = DateTime.Now;
            updateTender.TenderStartHour = DateTime.Now;
            updateTender.TenderEndTime = DateTime.Now;
            updateTender.TenderEndHour = DateTime.Now;
            updateTender.TenderCar = new List<TenderCar>();
            updateTender.TenderCar.Add(new TenderCar()
            {
                CarId = 1,
                CarBrand = "BMW",
                CarModel = "320",
                Statu = "Satışta",
                CreatedBy = "Okan",
                CreatedDate = DateTime.Now,
                TenderMinumumCarPrice = 100,
                TenderStartCarPrice = 150,
            });

            return View(updateTender);
        }

        [HttpPost]
        public IActionResult UpdateTender(int id)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            UpdateTenderDTO updateTender = new UpdateTenderDTO();
            //await tenderService.UpdateTender(token, updateTender);
            updateTender.CompanyName = "A";
            updateTender.TenderName = "Yeniİhale";
            updateTender.IndividualOrCorparate = "Bireysel";
            updateTender.Statu = "Başladı";
            updateTender.TenderStartDate = DateTime.Now;
            updateTender.TenderStartHour = DateTime.Now;
            updateTender.TenderEndTime = DateTime.Now;
            updateTender.TenderEndHour = DateTime.Now;
            updateTender.TenderCar = new List<TenderCar>();
            updateTender.TenderCar.Add(new TenderCar()
            {
                CarId = 1,
                CarBrand = "BMW",
                CarModel = "320",
                Statu = "Satışta",
                CreatedBy = "Okan",
                CreatedDate = DateTime.Now,
                TenderMinumumCarPrice = 100,
                TenderStartCarPrice = 150,
            });
            return RedirectToAction("Tender");
        }

        public async Task<IActionResult> DeleteTender(int id)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            await tenderService.DeleteTender(token, id);
            return RedirectToAction("Tender");
        }

    }
}
