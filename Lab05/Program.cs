namespace Lab05
{
    // ����� ������
    public class Fact
    {
        public string id;
        public string factName { get; set; } // ��� �����
        public bool isDeduced { get; set; } = false; // �������� �� �� ����������

        public Fact(string id, string factName)
        {
            this.id = id;
            this.factName = factName;
        }

    }
    // ����� ������
    public class Rule
    {

        public List<Fact> sendings { get; set; }
        public List<Fact> conclusions { get; set; }

    }

    public class model
    {
        List<Fact> facts;
        List<Rule> rules;
        //����������� ��� ������ �� �����
        model(string FactsFileName, string RulesFileName)
        {

        }
    }

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
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        }
    }
}