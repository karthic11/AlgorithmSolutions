using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels
{
    //Imagine about Card Deck maching in the casino that supplies cards to the players
    public class StandardCardDeck<T> where T : Card
    {
        private  List<T> cards = null;
        //Dealt index is the no of the cards that is distributed
        private int dealtindex = 0;

        public StandardCardDeck()
        {
            cards = new List<T>();
        }

        public void ShuffleCards()
        {
            //The machine does the physical job to shuffle the cards
        }

        public void SetDeckofCards()
        {
            //The machine sets deck of cards
        }

        public T DealCard()
        {
            
            //The machine supplies/gives one card to the dealers
           // dealtindex++;
            return cards[0];

        }

        public List<T> DealHand(int numberofcards)
        {
            return  new List<T>();
        }


    }
}
