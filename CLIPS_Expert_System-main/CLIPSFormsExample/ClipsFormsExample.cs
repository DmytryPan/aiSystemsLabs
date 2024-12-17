using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using CLIPSNET;
using System.Data.SqlTypes;


namespace ClipsFormsExample
{
    public partial class ClipsFormsExample : Form
    {
        private CLIPSNET.Environment clips = new CLIPSNET.Environment();

        public ClipsFormsExample()
        {
            InitializeComponent();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        string deducedFact;

        private void HandleResponse()
        {
            bool isProven = false;    
            
            String evalStr = "(find-fact ((?f ioproxy)) TRUE)";
            FactAddressValue fv = (FactAddressValue)((MultifieldValue)clips.Eval(evalStr))[0];

            MultifieldValue damf = (MultifieldValue)fv["messages"];
            // Извлекаем сообщения:

            outputBox.Text += "\r\nНачало итерации:" + System.Environment.NewLine;
            for (int i = 0; i < damf.Count; i++)
            {
                LexemeValue da = (LexemeValue)damf[i];

                byte[] bstring = Encoding.Default.GetBytes(da.Value);
                //outputBox.Text += da.Value;

                string responseMessage = Encoding.UTF8.GetString(bstring); // must have иначе получаем пылесос символов
                //string responseMessage = Encoding.UTF8.GetString(da.Value);


                outputBox.Text += responseMessage + "\r\n\r\n";


                string[] words = responseMessage.Split(' ');
                string provedFact = words[words.Length - 1].Trim('<', '>');
                if (provedFact == deducedFact)
                {
                    isProven = true;
                    break;
                }
            }

            clips.Eval("(assert (clearmessage))");
            if (isProven)
                outputBox.Text += "Теорема выведена!\r\n";
            else  
                outputBox.Text += "Неудачный вывод: Не хватает исходных данных.\r\n";
          
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (codeBox.Text.Length == 0)
            {
                outputBox.Text += "Загрузите .clp-файл!\r\n";
                return;
            }

            List<string> initFacts = new List<string>();

            bool isChoosed = false;

            foreach (var item in checkedListBox1.CheckedItems)
            {
                initFacts.Add(item.ToString());
                isChoosed = true;
            }

            if (!isChoosed)
            {
                outputBox.Text += "Теоремы-посылки не выбраны!\r\n";
                return;
            }

            if (listBox1.SelectedItem == null)
            {
                outputBox.Text += "Выберите теорему, которую надо доказать! \r\n";
                return;
            }
            else deducedFact = listBox1.SelectedItem.ToString();

            outputBox.Text += "!\r\nДано:\r\n\r\n";
            foreach (string s in initFacts)
            {
                outputBox.Text += s+"\r\n\r\n";
            }
            outputBox.Text += "\r\nНужно доказать:\r\n"+deducedFact+"\r\n";

            if (initFacts.Contains(deducedFact))
            {
                outputBox.Text += "\r\n" + deducedFact + " - целевой факт содержится в посылках\r\n";
                return;
            }

            foreach (string s in initFacts)
                clips.Eval($"(assert (theorem {s}))");

            clips.Run();
            HandleResponse();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            nextButton.Enabled = true;

            outputBox.Text =  System.Environment.NewLine;
            //  Здесь сохранение в файл, и потом инициализация через него
            clips.Clear();
            //  Так тоже можно - без промежуточного вывода в файл
            clips.LoadFromString(codeBox.Text);

            clips.Reset();
        }

        HashSet<string> theoremsNames = new HashSet<string>();

        private void openFile_Click(object sender, EventArgs e)
        {
            if (clipsOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                theoremsNames.Clear();
                checkedListBox1.Items.Clear();
                listBox1.Items.Clear();

                string text = codeBox.Text = System.IO.File.ReadAllText(clipsOpenFileDialog.FileName);
                string pattern = @"\(theorem\s+(.*?)\)"; // регулярка для парсинга

                MatchCollection matches = Regex.Matches(text, pattern);

                foreach (Match match in matches)
                {
                    string name = match.Groups[1].Value;
                    theoremsNames.Add(name);
                }

                foreach (string theorem in theoremsNames)
                {
                    checkedListBox1.Items.Add(theorem);
                    listBox1.Items.Add(theorem);
                }

                clips.Clear();
                clips.LoadFromString(codeBox.Text);
                clips.Reset();
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

        private void outputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void codeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
