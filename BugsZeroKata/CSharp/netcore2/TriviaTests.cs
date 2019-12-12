using Xunit;

namespace Trivia
{
    public class TriviaTests
    {
        [Fact]
        public void ShouldLandInPenaltyBoxOnWrongAnswer()
        {
            var game = new Game(new Player("bobby"), new Player("lapinte"));
            game.Roll(1);
            
            game.WrongAnswer();
            game.EndPlayerTurn();
            
            Assert.True(game.IsPlayerInPenaltyBox(0));
            
        }

        [Fact]
        public void ShouldGiveCoinsToThePlayerWhoAnsweredCorrectly()
        {
            var game = new Game(new Player("bobby"), new Player("lapinte"));
            game.Roll(1);
            
            game.WasCorrectlyAnswered();
            game.EndPlayerTurn();
            
            Assert.Equal(1, game.GetPlayerCoins(0));
            Assert.Equal(0, game.GetPlayerCoins(1));
        }

        [Fact]
        public void ShouldGiveCoinsToThePlayerWhoWasInPenaltyBoxAndAnsweredCorrectly()
        {
            var game = new Game(new Player("bobby"), new Player("lapinte"));
            
            game.Roll(1);
            game.WrongAnswer();
            game.EndPlayerTurn();
            
            game.Roll(1);
            game.WrongAnswer();
            game.EndPlayerTurn();

            game.Roll(1);
            game.WasCorrectlyAnswered();
            game.EndPlayerTurn();

            Assert.Equal(1, game.GetPlayerCoins(0));
            Assert.Equal(0, game.GetPlayerCoins(1));
        }
        
        [Fact]
        public void ShouldLeavePenaltyBoxOnOddRollAndAnsweredCorrectly()
        {
            
            Game game = new Game(new Player("bobby"), new Player("lapinte"));
            // current player : bobby - maybe we should have a way to keep track of who's playing right now ? /shrug
            game.Roll(1);
            game.WrongAnswer();
            game.EndPlayerTurn();
            
            // current player : lapinte
            game.Roll(1); // we don't care about this roll
            game.WasCorrectlyAnswered();
            game.EndPlayerTurn();

            // current player : bobby
            game.Roll(1);
            game.WasCorrectlyAnswered();
            game.EndPlayerTurn();
            
            Assert.False(game.IsPlayerInPenaltyBox(0));
        }
        
        [Fact]
        public void ShouldStayInPenaltyBoxOnOddRollAndAnsweredWrongly()
        {
            
            Game game = new Game(new Player("bobby"), new Player("lapinte"));
            // current player : bobby - maybe we should have a way to keep track of who's playing right now ? /shrug
            game.Roll(1);
            game.WrongAnswer();
            game.EndPlayerTurn();
            
            // current player : lapinte
            game.Roll(1); // we don't care about this roll
            game.WasCorrectlyAnswered();
            game.EndPlayerTurn();

            // current player : bobby
            game.Roll(1);
            game.WrongAnswer();
            game.EndPlayerTurn();
            
            Assert.True(game.IsPlayerInPenaltyBox(0));
        }
        
        [Fact]
        public void ShouldStayInPenaltyBoxOnEvenRoll()
        {
            
            Game game = new Game("bobby", "lapinte");
            // current player : bobby - maybe we should have a way to keep track of who's playing right now ? /shrug
            game.Roll(1);
            game.WrongAnswer();
            game.EndPlayerTurn();
            
            // current player : lapinte
            game.Roll(1); // we don't care about this roll
            game.WasCorrectlyAnswered();
            game.EndPlayerTurn();

            // current player : bobby
            game.Roll(2);
            Assert.True(game.IsPlayerInPenaltyBox(0));
        }
    }
}
