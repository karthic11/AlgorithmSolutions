using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels
{

    //black jack game
    //face card above 10 is 10
    //ace is 1 or 11


    public class BlackJackHand<T> : Hand<T> where T: Card
    {
        public override int Score()
        {

            //In case black jack..we have multiple score based on the value of ace. so we need to calculate the best score
            List<int> scores = GetPossibleScores();
            //after getting all possible scores ..calculate the best score

            int maxBest = Int32.MinValue;
            int minBest = Int32.MaxValue;

            foreach (int score in scores )
            {

                if (score > 21 && score < minBest)
                {

                   //if the scores are greater than 21..pick the min of those
                    minBest = score;
                }
                else if (score <= 21 && score > maxBest)
                {
                    //if the scores are lesser than 21//pick the max of those scores
                    maxBest = score;

                }
            }

            //tricky part..If the score is never lesser than 21 then pick the  minBest else maxbest
            return (maxBest == Int32.MinValue) ? minBest : maxBest;


        }

        public List<int> GetPossibleScores()
        {
            //base on the ace value either 1 0r 11..we need to have the combination of the scores

            return  new List<int>();
        }

        public bool IsBusted()
        {
            return (Score() > 21);
        }

        public bool Is21()
        {
            return (Score() == 21);

        }
        //black jack eg 10, 11(clubs) or any 10, clubs 11
        public bool IsBlackJack()
        {
            //check for card count 11 and check for clubs and check for cards
            if (this.Cards.Count == 2 && this.Score() == 20)
            {
                foreach (var card in this.Cards)
                {
                     //since score is 20..both will be 10 or more
                    //if either one facevalue is 11 and cardsuit is clubs
                    if (card.facevalue == 11 && card.cardtype == CardSuit.Club)
                    {

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
