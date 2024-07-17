public class UnsafeTools
{
    static unsafe IntPtr AddressOf(object o)
    {
        TypedReference reference = __makeref(o);
        return **(IntPtr**)&reference;
    }

    public static unsafe int[] ByteToInt(byte[] arr)
    {
        var byteAddress = (long*)AddressOf(arr).ToPointer();
        var intArrayTypeReference = (long*)AddressOf(Array.Empty<int>()).ToPointer();
        *byteAddress = *intArrayTypeReference;
        *(byteAddress + 1) = arr.LongLength/(sizeof(int));
        return (int[])(object)arr;
    }
}