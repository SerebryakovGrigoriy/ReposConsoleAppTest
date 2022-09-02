using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {


            string connetionString;
            connetionString = @"Data Source=GTR-1\SQLSERVER;Initial Catalog=CVD;User ID=sa;Password=j3qq5012zX";

            var cnn = new SqlConnection(connetionString);
            cnn.Open();
            System.Console.WriteLine("Connection Open  !");


            //-- суммарная зарплата в разрезе департаментов
            var sqlCommand_SumSalary = new SqlCommand("SELECT SUM(e.salary) AS sum_salary_per_department, d.name FROM[CVD].[dbo].[employee]  e JOIN[CVD].[dbo].[department] d ON e.department_id = d.id WHERE EXISTS(SELECT 1  FROM employee e2  WHERE e2.department_id = e.department_id  ) GROUP BY d.name", cnn);
            var sqlDataReader_SumSalary = sqlCommand_SumSalary.ExecuteReader();

            while (sqlDataReader_SumSalary.Read())
            {
                var strOut = $"Sum salary per department : {sqlDataReader_SumSalary.GetValue(0)}, Departmant name : {sqlDataReader_SumSalary.GetValue(1)}";
                System.Console.WriteLine(strOut);
            }
            sqlDataReader_SumSalary.Close();
            sqlCommand_SumSalary.Dispose();


            //-- Департамент, в котором у сотрудника зарплата максимальна
            var sqlCommand_DepartmentWithMaxalary = new SqlCommand("SELECT d.name FROM[CVD].[dbo].[department] d WHERE d.id in (SELECT top(1) e.department_id FROM[CVD].[dbo].[employee]  e order by e.salary desc)", cnn);
            var sqlDataReader_DepartmentWithMaxalary = sqlCommand_DepartmentWithMaxalary.ExecuteReader();

            while (sqlDataReader_DepartmentWithMaxalary.Read())
            {
                var strOut = $"Sum salary per department : {sqlDataReader_DepartmentWithMaxalary.GetValue(0)}, Departmant name : {sqlDataReader_DepartmentWithMaxalary.GetValue(1)}";
                System.Console.WriteLine(strOut);
            }
            sqlDataReader_DepartmentWithMaxalary.Close();
            sqlCommand_DepartmentWithMaxalary.Dispose();


            cnn.Close();
            System.Console.WriteLine("Connection Closed  !");
            System.Console.ReadKey();
        }
    }
}
