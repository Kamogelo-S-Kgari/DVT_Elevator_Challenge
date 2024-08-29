using System;
using System.Collections.Generic;

namespace DVT_Elevator_Challenge
{
    // Enum representing different types of elevators
    public enum ElevatorType { Standard, HighSpeed, Freight }

    // Enum representing the direction of elevator movement
    public enum Direction { Up, Down, Idle }

    // Class representing an Elevator in the system
    public class Elevator
    {
        // Properties to track the elevator's state
        public int CurrentFloor { get; private set; }            // Current floor the elevator is on
        public Direction MovementDirection { get; private set; } // Direction the elevator is moving in
        public bool IsMoving { get; private set; }               // Indicates if the elevator is currently moving
        public int PassengerCount { get; private set; }          // Number of passengers currently in the elevator
        public int Capacity { get; private set; }                // Maximum capacity of the elevator (in people)
        public int WeightLimit { get; private set; }             // Maximum weight limit of the elevator (expressed as a number of people)

        public ElevatorType Type { get; private set; }           // Type of the elevator (Standard, HighSpeed, Freight)

        private Queue<int> floorQueue;                           // Queue to hold the floors the elevator needs to visit

        // Constructor to initialize the elevator with capacity, weight limit, and type
        public Elevator(int capacity, int weightLimit, ElevatorType type = ElevatorType.Standard)
        {
            Capacity = capacity;
            WeightLimit = weightLimit;
            Type = type;
            CurrentFloor = 0;                  // Start at the ground floor (floor 0)
            MovementDirection = Direction.Idle; // Initially idle
            IsMoving = false;                   // Not moving initially
            PassengerCount = 0;                 // Start with no passengers
            floorQueue = new Queue<int>();      // Initialize the queue for floor requests
        }

        // Method to set the initial floor of the elevator, useful for testing or initialization
        public void SetInitialFloor(int floor)
        {
            CurrentFloor = floor;
        }

        // Method to add passengers and a destination floor to the elevator
        public void AddPassenger(int destinationFloor, int passengerCount)
        {
            // Check if adding the passengers would exceed the elevator's capacity or weight limit
            if (PassengerCount + passengerCount <= Math.Min(Capacity, WeightLimit))
            {
                floorQueue.Enqueue(destinationFloor);  // Add the destination floor to the queue
                PassengerCount += passengerCount;      // Increase the passenger count

                // If the elevator is idle, start moving to the next floor
                if (MovementDirection == Direction.Idle)
                    MoveToNextFloor();
            }
            else
            {
                // If the elevator cannot carry all the passengers due to weight limits, handle partial loading
                Console.WriteLine("Elevator cannot carry more passengers due to weight limits!");
                int availableSpace = Math.Min(Capacity, WeightLimit) - PassengerCount; // Calculate available space
                PassengerCount += availableSpace;    // Add as many passengers as possible
                floorQueue.Enqueue(destinationFloor); // Add the destination floor to the queue

                // Start moving to the next floor
                MoveToNextFloor();
            }
        }

        // Private method to move the elevator to the next floor in the queue
        private void MoveToNextFloor()
        {
            // Check if there are floors in the queue
            if (floorQueue.Count > 0)
            {
                IsMoving = true;  // Set the elevator to moving
                int nextFloor = floorQueue.Dequeue(); // Get the next floor to visit

                // Determine the direction of movement
                MovementDirection = nextFloor > CurrentFloor ? Direction.Up : Direction.Down;
                Console.WriteLine($"{Type} Elevator moving from floor {CurrentFloor} to floor {nextFloor}");

                // Move the elevator to the next floor
                CurrentFloor = nextFloor;

                // After reaching the destination, stop the elevator and set direction to idle
                IsMoving = false;
                MovementDirection = Direction.Idle;
                Console.WriteLine($"{Type} Elevator arrived at floor {CurrentFloor}.");
            }
        }

        // Method to display the current status of the elevator
        public void ShowStatus()
        {
            Console.WriteLine($"{Type} Elevator Status: Current Floor - {CurrentFloor}, " +
                              $"Direction - {MovementDirection}, " +
                              $"Passengers - {PassengerCount}/{Capacity}, " +
                              $"Weight Limit - {WeightLimit}, " +
                              $"Moving - {IsMoving}");
        }
    }
}
