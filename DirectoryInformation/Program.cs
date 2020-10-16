using System;
using System.IO;
using System.Security.Principal;

namespace DirectoryInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь к папке: ");
            var path = Console.ReadLine();

            if(!Directory.Exists(path))
            {
                Console.WriteLine("Каталога не существует.");
                return;
            }

            var directory = new DirectoryInfo(path);

            foreach (var dir in directory.GetDirectories())
            {
                Console.WriteLine($"{dir.Name} (directory)");
            }

            foreach (var file in directory.GetFiles())
            {
                Console.Write($"{Path.GetFileNameWithoutExtension(file.FullName)} Extension: {file.Extension} Size: {file.Length} b");

                var fc = File.GetAccessControl(file.FullName);
                var sid = fc.GetOwner(typeof(SecurityIdentifier));
                var account = sid.Translate(typeof(NTAccount));

                Console.WriteLine($" Owner: {account}");
            }
        }
    }
}