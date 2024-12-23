using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CLIPSNET;


namespace ClipsFormsExample
{
    public partial class ClipsFormsExample : Form
    {
        private CLIPSNET.Environment clips = new CLIPSNET.Environment();

        public Dictionary<string, string> Facts = new Dictionary<string, string>(); // входные факты
        
        public string target; // целевой факт, который надо вывести

        public Random random = new Random(); // рандомайзер
        string outputFilePath = "output.txt";

        bool CanContinue = true; 
        //Считывает факты из checkbox'ов и добавляет их в клипс
        public void ReadFacts()
        {
            Facts.Clear();
            //Читаем все факты
            foreach(var s in InputsListedCheckBox.Items)
            {
                var spl = s.ToString().Split(' ');
                var id = spl[0].ToUpper();
                var title = spl[1].ToUpper();
                Facts[id] = title;
            }
            foreach (var s in InputsListedCheckBox.CheckedItems)
            {
                var spl = s.ToString().Split(' ');
                var id = spl[0].ToUpper();
                //интерпретатор выполняет ассерт и добавляет факт в выведенные
                clips.Eval($"(assert (theorem (name {id}) (coef {Math.Round(random.NextDouble() * 0.9 + 0.1, 3).ToString().Replace(",", ".")})))");
            }
            target = GoalCheckBox.CheckedItems[0].ToString().Split(' ')[0].ToUpper() ;
            
            //foreach(var key in Facts.Keys)
            //{
            //    Console.WriteLine($"{key} -> {Facts[key]}");
            //}
        }

        // преобразовывает правило в читаемый вид
        public string ProcessID(string rule)
        {
            if (!rule.Contains('>')) return rule; // Если нет разделителя '>', возвращаем правило как есть

            // Разделяем правило на левую и правую части
            string left = rule.Split('=')[0].ToString(); 
            string right = rule.Split('>')[1].ToString();

            string res = "";

            var SplitLeft = left.Split('&');

            for (int i = 0; i < SplitLeft.Length - 1; ++i)
            {
                string factName = SplitLeft[i].Trim().ToUpper(); 
                res += Facts[factName] + " И ";
            }

            string t = SplitLeft.Last().Trim().ToUpper();
            res += Facts[t];

            res += (SplitLeft.Length > 1 ? " доказывают, что " : " доказывает, что ");

            string temp = right.Split(':')[0].ToString().Trim(); 
            res += Facts[temp] + " верна."; 

            res += " Коэффициент " + right.Split(':')[1].ToString();

            return res;
        }
        public ClipsFormsExample()
        {
            InitializeComponent();
           
        }

     

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        //private void HandleResponse()
        //{
        //    //  Вытаскиаваем факт из ЭС
        //    String evalStr = "(find-fact ((?f ioproxy)) TRUE)";
        //    FactAddressValue fv = (FactAddressValue)((MultifieldValue)clips.Eval(evalStr))[0];

        //    MultifieldValue damf = (MultifieldValue)fv["messages"];
        //    if (damf.Count == 0)
        //    {
        //        CanContinue = false;
        //        outputBox.Text += "Целевой факт не достижим" + System.Environment.NewLine + System.Environment.NewLine;
        //        return;
        //    }
        //    MultifieldValue vamf = (MultifieldValue)fv["answers"];

        //    outputBox.Text += "Новая итерация : " + System.Environment.NewLine + System.Environment.NewLine;
        //    for (int i = 0; i < damf.Count; i++)
        //    {
        //        LexemeValue da = (LexemeValue)damf[i];
        //        byte[] bytes = Encoding.Default.GetBytes(da.Value);
        //        string message = Encoding.UTF8.GetString(bytes);
        //        outputBox.Text += ProcessID(message) + System.Environment.NewLine + System.Environment.NewLine;

        //        string DeducedFact = message.Split(':')[0].Trim().Split(' ').Last().Split(new[] { "=>" }, StringSplitOptions.None)[1];
        //        if (DeducedFact == target)
        //        {
        //            CanContinue = false;
        //            outputBox.Text += "Целевой факт выводим" + System.Environment.NewLine;
        //            return;
        //        }
        //    }

