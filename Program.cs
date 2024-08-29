using System.Collections.Generic;

namespace DVT_Elevator_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Elevator> elevators = new List<Elevator>
            {
                new Elevator(5, 4, ElevatorType.Standard),    // Standard elevator with capacity of 5 and weight limit of 4 people
                new Elevator(10, 9, ElevatorType.HighSpeed),  // High-speed elevator with capacity of 10 and weight limit of 8 people
                new Elevator(8, 7, ElevatorType.Freight)      // Freight elevator with capacity of 8 and weight limit of 6 people
            };

            ElevatorSystem elevatorSystem = new ElevatorSystem(elevators);
            elevatorSystem.Start();
        }
    }
}
