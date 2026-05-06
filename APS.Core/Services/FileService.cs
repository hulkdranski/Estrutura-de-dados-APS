using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class FileService
{
    public List<int> ImportFromTxt(string path)
    {
        if (!File.Exists(path))
            throw new Exception("Arquivo não encontrado.");

        string content = File.ReadAllText(path);

        if (!InputValidator.TryParseNumbers(content, out var numbers, out var error))
            throw new Exception($"Erro no arquivo: {error}");

        return numbers;
    }
}