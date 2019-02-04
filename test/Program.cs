using System;
using System.Collections;

namespace test
{
    class PlayingCardSystem
    {
        protected static string[] suits = { "Diamonds", "Hearts", "Clubs", "Spades" };
        protected static string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        public struct Card
        {
            public string suit;
            public int valueIndex; // index in the array values
        }
        protected static ArrayList deck = new ArrayList();
        public struct Player
        {
            public ArrayList hand;
        }
        protected static ArrayList players = new ArrayList();

        // adds n players to the game
        protected static void AddPlayers(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Player p = new Player();
                p.hand = new ArrayList();
                players.Add(p);
            }
        }

        // Adds 13*4 cards to the deck
        protected static void InitDeck()
        {
            foreach (string s in suits)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    Card c = new Card();
                    c.suit = s;
                    c.valueIndex = i;
                    deck.Add(c);
                }
            }
        }

        // Shuffles the deck with linear running time
        public static void ShuffleDeck()
        {
            ArrayList shuffledDeck = new ArrayList();
            Random random = new Random();
            int counter = deck.Count;
            for (int i = 0; i < deck.Count; i++)
            {
                // Add a card at a random index to the new ArrayList
                int index = random.Next(0, counter);
                shuffledDeck.Add(deck[index]);
                counter--;
                // Swap the added Card to the end of the ArrayList to avoid adding it again
                Card tmp = (Card)deck[index];
                deck[index] = deck[counter];
                deck[counter] = tmp;
            }
            deck = shuffledDeck;
        }

        // Gives each player one card from the deck
        // Assuming that there are enough cards in the deck for each player
        public static void Deal1CardToEachPlayer()
        {
            foreach (Player p in players)
            {
                p.hand.Add(deck[deck.Count - 1]);
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }

    class HighCard : PlayingCardSystem
    {
        // Initializes the deck and the players
        static void InitGame()
        {
            InitDeck();
            ShuffleDeck();
            AddPlayers(2);
        }

        // Finds the winner and outputs to the screen(Console)
        // Assuming that the suit does not matter in determining the winner, only the value does
        static void HandleResult()
        {
            Card card1 = (Card)((Player)players[0]).hand[0];
            Card card2 = (Card)((Player)players[1]).hand[0];
            // Display each player's card
            Console.WriteLine("\nPlayer 1: " + values[card1.valueIndex] + " of " + card1.suit);
            Console.WriteLine("Player 2: " + values[card2.valueIndex] + " of " + card2.suit);
            // Display the winner
            if (card1.valueIndex > card2.valueIndex)
            {
                Console.WriteLine("Player 1 wins!");
            }
            else if (card1.valueIndex < card2.valueIndex)
            {
                Console.WriteLine("Player 2 wins!");
            }
            else
            {
                Console.WriteLine("Tie.");
            }
        }

        // Plays one round of the game
        static void Play1Round()
        {
            InitGame();
            Deal1CardToEachPlayer();
            HandleResult();
        }

        // Resets the game to the state before initialized
        static void Resetgame()
        {
            deck.Clear();
            players.Clear();
        }

        static void Main(string[] args)
        {
            bool keepPlaying = true;
            while (keepPlaying)
            {
                Play1Round();
                Resetgame();
                Console.WriteLine("Press SPACE to play again. Press anything else to exit.");
                if(Console.ReadKey().Key != ConsoleKey.Spacebar)
                {
                    keepPlaying = false;
                }
            }
        }
    }
}
