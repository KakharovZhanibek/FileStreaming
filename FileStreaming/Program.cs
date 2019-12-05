using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileStreaming
{
    class FileChunk
    {
        public string FileId;
        public byte[] Content;
        public int ChunkNo;
    }
    class Program
    {
        static void Demo()
        {
            string pathToDirectory = "c:\\files";
            var filesOld = Directory.GetFiles(pathToDirectory);

            var task = Task.Run(() =>
           {
               while (true)
               {
                   var filesNew = Directory.GetFiles(pathToDirectory);
                   if(filesOld.Length != filesNew.Length)
                   {
                       Console.WriteLine("Smth happened!");
                   }

                   Thread.Sleep(TimeSpan.FromSeconds(5));
               }
           });
        }
        static void Main(string[] args)
        {
            Demo();

            Console.ReadLine();
            string path = "c:\\files\\data.bin";
            int partitionsCount = 0;

            using (var fs = new FileStream(path, FileMode.Open))
            {
                var bytes = new byte[1024 * 1024];
                int readCount = fs.Read(bytes, 0, bytes.Length);
                partitionsCount++;

                Console.WriteLine($"Read {(readCount / 1024) / 1024} megabytes, " +
                    $"parition # {partitionsCount}");

                while(readCount == bytes.Length)
                {
                    readCount = fs.Read(bytes, 0, bytes.Length);
                    partitionsCount++;

                    Console.WriteLine($"Read {(readCount / 1024) / 1024} megabytes, " +
                    $"parition # {partitionsCount}");
                }
            }
        }
    }
}
