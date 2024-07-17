using System.Runtime.InteropServices;

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