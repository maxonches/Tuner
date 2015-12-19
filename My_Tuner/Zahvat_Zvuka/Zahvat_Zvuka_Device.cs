using System;
using System.Collections.Generic;
using Microsoft.DirectX.DirectSound;

namespace Zahvat_Zvuka
{
    /// <summary>
    /// класс звуковое устройство
    /// </summary>
    public class Zahvat_Zvuka_Device
    {
        Guid id; /*  глобальный уникальный идентификатор */
        string name;

        /// <summary>
        /// свойство - дефолтные найтройки или нет
        /// </summary>
        public bool IsDefault
        {
            get /* если поле id пустое(состоит из нулей) - то метод вернет True */
            {
                return id == Guid.Empty;
            }
        }

        /// <summary>
        /// свойство
        /// </summary>
        public string Name 
        {
            get /* возвращаем данные из поля Name */
            {
                return name;
            }

        }

        /// <summary>
        /// свойство
        /// </summary>
        internal Guid Id 
        {
            get /* возвращает уникальный идентификатор */
            {
                return id;
            }
        }


         /// <summary>
        /// конструктор internal определяет доступность члена во всех файлах сборки и его недоступность за пределами сборки
         /// </summary>
         /// <param name="id"></param>
         /// <param name="name"></param>
        internal Zahvat_Zvuka_Device(Guid id, string name)
        {
            /* начальные данные */
            this.id = id;
            this.name = name;
        }

        /// <summary>
        ///  метод возвращает массив звуковых устройств
        /// </summary>
        /// <returns></returns>
        public static Zahvat_Zvuka_Device[] GetDevices()
        {
            /* CaptureDevicesCollection -  Представляет коллекцию DeviceInformation для каждого доступного устройства захвата.*/
            CaptureDevicesCollection captureDevices = new CaptureDevicesCollection();
            /* список обьектов Zahvat_Zvuka_Device */
            List<Zahvat_Zvuka_Device> devices = new List<Zahvat_Zvuka_Device>();
            /* в цикле получаем DeviceInformation содержищиеся в captureDevices */
            foreach (DeviceInformation captureDevice in captureDevices)
            {   /* добавляем девайс */
                devices.Add(new Zahvat_Zvuka_Device(captureDevice.DriverGuid, captureDevice.Description));
            }
            return devices.ToArray();
        }
       
        /// <summary>
        /// метод возвращает дефолтный аудио девайс
         /// </summary>
        /// <returns></returns>
       public static Zahvat_Zvuka_Device GetDefaultDevice()
        {
            /* CaptureDevicesCollection - Представляет коллекцию DeviceInformation для каждого доступного устройства захвата.*/
            CaptureDevicesCollection captureDevices = new CaptureDevicesCollection();
            /* Создаем обьект Zahvat_Zvuka_Device */
            Zahvat_Zvuka_Device device = null;

            /* в цикле получаем DeviceInformation содержищиеся в captureDevices
             DeviceInformation - информация о девайсе */
            foreach (DeviceInformation captureDevice in captureDevices)
            {
                /* если глобальный уникальный идентификатор для данного девайса определен */
                if(captureDevice.DriverGuid == Guid.Empty)
                {
                    /* создаем новое звуковое устройство */
                    device = new Zahvat_Zvuka_Device(captureDevice.DriverGuid, captureDevice.Description);
                    /* выходим из цикла */
                    break;
                }
            }
            return device;
        }
    }
}
