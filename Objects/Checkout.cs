using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library
{
  public class Checkout
  {
    private int _id;
    private int _copyId;
    private int _patronId;
    private DateTime _checkoutDate;
    private DateTime _dueDate;
    private bool _returned;

    public Checkout (int copyId, int patronId, DateTime checkoutDate, DateTime dueDate, bool returned = false, int id = 0)
    {
      _id = id;
      _copyId = copyId;
      _patronId = patronId;
      _checkoutDate = checkoutDate;
      _dueDate = dueDate;
      _returned = returned;
    }

    public override bool Equals(System.Object otherCheckout)
    {
      if (!(otherCheckout is Checkout))
      {
        return false;
      }
      else
      {
        Checkout newCheckout = (Checkout) otherCheckout;
        bool idEquality = this.GetId() == newCheckout.GetId();
        bool copyIdEquality = this.GetCopyId() == newCheckout.GetCopyId();
        bool patronIdEquality = this.GetPatronId() == newCheckout.GetPatronId();
        bool checkoutDateEquality = this.GetCheckoutDate() == newCheckout.GetCheckoutDate();
        bool dueDateEquality = this.GetDueDate() == newCheckout.GetDueDate();
        bool returnedEquality = this.GetReturned() == newCheckout.GetReturned();

        return (idEquality && copyIdEquality && patronIdEquality && checkoutDateEquality && dueDateEquality && returnedEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public int GetCopyId()
    {
      return _copyId;
    }

    public int GetPatronId()
    {
      return _patronId;
    }

    public DateTime GetCheckoutDate()
    {
      return _checkoutDate;
    }

    public DateTime GetDueDate()
    {
      return _dueDate;
    }

    public bool GetReturned()
    {
      return _returned;
    }

    public void SetCopyId (int newCopyId)
    {
      _copyId = newCopyId;
    }

    public void SetPatronId (int newPatronId)
    {
      _patronId = newPatronId;
    }

    public void SetCheckoutDate (DateTime newCheckoutDate)
    {
      _checkoutDate = newCheckoutDate;
    }

    public void SetDueDate (DateTime newDueDate)
    {
      _dueDate = newDueDate;
    }

    public void SetReturned (bool newReturned)
    {
      _returned = newReturned;
    }

    public static List<Checkout> GetAll()
    {
      List<Checkout> allCheckouts = new List<Checkout> {};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM checkouts;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int checkoutId = rdr.GetInt32(0);
        int checkoutCopyId = rdr.GetInt32(1);
        int checkoutPatronId = rdr.GetInt32(2);
        DateTime checkoutDate = rdr.GetDateTime(3);
        DateTime checkoutDueDate = rdr.GetDateTime(4);
        bool checkoutReturned = rdr.GetBoolean(5);
        Checkout newCheckout = new Checkout(checkoutCopyId, checkoutPatronId, checkoutDate, checkoutDueDate, checkoutReturned, checkoutId);
        allCheckouts.Add(newCheckout);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCheckouts;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO checkouts (copy_id, patron_id, checkout_date, due_date, returned) OUTPUT INSERTED.id VALUES (@CheckoutCopyId, @CheckoutPatronId, @CheckoutDate, @CheckoutDueDate, @CheckoutReturned);", conn);

      SqlParameter copyIdParameter = new SqlParameter();
      copyIdParameter.ParameterName = "@CheckoutCopyId";
      copyIdParameter.Value = this.GetCopyId();
      cmd.Parameters.Add(copyIdParameter);
      SqlParameter patronIdParameter = new SqlParameter();
      patronIdParameter.ParameterName = "@CheckoutPatronId";
      patronIdParameter.Value = this.GetPatronId();
      cmd.Parameters.Add(patronIdParameter);
      SqlParameter checkoutDateParameter = new SqlParameter();
      checkoutDateParameter.ParameterName = "@CheckoutDate";
      checkoutDateParameter.Value = this.GetCheckoutDate();
      cmd.Parameters.Add(checkoutDateParameter);
      SqlParameter checkoutDueDateParameter = new SqlParameter();
      checkoutDueDateParameter.ParameterName = "@CheckoutDueDate";
      checkoutDueDateParameter.Value = this.GetDueDate();
      cmd.Parameters.Add(checkoutDueDateParameter);
      SqlParameter checkoutReturnedParameter = new SqlParameter();
      checkoutReturnedParameter.ParameterName = "@checkoutReturned";
      checkoutReturnedParameter.Value = this.GetReturned();
      cmd.Parameters.Add(checkoutReturnedParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static Checkout Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM checkouts WHERE id = @CheckoutId;", conn);
      SqlParameter checkoutIdParameter = new SqlParameter();
      checkoutIdParameter.ParameterName = "@CheckoutId";
      checkoutIdParameter.Value = id.ToString();
      cmd.Parameters.Add(checkoutIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCheckoutId = 0;
      int foundCopyId = 0;
      int foundPatronId = 0;
      DateTime foundCheckoutDate = DateTime.MinValue;
      DateTime foundDueDate = DateTime.MinValue;
      bool foundReturned = false;

      while(rdr.Read())
      {
        foundCheckoutId = rdr.GetInt32(0);
        foundCopyId = rdr.GetInt32(1);
        foundPatronId = rdr.GetInt32(2);
        foundCheckoutDate = rdr.GetDateTime(3);
        foundDueDate = rdr.GetDateTime(4);
        foundReturned = rdr.GetBoolean(5);
      }
      Checkout foundCheckout = new Checkout(foundCopyId, foundPatronId, foundCheckoutDate, foundDueDate, foundReturned, foundCheckoutId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCheckout;
    }

    public void Update(int newCopyId, int newPatronId, DateTime newCheckoutDate, DateTime newDueDate, bool newReturned)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE checkouts SET copy_id = @NewCopyId, patron_id = @NewPatronId, checkout_date = @NewCheckoutDate, due_date = @NewDueDate, returned = @NewReturned OUTPUT INSERTED.copy_id, INSERTED.patron_id, INSERTED.checkout_date, INSERTED.due_date, INSERTED.returned WHERE id = @CheckoutId;", conn);

      SqlParameter newCopyIdParameter = new SqlParameter();
      newCopyIdParameter.ParameterName = "@NewCopyId";
      newCopyIdParameter.Value = newCopyId;
      cmd.Parameters.Add(newCopyIdParameter);

      SqlParameter newPatronIdParameter = new SqlParameter();
      newPatronIdParameter.ParameterName = "@NewPatronId";
      newPatronIdParameter.Value = newPatronId;
      cmd.Parameters.Add(newPatronIdParameter);

      SqlParameter newCheckoutDateParameter = new SqlParameter();
      newCheckoutDateParameter.ParameterName = "@NewCheckoutDate";
      newCheckoutDateParameter.Value = newCheckoutDate;
      cmd.Parameters.Add(newCheckoutDateParameter);

      SqlParameter newDueDateParameter = new SqlParameter();
      newDueDateParameter.ParameterName = "@NewDueDate";
      newDueDateParameter.Value = newDueDate;
      cmd.Parameters.Add(newDueDateParameter);

      SqlParameter newReturnedParameter = new SqlParameter();
      newReturnedParameter.ParameterName = "@NewReturned";
      newReturnedParameter.Value = newReturned;
      cmd.Parameters.Add(newReturnedParameter);

      SqlParameter CheckoutIdParameter = new SqlParameter();
      CheckoutIdParameter.ParameterName = "@CheckoutId";
      CheckoutIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CheckoutIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._copyId = rdr.GetInt32(0);
        this._patronId = rdr.GetInt32(1);
        this._checkoutDate = rdr.GetDateTime(2);
        this._dueDate = rdr.GetDateTime(3);
        this._returned = rdr.GetBoolean(4);
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
    
    public static void DeleteAll()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM checkouts;", conn);
     cmd.ExecuteNonQuery();
    }
  }
}
