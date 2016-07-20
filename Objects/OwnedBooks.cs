using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class OwnedBooks
  {
    private int _id;
    private int _bookId;
    private bool _isPhysical;
    private int _storageId;

    public OwnedBooks(int bookId, bool isPhysical = true, int storageId = 0, int id = 0)
    {
      _id = id;
      _bookId = bookId;
      _isPhysical = isPhysical;
      _storageId = storageId;
    }

    public void SetBookId(int bookId)
    {
      _bookId = bookId;
    }
    public void SetIsPhysical(bool isPhysical)
    {
      _isPhysical = isPhysical;
    }
    public void SetStorageId(int storageId)
    {
      _storageId = storageId;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetBookId()
    {
      return _bookId;
    }
    public bool GetIsPhysical()
    {
      return _isPhysical;
    }
    public int GetStorageId()
    {
      return _storageId;
    }

    public override bool Equals (System.Object otherOwnedBooks)
    {
      if (otherOwnedBooks is OwnedBooks)
      {
       OwnedBooks newOwnedBooks = (OwnedBooks) otherOwnedBooks;
       bool idEquality = (this.GetId() == newOwnedBooks.GetId());
       bool bookIdEquality = (this.GetBookId() == newOwnedBooks.GetBookId());
       bool isPhysicalEquality = (this.GetIsPhysical() == newOwnedBooks.GetIsPhysical());
       bool storageIdEquality = (this.GetStorageId() == newOwnedBooks.GetStorageId());
       return (idEquality && bookIdEquality && isPhysicalEquality && storageIdEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<OwnedBooks> GetAll()
    {
      List<OwnedBooks> allOwnedBooks = new List<OwnedBooks>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM owned_books;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int ownedBooksId = rdr.GetInt32(0);
        int ownedBooksBookId = rdr.GetInt32(1);
        bool ownedBooksIsPhysical = rdr.GetBoolean(2);
        int ownedBooksStorageId = rdr.GetInt32(3);
        OwnedBooks newOwnedBook = new OwnedBooks (ownedBooksBookId, ownedBooksIsPhysical, ownedBooksStorageId, ownedBooksId);
        allOwnedBooks.Add(newOwnedBook);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allOwnedBooks;
    }

    // public void Save()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlDataReader rdr = null;
    //   SqlCommand cmd = new SqlCommand ("INSERT INTO all_books (title, author, read_bool) OUTPUT INSERTED.id VALUES (@BookTitle, @BookAuthor, @BookReadBool);", conn);
    //   SqlParameter titleParameter = new SqlParameter();
    //   titleParameter.ParameterName = "@BookTitle";
    //   titleParameter.Value = this.GetTitle();
    //   SqlParameter authorParameter = new SqlParameter();
    //   authorParameter.ParameterName = "@BookAuthor";
    //   authorParameter.Value = this.GetAuthor();
    //   SqlParameter readBoolParameter = new SqlParameter();
    //   readBoolParameter.Value = this.GetReadBool();
    //   readBoolParameter.ParameterName = "@BookReadBool";
    //   cmd.Parameters.Add(titleParameter);
    //   cmd.Parameters.Add(authorParameter);
    //   cmd.Parameters.Add(readBoolParameter);
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
    // public static Books Find (int queryBooksId)
    // {
    //   List<Books> allBooks = new List<Books> {};
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlDataReader rdr = null;
    //   SqlCommand cmd = new SqlCommand ("SELECT * FROM all_books WHERE id = @BookId;", conn);
    //   SqlParameter booksIdParameter = new SqlParameter ();
    //   booksIdParameter.ParameterName = "@BookId";
    //   booksIdParameter.Value = queryBooksId;
    //   cmd.Parameters.Add(booksIdParameter);
    //   rdr = cmd.ExecuteReader();
    //   while (rdr.Read())
    //   {
    //     int booksId = rdr.GetInt32(0);
    //     string booksTitle = rdr.GetString(1);
    //     string booksAuthor = rdr.GetString(2);
    //     bool booksReadBool = rdr.GetBoolean(3);
    //     Books newBooks = new Books (booksTitle, booksAuthor, booksReadBool, booksId);
    //     allBooks.Add(newBooks);
    //   }
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return allBooks[0];
    // }
    //
    // public void DeleteThis()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlCommand cmd = new SqlCommand ("DELETE FROM all_books WHERE id = @BookId;", conn);
    //   SqlParameter booksIdParameter = new SqlParameter ();
    //   booksIdParameter.ParameterName = "@BookId";
    //   booksIdParameter.Value = this.GetId();
    //   cmd.Parameters.Add(booksIdParameter);
    //   cmd.ExecuteNonQuery();
    // }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM all_books;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
