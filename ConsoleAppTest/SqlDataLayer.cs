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
        /// <param name="connectionString">строка подключения к БД</param>
        /// <returns></returns>
        public bool OpenConnection(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
            return (Connection.State == System.Data.ConnectionState.Open ? true : false);
        }

        /// <summary>
        /// Получить имя БД
        /// </summary>
        /// <returns></returns>
        public string GetDBName()
        {
            return Connection != null ? (Connection.State == System.Data.ConnectionState.Open ? Connection.Database : null) : null;
        }

        /// <summary>
        /// Выполнение SQL кода и формирование строки для отображения в окне консоли 
        /// </summary>
        /// <param name="caption">Заголовок результата</param>
        /// <param name="sql">sql код</param>
        /// <param name="template">строка - шаблон для формирования результата запроса</param>
        /// <param name="indexes">номера индексов полей ридера</param>
        public void ExecuteSql(string caption, string sql, string template, int[] indexes)
        {
            System.Console.WriteLine("==============================================================");
            System.Console.WriteLine(caption);
            try
            {
                var sqlCommand = new SqlCommand(sql, Connection);
                var sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    var objects = new object[indexes.Length];
                    for (int i=0;i< indexes.Length; i++)
                    {
                        objects[i] = sqlDataReader.GetValue(indexes[i]);
                    }
                    var strOut = string.Format(template, objects);
                    System.Console.WriteLine(strOut);
                }
                sqlDataReader.Close();
                sqlCommand.Dispose();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Закрыть подключение к БД
        /// </summary>
        /// <param name="connectionString"></param>
        public void CloseConnection()
        {
            if ((Connection.State != System.Data.ConnectionState.Closed) && (Connection != null))
            {
                Connection.Close();
            }
        }

    }
}
