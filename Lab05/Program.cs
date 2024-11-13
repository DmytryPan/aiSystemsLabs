namespace Lab05
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());

            Model model = new Model("description.txt", "rules.txt");
            Console.WriteLine("----------------------------");
            Console.Write("¬ведите ID фактов, которые нужно прин€ть за аксиомы: ");
            var inputFactsIDs = Console.ReadLine().Trim().Split(' ').ToList();
            //foreach (var s in inputFactsIDsString)
            //Console.Write(s + " ");
            Console.Write("¬ведите ID факта, который необходимо вывести: ");
            var targetFactID = Console.ReadLine();
            Console.WriteLine("ѕр€мой вывод: ");
            var forwardResolver = model.Forward(inputFactsIDs, targetFactID);
            //foreach (var snapshot in forwardResolver.DeducedFacts)
            //{
            //    foreach (var fact in snapshot)
            //    {
            //        Console.Write(fact.ID + " ");
            //    }
            //    Console.WriteLine();
            //}
            if (forwardResolver.isSuccessful)
            {
                foreach (var appRule in forwardResolver.ApplyedRules)
                {
                    Console.WriteLine($"{appRule.Description} ({appRule.DescriptionID})");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("не получилось вывести факт из текущего набора аксиом");
            }

            Console.WriteLine("ќбратный вывод: ");

            var BackwardResolver = model.Backward(inputFactsIDs, targetFactID);
            if (BackwardResolver.isSuccessful)
            {
                foreach (var appRule in BackwardResolver.ApplyedRules)
                {

                    Console.WriteLine($"{appRule.Description} ({appRule.DescriptionID})");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("не получилось вывести факт из текущего набора аксиом");
            }

        }
    }
}