        //    var phrases = new List<string>();
        //    if (vamf.Count > 0)
        //    {
        //        outputBox.Text += "----------------------------------------------------" + System.Environment.NewLine;
        //        for (int i = 0; i < vamf.Count; i++)
        //        {
        //            //  Варианты !!!!!
        //            LexemeValue va = (LexemeValue)vamf[i];
        //            byte[] bytes = Encoding.Default.GetBytes(va.Value);
        //            string message = Encoding.UTF8.GetString(bytes);
        //            phrases.Add(message);
        //            outputBox.Text += "Добавлен вариант для распознавания " + message + System.Environment.NewLine;
        //        }
        //    }

        //    if (vamf.Count == 0)
        //        clips.Eval("(assert (clearmessage))");
        //}

        private void HandleResponse()
        {

            // Открываем файл для записи (добавляем содержимое в конец файла)
            using (StreamWriter writer = new StreamWriter(outputFilePath, true, Encoding.UTF8))
            {
                
                //  Вытаскиаваем факт из ЭС
                String evalStr = "(find-fact ((?f ioproxy)) TRUE)";
                FactAddressValue fv = (FactAddressValue)((MultifieldValue)clips.Eval(evalStr))[0];

                MultifieldValue damf = (MultifieldValue)fv["messages"];
                if (damf.Count == 0)
                {
                    CanContinue = false;
                    writer.WriteLine("Целевой факт не достижим" + System.Environment.NewLine + System.Environment.NewLine);
                    return;
                }
                MultifieldValue vamf = (MultifieldValue)fv["answers"];

                writer.WriteLine("Новая итерация : " + System.Environment.NewLine + System.Environment.NewLine);
                for (int i = 0; i < damf.Count; i++)
                {
                    LexemeValue da = (LexemeValue)damf[i];
                    byte[] bytes = Encoding.Default.GetBytes(da.Value);
                    string message = Encoding.UTF8.GetString(bytes);
                    //Console.WriteLine(message);
                    if (message.Contains("=>"))
                    {
                        writer.WriteLine(ProcessID(message) + System.Environment.NewLine + System.Environment.NewLine);

                        string DeducedFact = message.Split(':')[0].Trim().Split(' ').Last().Split(new[] { "=>" }, StringSplitOptions.None)[1];
                        if (DeducedFact == target)
                        {
                            CanContinue = false;
                            writer.WriteLine("Целевой факт выводим" + System.Environment.NewLine);
                            return;
                        }
                    }
                    else
                        writer.WriteLine(message);

                }

                var phrases = new List<string>();
                if (vamf.Count > 0)
                {
                    writer.WriteLine("----------------------------------------------------" + System.Environment.NewLine);
                    for (int i = 0; i < vamf.Count; i++)
                    {
                        //  Варианты !!!!!
                        LexemeValue va = (LexemeValue)vamf[i];
                        byte[] bytes = Encoding.Default.GetBytes(va.Value);
                        string message = Encoding.UTF8.GetString(bytes);
                        phrases.Add(message);
                        writer.WriteLine("Добавлен вариант для распознавания " + message + System.Environment.NewLine);
                    }
                }
                outputBox.Text += "Результат вывода записан в файл output.txt";
                if (vamf.Count == 0)
                    clips.Eval("(assert (clearmessage))");
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            //ReadFacts();
            clips.Run();
            HandleResponse();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            outputBox.Text = "Выполнены команды Clear и Reset." + System.Environment.NewLine;
            //  Здесь сохранение в файл, и потом инициализация через него
            clips.Clear();

            /*string stroka = codeBox.Text;
            System.IO.File.WriteAllText("tmp.clp", codeBox.Text);
            clips.Load("tmp.clp");*/

            //  Так тоже можно - без промежуточного вывода в файл
            clips.LoadFromString(codeBox.Text);

            clips.Reset();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (clipsOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                codeBox.Text = System.IO.File.ReadAllText(clipsOpenFileDialog.FileName);
                Text = "Экспертная система \"Тиндер\" – " + clipsOpenFileDialog.FileName;
            }
        }

        private void fontSelect_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                codeBox.Font = fontDialog1.Font;
                outputBox.Font = fontDialog1.Font;
            }
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            clipsSaveFileDialog.FileName = clipsOpenFileDialog.FileName;
            if (clipsSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(clipsSaveFileDialog.FileName, codeBox.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadFacts();
            File.WriteAllText(outputFilePath, "Хочу вывести " + Facts[target] + System.Environment.NewLine);
            while (CanContinue) {
                nextBtn_Click(sender, e);
            }
            CanContinue = true;
        }
    }
}
