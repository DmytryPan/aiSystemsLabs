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
        public Dictionary<string, Fact> Facts = new Dictionary<string, Fact>(); 

        // Набор правил
        public List<Rule> Rules = new List<Rule>();

        public Model(string FileWithFacts, string FileWithRules)
        {
            if (File.Exists(FileWithFacts))
            {
                Facts = File.ReadAllLines(FileWithFacts)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => ParseFact(line))
                    .ToDictionary(f => f.ID, f=>f);
                Console.WriteLine("В продукционку успешно загружены факты");
               
                //foreach(var fact in Facts)
                //    Console.WriteLine(fact.Key + " " + fact.Value.FactName);
            }
            else
            {
                throw new Exception($"Не получилось открыть файл {FileWithFacts}");
            }

            if (File.Exists(FileWithRules))
            {
                Rules = File.ReadAllLines(FileWithRules)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => ParseRule(line, Facts))
                    .ToList();
                Console.WriteLine("В продукционку успешно загружены правила");
            }
            else
            {
                throw new Exception($"Не получилось открыть файл {FileWithRules}");
            }

            foreach (var rule in Rules)
            {
                var sb = new StringBuilder();
                sb.Append("Если ");
                foreach (var Sending in rule.Conditions)
                {
                    sb.Append($"{Sending.FactName} И ");
                }
                sb[^2] = ' ';
                sb.Append("То ");
                sb.Append(rule.Conclusion.FactName);
                rule.Description = sb.ToString();
            }
            //foreach (var rule in Rules)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine(rule.Description);
            //}
                
        }

        private static Fact ParseFact(string line)
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

        private static Rule ParseRule(string line, Dictionary<string, Fact> factsDict)
        {
            var rule = new Rule();

            if (line.Contains("=>"))
            {
                var LineParts = line.Trim().Split("=>");
                var CondsIDs = LineParts[0].Split('&', StringSplitOptions.TrimEntries);
                var ConclsIDs = LineParts[1].Trim();

                rule.Conditions = CondsIDs.Select(id => factsDict.ContainsKey(id) ? factsDict[id] : null).Where(f => f != null).ToList();

                if (factsDict.ContainsKey(ConclsIDs))
                    rule.Conclusion = factsDict[ConclsIDs];
                else
                    throw new Exception($"В базе Нет факта с id{ConclsIDs}");
            }
            //если спарсили аксиому
            else
            {
                rule.Conditions = new List<Fact>();
                rule.Conclusion = factsDict[line.Trim()];
            }
            return rule;
        }


    }
    public class Resolver
    {
        //Выведенные факты
        public List<List<Fact>> DeducedFacts;
        //Примененные правила
        public List<Rule> ApplyedRules;
        public bool isSuccessful;
        public Resolver() {
            DeducedFacts = new List<List<Fact>>();
            ApplyedRules = new List<Rule>();
            isSuccessful = false;
        }
    }

}

