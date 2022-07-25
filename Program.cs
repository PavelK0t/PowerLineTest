using System;

namespace PowerLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var car = new Car("Car", 70, 0.5, 70, 4);
            Console.WriteLine($"Общий запас хода: {car.PowerReserve()} км");
            Console.WriteLine($"Запас хода с 30 литрами: {car.PowerReserve(30)} км");
            Console.WriteLine($"Запас хода с 30 литрами и 1 пассажиром: {car.PowerReserve(30, 1)} км");
            Console.WriteLine($"Запас хода с 30 литрами и 3 пассажирами: {car.PowerReserve(30, 3)} км");

            var truck = new Truck("Truck", 150, 1, 70, 750);
            Console.WriteLine($"Общий запас хода: {truck.PowerReserve()} км");
            Console.WriteLine($"Запас хода с 75 литрами: {truck.PowerReserve(75)} км");
            Console.WriteLine($"Запас хода с 75 литрами и 200 кг груза: {truck.PowerReserve(75, 200.0)} км");
            Console.WriteLine($"Запас хода с 75 литрами и 555 кг груза: {truck.PowerReserve(75, 555.0)} км");

        }
    }

    /// <summary>
    /// Базовый класс ТС
    /// </summary>
    public abstract class Vehicle
    {
        /// <summary>
        /// Тип ТС
        /// </summary>
        public string VehicleType { get; set; }
        /// <summary>
        /// Расход топлива
        /// </summary>
        public double FuelSpending { get; set; }
        /// <summary>
        /// Объем бака
        /// </summary>
        public double TankVolume { get; set; }
        /// <summary>
        /// Скорость
        /// </summary>
        public double Speed { get; set; }
        public Vehicle(string _type, double _tankVolume, double _fuelSpending, double _speed) 
        {
            VehicleType = _type;
            TankVolume = _tankVolume;
            FuelSpending = _fuelSpending;
            Speed = _speed;
        }

        /// <summary>
        /// Возвращает максимальный запас хода
        /// </summary>
        /// <returns></returns>
        public virtual double PowerReserve() { return TankVolume / FuelSpending; }

        /// <summary>
        /// Возвращает запас хода от текущего уровня топлива
        /// </summary>
        /// <param name="_gasVolume">Уровень топлива</param>
        /// <returns></returns>
        public virtual double PowerReserve(double _gasVolume) { return _gasVolume / FuelSpending; }

        /// <summary>
        /// Возвращает запас хода от текущего уровня топлива и количества пассажиров
        /// </summary>
        /// <param name="_gasVolume">Уровень топлива</param>
        /// <param name="_passengerCount">Количество пассажиров</param>
        /// <returns></returns>
        public virtual double PowerReserve(double _gasVolume, byte _passengerCount) { return -1; }

        /// <summary>
        /// Возвращает запас хода от текущего уровня топлива и массы груза
        /// </summary>
        /// <param name="_gasVolume">Уровень топлива</param>
        /// <param name="_cargoWeight">Масса груза</param>
        /// <returns></returns>
        public virtual double PowerReserve(double _gasVolume, double _cargoWeight) { return -1; }

        /// <summary>
        /// Возвращает время необходимое на преодоление пути
        /// </summary>
        /// <param name="_gasVolume">Уровень топлива</param>
        /// <param name="_distance">Расстояние</param>
        /// <returns></returns>
        public virtual double TravelTime(double _gasVolume, double _distance) 
        {
            var time = _distance / Speed;
            var needGas = _distance / FuelSpending;
            if (_gasVolume >= needGas)
                return time;
            throw new Exception("Not enough fuel");
        }

    }

    /// <summary>
    /// Класс для легкового автомобиля
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// Максималоьное количество пассажиров
        /// </summary>
        private byte MaxPassengers { get; set; }

        public Car(string _type, double _tankVolume, double _fuelSpending, double _speed, byte _maxPassengers) : base(_type, _tankVolume, _fuelSpending, _speed)
        {
            MaxPassengers = _maxPassengers;
        }
        public override double PowerReserve(double _gasVolume, byte _passengersCount)
        {
            if (_passengersCount <= MaxPassengers)
            {
                return _gasVolume / (FuelSpending * (1 + 0.06 * _passengersCount));
            }
            throw new Exception("Wrong parameters");
        }
    }

    /// <summary>
    /// Класс для грузового автомобиля
    /// </summary>
    public class Truck : Vehicle
    {
        /// <summary>
        /// Грузоподъемность
        /// </summary>
        private double Carrying { get; set; }
        public Truck(string _type, double _tankVolume, double _fuelSpending, double _speed, double _carrying) : base(_type, _tankVolume, _fuelSpending, _speed)
        {
            Carrying = _carrying;
        }
        public override double PowerReserve(double _gasVolume, double _cargoWeight)
        {
            if (_cargoWeight <= Carrying)
            {
                return _gasVolume / (FuelSpending * (1 + 0.04 * _cargoWeight / 200));
            }
            throw new Exception("Wrong parameters");
        }
    }

    /// <summary>
    /// Класс для спортивного автомобиля
    /// </summary>
    public class SportCar : Vehicle
    {
        public SportCar(string _type, double _tankVolume, double _fuelSpending, double _speed) : base(_type, _tankVolume, _fuelSpending, _speed) { }
    }

}
