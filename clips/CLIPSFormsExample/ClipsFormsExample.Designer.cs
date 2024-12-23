namespace ClipsFormsExample
{
    partial class ClipsFormsExample
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipsFormsExample));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.codeBox = new System.Windows.Forms.TextBox();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.GoalCheckBox = new System.Windows.Forms.CheckedListBox();
            this.InputsListedCheckBox = new System.Windows.Forms.CheckedListBox();
            this.fontButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.clipsOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.clipsSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1191, 606);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.codeBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.outputBox);
            this.splitContainer1.Size = new System.Drawing.Size(1191, 606);
            this.splitContainer1.SplitterDistance = 558;
            this.splitContainer1.TabIndex = 2;
            // 
            // codeBox
            // 
            this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.codeBox.Location = new System.Drawing.Point(0, 0);
            this.codeBox.Multiline = true;
            this.codeBox.Name = "codeBox";
            this.codeBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.codeBox.Size = new System.Drawing.Size(558, 606);
            this.codeBox.TabIndex = 2;
            // 
            // outputBox
            // 
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outputBox.Location = new System.Drawing.Point(0, 0);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(629, 606);
            this.outputBox.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.GoalCheckBox);
            this.panel2.Controls.Add(this.InputsListedCheckBox);
            this.panel2.Controls.Add(this.fontButton);
            this.panel2.Controls.Add(this.nextButton);
            this.panel2.Controls.Add(this.resetButton);
            this.panel2.Controls.Add(this.saveAsButton);
            this.panel2.Controls.Add(this.openButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 328);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1193, 332);
            this.panel2.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(390, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "Вывод";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GoalCheckBox
            // 
            this.GoalCheckBox.FormattingEnabled = true;
            this.GoalCheckBox.Items.AddRange(new object[] {
            "A1 Полнота_действительных_чисел",
            "A2 Архимедова_аксиома",
            "A3 Предел_последовательности_Коши",
            "A4 Полнота_порядка",
            "A5 Линейность_операции_сложения",
            "A6 Линейность_операции_умножения",
            "A7 Монотонность_неравенств",
            "A8 Свойства_абсолютного_значения",
            "A9 Аксиома_Кантора",
            "A10 Аксиома_Вейерштрасса",
            "A11 Линейность_интеграла",
            "A12 Линейность_производной",
            "A13 Непрерывность_суммы",
            "A14 Непрерывность_произведения",
            "A15 Свойства_супремума_и_инфимума",
            "T1 Теорема_о_существовании_предела_монотонной_ограниченной_последовательности",
            "T2 Теорема_о_существовании_и_единственности_предела",
            "T3 Теорема_Больцано-Вейерштрасса",
            "T4 Теорема_о_промежуточных_значениях",
            "T5 Теорема_о_предельном_переходе_в_неравенствах",
            "T6 Теорема_о_непрерывности_суммы_и_произведения_функций",
            "T7 Основная_теорема_анализа",
            "T8 Свойства_пределов_сложения",
            "T9 Свойства_пределов_произведения",
            "T10 Теорема_о_среднем_значении",
            "T11 Интегрируемость_непрерывных_функций",
            "T12 Интегрируемость_ограниченных_функций",
            "T13 Предел_производной",
            "T14 Дифференцируемость_суммы",
            "T15 Дифференцируемость_произведения",
            "T16 Формула_Ньютона-Лейбница",
            "T17 Теорема_о_непрерывности_обратной_функции",
            "T18 Существование_и_единственность_решения_дифференциального_уравнения",
            "T19 Теорема_об_единственности_предела",
            "T20 Теорема_о_замене_переменной_в_интеграле",
            "T21 Свойства_интегралов",
            "T22 Свойства_производных",
            "T23 Ряд_Тейлора",
            "T24 Интегрируемость_непрерывно_дифференцируемых_функций",
            "T25 Сходимость_интегралов_Римана",
            "T26 Признак_сходимости_ряда",
            "T27 Признак_Даламбера",
            "T28 Признак_сравнения_для_рядов",
            "T29 Теорема_о_лемме_Фату",
            "T30 Теорема_о_равномерной_непрерывности",
            "T31 Теорема_о_непрерывности_предела",
            "T32 Теорема_о_точечной_сходимости_последовательности_функций",
            "T33 Теорема_о_предельном_переходе_в_интегралах",
            "T34 Теорема_о_пределе_ряда_функций",
            "T35 Свойства_рядов_Тейлора",
            "T36 Признак_сходимости_интегралов",
            "T37 Признак_сходимости_Абеля",
            "T38 Теорема_Вейерштрасса_о_сходимости_ряда_функций",
            "T39 Теорема_о_дифференцировании_под_знаком_интеграла",
            "T40 Теорема_о_непрерывности_интеграла_по_параметру",
            "T41 Теорема_Стокса",
            "T42 Теорема_Гаусса-Остроградского",
            "T43 Теорема_о_характеристическом_свойстве_интеграла",
            "T44 Признак_Вейерштрасса",
            "T45 Интеграл_Лебега",
            "T46 Теорема_о_смене_порядка_интегрирования",
            "T47 Признак_Коши_сходимости_интегралов",
            "T48 Теорема_о_компактности_в_Lp-пространствах",
            "T49 Признак_абсолютной_сходимости",
            "T50 Теорема_о_последовательности_непрерывных_функций",
            "T51 Теорема_о_равномерной_сходимости_последовательности_функций",
            "T52 Свойства_функций_класса_C1",
            "T53 Дифференцирование_под_знаком_суммы",
            "T54 Теорема_о_замене_переменной_в_кратном_интеграле",
            "T55 Теорема_о_непрерывности_меры",
            "T56 Лемма_о_распределении_предела",
            "T57 Теорема_о_линейной_непрерывности_оператора",
            "T58 Теорема_о_преобразовании_Фурье",
            "T59 Свойства_преобразования_Фурье",
            "T60 Теорема_о_замене_переменной_для_интегралов_Лебега",
            "T61 Свойства_градиента",
            "T62 Свойства_дивергенции",
            "T63 Лемма_Фату_для_интегралов_Лебега",
            "T64 Теорема_о_выпуклых_функциях",
            "T65 Теорема_о_неравенстве_Йенсена",
            "T66 Теорема_о_независимости_порядка_интегрирования_в_условных_интегралах",
            "T67 Теорема_о_спрямляемых_кривых",
            "T68 Неравенство_Гёльдера",
            "T69 Неравенство_Минковского",
            "T70 Принцип_локальной_компактности",
            "T71 Теорема_о_пределе_интегралов_Лебега",
            "T72 Лемма_о_сходимости_Фату",
            "T73 Неравенство_Чебышёва_для_интегралов",
            "T74 Признак_Абеля_для_рядов",
            "T75 Теорема_о_линейности_интеграла_Лебега",
            "T76 Признак_Римана_для_абсолютной_сходимости",
            "T77 Лемма_о_слабой_сходимости",
            "T78 Теорема_о_слабой_сходимости_в_Lp-пространствах",
            "T79 Теорема_о_неравенстве_Хёльдера",
            "T80 Теорема_о_свойствах_пространства_L2",
            "T81 Теорема_о_компактности_множества_в_Lp-пространствах",
            "T82 Теорема_о_неравенстве_треугольника_в_Lp-пространствах",
            "T83 Неравенство_Йенсена_в_пространстве_Lp",
            "T84 Теорема_о_равномерной_ограниченности_операторов",
            "T85 Теорема_о_непрерывности_сужения",
            "T86 Лемма_об_интегрируемых_функциях",
            "T87 Теорема_о_разложении_функций",
            "T88 Теорема_о_равномерной_сходимости_функций",
            "T89 Принцип_наименьшего_действия",
            "T90 Теорема_о_равномерной_ограниченности_интегралов",
            "T91 Теорема_о_непрерывности_производной",
            "T92 Принцип_компактности_в_анализе",
            "T93 Теорема_о_минимуме_в_компактных_множествах",
            "T94 Принцип_вытеснения",
            "T95 Признак_Коши_для_рядов_в_Lp-пространствах",
            "T96 Лемма_о_собственных_функциях_оператора",
            "T97 Теорема_о_проекции_на_подпространство",
            "T98 Теорема_о_слабой_компактности_в_Lp-пространствах",
            "T99 Принцип_максимума",
            "T100 Теорема_о_монотонной_сходимости",
            "T101 Теорема_о_вложении_Соболева",
            "T102 Теорема_о_равномерной_сходимости_рядов_функций",
            "T103 Лемма_об_интегралах_Фубини",
            "T104 Теорема_о_существовании_решений_для_уравнений_с_частными_производными",
            "T105 Теорема_о_плотности_в_пространстве_Lp"});
            this.GoalCheckBox.Location = new System.Drawing.Point(336, 48);
            this.GoalCheckBox.Name = "GoalCheckBox";
            this.GoalCheckBox.Size = new System.Drawing.Size(297, 214);
            this.GoalCheckBox.TabIndex = 11;
            // 
            // InputsListedCheckBox
            // 
            this.InputsListedCheckBox.FormattingEnabled = true;
            this.InputsListedCheckBox.Items.AddRange(new object[] {
            "A1 Полнота_действительных_чисел",
            "A2 Архимедова_аксиома",
            "A3 Предел_последовательности_Коши",
            "A4 Полнота_порядка",
            "A5 Линейность_операции_сложения",
            "A6 Линейность_операции_умножения",
            "A7 Монотонность_неравенств",
            "A8 Свойства_абсолютного_значения",
            "A9 Аксиома_Кантора",
            "A10 Аксиома_Вейерштрасса",
            "A11 Линейность_интеграла",
            "A12 Линейность_производной",
            "A13 Непрерывность_суммы",
            "A14 Непрерывность_произведения",
            "A15 Свойства_супремума_и_инфимума",
            "T1 Теорема_о_существовании_предела_монотонной_ограниченной_последовательности",
            "T2 Теорема_о_существовании_и_единственности_предела",
            "T3 Теорема_Больцано-Вейерштрасса",
            "T4 Теорема_о_промежуточных_значениях",
            "T5 Теорема_о_предельном_переходе_в_неравенствах",
            "T6 Теорема_о_непрерывности_суммы_и_произведения_функций",
            "T7 Основная_теорема_анализа",
            "T8 Свойства_пределов_сложения",
            "T9 Свойства_пределов_произведения",
            "T10 Теорема_о_среднем_значении",
            "T11 Интегрируемость_непрерывных_функций",
            "T12 Интегрируемость_ограниченных_функций",
            "T13 Предел_производной",
            "T14 Дифференцируемость_суммы",
            "T15 Дифференцируемость_произведения",
            "T16 Формула_Ньютона-Лейбница",
            "T17 Теорема_о_непрерывности_обратной_функции",
            "T18 Существование_и_единственность_решения_дифференциального_уравнения",
            "T19 Теорема_об_единственности_предела",
            "T20 Теорема_о_замене_переменной_в_интеграле",
            "T21 Свойства_интегралов",
            "T22 Свойства_производных",
            "T23 Ряд_Тейлора",
            "T24 Интегрируемость_непрерывно_дифференцируемых_функций",
            "T25 Сходимость_интегралов_Римана",
            "T26 Признак_сходимости_ряда",
            "T27 Признак_Даламбера",
            "T28 Признак_сравнения_для_рядов",
            "T29 Теорема_о_лемме_Фату",
            "T30 Теорема_о_равномерной_непрерывности",
            "T31 Теорема_о_непрерывности_предела",
            "T32 Теорема_о_точечной_сходимости_последовательности_функций",
            "T33 Теорема_о_предельном_переходе_в_интегралах",
            "T34 Теорема_о_пределе_ряда_функций",
            "T35 Свойства_рядов_Тейлора",
            "T36 Признак_сходимости_интегралов",
            "T37 Признак_сходимости_Абеля",
            "T38 Теорема_Вейерштрасса_о_сходимости_ряда_функций",
            "T39 Теорема_о_дифференцировании_под_знаком_интеграла",
            "T40 Теорема_о_непрерывности_интеграла_по_параметру",
            "T41 Теорема_Стокса",
            "T42 Теорема_Гаусса-Остроградского",
            "T43 Теорема_о_характеристическом_свойстве_интеграла",
            "T44 Признак_Вейерштрасса",
            "T45 Интеграл_Лебега",
            "T46 Теорема_о_смене_порядка_интегрирования",
            "T47 Признак_Коши_сходимости_интегралов",
            "T48 Теорема_о_компактности_в_Lp-пространствах",
            "T49 Признак_абсолютной_сходимости",
            "T50 Теорема_о_последовательности_непрерывных_функций",
            "T51 Теорема_о_равномерной_сходимости_последовательности_функций",
            "T52 Свойства_функций_класса_C1",
            "T53 Дифференцирование_под_знаком_суммы",
            "T54 Теорема_о_замене_переменной_в_кратном_интеграле",
            "T55 Теорема_о_непрерывности_меры",
            "T56 Лемма_о_распределении_предела",
            "T57 Теорема_о_линейной_непрерывности_оператора",
            "T58 Теорема_о_преобразовании_Фурье",
            "T59 Свойства_преобразования_Фурье",
            "T60 Теорема_о_замене_переменной_для_интегралов_Лебега",
            "T61 Свойства_градиента",
            "T62 Свойства_дивергенции",
            "T63 Лемма_Фату_для_интегралов_Лебега",
            "T64 Теорема_о_выпуклых_функциях",
            "T65 Теорема_о_неравенстве_Йенсена",
            "T66 Теорема_о_независимости_порядка_интегрирования_в_условных_интегралах",
            "T67 Теорема_о_спрямляемых_кривых",
            "T68 Неравенство_Гёльдера",
            "T69 Неравенство_Минковского",
            "T70 Принцип_локальной_компактности",
            "T71 Теорема_о_пределе_интегралов_Лебега",
            "T72 Лемма_о_сходимости_Фату",
            "T73 Неравенство_Чебышёва_для_интегралов",
            "T74 Признак_Абеля_для_рядов",
            "T75 Теорема_о_линейности_интеграла_Лебега",
            "T76 Признак_Римана_для_абсолютной_сходимости",
            "T77 Лемма_о_слабой_сходимости",
            "T78 Теорема_о_слабой_сходимости_в_Lp-пространствах",
            "T79 Теорема_о_неравенстве_Хёльдера",
            "T80 Теорема_о_свойствах_пространства_L2",
            "T81 Теорема_о_компактности_множества_в_Lp-пространствах",
            "T82 Теорема_о_неравенстве_треугольника_в_Lp-пространствах",
            "T83 Неравенство_Йенсена_в_пространстве_Lp",
            "T84 Теорема_о_равномерной_ограниченности_операторов",
            "T85 Теорема_о_непрерывности_сужения",
            "T86 Лемма_об_интегрируемых_функциях",
            "T87 Теорема_о_разложении_функций",
            "T88 Теорема_о_равномерной_сходимости_функций",
            "T89 Принцип_наименьшего_действия",
            "T90 Теорема_о_равномерной_ограниченности_интегралов",
            "T91 Теорема_о_непрерывности_производной",
            "T92 Принцип_компактности_в_анализе",
            "T93 Теорема_о_минимуме_в_компактных_множествах",
            "T94 Принцип_вытеснения",
            "T95 Признак_Коши_для_рядов_в_Lp-пространствах",
            "T96 Лемма_о_собственных_функциях_оператора",
            "T97 Теорема_о_проекции_на_подпространство",
            "T98 Теорема_о_слабой_компактности_в_Lp-пространствах",
            "T99 Принцип_максимума",
            "T100 Теорема_о_монотонной_сходимости",
            "T101 Теорема_о_вложении_Соболева",
            "T102 Теорема_о_равномерной_сходимости_рядов_функций",
            "T103 Лемма_об_интегралах_Фубини",
            "T104 Теорема_о_существовании_решений_для_уравнений_с_частными_производными",
            "T105 Теорема_о_плотности_в_пространстве_Lp"});
            this.InputsListedCheckBox.Location = new System.Drawing.Point(12, 48);
            this.InputsListedCheckBox.Name = "InputsListedCheckBox";
            this.InputsListedCheckBox.Size = new System.Drawing.Size(297, 214);
            this.InputsListedCheckBox.TabIndex = 10;
            // 
            // fontButton
            // 
            this.fontButton.Location = new System.Drawing.Point(264, 12);
            this.fontButton.Name = "fontButton";
            this.fontButton.Size = new System.Drawing.Size(120, 30);
            this.fontButton.TabIndex = 9;
            this.fontButton.Text = "Шрифт...";
            this.fontButton.UseVisualStyleBackColor = true;
            this.fontButton.Click += new System.EventHandler(this.fontSelect_Click);
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.Location = new System.Drawing.Point(880, 264);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(120, 30);
            this.nextButton.TabIndex = 8;
            this.nextButton.Text = "Дальше";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.Location = new System.Drawing.Point(754, 264);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(120, 30);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "Рестарт";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Location = new System.Drawing.Point(138, 12);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(120, 30);
            this.saveAsButton.TabIndex = 6;
            this.saveAsButton.Text = "Сохранить как...";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(12, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(120, 30);
            this.openButton.TabIndex = 5;
            this.openButton.Text = "Открыть";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openFile_Click);
            // 
            // clipsOpenFileDialog
            // 
            this.clipsOpenFileDialog.Filter = "CLIPS files|*.clp|All files|*.*";
            this.clipsOpenFileDialog.Title = "Открыть файл кода CLIPS";
            // 
            // clipsSaveFileDialog
            // 
            this.clipsSaveFileDialog.Filter = "CLIPS files|*.clp|All files|*.*";
            this.clipsSaveFileDialog.Title = "Созранить файл как...";
            // 
            // ClipsFormsExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 660);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(660, 298);
            this.Name = "ClipsFormsExample";
            this.Text = "Экспертная система \"Тиндер\"";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox codeBox;
    private System.Windows.Forms.TextBox outputBox;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.Button resetButton;
    private System.Windows.Forms.Button saveAsButton;
    private System.Windows.Forms.Button openButton;
    private System.Windows.Forms.OpenFileDialog clipsOpenFileDialog;
    private System.Windows.Forms.Button fontButton;
    private System.Windows.Forms.FontDialog fontDialog1;
    private System.Windows.Forms.SaveFileDialog clipsSaveFileDialog;
        private System.Windows.Forms.CheckedListBox GoalCheckBox;
        private System.Windows.Forms.CheckedListBox InputsListedCheckBox;
        private System.Windows.Forms.Button button1;
    }
}

