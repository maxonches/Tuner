using System;

namespace BPF
{
    public static class BPF_Algoritm
    {
        /// <summary>
        /// преобразование Фурье
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double[] Calc(double[] x)
        {
            int dlina;
            int bit_v_dl;

            if (Pow_Two(x.Length)) /* если длина массива является степенью числа 2 */
            {
                dlina = x.Length; /* запоминаем длину массива */
                bit_v_dl = Log2(dlina) - 1; /* количество бит */
            }

            else
            {
                bit_v_dl = Log2(x.Length);  /* кол-во бит */
                dlina = 1 << bit_v_dl; /* битовый сдвиг влево на bit_v_dl бит */
            }

            Kompl_Chisl[] data = new Kompl_Chisl[dlina]; /* создаем комплексное число */

            for (int i = 0; i < x.Length; i++) /* в цикле разворачиваем биты и записываем в массив комплексное число */
            {
                int j = Razv_bits(i, bit_v_dl);
                data[j] = new Kompl_Chisl(x[i]);
            }

            for (int i = 0; i < bit_v_dl; i++)
            {
                int m = 1 << i;
                int n = m * 2;
                double ALPHA = -(2 * Math.PI / n);

                for (int k = 0; k < m; k++)
                {
                    Kompl_Chisl nech_chast_mnoj = new Kompl_Chisl(0, ALPHA * k).PowE();

                    for (int j = k; j < dlina; j += n)
                    {
                        Kompl_Chisl ravn_chast = data[j];
                        Kompl_Chisl nech_chast = nech_chast_mnoj * data[j + m];
                        data[j] = ravn_chast + nech_chast;
                        data[j + m] = ravn_chast - nech_chast;
                    }
                }
            }

            /* спектрограмма */
            double[] spectrogramma = new double[dlina];

            for (int i = 0; i < spectrogramma.Length; i++)
            {
                spectrogramma[i] = data[i].Abs_Pow_2();
            }
            return spectrogramma;
        }

        /// <summary>
        ///  количество бит в числе
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int Log2(int n)
        {
            int i = 0;

            while (n > 0) /* цикл - пока n больше нуля */
            {
                ++i; /* увеличиваем счетчик i */
                n >>= 1; /* сдвигаем n на 1 бит вправо */
            }
            return i;
        }

        /// <summary>
        ///  переворачиваем биты
        /// </summary>
        /// <param name="n"></param>
        /// <param name="Count_bits"></param>
        /// <returns></returns>
        private static int Razv_bits(int n, int Count_bits)
        {
            int perevern = 0;

            for (int i = 0; i < Count_bits; i++)
            {
                int sled_bit = n & 1; /* битовое И */
                n >>= 1; /* битовый сдвиг вправо */
                perevern <<= 1; /* битовый сдвиг влево */
                perevern |= sled_bit; /* Оператор присваивания OR */
            }
            return perevern;
        }

        /// <summary>
        /// метод проверяем является ли число степенью числа 2
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static bool Pow_Two(int n)
        {            
            return n > 1 && (n & (n - 1)) == 0;
        }
    }
}

