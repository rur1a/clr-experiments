var source = typeof(OriginClass).GetMethod("Method");
var destination = typeof(HijackedClass).GetMethod("Method");
    
var hijacker = new MethodHijacker();
hijacker.HijackMethod(source, destination);

var originClass = new OriginClass();
originClass.Method();

sealed class OriginClass
{
    public void Method() => Console.WriteLine("Origin Method");
}

sealed class HijackedClass
{
    public void Method() => Console.WriteLine("Hijacked Method");
}