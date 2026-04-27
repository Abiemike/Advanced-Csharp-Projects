using MyMathsLibrary;

namespace ExtensionMethodDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var calcLibrary = new BasicMathsLibrary();

            var sum = calcLibrary.AddNumbers(10, 25);

            Console.WriteLine(sum);

            //Calling the extension methods

            var square = calcLibrary.GetSquare(2);
            Console.WriteLine($"Square: {square}");

            var cube= calcLibrary.GetCube(5);

            Console.WriteLine($"Cube: {cube}");

            Console.ReadLine();
        }
    }
}
