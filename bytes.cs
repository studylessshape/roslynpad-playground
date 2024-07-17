string ascii = "12.96L/m";
var bytes = Encoding.ASCII.GetBytes(ascii);
for(int i = 0;i<bytes.Length;i++)
{
    Console.WriteLine($"{bytes[i]}");
}