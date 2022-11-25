namespace AccountHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(PasswordUtils.CreatePasswordHash("12345"));
        }
    }
}