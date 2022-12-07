using System.Collections;
using System.Collections.Specialized;

namespace ClassLibrary1;

public class DynamicArray<T> : IEnumerable<T>, INotifyCollectionChanged
{
    Node<T> head;
    Node<T> tail; 
    int count;
    
    public void AddLast(T data)
    {
        Node<T> node = new Node<T>(data);
 
        if (head == null)
            head = node;
        else
        {
            tail.Next = node;
            node.Previous = tail;
        }
        tail = node;
        count++;
        CollectionChanged?.Invoke(this,
            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, data));
    }
    public void AddFirst(T data)
    {
        Node<T> node = new Node<T>(data);
        Node<T> temp = head;
        node.Next = temp;
        head = node;
        if (count == 0)
            tail = head;
        else
            temp.Previous = node;
        count++;
        CollectionChanged?.Invoke(this,
            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, data));
    }
    public T RemoveFirst()
    {
        if (count == 0)
            throw new InvalidOperationException();
        T output = head.Data;
        if(count==1)
        {
            head = tail = null;
        }
        else
        {
            head = head.Next;
            head.Previous = null;
        }
        count--;
        CollectionChanged?.Invoke(this,
        new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, head));
        return output;
    }
    public T RemoveLast()
    {
        if (count == 0)
            throw new InvalidOperationException();
        T output = tail.Data;
        if (count == 1)
        {
            head = tail = null;
        }
        else
        {
            tail = tail.Previous;
            tail.Next = null;
        }
        count--;
        CollectionChanged?.Invoke(this,
            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, tail));
        return output;
        
    }

    public T First
    {
        get
        {
            if (IsEmpty)
                throw new InvalidOperationException();
            return head.Data;
        }
    }
    public T Last
    {
        get
        {
            if (IsEmpty)
                throw new InvalidOperationException();
            return tail.Data;
        }
    }
 
    public int Count { get { return count; } }
    public bool IsEmpty { get { return count == 0; } }
 
    public void Clear()
    {
        head = null;
        tail = null;
        count = 0;
        
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

    }
 
    public bool Contains(T data)
    {
        Node<T> current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
                return true;
            current = current.Next;
        }
        return false;
    }
    
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        if (arrayIndex + Count > array.Length)
            throw new ArgumentException(null, nameof(arrayIndex));

        var current = head;
        for (var i = arrayIndex; i < array.Length; i++)
        {
            array[i] = current!.Data;
            current = current.Next;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)this).GetEnumerator();
    }
 
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        Node<T> current = head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
}