using DataAccessLibrary;

namespace Project_B;
static class Universal
{
    /// <summary>
    /// Database connection
    /// </summary>
    //public static SQliteDataAccess? Db { get; set; }

    public static string printAsTitle(string input)
    {
        input = input.Trim();
        string toAdd = "";
        for (int i = 0; i < Console.WindowWidth / 2 / 4 - (input.Length + 6) / 2 - 1; i++) toAdd += "-";
        WriteColor(toAdd + "== ", ConsoleColor.Blue);
        Console.Write(input);
        WriteColor(" ==" + toAdd, ConsoleColor.Blue);
        return ""; // (toAdd + input + toAdd).Substring(0, 119);
    }
    public static string centerToScreen(string input)
    {
        string toAdd = "";
        for (int i = 0; i < Console.WindowWidth / 2 / 4 - (input.Length + 6) / 2 - 1 + 4; i++) toAdd += " ";
        return toAdd + input + toAdd;
    }
    public static string ChangeColour(ConsoleColor colour)
    {
        Console.ForegroundColor = colour;
        return "";
    }
    public static string WriteColor(string toPrint, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(toPrint);
        Console.ForegroundColor = ConsoleColor.White;
        return "";
    }
    private static void setupFolder(string folderName) => System.IO.Directory.CreateDirectory(System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\" + folderName)));
    public static string datafolderPath
    {
        get
        {
            setupFolder("DataSource");
            return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
        }
    }
}