namespace IceCity_W4CC
{
    public static class IntExtensionMethods
    {
        public static bool IsEven(this int value) => value % 2 == 0;
        public static bool IsOdd(this int value) => value % 2 != 0;
    }
}

