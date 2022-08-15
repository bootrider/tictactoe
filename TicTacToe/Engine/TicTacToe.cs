namespace Engine
{
    public class TicTacToe
    {
        public IBoard Board { get; set; }

        public bool? NextPlayer { get; set; }

        public bool? Winner { get; set; }

        public bool IsDraw { get; private set; }

        public void Start()
        {
            this.Board ??= new Board();
        }

        public void Play(int x, int y, bool player)
        {
            if ((x < 0 || x > 2) || (y < 0 || y > 2))
            {
                throw new ArgumentException("Position is not valid");
            }

            if (this.NextPlayer is not null && player != this.NextPlayer)
            {
                throw new ArgumentException("A player cannot play twice");
            }

            if (this.Board.GetValue(x, y) is not null)
            {
                throw new ArgumentException("Invalid movement, cell already taken");
            }

            this.Board.SetValue(x, y, player);
            this.EvaluatePlay(player);
            if (this.Winner is null)
            {
                this.NextPlayer = !player;
            }
        }

        private void EvaluatePlay(bool player)
        {
            if (this.RowIsEqual(0) || this.RowIsEqual(1) || this.RowIsEqual(2))
            {
                this.Winner = player;
            }

            if (this.ColumnIsEqual(0) || this.ColumnIsEqual(1) || this.ColumnIsEqual(2))
            {
                this.Winner = player;
            }

            if (this.DiagonalIsEqual() || this.DiagonalIsEqual(true))
            {
                this.Winner = player;
            }

            this.IsDraw = this.Board.IsFull;
        }

        private bool DiagonalIsEqual(bool counterwise = false)
        {
            var columnValues = new List<bool?>();
            for (int i = 0; i < 3; i++)
            {
                var j = counterwise ? 2 - i : i;
                columnValues.Add(this.Board.GetValue(i, j));
            }

            return columnValues.All(o => columnValues.First() is not null && columnValues.First() == o);
        }

        private bool ColumnIsEqual(int j)
        {
            var columnValues = new List<bool?>();
            for (int i = 0; i < 3; i++)
            {
                columnValues.Add(this.Board.GetValue(i, j));
            }

            return columnValues.All(o => columnValues.First() is not null && columnValues.First() == o);
        }

        private bool RowIsEqual(int i)
        {
            var rowValues = new List<bool?>();
            for (int j = 0; j < 3; j++)
            {
                rowValues.Add(this.Board.GetValue(i, j));
            }

            return rowValues.All(o => rowValues.First() is not null && rowValues.First() == o);
        }
    }
}