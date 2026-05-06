using System.Collections.Generic;

public class SortingService
{
    public List<int> BubbleSort(List<int> list)
    {
        var arr = new List<int>(list);

        for (int i = 0; i < arr.Count - 1; i++)
        {
            for (int j = 0; j < arr.Count - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }

        return arr;
    }

    public List<int> SelectionSort(List<int> list)
    {
        var arr = new List<int>(list);

        for (int i = 0; i < arr.Count - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < arr.Count; j++)
            {
                if (arr[j] < arr[minIndex])
                    minIndex = j;
            }

            int temp = arr[i];
            arr[i] = arr[minIndex];
            arr[minIndex] = temp;
        }

        return arr;
    }

    public List<int> InsertionSort(List<int> list)
    {
        var arr = new List<int>(list);

        for (int i = 1; i < arr.Count; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            arr[j + 1] = key;
        }

        return arr;
    }

    public List<int> MergeSort(List<int> list)
{
    if (list.Count <= 1)
        return list;

    int middle = list.Count / 2;

    var left = list.GetRange(0, middle);
    var right = list.GetRange(middle, list.Count - middle);

    left = MergeSort(left);
    right = MergeSort(right);

    return Merge(left, right);
}

    private List<int> Merge(List<int> left, List<int> right)
    {
        List<int> result = new List<int>();

        int i = 0, j = 0;

        while (i < left.Count && j < right.Count)
        {
            if (left[i] <= right[j])
            {
                result.Add(left[i]);
                i++;
            }
            else
            {
                result.Add(right[j]);
                j++;
            }
        }

        // Adiciona o restante
        while (i < left.Count)
        {
            result.Add(left[i]);
            i++;
        }

        while (j < right.Count)
        {
            result.Add(right[j]);
            j++;
        }

        return result;
    }

    public List<int> QuickSort(List<int> list)
    {
        var arr = new List<int>(list);
        QuickSortRecursive(arr, 0, arr.Count - 1);
        return arr;
    }

    private void QuickSortRecursive(List<int> arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);

            QuickSortRecursive(arr, low, pivotIndex - 1);
            QuickSortRecursive(arr, pivotIndex + 1, high);
        }
    }

    private int Partition(List<int> arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;

                // troca
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int tempPivot = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = tempPivot;

        return i + 1;
    }

    public int BinarySearch(List<int> list, int target)
    {
        int left = 0;
        int right = list.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (list[mid] == target)
                return mid;

            if (list[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1; // não encontrado
    }
}