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
            Console.Write("Введите ID фактов, которые нужно принять за аксиомы: ");
            var inputFactsIDs = Console.ReadLine().Trim().Split(' ').ToList();
            //foreach (var s in inputFactsIDsString)
            //Console.Write(s + " ");
            Console.Write("Введите ID факта, который необходимо вывести: ");
            var targetFactID = Console.ReadLine();
            Console.WriteLine("Прямой вывод: ");
            var forwardResolver = model.Forward(inputFactsIDs, targetFactID);
            //foreach (var snapshot in forwardResolver.DeducedFacts)
            //{
            //    foreach (var fact in snapshot)
            //    {
            //        Console.Write(fact.ID + " ");
            //    }
            //    Console.WriteLine();
            //}
            foreach (var appRule in forwardResolver.ApplyedRules)
            {
                //appRule.Conditions.ForEach(cond => Console.Write(cond.FactName+";"));
                Console.WriteLine(appRule.DescriptionID);
            }

            Console.WriteLine("Обратный вывод: ");
            var BackwardResolver = model.BackWard(inputFactsIDs, targetFactID);

        }
    }
}