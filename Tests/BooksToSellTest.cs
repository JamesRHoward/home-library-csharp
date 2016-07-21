using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class BooksToSellTest : IDisposable
  {
    public BooksToSellTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = BooksToSell.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      BooksToSell firstBooksToSell = new BooksToSell(1);
      BooksToSell secondBooksToSell = new BooksToSell(1);
      Assert.Equal(firstBooksToSell, secondBooksToSell);
    }
    [Fact]
    public void Test_Save_SavesBooksToSellToDatabase()
    {
      BooksToSell testBooksToSell = new BooksToSell(1);
      testBooksToSell.Save();
      Assert.Equal(1, BooksToSell.GetAll().Count);
    }
    [Fact]
    public void Test_Find_ReturnsBooksToSellById()
    {
      BooksToSell testBooksToSell = new BooksToSell(1);
      testBooksToSell.Save();
      BooksToSell notherTestBooksToSell = new BooksToSell(2);
      testBooksToSell.Save();
      notherTestBooksToSell.Save();
      int idToSearchBy = notherTestBooksToSell.GetId();
      BooksToSell resultBooksToSell = BooksToSell.Find(idToSearchBy);
      Assert.Equal(notherTestBooksToSell, resultBooksToSell);
    }
    [Fact]
    public void Test_DeleteThis_RemoveSelectedBooksToSellFromDataBase()
    {
      BooksToSell testBooksToSell = new BooksToSell (1);
      testBooksToSell.Save();
      int countAfterSave = BooksToSell.GetAll().Count;
      testBooksToSell.DeleteThis();
      int countAfterDeleteThis = BooksToSell.GetAll().Count;
      int[] expected = { 1, 0 };
      int[] result = { countAfterSave, countAfterDeleteThis };
      Assert.Equal(expected, result);
    }
    [Fact]
    public void Test_SellBook_ChangeIsSoldBooleanToTrue()
    {
      BooksToSell testBooksToSell = new BooksToSell (1);
      testBooksToSell.Save();
      testBooksToSell.SellBook();
      BooksToSell retrievedTestSoldBook = BooksToSell.GetAll()[0];
      bool result = retrievedTestSoldBook.GetIsSold();
      Assert.Equal(true, result);
    }
    public void Dispose()
    {
      BooksToSell.DeleteAll();
    }
  }
}
