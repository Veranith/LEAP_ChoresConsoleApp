using System;
using ChoresLibrary;

namespace ChoresApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var choreApp = new Chores();
            choreApp.run();
        }
    }
}
