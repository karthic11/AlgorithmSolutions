using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels
{
    //In playing card, hands contain a set of cards
    //There should be add and remove method 
    //There should be overall score for the hand....not sure :)
    //There should be the size prop to give no of card in hand
    public class Hand<T> where T: Card
    {



        public List<T> Cards = null;

        public Hand()
        {
            Cards = new List<T>();
        }

        public int CardsCount()
        {

            return Cards.Count;
        }
      
        public void AddCard(T c)
        {
            Cards.Add(c);
    

        }

        public void RemoveCard(T c)
        {
            //

        }

        public int Score()
        {
            int score = 0;

            foreach (var card in Cards)
            {
                score = score + ((T)card).value();

            }

            return score;
        }


    }
}
