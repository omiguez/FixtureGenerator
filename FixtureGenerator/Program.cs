using FixtureGenerator.CommonTreeGeneration;
using System;

namespace FixtureGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //new FixtureGenerator().DoStuff();
            new CommonTreeBuilder().TryBuildTree();
            Console.WriteLine("We done!");
        }
    }
}
