# Authoring

In order to build animation without writing a single line code there are 4 authoring scripts:
1. `Tween Authoring` - component with 1 tween.
2. `Echo Authoring` - component with 1 echo.
3. `Flow Authoring` - component with 1 flow.
4. `Animations Authoring` - component with a list of animations.

#### Options

 Each of them are components that can be added to a game object and with a few settings you are ready to go. Here are the options available:

 `Start Mode` - Which Unity event should it be used to start the animation. Possible options:
- Awake
- Start
- OnEnable
- Custom

`Delay` - How many seconds should it wait since the start event till the animation is started.

`Auto Destroy` - Should the animation be killed or not if the object was deleted.

`Trigger On Completed` - Should the "OnComplete" callback be triggered if the animation was stopped before it's completion.

`Builder(s)` - Authoring scripts use the [Builders](~/manual/core/builders/index.md) in order to expose the animations to the editor. This means that if you create your own motions with their builders they will be read available in here.