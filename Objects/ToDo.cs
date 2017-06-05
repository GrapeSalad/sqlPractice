using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ToDoTask
{
  public class Task
  {
    private int _id;
    private string _description;
    private string _author;


    public Task(string Description, string author, int Id = 0)
    {
      _id = Id;
      _description = Description;
      _author = author;
    }

    public Task(string Description, int Id = 0)
    {
      _id = Id;
      _description = Description;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }

    public string GetAuthor()
    {
      return _author;
    }

    public void SetAuthor(string author)
    {
      _author = author;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public override bool Equals(System.Object otherTask)
      {
        if (!(otherTask is Task))
        {
          return false;
        }
        else
        {
         Task newTask = (Task) otherTask;
         bool idEquality = (this.GetId() == newTask.GetId());
         bool descriptionEquality = (this.GetDescription() == newTask.GetDescription());
         bool authorEquality = (this.GetAuthor() == newTask.GetAuthor());

         return (idEquality && descriptionEquality && authorEquality);
        }
      }

    public static List<Task> GetAll()
    {
      List<Task> allTasks = new List<Task>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT id, description, author FROM tasks;", conn);
      // Console.WriteLine(cmd);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        string taskAuthor = rdr.GetString(2);
        Task newTask = new Task(taskDescription, taskAuthor, taskId);
        allTasks.Add(newTask);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allTasks;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO tasks (description, author) OUTPUT INSERTED.id VALUES (@TaskDescription, @TaskAuthor);", conn);

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@TaskDescription";
      descriptionParameter.Value = this.GetDescription();

      SqlParameter authorParameter = new SqlParameter();
      authorParameter.ParameterName = "@TaskAuthor";
      authorParameter.Value = this.GetAuthor();


      cmd.Parameters.Add(descriptionParameter);
      cmd.Parameters.Add(authorParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM tasks;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static Task Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks WHERE id = @TaskId;", conn);
      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = id.ToString();
      cmd.Parameters.Add(taskIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundTaskId = 0;
      string foundTaskDescription = null;
      string foundTaskAuthor = null;

      while(rdr.Read())
      {
        foundTaskId = rdr.GetInt32(0);
        foundTaskDescription = rdr.GetString(1);
        foundTaskAuthor = rdr.GetString(2);
      }
      Task foundTask = new Task(foundTaskDescription, foundTaskAuthor, foundTaskId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundTask;
    }
  }
}
