using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace NanoVirus_BrennanBuitendag
{
    public static class FileWriter
    {
        //Path to the MyDocuments folder of the current user
        private static string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Nano Virus";
        private static string fileName = string.Format("{0}\\NanoVirusSimulation {1}.txt", directory, DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss"));

        public static void CreateDirectory()
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        static FileWriter()
        {
            CreateDirectory();
        }

        public static void WriteToFile(int cycleNumber)
        {
            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    CellArea cellArea = CellArea.GetInstance;

                    StreamWriter writer = new StreamWriter(file);

                    writer.WriteLine(cellArea.ToString());

                    writer.Close();
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(String.Format("\nAn Error Occurred:\n{0}\n", e.Message));
            }
            catch (IOException e)
            {
                Console.WriteLine(String.Format("\nAn Error Occurred:\n{0}\n", e.Message));
            }
        }
    }
}
