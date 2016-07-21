using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class StorageTest : IDisposable
  {
    public StorageTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = Storage.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      Storage firstStorage = new Storage("romance");
      Storage secondStorage = new Storage("romance");
      Assert.Equal(firstStorage, secondStorage);
    }
    [Fact]
    public void Test_Save_SavesStorageToDatabase()
    {
      Storage testStorage = new Storage("horror");
      testStorage.Save();
      Assert.Equal(1, Storage.GetAll().Count);
    }
    [Fact]
    public void Test_Find_ReturnsStorageById()
    {
      Storage testStorage = new Storage("horror");
      testStorage.Save();
      Storage notherTestStorage = new Storage("graphic novel");
      testStorage.Save();
      notherTestStorage.Save();
      int idToSearchBy = notherTestStorage.GetId();
      Storage resultStorage = Storage.Find(idToSearchBy);
      Assert.Equal(notherTestStorage, resultStorage);
    }
    [Fact]
    public void Test_DeleteThis_RemoveSelectedStorageFromDataBase()
    {
      Storage testStorage = new Storage ("biography");
      testStorage.Save();
      int countAfterSave = Storage.GetAll().Count;
      testStorage.DeleteThis();
      int countAfterDeleteThis = Storage.GetAll().Count;
      int[] expected = { 1, 0 };
      int[] result = { countAfterSave, countAfterDeleteThis };
      Assert.Equal(expected, result);
    }
    public void Dispose()
    {
      Storage.DeleteAll();
    }
  }
}
