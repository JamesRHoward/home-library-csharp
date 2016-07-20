using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class BooksTest : IDisposable
  {
    public BooksTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = Books.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      Books firstBook = new Books ("The Hobbit", "JRR Tolkien", true);
      Books secondBook = new Books ("The Hobbit", "JRR Tolkien", true);
      Assert.Equal(firstBook, secondBook);
    }
    [Fact]
    public void Test_Save_SavesBookToDatabase()
    {
      Books bookToSave = new Books ("Mastery", "Robert Greene");
      bookToSave.Save();
      Assert.Equal(bookToSave, Books.GetAll()[0]);
      Assert.Equal(1, Books.GetAll().Count);
    }
    [Fact]
    public void Test_Find_ReturnsBooksByBooksNumber()
    {
      Books firstBook = new Books ("The Fellowship of the Ring", "JRR Tolkien");
      firstBook.Save();
      Books secondBook = new Books ("The Two Towers", "JRR Tolkien");
      secondBook.Save();
      Books foundBook = Books.Find(firstBook.GetId());
      Assert.Equal(firstBook, foundBook);
    }
    [Fact]
    public void Test_DeleteThis_RemoveSelectedBookFromDataBase()
    {
      Books firstBook = new Books ("The Fellowship of the Ring", "JRR Tolkien");
      firstBook.Save();
      Books secondBook = new Books ("The Two Towers", "JRR Tolkien");
      secondBook.Save();
      List<Books> expectedBooksList = new List<Books> {secondBook};
      firstBook.DeleteThis();
      List<Books> result = Books.GetAll();
      Assert.Equal(expectedBooksList, result);
    }
    [Fact]
    public void Test_GetCategories_ReturnCategoriesFromJoinStatement()
    {
      Books firstBook = new Books ("The Fellowship of the Ring", "JRR Tolkien");
      firstBook.Save();
      Books secondBook = new Books ("The Two Towers", "JRR Tolkien");
      secondBook.Save();
      Categories testCategory = new Categories("sequel");
      testCategory.Save();
      Categories notherTestCategory = new Categories("graphic novel");
      testCategory.Save();
      secondBook.AddCategory(testCategory);
      Assert.Equal(1, secondBook.GetCategories().Count);
    }
    [Fact]
    public void Test_GetCategories_RemoveSpecificCategoryFromBookInstance()
    {
      Books firstBook = new Books ("The Fellowship of the Ring", "JRR Tolkien");
      firstBook.Save();
      Books secondBook = new Books ("The Two Towers", "JRR Tolkien");
      secondBook.Save();
      Categories testCategory = new Categories("sequel");
      testCategory.Save();
      Categories notherTestCategory = new Categories("graphic novel");
      testCategory.Save();
      secondBook.AddCategory(testCategory);
      secondBook.AddCategory(notherTestCategory);
      secondBook.DeleteCategory(notherTestCategory);
      Assert.Equal(1, secondBook.GetCategories().Count);
    }
    public void Dispose()
    {
      Books.DeleteAll();
      Categories.DeleteAll();
    }
  }
}
