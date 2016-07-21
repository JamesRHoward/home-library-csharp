using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class ReadingListTest : IDisposable
  {
    public ReadingListTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = ReadingList.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      ReadingList firstReadingList = new ReadingList(1);
      ReadingList secondReadingList = new ReadingList(1);
      Assert.Equal(firstReadingList, secondReadingList);
    }
    [Fact]
    public void Test_Save_SavesReadingListToDatabase()
    {
      ReadingList testReadingList = new ReadingList(1);
      testReadingList.Save();
      Assert.Equal(1, ReadingList.GetAll().Count);
    }
    [Fact]
    public void Test_Find_ReturnsReadingListById()
    {
      ReadingList testReadingList = new ReadingList(1);
      testReadingList.Save();
      ReadingList notherTestReadingList = new ReadingList(2);
      testReadingList.Save();
      notherTestReadingList.Save();
      int idToSearchBy = notherTestReadingList.GetId();
      ReadingList resultReadingList = ReadingList.Find(idToSearchBy);
      Assert.Equal(notherTestReadingList, resultReadingList);
    }
    [Fact]
    public void Test_DeleteThis_RemoveSelectedReadingListFromDataBase()
    {
      ReadingList testReadingList = new ReadingList (1);
      testReadingList.Save();
      int countAfterSave = ReadingList.GetAll().Count;
      testReadingList.DeleteThis();
      int countAfterDeleteThis = ReadingList.GetAll().Count;
      int[] expected = { 1, 0 };
      int[] result = { countAfterSave, countAfterDeleteThis };
      Assert.Equal(expected, result);
    }
    public void Dispose()
    {
      ReadingList.DeleteAll();
    }
  }
}
