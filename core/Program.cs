using var fileWriteStream = File.OpenWrite("data.bin");
var data = new[]
{
    new SampleStruct { B1 = 0xAA, B2 = 0xBB, I = 0xCCDDEEFF, L = 0x001122334455667788U },
    new SampleStruct { B1 = 0xAA, B2 = 0xBB, I = 0xCCDDEEFF, L = 0x001122334455667788U },
    new SampleStruct { B1 = 0xAA, B2 = 0xBB, I = 0xCCDDEEFF, L = 0x001122334455667788U },
};
UnsafeSerializer.Serialize(fileWriteStream, data);

using var fileReadStream = File.OpenRead("data.bin");
var deserializedData = UnsafeSerializer.Deserialize<SampleStruct>(fileReadStream); 
foreach (var item in deserializedData) 
    Console.WriteLine($"B1: {item.B1:X2}, B2: {item.B2:X2}, I: {item.I:X8}, L: {item.L:X16}");

struct SampleStruct
{
    public byte B1;
    public byte B2;
    public ulong L;
    public uint I;
}
