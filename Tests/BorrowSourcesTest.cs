using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class BorrowSourceTest : IDisposable
  {
    public BorrowSourceTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = BorrowSource.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      BorrowSource firstBorrowSource = new BorrowSource("romance");
      BorrowSource secondBorrowSource = new BorrowSource("romance");
      Assert.Equal(firstBorrowSource, secondBorrowSource);
    }
    [Fact]
    public void Test_Save_SavesBorrowSourceToDatabase()
    {
      BorrowSource testBorrowSource = new BorrowSource("horror");
      testBorrowSource.Save();
      Assert.Equal(1, BorrowSource.GetAll().Count);
    }
    [Fact]
    public void Test_Find_ReturnsBorrowSourceById()
    {
      BorrowSource testBorrowSource = new BorrowSource("horror");
      testBorrowSource.Save();
      BorrowSource notherTestBorrowSource = new BorrowSource("graphic novel");
      testBorrowSource.Save();
      notherTestBorrowSource.Save();
      int idToSearchBy = notherTestBorrowSource.GetId();
      BorrowSource resultBorrowSource = BorrowSource.Find(idToSearchBy);
      Assert.Equal(notherTestBorrowSource, resultBorrowSource);
    }
    [Fact]
    public void Test_DeleteThis_RemoveSelectedBorrowSourceFromDataBase()
    {
      BorrowSource testBorrowSource = new BorrowSource ("biography");
      testBorrowSource.Save();
      int countAfterSave = BorrowSource.GetAll().Count;
      testBorrowSource.DeleteThis();
      int countAfterDeleteThis = BorrowSource.GetAll().Count;
      int[] expected = { 1, 0 };
      int[] result = { countAfterSave, countAfterDeleteThis };
      Assert.Equal(expected, result);
    }
    public void Dispose()
    {
      BorrowSource.DeleteAll();
    }
  }
}
