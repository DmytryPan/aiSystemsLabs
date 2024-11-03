namespace Lab05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Model model = new Model("description.txt", "rules.txt");

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
