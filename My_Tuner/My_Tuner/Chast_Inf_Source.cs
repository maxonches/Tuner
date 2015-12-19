using System;
using Zahvat_Zvuka;

namespace My_Tuner
{
    class Chast_Inf_Source
    {
        Zahvat_Zvuka_Device DEVICE; /* аудиоустройство */
        Adapter adapter; /* адаптер */

        internal Chast_Inf_Source(Zahvat_Zvuka_Device DEVICE_)
        {
            this.DEVICE = DEVICE_;
        }

        /// <summary>
        ///  метод запускает адаптер
        /// </summary>
        public void Slushat()
        {
            adapter = new Adapter(this, DEVICE); /* создали адаптер */
            adapter.Start(); /* запустили */
        }

        /// <summary>
        ///  останавливаем адаптер
        /// </summary>
        public void Stop()
        {
            adapter.Stop();
        }

        /// <summary>
        ///  Объявляем событие
        /// </summary>

        public event EventHandler<Nayden_Chast_Event_Args> Nayden_Chast;

        /// <summary>
        ///  Используем метод для запуска события
        /// </summary>
        /// <param name="e"></param>
        public void On_Nayden_Chast(Nayden_Chast_Event_Args e) 
        {
            if (Nayden_Chast != null)
            {
                Nayden_Chast(this, e);
            }
        }
    }

    /* EventArgs - это класс, дающий возможность передать какую-нибудь дополнительную
     информацию обработчику (например, текущие координаты мыши при событии MouseMove) */
    class Nayden_Chast_Event_Args : EventArgs
    {
        double chastota;

        /// <summary>
        ///  свойство 
        /// </summary>
        public double Chastota 
        {
            get /* возвращает значение поля chastota */
            {
                return chastota;
            }
        }

        public Nayden_Chast_Event_Args(double chastota_) /* конструктор */
        {
            this.chastota = chastota_;
        }
    }

    /// <summary>
    /// . класс адаптер ( базовый класc Zahvat_Zvuka_Base)
    /// </summary>
    class Adapter : Zahvat_Zvuka_Base
    {
        Chast_Inf_Source OWNER;

        /* мин и макс частоты */
        const double Min_Chast = 60;
        const double Max_Chast = 1300;

        /* конструктор
        базовый класс DEVICE_ */
        internal Adapter(Chast_Inf_Source OWNER_, Zahvat_Zvuka_Device DEVICE_) : base(DEVICE_)
        {
            this.OWNER = OWNER_;
        }

        /// <summary>
        ///  метод изменяет данные
        /// </summary>
        /// <param name="data"></param>
        protected override void Proc_Data(short[] data) 
        {
            double[] x = new double[data.Length]; /* создали массив */

            for (int i = 0; i < x.Length; i++) /* проходим в цикле по переденному массиву и делим значения на  32768.0 */
            {
                x[i] = data[i] / 32768.0;
            }

            double chast = Chast_Utils.Poisk_Gl_Chast(x, Sample_Rate, Min_Chast, Max_Chast); /* ищем частоту */
            OWNER.On_Nayden_Chast(new Nayden_Chast_Event_Args(chast)); /* вызвали событие */
        }
    }
}
