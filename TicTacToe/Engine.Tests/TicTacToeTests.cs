using NSubstitute;
using System.Data.Common;
using System.Numerics;

namespace Engine.Tests
{
    public class TicTacToeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Start_GivenNoParameters_BoardIsEmpty()
        {
            // Arrange
            var game = new TicTacToe();

            // Act
            game.Start();

            // Assert
            Assert.NotNull(game, "Is Null");
            Assert.IsTrue(game.Board.IsEmpty);
        }

        [Test]
        public void Play_GivenValidPosition_ChangesValueOnBoard()
        {
            // Arrange
            var game = new TicTacToe();
            game.Start();

            // Act
            game.Play(0, 0, true);

            // Assert
            Assert.NotNull(game, "Is Null");
            Assert.IsFalse(game.Board.IsEmpty);            
        }

        [Test]
        public void Play_GivenInvalidPosition_ThrowsException()
        {
            // Arrange
            var game = new TicTacToe();
            game.Start();

            // Act
            Assert.Throws<ArgumentException>(() => game.Play(-1, -1, true));            
        }

        [Test]
        public void Play_GivenValidPosition_SetNextPlayer()
        {
            // Arrange
            var game = new TicTacToe();
            game.Start();

            // Act
            game.Play(0, 0, true);

            // Assert
            Assert.IsFalse(game.NextPlayer);
        }

        [Test]
        public void Play_GivenValidPositionByIncorrectPlayer_ThrowsException()
        {
            // Arrange
            var game = new TicTacToe();
            game.Start();
            game.Play(0, 0, true);

            // Act
            Assert.Throws<ArgumentException>(() => game.Play(0, 1, true));
        }

        [Test]
        public void Play_GivenUsedPosition_ThrowsException()
        {
            // Arrange
            var board = Substitute.For<IBoard>();
            board.GetValue(0, 0).Returns(true);
            var game = new TicTacToe();
            game.Board = board;

            game.Start();            

            // Act
            Assert.Throws<ArgumentException>(() => game.Play(0, 0, false));
        }

        [Test]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        public void Play_GivenValidTurnAndExistingValuesInRow_PlayerWins(int row, bool player)
        {
            // Arrange
            var board = Substitute.For<IBoard>();
            board.GetValue(row, 0).Returns(player);
            board.GetValue(row, 1).Returns(player);
            board.GetValue(row, 2).Returns(null, player);
            var game = new TicTacToe();
            game.Board = board;

            // Act
            game.Start();
            game.Play(row, 2, player);

            // Assert
            Assert.IsTrue(game.Winner);
        }

        [Test]        
        public void Play_GivenValidTurnAndExistingValuesInRow_PlayersDraws()
        {
            // Arrange
            var board = Substitute.For<IBoard>();
            board.GetValue(0, 0).Returns(true);
            board.GetValue(0, 1).Returns(true);
            board.GetValue(0, 2).Returns(false);

            board.GetValue(1, 0).Returns(false);
            board.GetValue(1, 1).Returns(true);
            board.GetValue(1, 2).Returns(true);

            board.GetValue(2, 0).Returns(true);
            board.GetValue(2, 1).Returns(false);
            
            board.GetValue(2, 2).Returns(null, false);
            board.IsFull.Returns(true);
            var game = new TicTacToe();
            game.Board = board;

            // Act
            game.Start();
            game.Play(2, 2, false);

            // Assert
            Assert.IsNull(game.Winner);
            Assert.IsTrue(game.IsDraw);
        }

        [Test]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        public void Play_GivenValidTurnAndExistingValuesInColumn_PlayerWins(int column, bool player)
        {
            // Arrange
            var board = Substitute.For<IBoard>();
            board.GetValue(0, column).Returns(player);
            board.GetValue(1, column).Returns(player);
            board.GetValue(2, column).Returns(null, player);
            var game = new TicTacToe();
            game.Board = board;

            // Act
            game.Start();
            game.Play(2, column, player);

            // Assert
            Assert.IsTrue(game.Winner);
        }

        [Test]
        public void Play_GivenValidTurnAndExistingValuesInDiagonal_PlayerWins()
        {
            // Arrange
            var board = Substitute.For<IBoard>();
            board.GetValue(0, 0).Returns(true);
            board.GetValue(1, 1).Returns(true);
            board.GetValue(2, 2).Returns(null, true);
            var game = new TicTacToe();
            game.Board = board;

            // Act
            game.Start();
            game.Play(2, 2, true);

            // Assert
            Assert.IsTrue(game.Winner);
        }

        [Test]
        public void Play_GivenValidTurnAndExistingValuesInDiagonalCounterwise_PlayerWins()
        {
            // Arrange
            var board = Substitute.For<IBoard>();
            board.GetValue(0, 2).Returns(true);
            board.GetValue(1, 1).Returns(true);
            board.GetValue(2, 0).Returns(null, true);
            var game = new TicTacToe();
            game.Board = board;

            // Act
            game.Start();
            game.Play(2, 0, true);

            // Assert
            Assert.IsTrue(game.Winner);
        }

    }
}