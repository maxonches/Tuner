using System;

namespace BPF
{
    struct Kompl_Chisl
    {
        public double RE;
        public double IM;

        public Kompl_Chisl(double RE_)
        {
            this.RE = RE_;
            this.IM = 0;
        }

        public Kompl_Chisl(double RE_, double IM_)
        {
            this.RE = RE_;
            this.IM = IM_;
        }

        /// <summary>
        ///  перегрузка оператора умножения
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static Kompl_Chisl operator *(Kompl_Chisl n1, Kompl_Chisl n2)
        {
            return new Kompl_Chisl(n1.RE * n2.RE - n1.IM * n2.IM, n1.IM * n2.RE + n1.RE * n2.IM);
        }

         /// <summary>
         ///  перегрузка оператора сложения
         /// </summary>
         /// <param name="n1"></param>
         /// <param name="n2"></param>
         /// <returns></returns>
        public static Kompl_Chisl operator +(Kompl_Chisl n1, Kompl_Chisl n2)
        {
            return new Kompl_Chisl(n1.RE + n2.RE, n1.IM + n2.IM);
        }

        /// <summary>
        ///  перегрузка оператора вычитания
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static Kompl_Chisl operator -(Kompl_Chisl n1, Kompl_Chisl n2) 
        {
            return new Kompl_Chisl(n1.RE - n2.RE, n1.IM - n2.IM);
        }

        /// <summary>
        ///  перегрузка оператора отрицания
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Kompl_Chisl operator -(Kompl_Chisl n)
        {
            return new Kompl_Chisl(-n.RE, -n.IM);
        }

        /// <summary>
        ///  преобразуем Double в Kompl_Chisl
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static implicit operator Kompl_Chisl(double n) 
        {
            return new Kompl_Chisl(n, 0);
        }
        
        public Kompl_Chisl PowE()
        {
            double e = Math.Exp(RE);
            return new Kompl_Chisl(e * Math.Cos(IM), e * Math.Sin(IM)); /* вычисляем новое комплексное число */
        }

        /// <summary>
        ///  метод возводит в квадрат и вычитает части комплесного числа
        /// </summary>
        /// <returns></returns>
        public double Pow_2() 
        {
            return RE * RE - IM * IM;
        }

        /// <summary>
        ///  метод возводит в квадрат и складывает части комплесного числа
        /// </summary>
        /// <returns></returns>
        public double Abs_Pow_2()
        {
            return RE * RE + IM * IM;
        }

        /// <summary>
        ///  перегрузка метода ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() 
        {
            return String.Format("{0}+i*{1}", RE, IM); /* возвращает строку заданного формата */
        }
    }
}
