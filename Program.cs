using System;
using System.IO;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папка для очистки");
            string dir = Console.ReadLine();
            if(!string.IsNullOrEmpty(dir))
            {
                ClearFolder(dir); // Вызов метода очистки папки
            }
        }

        /// Метод рекурсивной очистки папки
        static void ClearFolder(string dir)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                if((dirInfo.Exists) || (dir.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                {
                    DirectoryInfo[] dirsArray = dirInfo.GetDirectories(); // Получаем массив вложенных папок

                    foreach(var d in dirsArray) // Чистим рекурсивно от папок, к которым не обращались более 30 минут
                    {
                        if(DateTime.Now - d.LastAccessTime > TimeSpan.FromMinutes(30))
                        {
                            d.Delete(true);
                        }
                    }

                    FileInfo[] filesArray = dirInfo.GetFiles(); // Получаем массив файлов

                    foreach(var f in filesArray)
                    {
                        if(DateTime.Now - f.LastAccessTime > TimeSpan.FromMinutes(30))
                        {
                            f.Delete();
                        }
                    }
                }

                Console.WriteLine("Папка очищена");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
