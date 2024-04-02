using System;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

class CSharpModelGenerator
{
    public static void GenerateModels(SQLiteConnection conn)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("GeneratedModels.cs"))
            {
                foreach (var table in GetDatabaseTables(conn))
                {
                    writer.WriteLine($"public class {table} {{");
                    foreach (var column in GetColumnsForTable(conn, table))
                    {
                        writer.WriteLine($"    public {column.Type} {column.Name} {{ get; set; }}");
                    }
                    writer.WriteLine("}");
                    writer.WriteLine();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while generating C# models: {ex.Message}");
        }
    }

    private static List<string> GetDatabaseTables(SQLiteConnection conn)
    {
        List<string> tables = new List<string>();

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
        using (SQLiteDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }
        }

        return tables;
    }

    private static List<(string Name, string Type)> GetColumnsForTable(SQLiteConnection conn, string tableName)
    {
        List<(string Name, string Type)> columns = new List<(string Name, string Type)>();

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $"PRAGMA table_info({tableName});";
        using (SQLiteDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                columns.Add((reader.GetString(1), reader.GetString(2)));
            }
        }

        return columns;
    }
}
