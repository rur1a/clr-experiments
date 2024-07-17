using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public unsafe class MethodHijacker : IMethodHijacker
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

public unsafe class UnsafeMemoryChange
{
    private readonly uint* pointer;
    private readonly uint data;
    private readonly uint originalData;

    public UnsafeMemoryChange(uint* pointer, uint data)
    {
        this.pointer = pointer;
        this.data = data;
        originalData = *pointer;
        Apply();
    }

    public void Apply()
    {
        VirtualProtect((byte*)pointer, 8, 0x40, out var oldProtect);
        *pointer = data;
        VirtualProtect((byte*)pointer,8, oldProtect, out oldProtect);
    }

    public void Undo()
    {
        VirtualProtect((byte*)pointer, 8, 0x40, out var oldProtect);
        *pointer = originalData;
        VirtualProtect((byte*)pointer, 8, oldProtect, out oldProtect);
    }

    [DllImport("kernel32")]
    static extern bool VirtualProtect(byte* lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);
}

public interface IMethodHijacker
{
    UnsafeMemoryChange HijackMethod(MethodInfo source, MethodInfo destination);
}