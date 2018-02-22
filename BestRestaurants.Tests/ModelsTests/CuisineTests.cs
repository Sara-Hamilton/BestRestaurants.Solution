using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
  [TestClass]
  public class CuisineTest : IDisposable
  {
    public void CuisineTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }

    [TestMethod]
     public void GetAll_CuisineEmptyAtFirst_0()
     {
       //Arrange, Act
       int result = Cuisine.GetAll().Count;

       //Assert
       Assert.AreEqual(0, result);
     }

     [TestMethod]
     public void Equals_ReturnsTrueForSameName_Cuisine()
     {
       //Arrange, Act
       Cuisine firstCuisine = new Cuisine("Italian");
       Cuisine secondCuisine = new Cuisine("Italian");

       //Assert
       Assert.AreEqual(firstCuisine, secondCuisine);
     }

     [TestMethod]
      public void Save_SavesCuisineToDatabase_CuisineList()
      {
        //Arrange
        Cuisine testCuisine = new Cuisine("Italian");
        testCuisine.Save();

        //Act
        List<Cuisine> result = Cuisine.GetAll();
        List<Cuisine> testList = new List<Cuisine>{testCuisine};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }

      [TestMethod]
       public void Save_DatabaseAssignsIdToCuisine_Id()
       {
         //Arrange
         Cuisine testCuisine = new Cuisine("Italian");
         testCuisine.Save();

         //Act
         Cuisine savedCuisine = Cuisine.GetAll()[0];

         int result = savedCuisine.GetId();
         int testId = testCuisine.GetId();

         //Assert
         Assert.AreEqual(testId, result);
      }

      [TestMethod]
      public void Find_FindsCuisineInDatabase_Cuisine()
      {
        //Arrange
        Cuisine testCuisine = new Cuisine("Italian");
        testCuisine.Save();

        //Act
        Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

        //Assert
        Assert.AreEqual(testCuisine, foundCuisine);
      }

      [TestMethod]
      public void GetItems_RetrievesAllItemsWithCuisine_RestaurantList()
      {
        //Arrange
        Cuisine testCuisine = new Cuisine("Italian");
        testCuisine.Save();

        //Act
        Restaurant firstRestaurant = new Restaurant("Gino's", "$$", testCuisine.GetId());
        firstRestaurant.Save();
        Restaurant secondRestaurant = new Restaurant("Taco Hut", "$", testCuisine.GetId());
        secondRestaurant.Save();

        List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
        List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

        //Assert
        CollectionAssert.AreEqual(testRestaurantList, resultRestaurantList);
      }
    }
  }
