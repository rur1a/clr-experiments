byte[] byteArray = [0xAA, 0xBB, 0xCC, 0xDD];
int[] intArray = UnsafeTools.ByteToInt(byteArray);
Console.WriteLine(intArray);