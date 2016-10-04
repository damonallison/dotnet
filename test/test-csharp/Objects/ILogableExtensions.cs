using System;
using System.Linq;
using System.Collections.Generic;

namespace DamonAllison.CSharpTests.Objects
{
    /// <summary>
    /// Extension methods are allowed on interfaces. This is rather nice as it 
    /// adds functionality to *all* types which derive from the interface.
    /// </summary>
    public static class ILogableExtensions
    {
        public static void LogToDB(this ILogable loggable) 
        {
            Console.WriteLine($"[To DB] {loggable}");
        }
        
        public static void LogAll(this IList<ILogable> loggables) 
        {
            loggables.ToList().ForEach(l => l.Log());
        }
    }
}