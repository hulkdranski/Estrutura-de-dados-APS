using System;
using System.Collections.Generic;
using System.Linq;

public static class InputValidator
{
    public static bool TryParseNumbers(string input, out List<int> numbers, out string error)
    {
        numbers = new List<int>();
        error = "";

        if (string.IsNullOrWhiteSpace(input))
        {
            error = "Entrada vazia.";
            return false;
        }

        var parts = input.Split(',');

        foreach (var part in parts)
        {
            if (!int.TryParse(part.Trim(), out int num))
            {
                error = $"Valor inválido: '{part}'";
                return false;
            }

            numbers.Add(num);
        }

        return true;
    }

    public static bool ValidateRange(int min, int max, out string error)
    {
        error = "";

        if (min >= max)
        {
            error = "O valor mínimo deve ser menor que o máximo.";
            return false;
        }

        return true;
    }

    public static bool ValidateQuantity(int quantity, out string error)
    {
        error = "";

        if (quantity <= 0)
        {
            error = "A quantidade deve ser maior que zero.";
            return false;
        }

        return true;
    }
}