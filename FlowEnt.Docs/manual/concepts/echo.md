# Echo

Echo is a type of animation that is executed every frame and provides a delta time to animate. It's much simpler than a tween and it doesn't have as many options. The idea is that it will execute something until it's told not to or it times out. Check out the examples below to see what you can do with this kind of animation.

## 1. Options
[!INCLUDE [options](animation.options.md)]
`Timeout` - A timeout can be set if there is a need to put a time limit on this echo. Similar to the Tween Time, but not mandatory. `[Default: None]`

`Loop Count` -  How many loops should this animation have(Ignored if there is no stop condition for the echo, such as timeout). `[Default: 1]`

## 2. Events
[!INCLUDE [events](animation.events.md)]

## 3. Motions
[!INCLUDE [motions](animation.motions.md)]