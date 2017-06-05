using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ToDoTask
{
  public class ToDoTest
  {
    public ToDoTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo;Integrated Security=SSPI;";
    }

    // public void Dispose()
    // {
    //   Task.DeleteAll();
    // }

  // [Fact]
  //  public void Test_DatabaseEmptyAtFirst()
  //  {
  //    //Arrange, Act
  //    int result = Task.GetAll().Count;
  //
  //    //Assert
  //    Assert.Equal(0, result);
  //  }
  //
  //  [Fact]
  //   public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
  //   {
  //     //Arrange, Act
  //     Task firstTask = new Task("Mow the lawn", "Lena");
  //     Task secondTask = new Task("Mow the lawn", "Lena");
  //     Console.WriteLine(firstTask.GetAuthor());
  //     //Assert
  //     Assert.Equal(firstTask, secondTask);
  //   }
  //
  //   [Fact]
  //   public void Test_Save_AssignsIdToObject()
  //   {
  //     //Arrange
  //     Task testTask = new Task("Mow the lawn", "Lena");
  //
  //     //Act
  //     testTask.Save();
  //     Task savedTask = Task.GetAll()[0];
  //
  //     int result = savedTask.GetId();
  //     int testId = testTask.GetId();
  //
  //     //Assert
  //     Assert.Equal(testId, result);
  //   }
  //
  //   [Fact]
  //   public void Test_Find_FindsTaskInDatabase()
  //   {
  //     //Arrange
  //     Task testTask = new Task("Mow the lawn", "Lena");
  //     testTask.Save();
  //
  //     //Act
  //     Task foundTask = Task.Find(testTask.GetId());
  //
  //     //Assert
  //     Assert.Equal(testTask, foundTask);
  //   }

    [Fact]
    public void Test_GetTasks_RetrievesAllTasksWithAuthorName()
    {

      Task firstTask = new Task("walk the dog", "Lena");
      firstTask.Save();
      Task secondTask = new Task("buy some food", "Lena");
      secondTask.Save();
      Task thirdTask = new Task("eat the food", "David");
      thirdTask.Save();
      Task fourthTask = new Task("sleep after eating", "David");
      fourthTask.Save();
      Task fifthTask = new Task("buy some more food for David", "Lena");
      fifthTask.Save();
      string author = "Lena";

      List<Task> testTaskList = new List<Task> {firstTask, secondTask, fifthTask};
      List<Task> resultTaskList = Task.GetTasks(author);

      Assert.Equal(testTaskList, resultTaskList);
    }
  }
}
