using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Portal
{
    // Класс обработки файлов - чтение данных из файлов и запись в них
    public class FileHelper
    {
        public static List<T> createListFromFile<T>(string FileName)
        {
            if (File.Exists(FileName))
            {
                Stream stm = File.Open(FileName, FileMode.Open);
                try {
                    return (List<T>)(new BinaryFormatter()).Deserialize(stm);
                }
                finally {
                    stm.Close();
                }
            }
            else 
                return new List<T>() ;
        }
        public static void saveListToFile<T>(List<T> list,string FileName)
        {
            Stream stm = File.Create(FileName);
            (new BinaryFormatter()).Serialize(stm, list);
            stm.Close();
        }
        public static void saveObjToFile<T>(T obj, string FileName)
        {
            Stream stm = File.Create(FileName);
            (new BinaryFormatter()).Serialize(stm, obj);
            stm.Close();
        }
        public static T getObjFromFile<T>(string FileName)
        {
            Stream stm = File.Open(FileName, FileMode.Open);
            try
            {
                return (T)(new BinaryFormatter()).Deserialize(stm);
            }
            finally
            {
                stm.Close();
            }
        }
    }
}
