namespace MyMathsLibrary
{
    public static class BasicMathsLibraryExtensionsv1
    {
        public static double GetSquare(this BasicMathsLibrary library,double number)
        {
            if (!library.ValidateNumber(number))
            {
                var result = Math.Pow(number, 2);
                return result;
            }

            return 0;
        }

        public static double GetCube(this BasicMathsLibrary library, double number)
        {
            if (!library.ValidateNumber(number))
            {
                var result = Math.Pow(number, 3);
                return result;
            }
            return 0;
        }
    }
}
