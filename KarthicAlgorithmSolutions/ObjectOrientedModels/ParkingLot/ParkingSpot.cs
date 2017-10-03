using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.ParkingLot
{

    //we can have abstract class ParkingSpot and CompactSpot : ParkingSpot etc
    public class ParkingSpot
    {

        private Vehicle _Vehicle { get; set; }    
        private VehicleSize _vehicleSize { get; set; }
        private ParkingLevel _level = null;

        private int _rowno;
        private int _spotnumber;
   

        public ParkingSpot(ParkingLevel level, int rowno, int spotnumber, VehicleSize xVehicleSize)
        {
            _level = level;
            _rowno = rowno;
            _spotnumber = spotnumber;
            _vehicleSize = xVehicleSize;

        }

        public bool IsAvailable()
        {

            return true;
        }

        public bool CanFitVehicle(Vehicle vehicle)
        {
            return true;

        }

        //park vehicle in the spot
        public Boolean park(Vehicle v)
        {
            return true;
        }

       //Remove vehicle from the spot and notify level that new spot is available
        public void RemoveVehicle()
        {
            
        }

    }
}
