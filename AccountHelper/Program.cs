namespace AccountHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(PasswordUtils.CreatePasswordHash("12345"));
            //(A2FTfGFznk3ONfbPlJnK2A==, yJGqCb3SNntHBd2WRzu6sTXvLufqGPsgaHWZodMUxK8izgBQC5t79/HPyYvKfObTFSI6vPeiWZu7q+WyIWqq/A==)
        }
    }
}