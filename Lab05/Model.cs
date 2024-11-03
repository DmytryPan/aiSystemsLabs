using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    public class Model
    {
        // набор фактов в продукционке
        public Dictionary<string, string> Facts = new Dictionary<string, string>(); 

        // Набор правил
        public List<Rule> Rules = new List<Rule>();

        public Model(string FileWithFacts, string FileWithRules)
        {
            if (File.Exists(FileWithFacts))
            {
                Facts = File.ReadAllLines(FileWithFacts)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => ParseFact(line)).ToDictionary(f => f.ID, f => f.FactName);
                Console.WriteLine("В продукционку успешно загружены файлы");
               
                foreach(var fact in Facts)
                    Console.WriteLine(fact.Key + " " + fact.Value);
                
            }
            else
            {
                throw new Exception($"Не получилось открыть файл {FileWithFacts}");
            }
        }


        public static Fact ParseFact(string line)
        {
            var lineParts = line.Trim().Split(' ');
            if(lineParts is not null)
            {
                Fact fact = new Fact(lineParts[0], lineParts[1]);
                return fact;
            }
            else
            {
                throw new Exception($"Не получилось спарсить строку: {line}");
            }
        }
    }
}
