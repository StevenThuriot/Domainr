# ![Domainr](https://cloud.githubusercontent.com/assets/544444/6084594/8b52237e-ae32-11e4-9a1f-7ecf4dcb839a.png)
.NET Helper to make executing a piece of code in a separate domain easier.

## Usage

```csharp
using (var domain = await Domainr.InitAsync("MyNewAppdomain", variable1.AsArgument("variable1"), variable2.AsArgument("variable2")))
{
    await domain.RunAsync(Execute);
    var result = await domain.GetValue<ResultType>("result");
}

...


public static void Execute()
{
    var variable1 = Domainr.GetValue<YourType>("variable1");
    
    //Or you could also get them all at once:    
    object[] values = Domainr.GetValues("variable1", "variable2");
    
    
    //Do stuff
    var result = ... ;
    
    result.SetValueInDomain("result");
}
```

When the domain instance disposes, it will automatically unload.