using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class LentBooksTest : IDisposable
  {
    public LentBooksTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = LentBooks.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      LentBooks firstLentBooks = new LentBooks(1, "Jimbo");
      LentBooks secondLentBooks = new LentBooks(1, "Jimbo");
      Assert.Equal(firstLentBooks, secondLentBooks);
    }
    [Fact]
    public void Test_Save_SavesLentBooksToDatabase()
    {
      LentBooks testLentBooks = new LentBooks(1, "Jimbo");
      testLentBooks.Save();
      Assert.Equal(1, LentBooks.GetAll().Count);
    }
    [Fact]
    public void Test_Find_FindsLentBooksById()
    {
      LentBooks testLentBooks = new LentBooks(1, "Jimbo");
      testLentBooks.Save();
      LentBooks notherTestLentBooks = new LentBooks(2, "Carlita");
      notherTestLentBooks.Save();
      int idToSearchBy = notherTestLentBooks.GetOwnedBookId();
      LentBooks resultLentBooks = LentBooks.Find(idToSearchBy);
      Assert.Equal(notherTestLentBooks, resultLentBooks);
    }
    [Fact]
    public void Test_DeleteThis_RemoveSelectedLentBooksFromDataBase()
    {
      LentBooks testLentBooks = new LentBooks (1, "Jimbo");
      testLentBooks.Save();
      int countAfterSave = LentBooks.GetAll().Count;
      testLentBooks.DeleteThis();
      int countAfterDeleteThis = LentBooks.GetAll().Count;
      int[] expected = { 1, 0 };
      int[] result = { countAfterSave, countAfterDeleteThis };
      Assert.Equal(expected, result);
    }
    [Fact]
    public void Test_SellBook_ChangeReturnedBoolBooleanToTrue()
    {
      LentBooks testLentBooks = new LentBooks (1, "Jimbo");
      testLentBooks.Save();
      testLentBooks.ReturnBook();
      LentBooks retrievedTestSoldBook = LentBooks.GetAll()[0];
      bool result = retrievedTestSoldBook.GetReturnedBool();
      Assert.Equal(true, result);
    }
    public void Dispose()
    {
      LentBooks.DeleteAll();
    }
  }
}
