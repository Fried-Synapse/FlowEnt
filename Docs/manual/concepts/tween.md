# Tween

Tweens are probably the most popular know animation type in the game industry. The concept is very basic: `tween` comes from "in-between" and it refers to the idea of interpolating a sequence of frames between 0 and 1(0%->100%) which is the animation's timeline.
This interpolation can be manipulated in may ways using the options provided.

## 1. Options
[!INCLUDE [animation.options](animation.options.md)]
`Time` - The length of the animation in seconds. `[Default: 1]`

`Easing Type` -  The type of easing modifier you want applied. Two options: Predefined([View more](https://easings.net/)) or Animation Curve(Unity's). `[Default: Predefined]`

`Easing`/`Easing Curve` - If `Easing Type` is set to Predefined then you can pick from a list otherwise you can use the Animation Curve widget to define one.`[Default: Linear]`

`Loop Count` -  How many loops should this animation have. `[Default: 1]`

`Loop Type` -  Two options: Ping-Pong or Reset. Ping-Pong means it's do one look forward and the next backwards and so on. Reset will do all loops starting from the initial point. `[Default: Reset]`

## 2. Events
[!INCLUDE [animation.events](animation.events.md)]

## 3. Motions
[!INCLUDE [animation.motions](animation.motions.md)]