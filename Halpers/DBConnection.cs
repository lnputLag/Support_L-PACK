﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Support_L_PACK.Halpers
{
    public class DBConnection
    {
        private static string _connectionString;
        private static readonly Lazy<DBConnection> _instance =
        new Lazy<DBConnection>(() => new DBConnection());

        private DBConnection()
        {

        }

        public static DBConnection Instance => _instance.Value;

        public static async Task InitAsync()
        {
            // Получаем строку подключения из app.config
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // Проверка, что строка подключения не пустая
            if (string.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("Ошибка: строка подключения не найдена в app.config.");
                return;
            }

            // Использование строки подключения для подключения к MySQL
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    MessageBox.Show("Подключение к базе данных MySQL успешно установлено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        // Метод для выполнения запросов без возврата данных (INSERT, UPDATE, DELETE)
        public static async Task<int> ExecuteNonQueryAsync(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при выполнении запроса: " + ex.Message);
                }
            }

        }

        // Метод для выполнения запросов с возвратом данных (SELECT)
        public static async Task<DataTable> ExecuteQueryAsync(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();

                            await Task.Run(() => adapter.Fill(dataTable));
                            //adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при выполнении запроса: " + ex.Message);
                }
            }
        }


        public static async Task<object> ExecuteScalarAsync(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return await command.ExecuteScalarAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при выполнении запроса: " + ex.Message);
                }
            }
        }
    }
}
