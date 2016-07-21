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
        Copy newCopy = new Copy(newBook.GetId());
        newCopy.Save();
        List<Book> allBooks = Book.GetAll();
        return View["books.cshtml", allBooks];
      };

      Get["/books/{id}"] = parameters => {
        Book selectedBook = Book.Find(parameters.id);
        Dictionary<string,object> model = new Dictionary<string,object>();
        List<Author> bookAuthors = selectedBook.GetAuthors();
        List<Author> allAuthors = Author.GetAll();
        List<Copy> copyOfBook = selectedBook.GetAvailableCopies();
        model.Add("book", selectedBook);
        model.Add("bookAuthor",bookAuthors);
        model.Add("allAuthors", allAuthors);
        model.Add("copyOfBook", copyOfBook);

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
      Get ["/authors/search"]= _ =>{
        string searchString = Request.Query["author_name_search"];
        List<Author> authorList = Author.SearchAuthor(searchString);
        return View["author_results.cshtml", authorList];
      };
      Get ["/books/search"]= _ =>{
        string searchString = Request.Query["book_title_search"];
        List<Book> bookList = Book.SearchTitle(searchString);
        return View["book_results.cshtml", bookList];
      };
      Get ["/book/{book_id}/copy/{copy_id}"]= parameter =>{
        Book selectedBook = Book.Find(parameter.book_id);
        Copy selectedCopy = Copy.Find(parameter.copy_id);
        List<Patron> selectedPatrons = Patron.GetAll();

        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Copy> copyOfBook = selectedBook.GetCopies();
        model.Add("book", selectedBook);
        model.Add("copy", selectedCopy);
        model.Add("copyOfBook", copyOfBook);
        model.Add("patron", selectedPatrons);
        return View["copy.cshtml", model];
      };
      Post ["/patron/add"]= _ =>{
        Patron newPatron = new Patron(Request.Form["patron_name"], Request.Form["patron_phone_number"]);
        newPatron.Save();
        List<Author> allAuthors = Author.GetAll();
        return View ["index.cshtml", allAuthors];
      };
      Post ["/checkout/new"]= _ =>{
        int copyId = Request.Form["copy_id"];
        int patronId = Request.Form["patron"];
        Checkout newCheckout = new Checkout(copyId, patronId, new DateTime(2016, 7, 21), new DateTime (2016, 6, 21), false);
        newCheckout.Save();
        Copy checkedOutCopy = Copy.Find(copyId);
        checkedOutCopy.setStatus(true);
        checkedOutCopy.Update(checkedOutCopy.GetBookId(), checkedOutCopy.GetStatus());
        Book checkedOutBook = newCheckout.GetBook();

        Dictionary<string, object> model = new Dictionary<string, object>();

        model.Add("checkout", newCheckout);
        model.Add("book", checkedOutBook);

        return View ["checkout.cshtml", model];
      };
      Get ["/patron/new"]= _ =>{
        List<Patron> allPatrons = Patron.GetAll();
        return View ["patrons.cshtml", allPatrons];
      };
      Get["/patron/{id}"] = parameters => {
        Patron selectedPatron = Patron.Find(parameters.id);
        List<Checkout> patronCheckouts = selectedPatron.GetCheckouts();

        Dictionary<string,object> model = new Dictionary<string,object>();

        model.Add("patron", selectedPatron);
        model.Add("checkouts", patronCheckouts);

        return View["patron.cshtml",model];
      };
      Get["/overdue"] = _ => {
        List<Checkout> overdues = Checkout.GetOverdue();
        return View["overdue.cshtml", overdues];
      };
    }
  }
}
