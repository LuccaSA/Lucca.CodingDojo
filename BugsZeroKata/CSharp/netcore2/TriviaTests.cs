using System;
using System.IO;
using System.Text;
using Xunit;
using Assent;
using Assent.Reporters;
using Assent.Reporters.DiffPrograms;

namespace Trivia
{
    public class TriviaTests
    {
        [Fact]
        public void RefactoringTests()
        {
            var output = new StringBuilder();
            Console.SetOut(new StringWriter(output));
 
            Game aGame = new Game(new Player("Chet"), new Player("Pat"), new Player("Sue"));
            Console.WriteLine(aGame.IsPlayable());

            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);

            aGame.WasCorrectlyAnswered();
            aGame.WrongAnswer();

            aGame.Roll(2);

            aGame.Roll(6);

            aGame.WrongAnswer();

            aGame.Roll(2);

            aGame.Roll(2);


            aGame.WrongAnswer();

            aGame.WasCorrectlyAnswered();
            aGame.Roll(1);
            aGame.WasCorrectlyAnswered();

            var configuration = BuildConfiguration();
            this.Assent(output.ToString(), configuration);
        }

        [Fact]
        public void ShouldLandInPenaltyBoxOnWrongAnswer()
        {
            
            Game game = new Game(new Player("bobby"), new Player("lapinte"));
            game.Roll(1);
            
            game.WrongAnswer();
            Assert.True(game.IsPlayerInPenaltyBox(0));
            
        }

        [Fact]
        public void ShouldGiveCoinsToThePlayerWhoAnsweredCorrectly()
        {
            var game = new Game(new Player("bobby"), new Player("lapinte"));
            game.Roll(1);
            
            game.WasCorrectlyAnswered();
            
            Assert.Equal(1, game.GetPlayerCoins(0));
            Assert.Equal(0, game.GetPlayerCoins(1));
        }

        [Fact]
        public void ShouldGiveCoinsToThePlayerWhoWasInPenaltyBoxAndAnsweredCorrectly()
        {
            var game = new Game(new Player("bobby"), new Player("lapinte"));
            
            game.Roll(1);
            game.WrongAnswer();
            
            game.Roll(1);
            game.WrongAnswer();

            game.Roll(1);
            game.WasCorrectlyAnswered();

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
            
            // current player : lapinte
            game.Roll(1); // we don't care about this roll
            game.WasCorrectlyAnswered();

            // current player : bobby
            game.Roll(1);
            game.WasCorrectlyAnswered();
            Assert.False(game.IsPlayerInPenaltyBox(0));
        }
        
        [Fact]
        public void ShouldStayInPenaltyBoxOnOddRollAndAnsweredWrongly()
        {
            
            Game game = new Game(new Player("bobby"), new Player("lapinte"));
            // current player : bobby - maybe we should have a way to keep track of who's playing right now ? /shrug
            game.Roll(1);
            game.WrongAnswer();
            
            // current player : lapinte
            game.Roll(1); // we don't care about this roll
            game.WasCorrectlyAnswered();

            // current player : bobby
            game.Roll(1);
            game.WrongAnswer();
            Assert.True(game.IsPlayerInPenaltyBox(0));
        }
        
        [Fact]
        public void ShouldStayInPenaltyBoxOnEvenRoll()
        {
            
            Game game = new Game("bobby", "lapinte");
            // current player : bobby - maybe we should have a way to keep track of who's playing right now ? /shrug
            game.Roll(1);
            game.WrongAnswer();
            
            // current player : lapinte
            game.Roll(1); // we don't care about this roll
            game.WasCorrectlyAnswered();

            // current player : bobby
            game.Roll(2);
            Assert.True(game.IsPlayerInPenaltyBox(0));
        }
        

        private static Configuration BuildConfiguration()
        {
            return 
                new Configuration()
                
            // Uncomment this block if an exception 
            // « Could not find a diff program to use »
            // is thrown and if you have VsCode installed.
            // Otherwise, use other DiffProgram with its full path
            // as parameter.
            // See  https://github.com/droyad/Assent/wiki/Reporting
//                    .UsingReporter(
//                        new DiffReporter(
//                            new []
//                            {
                                // For linux
//                                new VsCodeDiffProgram(new []
//                                {
//                                    "/usr/bin/code"
//                                })
                
                                // For Windows
//                                new VsCodeDiffProgram(), 
//                            }))
                ;
        }
    }
}
