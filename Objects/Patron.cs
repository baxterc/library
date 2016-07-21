using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library
{
  public class Patron
  {
    private int _id;
    private string _name;
    private string _phoneNumber;

    public Patron(string name, string phoneNumber, int id = 0)
    {
      _id = id;
      _name = name;
      _phoneNumber = phoneNumber;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public string GetPhoneNumber()
    {
      return _phoneNumber;
    }
    public void SetPhoneNumber(string newPhoneNumber)
    {
      _phoneNumber = newPhoneNumber;
    }
    public override bool Equals(System.Object otherPatron)
    {
      if (!(otherPatron is Patron))
      {
        return false;
      }
      else
      {
        Patron newPatron = (Patron) otherPatron;
        bool idEquality = this.GetId() == newPatron.GetId();
        bool nameEquality = this.GetName() == newPatron.GetName();
        bool phoneNumberEquality = this.GetPhoneNumber() == newPatron.GetPhoneNumber();
        return (idEquality && nameEquality && phoneNumberEquality);
      }
    }
    public static List<Patron> GetAll()
    {
      List<Patron> allPatrons = new List<Patron>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patrons;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int patronId = rdr.GetInt32(0);
        string patronName = rdr.GetString(1);
        string patronPhone = rdr.GetString(2);
        Patron newPatron = new Patron(patronName, patronPhone, patronId);
        allPatrons.Add(newPatron);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allPatrons;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patrons (name, phonenum) OUTPUT INSERTED.id VALUES (@PatronName, @PatronNum);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@PatronName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter phoneNumParameter = new SqlParameter();
      phoneNumParameter.ParameterName = "@PatronNum";
      phoneNumParameter.Value = this.GetPhoneNumber();

      cmd.Parameters.Add(phoneNumParameter);
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
    public static Patron Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patrons WHERE id = @PatronId;", conn);
      SqlParameter patronIdParameter = new SqlParameter();
      patronIdParameter.ParameterName = "@PatronId";
      patronIdParameter.Value = id.ToString();
      cmd.Parameters.Add(patronIdParameter);
      rdr = cmd.ExecuteReader();

      int foundPatronId = 0;
      string foundPatronName = null;
      string foundPatronNum = null;

      while(rdr.Read())
      {
        foundPatronId = rdr.GetInt32(0);
        foundPatronName = rdr.GetString(1);
        foundPatronNum = rdr.GetString(2);
      }
      Patron foundPatron = new Patron(foundPatronName, foundPatronNum, foundPatronId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundPatron;
    }
    public List<Checkout> GetCheckouts()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT * FROM checkouts WHERE patron_id = @PatronId;", conn);

      SqlParameter patronIdParameter = new SqlParameter();
      patronIdParameter.ParameterName = "@PatronId";
      patronIdParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(patronIdParameter);

      rdr = cmd.ExecuteReader();

      List<Checkout> checkouts = new List<Checkout>{};
      while (rdr.Read())
      {
        int checkoutId = rdr.GetInt32(0);
        int copyId = rdr.GetInt32(1);
        int patronId = rdr.GetInt32(2);
        DateTime checkoutDate = rdr.GetDateTime(3);
        DateTime dueDate = rdr.GetDateTime(4);
        bool returned = rdr.GetBoolean(5);
        Checkout newCheckout = new Checkout(copyId, patronId, checkoutDate, dueDate, returned, checkoutId);
        checkouts.Add(newCheckout);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      return checkouts;

    }
    public void Update(string newName, string newPhoneNumber)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE patrons SET name = @NewName, phonenum = @NewPhoneNumber OUTPUT INSERTED.name, INSERTED.phonenum WHERE id = @PatronId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter newPhoneNumberParameter = new SqlParameter();
      newPhoneNumberParameter.ParameterName = "@NewPhoneNumber";
      newPhoneNumberParameter.Value = newPhoneNumber;
      cmd.Parameters.Add(newPhoneNumberParameter);

      SqlParameter PatronIdParameter = new SqlParameter();
      PatronIdParameter.ParameterName = "@PatronId";
      PatronIdParameter.Value = this.GetId();
      cmd.Parameters.Add(PatronIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._phoneNumber = rdr.GetString(1);
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
    public void Delete()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("DELETE FROM patrons WHERE id = @PatronId; DELETE FROM checkouts WHERE patron_id = @PatronId;", conn);

     SqlParameter patronIdParameter = new SqlParameter();
     patronIdParameter.ParameterName = "@PatronId";
     patronIdParameter.Value = this.GetId();

     cmd.Parameters.Add(patronIdParameter);
     cmd.ExecuteNonQuery();

     if (conn != null)
     {
       conn.Close();
     }
   }
    public static void DeleteAll()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM patrons;", conn);
     cmd.ExecuteNonQuery();
    }
  }
}
