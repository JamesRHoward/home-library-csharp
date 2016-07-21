using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class BorrowedBooks
  {
    private int _id;
    private int _bookId;
    private DateTime? _dueDate;
    private bool _returnedBool;
    private int _sourceId;

    public BorrowedBooks(int bookId, int sourceId, DateTime? dueDate = null, bool returnedBool = false, int id = 0)
    {
      _id = id;
      _bookId = bookId;
      if (dueDate == null)
      {
        _dueDate = new DateTime(2000, 7, 18);
      }
      else
      {
      _dueDate = dueDate;
      }
      _returnedBool = returnedBool;
      _sourceId = sourceId;
    }

    public void SetBookId(int bookId)
    {
      _bookId = bookId;
    }
    public void SetDueDate(DateTime? dueDate)
    {
      _dueDate = dueDate;
    }
    public void SetReturnedBool(bool isReturned)
    {
      _returnedBool = isReturned;
    }
    public void SetSourceId(int sourceId)
    {
      _sourceId = sourceId;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetBookId()
    {
      return _bookId;
    }
    public DateTime? GetDueDate()
    {
      return _dueDate;
    }
    public bool GetReturnedBool()
    {
      return _returnedBool;
    }
    public int GetSourceId()
    {
      return _sourceId;
    }

    public override bool Equals (System.Object otherBorrowedBooks)
    {
      if (otherBorrowedBooks is BorrowedBooks)
      {
       BorrowedBooks newBorrowedBooks = (BorrowedBooks) otherBorrowedBooks;
       bool idEquality = (this.GetId() == newBorrowedBooks.GetId());
       bool bookIdEquality = (this.GetBookId() == newBorrowedBooks.GetBookId());
       bool returnedBoolEquality = (this.GetReturnedBool() == newBorrowedBooks.GetReturnedBool());
       bool sourceIdEquality = (this.GetSourceId() == newBorrowedBooks.GetSourceId());
       return (idEquality && bookIdEquality && returnedBoolEquality && sourceIdEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<BorrowedBooks> GetAll()
    {
      List<BorrowedBooks> allBorrowedBooks = new List<BorrowedBooks>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM borrowed_books;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int borrowedBooksId = rdr.GetInt32(0);
        int borrowedBooksBookId = rdr.GetInt32(1);
        DateTime? borrowedBooksDueDate = rdr.GetDateTime(2);
        bool borrowedBooksReturnedBool = rdr.GetBoolean(3);
        int borrowedBooksSourceId = rdr.GetInt32(4);
        BorrowedBooks newBorrowedBook = new BorrowedBooks (borrowedBooksBookId, borrowedBooksSourceId, borrowedBooksDueDate, borrowedBooksReturnedBool, borrowedBooksId);
        allBorrowedBooks.Add(newBorrowedBook);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBorrowedBooks;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO borrowed_books (book_id, returned_bool, source_id, due_date) OUTPUT INSERTED.id VALUES (@BorrowedBooksBookId, @BorrowedBooksReturnedBool, @BorrowedBooksSourceId, @BorrowedBooksDueDate);", conn);
      SqlParameter borrowedBooksIdParameter = new SqlParameter();
      borrowedBooksIdParameter.ParameterName = "@BorrowedBooksBookId";
      borrowedBooksIdParameter.Value = this.GetBookId();
      SqlParameter physicalBoolParameter = new SqlParameter();
      physicalBoolParameter.ParameterName = "@BorrowedBooksReturnedBool";
      physicalBoolParameter.Value = this.GetReturnedBool();
      SqlParameter sourceIdParameter = new SqlParameter();
      sourceIdParameter.Value = this.GetSourceId();
      sourceIdParameter.ParameterName = "@BorrowedBooksSourceId";
      SqlParameter dueDateParameter = new SqlParameter();
      dueDateParameter.Value = this.GetDueDate();
      dueDateParameter.ParameterName = "@BorrowedBooksDueDate";
      cmd.Parameters.Add(borrowedBooksIdParameter);
      cmd.Parameters.Add(physicalBoolParameter);
      cmd.Parameters.Add(sourceIdParameter);
      cmd.Parameters.Add(dueDateParameter);
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

      SqlCommand cmd = new SqlCommand("UPDATE borrowed_books SET returned_bool = @BorrowedBooksReturnedBool OUTPUT INSERTED.returned_bool WHERE id = @BorrowedBooksId;", conn);

      SqlParameter updateReturnedBoolParameter = new SqlParameter();
      updateReturnedBoolParameter.ParameterName = "@BorrowedBooksReturnedBool";
      updateReturnedBoolParameter.Value = isReturned;
      cmd.Parameters.Add(updateReturnedBoolParameter);

      SqlParameter borrowedBooksIdParameter = new SqlParameter();
      borrowedBooksIdParameter.ParameterName = "@BorrowedBooksId";
      borrowedBooksIdParameter.Value = this.GetId();
      cmd.Parameters.Add(borrowedBooksIdParameter);
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

    public static BorrowedBooks Find (int findId)
    {
      List<BorrowedBooks> allBorrowedBooks = new List<BorrowedBooks> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM borrowed_books WHERE id = @BorrowedBooksId;", conn);
      SqlParameter borrowedBooksIdParameter = new SqlParameter ();
      borrowedBooksIdParameter.ParameterName = "@BorrowedBooksId";
      borrowedBooksIdParameter.Value = findId;
      cmd.Parameters.Add(borrowedBooksIdParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int borrowedBooksId = rdr.GetInt32(0);
        int borrowedBooksBookId = rdr.GetInt32(1);
        DateTime? borrowedBooksDueDate = rdr.GetDateTime(2);
        bool borrowedBooksReturnedBool = rdr.GetBoolean(3);
        int borrowedBooksSourceId = rdr.GetInt32(4);
        BorrowedBooks newBorrowedBooks = new BorrowedBooks (borrowedBooksBookId, borrowedBooksSourceId, borrowedBooksDueDate, borrowedBooksReturnedBool, borrowedBooksId);
        allBorrowedBooks.Add(newBorrowedBooks);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBorrowedBooks[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM borrowed_books WHERE id = @BookId;", conn);
      SqlParameter borrowedBooksIdParameter = new SqlParameter ();
      borrowedBooksIdParameter.ParameterName = "@BookId";
      borrowedBooksIdParameter.Value = this.GetId();
      cmd.Parameters.Add(borrowedBooksIdParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM borrowed_books;", conn);
      cmd.ExecuteNonQuery();
    }

    public void ReturnBook()
    {
      this.Update(true);
    }
  }
}
