using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class MethodHijacker : IMethodHijacker
{
    public unsafe void HijackMethod(MethodInfo source, MethodInfo destination)
    {
        RuntimeHelpers.PrepareMethod(source.MethodHandle);
        RuntimeHelpers.PrepareMethod(destination.MethodHandle);
        
        var sourceFp = source.MethodHandle.GetFunctionPointer();
        var destinationFp = destination.MethodHandle.GetFunctionPointer();
        
        var sourcePointer = (byte*)sourceFp.ToPointer();
        var destinationPointer = (byte*)destinationFp.ToPointer();

        VirtualProtect(sourcePointer, 8, 0x40, out var oldProtect);
        
        var jump = (uint)destinationPointer - (uint)sourcePointer - 5;
        *sourcePointer = 0xE9;
        *(uint*)(sourcePointer + 1) = jump;

        VirtualProtect(sourcePointer, 8, oldProtect, out oldProtect);
    }

    [DllImport("kernel32")]
    static extern unsafe bool VirtualProtect(byte* lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);
}

public interface IMethodHijacker
{
    void HijackMethod(MethodInfo source, MethodInfo destination);
}