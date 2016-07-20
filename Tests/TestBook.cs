using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class BookTest : IDisposable
  {
    public BookTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Book.DeleteAll();
      Author.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Book.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_SaveBookToDatabase()
    {
      Book testBook = new Book("The Bible");

      testBook.Save();
      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfTitlesAreTheSame()
    {
      //Arrange, Act
      Book firstBook = new Book("Harry Potter");
      Book secondBook = new Book("Harry Potter");

      //Assert
      Assert.Equal(firstBook, secondBook);
    }
    [Fact]
    public void Test_FindFindsBookInDatabase()
    {
      //Arrange
      Book testBook = new Book("The Wind in the Willows");
      testBook.Save();

      //Act
      Book foundBook = Book.Find(testBook.GetId());

      //Assert
      Assert.Equal(testBook, foundBook);
    }
    [Fact]
    public void Test_AddAuthorToBook()
    {
      Author newAuthor = new Author("J.K. Rowling");
      newAuthor.Save();
      Book newBook = new Book("Harry Potter");
      newBook.Save();

      newBook.AddAuthor(newAuthor);

      List<Author> result = newBook.GetAuthors();
      List<Author> testList = new List<Author>{newAuthor};

      Assert.Equal(result, testList);
    }
    [Fact]
    public void Test_Delete_RemovesBookFromDatabase()
    {

      Author testAuthor1 = new Author("Mark Twain");
      testAuthor1.Save();
      Author testAuthor2 = new Author("J. K. Rowling");
      testAuthor2.Save();

      Book testBook1 = new Book("Adventures of Huckleberry Finn");
      testBook1.Save();
      Book testBook2 = new Book("Harry Potter");
      testBook2.Save();
      testAuthor1.AddBook(testBook1);
      testAuthor2.AddBook(testBook2);
      testBook1.Delete();

      List<Book> resultBooks = Book.GetAll();
      List<Book> testBooks = new List<Book> {testBook2};

      List<Author> resultAuthors = Author.GetAll();
      List<Author> testAuthors = new List<Author> {testAuthor1, testAuthor2};

      Assert.Equal(resultAuthors, testAuthors);
      Assert.Equal(resultBooks, testBooks);
    }
    [Fact]
    public void Test_Update_UpdatesBookInDatabase()
    {
      Book testBook = new Book("Harry Potter");
      testBook.Save();
      string newTitle = "Harry Potter II";
      testBook.Update(newTitle);
      Assert.Equal(newTitle, testBook.GetTitle());
    }
  }
}
