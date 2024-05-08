using System.Globalization;
using Models;
using DataAccessLibrary;
using DataAccessLibrary.models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Project_B.services;

namespace Project_B
{
    class Program
    {
        public static void Main()
        {
            StartUpMenu.Menu();
            HomeMenu.UseMenu(() => Universal.printAsTitle("Main Menu"));
        }
    }
}
