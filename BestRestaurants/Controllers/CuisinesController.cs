using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Controllers
{
    public class CuisinesController : Controller
    {
      [HttpGet("/cuisines")]
      public ActionResult Index()
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View(allCuisines);
      }

      [HttpGet("/cuisines/new")]
      public ActionResult CreateForm()
      {
        return View();
      }

      [HttpPost("/cuisines")]
      public ActionResult Create()
      {
        Cuisine newCuisine = new Cuisine (Request.Form["new-name"]);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View("Index", allCuisines);
      }

      [HttpPost("/cuisines/delete")]
      public ActionResult DeleteAll()
      {
        Cuisine.DeleteAll();
        return View();
      }

      [HttpGet("/cuisines/{id}/update")]
      public ActionResult UpdateForm(int id)
      {
        Cuisine thisCuisine = Cuisine.Find(id);
        return View(thisCuisine);
      }

      [HttpPost("/cuisines/{id}/update")]
      public ActionResult Update(int id)
      {
        Cuisine thisCuisine = Cuisine.Find(id);
        thisCuisine.Edit(Request.Form["new-name"]);
        return RedirectToAction("Index");
      }

      [HttpGet("/cuisines/{id}/delete")]
      public ActionResult Delete(int id)
      {
        Cuisine thisCuisine = Cuisine.Find(id);
        thisCuisine.Delete();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View("Index", allCuisines);
      }

      [HttpGet("/cuisines/{id}/view")]
      public ActionResult ViewRestaurants(int id)
      {
        Cuisine thisCuisine = Cuisine.Find(id);
        List<Restaurant> allCuisineRestaurants = thisCuisine.GetRestaurants();
        return View(allCuisineRestaurants);
      }
    }
  }
