using LT_PhotoAlbum.Abstractions;
using System;

namespace LT_PhotoAlbum.Services
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
