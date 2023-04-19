string phrase = Console.ReadLine();
string[] taoWords = phrase.Split(' ');
string[] words = new string[taoWords.Length];


int current = taoWords.Length / 2;
for (int i = 0; i < taoWords.Length; i++)
{
    words[i] = taoWords[current];
    if (i % 2 == 0) current -= i + 1;
    else current += i + 1;
}

for (int i = 0; i < words.Length; i++)
{
    string word = "";
    current = words[i].Length / 2;
    for (int j = 0; j < words[i].Length; j++)
    {
        word += words[i][current];
        if (j % 2 == 0) current -= j + 1;
        else current += j + 1;
    }
    words[i] = word;
}

string output = "";
for (int i = 0; i < words.Length; i++)
{
    output += words[i] + " ";
}
output = output.TrimEnd();
Console.WriteLine(output);