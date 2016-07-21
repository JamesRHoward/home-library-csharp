using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class BorrowedBooksTest : IDisposable
  {
    public BorrowedBooksTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = BorrowedBooks.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      BorrowedBooks firstBorrowedBooks = new BorrowedBooks(1, 1);
      BorrowedBooks secondBorrowedBooks = new BorrowedBooks(1, 1);
      Assert.Equal(firstBorrowedBooks, secondBorrowedBooks);
    }
    [Fact]
    public void Test_Save_SavesBorrowedBooksToDatabase()
    {
      BorrowedBooks testBorrowedBooks = new BorrowedBooks(1, 1);
      testBorrowedBooks.Save();
      Assert.Equal(1, BorrowedBooks.GetAll().Count);
    }
    // [Fact]
    // public void Test_Find_FindsBorrowedBooksById()
    // {
    //   BorrowedBooks testBorrowedBooks = new BorrowedBooks(1, "Jimbo");
    //   testBorrowedBooks.Save();
    //   BorrowedBooks notherTestBorrowedBooks = new BorrowedBooks(2, "Carlita");
    //   notherTestBorrowedBooks.Save();
    //   int idToSearchBy = notherTestBorrowedBooks.GetOwnedBookId();
    //   BorrowedBooks resultBorrowedBooks = BorrowedBooks.Find(idToSearchBy);
    //   Assert.Equal(notherTestBorrowedBooks, resultBorrowedBooks);
    // }
    // [Fact]
    // public void Test_DeleteThis_RemoveSelectedBorrowedBooksFromDataBase()
    // {
    //   BorrowedBooks testBorrowedBooks = new BorrowedBooks (1, "Jimbo");
    //   testBorrowedBooks.Save();
    //   int countAfterSave = BorrowedBooks.GetAll().Count;
    //   testBorrowedBooks.DeleteThis();
    //   int countAfterDeleteThis = BorrowedBooks.GetAll().Count;
    //   int[] expected = { 1, 0 };
    //   int[] result = { countAfterSave, countAfterDeleteThis };
    //   Assert.Equal(expected, result);
    // }
    // [Fact]
    // public void Test_SellBook_ChangeReturnedBoolBooleanToTrue()
    // {
    //   BorrowedBooks testBorrowedBooks = new BorrowedBooks (1, "Jimbo");
    //   testBorrowedBooks.Save();
    //   testBorrowedBooks.ReturnBook();
    //   BorrowedBooks retrievedTestSoldBook = BorrowedBooks.GetAll()[0];
    //   bool result = retrievedTestSoldBook.GetReturnedBool();
    //   Assert.Equal(true, result);
    // }
    public void Dispose()
    {
      BorrowedBooks.DeleteAll();
    }
  }
}
