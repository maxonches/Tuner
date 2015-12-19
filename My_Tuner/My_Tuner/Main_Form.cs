using System;
using System.Windows.Forms;
using Zahvat_Zvuka;

namespace My_Tuner
{
    public partial class Main_Form : Form
    {
        bool is_Listenning = false;

        /// <summary>
        ///  свойство - записываем или нет
        /// </summary>
        public bool Is_Listenning
        {
            get { return is_Listenning; }
        }

        public Main_Form()
        {
            Initialize_Component();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        Chast_Inf_Source chast_info_source;

        /// <summary>
        ///  метод останавливает тюнер
        /// </summary>
        private void StopListenning() 
        {
            is_Listenning = false; /* установили флаг, что запись остановлена */

            chast_info_source.Stop();
            chast_info_source.Nayden_Chast -= new EventHandler<Nayden_Chast_Event_Args>(chast_info_source_Nayden_Chast);
            chast_info_source = null;
        }

        /// <summary>
        ///  метод запуска тюнера
        /// </summary>
        /// <param name="DEVICE_"></param>
        private void Start_Listenning(Zahvat_Zvuka_Device DEVICE_) 
        {
            is_Listenning = true; /* устанавливаем флаг, что тюнер работает */

            chast_info_source = new Chast_Inf_Source(DEVICE_);
            chast_info_source.Nayden_Chast += new EventHandler<Nayden_Chast_Event_Args>(chast_info_source_Nayden_Chast);
            chast_info_source.Slushat();
        }

        void chast_info_source_Nayden_Chast(object sender, Nayden_Chast_Event_Args e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<Nayden_Chast_Event_Args>(chast_info_source_Nayden_Chast), sender, e);
            }
            else
            {
                Update_Chast_Displays(e.Chastota);
            }
        }

        /// <summary>
        ///  обновление частоты
        /// </summary>
        /// <param name="chastota"></param>
        private void Update_Chast_Displays(double chastota) 
        {
            /* если значение chastota (частоты) больше 0 */
            if (chastota > 0)
            {
                shkala_Chastoty1.SignalDetected = true;
                shkala_Chastoty1.Current_Chast = chastota;

                chastota_TextBox.Enabled = true;
                chastota_TextBox.Text = chastota.ToString("f2");

                double closestFrequency;
                string noteName;
                int octave = 0;
                if (chastota < 440.0 * Math.Pow(ToneStep, 3) / 4)
                    octave = 2;
                else if (chastota < 440.0 * Math.Pow(ToneStep, 3) / 2)
                    octave = 3;
                else if (chastota < 440.0 * Math.Pow(ToneStep, 3))
                    octave = 4;
                else if (chastota < 440.0 * Math.Pow(ToneStep, 3) * 2)
                    octave = 5;
                else if (chastota < 440.0 * Math.Pow(ToneStep, 3) * 4)
                    octave = 6;
                FindClosestNote(chastota, out closestFrequency, out noteName);
                nota_chastTextBox.Enabled = true;
                nota_chastTextBox.Text = closestFrequency.ToString("f2");
                nota_TextBox.Enabled = true;
                nota_TextBox.Text = noteName + octave.ToString();
            }
            else
            {
                shkala_Chastoty1.SignalDetected = false;

                chastota_TextBox.Enabled = false;
                nota_chastTextBox.Enabled = false;
                nota_TextBox.Enabled = false;
            }

        }

        static string[] NoteNames = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
        static double ToneStep = Math.Pow(2, 1.0 / 12);

        private void FindClosestNote(double frequency, out double closestFrequency, out string noteName)
        {
            const double AFrequency = 440.0;
            const int ToneIndexOffsetToPositives = 120;

            int toneIndex = (int)Math.Round( Math.Log(frequency / AFrequency, ToneStep) );
            noteName = NoteNames[(ToneIndexOffsetToPositives + toneIndex) % NoteNames.Length];
            closestFrequency = Math.Pow(ToneStep, toneIndex) * AFrequency;
        }

        private void start_Button_Click(object sender, EventArgs e)
        {
            /* запускаем тюнер */
            Zahvat_Zvuka_Device device = null; /* создаем объект класса Zahvat_Zvuka_Device */

            using (Vibor_Device form = new Vibor_Device()) /* вызываем форму Vibor_Device */
            {
                if (form.ShowDialog() == DialogResult.OK) /* если на вызванной форме нажали выбрать */
                {
                    device = form.SelectedDevice; /* присвовили выбранный девайс */
                }
            }

            if (device != null) /* если девайс определен */
            {
                Start_Listenning(device); /* запустили */
                UpdateListenStopButtons(); /* выполняем метод UpdateListenStopButtons */
            }
        }

        /// <summary>
        /// делаем неактивными кнопки в зависимости от значения в поле is_Listenning
        /// </summary>
        private void UpdateListenStopButtons()
        {
            start_Button.Enabled = !is_Listenning;
            stop_Button.Enabled = is_Listenning;
        }

        private void stop_Button_Click(object sender, EventArgs e)
        {
            StopListenning(); /* останавливаем тюнер */

            UpdateListenStopButtons(); /* запускаем метод UpdateListenStopButtons */
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) /* закрытие формы */
        {
            if (Is_Listenning) /* если тюнер работает */
            {
                StopListenning(); /* останавливаем его */
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            start_Button.Focus(); /* установили фокус */
        }

        private void shkala_Chastoty1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Main_Form
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Main_Form";
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.ResumeLayout(false);

        }

        private void Main_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
