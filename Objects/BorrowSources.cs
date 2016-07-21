using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class BorrowSource
  {
    private int _id;
    private string _name;

    public BorrowSource(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public override bool Equals (System.Object otherBorrowSource)
    {
      if (otherBorrowSource is BorrowSource)
      {
       BorrowSource newBorrowSource = (BorrowSource) otherBorrowSource;
       bool idEquality = (this.GetId() == newBorrowSource.GetId());
       bool nameEquality = (this.GetName() == newBorrowSource.GetName());
       return (idEquality && nameEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<BorrowSource> GetAll()
    {
      List<BorrowSource> allBorrowSource = new List<BorrowSource>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM borrow_sources;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int borrowSourceId = rdr.GetInt32(0);
        string borrowSourceName = rdr.GetString(1);
        BorrowSource newBorrowSource = new BorrowSource (borrowSourceName, borrowSourceId);
        allBorrowSource.Add(newBorrowSource);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBorrowSource;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO borrow_sources (name) OUTPUT INSERTED.id VALUES (@BorrowSourceName);", conn);
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BorrowSourceName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
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

    public static BorrowSource Find (int queryBorrowSourceId)
    {
      List<BorrowSource> allBorrowSource = new List<BorrowSource> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM borrow_sources WHERE id = @BorrowSourceId;", conn);
      SqlParameter nameParameter = new SqlParameter ();
      nameParameter.ParameterName = "@BorrowSourceId";
      nameParameter.Value = queryBorrowSourceId;
      cmd.Parameters.Add(nameParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        BorrowSource newBorrowSource = new BorrowSource (name, id);
        allBorrowSource.Add(newBorrowSource);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBorrowSource[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM borrow_sources WHERE id = @BorrowSourceId;", conn);
      SqlParameter idParameter = new SqlParameter ();
      idParameter.ParameterName = "@BorrowSourceId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM borrow_sources;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
