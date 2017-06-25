using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalWendr.Models;

namespace LocalWendr.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            ViewBag.radius = 5;
            return View();
        }

        public double radians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        public List<Seller> shopNearMe(double currLat, double currLng,double radius,List<Seller> Sl)
        {
            List<Seller> VM = new List<Seller>();
            for (var i = 0; i < Sl.Count; i++)
            {
                var Slr = distFrom(currLat, currLng, Sl[i].Latitude.Value, Sl[i].Longitude.Value);

                if (Slr < radius)
                {
                    VM.Add(Sl[i]);
                }
            }
            return VM;
        }
        public double distFrom(double Currentlat, double Currentlng, double Destlat, double Destlng)
        {
            var earthRadius = 3958.75;
            var dLat = radians(Destlat - Currentlat);
            var dLng = radians(Destlng - Currentlng);
            var sindLat = Math.Sin(dLat / 2);
            var sindLng = Math.Sin(dLng / 2);
            var a = Math.Pow(sindLat, 2) + Math.Pow(sindLng, 2)
                    * Math.Cos(radians(Currentlat)) * Math.Cos(radians(Destlat));
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var dist = earthRadius * c;
            return dist;
        }
        [HttpPost]
        public ActionResult SearchByCategory(string category, string search_param, double lat, double lan,double radius)
        {
            LocalWendrEntities LEW = new LocalWendrEntities();
            List<ViewModel> VMList = new List<ViewModel>();
            List<ProductDetail> RangedProducts;
            var SellersList = LEW.Sellers.ToList();
            var InRangeSellers=shopNearMe(lat, lan, radius, SellersList);
            if(string.IsNullOrEmpty(category))
            {
                RangedProducts = LEW.ProductDetails.Where(s=>s.Product_Name.Contains(search_param)).ToList();
            }
            else
            {
                RangedProducts = LEW.ProductDetails.Where(s => s.Category == category && s.Product_Name.Contains(search_param)).ToList();
            }
            
            foreach(var x in RangedProducts)
            {
                foreach(var y in InRangeSellers)
                {
                    if(x.Seller_Id==y.Seller_Id)
                    {
                        VMList.Add(CopyToViewModel(x, y));
                    }
                }
            }
            ViewBag.radius = radius;
            return View(VMList);
        }

        public ViewModel CopyToViewModel(ProductDetail PD, Seller S)
        {
            ViewModel Vm = new ViewModel();
            Vm.Seller_Id = PD.Seller_Id;
            Vm.Product_Name = PD.Product_Name;
            Vm.ImageName= PD.Product_Name + ".jpg"; ;
            Vm.Product_Id = PD.Product_Id;
            Vm.Product_Description = PD.Product_Description;
            Vm.Product_Price = PD.Product_Price;
            Vm.Availability = PD.Availability;
            Vm.Category = PD.Category;
            Vm.Latitude = S.Latitude;
            Vm.Longitude = S.Longitude;
            Vm.Store_Name = S.Store_Name;
            Vm.LandmarkName = S.LandmarkName;
            Vm.Location = S.Location;
            return Vm;
        }
    }
}