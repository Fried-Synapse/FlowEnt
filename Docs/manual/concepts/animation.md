# Animation

The **Animation** is the core concept for FlowEnt and is built out of 3 distinct parts:
 - Options
 - Events
 - Functionality


## 1. Options
This part is meant to define how the intended functionality will behave.

Options can vary from animation to animation, but some of them are universal for all animations, such as the `Time Scale` or `Loop Count`, and some of them are animation specific such as `Easing` or `Time` for tweens. 

## 2. Events
Events are callback that can be used to link into specific times of the animation, to execute specific actions before the animation has started(`OnStarted`) or just before it ended(`OnCompleting`).

## 3. Functionality
This part differs from animation to animation, for example for [tweens](~/manual/concepts/tween.md) and [echoes](~/manual/concepts/echo.md) you can add [motions](~/manual/concepts/motion.md) but for [flows](~/manual/concepts/flow.md) you can add other animations under queues. This will allow the creation of complex animations using the same options that can interact with multiple unity components.