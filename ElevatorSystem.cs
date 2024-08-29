using System;
using System.Collections.Generic;

namespace DVT_Elevator_Challenge
{
    public class ElevatorSystem
    {
        private Building building;

        public ElevatorSystem(List<Elevator> elevators)
        {
            building = new Building(elevators);
        }

        // Refactored method to handle an elevator request programmatically
        public void HandleElevatorRequest(int floor, int passengerCount, ElevatorType type)
        {
            building.RequestElevator(floor, passengerCount, type);
        }

        // Original Start method for console interaction (can be tested by simulating user inputs)
        public void Start()
        {
            while (true)
            {
                Console.Clear();
                building.ShowElevatorsStatus();

                Console.WriteLine("\nEnter the floor number to call an elevator (or 'q' to quit): ");
                var floorInput = Console.ReadLine();

                if (floorInput.ToLower() == "q")
                    break;
               

                if (!int.TryParse(floorInput, out int floor))
                {
                    Console.WriteLine("Invalid input. Please enter a valid floor number.");
                    continue;
                }

                Console.WriteLine("Enter the number of passengers waiting: ");
                var passengerInput = Console.ReadLine();

                if (!int.TryParse(passengerInput, out int passengerCount) || passengerCount <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number of passengers.");
                    continue;
                }

                Console.WriteLine("Choose Elevator Type (1 - Standard, 2 - HighSpeed, 3 - Freight): ");
                var typeInput = Console.ReadLine();

                ElevatorType type = typeInput switch
                {
                    "2" => ElevatorType.HighSpeed,
                    "3" => ElevatorType.Freight,
                    _ => ElevatorType.Standard,
                };

                HandleElevatorRequest(floor, passengerCount, type);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
