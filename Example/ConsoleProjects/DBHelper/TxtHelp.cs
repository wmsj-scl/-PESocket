using PENet;
using Protocol;
using System;
using System.IO;

namespace DBHelper
{
    public static class TxtHelp
    {
        public const string fileSuffix = ".txt";

        private static string GetDirectoryPatch(FileType fileType)
        {
            var path = Directory.GetCurrentDirectory() + "\\Data\\" + fileType.ToString();
            if (!Directory.Exists(path))
            {
                var basePath = Directory.GetCurrentDirectory() + "\\Data";
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                Directory.CreateDirectory(path);
            }

            return Directory.GetCurrentDirectory() + "\\Data\\" + fileType.ToString();
        }

        public static string GetPath(FileType fileType, string name)
        {
            var path = GetDirectoryPatch(fileType);
            return path + "\\" + name + fileSuffix;
        }

        /// <summary>
        /// 覆盖写入
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="name"></param>
        /// <param name="txt"></param>
        public static void Write(FileType fileType, string name, byte[] bytes)
        {           
            FileStream fs = new FileStream(GetPath(fileType, name), FileMode.OpenOrCreate);
         
            //开始写入
            fs.Write(bytes, 0, bytes.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="name"></param>
        /// <param name="error"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] Read(FileType fileType, string name , out ErrorCode error, int size = 1024*1024)
        {
            error = ErrorCode.Succeed;
            var filePath = GetPath(fileType, name);
            byte[] res = new Byte[size];
            if (File.Exists(filePath))
            {
                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    fs.Read(res, 0, res.Length);
                    fs.Close();
                    fs.Dispose();
                }
                catch (IOException e)
                {
                    PETool.LogMsg(string.Format("读取文件失败：file:{0} err:{1}", filePath, e.Message));
                    error = ErrorCode.FailedReadFile;
                }
            }
            else
            {
                error = ErrorCode.FailedFileNotExists;
            }

            return res;
        }

        public static string[] GetFileList(FileType fileType)
        {
            return Directory.GetFiles(GetDirectoryPatch(FileType.AccountSingle), ".", SearchOption.AllDirectories);
        }

        public static byte[] ReadByPath(string filePath, out ErrorCode error, int size = 1024 * 1024)
        {
            error = ErrorCode.Succeed;
            byte[] res = new Byte[size];
            if (File.Exists(filePath))
            {
                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    fs.Read(res, 0, res.Length);
                    fs.Close();
                    fs.Dispose();
                }
                catch (IOException e)
                {
                    PETool.LogMsg(string.Format("读取文件失败：file:{0} err:{1}", filePath, e.Message));
                    error = ErrorCode.FailedReadFile;
                }
            }
            else
            {
                error = ErrorCode.FailedFileNotExists;
            }

            return res;
        }
    }
}
