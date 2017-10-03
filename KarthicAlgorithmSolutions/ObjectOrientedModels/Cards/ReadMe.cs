using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.Cards
{
    public class ReadMe
    {


    }

    //Cards:
    //Step 1: Clarification from the Questions:
    //Ask the interview what she means bu generic cards...make sure they mean the standard deck of cards..52 cards

    //Step 2: Identify the objects and establish the relationship
    //Step 3: Identify the prop and methods for each object

    //enum CardSuit { Diamond, Clubs, Hearts, Spade }
    //abstract class Cards  prop like facevalue, cardsuit, abstract int value
    //class DeckofCards<T> where T : Cards...Props like list<T> cards, int dealtindex... Methods like shuffle(), DisperseCard/Deal(), DealHand()
    //class Hand<T> where T: Cards..Prop like List<T>..Methods: AddCard(), RemoveCard(), CardsCount(), Score()

    //Game Logic
    //Eg: Black Jack
    //class BlackJackCards : Cards ..Implement value for blackjact based on black jack rule..Method IsAce(), IsFaceCard(), int Minvalue(), int MaxValue()
    //class BlackJackHand<T>  : Hand<T> where T: Cards...Methods: GetScore(), GetPossibleScore(), IsBusted(), Is21(), IsBlackJack()



   
}
