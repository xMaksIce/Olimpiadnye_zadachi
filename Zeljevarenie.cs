for (int i = 1; i <= 10; i++)
{
    StreamReader file = new("C:\\Users\\Maksim\\Downloads\\Зельеварение\\input" + i + ".txt");
    string result = Zeljevarenie(file);
    string answer = File.ReadLines("C:\\Users\\Maksim\\Downloads\\Зельеварение\\output" + i + ".txt").ToArray()[0];
    Console.WriteLine("Результат: " + result);
    Console.WriteLine("Ответ: " + answer);
    Console.WriteLine(result == answer);
    Console.WriteLine();
}

string Zeljevarenie(StreamReader stream)
{
    List<string> magicWords = new();
    while (!stream.EndOfStream)
    {
        string ingredients = "";
        string[] words = stream.ReadLine().Split(" ");   
        for(int j = 1; j < words.Length; j++)
        {
            try
            {
                int linkIndex = Convert.ToInt32(words[j]);
                ingredients += magicWords[linkIndex - 1];
            }
            catch
            {
                ingredients += words[j];
            }
        }
        string method = words[0];
        switch (method)
        {
            case "MIX":
                {
                    magicWords.Add(Mix(ingredients));
                    break;
                }
            case "WATER":
                {
                    magicWords.Add(Water(ingredients));
                    break;
                }
            case "DUST":
                {
                    magicWords.Add(Dust(ingredients));
                    break;
                }
            case "FIRE":
                {
                    magicWords.Add(Fire(ingredients));
                    break;
                }
            default: break;
        }
    }
    return magicWords.Last();
    string Mix(string ingredients) => "MX" + ingredients + "XM";
    string Water(string ingredients) => "WT" + ingredients + "TW";
    string Dust(string ingredients) => "DT" + ingredients + "TD";
    string Fire(string ingredients) => "FR" + ingredients + "RF";
}