using UnityEngine;
using UnityEngine.Events;

namespace Grids
{
    public class SquareGrid<T>
    {
        private readonly UnityEvent<OnValueChangedArgs> _onValueChanged = new UnityEvent<OnValueChangedArgs>();
        public class OnValueChangedArgs
        {
            public int X;
            public int Y;
        }

        private readonly int _width;
        private readonly int _height;
        private readonly T[] _cells;
        private readonly IIndexesHelper _indexesHelper;
        private readonly Vector3 _originPosition;
        private readonly float _cellSize;

        public int Width => _width;
        public int Height => _height;

        public Vector3 OriginPosition => _originPosition;
        public float CellSize => _cellSize;
        
        public SquareGrid(int width, int height, float cellSize, Vector3 originPosition, System.Func<SquareGrid<T>, int, int, T> createCell, bool xzPlane = true)
        {
            _width = width;
            _height = height;
            _originPosition = originPosition;
            _cellSize = cellSize;
            
            _cells = new T[width * height];

            if (xzPlane)
            {
                _indexesHelper = new IndexesHelperXZ(cellSize, originPosition);
            }
            else
            {
                _indexesHelper = new IndexesHelperXY(cellSize, originPosition);
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var index = x + y * _width;
                    _cells[index] = createCell(this, x, y);
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return _indexesHelper.GetWorldPosition(x, y);
        }

        public Vector2Int GetIndexes(Vector3 worldPos)
        {
            return _indexesHelper.GetCellIndex(worldPos);
        }

        public void SetValue(Vector3 worldPos, T value)
        {
            var indexes = GetIndexes(worldPos);
            SetValue(indexes.x, indexes.y, value);
        }
        public void SetValue(int x, int y, T value)
        {
            var index = x + y * _width;
            if (index < _cells.Length)
            {
                _cells[index] = value;
                TriggerOnValueChanged(x, y);
            }
            else
            {
                Debug.LogError($"Index out of range : {index} ({x}:{y})");
            }
        }

        public T GetValue(Vector3 worldPos)
        {
            var indexes = GetIndexes(worldPos);
            return GetValue(indexes.x, indexes.y);
        }
        public T GetValue(int x, int y)
        {
            var index = x + y * _width;
            return index < _cells.Length ? _cells[index] : default;
        }

        public void TriggerOnValueChanged(int x, int y)
        {
            _onValueChanged.Invoke(new OnValueChangedArgs() {X = x, Y = y});
        }

        public void SubscribeOnValueChanged(UnityAction<OnValueChangedArgs> action)
        {
            _onValueChanged.AddListener(action);
        }
        public void UnsubscribeOnValueChanged(UnityAction<OnValueChangedArgs> action)
        {
            _onValueChanged.RemoveListener(action);
        }
        
        

        #region IndexesHelper

        private interface IIndexesHelper
        {
            Vector3 GetWorldPosition(int x, int y);
            Vector2Int GetCellIndex(Vector3 worldPos);
        }
        private class IndexesHelperXZ : IIndexesHelper
        {
            private readonly float _cellSize;
            private readonly Vector3 _originPosition;

            public IndexesHelperXZ(float cellSize, Vector3 originPosition)
            {
                _cellSize = cellSize;
                _originPosition = originPosition;
            }
            
            public Vector3 GetWorldPosition(int x, int y)
            {
                return new Vector3(x, 0, y) * _cellSize + _originPosition;
            }

            public Vector2Int GetCellIndex(Vector3 worldPos)
            {
                return SquareGridCellUtil.GetCellIndexXZ(worldPos, _originPosition, _cellSize);
            }
        }
        private class IndexesHelperXY : IIndexesHelper
        {
            private readonly float _cellSize;
            private readonly Vector3 _originPosition;

            public IndexesHelperXY(float cellSize, Vector3 originPosition)
            {
                _cellSize = cellSize;
                _originPosition = originPosition;
            }
            
            public Vector3 GetWorldPosition(int x, int y)
            {
                return new Vector3(x, y, 0) * _cellSize + _originPosition;
            }

            public Vector2Int GetCellIndex(Vector3 worldPos)
            {
                return SquareGridCellUtil.GetCellIndexXY(worldPos, _originPosition, _cellSize);
            }
        }
        
        #endregion //IndexesHelper
    }

    public static class SquareGridCellUtil
    {
        public static Vector2Int GetCellIndexXY(Vector3 worldPos, Vector3 gridOriginPos, float gridCellSize)
        {
            var pos = worldPos - gridOriginPos;
            return new Vector2Int(Mathf.RoundToInt(pos.x / gridCellSize),
                Mathf.RoundToInt(pos.y / gridCellSize));
        }
        public static Vector2Int GetCellIndexXZ(Vector3 worldPos, Vector3 gridOriginPos, float gridCellSize)
        {
            var pos = worldPos - gridOriginPos;
            return new Vector2Int(Mathf.RoundToInt(pos.x / gridCellSize),
                Mathf.RoundToInt(pos.z / gridCellSize));
        }
        
        public static int CalculateIndex(int x, int y, int gridWidth)
        {
            return x + y * gridWidth;
        }
    }
}