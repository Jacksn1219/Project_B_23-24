using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

public class DatabaseToCSharpModelGenerator
{
    private static readonly string connectionString = $"Data Source={GetDatabasePath()};Version=3;";

    public static void GenerateModel()
    {
        var tables = GetDatabaseSchema();

        foreach (var table in tables)
        {
            Console.WriteLine($"public class {table.TableName}");
            Console.WriteLine("{");

            foreach (var column in table.Columns)
            {
                Console.WriteLine($"    public {column.DataType} {column.ColumnName} {{ get; set; }}");
            }

            Console.WriteLine("}");
            Console.WriteLine();
        }
    }

    private static List<DatabaseTable> GetDatabaseSchema()
    {
        List<DatabaseTable> tables = new List<DatabaseTable>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            DataTable schema = connection.GetSchema("Tables");

            foreach (DataRow row in schema.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                List<DatabaseColumn> columns = new List<DatabaseColumn>();

                using (SQLiteCommand command = new SQLiteCommand($"PRAGMA table_info({tableName})", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader["name"].ToString();
                            string dataType = reader["type"].ToString();

                            columns.Add(new DatabaseColumn
                            {
                                ColumnName = columnName,
                                DataType = ConvertSQLiteTypeToCSharpType(dataType)
                            });
                        }
                    }
                }

                tables.Add(new DatabaseTable
                {
                    TableName = tableName,
                    Columns = columns
                });
            }

            connection.Close();
        }

        return tables;
    }

    private static string GetDatabasePath()
    {
        string folderName = "DataSource";
        string folderPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\" + folderName));
        System.IO.Directory.CreateDirectory(folderPath);
        return System.IO.Path.Combine(folderPath, "database.db");
    }

    private static string ConvertSQLiteTypeToCSharpType(string sqliteType)
    {
        switch (sqliteType.ToUpper())
        {
            case "INTEGER":
                return "int";
            case "TEXT":
                return "string";
            case "REAL":
                return "double";
            default:
                return "object";
        }
    }
}

public class DatabaseTable
{
    public string TableName { get; set; }
    public List<DatabaseColumn> Columns { get; set; }
}

public class DatabaseColumn
{
    public string ColumnName { get; set; }
    public string DataType { get; set; }
}











/*

using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class DatabaseToCSharpModelGenerator
{
    private static readonly string connectionString = $"Data Source={GetDatabasePath()};Version=3;";

    public static void GenerateModel()
    {
        var tables = new List<DatabaseTable>
        {
            new DatabaseTable
            {
                TableName = "Author",
                Columns = new List<DatabaseColumn>
                {
                    new DatabaseColumn { ColumnName = "ID", DataType = "int" },
                    new DatabaseColumn { ColumnName = "Name", DataType = "string" },
                    new DatabaseColumn { ColumnName = "Description", DataType = "string" },
                    new DatabaseColumn { ColumnName = "Age", DataType = "int" }
                }
            },
            new DatabaseTable
            {
                TableName = "Director",
                Columns = new List<DatabaseColumn>
                {
                    new DatabaseColumn { ColumnName = "ID", DataType = "int" },
                    new DatabaseColumn { ColumnName = "Name", DataType = "string" },
                    new DatabaseColumn { ColumnName = "Description", DataType = "string" },
                    new DatabaseColumn { ColumnName = "Age", DataType = "int" }
                }
            },
            // Add other tables and columns similarly
        };

        foreach (var table in tables)
        {
            Console.WriteLine($"public class {table.TableName}");
            Console.WriteLine("{");

            foreach (var column in table.Columns)
            {
                Console.WriteLine($"    public {column.DataType} {column.ColumnName} {{ get; set; }}");
            }

            Console.WriteLine("}");
            Console.WriteLine();
        }
    }

    private static string GetDatabasePath()
    {
        string folderName = "DataSource";
        string folderPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\" + folderName));
        System.IO.Directory.CreateDirectory(folderPath);
        return System.IO.Path.Combine(folderPath, "database.db");
    }
}

public class DatabaseTable
{
    public string TableName { get; set; }
    public List<DatabaseColumn> Columns { get; set; }
}

public class DatabaseColumn
{
    public string ColumnName { get; set; }
    public string DataType { get; set; }
}
*/