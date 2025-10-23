namespace Server
{
    internal class Program
    {
        public static Server ServerInstance;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ServerInstance = new Server();
            ServerInstance.Listen(3000);
        }
    }
}
