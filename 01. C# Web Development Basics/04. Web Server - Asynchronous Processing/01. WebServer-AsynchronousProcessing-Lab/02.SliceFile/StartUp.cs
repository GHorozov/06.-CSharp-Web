namespace _02.SliceFile
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var videoPath = "../../../videos/Unity 2018.3_ New Features and Improvements.mp3";
            var destination = "../../../result";
            int pieces = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destination, pieces);

            Console.WriteLine("Do something else");
            while (true)
            {
                var input = Console.ReadLine();
                if(input == "exit")
                {
                    break;
                }
            }
        }

        private static void SliceAsync(string videoPath, string destination, int pieces)
        {
            Task.Run(() =>
            {
                Slice(videoPath, destination, pieces);
            });
        }

        private static void Slice(string videoPath, string destination, int pieces)
        {
            if (Directory.Exists(destination))
            {
                Directory.Delete(destination);
            }

            Directory.CreateDirectory(destination);

            using(var source = new FileStream(videoPath, FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(videoPath);

                long partLenght = (source.Length / pieces) + 1;
                long currentByte = 0;

                for (int currentPart = 1; currentPart < pieces; currentPart++)
                {
                    var filePath = $"{destination}/{currentPart}.{fileInfo.Extension}";

                    using (var destinationPath = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[2];
                        while (currentByte < (partLenght * currentPart))
                        {
                            int readBytesCount = source.Read(buffer, 0, buffer.Length);
                            if(readBytesCount == 0)
                            {
                                break;
                            }

                            destinationPath.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }

                    Console.WriteLine("Slice complete.");
                }
            }
        }
    }
}
