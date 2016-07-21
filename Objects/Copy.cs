using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library
{
  public class Copy
  {
    private int _id;
    private int _bookId;
    private bool _status;

    public Copy (int bookId, bool status = false, int id = 0)
    {
      _id = id;
      _bookId = bookId;
      _status = status;
    }

    public override bool Equals(System.Object otherCopy)
    {
      if (!(otherCopy is Copy))
      {
        return false;
      }
      else
      {
        Copy newCopy = (Copy) otherCopy;
        bool idEquality = this.GetId() == newCopy.GetId();
        bool bookIdEquality = this.GetBookId() == newCopy.GetBookId();
        bool statusEquality = this.GetStatus() == newCopy.GetStatus();
        return (idEquality && bookIdEquality && statusEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public int GetBookId()
    {
      return _bookId;
    }
    public bool GetStatus()
    {
      return _status;
    }

    public void SetBookId (int newBookId)
    {
      _id = newBookId;
    }
    public void setStatus (bool newStatus)
    {
      _status = newStatus;
    }

    public static List<Copy> GetAll()
    {
      List<Copy> allCopies = new List<Copy>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int copyId = rdr.GetInt32(0);
        int copyBookId = rdr.GetInt32(1);
        bool copyStatus = rdr.GetBoolean(2);
        Copy newCopy = new Copy(copyBookId, copyStatus, copyId);
        allCopies.Add(newCopy);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCopies;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO copies (book_id, status) OUTPUT INSERTED.id VALUES (@BookId, @Status);", conn);

      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetBookId();
      cmd.Parameters.Add(bookIdParameter);

      SqlParameter statusParameter = new SqlParameter();
      statusParameter.ParameterName = "@Status";
      statusParameter.Value = this.GetStatus();
      cmd.Parameters.Add(statusParameter);

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

    public static Copy Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies WHERE id = @CopyId;", conn);
      SqlParameter copyIdParameter = new SqlParameter();
      copyIdParameter.ParameterName = "@CopyId";
      copyIdParameter.Value = id.ToString();
      cmd.Parameters.Add(copyIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCopyId = 0;
      int foundCopyBookId = 0;
      bool foundCopyStatus = false;

      while(rdr.Read())
      {
        foundCopyId = rdr.GetInt32(0);
        foundCopyBookId = rdr.GetInt32(1);
        foundCopyStatus = rdr.GetBoolean(2);
      }
      Copy foundCopy = new Copy(foundCopyBookId, foundCopyStatus, foundCopyId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCopy;
    }

    public void Update(int newBookId, bool newStatus)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE copies SET book_id = @NewBookId, status = @NewStatus OUTPUT INSERTED.book_id, INSERTED.status WHERE id = @CopyId;", conn);

      SqlParameter newBookIdParameter = new SqlParameter();
      newBookIdParameter.ParameterName = "@NewBookId";
      newBookIdParameter.Value = newBookId;
      cmd.Parameters.Add(newBookIdParameter);

      SqlParameter newStatusParameter = new SqlParameter();
      newStatusParameter.ParameterName = "@NewStatus";
      newStatusParameter.Value = newStatus;
      cmd.Parameters.Add(newStatusParameter);

      SqlParameter CopyIdParameter = new SqlParameter();
      CopyIdParameter.ParameterName = "@CopyId";
      CopyIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CopyIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._bookId = rdr.GetInt32(0);
        this._status = rdr.GetBoolean(1);
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

      SqlCommand cmd = new SqlCommand("DELETE FROM copies WHERE id = @CopyId; DELETE FROM checkouts WHERE copy_id = @CopyId", conn);

      SqlParameter CopyIdParameter = new SqlParameter();
      CopyIdParameter.ParameterName = "@CopyId";
      CopyIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CopyIdParameter);
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
     SqlCommand cmd = new SqlCommand("DELETE FROM copies;", conn);
     cmd.ExecuteNonQuery();
    }
  }
}
