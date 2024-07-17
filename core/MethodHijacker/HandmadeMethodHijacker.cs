using System.Reflection;
using System.Runtime.CompilerServices;

public unsafe class HandmadeMethodHijacker
{
    public UnsafeMemoryChange HijackMethod(MethodInfo source, MethodInfo destination)
    {
        RuntimeHelpers.PrepareMethod(source.MethodHandle);
        RuntimeHelpers.PrepareMethod(destination.MethodHandle);
        
        var sourceFp = source.MethodHandle.GetFunctionPointer();
        var destinationFp = destination.MethodHandle.GetFunctionPointer();
        
        var sourcePointer = (byte*)sourceFp.ToPointer();
        var destinationPointer = (byte*)destinationFp.ToPointer();

        var jump = (uint)destinationPointer - (uint)sourcePointer - 5;
        return new UnsafeMemoryChange((uint*)sourcePointer, 0xE9 | (jump << 8));
    }

   
}