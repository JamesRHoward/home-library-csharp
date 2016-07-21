using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class OwnedBooksTest : IDisposable
  {
    public OwnedBooksTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = OwnedBooks.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      Books firstBook = new Books ("The Hobbit", "JRR Tolkien", true);
      firstBook.Save();
      List<Books> testBooksList = Books.GetAll();
      Books testBook = testBooksList[0];
      OwnedBooks firstOwnedBook = new OwnedBooks (testBook.GetId());
      OwnedBooks secondOwnedBook = new OwnedBooks (testBook.GetId());
      Assert.Equal(firstOwnedBook, secondOwnedBook);
    }
    [Fact]
    public void Test_Save_SavesOwnedBookToDatabase()
    {
      Books bookToSave = new Books ("Mastery", "Robert Greene");
      bookToSave.Save();
      OwnedBooks ownedBookToSave = new OwnedBooks(Books.GetAll()[0].GetId());
      ownedBookToSave.Save();
      Assert.Equal(1, OwnedBooks.GetAll().Count);
    }
    [Fact]
    public void Test_Find_ReturnsOwnedBooksByBooksIdNumber()
    {
      Books bookToSave = new Books ("Mastery", "Robert Greene");
      bookToSave.Save();
      Books notherBookToSave = new Books ("To Be the Man", "Ric Flair");
      notherBookToSave.Save();
      int bookIdToSearchBy = Books.GetAll()[0].GetId();
      OwnedBooks ownedBookToSave = new OwnedBooks(bookIdToSearchBy);
      ownedBookToSave.Save();
      OwnedBooks testOwnedBook = OwnedBooks.Find(bookIdToSearchBy);
      Assert.Equal(OwnedBooks.GetAll()[0], testOwnedBook);
    }
    [Fact]
    public void Test_DeleteThis_RemoveSelectedBookFromDataBase()
    {
      Books firstBook = new Books ("The Fellowship of the Ring", "JRR Tolkien");
      firstBook.Save();
      Books secondBook = new Books ("The Two Towers", "JRR Tolkien");
      secondBook.Save();
      List<Books> testBooksList = new List<Books> {secondBook};
      Books testBook = testBooksList[0];
      OwnedBooks testOwnedBook = new OwnedBooks (testBook.GetId());
      testOwnedBook.Save();
      int countAfterSave = OwnedBooks.GetAll().Count;
      testOwnedBook.DeleteThis();
      int countAfterDeleteThis = OwnedBooks.GetAll().Count;
      int[] expected = { 1, 0 };
      int[] result = { countAfterSave, countAfterDeleteThis };
      Assert.Equal(expected, result);
    }
    [Fact]
    public void Test_UpdateStorageLocation_UpdatesBooksStorageId()
    {
      Books firstBook = new Books ("The Fellowship of the Ring", "JRR Tolkien");
      firstBook.Save();
      OwnedBooks firstOwnedBook = new OwnedBooks(firstBook.GetId());
      firstOwnedBook.Save();
      firstOwnedBook.UpdateStorageLocation(23);
      int result = OwnedBooks.GetAll()[0].GetStorageId();
      Assert.Equal(23, result);
    }
    public void Dispose()
    {
      Books.DeleteAll();
      OwnedBooks.DeleteAll();
    }
  }
}
