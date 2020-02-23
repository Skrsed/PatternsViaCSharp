using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public interface IClonable<T>
    {
        T ShallowCopy();

        T DeepCopy();
    }
    class Program
    {
        static void Main(string[] args)
        {
            var facade = new Facade();
            var result = facade.CalculateDistanceFromFileInput("file1");
            Console.WriteLine($"supposed distance is {result} km");
            Console.ReadKey();
        }
    }
    class Facade
    {
        private Convector convector = new Convector();
        private DistanceCalculator dictanceCalculator = new DistanceCalculator();

        public double CalculateDistanceFromFileInput(string file)
        {
            var reader = new FileReader();
            var data = reader.ReadValues(file);
            var dataCopy = data.DeepCopy();

            if (data.Speed.Type == SpeedType.mph)
            {
                dataCopy.Speed.Type = SpeedType.kmh;
                dataCopy.Speed.Value = convector.MphToKmh(dataCopy.Speed.Value);
            }
            return dictanceCalculator.Distance(dataCopy.Speed.Value, data.Time);
        }

    }
    class Convector
    {
        const double coef = 1.609344;
        public double MphToKmh(double speed)
        {
            return speed * coef;
        }
    }
    class FileReader{
        public File ReadValues(string file)
        {
            if(file == "file1")
                return new File(new Speed(SpeedType.mph,60),2);
            else return new File(new Speed(SpeedType.kmh, 100), 1);
        }
    }
    
    class DistanceCalculator
    {
        public double Distance(double speed, double time)
        {
            return speed * time;
        }
    }
    class File:IClonable<File>
    {

        public File(Speed speed, double time)
        {
            Speed = speed;
            Time = time;
        }
        public Speed Speed { get; set; }
        public double Time { get; set; }

        public File DeepCopy()
        {
            File other = (File)this.MemberwiseClone();
            other.Speed = new Speed(Speed.Type, Speed.Value);
            other.Time = Time;
            return other;
        }

        public File ShallowCopy()
        {
            return (File)MemberwiseClone();
        }
    }
    class Speed
    {
        public Speed(SpeedType type, double value)
        {
            Type = type;
            Value = value;
        }
        public SpeedType Type { get; set; }
        public double Value { get; set; }
    }
    enum SpeedType
    {
        mph,
        kmh
    }
}
