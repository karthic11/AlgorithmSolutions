using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.ParkingLot
{
    class ReadMe
    {
    }

    //Parking Lot:
    //Step 1: Clarification from the Questions:
    //Ask the interview the following questions
    //What kind of vehicle the lot should have? like motorcycle, car, bus etc
    //Does the parking lot have multiple levels?...

    //Assumptions:
    //Parking lot has multiple levels and each level has multiple rows
    //Parking lot has vehicles like motorcycle, cars, truck, bus etc
    //Parking lot has motorcycle spots, compact spots, large spots
    //Bus can park in 5 large spots, car can park in either compact or large spots, truck in large spot and mc in mc spot

    //Step 2: Identify the objects and establish the relationship
    //Step 3: Identify the prop and methods for each object

    //enum VehicleSize { MotorCycle, Compact, Large }

    //abstract class Vehicle  
    //prop like licensenumber, VehicleSize, int spotsneeded, int spotnumber, list<ParkingSpot>
    //methods like abstract Boolean CanFitInSpot(), void ParkVehicleInSport(ParkingSpot s), void ClearSpots()

    //class Car : Vehicle ...override bool CanFitInSpot(ParkingSpot s)
    //class MotorCycle : Vehicle.. override bool CanFitInSpot(ParkingSpot s)
    //class Bus : Vehicle...override bool CanFitInSpot(ParkingSpot s)

    //abstract class ParkingSpot...
    //props ParkingLevel level, int rowno, int spotnumber, VehicleSize xVehicleSize, Vehicle _Vehicle
    //methods bool IsAvailable(), bool CanFitVehicle(Vehicle vehicle), Boolean park(Vehicle v), void RemoveVehicle()

    //class CompactParkingSpot : ParkingSpot
    //class LargeParkingSpot : ParkingSpot
    //class MotorCycleParkingSpot : ParkingSpot

    //class ParkingLevel
    //props List<ParkingSpot> _spots, int floorno, int numberofspots, int AvailableSpots
    //methods refer to the class

    //class ParkingLot
    //props: List<ParkingLevel> _levels, static int NoofLevels = 5;
    //methods: bool ParkVehicle(Vehicle vehicle)



    


}
