using HarmonyLib;

var harmony = new Harmony("com.example.patch");
var source = AccessTools.Method(typeof(OriginClass), "Method");
var destination = SymbolExtensions.GetMethodInfo(() => HijackedClass.Method());

harmony.Patch(source, new HarmonyMethod(destination));

var originClass = new OriginClass();
originClass.Method();

sealed class OriginClass
{
    public void Method() => Console.WriteLine("Origin Method");
}

sealed class HijackedClass
{
    public static bool Method()
    {
        Console.WriteLine("Hijacked Method");
        return false;
    }
}