using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class CategoriesTest : IDisposable
  {
    public CategoriesTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = Categories.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      Categories firstCategory = new Categories("romance");
      Categories secondCategory = new Categories("romance");
      Assert.Equal(firstCategory, secondCategory);
    }
    [Fact]
    public void Test_Save_SavesCategoryToDatabase()
    {
      Categories testCategory = new Categories("horror");
      testCategory.Save();
      Assert.Equal(1, Categories.GetAll().Count);
    }
    [Fact]
    public void Test_Find_ReturnsCategoryById()
    {
      Categories testCategory = new Categories("horror");
      testCategory.Save();
      Categories notherTestCategory = new Categories("graphic novel");
      testCategory.Save();
      notherTestCategory.Save();
      int idToSearchBy = notherTestCategory.GetId();
      Categories resultCategory = Categories.Find(idToSearchBy);
      Assert.Equal(notherTestCategory, resultCategory);
    }
    // [Fact]
    // public void Test_DeleteThis_RemoveSelectedBookFromDataBase()
    // {
    //   Books firstBook = new Books ("The Fellowship of the Ring", "JRR Tolkien");
    //   firstBook.Save();
    //   Books secondBook = new Books ("The Two Towers", "JRR Tolkien");
    //   secondBook.Save();
    //   List<Books> testBooksList = new List<Books> {secondBook};
    //   Books testBook = testBooksList[0];
    //   OwnedBooks testOwnedBook = new OwnedBooks (testBook.GetId());
    //   testOwnedBook.Save();
    //   int countAfterSave = OwnedBooks.GetAll().Count;
    //   testOwnedBook.DeleteThis();
    //   int countAfterDeleteThis = OwnedBooks.GetAll().Count;
    //   int[] expected = { 1, 0 };
    //   int[] result = { countAfterSave, countAfterDeleteThis };
    //   Assert.Equal(expected, result);
    // }
    public void Dispose()
    {
      Categories.DeleteAll();
    }
  }
}
