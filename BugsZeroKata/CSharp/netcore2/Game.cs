using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Trivia
{
    public class Game
    {
        private const int PlacesPerCategory = 3;
        private const int CoinsToWin = 6;
        private const int QuestionCount = 50;
        List<Player> players = new List<Player>();

        
        private List<Category> categories = new List<Category>();

        private Category popCategory = new Category("Pop");
        private Category rockCategory = new Category("Rock");
        private Category scienceCategory = new Category("Science");
        private Category sportsCategory = new Category("Sports");
        
        private Deck popDeck;
        private Deck rockDeck;
        private Deck scienceDeck;
        private Deck sportsDeck;

        int[] places = new int[6];
        int[] purses = new int[6];

        bool[] inPenaltyBox = new bool[6];

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game(Player player1, Player player2)
        {
            Add(player1);
            Add(player2);
            categories.AddRange(new []{popCategory,scienceCategory,sportsCategory,rockCategory});
            
            LinkedList<string> popQuestions = new LinkedList<string>();
            LinkedList<string> scienceQuestions = new LinkedList<string>();
            LinkedList<string> sportsQuestions = new LinkedList<string>();
            LinkedList<string> rockQuestions = new LinkedList<string>();
            
            for (int i = 0; i < QuestionCount; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
            
            popDeck = new Deck(popCategory, popQuestions);
            rockDeck = new Deck(rockCategory, rockQuestions);
            scienceDeck = new Deck(scienceCategory, scienceQuestions);
            sportsDeck = new Deck(sportsCategory, sportsQuestions);
        }

        public Game(Player player1, Player player2, Player player3)
            : this(player1, player2)
        {
            Add(player3);
        }

        public Game(Player player1, Player player2, Player player3, Player player4)
            : this(player1, player2, player3)
        {
            Add(player4);
        }

        public Game(Player player1, Player player2, Player player3, Player player4, Player player5)
            : this(player1, player2, player3, player4)
        {
            Add(player5);
        }

        public Game(Player player1, Player player2, Player player3, Player player4, Player player5, Player player6)
            : this(player1, player2, player3, player4, player5)
        {
            Add(player6);
        }

        private int PlacesCount => categories.Count * PlacesPerCategory;
        
        public String CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        private bool Add(Player player)
        {
            players.Add(player);
            places[HowManyPlayers()] = 0;
            purses[HowManyPlayers()] = 0;
            inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(player + " was Added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public bool IsPlayerInPenaltyBox(int index)
        {
            return inPenaltyBox[index];
        }
        
        public int HowManyPlayers()
        {
            return players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (IsOdd(roll))
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    places[currentPlayer] = places[currentPlayer] + roll;
                    if (places[currentPlayer] >= PlacesCount)
                    {
                        places[currentPlayer] = places[currentPlayer] - PlacesCount;
                    }

                    Console.WriteLine(players[currentPlayer]
                            + "'s new location is "
                            + places[currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                places[currentPlayer] = places[currentPlayer] + roll;
                if (places[currentPlayer] >= PlacesCount)
                {
                    places[currentPlayer] = places[currentPlayer] - PlacesCount;
                }

                Console.WriteLine(players[currentPlayer]
                        + "'s new location is "
                        + places[currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }

        }

        private static bool IsOdd(int roll)
        {
            return roll % 2 != 0;
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == popDeck.Category)
            {
                Console.WriteLine(popDeck.Pick());
            }
            if (CurrentCategory() == scienceCategory)
            {
                Console.WriteLine(scienceDeck.Pick());
            }
            if (CurrentCategory() == sportsCategory)
            {
                Console.WriteLine(sportsDeck.Pick());
            }
            if (CurrentCategory() == rockDeck.Category)
            {
                Console.WriteLine(rockDeck.Pick());
            }
        }


        private Category CurrentCategory()
        {
            var index = places[currentPlayer] % categories.Count;
            return categories[index];
        }

        public bool WasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    inPenaltyBox[currentPlayer] = false; 
                    
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    return DidPlayerWin();
                    
                }
                else
                {
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was correct!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                return DidPlayerWin();

            }
        }

        public void EndPlayerTurn()
        {
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;
            
            return true;
        }


        private bool DidPlayerWin()
        {
            return purses[currentPlayer] == CoinsToWin;
        }

        public int GetPlayerCoins(int index) => 
            purses[index];
    }

}
