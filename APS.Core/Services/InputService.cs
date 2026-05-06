using System;
using System.Collections.Generic;
using System.Linq;

public class InputService
{
    public List<int> GetFromUser(string input)
    {
        if (!InputValidator.TryParseNumbers(input, out var numbers, out var error))
        {
            throw new Exception(error);
        }

        return numbers;
    }
}