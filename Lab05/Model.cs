using System.Data;
using System.Text;
namespace Lab05
{
    public class Model
    {
        // набор фактов в продукционке
        public Dictionary<string, Fact> Facts = new Dictionary<string, Fact>();

        // Набор правил
        public List<Rule> Rules = new List<Rule>();
        // Хранит ID аксиом

        public Model(string FileWithFacts, string FileWithRules)
        {
            if (File.Exists(FileWithFacts))
            {
                Facts = File.ReadAllLines(FileWithFacts)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => ParseFact(line))
                    .ToDictionary(f => f.ID, f => f);
                Console.WriteLine("В продукционку успешно загружены факты");

                foreach (var fact in Facts)
                    Console.WriteLine(fact.Key + " " + fact.Value.FactName);
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
                var sb2 = new StringBuilder();

                sb.Append("Если ");

                foreach (var Sending in rule.Conditions)
                {
                    sb.Append($"{Sending.FactName} И ");
                    sb2.Append($"{Sending.ID} ");
                }
                sb[^2] = ' ';
                sb.Append("То ");
                sb.Append(rule.Conclusion.FactName);
                sb2.Append("=> ");
                sb2.Append(rule.Conclusion.ID);
                rule.Description = sb.ToString();
                rule.DescriptionID = sb2.ToString();
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
            if (lineParts is not null)
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
                var lineParts = line.Trim().Split("=>");
                var conditionsIds = lineParts[0].Split('&', StringSplitOptions.TrimEntries);
                var conclusionId = lineParts[1].Trim();

                rule.Conditions = conditionsIds.Select(id => factsDict.ContainsKey(id) ? factsDict[id] : null)
                                               .Where(f => f != null).ToList();

                if (factsDict.ContainsKey(conclusionId))
                    rule.Conclusion = factsDict[conclusionId];
                else
                    throw new Exception($"Факт с ID {conclusionId} не найден в базе.");
            }
            else // Это аксиома
            {
                rule.Conditions = new List<Fact>();
                rule.Conclusion = factsDict[line.Trim()];
            }

            return rule;
        }

        //Прямой вывод. принимает список Идентификаторов фактов-посылок и идентификатор целевого факта
        public Resolver Forward(List<string> InputFactsIDs, string TargetFactID)
        {
            HashSet<Fact> InputFacts = new HashSet<Fact>(InputFactsIDs.Select(id => Facts[id]));
            var TargetFact = Facts[TargetFactID];

            Resolver resolver = new Resolver();
            resolver.DeducedFacts.Add(new List<Fact>(InputFacts.ToList()));
            //Если целевой факт есть в посылках
            if (InputFacts.Contains(TargetFact))
            {
                resolver.isSuccessful = true;
                return resolver;
            }

            bool newFactDeduced;
            do
            {
                newFactDeduced = false;
                foreach (var rule in Rules)
                {
                    //Если можем применить правило и множество исходных фактов не содержит заключения, то
                    if (rule.Conditions.Count > 0 && rule.Conditions.All(cond => InputFacts.Contains(cond)) && !InputFacts.Contains(rule.Conclusion))
                    {
                        newFactDeduced = true;
                        InputFacts.Add(rule.Conclusion);
                        resolver.ApplyedRules.Add(rule);
                        resolver.DeducedFacts.Add(new List<Fact>(InputFacts.ToList()));

                        if (rule.Conclusion.ID == TargetFactID)
                        {
                            resolver.isSuccessful = true;
                            return resolver;
                        }
                    }
                }
            } while (newFactDeduced);

            return resolver;
        }
        //геттер для первого правила по заключению
        public Rule FirstRuleByConclusion(Fact fact, HashSet<Rule> visited)
        {
            foreach (var rule in Rules)
            {
                if (visited.Contains(rule)) continue;
                if (rule.Conclusion.ID == fact.ID) return rule;
            }
            return null;
        }

        public Resolver BackwardC(List<string> initialFactIds, string targetFactId)
        {
            Resolver resolver = new Resolver();
            HashSet<Fact> axioms = initialFactIds.Select(id => Facts[id]).ToHashSet();
            HashSet<Rule> visited = new HashSet<Rule>();
            Fact target = Facts[targetFactId];

            Stack<Fact> open = new Stack<Fact>();
            open.Push(target);

            while (open.Count > 0)
            {
                Fact current = open.Peek();
                if (axioms.Contains(current))
                {
                    open.Pop();

                    if (current == target)
                    {
                        resolver.isSuccessful= true;
                        break;
                    }

                    continue;
                }
                Rule rule = FirstRuleByConclusion(current, visited);
                if (rule != null)
                {
                    bool proven = true;
                    foreach (var fact in rule.Conditions)
                    {
                        if (!axioms.Contains(fact))
                        {
                            proven = false;
                            var fact_rule = FirstRuleByConclusion(fact, visited);
                            if (fact_rule != null)
                            {
                                open.Push(fact);
                            }
                            else
                            {
                                visited.Add(rule);
                            }
                            break;
                        }
                    }
                    if (proven)
                    {
                        resolver.DeducedFacts.Add(new List<Fact>(axioms));
                        resolver.ApplyedRules.Add(rule);


                        axioms.Add(current);
                        visited.Add(rule);
                        open.Pop();

                        if (current == target)
                        {
                            resolver.isSuccessful = true;
                            break;
                        }
                    }
                }
                else
                {
                    open.Pop();
                }
            }

            resolver.ApplyedRules.Reverse();
            resolver.DeducedFacts.Reverse();
            return resolver;
        }





    }
    public class Resolver
    {
        //Выведенные факты
        public List<List<Fact>> DeducedFacts;
        //Примененные правила
        public List<Rule> ApplyedRules;
        public bool isSuccessful;
        public Resolver()
        {
            DeducedFacts = new List<List<Fact>>();
            ApplyedRules = new List<Rule>();
            isSuccessful = false;
        }
    }

}

