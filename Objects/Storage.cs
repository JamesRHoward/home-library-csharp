using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class Storage
  {
    private int _id;
    private string _place;

    public Storage(string placeName, int id = 0)
    {
      _id = id;
      _place = placeName;
    }

    public void SetBookId(string newPlaceName)
    {
      _place = newPlaceName;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetPlace()
    {
      return _place;
    }

    public override bool Equals (System.Object otherStorage)
    {
      if (otherStorage is Storage)
      {
       Storage newStorage = (Storage) otherStorage;
       bool idEquality = (this.GetId() == newStorage.GetId());
       bool placeEquality = (this.GetPlace() == newStorage.GetPlace());
       return (idEquality && placeEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<Storage> GetAll()
    {
      List<Storage> allStorage = new List<Storage>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM storage;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int storageId = rdr.GetInt32(0);
        string storagePlace = rdr.GetString(1);
        Storage newStorage = new Storage (storagePlace, storageId);
        allStorage.Add(newStorage);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStorage;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("INSERT INTO storage (place_name) OUTPUT INSERTED.id VALUES (@StoragePlace);", conn);
      SqlParameter placeParameter = new SqlParameter();
      placeParameter.ParameterName = "@StoragePlace";
      placeParameter.Value = this.GetPlace();
      cmd.Parameters.Add(placeParameter);
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

    public static Storage Find (int queryStorageId)
    {
      List<Storage> allStorage = new List<Storage> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM storage WHERE id = @StorageId;", conn);
      SqlParameter placeParameter = new SqlParameter ();
      placeParameter.ParameterName = "@StorageId";
      placeParameter.Value = queryStorageId;
      cmd.Parameters.Add(placeParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string place = rdr.GetString(1);
        Storage newStorage = new Storage (place, id);
        allStorage.Add(newStorage);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStorage[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM storage WHERE id = @StorageId;", conn);
      SqlParameter idParameter = new SqlParameter ();
      idParameter.ParameterName = "@StorageId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM storage;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
