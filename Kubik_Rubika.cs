string[] file = File.ReadAllLines("C:\\Users\\Maksim\\Downloads\\input_s1_01.txt");
for (int i = 1; i < file.Length; i++)
{
    file[i] = file[i].Replace('X', '1');
    file[i] = file[i].Replace('Y', '2');
    file[i] = file[i].Replace('Z', '3');
} // создаём массив строк из файла, в которых меняем все X на 1, Y на 2, Z на 3 для дальнейшего удобства считывания

StreamWriter fi = new StreamWriter("C:\\Users\\Maksim\\Downloads\\input_s1_01.txt");
for (int i = 0; i < file.Length; i++)
{
    fi.WriteLine(file[i]);
} // записываем изменённые строки обратно в файл

fi.Close();

StreamReader f = new StreamReader("C:\\Users\\Maksim\\Downloads\\input_s1_01.txt");
var ints = f.ReadLine().Split(' ').Select(Int32.Parse).ToArray(); // создаём массив из данных первой строки - размерности кубика и количеству действий
int n = ints[0], m = ints[1]; 
var cords = f.ReadLine().Split(' ').Select(Int32.Parse).ToArray(); // создаём массив изначальных координат
int x = cords[0], y = cords[1], z = cords[2];
for (int i = 0; i < m; i++) // построчно считываем строки с заданными вращениями кубика
{
    var lines = f.ReadLine().Split(' ').Select(Int32.Parse).ToArray(); 
    int a = lines[0], k = lines[1], s = lines[2];
    if (a == 1 && x == k) Rodarora(ref y, ref z, s); // для вращения вокруг оси X. проверяем блок вращения K, т.к. если вращаем не его, то координата искомой ячейки кубика не меняется
    else if (a == 2 && y == k) Rodarora(ref x, ref z, s); // так же и для Y
    else if (a == 3 && z == k) Rodarora(ref x, ref y, s); // и Z
}
Console.WriteLine($"{x} {y} {z}");

// алгоритм вращения кубика заключается в том, что координата, вокруг которой происходит вращение не изменяется, а при вращении по часовой стрелке
// новая первая координата оказывается равна предыдущей второй, а новая вторая координата - разности размерности кубика, увеличенной на единицу, и старой первой координаты
// при вращении против часовой стрелки происходит наоборот.
void Rodarora(ref int cor1, ref int cor2, int s)
{
    if (s == 1)
    {
        int temp = cor2;
        cor2 = n - cor1 + 1;
        cor1 = temp;
    }
    else
    {
        int temp = cor1;
        cor1 = n - cor2 + 1;
        cor2 = temp;
    }
}

