using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MyEntityModel
{
    public class EmployeeRepository
    {
        public static List<Employee> GetEmployees(int pageIndex, int pageSize,
            string sortExpression, string sortDirection, out int totalRows)
        {
            List<Employee> listEmployees = new List<Employee>();

            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployees_by_PageIndex_and_PageSize", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramStartIndex = new SqlParameter();
                paramStartIndex.ParameterName = "@PageIndex";
                paramStartIndex.Value = pageIndex;
                cmd.Parameters.Add(paramStartIndex);

                SqlParameter paramMaximumRows = new SqlParameter();
                paramMaximumRows.ParameterName = "@PageSize";
                paramMaximumRows.Value = pageSize;
                cmd.Parameters.Add(paramMaximumRows);

                SqlParameter paramSortExpression = new SqlParameter();
                paramSortExpression.ParameterName = "@SortExpression";
                paramSortExpression.Value = sortExpression;
                cmd.Parameters.Add(paramSortExpression);

                SqlParameter paramSortDirection = new SqlParameter();
                paramSortDirection.ParameterName = "@SortDirection";
                paramSortDirection.Value = sortDirection;
                cmd.Parameters.Add(paramSortDirection);

                SqlParameter paramOutputTotalRows = new SqlParameter();
                paramOutputTotalRows.ParameterName = "@TotalRows";
                paramOutputTotalRows.Direction = ParameterDirection.Output;
                paramOutputTotalRows.SqlDbType = SqlDbType.Int;

                cmd.Parameters.Add(paramOutputTotalRows);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.FirstName = rdr["FirstName"].ToString();
                    employee.LastName = rdr["LastName"].ToString();
                    employee.Title = rdr["Title"].ToString();
                    employee.City = rdr["City"].ToString();

                    listEmployees.Add(employee);
                }

                rdr.Close();
                totalRows = (int)cmd.Parameters["@TotalRows"].Value;

            }
            return listEmployees;
        }

        public static List<Employee> GetEmployeesUsingEF(int pageIndex, int pageSize,
            string sortExpression, string sortDirection, out int totalRows)
        {
            List<Employee> listEmployees = new List<Employee>();

            using (NorthwindEntities entities = new NorthwindEntities())
            {
                int skipRows = (pageIndex * pageSize);

                var result = entities.Employees.SortBy(sortExpression, sortDirection).Skip(skipRows).Take(pageSize);

                //    var result = from employee in entities.Employees
                //                 select employee;

                //    if (sortDirection == "ASC")
                //    {
                //        switch (sortExpression)
                //        {
                //            case "EmployeeID":
                //                result = result.OrderBy(x => x.EmployeeID).Skip(skipRows).Take(pageSize);
                //                break;
                //            case "FirstName":
                //                result = result.OrderBy(x => x.FirstName).Skip(skipRows).Take(pageSize);
                //                break;
                //            case "LastName":
                //                result = result.OrderBy(x => x.FirstName).Skip(skipRows).Take(pageSize);
                //                break;
                //            case "Title":
                //                result = result.OrderBy(x => x.FirstName).Skip(skipRows).Take(pageSize);
                //                break;
                //            case "City":
                //                result = result.OrderBy(x => x.FirstName).Skip(skipRows).Take(pageSize);
                //                break;
                //            default:
                //                result = result.OrderBy(x => x.EmployeeID).Skip(skipRows).Take(pageSize);
                //                break;
                //        }
                //    }
                //    else if (sortDirection == "DESC")
                //    {
                //        switch (sortExpression)
                //        {
                //            case "EmployeeID":
                //                result = result.OrderByDescending(x => x.EmployeeID).Skip(skipRows).Take(pageSize);
                //                break;
                //            case "FirstName":
                //                result = result.OrderByDescending(x => x.FirstName).Skip(skipRows).Take(pageSize);
                //                break;
                //            case "LastName":
                //                result = result.OrderByDescending(x => x.FirstName).Skip(skipRows).Take(pageSize);
                //                break;
                //            case "Title":
                //                result = result.OrderByDescending(x => x.FirstName).Skip(skipRows).Take(pageSize);
                //                break;
                //            case "City":
                //                result = result.OrderByDescending(x => x.FirstName).Skip(skipRows).Take(pageSize);
                //                break;
                //            default:
                //                result = result.OrderByDescending(x => x.EmployeeID).Skip(skipRows).Take(pageSize);
                //                break;
                //        }
                //    }

                if (result != null)
                {
                    foreach (Employee emp in result)
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = emp.EmployeeID;
                        employee.FirstName = emp.FirstName;
                        employee.LastName = emp.LastName;
                        employee.Title = emp.Title;
                        employee.City = emp.City;

                        listEmployees.Add(employee);
                    }
                }
                totalRows = entities.Employees.Count();
            }
            return listEmployees;
        }
    }
}