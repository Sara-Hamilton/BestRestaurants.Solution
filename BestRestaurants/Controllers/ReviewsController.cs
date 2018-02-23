using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Controllers
{
  public class ReviewsController : Controller
  {

    [HttpGet("/reviews")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/reviews/new")]
    public ActionResult CreateForm()
    {
      List<Restaurant> allRestaurants = Restaurant.GetAll();
      return View(allRestaurants);
    }

    [HttpPost("/reviews")]
    public ActionResult Create()
    {
      Review newReview = new Review (Request.Form["new-rating"], Request.Form["new-title"], Request.Form["new-content"], Int32.Parse(Request.Form["new-restaurant"]));
      newReview.Save();
      int id = Int32.Parse(Request.Form["new-restaurant"]);
      return View("Index");
    }

  }
}
