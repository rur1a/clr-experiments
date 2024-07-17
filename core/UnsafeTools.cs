using System.Runtime.InteropServices;

public static class UnsafeTools
{
    static unsafe IntPtr AddressOf(object o)
    {
        TypedReference reference = __makeref(o);
        return **(IntPtr**)&reference;
    }
    
    public static unsafe byte[] ToByte<T>(this T[] origin) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        var originPointer = (long*)AddressOf(origin).ToPointer();
        var destinationPointer = (long*)AddressOf(Array.Empty<byte>()).ToPointer();
        var newLength = origin.LongLength * size / sizeof(byte);
        *originPointer = *destinationPointer;
        *(originPointer + 1) = newLength;
        return (byte[])(object)origin;
    }
    
    public static unsafe T[] FromByteTo<T>(this byte[] origin) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        var originPointer = (long*)AddressOf(origin).ToPointer();
        var destinationPointer = (long*)AddressOf(Array.Empty<T>()).ToPointer();
        var newLength = origin.LongLength/size;
        *originPointer = *destinationPointer;
        *(originPointer + 1) = newLength;
        return (T[])(object)origin;
    }
}