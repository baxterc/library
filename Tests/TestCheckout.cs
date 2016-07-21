using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class CheckoutTest : IDisposable
  {
    public CheckoutTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Checkout.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Checkout.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfPatronsAreTheSame()
    {
      //Arrange, Act
      Checkout firstCheckout = new Checkout(1, 1, new DateTime(2016, 7, 23), new DateTime(2016, 8, 23), false);
      Checkout secondCheckout = new Checkout(1, 1, new DateTime(2016, 7, 23), new DateTime(2016, 8, 23), false);

      //Assert
      Assert.Equal(firstCheckout, secondCheckout);
    }
    [Fact]
    public void Test_SaveCheckoutToDatabase()
    {
      Checkout testCheckout = new Checkout(1, 1, new DateTime(2016, 7, 23), new DateTime(2016, 8, 23), false);

      testCheckout.Save();
      List<Checkout> result = Checkout.GetAll();
      List<Checkout> testList = new List<Checkout>{testCheckout};

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_FindFindsCheckoutInDatabase()
    {
      //Arrange
      Checkout testCheckout = new Checkout(1, 1, new DateTime(2016, 7, 23), new DateTime(2016, 8, 23), false);
      testCheckout.Save();

      //Act
      Checkout foundCheckout = Checkout.Find(testCheckout.GetId());

      //Assert
      Assert.Equal(testCheckout, foundCheckout);
    }

    [Fact]
    public void Test_Update_UpdatesCheckoutInDatabase()
    {
      Checkout testCheckout = new Checkout(1, 1, new DateTime(2016, 7, 23), new DateTime(2016, 8, 23), false);
      testCheckout.Save();
      testCheckout.Update(1, 2, new DateTime(2016, 7, 23), new DateTime(2016, 8, 23), true);
      Checkout resultCheckout = Checkout.Find(testCheckout.GetId());
      Assert.Equal(testCheckout, resultCheckout);
    }
    [Fact]
    public void Test_Delete_DeletesCheckoutFromDatabase()
    {
      Checkout firstCheckout = new Checkout(1, 1, new DateTime(2016, 7, 23), new DateTime(2016, 8, 23), false);
      firstCheckout.Save();
      Checkout secondCheckout = new Checkout(1, 2, new DateTime(2016, 7, 23), new DateTime(2016, 8, 23), false);
      secondCheckout.Save();
      List<Checkout> testCheckouts = new List<Checkout>{firstCheckout, secondCheckout};

      firstCheckout.Delete();
      testCheckouts.Remove(firstCheckout);
      List <Checkout> resultCheckouts = Checkout.GetAll();

      Assert.Equal(testCheckouts, resultCheckouts);
    }
  }
}
