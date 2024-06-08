namespace DStruct.List
{
    public interface IList<T> : ICollection<T>
    {
        T this[int index] { set; get; }

        void PushBack(T e);

        T PopBack();

        T PeekBack();

        void Remove(int index);

        void Insert(int index, T e);
    }
}