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

Arguments can also be build more easily using the builder.

```csharp
Type type = ...
string value = ...

Argument[] arguments = Arguments.Build(myType: type, myString: value)
```

The parameter name will be used as the key to retrieve them.

```csharp
var values = Domainr.GetValues("myType", "myString");
```

It's also possible to ommit names. In this case, a preset name will be used as key. This preset is a concatination of `value.` and the key number, starting with 0;

```csharp
Type type = ...
string value = ...

Argument[] arguments = Arguments.Build(type, value)
var values = Domainr.GetValues("value.0", "value.1");
```

Mixing is also allowed.

```csharp
Argument[] arguments = Arguments.Build(type, myString: value)
var values = Domainr.GetValues("value.0", "myString");
```