namespace Lab05
{
    public class Rule
    {
        //Факты-условия
        internal List<Fact> Conditions { get; set; }
        //Факты-заключения
        internal Fact Conclusion { get; set; }
        //Словесное описание правила
        public string Description { get; set; }
        //Описание правила в ID
        public string DescriptionID { get; set; }

    }
}
