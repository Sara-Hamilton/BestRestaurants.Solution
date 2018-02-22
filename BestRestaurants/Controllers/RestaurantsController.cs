using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Controllers
{
    public class RestaurantsController : Controller
    {

      [HttpGet("/restaurants")]
        public ActionResult Index()
        {
          List<Restaurant> allRestaurants = Restaurant.GetAll();
          return View(allRestaurants);
        }

        [HttpGet("/restaurants/new")]
        public ActionResult CreateForm()
        {
          List<Cuisine> allCuisines = Cuisine.GetAll();
          return View(allCuisines);
        }

        [HttpPost("/restaurants")]
        public ActionResult Create()
        {
          Restaurant newRestaurant = new Restaurant (Request.Form["new-name"], Request.Form["new-price"], Int32.Parse(Request.Form["new-cuisine"]));
          newRestaurant.Save();
          List<Restaurant> allRestaurants = Restaurant.GetAll();
          return View("Index", allRestaurants);
        }

        [HttpPost("/restaurants/delete")]
        public ActionResult DeleteAll()
        {
          Restaurant.DeleteAll();
          List<Restaurant> allRestaurants = Restaurant.GetAll();
          return View("Index", allRestaurants);
          // return View("Index");
        }

        [HttpGet("/restaurants/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
          Restaurant thisRestaurant = Restaurant.Find(id);
          Cuisine thisCuisine = Cuisine.Find(thisRestaurant.GetCuisineId());
          List<Cuisine> allCuisines = Cuisine.GetAll();
          Dictionary<string, object> restaurantDetails = new Dictionary <string, object>();
          restaurantDetails.Add("restaurant", thisRestaurant);
          restaurantDetails.Add("cuisines", allCuisines);
          return View(restaurantDetails);
        }

        [HttpPost("/restaurants/{id}/update")]
        public ActionResult Update(int id)
        {
          Restaurant thisRestaurant = Restaurant.Find(id);
          thisRestaurant.Edit(Request.Form["new-name"], Request.Form["new-price"], Int32.Parse(Request.Form["new-cuisine"]));
          return RedirectToAction("Index");
        }

        [HttpGet("/restaurants/{id}/delete")]
        public ActionResult Delete(int id)
        {
          Restaurant thisRestaurant = Restaurant.Find(id);
          thisRestaurant.Delete();
          List<Restaurant> allRestaurants = Restaurant.GetAll();
          return View("Index", allRestaurants);
        }
    }
  }
