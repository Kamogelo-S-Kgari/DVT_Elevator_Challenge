using System;
using System.Collections.Generic;

namespace DVT_Elevator_Challenge
{
    public class Building
    {
        private List<Elevator> elevators;

        public Building(List<Elevator> elevators)
        {
            this.elevators = elevators;
        }

        public void ShowElevatorsStatus()
        {
            foreach (var elevator in elevators)
            {
                elevator.ShowStatus();
            }
        }
        // Method to request the elevator
        public void RequestElevator(int floor, int passengerCount, ElevatorType type = ElevatorType.Standard)
        {
            Elevator nearestElevator = null;
            int shortestDistance = int.MaxValue;

            foreach (var elevator in elevators)
            {
                if (elevator.Type == type)
                {
                    int distance = Math.Abs(elevator.CurrentFloor - floor);
                    if (distance < shortestDistance && !elevator.IsMoving)
                    {
                        nearestElevator = elevator;
                        shortestDistance = distance;
                    }
                }
            }

            nearestElevator?.AddPassenger(floor, passengerCount);
        }
    }
}
