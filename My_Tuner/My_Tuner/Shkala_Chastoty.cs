using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace My_Tuner
{
    public partial class Shkala_Chastoty : UserControl
    {
        /* начальные констатны */
        const double MinChast = 70;
        const double MaxChast = 1200;
        const double AChast = 440;
        static double Tone_Step = Math.Pow(2, 1.0 / 12);

        /* массив Shkala_not */
        static Shkala_not[] Labels = 
        {
             new Shkala_not() { Title = "E2", Frequency =  82.4069, Color=Color.Green},
             new Shkala_not() { Title = "A2", Frequency = 110.0000, Color=Color.Green},
             new Shkala_not() { Title = "D3", Frequency = 146.8324, Color=Color.Green},
             new Shkala_not() { Title = "G3", Frequency = 195.9977, Color=Color.Green},
             new Shkala_not() { Title = "B3", Frequency = 246.9417, Color=Color.Green},
             new Shkala_not() { Title = "E4", Frequency = 329.6276, Color=Color.Green},
        };       

        double current_Chast;

        /* задаем дефолтное значение */
        [DefaultValue(0.0)]
                  
        public double Current_Chast
        {
            get
            {
                return current_Chast; /* возвращает текущее значение частоты */
            }
            set
            {
                /* если текущее значение изменилось - то меняем его */
                if (current_Chast != value)
                { 
                    current_Chast = value; Invalidate(); 
                }
            }
        }

        bool signalDetected = false;

        /* задаем дефолтное значение */
        [DefaultValue(false)]

        /* свойство */
        public bool SignalDetected
        {
            /* возвращает, обнаружен ли сигнал */
            get
            {
                return signalDetected;
            }

            set
            {
                /* меняем флаг signalDetected */
                if (signalDetected != value)
                {
                    signalDetected = value; Invalidate();
                }
            }
        }

        /// <summary>
        ///  конструктор
        /// </summary>
        public Shkala_Chastoty()
        {
            /* создаем компоненты */
            InitializeComponent();
            InitializeComponent2();
        }

        /// <summary>
        ///  конструктор
        /// </summary>
        /// <param name="container"></param>
        public Shkala_Chastoty(IContainer container)
        {
            container.Add(this);  /* указываем контейнер */

            /* создаем компоненты */
            InitializeComponent();
            InitializeComponent2();
        }

        /// <summary>
        ///  создаем компонеты
        /// </summary>
        private void InitializeComponent2()
        {
            /* установка стилей */
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        static Pen MarkerPen = new Pen(Color.White);
        static Brush ActiveSliderBrush = new SolidBrush(Color.White);
        static Brush InactiveSliderBrush = new SolidBrush(Color.FromArgb(70, Color.Red));
        const int DisplayPadding = 5; /* высота названий нот */
        const int MarkWidth = 7; /* высота линий штриховки */
        const int LabelMarkSize = 10;

        /// <summary>
        ///  метод перерисовки окна
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e) 
        {
            /* задали минимальный и максимальный шаг */
            int minStep = (int)Math.Floor(GetToneStep(MinChast));
            int maxStep = (int)Math.Ceiling(GetToneStep(MaxChast));

            /* центр */
            int center = Height / 2;
            int totalSteps = maxStep - minStep;
            /* размер шага */
            float stepSize = (float)(this.Width - 2 * DisplayPadding) / totalSteps;

            /* рисуем */
            for (int i = 0; i <= totalSteps; i++)
            {
                float x = stepSize * i + DisplayPadding;

                /* рисует лиини по заданным координатам с заданым свойством Pen */
                e.Graphics.DrawLine(MarkerPen, x, center - MarkWidth / 2, x, center + MarkWidth / 2);
            }
            float maxTextWidth = e.Graphics.MeasureString("W", Font).Width;

            foreach (Shkala_not label in Labels) /* выводим юзерские компонеты Shkala_not */
            {
                Brush labelBrush = new SolidBrush(label.Color);
                double labelStep = GetToneStep(label.Frequency);
                float labelPosition = (float)(stepSize * (maxStep - labelStep) + DisplayPadding);

                /* рисуем треугольник */
                e.Graphics.FillPolygon(labelBrush, new PointF[]  
                {
                    new PointF(labelPosition, center ),
                    new PointF(labelPosition - 4, center - 17),
                    new PointF(labelPosition + 4, center - 17),
                });

                SizeF titleSize = e.Graphics.MeasureString(label.Title, Font);

                /* выводим текст */
                e.Graphics.DrawString(label.Title, Font, Brushes.Green,
                    new PointF(labelPosition - titleSize.Width / 2, center - 19 - titleSize.Height / 2 - DisplayPadding));
            }

            if (Current_Chast > 0) /* если частота больше 0 */
            {
                Brush sliderBrush;

                /* и если сигнал обнаружен */
                if (!SignalDetected)
                {
                    /* задаем кисть красного цвета */
                    sliderBrush = InactiveSliderBrush;
                }
                else
                {
                    /* задаем кисть белого цвета */
                    sliderBrush = ActiveSliderBrush;
                }
                
                /* вычисляем шаг */
                double sliderStep = GetToneStep(Current_Chast);
                float sliderPosition = (float)(stepSize * (maxStep - sliderStep) + DisplayPadding);

                /* рисуем прямоугольник с заданными настройками */
                e.Graphics.FillPolygon(sliderBrush, new PointF[] 
                {
                    new PointF(sliderPosition, center ),
                    new PointF(sliderPosition - 5, center + 27),
                    new PointF(sliderPosition + 5, center + 27),
                });
            }

        }

        /// <summary>
        ///  метод вычисляет шаг
        /// </summary>
        /// <param name="chastota"></param>
        /// <returns></returns>
        private double GetToneStep(double chastota) 
        {
            return Math.Log(chastota / AChast, Tone_Step);
        }

        /// <summary>
        ///  класс Shkala_not содержит 3 поля
        /// </summary>
        class Shkala_not
        {
            public string Title;
            public double Frequency;
            public Color Color;
        }
    }
}
