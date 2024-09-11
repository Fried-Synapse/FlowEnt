# Inspector

The inspector tool is a great resource to help you with debugging by allowing you to see inside an animation, what motions does it have and where the animation was created.

In order to open it just go to Tools > FlowEnt > Inspector. You shall now see a new window.

![Window](~/resources/tools/inspector/window.png)

On the top of the window there are some controls that can be used to control the *whole* animation engine.
Bellow a list of animations, that will be filled once animations are started.
This tool works both in play mode and in edit mode as well.

![Window with items](~/resources/tools/inspector/window-with-items.png)

Using the demo scene as an example, after a few second of playing, information about ongoing animations can seen

The main Flow that is used in the demo animation can be seen, with its inner flows and tweens. Upon first sight, we can see that the animations have some basic info attached, such as an Id. This is auto-incremented starting from 0 and it's used internally by the framework. Another thing is the Name. The second flow (Id: 1) has a name that is set using the Options for any animation(Flow, Tween, or Echo). Another useful thing is the "i"(info) button which will open the [Animation Info Window](#animation-info-window).

### Controls
Controls are using the IControllable interface.

**Play/Pause** - calls
``` csharp
IControllable.Play()
```
and
``` csharp
IControllable.Pause()
```

**Skip** - simulates a single frame by calling
``` csharp
IControllable.SimulateFrames(1)
```

**Stop** - calls
``` csharp
IControllable.Stop()
```

**Time scale** - sets's the global timescale using
``` csharp
IControllable.TimeScale
```


### Animation Info Window

Clicking on the info button for the character flow the following window can be seen. 

![Info](~/resources/tools/inspector/info.png)

In case you don't have debug enabled the following message will be displayed
>  <font color="#8bbce8">"Stack trace only available when debugging enabled. Please enable it in settings."</font>

This setting can be enabled from FlowEnt Settings. Once it's enabled the inspector will display the full stack trace as seen in the following image. 

![Info with stack](~/resources/tools/inspector/info-stack.png)

The controls can be used to manipulate this specific image while inspecting it.