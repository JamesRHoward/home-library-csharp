using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class LentBooks
  {
    private int _id;
    private int _ownedBookId;
    private bool _returnedBool;
    private string _recipient;

    public LentBooks(int ownedBookId, string recipient, bool returnedBool = false, int id = 0)
    {
      _id = id;
      _ownedBookId = ownedBookId;
      _returnedBool = returnedBool;
      _recipient = recipient;
    }

    public void SetBookId(int ownedBookId)
    {
      _ownedBookId = ownedBookId;
    }
    public void SetReturnedBool(bool returnedBool)
    {
      _returnedBool = returnedBool;
    }
    public void SetRecipient(string recipient)
    {
      _recipient = recipient;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetOwnedBookId()
    {
      return _ownedBookId;
    }
    public bool GetReturnedBool()
    {
      return _returnedBool;
    }
    public string GetRecipient()
    {
      return _recipient;
    }

    public override bool Equals (System.Object otherLentBooks)
    {
      if (otherLentBooks is LentBooks)
      {
       LentBooks newLentBooks = (LentBooks) otherLentBooks;
       bool idEquality = (this.GetId() == newLentBooks.GetId());
       bool ownedBookIdEquality = (this.GetOwnedBookId() == newLentBooks.GetOwnedBookId());
       bool returnedBoolEquality = (this.GetReturnedBool() == newLentBooks.GetReturnedBool());
       bool recipientEquality = (this.GetRecipient() == newLentBooks.GetRecipient());
       return (idEquality && ownedBookIdEquality && returnedBoolEquality && recipientEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<LentBooks> GetAll()
    {
      List<LentBooks> allLentBooks = new List<LentBooks>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM lent_books;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int lentBooksId = rdr.GetInt32(0);
        int lentBooksBookId = rdr.GetInt32(1);
        bool lentBooksReturnedBool = rdr.GetBoolean(2);
        string lentBooksRecipient = rdr.GetString(3);
        LentBooks newLentBook = new LentBooks (lentBooksBookId, lentBooksRecipient, lentBooksReturnedBool,  lentBooksId);
        allLentBooks.Add(newLentBook);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allLentBooks;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO lent_books (owned_book_id, returned_bool, recipient) OUTPUT INSERTED.id VALUES (@LentBookBookId, @LentBookReturnedBool, @LentBookRecipient);", conn);
      SqlParameter ownedBookIdParameter = new SqlParameter();
      ownedBookIdParameter.ParameterName = "@LentBookBookId";
      ownedBookIdParameter.Value = this.GetOwnedBookId();
      SqlParameter physicalBoolParameter = new SqlParameter();
      physicalBoolParameter.ParameterName = "@LentBookReturnedBool";
      physicalBoolParameter.Value = this.GetReturnedBool();
      SqlParameter recipientParameter = new SqlParameter();
      recipientParameter.Value = this.GetRecipient();
      recipientParameter.ParameterName = "@LentBookRecipient";
      cmd.Parameters.Add(ownedBookIdParameter);
      cmd.Parameters.Add(physicalBoolParameter);
      cmd.Parameters.Add(recipientParameter);
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

    public void Update(bool isReturned)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE lent_books SET returned_bool = @LentBooksReturnBool OUTPUT INSERTED.returned_bool WHERE id = @LentBooksId;", conn);

      SqlParameter updateReturnedBoolParameter = new SqlParameter();
      updateReturnedBoolParameter.ParameterName = "@LentBooksReturnedBool";
      updateReturnedBoolParameter.Value = isReturned;
      cmd.Parameters.Add(updateReturnedBoolParameter);

      SqlParameter lentBooksIdParameter = new SqlParameter();
      lentBooksIdParameter.ParameterName = "@LentBooksId";
      lentBooksIdParameter.Value = this.GetId();
      cmd.Parameters.Add(lentBooksIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._returnedBool = rdr.GetBoolean(0);
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

    public static LentBooks Find (int queryLentBooksBookId)
    {
      List<LentBooks> allLentBooks = new List<LentBooks> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM lent_books WHERE owned_book_id = @LentBookBookId;", conn);
      SqlParameter lentBooksIdParameter = new SqlParameter ();
      lentBooksIdParameter.ParameterName = "@LentBookBookId";
      lentBooksIdParameter.Value = queryLentBooksBookId;
      cmd.Parameters.Add(lentBooksIdParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int lentBooksId = rdr.GetInt32(0);
        int lentBooksOwnedBookId = rdr.GetInt32(1);
        bool lentBooksReturnedBool = rdr.GetBoolean(2);
        string lentBooksRecipient = rdr.GetString(3);
        LentBooks newLentBooks = new LentBooks (lentBooksOwnedBookId, lentBooksRecipient, lentBooksReturnedBool, lentBooksId);
        allLentBooks.Add(newLentBooks);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allLentBooks[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM lent_books WHERE id = @BookId;", conn);
      SqlParameter lentBooksIdParameter = new SqlParameter ();
      lentBooksIdParameter.ParameterName = "@BookId";
      lentBooksIdParameter.Value = this.GetId();
      cmd.Parameters.Add(lentBooksIdParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM lent_books;", conn);
      cmd.ExecuteNonQuery();
    }

    public void ReturnBook()
    {
      this.Update(true);
    }
  }
}
