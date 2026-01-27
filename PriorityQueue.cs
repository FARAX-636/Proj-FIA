/* implementazione personale della PriorityQueue per Unity 2022.3 */

using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T item, float priority)> heap = new List<(T item, float priority)>();
    public int Count => heap.Count;

    public void Enqueue(T item, float priority)
    {
        heap.Add((item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 1; i < heap.Count; i++)
        {
            if (heap[i].priority < heap[bestIndex].priority)
            {
                bestIndex = i;
            }
        }

        T bestItem = heap[bestIndex].item;
        heap.RemoveAt(bestIndex);

        return bestItem;
    }

    public bool Contains(T item)
    {
        foreach (var element in heap)
        {
            if(EqualityComparer<T>.Default.Equals(element.item, item))
            {
                return true;
            }
        }

        return false;
    }
}
