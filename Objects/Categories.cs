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

    // public void Save()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlDataReader rdr = null;
    //   SqlCommand cmd = new SqlCommand ("INSERT INTO owned_books (book_id, physical_bool, storage_id) OUTPUT INSERTED.id VALUES (@OwnedBookBookId, @OwnedBookPhysicalBool, @OwnedBookStorageId);", conn);
    //   SqlParameter bookIdParameter = new SqlParameter();
    //   bookIdParameter.ParameterName = "@OwnedBookBookId";
    //   bookIdParameter.Value = this.GetBookId();
    //   SqlParameter physicalBoolParameter = new SqlParameter();
    //   physicalBoolParameter.ParameterName = "@OwnedBookPhysicalBool";
    //   physicalBoolParameter.Value = this.GetIsPhysical();
    //   SqlParameter storageIdParameter = new SqlParameter();
    //   storageIdParameter.Value = this.GetStorageId();
    //   storageIdParameter.ParameterName = "@OwnedBookStorageId";
    //   cmd.Parameters.Add(bookIdParameter);
    //   cmd.Parameters.Add(physicalBoolParameter);
    //   cmd.Parameters.Add(storageIdParameter);
    //   rdr = cmd.ExecuteReader();
    //   while (rdr.Read())
    //   {
    //    this._id = rdr.GetInt32(0);
    //   }
    //   if (rdr != null)
    //   {
    //    rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //    conn.Close();
    //   }
    // }
    //
    // public static OwnedBooks Find (int queryOwnedBooksBookId)
    // {
    //   List<OwnedBooks> allOwnedBooks = new List<OwnedBooks> {};
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlDataReader rdr = null;
    //   SqlCommand cmd = new SqlCommand ("SELECT * FROM owned_books WHERE book_id = @OwnedBookBookId;", conn);
    //   SqlParameter ownedBooksIdParameter = new SqlParameter ();
    //   ownedBooksIdParameter.ParameterName = "@OwnedBookBookId";
    //   ownedBooksIdParameter.Value = queryOwnedBooksBookId;
    //   cmd.Parameters.Add(ownedBooksIdParameter);
    //   rdr = cmd.ExecuteReader();
    //   while (rdr.Read())
    //   {
    //     int ownedBooksId = rdr.GetInt32(0);
    //     int ownedBooksBookId = rdr.GetInt32(1);
    //     bool ownedBooksIsPhysical = rdr.GetBoolean(2);
    //     int ownedBooksStorageId = rdr.GetInt32(3);
    //     OwnedBooks newOwnedBooks = new OwnedBooks (ownedBooksBookId, ownedBooksIsPhysical, ownedBooksStorageId, ownedBooksId);
    //     allOwnedBooks.Add(newOwnedBooks);
    //   }
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return allOwnedBooks[0];
    // }
    //
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
    // public static void DeleteAll()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlCommand cmd = new SqlCommand ("DELETE FROM owned_books;", conn);
    //   cmd.ExecuteNonQuery();
    // }

  }
}
