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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO owned_books (book_id, physical_bool, storage_id) OUTPUT INSERTED.id VALUES (@OwnedBookBookId, @OwnedBookPhysicalBool, @OwnedBookStorageId);", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@OwnedBookBookId";
      bookIdParameter.Value = this.GetBookId();
      SqlParameter physicalBoolParameter = new SqlParameter();
      physicalBoolParameter.ParameterName = "@OwnedBookPhysicalBool";
      physicalBoolParameter.Value = this.GetIsPhysical();
      SqlParameter storageIdParameter = new SqlParameter();
      storageIdParameter.Value = this.GetStorageId();
      storageIdParameter.ParameterName = "@OwnedBookStorageId";
      cmd.Parameters.Add(bookIdParameter);
      cmd.Parameters.Add(physicalBoolParameter);
      cmd.Parameters.Add(storageIdParameter);
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

    public static OwnedBooks Find (int queryOwnedBooksBookId)
    {
      List<OwnedBooks> allOwnedBooks = new List<OwnedBooks> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM owned_books WHERE book_id = @OwnedBookBookId;", conn);
      SqlParameter ownedBooksIdParameter = new SqlParameter ();
      ownedBooksIdParameter.ParameterName = "@OwnedBookBookId";
      ownedBooksIdParameter.Value = queryOwnedBooksBookId;
      cmd.Parameters.Add(ownedBooksIdParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int ownedBooksId = rdr.GetInt32(0);
        int ownedBooksBookId = rdr.GetInt32(1);
        bool ownedBooksIsPhysical = rdr.GetBoolean(2);
        int ownedBooksStorageId = rdr.GetInt32(3);
        OwnedBooks newOwnedBooks = new OwnedBooks (ownedBooksBookId, ownedBooksIsPhysical, ownedBooksStorageId, ownedBooksId);
        allOwnedBooks.Add(newOwnedBooks);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allOwnedBooks[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM owned_books WHERE id = @BookId;", conn);
      SqlParameter ownedBooksIdParameter = new SqlParameter ();
      ownedBooksIdParameter.ParameterName = "@BookId";
      ownedBooksIdParameter.Value = this.GetId();
      cmd.Parameters.Add(ownedBooksIdParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM owned_books;", conn);
      cmd.ExecuteNonQuery();
    }

    public void UpdateStorageLocation(int storageId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE owned_books SET storage_id = @OwnedBooksStorageId OUTPUT INSERTED.storage_id WHERE id = @OwnedBooksId;", conn);

      SqlParameter updateStorageIdParameter = new SqlParameter();
      updateStorageIdParameter.ParameterName = "@OwnedBooksStorageId";
      updateStorageIdParameter.Value = storageId;
      cmd.Parameters.Add(updateStorageIdParameter);

      SqlParameter ownedBooksIdParameter = new SqlParameter();
      ownedBooksIdParameter.ParameterName = "@OwnedBooksId";
      ownedBooksIdParameter.Value = this.GetId();
      cmd.Parameters.Add(ownedBooksIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._storageId = rdr.GetInt32(0);
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
  }
}
