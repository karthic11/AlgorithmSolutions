using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.ParkingLot
{
   public class ParkingLevel
    {
       public int FloorNo { get; set; }
       public static int SpotsPerRow { get; set; }
       private List<ParkingSpot> _spots = new List<ParkingSpot>();
       public int AvailableSpots { get; set; }

       public ParkingLevel(int floorno, int numberofspots)
       {
           this.FloorNo = floorno;
           this._spots.Capacity = numberofspots;
       }

       
       public bool ParkVehicleBySpotNumber(int spotnumber, Vehicle vehicle)
       {
           return true;
       }

       //Find the spot to park the vehicle and return the index of the parking spot..return -1 on failure
       private int FindAvailableSpots(Vehicle vehicle)
       {
           return -1;
       }

       //Find a place to park the given vehicle and return false if failed
       public bool ParkVehicle(Vehicle vehicle)
       {
           //For the given vehicle type apply the logic and find the avalible spots

           int i =  FindAvailableSpots(vehicle);
           return true;
       }

       //Free the spot
       public void SpotFreed()
       {
           AvailableSpots++;
       }

    }
}
