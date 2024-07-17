public class UnsafeSerializer
{
    public static void Serialize<T>(Stream stream, T[] @object) where T : struct
    {
        var bytes = @object.ToByte();
        try
        {
            using var writer = new BinaryWriter(stream);
            writer.Write(bytes.LongLength);
            writer.Write(bytes);
        }
        finally
        {
            bytes.FromByteTo<T>();
        }
    }
    
    public static T[] Deserialize<T>(Stream stream) where T:struct
    {
        using var reader = new BinaryReader(stream);
        var length = reader.ReadInt64();
        var bytes = reader.ReadBytes((int)length);
        return bytes.FromByteTo<T>();
    }
}