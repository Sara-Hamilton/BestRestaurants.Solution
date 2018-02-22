using System.Collections.Generic;
using System;
using BestRestaurants;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _price;
    private int _cuisineId;

    public Restaurant(string Name, string Price, int CuisineId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _price = Price;
      _cuisineId = CuisineId;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool nameEquality = (this.GetName() == newRestaurant.GetName());
        bool priceEquality = (this.GetPrice() == newRestaurant.GetPrice());
        bool cuisineEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        return (idEquality && nameEquality && priceEquality && cuisineEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetPrice()
    {
      return _price;
    }

    public void SetPrice(string newPrice)
    {
      _price = newPrice;
    }

    public int GetCuisineId()
    {
      return _cuisineId;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantPrice = rdr.GetString(2);
        int restaurantCuisineId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantPrice, restaurantCuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allRestaurants;
    }

    public static void DeleteAll()
    {
    MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM restaurants;";

     cmd.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
      conn.Dispose();
     }
    }

    public void Save()
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"INSERT INTO `restaurants` (name, price, cuisine_id) VALUES (@RestaurantName, @RestaurantPrice, @RestaurantCuisineId);";

     MySqlParameter name = new MySqlParameter();
     name.ParameterName = "@RestaurantName";
     name.Value = this._name;
     cmd.Parameters.Add(name);

     MySqlParameter price = new MySqlParameter();
     price.ParameterName = "@RestaurantPrice";
     price.Value = this._price;
     cmd.Parameters.Add(price);

     MySqlParameter cuisineId = new MySqlParameter();
     cuisineId.ParameterName = "@RestaurantCuisineId";
     cuisineId.Value = this._cuisineId;
     cmd.Parameters.Add(cuisineId);

     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Restaurant Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM `restaurants` WHERE id = @thisId;";

     MySqlParameter thisId = new MySqlParameter();
     thisId.ParameterName = "@thisId";
     thisId.Value = id;
     cmd.Parameters.Add(thisId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int restaurantId = 0;
     string restaurantName = "";
     string restaurantPrice = "";
     int restaurantCuisineId = 0;

     while (rdr.Read())
     {
       restaurantId = rdr.GetInt32(0);
       restaurantName = rdr.GetString(1);
       restaurantPrice = rdr.GetString(2);
       restaurantCuisineId = rdr.GetInt32(3);
     }

     Restaurant foundRestaurant= new Restaurant(restaurantName, restaurantPrice, restaurantCuisineId, restaurantId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundRestaurant;
    }

    public void Edit (string newName, string newPrice, int newCuisineId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE restaurants SET name = @newName, cuisine_id = @newCuisineId, price = @newPrice WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      MySqlParameter cuisineId = new MySqlParameter();
      cuisineId.ParameterName = "@newCuisineId";
      cuisineId.Value = newCuisineId;
      cmd.Parameters.Add(cuisineId);

      MySqlParameter price = new MySqlParameter();
      price.ParameterName = "@newPrice";
      price.Value = newPrice;
      cmd.Parameters.Add(price);

      cmd.ExecuteNonQuery();
      _name = newName;
      _cuisineId = newCuisineId;
      _price = newPrice;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete (int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

  }
}
