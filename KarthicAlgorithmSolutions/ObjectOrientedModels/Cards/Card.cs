using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels
{

    //Think about playing card. Each card has a face number (1 to 13) and Card Type/ Card Suit (Diamond, Clubs, Hearts, Spade)
    //Each card has a value and the value depends on the game eg AceGame the value = facevalue, blackjack - anything greater than 10 is 10
    //Assumption: we are playing with one set of cards so each card is available to only one person/hand..so each card has the status property which has avaiable and not available

    public enum CardSuit
    {
        //Club, Diamond, Hearts, Spade  or with arbitary value
        Club = 0,
        Diamnond =1,
        Spade =2,
        Hearts = 3

    }

   //Abstract class may contain both abstract method and concrete method
    public abstract class Card
    {

        public CardSuit cardtype;
        public int facevalue;
        public bool CardAvailable { get; set; }

        public Card(int facevalue, CardSuit cardtype)
        {
            this.cardtype = cardtype;
            this.facevalue = facevalue;

        }

         //Value is a abstract method bcoz it depends on the game so the implementation willl be in the actual game
        public abstract int value();

        //checks if the card is available to give out to someone\

        public bool IsAvailable()
        {
            return CardAvailable;
        }

        public void SetAvailable()
        {
            CardAvailable = true;
        }

        public void SetUnAvailable()
        {
            CardAvailable = false;
        }

    }
}
