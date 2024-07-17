byte[] byteArray = [0xAA, 0xBB, 0xCC, 0xDD];
int[] intArray = byteArray.FromByteTo<int>();
Console.WriteLine($"{intArray} {string.Join(", ",  intArray.Select(i => i.ToString("X")))}");
byteArray = intArray.ToByte();
Console.WriteLine($"{byteArray} {string.Join(", ",  byteArray.Select(b => b.ToString("X")))}");
