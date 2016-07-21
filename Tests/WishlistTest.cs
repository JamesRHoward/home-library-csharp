using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HomeLibrary
{
  public class WishlistTest : IDisposable
  {
    public WishlistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=home_library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_GetAll_DatabaseEmptyAtFirst()
    {
      int result = Wishlist.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_SameEntriesMatch()
    {
      Wishlist firstWishlist = new Wishlist(1);
      Wishlist secondWishlist = new Wishlist(1);
      Assert.Equal(firstWishlist, secondWishlist);
    }
    [Fact]
    public void Test_Save_SavesWishlistToDatabase()
    {
      Wishlist testWishlist = new Wishlist(1);
      testWishlist.Save();
      Assert.Equal(1, Wishlist.GetAll().Count);
    }
    [Fact]
    public void Test_Find_ReturnsWishlistById()
    {
      Wishlist testWishlist = new Wishlist(1);
      testWishlist.Save();
      Wishlist notherTestWishlist = new Wishlist(2);
      testWishlist.Save();
      notherTestWishlist.Save();
      int idToSearchBy = notherTestWishlist.GetId();
      Wishlist resultWishlist = Wishlist.Find(idToSearchBy);
      Assert.Equal(notherTestWishlist, resultWishlist);
    }
    [Fact]
    public void Test_DeleteThis_RemoveSelectedWishlistFromDataBase()
    {
      Wishlist testWishlist = new Wishlist (1);
      testWishlist.Save();
      int countAfterSave = Wishlist.GetAll().Count;
      testWishlist.DeleteThis();
      int countAfterDeleteThis = Wishlist.GetAll().Count;
      int[] expected = { 1, 0 };
      int[] result = { countAfterSave, countAfterDeleteThis };
      Assert.Equal(expected, result);
    }
    public void Dispose()
    {
      Wishlist.DeleteAll();
    }
  }
}
