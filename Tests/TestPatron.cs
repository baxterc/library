using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class PatronTest : IDisposable
  {
    public PatronTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Patron.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Patron.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfPatronsAreTheSame()
    {
      //Arrange, Act
      Patron firstPatron = new Patron("Jack", "9715555555");
      Patron secondPatron = new Patron("Jack", "9715555555");

      //Assert
      Assert.Equal(firstPatron, secondPatron);
    }
    [Fact]
    public void Test_SavePatronToDatabase()
    {
      Patron testPatron = new Patron("Jack", "9715555555");

      testPatron.Save();
      List<Patron> result = Patron.GetAll();
      List<Patron> testList = new List<Patron>{testPatron};

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_FindFindsPatronInDatabase()
    {
      //Arrange
      Patron testPatron = new Patron("Jack", "9715555555");
      testPatron.Save();

      //Act
      Patron foundPatron = Patron.Find(testPatron.GetId());

      //Assert
      Assert.Equal(testPatron, foundPatron);
    }
    [Fact]
    public void Test_Update_UpdatesPatronInDatabase()
    {
      Patron testPatron = new Patron("Jack", "9715555555");
      testPatron.Save();
      testPatron.Update("Mack", "9718885555");
      Patron resultPatron = Patron.Find(testPatron.GetId());
      Assert.Equal(testPatron, resultPatron);
    }
    [Fact]
    public void Test_Delete_DeletesPatronInDatabase()
    {
      Patron testPatron = new Patron("Jack", "9715555555");
      Patron testPatron2 = new Patron("Mack", "9718885555");
      testPatron.Save();
      testPatron2.Save();
      List<Patron> testList = new List<Patron> {testPatron, testPatron2};
      testList.Remove(testPatron);
      testPatron.Delete();
      List<Patron> resultList = Patron.GetAll();

      Assert.Equal(testList, resultList);
    }
  }
}
