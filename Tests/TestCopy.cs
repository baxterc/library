using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class CopyTest : IDisposable
  {
    public CopyTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Author.DeleteAll();
      Book.DeleteAll();
      Copy.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Copy.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfCopiesAreTheSame()
    {
      //Arrange, Act
      Copy firstCopy = new Copy(1, false, 1);
      Copy secondCopy = new Copy(1, false, 1);

      //Assert
      Assert.Equal(firstCopy, secondCopy);
    }
    [Fact]
    public void Test_SaveCopyToDatabase()
    {
      Copy testCopy = new Copy(1, false);

      testCopy.Save();
      List<Copy> result = Copy.GetAll();
      List<Copy> testList = new List<Copy>{testCopy};

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_FindFindsCopyInDatabase()
    {
      //Arrange
      Copy testCopy = new Copy(1, false);
      testCopy.Save();

      //Act
      Copy foundCopy = Copy.Find(testCopy.GetId());

      //Assert
      Assert.Equal(testCopy, foundCopy);
    }
    [Fact]
    public void Test_Update_UpdatesCopyInDatabase()
    {
      Copy testCopy = new Copy(1, false);
      testCopy.Save();
      testCopy.Update(2,false);
      Copy resultCopy = Copy.Find(testCopy.GetId());
      Assert.Equal(testCopy, resultCopy);
    }
    [Fact]
    public void Test_Delete_DeletesCopyInDatabase()
    {
      Copy testCopy = new Copy(1, false);
      Copy testCopy2 = new Copy(1, true);
      testCopy.Save();
      testCopy2.Save();
      List<Copy> testList = new List<Copy> {testCopy, testCopy2};
      testList.Remove(testCopy);
      testCopy.Delete();
      List<Copy> resultList = Copy.GetAll();

      Assert.Equal(testList, resultList);
    }
  }
}
