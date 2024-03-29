using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;

public class AsyncAwait1
    {
        public static void Main1(string[] args)
        {
            string content = GetContentAsync(@"//Users/chunyaozou/Documents/c/ParallelProgrammingCSharp/OtherExample/test.txt").Result;
            //调用同步方法
            //string content = GetContent(Environment.CurrentDirectory + @"/test.txt");
            Console.WriteLine(content);
            //Console.ReadKey();
        }
        //异步读取文件内容
        async static Task<string> GetContentAsync(string filename)
        {
            
            FileStream fs = new FileStream(filename, FileMode.Open);
            var bytes = new byte[fs.Length];
            //ReadAync方法异步读取内容，不阻塞线程
            Console.WriteLine("开始读取文件");
            int len = await fs.ReadAsync(bytes, 0, bytes.Length);
            string result = Encoding.UTF8.GetString(bytes);
            return result;
        }
        //同步读取文件内容
        static string GetContent(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            var bytes = new byte[fs.Length];
            //Read方法同步读取内容，阻塞线程
            int len =  fs.Read(bytes, 0, bytes.Length);
            string result = Encoding.UTF8.GetString(bytes);
            return result;
        }
    }