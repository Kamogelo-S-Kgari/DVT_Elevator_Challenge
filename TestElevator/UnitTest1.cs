using System;
using System.Collections.Generic;
using Xunit;

namespace DVT_Elevator_Challenge.Tests
{
    public class ElevatorSystemTests
    {
        [Fact]
        public void HandleElevatorRequest_ShouldDispatchElevatorToCorrectFloor()
        {
            // Arrange
            var elevators = new List<Elevator>
            {
                new Elevator(5,4, ElevatorType.Standard),
                new Elevator(10,9, ElevatorType.HighSpeed),
                new Elevator(8,7, ElevatorType.Freight)
            };
            var elevatorSystem = new ElevatorSystem(elevators);

            // Act
            elevatorSystem.HandleElevatorRequest(5, 3, ElevatorType.Standard);

            // Assert
            Assert.Equal(5, elevators[0].CurrentFloor);  // Standard elevator should be on floor 5
            Assert.Equal(3, elevators[0].PassengerCount); // Passenger count should be 3
        }

        [Fact]
        public void HandleElevatorRequest_ShouldRespectElevatorType()
        {
            // Arrange
            var elevators = new List<Elevator>
            {
                new Elevator(5,4, ElevatorType.Standard),
                new Elevator(10,9, ElevatorType.HighSpeed),
                new Elevator(8,7, ElevatorType.Freight)
            };
            var elevatorSystem = new ElevatorSystem(elevators);

            // Act
            elevatorSystem.HandleElevatorRequest(7, 2, ElevatorType.HighSpeed);

            // Assert
            Assert.Equal(7, elevators[1].CurrentFloor);  // High-speed elevator should be on floor 7
            Assert.Equal(2, elevators[1].PassengerCount); // Passenger count should be 2
        }

        [Fact]
        public void HandleElevatorRequest_ShouldNotOverloadElevator()
        {
            // Arrange
            var elevators = new List<Elevator>
            {
                new Elevator(5,4, ElevatorType.Standard)
            };
            var elevatorSystem = new ElevatorSystem(elevators);
            
            // Act
            elevatorSystem.HandleElevatorRequest(2, 6, ElevatorType.Standard); // Exceeds capacity

            // Assert
            Assert.Equal(4, elevators[0].PassengerCount);  // Passenger count should be at max capacity (4)
            Assert.Equal(2, elevators[0].CurrentFloor);    // Elevator should have moved to floor 2
        }

        [Fact]
        public void HandleElevatorRequest_ShouldChooseNearestElevator()
        {
            // Arrange
            var elevators = new List<Elevator>
    {
        new Elevator(5,4, ElevatorType.Standard),
        new Elevator(5,4, ElevatorType.Standard)
    };
            elevators[0].SetInitialFloor(1);
            elevators[1].SetInitialFloor(10);

            var elevatorSystem = new ElevatorSystem(elevators);

            // Act
            elevatorSystem.HandleElevatorRequest(3, 2, ElevatorType.Standard);

            // Assert
            Assert.Equal(3, elevators[0].CurrentFloor);  // Nearest elevator (floor 1) should move to floor 3
        }

    }
}
