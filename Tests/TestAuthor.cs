using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class AuthorTest : IDisposable
  {
    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Author.DeleteAll();
      Book.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Author.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_SaveAuthorToDatabase()
    {
      Author testAuthor = new Author("Mark Twain");

      testAuthor.Save();
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      Author firstAuthor = new Author("Mark Twain");
      Author secondAuthor = new Author("Mark Twain");

      //Assert
      Assert.Equal(firstAuthor, secondAuthor);
    }
    [Fact]
    public void Test_FindFindsAuthorInDatabase()
    {
      //Arrange
      Author testAuthor = new Author("Mark Twain");
      testAuthor.Save();

      //Act
      Author foundAuthor = Author.Find(testAuthor.GetId());

      //Assert
      Assert.Equal(testAuthor, foundAuthor);
    }
    [Fact]
    public void Test_AddBookToAuthor()
    {
      Book newBook = new Book("Harry Potter");
      newBook.Save();
      Author newAuthor = new Author("J. K. Rowling");
      newAuthor.Save();

      newAuthor.AddBook(newBook);

      List<Book> result = newAuthor.GetBooks();
      List<Book> testList = new List<Book>{newBook};

      Assert.Equal(result, testList);
    }
    [Fact]
    public void Test_Delete_RemovesAuthorFromDatabase()
    {
      List<Author> TestAuthors = new List<Author>{};

      Author testAuthor1 = new Author("Mark Twain");
      testAuthor1.Save();
      Author testAuthor2 = new Author("J. K. Rowling");
      testAuthor2.Save();

      Book TestBook1 = new Book("Adventures of Huckleberry Finn");
      TestBook1.Save();
      Book TestBook2 = new Book("Harry Potter");
      TestBook2.Save();
      TestBook1.AddAuthor(testAuthor1);
      TestBook2.AddAuthor(testAuthor2);
      testAuthor1.Delete();

      List<Author> resultAuthors = Author.GetAll();
      List<Author> testAuthors = new List<Author> {testAuthor2};

      List<Book> resultBooks = Book.GetAll();
      List<Book> testBooks = new List<Book> {TestBook1, TestBook2};

      Assert.Equal(resultAuthors, testAuthors);
      Assert.Equal(resultBooks, testBooks);
    }
    [Fact]
    public void Test_Update_UpdatesAuthorInDatabase()
    {
      Author testAuthor = new Author("J.K. Rowling");
      testAuthor.Save();
      string newName = "Danielle Steele";
      testAuthor.Update(newName);
      Assert.Equal(newName, testAuthor.GetName());
    }
  }
}
