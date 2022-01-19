using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Controllers
{
    public class FlowerController : Controller
    {
        private readonly AppDbContext _context;
        public FlowerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Details(int id,int categoryId)
        {
            Flower flower = _context.Flowers.Include(f=>f.Campaigns).Include(f=>f.FlowerCategories).ThenInclude(fc=>fc.Category).Include(f=>f.FlowerTags).ThenInclude(ft=>ft.Tag).Include(f=>f.FlowerImages).FirstOrDefault(f => f.Id == id);
            if (flower == null) return NotFound();
            ViewBag.RelatedFlowers = _context.Flowers.Include(f => f.FlowerCategories).ThenInclude(f=>f.Category).Include(f=>f.Campaigns).Include(f=>f.FlowerImages).Where(f=>f.FlowerCategories.FirstOrDefault().CategoryId==categoryId).Take(4).ToList();
            //List<Flower> flowers = _context.Flowers.Include(f => f.FlowerCategories).ThenInclude(f => f.Category).Include(f => f.Campaigns).Include(f => f.FlowerImages).Where(f => f.FlowerCategories.FirstOrDefault().CategoryId == categoryId).Take(4).ToList();
            //return Json(flowers);
            return View(flower);
        }
        public IActionResult AddBasket(int id)
        {
            Flower flower = _context.Flowers.Include(f=>f.Campaigns).FirstOrDefault(f => f.Id == id);

            string basket = HttpContext.Request.Cookies["Basket"];
            //List<Flower> flowers;
            if (basket==null)
            {
                BasketVM basketVM = new BasketVM
                {
                    BasketItems = new List<BasketItemVM>(),
                    Count = 1,
                    TotalPrice = 0
                };

                BasketItemVM basketItem = new BasketItemVM
                {
                    Flower = flower,
                    Count = 1,
                };

                basketVM.BasketItems.Add(basketItem);
                if (flower.CampaignId==null)
                {
                    basketVM.TotalPrice =flower.Price;
                }
                else
                {
                    basketVM.TotalPrice = flower.Price * (100 - flower.Campaigns.DiscountPercent) / 100;
                }
                Math.Round(basketVM.TotalPrice, 3);
                string basketStr = JsonConvert.SerializeObject(basketVM);
                HttpContext.Response.Cookies.Append("Basket", basketStr);
                //string basketStr = JsonConvert.SerializeObject(basketVM);
                //HttpContext.Response.Cookies.Append("Basket", basketStr);

                //flowers = new List<Flower>();
                //flowers.Add(flower);
            }
            else
            {
                BasketVM basketVM = JsonConvert.DeserializeObject<BasketVM>(basket);
                BasketItemVM basketItem = basketVM.BasketItems.FirstOrDefault(b => b.Flower.Id == flower.Id);
                if (basketItem ==null)
                {
                    basketItem = new BasketItemVM
                    {
                        Flower = flower,
                        Count = 1
                    };
                    basketVM.BasketItems.Add(basketItem);
                    basketVM.Count++;
                }
                else
                {
                    basketItem.Count++;
                    basketVM.BasketItems.Add(basketItem);
                }
                if (flower.CampaignId == null)
                {
                    basketVM.TotalPrice += basketItem.Flower.Price;
                }
                else
                {
                    basketVM.TotalPrice += basketItem.Flower.Price * (100 - basketItem.Flower.Campaigns.DiscountPercent) / 100;
                }
                //flowers = JsonConvert.DeserializeObject<List<Flower>>(basket);
                Math.Round(basketVM.TotalPrice, 3);
                string basketStr = JsonConvert.SerializeObject(basketVM);
                HttpContext.Response.Cookies.Append("Basket", basketStr);

                //flowers.Add(flower);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ShowBasket()
        {
            string basketStr = HttpContext.Request.Cookies["Basket"];
            //if (string.IsNullOrEmpty(basketStr))
            
            if (!string.IsNullOrEmpty(basketStr))
            {
                BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                return Json(basket);
            }
            return Content("Basket is empty");

            //return Content("Basket is empty");
        }
        //public IActionResult SetCookie(int id)
        //{
        //    Flower flower = _context.Flowers.FirstOrDefault(f => f.Id == id);
        //    HttpContext.Response.Cookies.Append("Cookie", flower.Name);
        //    return RedirectToAction("Index", "Home");
        //}
        //public IActionResult ShowCookie()
        //{
        //    return Content(HttpContext.Request.Cookies["Cookie"]);
        //}
        //public IActionResult DeleteCookie(string key)
        //{
        //    HttpContext.Response.Cookies.Delete(key);
        //    return RedirectToAction("Index", "Home");
        //}

    }
}
