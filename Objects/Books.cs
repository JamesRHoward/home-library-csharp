
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeLibrary
{
  public class Books
  {
    private int _id;
    private string _title;
    private string _author;
    private bool _readBool;

    public Books(string title, string author, bool readBool = false, int id = 0)
    {
      _id = id;
      _title = title;
      _author = author;
      _readBool = readBool;
    }

    public void SetTitle(string title)
    {
      _title = title;
    }
    public void SetAuthor(string author)
    {
      _author = author;
    }
    public void SetReadBool(bool readBool)
    {
      _readBool = readBool;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetTitle()
    {
      return _title;
    }
    public string GetAuthor()
    {
      return _author;
    }
    public bool GetReadBool()
    {
      return _readBool;
      //readBool gives you wiiiiiings
    }

    public override bool Equals (System.Object otherBooks)
    {
      if (otherBooks is Books)
      {
       Books newBooks = (Books) otherBooks;
       bool idEquality = (this.GetId() == newBooks.GetId());
       bool titleEquality = (this.GetTitle() == newBooks.GetTitle());
       bool authorEquality = (this.GetAuthor() == newBooks.GetAuthor());
       bool readBoolEquality = (this.GetReadBool() == newBooks.GetReadBool());
       return (idEquality && titleEquality && authorEquality && readBoolEquality);
      }
      else
      {
       return false;
      }
    }

    public static List<Books> GetAll()
    {
      List<Books> allBooks = new List<Books>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM all_books ORDER by title ASC;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int booksId = rdr.GetInt32(0);
        string booksTitle = rdr.GetString(1);
        string booksAuthor = rdr.GetString(2);
        bool booksReadBool = rdr.GetBoolean(3);
        Books newBooks = new Books(booksTitle, booksAuthor, booksReadBool, booksId);
        allBooks.Add(newBooks);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBooks;
    }

    public void Save()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlDataReader rdr = null;
     SqlCommand cmd = new SqlCommand ("INSERT INTO all_books (title, author, read_bool) OUTPUT INSERTED.id VALUES (@BookTitle, @BookAuthor, @BookReadBool);", conn);
     SqlParameter titleParameter = new SqlParameter();
     titleParameter.ParameterName = "@BookTitle";
     titleParameter.Value = this.GetTitle();
     SqlParameter authorParameter = new SqlParameter();
     authorParameter.ParameterName = "@BookAuthor";
     authorParameter.Value = this.GetAuthor();
     SqlParameter readBoolParameter = new SqlParameter();
     readBoolParameter.Value = this.GetReadBool();
     readBoolParameter.ParameterName = "@BookReadBool";
     cmd.Parameters.Add(titleParameter);
     cmd.Parameters.Add(authorParameter);
     cmd.Parameters.Add(readBoolParameter);
     rdr = cmd.ExecuteReader();
     while (rdr.Read())
     {
       this._id = rdr.GetInt32(0);
     }
     if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
   }

   public static Books Find (int queryBooksId)
    {
      List<Books> allBooks = new List<Books> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM all_books WHERE id = @BookId;", conn);
      SqlParameter booksIdParameter = new SqlParameter ();
      booksIdParameter.ParameterName = "@BookId";
      booksIdParameter.Value = queryBooksId;
      cmd.Parameters.Add(booksIdParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int booksId = rdr.GetInt32(0);
        string booksTitle = rdr.GetString(1);
        string booksAuthor = rdr.GetString(2);
        bool booksReadBool = rdr.GetBoolean(3);
        Books newBooks = new Books (booksTitle, booksAuthor, booksReadBool, booksId);
        allBooks.Add(newBooks);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBooks[0];
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM all_books WHERE id = @BookId;", conn);
      SqlParameter booksIdParameter = new SqlParameter ();
      booksIdParameter.ParameterName = "@BookId";
      booksIdParameter.Value = this.GetId();
      cmd.Parameters.Add(booksIdParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM all_books;", conn);
      cmd.ExecuteNonQuery();
    }

    public void AddCategory (Categories newCategory)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("INSERT INTO books_categories (book_id, category_id) VALUES (@BookId, @CategoryId);", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetId();
      SqlParameter categoryIdParameter = new SqlParameter ();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = newCategory.GetId();
      cmd.Parameters.Add(bookIdParameter);
      cmd.Parameters.Add(categoryIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Categories> GetCategories()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT categories.* FROM all_books JOIN books_categories ON (all_books.id = books_categories.book_id) JOIN categories ON (books_categories.category_id = categories.id) WHERE all_books.id = @BookId;", conn);
      SqlParameter BookIdParameter = new SqlParameter();
      BookIdParameter.ParameterName = "@BookId";
      BookIdParameter.Value = this.GetId().ToString();

      cmd.Parameters.Add(BookIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Categories> categories = new List<Categories>{};

      while(rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        string categoryGenre = rdr.GetString(1);
        Categories newCategories = new Categories(categoryGenre, categoryId);
        categories.Add(newCategories);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return categories;
    }

    public void DeleteCategory(Categories categoryToRemove)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM books_categories WHERE book_id = @BookId AND  category_id = @CategoryId;", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetId().ToString();
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = categoryToRemove.GetId().ToString();

      cmd.Parameters.Add(bookIdParameter);
      cmd.Parameters.Add(categoryIdParameter);

      cmd.ExecuteNonQuery();
    }

  }
}
