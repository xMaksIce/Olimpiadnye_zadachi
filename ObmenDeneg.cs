for (int i = 1; i <= 14; i++)
{
    StreamReader file = new("C:\\Users\\Maksim\\Downloads\\Обмен денег\\input" + i + ".txt");
    string result = Obmen(file);
    string answer = File.ReadLines("C:\\Users\\Maksim\\Downloads\\Обмен денег\\output" + i + ".txt").ToArray()[0];
    Console.WriteLine("Результат: " + result);
    Console.WriteLine("Ответ: " + answer);
    Console.WriteLine(result == answer);
    Console.WriteLine();
}

string Obmen(StreamReader file)
{
    int startAmount = file.Read() - '0';
    if (startAmount != 1) file.Read();
    else file.ReadLine();

    int[] startRatios;
    if (startAmount != 1) startRatios = file.ReadLine().Split().Select(Int32.Parse).ToArray();
    else startRatios = Array.Empty<int>();

    List<int> startBadNums = file.ReadLine().Split().Select(Int32.Parse).ToList();
    int startBadNumsAmount = startBadNums[0];
    startBadNums.RemoveAt(0);
    startBadNums.Sort();
    startBadNums.Reverse();

    int endAmount = file.Read() - '0';
    if (endAmount != 1) file.Read();
    else file.ReadLine();

    int[] endRatios;
    if (endAmount != 1) endRatios = file.ReadLine().Split().Select(Int32.Parse).ToArray();
    else endRatios = Array.Empty<int>();

    List<int> endBadNums = file.ReadLine().Split().Select(Int32.Parse).ToList();
    int endBadNumsAmount = endBadNums[0];
    endBadNums.RemoveAt(0);
    endBadNums.Sort();

    int[] startRepresValues = file.ReadLine().Split().Select(Int32.Parse).ToArray();

    int[] startActualValues = new int[startAmount];
    for (int i = 0; i < startAmount; i++)
    {
        int actualValue = startRepresValues[^(i+1)];
        foreach (int badValue in startBadNums)
        {
            if (badValue <= actualValue) actualValue--;
        }
        startActualValues[^(i + 1)] = actualValue;
    }

    int startInLesserValue = 0;
    for (int i = 0; i < startAmount; i++)
    {
        if (i == startAmount - 1) startInLesserValue += startActualValues[i];
        else startInLesserValue = (startInLesserValue + startActualValues[i]) * startRatios[i];
    }

    int[] endActualValues = new int[endAmount];
    for (int i = 0; i < endAmount; i++)
    {
        if (i == endAmount - 1) endActualValues[^(i + 1)] = startInLesserValue;
        else
        {
            endActualValues[^(i + 1)] = startInLesserValue % endRatios[^(i + 1)];
            startInLesserValue /= endRatios[^(i + 1)];
        }
    }

    int[] endRepresValues = new int[endAmount];
    Array.Copy(endActualValues, endRepresValues, endAmount);

    for (int i = 0; i < endAmount; i++) 
    {
        foreach (int badValue in endBadNums)
        {
            if (badValue <= endRepresValues[^(i + 1)]) endRepresValues[^(i + 1)]++;
        }
    }

    string answer = "";
    foreach (int num in endRepresValues) answer += num + " ";
    answer = answer.TrimEnd();
    return answer;
}