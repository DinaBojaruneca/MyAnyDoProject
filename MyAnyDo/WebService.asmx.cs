using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Web.Script.Services;

namespace MyAnyDo
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        string connstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        [WebMethod]
        public void GetCategory()
        {
            List<Category> listCategories = new List<Category>();

            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand("Select * from Category", con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                //DataView dataViewTasks = new DataView(ds.Tables[1]);
                foreach (DataRow categoryDataRow in ds.Tables[0].Rows)
                {
                    Category category = new Category();
                    category.Id = Convert.ToInt32(categoryDataRow["Id"]);
                    category.Name = categoryDataRow["Name"].ToString();

                    //dataViewTasks.RowFilter = "CategoryId = '" + category.Id + "'";

                    //List<Task> listTasks = new List<Task>();
                    
                    //foreach (DataRowView taskDataRowView in dataViewTasks)
                    //{
                    //    DataRow taskDataRow = taskDataRowView.Row;
                    //    Task task = new Task();
                    //    task.Id = Convert.ToInt32(taskDataRow["Id"]);
                    //    task.Name = taskDataRow["Name"].ToString();
                    //    task.CategoryId = Convert.ToInt32(taskDataRow["CategoryId"]);
                    //    task.HighPriority = taskDataRow["HighPriority"].ToString();
                    //    task.TimeId = Convert.ToInt32(taskDataRow["TimeId"]);
                                               
                    //    listTasks.Add(task);
                    //}

                    //category.Tasks = listTasks;
                    listCategories.Add(category);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listCategories));
        }

        [WebMethod]
        public void GetTask()
        {            
            List<Task> listTasks = new List<Task>();

            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand(" Select * from Task", con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                DataView dataViewTasks = new DataView(ds.Tables[0]);                

                    foreach (DataRowView taskDataRowView in dataViewTasks)
                    {
                        DataRow taskDataRow = taskDataRowView.Row;
                        Task task = new Task();
                        task.Id = Convert.ToInt32(taskDataRow["Id"]);
                        task.Name = taskDataRow["Name"].ToString();
                        task.CategoryId = Convert.ToInt32(taskDataRow["CategoryId"]);
                        task.HighPriority = taskDataRow["HighPriority"].ToString();
                        task.TimeId = Convert.ToInt32(taskDataRow["TimeId"]);
                                            
                        listTasks.Add(task);
                    }                    
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listTasks));
        }

        [WebMethod]       
        public void GetSubTask()
        {
            List<SubTask> listSubTasks = new List<SubTask>();
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand(" Select * from SubTask", con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                DataView dataViewSubTask = new DataView(ds.Tables[0]);
                foreach (DataRowView subTaskDataRowView in dataViewSubTask)
                {
                    DataRow subTaskDataRow = subTaskDataRowView.Row;
                    SubTask subTask = new SubTask();
                    subTask.Id = Convert.ToInt32(subTaskDataRow["Id"]);
                    subTask.Name = subTaskDataRow["Name"].ToString();
                    subTask.TaskId = Convert.ToInt32(subTaskDataRow["TaskId"]);

                    listSubTasks.Add(subTask);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listSubTasks));
        }

        [WebMethod]
        public void GetNote()
        {
            List<Note> listNotes = new List<Note>();
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand(" Select * from Note", con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                DataView dataViewNote = new DataView(ds.Tables[0]);
                foreach (DataRowView noteDataRowView in dataViewNote)
                {
                    DataRow noteDataRow = noteDataRowView.Row;
                    Note note = new Note();
                    note.Id = Convert.ToInt32(noteDataRow["Id"]);
                    note.Name = noteDataRow["Name"].ToString();
                    note.TaskId = Convert.ToInt32(noteDataRow["TaskId"]);

                    listNotes.Add(note);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listNotes));
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InsertCategory(string name)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Category (Name) VALUES (@Name)";
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }
        

        [WebMethod]
        public void GetTaskByTime()
        {
            List<Time> listTimes = new List<Time>();

            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand("Select * from Time; Select * from Task", con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                DataView dataViewTasks = new DataView(ds.Tables[1]);
                foreach (DataRow timeDataRow in ds.Tables[0].Rows)
                {
                    Time time = new Time();
                    time.Id = Convert.ToInt32(timeDataRow["Id"]);
                    time.Name = timeDataRow["Name"].ToString();

                    dataViewTasks.RowFilter = "TimeId = '" + time.Id + "'";

                    List<Task> listTasks = new List<Task>();                   

                    foreach (DataRowView taskDataRowView in dataViewTasks)
                    {
                        DataRow taskDataRow = taskDataRowView.Row;
                        Task task = new Task();
                        task.Id = Convert.ToInt32(taskDataRow["Id"]);
                        task.Name = taskDataRow["Name"].ToString();
                        task.CategoryId = Convert.ToInt32(taskDataRow["CategoryId"]);
                        task.HighPriority = taskDataRow["HighPriority"].ToString();
                        task.TimeId = Convert.ToInt32(taskDataRow["TimeId"]);
                                              
                        listTasks.Add(task);
                    }

                    time.Tasks = listTasks;
                    listTimes.Add(time);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listTimes));
        }

        [WebMethod]            
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InsertTask(int categoryId, string name, int timeId, int highPri)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Task (Name, CategoryId, TimeId, HighPriority) VALUES (@Name, @CategoryId, @TimeId, @HighPriority)";
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@TimeId", timeId);
                    cmd.Parameters.AddWithValue("@HighPriority", highPri);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InsertSubTask(string name, int taskId)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO SubTask (Name, TaskId) VALUES (@Name, @TaskId)";
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@TaskId", taskId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InsertNote(string name, int taskId)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Note (Name, TaskId) VALUES (@Name, @TaskId)";
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@TaskId", taskId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM Category WHERE Id = " + id + "";
                    int roweffected = cmd.ExecuteNonQuery();
                    return roweffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteTask(int id)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM Task WHERE Id = " + id + "";
                    int roweffected = cmd.ExecuteNonQuery();
                    return roweffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteSubTask(int id)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM SubTask WHERE Id = " + id + "";
                    int roweffected = cmd.ExecuteNonQuery();
                    return roweffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteNote(int id)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM Note WHERE Id = " + id + "";
                    int roweffected = cmd.ExecuteNonQuery();
                    return roweffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int SetAsHighPriority(int id, int value)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE Task SET HighPriority="+value+"  WHERE Id = "+id+"";
                    //cmd.Parameters.AddWithValue("@value", value);
                    //cmd.Parameters.AddWithValue("@TaskId", id);
                    int roweffected = cmd.ExecuteNonQuery();
                    return roweffected;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
        }

    }        
}
