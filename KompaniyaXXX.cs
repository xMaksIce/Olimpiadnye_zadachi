
for (int i = 1; i <= 16; i++)
{
    string num;
    if (i < 10) num = "0" + Convert.ToString(i);
    else num = Convert.ToString(i);
    StreamReader file = new("C:\\Users\\Maksim\\Downloads\\Компания ХХХ\\input_s1_" + num + ".txt");
    string[] result = CompanyXXX(file);
    string[] answer = File.ReadAllLines("C:\\Users\\Maksim\\Downloads\\Компания ХХХ\\output_s1_" + num + ".txt");
    Console.WriteLine($"{num}. {Enumerable.SequenceEqual(result, answer)}");
    Console.WriteLine();
}


string[] CompanyXXX(StreamReader sr)
{
    Dictionary<string, string> numToName = new();
    Dictionary<string, List<string>> bossToSub = new();
    string str = sr.ReadLine().Trim();
    string nextStr;
    while (str != "END")
    {
        if (str.Length != 4)
        {
            numToName[str[..4]] = str[5..];
        }
        else
        {
            if (!numToName.ContainsKey(str[..4]))
            {
                numToName[str[..4]] = "Unknown Name";
            }
        }

        nextStr = sr.ReadLine().Trim();
        if (!bossToSub.ContainsKey(str[..4]))
        {
            bossToSub[str[..4]] = new();
        }
        bossToSub[str[..4]].Add(nextStr[..4]);

        if (nextStr.Length != 4)
        {
            numToName[nextStr[..4]] = nextStr[5..];
        }
        else
        {
            if (!numToName.ContainsKey(nextStr[..4]))
            {
                numToName[nextStr[..4]] = "Unknown Name";
            }
        }
        str = sr.ReadLine().Trim();
    }

    List<string> resultSubs = new();
    string boss = sr.ReadLine().Trim();
    string bossNum = "";
    bool bossIsNum = true;
    try
    {
        int test = Convert.ToInt32(boss);
    }
    catch
    {
        bossIsNum = false;
    }

    if (bossIsNum)
    {
        bossNum = boss;

    }
    else
    {
        string bossName = boss;
        foreach (var (key, value) in numToName)
        {
            if (value == bossName)
            {
                bossNum = key;
                break;
            }
        }
    }


    void Bulldozer(string bossNum)
    {
        if (bossToSub.ContainsKey(bossNum))
        {
            List<string> bossWorkers = bossToSub[bossNum];
            resultSubs.AddRange(bossWorkers);
            foreach (string boss in bossWorkers)
            {
                Bulldozer(boss);
            }
        }
    }

    Bulldozer(bossNum);

    List<string> resultInList = new();
    if (resultSubs.Count == 0)
    {
        Console.WriteLine("NO");
        resultInList.Add("NO");
    }
    else
    {
        resultSubs.Sort();
        foreach (string result in resultSubs)
        {
            Console.WriteLine($"{result} {numToName[result]}");
            resultInList.Add(result + " " + numToName[result]);
        }
    }

    return resultInList.ToArray();
}
