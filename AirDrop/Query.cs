using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Forms;

// Класс для работы с БД
class Query
{
    //public static string connect = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bdqt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    public static string connect = "Data Source = bdqt.db; Version = 3";    // Строка подключения к БД

    // Проверка подлючения к базе
    public static bool TryConnect()
    {
        try
        {
            using (SQLiteConnection Connection = new SQLiteConnection(connect))
            {
                SQLiteCommand Command = new SQLiteCommand("SELECT * FROM Aircraft", Connection);    // Любой запрос для проверки подключения
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                Connection.Dispose();
            }
        }
        catch(Exception e)
        {
            MessageBox.Show("Ошибка подключения к базе данных\n" + e.Message, "Ошибка");
            return false;
        }
        return true;
    }

    // Выполнить запрос на вставку, обновление или удаления таблицы
    public static void InUpDel(string query, ref bool rez)
    {
        using (SQLiteConnection Connection = new SQLiteConnection(connect))
        {
            SQLiteCommand Command = new SQLiteCommand(query, Connection);
            Connection.Open();
            try
            {
                if (Command.ExecuteNonQuery() == 1) // если 1 то добавлено
                    rez = true;
                else
                    rez = false;
            }
            catch (Exception e)
            {
                if (!e.Message.Contains("UNIQUE"))      // Если исключение не об ункальности записи
                    MessageBox.Show(e.Message);
            }
            Connection.Close();
            Connection.Dispose();
        }
    }

    // Выполнить запрос на получение данных из БД
    public static List<string[]> Select(string query)
    {
        // Список массива строк (таблица)
        List<string[]> List_db = new List<string[]>();
        // Массив строк (ряд таблицы)
        string[] str_bd;

        using (SQLiteConnection Connection = new SQLiteConnection(connect))
        {
            SQLiteCommand Command = new SQLiteCommand(query, Connection);
            Connection.Open();
            try
            {
                SQLiteDataReader reader = Command.ExecuteReader();
                // Построчное считывание
                while (reader.Read())
                {
                    str_bd = new string[reader.FieldCount];
                    // Цикл по столбцам
                    for (int i = 0; i < reader.FieldCount; i++)
                        str_bd[i] = reader[i].ToString();

                    List_db.Add(str_bd);
                }
            }
            // Обработка исключения, если считывание не удалось
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            Connection.Close();
            Connection.Dispose();
        }

        return List_db;
    }
}
