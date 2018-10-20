using Daisy.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Daisy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            var storage = new DefaultStorageImpl();
            Console.WriteLine("Started");
            ChainEngine<ExampleStorableImpl, int, ExampleRuleCollection> engine =
                new ChainEngine<ExampleStorableImpl, int, ExampleRuleCollection>(
                    new ExampleRulesetImpl(),
                    new ExampleImporterImpl(10),
                    storage);
            sw.Start();
            engine.Start();
            sw.Stop();
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms");


            Console.WriteLine(storage.FetchLatest(0).ThisJson);
            Console.WriteLine(storage.FetchLatest(1).ThisJson);

            while (true)
            {
                string command = Console.ReadLine();
                if(command.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                } 
                if(command.Equals("h", StringComparison.InvariantCultureIgnoreCase))
                {
                    List<int> ident = storage.GetIdentities().ToList();
                    Console.WriteLine("Select a identity to explore");
                    ident.ForEach(Console.WriteLine);
                    string identity = Console.ReadLine();
                    if (ident.Contains(int.Parse(identity)))
                    {
                        while (true)
                        {
                            List<string> states = storage.GetPatchKeys(int.Parse(identity)).ToList();
                            Console.WriteLine("Select a state to retieve");
                            states.ForEach(Console.WriteLine);
                            string state = Console.ReadLine();
                            if (states.Contains(state))
                            {
                                Console.WriteLine(storage.GetState(int.Parse(identity), state));
                            }
                            if (state.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
