using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public LayoutServices(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor;
        }
        public BasketVM ShowBasket()
        {
            string basket = _httpContext.HttpContext.Request.Cookies["Basket"];
            BasketVM basketVM = new BasketVM();
            if (!string.IsNullOrEmpty(basket))
            {
                basketVM = JsonConvert.DeserializeObject<BasketVM>(basket);
            }
            return basketVM;
        }
    }
}
