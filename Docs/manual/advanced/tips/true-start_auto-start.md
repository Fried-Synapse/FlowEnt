# True-start vs Auto-start

The current ideology for making a tween in today's Unity3D world is that they start automatically the next frame. This has some disadvantages and while FlowEnt provides the option to use auto-start, it encourages to use true-start

Here are the main differences:

1. **Control**: Auto-start does not provide full control over the creation and execution of an animation. With true-start the animation can be warmed up, by applying all its settings and starting it at a later state

2. **Performance**: Auto-start basically means an observer has to start and wait for the next frame to start and then trigger the start of the animation

3. **Skipped frame**: In order to auto-start with a fluent paradigm waiting till the end of the frame is mandatory in order to ensure that all the options of the tween were applied

4. **Async**: True-Start animations will provide the option to be awaited

## Examples

<table style="border: transparent 1px solid; ">
    <tr>
        <th>Auto-Start</th>
        <th>True-Start</th>
    </tr>
    <tr>
<td>

``` csharp
Tween autoStart = new Tween(1f, true)
    .For(transform)
        .Move(Vector3.one);
```

_notice the **true** parameter_
</td>
<td>

``` csharp
Tween trueStart = new Tween(1f)
    .For(transform)
        .Move(Vector3.one)
    .Start();

Tween trueStartAsync = await new Tween(1f)
    .For(transform)
        .Move(Vector3.one)
    .StartAsync();
```
</td>
    </tr>
</table>

