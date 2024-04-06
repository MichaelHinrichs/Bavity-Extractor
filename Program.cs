using System;
using System.IO;

namespace Bavity_Extractor
{
    class Program
    {
        public static BinaryReader br;
        static void Main(string[] args)
        {
            br = new BinaryReader(File.OpenRead(args[0]));
            int tableEnd = br.ReadInt32();
            System.Collections.Generic.List<FileData> files = new();
            while (br.BaseStream.Position < tableEnd)
                files.Add(new());

            foreach (FileData file in files)
            {
                br.BaseStream.Position = file.start;
                Directory.CreateDirectory(Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]) + "//" + Path.GetDirectoryName(file.name));
                BinaryWriter bw = new(File.Create(Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]) + "//" + file.name));
                bw.Write(br.ReadBytes(file.size));
                bw.Close();
            }
        }

        class FileData
        {
            public string name = NullTerminatedString();
            public int start = br.ReadInt32();
            public int size = br.ReadInt32();
        }

        public static string NullTerminatedString()
        {
            char[] fileName = Array.Empty<char>();
            char readchar = (char)1;
            while (readchar > 0)
            {
                readchar = br.ReadChar();
                Array.Resize(ref fileName, fileName.Length + 1);
                fileName[^1] = readchar;
            }
            Array.Resize(ref fileName, fileName.Length - 1);
            string name = new(fileName);
            return name;
        }
    }
}
