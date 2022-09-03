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
            try
            {
                var sqlDataLayer = new SqlDataLayer();
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\ConnectionString.txt";
                if (!System.IO.File.Exists(path))
                {
                    System.Console.WriteLine("Файл 'ConnectionString.txt' не найден!");
                    return;
                }
                // получить строку подключения из файла ConnectionString.txt, который должен быть расположен в директории запуска исполняемого файла ConsoleAppTest.exe
                var connectionString = System.IO.File.ReadAllText(path);

                if (string.IsNullOrEmpty(connectionString))
                {
                    System.Console.WriteLine("Файл 'ConnectionString.txt' должен быть заполнен!");
                    return;
                }
                if (sqlDataLayer.OpenConnection(connectionString))
                {
                    string DatabaseName = $"[{sqlDataLayer.GetDBName()}]";

                    //-- суммарная зарплата в разрезе департаментов (без руководителей)
                    sqlDataLayer.ExecuteSql("Суммарная зарплата в разрезе департаментов (без руководителей):",
                                            $"SELECT SUM(e.salary) AS sum_salary_per_department, d.name FROM{DatabaseName}.[dbo].[employee]  e JOIN{DatabaseName}.[dbo].[department] d ON e.department_id = d.id WHERE EXISTS(SELECT 1  FROM employee e2  WHERE e2.department_id = e.department_id)  AND e.id not in (SELECT distinct ex.chief_id FROM{DatabaseName}.[dbo].[employee] ex where ex.chief_id is not null) GROUP BY d.name",
                                            @"Наименование департамента : {1}, Суммарная зарплата в разрезе департаментов : {0}",
                                            new int[] { 1, 0 });


                    //-- суммарная зарплата в разрезе департаментов (с руководителями)
                    sqlDataLayer.ExecuteSql("Суммарная зарплата в разрезе департаментов(с руководителями):",
                                             $"SELECT SUM(e.salary) AS sum_salary_per_department, d.name FROM{DatabaseName}.[dbo].[employee]  e JOIN{DatabaseName}.[dbo].[department] d ON e.department_id = d.id WHERE EXISTS(SELECT 1  FROM employee e2  WHERE e2.department_id = e.department_id) GROUP BY d.name",
                                             @"Наименование департамента : {1}, Суммарная зарплата в разрезе департаментов: {0}",
                                             new int[] { 1, 0 });


                    //-- Департамент, в котором у сотрудника зарплата максимальна
                    sqlDataLayer.ExecuteSql("Департамент, в котором у сотрудника зарплата максимальная:",
                                            $"SELECT d.name FROM{DatabaseName}.[dbo].[department] d WHERE d.id in (SELECT top(1) e.department_id FROM{DatabaseName}.[dbo].[employee]  e order by e.salary desc)",
                                            @"Наименование департамента в котором у сотрудника зарплата максимальна : {0}",             
                                            new int[] { 0 });

                    //-- Зарплаты руководителей департаментов (по убыванию)
                    sqlDataLayer.ExecuteSql("Зарплаты руководителей департаментов (по убыванию):",
                                             $"SELECT d.name, a.name, a.salary  from{DatabaseName}.[dbo].[employee]  a LEFT JOIN{DatabaseName}.[dbo].[department] d ON a.department_id = d.id where  a.id in (SELECT distinct e.chief_id FROM{DatabaseName}.[dbo].[employee] e where e.chief_id is not null)  order by a.salary desc",
                                             @"Департамент: {0},  Имя руководителя : {1},  Зарплата руководителя : {2}",
                                             new int[] { 0, 1, 2 });
                }

                sqlDataLayer.CloseConnection();

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            System.Console.ReadKey();
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      