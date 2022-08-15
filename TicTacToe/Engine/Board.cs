namespace Engine
{
    public class Board : IBoard
    {
        private bool?[,] content = new bool?[3, 3];

        public bool IsEmpty
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (this.content[i, j] is not null)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public bool? GetValue(int x, int y)
        {
            return this.content[x, y];
        }

        public void SetValue(int x, int y, bool player) => this.content[x, y] = player;
    }
}