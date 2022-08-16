using Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreWebAppTTT.Pages
{
    public class TicTacToeModel : PageModel
    {
        public static TicTacToe Game;

        [BindProperty(Name = "Position")]
        public int? Position { get; set; }

        public bool? Player { get => TicTacToeModel.Game?.NextPlayer ?? true; }

        public TicTacToeModel()
        {
            if (TicTacToeModel.Game is null || TicTacToeModel.Game.IsDraw || TicTacToeModel.Game.Winner.HasValue)
            {
                TicTacToeModel.Game = new TicTacToe();
            }
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (TicTacToeModel.Game.Board is null)
            {
                TicTacToeModel.Game.Start();
            }

            if (this.Position.HasValue)
            {
                var play = TransformPosition(this.Position.Value);
                try
                {
                    var player = TicTacToeModel.Game.NextPlayer ?? true;
                    TicTacToeModel.Game.Play(play.X, play.Y, player);
                }
                catch (ArgumentException ex)
                {
                    return;
                }
            }
        }

        public (int X, int Y) TransformPosition(int numPosition)
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
}