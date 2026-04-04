using System;
using System.Collections.Generic;

public class ObjectPool<T>
{
    private Queue<T> _pool;

    private Func<T> _createObject;
    private Action<T> _onGetObject;
    private Action<T> _onPutObject;
    private Action<T> _destroyObject;
    private int _capacity;

    public int Count => _pool.Count;

    public ObjectPool(Func<T> createObject, Action<T> destroyObject,
                      Action<T> onGetObject, Action<T> onPutObject, int capacity)
    {
        _createObject = createObject;
        _destroyObject = destroyObject;
        _onGetObject = onGetObject;
        _onPutObject = onPutObject;
        _capacity = capacity;

        _pool = new();
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
            _pool.Enqueue(_createObject());

        _onGetObject(_pool.Peek());

        return _pool.Dequeue();
    }

    public void PutObject(T obj)
    {
        _pool.Enqueue(obj);
        _onPutObject(obj);

        while (_pool.Count > _capacity)
        {
            _destroyObject(obj);
        }
    }
}
