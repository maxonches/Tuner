using System;
using System.Threading;
using Microsoft.DirectX.DirectSound;
using Microsoft.Win32.SafeHandles;

namespace Zahvat_Zvuka
{
    /// <summary>
    ///  Базовый класс для аудиоустройств
    /// </summary>
    public abstract class Zahvat_Zvuka_Base : IDisposable
    {
        /* устанавливаем константы */
        const int BufferSeconds = 3;
        const int NotifyPointsInSecond = 2;

        const int BitsPerSample = 16; 
        const int ChannelCount = 1; 

        int sampleRate = 44100;
        bool isCapturing = false;
        bool disposed = false;

        /// <summary>
        ///  свойство
        /// </summary>
        public bool IsCapturing
        {
            get
            {
                return isCapturing;
            }
        }

        /// <summary>
        ///  свойство частота
        /// </summary>
        public int Sample_Rate
        {
            /* возвращает значение sampleRate */
            get
            {
                return sampleRate;
            }

            set 
            {
                /* если sampleRate меньше нуля - генерируем исключение */
                if (sampleRate <= 0) throw new ArgumentOutOfRangeException();
                /* иначе устанавливаем значение */
                sampleRate = value; 
            }
        }

        Capture capture;
        CaptureBuffer buffer;
        Notify notify;
        int bufferLength;
        AutoResetEvent positionEvent;
        SafeWaitHandle positionEventHandle;

        /// <summary>
        ///  системные события - при использовании потоков
        /// </summary>
        ManualResetEvent terminated;
        Thread thread;
        Zahvat_Zvuka_Device device;

        /// <summary>
        ///  конструктор
        /// </summary>
        public Zahvat_Zvuka_Base() : this(Zahvat_Zvuka_Device.GetDefaultDevice())
        {

        }

        /// <summary>
        ///  конструктор
        /// </summary>
        /// <param name="device"></param>

        public Zahvat_Zvuka_Base(Zahvat_Zvuka_Device device)
        {
            /* задаем начальные значения */
            this.device = device;
            /* указываем, что создаваемый объект изначально не будет в сигнальном состоянии */
            positionEvent = new AutoResetEvent(false);
           
            positionEventHandle = positionEvent.SafeWaitHandle;
           /* указываем, что создаваемый объект изначально будет в сигнальном состоянии */
            terminated = new ManualResetEvent(true);
        }

        /// <summary>
        ///  Start
        /// </summary>
        public void Start()
        {
            isCapturing = true; /* устанавливаем флаг в TRUE */

            WaveFormat format = new WaveFormat(); /* создаем обьект типа WaveFormat - класс которые содержит
                                                     свойства, которые определяются форматом WAV */
            format.Channels = ChannelCount; /* устанавливаем количество каналов */
            format.BitsPerSample = BitsPerSample; /* установки количество бит */
            format.SamplesPerSecond = Sample_Rate; /* устанавливаем частоту */
            format.FormatTag = WaveFormatTag.Pcm; /* устанавливаем формат */
            format.BlockAlign = (short)((format.Channels * format.BitsPerSample + 7) / 8); /* устанавливаем минимальный размер блока данных */
            format.AverageBytesPerSecond = format.BlockAlign * format.SamplesPerSecond; /* устанавливаем скорость передачи данных в байт/cек */
            bufferLength = format.AverageBytesPerSecond * BufferSeconds; /* вычисляем размер буфера */
            CaptureBufferDescription desciption = new CaptureBufferDescription(); /* CaptureBufferDescription структура описывающиа объект CaptureBuffer */
            desciption.Format = format; /* устанавливаем формат */
            desciption.BufferBytes = bufferLength; /* устанавливаем буфер */
            capture = new Capture(device.Id); /* создаем устройство */
            buffer = new CaptureBuffer(desciption, capture); /* создаем буфер */

            int waitHandleCount = BufferSeconds * NotifyPointsInSecond;
            BufferPositionNotify[] positions = new BufferPositionNotify[waitHandleCount];

            for (int i = 0; i < waitHandleCount; i++)
            {
                BufferPositionNotify position = new BufferPositionNotify();
                position.Offset = (i + 1) * bufferLength / positions.Length - 1;
                position.EventNotifyHandle = positionEventHandle.DangerousGetHandle();
                positions[i] = position;
            }

            notify = new Notify(buffer);
            notify.SetNotificationPositions(positions);

            terminated.Reset();
            thread = new Thread(new ThreadStart(ThreadLoop));
            thread.Name = "Sound capture";
            thread.Start();
        }

        /// <summary>
        ///  Постоянно получаем данные и отправляем их на обработку
        /// </summary>
        private void ThreadLoop() 
        {
            buffer.Start(true); /* пытаемся писать в буфер */
            try
            {
                int nextCapturePosition = 0;
                WaitHandle[] handles = new WaitHandle[] { terminated, positionEvent };
                while (WaitHandle.WaitAny(handles) > 0)
                {
                    int capturePosition, readPosition;
                    buffer.GetCurrentPosition(out capturePosition, out readPosition);

                    int lockSize = readPosition - nextCapturePosition;
                    if (lockSize < 0) lockSize += bufferLength;
                    if((lockSize & 1) != 0) lockSize--;

                    int itemsCount = lockSize >> 1;

                    short[] data = (short[])buffer.Read(nextCapturePosition, typeof(short), LockFlag.None, itemsCount);
                    Proc_Data(data);
                    nextCapturePosition = (nextCapturePosition + lockSize) % bufferLength;
                }
            }
            finally
            {
                /* останавливаем запись */
                buffer.Stop();
            }
        }

        /// <summary>
        ///  абстрактный метод
        /// </summary>
        /// <param name="data"></param>
        protected abstract void Proc_Data(short[] data);

        public void Stop()
        {
            if (isCapturing)
            {
                isCapturing = false; /* сбрасываем флаг */
                terminated.Set(); /* уведомляем, что событие произошло */
                thread.Join(); /* блокируем вызывающий поток, до завершения потока */

                /* высвобождаем ресурсы */
                notify.Dispose();
                buffer.Dispose();
                capture.Dispose();
            }
        }

        /// <summary>
        ///  высвобождаем ресурсы
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        ~Zahvat_Zvuka_Base()
        {
            Dispose(false);
        }

        /// <summary>
        ///  высвобождаем ресурсы
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposed) return;

            disposed = true;
            GC.SuppressFinalize(this);
            if (IsCapturing) Stop();
            positionEventHandle.Dispose();
            positionEvent.Close();
            terminated.Close();            
        }
    }
}
