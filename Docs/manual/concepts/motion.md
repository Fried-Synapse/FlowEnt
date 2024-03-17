# Motion

The motion is probably the most important concept in here. It's the motion that gets things moving when it comes to the animation. 
It uses the options and events of the animations to generate a unique and atomic movement based on a linear extrapolation, which means the synchronisation of movements can be done much easier, without having to copy the settings over and over.
The way the motions are build provide the scalability and extensibility of FlowEnt.

The basic principle, based on the linear extrapolation means that you have a start and an end for each of the motions. Most of them have `from` and `to`, or `value`("by") parameters, which will describe the said start and end. This means there are a total of 4 cases:

1. `from -> to` - This means you provide a value where to start and where to end. The current position will be ignored.
2. `current -> to` - This means the motion will start wherever the current state is and it'll go towards the `to` state.
3. `current by value` - This mean it'll create a `to` based on "adding" the `value` to the current state.
4. `custom` - Some motions require a bit more complex functionality and don't abide to the previous 3  cases.

_"What if I need a very peculiar motion?"_
Have no worry! With very little code you can write your own motion and even create a builder for it so you can use it in the editor. Check out more [here](~/manual/extensibility/motions.md)