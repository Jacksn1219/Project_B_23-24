using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

class Program
{
    static void MyEntryPoint(string[] args)
    {
        GenerateCSharpModels();
    }

    static void GenerateCSharpModels()
    {
        SQLiteConnection sqlite_conn;
        string databasePath = GetDatabasePath();
        try
        {
            sqlite_conn = CreateConnection(databasePath);

            // Get table names in the database
            List<string> tableNames = GetTableNames(sqlite_conn);

            // Generate C# class models for each table
            foreach (string tableName in tableNames)
            {
                string classModel = GenerateClassModel(sqlite_conn, tableName);
                Console.WriteLine(classModel);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static string GetDatabasePath()
    {
        string databaseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource");
        string databasePath = Path.Combine(databaseFolder, "database.db");
        return databasePath;
    }

    static SQLiteConnection CreateConnection(string databasePath)
    {
        SQLiteConnection sqlite_conn = new SQLiteConnection($"Data Source={databasePath}; Version=3; New=True; Compress=True;");
        sqlite_conn.Open();
        return sqlite_conn;
    }

    static List<string> GetTableNames(SQLiteConnection conn)
    {
        List<string> tableNames = new List<string>();
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tableNames.Add(reader.GetString(0));
                }
            }
        }
        return tableNames;
    }

    static string GenerateClassModel(SQLiteConnection conn, string tableName)
    {
        string classModel = $"public class {tableName}" + Environment.NewLine + "{" + Environment.NewLine;

        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = $"PRAGMA table_info('{tableName}');";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string columnName = reader.GetString(1);
                    string columnType = reader.GetString(2);
                    classModel += $"    public {GetCSharpType(columnType)} {columnName} {{ get; set; }}" + Environment.NewLine;
                }
            }
        }

        classModel += "}";
        return classModel;
    }

    static string GetCSharpType(string dbType)
    {
        switch (dbType.ToLower())
        {
            case "integer":
                return "int";
            case "real":
                return "double";
            case "text":
                return "string";
            default:
                return "object";
        }
    }

    
}
