using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class Wishlist
  {
    private int _id;
    private int _bookId;

    public Wishlist(int bookId, int id = 0)
    {
      _id = id;
      _bookId = bookId;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetBookId()
    {
      return _bookId;
    }
    public void SetBookId(int bookId)
    {
      _bookId = bookId;
    }
    public override bool Equals (System.Object otherWishlist)
    {
      if (otherWishlist is Wishlist)
      {
       Wishlist newWishlist = (Wishlist) otherWishlist;
       bool idEquality = (this.GetId() == newWishlist.GetId());
       bool bookIdEquality = (this.GetBookId() == newWishlist.GetBookId());
       return (idEquality && bookIdEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<Wishlist> GetAll()
    {
      List<Wishlist> allWishlist = new List<Wishlist>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM wishlist;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int wishlistId = rdr.GetInt32(0);
        int wishlistBookId = rdr.GetInt32(1);
        Wishlist newWishlist = new Wishlist (wishlistBookId, wishlistId);
        allWishlist.Add(newWishlist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allWishlist;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO wishlist (book_id) OUTPUT INSERTED.id VALUES (@WishlistBookId);", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@WishlistBookId";
      bookIdParameter.Value = this.GetBookId();
      cmd.Parameters.Add(bookIdParameter);
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

    public static Wishlist Find (int queryWishlistId)
    {
      List<Wishlist> allWishlist = new List<Wishlist> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM wishlist WHERE id = @WishlistId;", conn);
      SqlParameter bookIdParameter = new SqlParameter ();
      bookIdParameter.ParameterName = "@WishlistId";
      bookIdParameter.Value = queryWishlistId;
      cmd.Parameters.Add(bookIdParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int bookId = rdr.GetInt32(1);
        Wishlist newWishlist = new Wishlist (bookId, id);
        allWishlist.Add(newWishlist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allWishlist[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM wishlist WHERE id = @WishlistId;", conn);
      SqlParameter idParameter = new SqlParameter ();
      idParameter.ParameterName = "@WishlistId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM wishlist;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
