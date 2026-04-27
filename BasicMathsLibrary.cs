namespace MyMathsLibrary;

public class BasicMathsLibrary
{
    public double AddNumbers(double firstNumber, double secondNumber)
    {
        return firstNumber + secondNumber;
    }

    public double SubtractNumbers(double firstNumber, double secondNumber)
    {
        return firstNumber - secondNumber;
    }

    public double MultiplyNumbers(double firstNumber, double secondNumber)
    {
        return firstNumber * secondNumber;
    }

    public double DivideNumbers(double firstNumber, double secondNumber)
    {
        if (!ValidateNumber(secondNumber))
            return firstNumber / secondNumber;
        return 0;
    }

    internal bool ValidateNumber(double number)
    {   
        return number==0;
    }
}