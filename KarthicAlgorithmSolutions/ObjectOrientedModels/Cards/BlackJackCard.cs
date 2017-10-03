using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels
{
    //Black Jack Card inherits from card class...
    public class BlackJackCard : Card
    {
        //pass the values to the base constructor
        public BlackJackCard(int facenumber, CardSuit cardtype) : base(facenumber, cardtype)
        {
        }
          
        //Implemented the abstract method value which came from the abstract card class
        public override int value()
        {
            if (IsAce())
            {
                return 1;
            }
            else if (IsFaceCard())
            {
                return 10; //face card value is 10 in black jack
            }
            else
            {
                return this.facevalue;
            }
        }


        public int minvalue()
        {
            if (IsAce())
            {
                return 1;
            }
            else
            {
               return value();
            }
        }

        public int maxvalue()
        {
            if (IsAce())
            {
                return 11;

            }
            else
            {
                return value();
            }
        }

        public bool IsAce()
        {
           //we can access base class prop
            return (this.facevalue == 1);
        }

        public bool IsFaceCard()
        {
            return (this.facevalue >= 11 && this.facevalue <= 13);
        }
    }
}
