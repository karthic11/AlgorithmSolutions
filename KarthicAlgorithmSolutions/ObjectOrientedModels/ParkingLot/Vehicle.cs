using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.ParkingLot
{


    public enum VehicleSize
    {
        MotorCycle, Compact, Large
    }

    public abstract class Vehicle
    {
        public string LicenseNumber { get; set; }
        public VehicleSize VehicleSize { get; set; }
        public int SpotsNeeded { get; set; }
        List<ParkingSpot> spots = new List<ParkingSpot>();

        //Park the Vehicle
        public void ParkVehicleInSport(ParkingSpot s)
        {
            spots.Add(s);
        }

        //clear the vehicle and notify spot about that
        public void ClearSpots()
        {
            foreach (var parkingSpot in spots)
            {
                //clear the spot
            }
        }


        //abstract method bcoz changes for each vehicle type.
        //This check whether spot is available and the spot is big enought to fit the vehicle
        public abstract Boolean CanFitInSpot(ParkingSpot s);

    }



    public class Car : Vehicle
    {
        public Car()
        {
            this.SpotsNeeded = 1;
            this.VehicleSize = VehicleSize.Compact;

        }
        public override bool CanFitInSpot(ParkingSpot s)
        {
            throw new NotImplementedException();
        }
    }

    public class MotorCycle : Vehicle
    {
        public MotorCycle()
        {
            this.SpotsNeeded = 1;
            this.VehicleSize = VehicleSize.MotorCycle;
        }

        public override bool CanFitInSpot(ParkingSpot s)
        {
            throw new NotImplementedException();
        }
    }


    public class Bus : Vehicle
    {
        public Bus()
        {
            this.SpotsNeeded = 5;
            this.VehicleSize = VehicleSize.Large;
        }

        public override bool CanFitInSpot(ParkingSpot s)
        {
            throw new NotImplementedException();
        }
    }


    public class Truck : Vehicle
    {
        public Truck()
        {
            this.SpotsNeeded = 2;
            this.VehicleSize = VehicleSize.Large;
        }

        public override bool CanFitInSpot(ParkingSpot s)
        {
            throw new NotImplementedException();
        }
    }
}
