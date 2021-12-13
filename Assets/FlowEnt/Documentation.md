<div align="center">

[![Banner](https://github.com/Fried-Synapse/FlowEnt/blob/main/Assets/FlowEnt/Demo/Content/Banner/Textures/Banner.png)](https://flowent.friedsynapse.com)
  
</div>

<h1 align="center">FlowEnt</h1>

<div align="center">
  
[![Website](https://img.shields.io/badge/-Website-blue)](https://flowent.friedsynapse.com)
[![Email](https://img.shields.io/badge/-Support-blue)](mailto:flowent@friedsynapse.com)

FlowEnt is a tweening library based on the fluent paradigm meant to provide a framework for tweening in a simple or complex fashion but also allowing its users to create their own "motions" based on their needs.
The library is revolving around the concept of reusability and extensibility.
  
</div>

# 🛠 Installation 

FlowEnt requires no special configuration and can be used straight after installation.

# 💻 Usage 

## Tweens 

Tweens are the core of animations and they have 3 parts: Options, Events, and Motions.

### Options
The tween options are used to create a tween and set specific parameters for the tween.

### Events
The events are used to register callbacks for when specific, tween-related events happen.

### Motions
Motions are the essence of the animations. They are used to register the animation using the tween parameters

#### Here's an example of a tween:
```c#
Tween tween = new Tween(10f)
    .SetLoopType(LoopType.PingPong)
    .SetLoopCount(4)
    .SetDelay(4f)
    .OnStarting(() => Debug.Log("Before start."))
    .OnCompleted(() => Debug.Log("After completion!"))
    .For(transform1)
        .Move(Vector3.one)
    .For(transform2)
        .Move(Vector3.one * 2)
    .For(transform3)
        .Rotate(Vector3.up * 360)
        .Scale(Vector3.one * 3)
    .Start();
```

## Flows

The concept of flows exists because tweening is never enough and we sometimes need to link multiple tweens together to create an animation.
You can create an animation by queueing up tweens one after another in a flow, but also you can thread those queues, all within one flow.

#### Here is an example of a flow:
```c#
Flow flow = new Flow()
    .Queue(new Tween(2f).For(transform1).MoveX(10f))
    .Queue(new Tween(3f).For(transform1).MoveY(10f))
    .At(1f, new Tween(2f).For(transform2).MoveTo(new Vector3(10f, 10f, 0f)))
    .Start();
```

You can also queue other flows inside a flow
```c#
Flow innerFlow = new Flow()
    .Queue(new Tween(2f).For(transform).MoveY(10f));

Flow flow = new Flow()
    .Queue(new Tween(2f).For(transform).MoveX(10f))
    .Queue(innerFlow)
    .Start();
```

# 📚 Additional Info

More info can be found on FlowEnt's website. 
