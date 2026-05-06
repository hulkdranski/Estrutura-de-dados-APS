using System;
using System.Collections.Generic;
using System.Linq;

public class AppController
{
    private FileService _fileService = new FileService();
    private RandomService _randomService = new RandomService();
    private InputService _inputService = new InputService();
    private SortingService _sortingService = new SortingService();

    public List<int> ImportFile(string path)
    {
        return _fileService.ImportFromTxt(path);
    }

    public List<int> GenerateNumbers(int qtd, int min, int max)
    {
        return _randomService.Generate(qtd, min, max);
    }

    public List<int> ManualInput(string input)
    {
        return _inputService.GetFromUser(input);
    }

    public List<int> SortMerge(List<int> list)
    {
        return _sortingService.MergeSort(list);
    }

    public List<int> SortQuick(List<int> list)
    {
        return _sortingService.QuickSort(list);
    }

    public List<int> SortBubble(List<int> list)
    {
        return _sortingService.BubbleSort(list);
    }

    public List<int> SortInsertion(List<int> list)
    {
        return _sortingService.InsertionSort(list);
    }

    public List<int> SortSelection(List<int> list)
    {
        return _sortingService.SelectionSort(list);
    }

    public int SearchBinary(List<int> list, int target)
    {
        return _sortingService.BinarySearch(list, target);
    }

    public SortResult MeasureBubble(List<int> list)
    {
        return _sortingService.MeasureSort(_sortingService.BubbleSort, list);
    }

    public SortResult MeasureQuick(List<int> list)
    {
        return _sortingService.MeasureSort(_sortingService.QuickSort, list);
    }

    public SortResult MeasureMerge(List<int> list)
    {
        return _sortingService.MeasureSort(_sortingService.MergeSort, list);
    }

    public SortResult MeasureInsertion(List<int> list)
    {
        return _sortingService.MeasureSort(_sortingService.InsertionSort, list);
    }

    public SortResult MeasureSelection(List<int> list)
    {
        return _sortingService.MeasureSort(_sortingService.SelectionSort, list);
    }
}