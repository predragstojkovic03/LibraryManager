namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Server server = new();
            server.Listen(3000);
        }
    }
}
