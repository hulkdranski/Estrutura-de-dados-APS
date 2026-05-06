using System;
using System.Collections.Generic;
using System.Linq;

public class RandomService
{
    public List<int> Generate(int quantity, int min, int max)
    {
        if (!InputValidator.ValidateQuantity(quantity, out var error))
            throw new Exception(error);

        if (!InputValidator.ValidateRange(min, max, out error))
            throw new Exception(error);

        Random rand = new Random();
        List<int> numbers = new List<int>();

        for (int i = 0; i < quantity; i++)
        {
            numbers.Add(rand.Next(min, max));
        }

        return numbers;
    }
}