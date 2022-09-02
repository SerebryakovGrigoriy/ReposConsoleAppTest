using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;


namespace TestProject
{
    public class SqlDataLayer
    {
        private SqlConnection Connection { get; set; }
        
        /// <summary>
        /// Открыть подключение к базе данных
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool OpenConnection(string connectionString)
        {
            bool isOpenConnection = false;
            try
            {
                Connection = new SqlConnection(connectionString);
                Connection.Open();
                isOpenConnection = true;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
            }
            return isOpenConnection;
        }

        /// <summary>
        /// Получить имя БД
        /// </summary>
        /// <returns></returns>
        public string GetDBName()
        {
            return Connection != null ? Connection.Database : null;
        }


        /// <summary>
        /// Cуммарная зарплата в разрезе департаментов (без руководителей)
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteSqlCommand_SumSalary_Without_Chief(string sql)
        {
            System.Console.WriteLine("==============================================================");
            System.Console.WriteLine("Суммарная зарплата в разрезе департаментов (без руководителей):");
            try
            {
                var sqlCommand_SumSalary_Without_Chief = new SqlCommand(sql, Connection);
                var sqlDataReader_SumSalary_Without_Chief = sqlCommand_SumSalary_Without_Chief.ExecuteReader();

                while (sqlDataReader_SumSalary_Without_Chief.Read())
                {
                    var strOut = $"Наименование департамента : {sqlDataReader_SumSalary_Without_Chief.GetValue(1)}, Суммарная зарплата в разрезе департаментов : {sqlDataReader_SumSalary_Without_Chief.GetValue(0)}";
                    System.Console.WriteLine(strOut);
                }
                sqlDataReader_SumSalary_Without_Chief.Close();
                sqlCommand_SumSalary_Without_Chief.Dispose();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
            }
           
        }

        /// <summary>
        /// Cуммарная зарплата в разрезе департаментов (с руководителями)
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteSqlCommand_SumSalary_With_Chief(string sql)
        {
            System.Console.WriteLine("==============================================================");
            System.Console.WriteLine("Суммарная зарплата в разрезе департаментов (с руководителями):");

            try
            {
                var sqlCommand_SumSalary_With_Chief = new SqlCommand(sql, Connection);
                var sqlDataReader_SumSalary_With_Chief = sqlCommand_SumSalary_With_Chief.ExecuteReader();

                while (sqlDataReader_SumSalary_With_Chief.Read())
                {
                    var strOut = $"Наименование департамента : {sqlDataReader_SumSalary_With_Chief.GetValue(1)}, Суммарная зарплата в разрезе департаментов : {sqlDataReader_SumSalary_With_Chief.GetValue(0)}";
                    System.Console.WriteLine(strOut);
                }
                sqlDataReader_SumSalary_With_Chief.Close();
                sqlCommand_SumSalary_With_Chief.Dispose();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
            }

        }

        /// <summary>
        /// Департамент, в котором у сотрудника зарплата максимальна
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteSqlCommand_DepartmentWithMaxalary(string sql)
        {
            System.Console.WriteLine("==============================================================");
            System.Console.WriteLine("Департамент, в котором у сотрудника зарплата максимальная:");

            try
            {
                var sqlCommand_DepartmentWithMaxalary = new SqlCommand(sql, Connection);
                var sqlDataReader_DepartmentWithMaxalary = sqlCommand_DepartmentWithMaxalary.ExecuteReader();

                while (sqlDataReader_DepartmentWithMaxalary.Read())
                {
                    var strOut = $"Наименование департамента в котором у сотрудника зарплата максимальна : {sqlDataReader_DepartmentWithMaxalary.GetValue(0)} ";
                    System.Console.WriteLine(strOut);
                }
                sqlDataReader_DepartmentWithMaxalary.Close();
                sqlCommand_DepartmentWithMaxalary.Dispose();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
            }
           
        }

        /// <summary>
        /// Зарплаты руководителей департаментов (по убыванию)
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteSqlCommand_Salary_Chief_Department(string sql)
        {
            System.Console.WriteLine("==============================================================");
            System.Console.WriteLine("Зарплаты руководителей департаментов (по убыванию):");

            try
            {
                var sqlCommand_Salary_Chief_Department = new SqlCommand(sql, Connection);
                var sqlDataReader_Salary_Chief_Department = sqlCommand_Salary_Chief_Department.ExecuteReader();

                while (sqlDataReader_Salary_Chief_Department.Read())
                {
                    var strOut = $"Департамент: {sqlDataReader_Salary_Chief_Department.GetValue(0)},  Имя руководителя : {sqlDataReader_Salary_Chief_Department.GetValue(1)},  Зарплата руководителя : {sqlDataReader_Salary_Chief_Department.GetValue(2)} ";
                    System.Console.WriteLine(strOut);
                }
                sqlDataReader_Salary_Chief_Department.Close();
                sqlCommand_Salary_Chief_Department.Dispose();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
            }
           
        }

        /// <summary>
        /// Закрыть подключение к БД
        /// </summary>
        /// <param name="connectionString"></param>
        public void CloseConnection()
        {
            try
            {
               Connection.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
            }
        }

    }
}
