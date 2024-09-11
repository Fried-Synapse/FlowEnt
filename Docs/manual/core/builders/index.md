# Builders

Builders are the proxy that helps you build an animation in the editor and with a simple call from the code side you can play the animation. Builders are separate entities from the animations and they are meant to convert editor data into animation data. 

The Options, Events, and other info can be found on the concepts pages.

##### Exposing builders from code

In order to enable the builder in the editor a small amount of code is needed. Here's a code snippet for a tween builder being exposed in a custom script and how that looks like in the editor.

``` csharp
public class TweenBuilderScript : MonoBehaviour
{
    [SerializeField]
    private TweenBuilder builder;

    private void Start()
    {
        builder.Build().Start();
    }
}
```

![Tween Builder Script](~/resources/samples/tween-builder-script.png)