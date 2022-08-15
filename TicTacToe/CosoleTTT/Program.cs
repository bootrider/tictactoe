using Engine;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;

        var game = new TicTacToe();
        game.Start();
        var player = true;
        while (game.Winner is null && !game.IsDraw)
        {
            Program.DrawBoard();
            Program.DrawPositions(game.Board);
            Program.DrawInformation(player);
            var key = Console.ReadKey();
            var posSelected = Program.TransformPosition(Convert.ToInt16(key.KeyChar.ToString()));
            game.Play(posSelected.X, posSelected.Y, player);
            player = game.NextPlayer.Value;
        }

        Program.DrawBoard();
        Program.DrawPositions(game.Board);
        Program.DrawWinner(game.Winner, game.IsDraw);
    }

    private static void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine("+-----+-----+-----+");
        Console.WriteLine("|     |     |     |");
        Console.WriteLine("+-----+-----+-----+");
        Console.WriteLine("|     |     |     |");
        Console.WriteLine("+-----+-----+-----+");
        Console.WriteLine("|     |     |     |");
        Console.WriteLine("+-----+-----+-----+");
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("/*************************************************************/");
    }

    private static void DrawPositions(IBoard board)
    {
        var pos = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.SetCursorPosition(3 + j * 6, 1 + i * 2);
                var cellValue = board.GetValue(i, j);
                ++pos;
                var value = cellValue is null ? (pos).ToString() : cellValue.Value ? "X" : "O";
                Console.WriteLine(value);
            }
        }
    }

    private static void DrawInformation(bool player)
    {
        var playerString = player ? "X" : "O";
        Console.SetCursorPosition(0, 7);
        Console.WriteLine($"{playerString} Plays.");
    }

    private static void DrawWinner(bool? player, bool isDraw)
    {
        var playerString = !player.HasValue ? String.Empty : player.Value ? "X" : "O";
        var message = isDraw ? "I t    i s   a    D r a w" : "           W I N S ! ! !";
        Console.WriteLine(isDraw ? string.Empty : playerString);
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine(message);
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("/*************************************************************/");
    }

    private static (int X, int Y) TransformPosition(int numPosition)
    {
        switch (numPosition)
        {
            case 1: return (0, 0);
            case 2: return (0, 1);
            case 3: return (0, 2);

            case 4: return (1, 0);
            case 5: return (1, 1);
            case 6: return (1, 2);

            case 7: return (2, 0);
            case 8: return (2, 1);
            case 9: return (2, 2);
            default: return (-1, -1);
        }
    }
}