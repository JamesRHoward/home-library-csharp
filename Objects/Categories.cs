using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class Categories
  {
    private int _id;
    private string _genre;

    public Categories(string genreName, int id = 0)
    {
      _id = id;
      _genre = genreName;
    }

    public void SetBookId(string newGenreName)
    {
      _genre = newGenreName;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetGenre()
    {
      return _genre;
    }

    public override bool Equals (System.Object otherCategories)
    {
      if (otherCategories is Categories)
      {
       Categories newCategories = (Categories) otherCategories;
       bool idEquality = (this.GetId() == newCategories.GetId());
       bool genreEquality = (this.GetGenre() == newCategories.GetGenre());
       return (idEquality && genreEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<Categories> GetAll()
    {
      List<Categories> allCategories = new List<Categories>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM categories;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int categoriesId = rdr.GetInt32(0);
        string categoriesGenre = rdr.GetString(1);
        Categories newCategory = new Categories (categoriesGenre, categoriesId);
        allCategories.Add(newCategory);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCategories;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO categories (genre) OUTPUT INSERTED.id VALUES (@CategoryGenre);", conn);
      SqlParameter genreParameter = new SqlParameter();
      genreParameter.ParameterName = "@CategoryGenre";
      genreParameter.Value = this.GetGenre();
      cmd.Parameters.Add(genreParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
       this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
       conn.Close();
      }
    }

    public static Categories Find (int queryCategoryId)
    {
      List<Categories> allCategories = new List<Categories> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM categories WHERE id = @CategoryId;", conn);
      SqlParameter genreParameter = new SqlParameter ();
      genreParameter.ParameterName = "@CategoryId";
      genreParameter.Value = queryCategoryId;
      cmd.Parameters.Add(genreParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string genre = rdr.GetString(1);
        Categories newCategories = new Categories (genre, id);
        allCategories.Add(newCategories);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCategories[0];
    }

    // public void DeleteThis()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlCommand cmd = new SqlCommand ("DELETE FROM owned_books WHERE id = @BookId;", conn);
    //   SqlParameter ownedBooksIdParameter = new SqlParameter ();
    //   ownedBooksIdParameter.ParameterName = "@BookId";
    //   ownedBooksIdParameter.Value = this.GetId();
    //   cmd.Parameters.Add(ownedBooksIdParameter);
    //   cmd.ExecuteNonQuery();
    // }
    //
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM categories;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
