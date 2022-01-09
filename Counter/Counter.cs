using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassCounter.Counter
{
    public class Counter
    {
        public static Dictionary<string, int> counter = new Dictionary<string, int>();
        public static int timer = 0;
        public static Debouncer debouncer = new Debouncer(2000);

        public Counter()
        {
            var className = this.GetType().Name;
            if (counter.ContainsKey(className))
            {
                counter[className] = ++counter[className];
                return;
            }

            counter.Add(className, 1);

            debouncer.Debounce(InformClassCreation);
        }

        private void InformClassCreation()
        {
            lock (counter)
            {
                Console.WriteLine("...");
                while (counter.Keys.Any())
                {
                    var key = counter.Keys.First();
                    var value = counter[key];
                    Console.WriteLine($"Counter: class '{key}' were created '{value}' times");
                    counter.Remove(key);
                }
                Console.WriteLine("...");
            }
        }

    }
}
