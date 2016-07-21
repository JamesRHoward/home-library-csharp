using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class ReadingList
  {
    private int _id;
    private int _bookId;

    public ReadingList(int bookId, int id = 0)
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
    public override bool Equals (System.Object otherReadingList)
    {
      if (otherReadingList is ReadingList)
      {
       ReadingList newReadingList = (ReadingList) otherReadingList;
       bool idEquality = (this.GetId() == newReadingList.GetId());
       bool bookIdEquality = (this.GetBookId() == newReadingList.GetBookId());
       return (idEquality && bookIdEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<ReadingList> GetAll()
    {
      List<ReadingList> allReadingList = new List<ReadingList>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM reading_list;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int readingListId = rdr.GetInt32(0);
        int readingListBookId = rdr.GetInt32(1);
        ReadingList newReadingList = new ReadingList (readingListBookId, readingListId);
        allReadingList.Add(newReadingList);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReadingList;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO reading_list (book_id) OUTPUT INSERTED.id VALUES (@ReadingListBookId);", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@ReadingListBookId";
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

    public static ReadingList Find (int queryReadingListId)
    {
      List<ReadingList> allReadingList = new List<ReadingList> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM reading_list WHERE id = @ReadingListId;", conn);
      SqlParameter bookIdParameter = new SqlParameter ();
      bookIdParameter.ParameterName = "@ReadingListId";
      bookIdParameter.Value = queryReadingListId;
      cmd.Parameters.Add(bookIdParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int bookId = rdr.GetInt32(1);
        ReadingList newReadingList = new ReadingList (bookId, id);
        allReadingList.Add(newReadingList);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReadingList[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM reading_list WHERE id = @ReadingListId;", conn);
      SqlParameter idParameter = new SqlParameter ();
      idParameter.ParameterName = "@ReadingListId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM reading_list;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
