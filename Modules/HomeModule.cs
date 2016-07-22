using System.Collections.Generic;
using System;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HomeLibrary
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Books> model = new List<Books> {};
        foreach (var book in OwnedBooks.GetAll())
        {
          model.Add(Books.Find(book.GetBookId()));
        }
        return View["index.cshtml", model];
      };
      Get["/Display/Your"] = _ => {
        List<Books> model = new List<Books> {};
        foreach (var book in OwnedBooks.GetAll())
        {
          model.Add(Books.Find(book.GetBookId()));
        }
        return View["index.cshtml", model];
      };
      Get["/Display/Lent"] = _ => {
        List<Books> model = new List<Books> {};
        foreach (var book in LentBooks.GetAll())
        {
          int ownedId = book.GetOwnedBookId();
          OwnedBooks ownedBook = OwnedBooks.Find(ownedId);
          int allBookId = ownedBook.GetBookId();
          Books bookToAddToModel = Books.Find(allBookId);
          model.Add(bookToAddToModel);
        }
        return View["index.cshtml", model];
      };
      Get["/Display/Borrowed"] = _ => {
        List<Books> model = new List<Books> {};
        foreach (var book in BorrowedBooks.GetAll())
        {
          model.Add(Books.Find(book.GetBookId()));
        }
        return View["index.cshtml", model];
      };
      Get["/Display/Wishlist"] = _ => {
        List<Books> model = new List<Books> {};
        foreach (var book in Wishlist.GetAll())
        {
          model.Add(Books.Find(book.GetBookId()));
        }
        return View["index.cshtml", model];
      };
      Get["/Display/ToSell"] = _ => {
        List<Books> model = new List<Books> {};
        foreach (var book in BooksToSell.GetAll())
        {
          model.Add(Books.Find(book.GetBookId()));
        }
        return View["index.cshtml", model];
      };
      Get["/Add/Your"]= _ => {
        return View["add_your_books.cshtml"];
      };
      Post["/Add/Your"]= _ => {
        // bool haveRead;
        // string title = Request.Form["title"];
        // string author = Request.Form["author"];
        // if ( Request.Form["haveRead"]) {
        //   haveRead = true;
        // }
        // else
        // {
        //   haveRead = false;
        // }
        Books newBook = new Books("The Two Towers", "JRR Tolkien", true);
        newBook.Save();
        OwnedBooks newOwnedBook = new OwnedBooks(newBook.GetId());
        newOwnedBook.Save();
        List<Books> model = new List<Books> {};
        foreach (var book in OwnedBooks.GetAll())
        {
          model.Add(Books.Find(book.GetBookId()));
        }
        return View["index.cshtml", model];
      };
    }
  }
}
