using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
  [TestClass]
  public class RestaurantTest : IDisposable
  {
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

    public void RestaurantTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Walk the dog.";
      string price = "$$";
      Restaurant newRestaurant = new Restaurant(name, price, 1);

      //Act
      string result = newRestaurant.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetPrice_ReturnsPrice_String()
    {
      //Arrange
      string newPrice = "$$";
      Restaurant newRestaurant = new Restaurant("Gino's", newPrice, 1);

      //Act
      string result = newRestaurant.GetPrice();

      //Assert
      Assert.AreEqual(newPrice, result);
    }

    [TestMethod]
    public void GetAll_ReturnsRestaurants_RestaurantList()
    {
      //Arrange
      string name01 = "Gino's";
      string price01 = "$$";
      string name02 = "Taco Hut";
      string price02 = "$";
      Restaurant newRestaurant1 = new Restaurant(name01, price01, 1);
      Restaurant newRestaurant2 = new Restaurant(name02, price02, 2);
      List<Restaurant> newList = new List<Restaurant> { newRestaurant1, newRestaurant2 };

      //Act
      newRestaurant1.Save();
      newRestaurant2.Save();
      List<Restaurant> result = Restaurant.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_RestaurantList()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Gino's", "$$", 1);

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Gino's", "$$", 1);
      testRestaurant.Save();

      //Act
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_True()
    {
      // Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Gino's", "$$", 1);
      Restaurant secondRestaurant = new Restaurant("Gino's", "$$", 1);

      // Assert
      Assert.AreEqual(firstRestaurant, secondRestaurant);
    }

    [TestMethod]
    public void Find_FindsRestaurantInDatabase_Restaurant()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Gino's", "$$", 1);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.AreEqual(testRestaurant, foundRestaurant);
    }

    [TestMethod]
    public void Edit_UpdatesRestaurantInDatabase_String()
    {
      //Arrange
      string firstName = "Gino's";
      string firstPrice = "$$";
      Restaurant testRestaurant = new Restaurant(firstName, firstPrice, 1);
      testRestaurant.Save();
      string secondName = "Taco Hut";
      string secondPrice = "$";

      //Act
      testRestaurant.Edit(secondName, secondPrice, 1);

      string nameResult = Restaurant.Find(testRestaurant.GetId()).GetName();
      string priceResult = Restaurant.Find(testRestaurant.GetId()).GetPrice();

      //Assert
      Assert.AreEqual(secondName , nameResult);
      Assert.AreEqual(secondPrice , priceResult);
    }

  }
}
