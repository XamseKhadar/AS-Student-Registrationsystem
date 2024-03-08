using Microsoft.AspNetCore.Authorization;
using Student_Registrationsystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Student_Registrationsystem.Repository
{
    [Authorize]
    public class studentsRepository
    {
        SqlConnection con;
        SqlCommand cmd;
        public studentsRepository()  //constructor
        {
            con = new SqlConnection("server=MUSTAFEALI\\SQLEXPRESS; database=studentRegistration; Trusted_Connection=True;");
        }

        public List<Students> getAll()
        {
            List<Students> data = new List<Students>();
            try
            {
                using (con)
                {
                    con.Open();
                    string _query = "SELECT TOP 50 * FROM students ORDER BY std_Id ASC; ";
                    using (SqlCommand cmd = new SqlCommand(_query, con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            data.Add(new Students() { Id = Convert.ToInt32(dr["std_Id"]), std_Fullname = dr["std_Fullname"].ToString(), std_Phone = dr["std_Phone"].ToString(), std_age = Convert.ToInt32(dr["std_age"]) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving student data. Please try again later or contact support.");
                Console.WriteLine("Error Details: " + ex.Message);
            }
            return data;
        }

        public Students get_by_id(int id)
        {
            Students data = new Students();
            try
            {
                using (con)
                {
                    con.Open();
                    string _query = $"select * from students where std_Id={id}";
                    cmd = new SqlCommand(_query, con);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        data = new Students() { Id = Convert.ToInt32(dr["std_Id"]), std_Fullname = dr["std_Fullname"].ToString(), std_Phone = dr["std_Phone"].ToString(), std_age = Convert.ToInt32(dr["std_age"]) };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving student data by ID. Please try again later or contact support.");
                Console.WriteLine("Error Details: " + ex.Message);
            }
            return data;
        }

        public bool create(string std_Fullname, string std_phone, string std_age)
        {
            try
            {
                using (con)
                {
                    con.Open();
                    string _query = $"insert into  students(std_Fullname, std_Phone, std_age) values('{@std_Fullname}','{@std_phone}','{@std_age}')";
                    cmd = new SqlCommand(_query, con);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating a new student record. Please try again later or contact support.");
                Console.WriteLine("Error Details: " + ex.Message);
                return false;
            }
        }

        public bool update(int std_Id, string std_Fullname, string std_Phone, string std_age)
        {
            try
            {
                using (con)
                {
                    con.Open();
                    string _query = $"update students set std_Fullname ='{std_Fullname}',std_Phone='{@std_Phone}',std_age='{@std_age}' where std_Id={std_Id}";
                    cmd = new SqlCommand(_query, con);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating student record. Please try again later or contact support.");
                Console.WriteLine("Error Details: " + ex.Message);
                return false;
            }
        }

        public bool delete(int id, string std_Fullname, string std_Phone, string std_age)
        {
            try
            {
                using (con)
                {
                    con.Open();
                    string _query = $"delete from students where std_Id={id} and std_Fullname='{std_Fullname}',and std_Fullname='{std_Phone}',and std_Fullname='{std_age}'";
                    cmd = new SqlCommand(_query, con);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting student record. Please try again later or contact support.");
                Console.WriteLine("Error Details: " + ex.Message);
                return false;
            }
        }
    }
}
