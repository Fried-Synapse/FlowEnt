# Debugging

In order to debug and find out where possible issues are coming from when creating animations FlowEnt comes with a set of tools and helps that will allow you to easily discover any problem. 

### Previewer

You can use the [Previewer](~/manual/advanced/tools/previewer.md) to play the animation in edit mode as you build it.

### Inspector

The [Inspector](~/manual/advanced/tools/inspector.md) is a useful too that can be used in both play mode and edit mode, and it'll provide you with controls for the animation, but useful information about it that will help you trace the issue easier.

### Debug Motion

The debug motion can be attached to any Tween or Echo and will print to the console relevant information on the events of an animation, such as *start*, *update*, and *end*. Here's an example on how to use it using the extension method.

``` csharp
new Echo().Debug("Debug name");
```

### Other tricks

Naming animations could prove quite helpful for debugging as it'll provide a way to find out which animation is causing problems. For that just use the SetName function, or pass it as a setting.

``` csharp
new Flow().SetName("Character flow")
```