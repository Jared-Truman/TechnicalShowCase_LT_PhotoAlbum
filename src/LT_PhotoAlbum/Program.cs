using CommandLine;
using LT_PhotoAlbum.Abstractions;
using LT_PhotoAlbum.Arguments;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace LT_PhotoAlbum
{
    class Program
    {
        private static IConsoleWrapper _console;

        static void Main(string[] args)
        {
            var services = StartUp.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            _console = serviceProvider.GetService<IConsoleWrapper>();

            _console.WriteLine("Welcome to the LT Photo Albums. (If you need help on available commands please say --help)");
            string[] cmdArgs = GetUserInput();

            while (IsNotExitArg(cmdArgs))
            {
                Parser.Default.ParseArguments<AlbumArgument, PhotoArgument>(cmdArgs)
                    .WithParsed<AlbumArgument>(arg => serviceProvider.GetService<IConsoleWriter<AlbumArgument>>().WriteLineAsync(arg).Wait())
                    .WithParsed<PhotoArgument>(arg => serviceProvider.GetService<IConsoleWriter<PhotoArgument>>().WriteLineAsync(arg).Wait());
                cmdArgs = GetUserInput();
            }

            _console.WriteLine("Bye!!");
        }

        private static bool IsNotExitArg(string[] cmdArgs)
        {
            return cmdArgs.Any() && IsNotEmptyExitArg(cmdArgs) && IsNotTheWordExitExitArg(cmdArgs);
        }

        private static bool IsNotTheWordExitExitArg(string[] cmdArgs)
        {
            return cmdArgs.First().ToLower() != "exit";
        }

        private static bool IsNotEmptyExitArg(string[] cmdArgs)
        {
            return !string.IsNullOrEmpty(cmdArgs.First());
        }

        private static string[] GetUserInput()
        {
            _console.WriteLine("Enter Command:");
            var cmdArgs = _console.ReadLine().Split(' ');
            return cmdArgs;
        }
    }
}
