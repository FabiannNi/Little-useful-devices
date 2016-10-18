using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project1
{
    class FileEditUtility
    {
        
        /// <summary>
        /// 搜索并编辑搜索内容
        /// </summary>
        /// <param name="address"></param>
        /// <param name="toBeReplacedContent"></param>
        /// <param name="contentUsedToReplace"></param>
        public static void EditContent(string address,string toBeReplacedContent, string contentUsedToReplace) {
            //bool ExistContent = FileStream.ReadLines(address).Contains(toBeReplacedContent);
            FileStream fs = new FileStream(address, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadToEnd();
            bool ExistContent = content.Contains(toBeReplacedContent);
            //StreamReader要占用资源，这里要关闭通道，避免Writer无法写入
            sr.Close();
            if (ExistContent)
            {
                string newContent = content.Replace(toBeReplacedContent, contentUsedToReplace);
                StreamWriter sw = new StreamWriter(address);
                sw.Write(newContent);
                sw.Flush();
                sw.Close();
            }
            else {
                Console.WriteLine("未搜索到关键词");
            }
        }

        /// <summary>
        /// 展示出当前输入目录下所有文件的名称
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static List<string> ShowAllFiles(string address){
            //获取所有文件
            List<string> filenames = new List<string>();
            int enumerator = 0;
            string[] files = Directory.GetFiles(address);
            foreach(string fs in files){
                filenames.Add(fs);
                enumerator += 1;
            }
            return filenames;
        }

        /// <summary>
        /// 删除重复的行
        /// </summary>
        /// <param name="address"></param>
        public static void deleteManySameLine(string address) {
            Queue<string> q1 = new Queue<string>();
            using (StreamReader sr = new StreamReader(address))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine();
                    if (!q1.Contains(currentLine))
                    {
                        q1.Enqueue(currentLine);
                    }
                }
            }
            using(StreamWriter sw = new StreamWriter(address))
            {
                foreach(string s in q1)
                {
                    sw.WriteLine(s);
                }
            }
            Console.Write("批量删除成功");
        }


        public static string GetCompletePath(string filename, string address)
        {
            string completePath = address + "/" + filename + "/";
            return completePath;
        }
            
    }
}
