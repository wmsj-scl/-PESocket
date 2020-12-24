using System;
using System.IO;

namespace DBHelper
{
    public static class TxtHelp
    {
        public static string GetPath(string name)
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Data"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Data");
            }
            Console.WriteLine( Directory.GetCurrentDirectory() + "\\Data\\" + name + ".txt");
            return Directory.GetCurrentDirectory() + "\\Data\\" + name + ".txt";
        }

        public static void Write(string name,string txt)
        {
            
            FileStream fs = new FileStream(GetPath(name), FileMode.OpenOrCreate);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(txt);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        public static void Read(string name)
        {
           
        }
    }
}
