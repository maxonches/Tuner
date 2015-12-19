using System;
using System.Windows.Forms;
using Zahvat_Zvuka;

namespace My_Tuner
{
    public partial class Vibor_Device : Form
    {
        /*  массив "звуковое устройство" */
        Zahvat_Zvuka_Device[] devices;

       /// <summary>
        ///  метод возвращает "звуковое устройство"
       /// </summary>
        public Zahvat_Zvuka_Device SelectedDevice
        {
        /* возвращает из списка устройств то устройство, порядковый номер которого равен выбранному элементу */ 
            get
            {
                return devices[Ustroistva.SelectedIndex];
            }
        }

        public Vibor_Device()
        {
            InitializeComponent();
        }

        private void Vibor_Device_Load(object sender, EventArgs e)
        {
            /* при загрзуке формы вызываем метод LoadDevices */
            LoadDevices();
        }

        /// <summary>
        ///  наполняем листбокc звуковыми устройствами
        /// </summary>
        private void LoadDevices() 
        {
            Ustroistva.Items.Clear(); /* очистили листбокc */

            int defaultDeviceIndex = 0;

            /* заполнили массив звуковыми устройствами */
            devices = Zahvat_Zvuka_Device.GetDevices();
            for (int i = 0; i < devices.Length; i++)
            {
                Ustroistva.Items.Add(devices[i].Name); 
                /* в цикле наполняем листбокс
                 если у i-отого девайса установлен флаг - IsDefault
                 то устанавливаем для него дефолтный индекс */
                if (devices[i].IsDefault)
                    defaultDeviceIndex = i;
            }
            Ustroistva.SelectedIndex = defaultDeviceIndex; /* выделяем дефолтный индекс */
        }

        private void deviceNamesListBox_DoubleClick(object sender, EventArgs e)
        {
            /* если при двойном нажатии на листбокс, ничего не выбрали - то выходим
            / в противном случае закрываем форму */
            if (Ustroistva.SelectedIndex < 0)
                return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }
    }
}