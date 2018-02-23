using System.Collections.Generic;
using System;
using BestRestaurants;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Review
  {
    private int _id;
    private string _rating;
    private string _title;
    private string _content;
    private int _restaurantId;

    public Review(string Rating, string Title, string Content, int RestaurantId, int Id = 0)
    {
      _id = Id;
      _rating = Rating;
      _title = Title;
      _content = Content;
      _restaurantId = RestaurantId;
    }

    public override bool Equals(System.Object otherReview)
    {
      if(!(otherReview is Review))
      {
        return false;
      }
      else
      {
        Review newReview = (Review) otherReview;
        bool idEquality = (this.GetId() == newReview.GetId());
        bool ratingEquality = (this.GetRating() == newReview.GetRating());
        bool titleEquality = (this.GetTitle() == newReview.GetTitle());
        bool contentEquality = (this.GetContent() == newReview.GetContent());
        bool restaurantEquality = (this.GetRestaurantId() == newReview.GetRestaurantId());
        return (idEquality && ratingEquality && titleEquality && contentEquality && restaurantEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetTitle().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetRating()
    {
      return _rating;
    }

    public void SetRating(string newRating)
    {
      _rating = newRating;
    }

    public string GetTitle()
    {
      return _title;
    }

    public void SetTitle(string newTitle)
    {
      _title = newTitle;
    }

    public string GetContent()
    {
      return _content;
    }

    public void SetContent(string newContent)
    {
      _content = newContent;
    }

    public int GetRestaurantId()
    {
      return _restaurantId;
    }

    public void Save()
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"INSERT INTO `reviews` (rating, title, content, restaurant_id) VALUES (@ReviewRating, @ReviewTitle, @ReviewContent, @ReviewRestaurantId);";

    MySqlParameter rating = new MySqlParameter();
    rating.ParameterName = "@ReviewRating";
    rating.Value = this._rating;
    cmd.Parameters.Add(rating);

     MySqlParameter title = new MySqlParameter();
     title.ParameterName = "@ReviewTitle";
     title.Value = this._title;
     cmd.Parameters.Add(title);

     MySqlParameter content = new MySqlParameter();
     content.ParameterName = "@ReviewContent";
     content.Value = this._content;
     cmd.Parameters.Add(content);

     MySqlParameter restaurantId = new MySqlParameter();
     restaurantId.ParameterName = "@ReviewRestaurantId";
     restaurantId.Value = this._restaurantId;
     cmd.Parameters.Add(restaurantId);

     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Review Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM `reviews` WHERE id = @thisId;";

     MySqlParameter thisId = new MySqlParameter();
     thisId.ParameterName = "@thisId";
     thisId.Value = id;
     cmd.Parameters.Add(thisId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int reviewId = 0;
     string reviewRating = "";
     string reviewTitle = "";
     string reviewContent = "";
     int reviewRestaurantId = 0;

     while (rdr.Read())
     {
       reviewId = rdr.GetInt32(0);
       reviewRating = rdr.GetString(1);
       reviewTitle = rdr.GetString(2);
       reviewContent = rdr.GetString(3);
       reviewRestaurantId = rdr.GetInt32(4);
     }

     Review foundReview= new Review(reviewRating, reviewTitle, reviewContent, reviewRestaurantId, reviewId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundReview;
    }

    // public List<Review> GetReviews()
    //  {
    //    List<Review> allReviewsByRestaurant = new List<Review> {};
    //    MySqlConnection conn = DB.Connection();
    //    conn.Open();
    //    var cmd = conn.CreateCommand() as MySqlCommand;
    //    cmd.CommandText = @"SELECT * FROM reviews WHERE restaurant_id = @restaurant_id;";
    //
    //    MySqlParameter restaurantId = new MySqlParameter();
    //    restaurantId.ParameterName = "@restaurant_id";
    //    restaurantId.Value = this._id;
    //    cmd.Parameters.Add(restaurantId);
    //
    //    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //    while(rdr.Read())
    //    {
    //      int reviewId = rdr.GetInt32(0);
    //      string reviewRating = rdr.GetString(1);
    //      string reviewTitle = rdr.GetString(2);
    //      string reviewContent = rdr.GetString(3);
    //      int reviewRestaurantId = rdr.GetInt32(4);
    //      Review newReview = new Review(reviewRating, reviewTitle, reviewContent, reviewRestaurantId, reviewId);
    //      allReviewsByRestaurant.Add(newReview);
    //    }
    //    conn.Close();
    //    if (conn != null)
    //    {
    //      conn.Dispose();
    //    }
    //    return allReviewsByRestaurant;
    //  }

  }
}
