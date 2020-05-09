using System;

namespace Felli
{
    class Program
    {
        static void Main(string[] args)
        {
            /* int number = 5;

            for (int i = number; i >= 2; i -= 2)
            {
                string spaces = new String(' ', (number - i) / 2);
                Console.WriteLine(spaces + new String('*', i) + spaces);
            }

            for (int i = 1; i <= number; i += 2)
            {
                string spaces = new String(' ', (number - i) / 2);
                Console.WriteLine(spaces + new String('*', i) + spaces);
            } */
            Render r = new Render();
            GameManager gm = new GameManager(r);
            r.Draw(gm.grid);
            
        }
    }
}
