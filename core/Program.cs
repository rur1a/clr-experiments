var source = typeof(OriginClass).GetMethod("Method");
var destination = typeof(HijackedClass).GetMethod("Method");
    
var hijacker = new MethodHijacker();
var memoryChange = hijacker.HijackMethod(source, destination);

HijackedClass.MemoryChange = memoryChange;
var originClass = new OriginClass();
originClass.Method();

sealed class OriginClass
{
    public void Method() => Console.WriteLine("Origin Method");
}

sealed class HijackedClass
{
    public static UnsafeMemoryChange MemoryChange;
    public static void Method(OriginClass origin)
    {
        MemoryChange.Undo();
        origin.Method();
        MemoryChange.Apply();
        Console.WriteLine("Hijacked Method");
    }
}