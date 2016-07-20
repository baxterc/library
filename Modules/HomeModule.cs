using System.Collections.Generic;
using System;
using Nancy;

namespace Library
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"]=_=> {
        List<Author> allAuthors = Author.GetAll();
        return View["index.cshtml", allAuthors];
      };

      Get["authors/all"] =_=> {
        List<Author> allAuthors = Author.GetAll();
        return View["authors.cshtml", allAuthors];
      };

      Get["/author/{id}"] = parameters =>{

        Dictionary<string,object> model = new Dictionary<string,object>();
        Author selectedAuthor = Author.Find(parameters.id);
        List<Book> booksByAuthors = selectedAuthor.GetBooks();
        model.Add("author", selectedAuthor);
        model.Add("books", booksByAuthors);
        return View["author.cshtml", model];
      };

      Post["/authors/new"] = _ => {
        Author newAuthor = new Author(Request.Form["author_name"]);
        newAuthor.Save();
        List<Author> allAuthors = Author.GetAll();
        return View["authors.cshtml", allAuthors];
      };

      Get["/books/all"] =_=> {
        List<Book> allBooks = Book.GetAll();
        return View["books.cshtml", allBooks];
      };

      Post["/books/new"] = _ => {
        Book newBook = new Book(Request.Form["book_title"]);
        newBook.Save();
        Author addAuthor = Author.Find(Request.Form["book_author"]);
        addAuthor.AddBook(newBook);
        List<Book> allBooks = Book.GetAll();
        return View["books.cshtml", allBooks];
      };

      Get["/books/{id}"] = parameters => {
        Book selectedBook = Book.Find(parameters.id);
        Dictionary<string,object> model = new Dictionary<string,object>();
        List<Author> bookAuthors = selectedBook.GetAuthors();
        List<Author> allAuthors = Author.GetAll();

        model.Add("book", selectedBook);
        model.Add("bookAuthor",bookAuthors);
        model.Add("allAuthors", allAuthors);


        return View["book.cshtml",model];
      };

      Post["/books/{id}"] = parameters => {
        Author selectedAuthor = Author.Find(Request.Form["add_new_author"]);

        Book selectedBook = Book.Find(parameters.id);
        selectedAuthor.AddBook(selectedBook);

        Dictionary<string,object> model = new Dictionary<string,object>();
        List<Author> bookAuthors = selectedBook.GetAuthors();
        List<Author> allAuthors = Author.GetAll();

        model.Add("book", selectedBook);
        model.Add("bookAuthor",bookAuthors);
        model.Add("allAuthors", allAuthors);

        return View["book.cshtml", model];
      };

      Get["/book/update/{id}"] = parameters => {
        Book selectedBook = Book.Find(parameters.id);
        return View["book_update.cshtml", selectedBook];
      };

      Patch["/book/{id}"] = parameters => {
        Book selectedBook = Book.Find(parameters.id);
        selectedBook.Update(Request.Form["book_title"]);

        Dictionary<string,object> model = new Dictionary<string,object>();
        List<Author> bookAuthors = selectedBook.GetAuthors();
        List<Author> allAuthors = Author.GetAll();

        model.Add("book", selectedBook);
        model.Add("bookAuthor",bookAuthors);
        model.Add("allAuthors", allAuthors);

        return View["book.cshtml", model];
      };

      Delete["/book/{book_id}/drop/{author_id}"] = parameters => {
        Book selectedBook = Book.Find(parameters.book_id);
        Author selectedAuthor = Author.Find(parameters.author_id);
        selectedBook.DeleteAuthor(selectedAuthor);

        Dictionary<string,object> model = new Dictionary<string,object>();
        List<Author> bookAuthors = selectedBook.GetAuthors();
        List<Author> allAuthors = Author.GetAll();

        model.Add("book", selectedBook);
        model.Add("bookAuthor",bookAuthors);
        model.Add("allAuthors", allAuthors);
        return View["book.cshtml", model];
      };
    }
  }
}
