using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
            => new Client().Init(args).GetAwaiter().GetResult();
    }
}
