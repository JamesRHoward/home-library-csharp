using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class BooksToSell
  {
    private int _id;
    private int _bookId;
    private bool _isSold;

    public BooksToSell(int bookId, bool isSold = false, int id = 0)
    {
      _id = id;
      _bookId = bookId;
      _isSold = isSold;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetBookId()
    {
      return _bookId;
    }
    public bool GetIsSold()
    {
      return _isSold;
    }
    public void SetBookId(int bookId)
    {
      _bookId = bookId;
    }
    public void SetIsSold(bool isSold)
    {
      _isSold = isSold;
    }
    public override bool Equals (System.Object otherBooksToSell)
    {
      if (otherBooksToSell is BooksToSell)
      {
       BooksToSell newBooksToSell = (BooksToSell) otherBooksToSell;
       bool idEquality = (this.GetId() == newBooksToSell.GetId());
       bool bookIdEquality = (this.GetBookId() == newBooksToSell.GetBookId());
       bool isSoldEquality = (this.GetIsSold() == newBooksToSell.GetIsSold());
       return (idEquality && bookIdEquality && isSoldEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<BooksToSell> GetAll()
    {
      List<BooksToSell> allBooksToSell = new List<BooksToSell>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM books_to_sell;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int booksToSellId = rdr.GetInt32(0);
        int booksToSellBookId = rdr.GetInt32(1);
        bool booksToSellIsSold = rdr.GetBoolean(2);
        BooksToSell newBooksToSell = new BooksToSell (booksToSellBookId, booksToSellIsSold, booksToSellId);
        allBooksToSell.Add(newBooksToSell);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBooksToSell;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO books_to_sell (book_id, sold_bool) OUTPUT INSERTED.id VALUES (@BooksToSellBookId, @BooksToSellIsSold);", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BooksToSellBookId";
      bookIdParameter.Value = this.GetBookId();
      SqlParameter isSoldParameter = new SqlParameter();
      isSoldParameter.ParameterName = "@BooksToSellIsSold";
      isSoldParameter.Value = this.GetIsSold();
      cmd.Parameters.Add(bookIdParameter);
      cmd.Parameters.Add(isSoldParameter);
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
    public void Update(bool isSold)
    {
     SqlConnection conn = DB.Connection();
     SqlDataReader rdr;
     conn.Open();

     SqlCommand cmd = new SqlCommand("UPDATE books_to_sell SET sold_bool = @BooksToSellIsSold OUTPUT INSERTED.sold_bool WHERE id = @BooksToSellId;", conn);

     SqlParameter updateIsSoldParameter = new SqlParameter();
     updateIsSoldParameter.ParameterName = "@BooksToSellIsSold";
     updateIsSoldParameter.Value = isSold;
     cmd.Parameters.Add(updateIsSoldParameter);

     SqlParameter booksToSellIdParameter = new SqlParameter();
     booksToSellIdParameter.ParameterName = "@BooksToSellId";
     booksToSellIdParameter.Value = this.GetId();
     cmd.Parameters.Add(booksToSellIdParameter);
     rdr = cmd.ExecuteReader();

     while(rdr.Read())
     {
       this._isSold = rdr.GetBoolean(0);
     }
     if (conn != null)
     {
       conn.Close();
     }
     if (rdr != null)
     {
       rdr.Close();
     }
   }

    public static BooksToSell Find (int queryBooksToSellId)
    {
      List<BooksToSell> allBooksToSell = new List<BooksToSell> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM books_to_sell WHERE id = @BooksToSellId;", conn);
      SqlParameter bookIdParameter = new SqlParameter ();
      bookIdParameter.ParameterName = "@BooksToSellId";
      bookIdParameter.Value = queryBooksToSellId;
      cmd.Parameters.Add(bookIdParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int bookId = rdr.GetInt32(1);
        bool isSold = rdr.GetBoolean(2);
        BooksToSell newBooksToSell = new BooksToSell (bookId, isSold, id);
        allBooksToSell.Add(newBooksToSell);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBooksToSell[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM books_to_sell WHERE id = @BooksToSellId;", conn);
      SqlParameter idParameter = new SqlParameter ();
      idParameter.ParameterName = "@BooksToSellId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM books_to_sell;", conn);
      cmd.ExecuteNonQuery();
    }

    public void SellBook()
    {
      // this.SetIsSold(true);
      this.Update(true);
    }
  }
}
