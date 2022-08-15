namespace Engine
{
    public interface IBoard
    {
        bool IsEmpty { get; }

        bool IsFull { get; }

        void SetValue(int x, int y, bool player);

        bool? GetValue(int x, int y);
    }
}