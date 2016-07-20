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
    [Fact]
    public void Test_DeleteThis_RemoveSelectedCategoryFromDataBase()
    {
      Categories testCategory = new Categories ("biography");
      testCategory.Save();
      int countAfterSave = Categories.GetAll().Count;
      testCategory.DeleteThis();
      int countAfterDeleteThis = Categories.GetAll().Count;
      int[] expected = { 1, 0 };
      int[] result = { countAfterSave, countAfterDeleteThis };
      Assert.Equal(expected, result);
    }
    public void Dispose()
    {
      Categories.DeleteAll();
    }
  }
}
