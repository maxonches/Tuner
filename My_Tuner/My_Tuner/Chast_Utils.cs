using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BPF;

namespace My_Tuner
{
    static class Chast_Utils
    {
        /// <summary>
        /// метод ищет основную частоту
        /// </summary>
        /// <param name="x"></param>
        /// <param name="sampleRate_"></param>
        /// <param name="min_chast"></param>
        /// <param name="max_chast"></param>
        /// <returns></returns>
 
        internal static double Poisk_Gl_Chast(double[] x, int sampleRate_, double min_chast, double max_chast)
        {
            double[] spectr = BPF_Algoritm.Calc(x); /* обьявляем массив спектров */

            /* вычисляем максимальный и минимальный спектр */
            int Min_spectr = Math.Max(0, (int)(min_chast * spectr.Length / sampleRate_));
            int Max_spectr = Math.Min(spectr.Length, (int)(max_chast * spectr.Length / sampleRate_) + 1);
            
            const int Peak_Count = 5;
            int[] peak_search;

           /* ищем пики */
            peak_search = Such_Peaks(spectr, Min_spectr, Max_spectr - Min_spectr, Peak_Count);

            /* ищем peak_search в peak_search значение Min_spectr
             если не нашли - выходим */
            if (Array.IndexOf(peak_search, Min_spectr) >= 0)
            {
                return 0;
            }

            const int prov_smesh_fragm = 0;
            int prov_dlin_fragm = (int)(sampleRate_ / min_chast);
            double zn_min_peak = Double.PositiveInfinity;
            int ind_min_peak = 0;
            int min_opt_interv = 0;

            for (int i = 0; i < peak_search.Length; i++)  /* перебираем все пики в поиске минимального значения */
            {
                int index = peak_search[i];
                int bin_Inter_Start = spectr.Length / (index + 1), bin_Inter_End = spectr.Length / index;
                int inter;
                double zn_peak;

                /* сканирование частоты/интервала сигнала */
                Scan_Sign_Inter(x, prov_smesh_fragm, prov_dlin_fragm, bin_Inter_Start, bin_Inter_End, out inter, out zn_peak);

                if (zn_peak < zn_min_peak) /* если найденный меньше минимального - делаем его миниальным */
                {
                    zn_min_peak = zn_peak;
                    ind_min_peak = index;
                    min_opt_interv = inter;
                }
            }
            return (double)sampleRate_ / min_opt_interv;
        }

        private static void Scan_Sign_Inter(double[] x, int index, int dlina,
            int inter_Min, int inter_Max, out int opt_Inter, out double opt_Zn)
        {
            opt_Zn = Double.PositiveInfinity;
            opt_Inter = 0;

            const int Max_Chislo_Shag = 30;

            /* расчитываем шаг */
            int shagi = inter_Max - inter_Min;
            if (shagi > Max_Chislo_Shag)
                shagi = Max_Chislo_Shag;

            else if (shagi <= 0)
                shagi = 1;

            for (int i = 0; i < shagi; i++) /* Ищем интервалы с минимальной волной */
            {
                /* текущий интервал */
                int interval = inter_Min + (inter_Max - inter_Min) * i / shagi;

                double sum = 0;
               
                for (int j = 0; j < dlina; j++)
                {
                    double dif = x[index + j] - x[index + j + interval];
                    sum += dif * dif;
                }
                if (opt_Zn > sum)
                {
                    opt_Zn = sum;
                    opt_Inter = interval;
                }
            }
        }

        private static int[] Such_Peaks(double[] znach, int index, int dlina, int peaks_Count)  /* ищем пики */
        {
            double[] peak_Zn = new double[peaks_Count];
            int[] peak_Ind = new int[peaks_Count];

            for (int i = 0; i < peaks_Count; i++) /* заполняем пики */
            {
                peak_Zn[i] = znach[peak_Ind[i] = i + index];
            }
            
            /* находим минимальное значение пика */
            double min_Stored_Peak = peak_Zn[0];
            int minInd = 0;

            for (int i = 1; i < peaks_Count; i++)
            {
                if (min_Stored_Peak > peak_Zn[i]) min_Stored_Peak = peak_Zn[minInd = i];
            }

            for (int i = peaks_Count; i < dlina; i++)
            {
                if (min_Stored_Peak < znach[i + index])
                {

                    /* меняем местами минмальный с максимальным значениями пик */
                    peak_Zn[minInd] = znach[peak_Ind[minInd] = i + index];

                    /* снова находим минимальное значение */
                    min_Stored_Peak = peak_Zn[minInd = 0];

                    for (int j = 1; j < peaks_Count; j++)
                    {
                        if (min_Stored_Peak > peak_Zn[j]) min_Stored_Peak = peak_Zn[minInd = j];
                    }
                }
            }
            return peak_Ind;
        }
    }
}
