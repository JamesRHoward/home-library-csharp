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
    }
  }
}
