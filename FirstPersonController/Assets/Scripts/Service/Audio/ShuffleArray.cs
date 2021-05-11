using System.Linq;

namespace VHS
{
    public class ShuffleArray<T>
    {
        T[] _array;

        int _currentOrderIndex = 0;
        int _prevOrder = 0;

        int[] _orders;

        public ShuffleArray(T[] array)
        {
            _array = array;
            _orders = new int[_array.Length];
            for (int i = 0; i < _array.Length; i++) _orders[i] = i;
            Shuffle();
        }

        public T Get()
        {
            _currentOrderIndex++;

            if (_orders.Length <= _currentOrderIndex)
            {
                Shuffle();
                _currentOrderIndex = 0;
            }

            int order = _orders[_currentOrderIndex];
            _prevOrder = order;

            return _array[order];
        }

        public void Shuffle()
        {
            _orders = _orders.OrderBy(i => System.Guid.NewGuid()).ToArray();

            if (_orders[0] != _prevOrder) return;
            Shuffle();
        }
    }
}
