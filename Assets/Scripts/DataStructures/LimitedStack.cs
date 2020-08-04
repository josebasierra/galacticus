


public class LimitedStack<T>
{
    T[] elements;

    int maxCapacity;
    int count;
    int first, last;


    public LimitedStack(int maxCapacity)
    {
        elements = new T[maxCapacity];

        this.maxCapacity = maxCapacity;
        count = 0;
        first = last = -1;
    }


    public void Push(T element)
    {
        count++;
        if (count > maxCapacity)
        {
            first = mod(first + 1, maxCapacity);
            count--;
        }

        int insertPosition = mod(last+1, maxCapacity);
        elements[insertPosition] = element;
        last = insertPosition;
    }


    public void Pop()
    {
        if (count <= 0) return;

        count--;
        if (count <= 0)
        {
            first = last = -1;
        }
        else
        {
            last = mod(last - 1, maxCapacity);
        }
    }


    public T Top()
    {
        return elements[last];
    }


    public bool IsEmpty()
    {
        return count == 0;
    }


    public int Count()
    {
        return count;
    }


    public int MaxCapacity()
    {
        return maxCapacity;
    }


    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }

}
