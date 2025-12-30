# Motions

[Motions](~/manual/concepts/motion.md) are at the core of this library and the way they were thought was to be able to add as many as possible and customise them in any way you want. Here are some examples on of how to do that

#### Create
<div class="flex-container">
  <div class="flex-column">
In the first file, there is an example of a motion that is built for an AnimationObject
    <blockquote>
        <p><strong>Note:</strong> Only OnUpdate is mandatory for overriding. Other events available(virtual) are OnStart, OnLoopComplete, OnComplete</p>
    </blockquote>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">public class CustomMotion : AbstractTweenMotion<AnimationObject>
{
    public CustomMotion(AnimationObject item, float value) : base(item)
    {
        this.value = value;
    }
&nbsp;
    private readonly float value;
    private float from;
    private float to;
&nbsp;
    public override void OnStart()
    {
        from = item.Value;
        to = from + value;
    }
&nbsp;
    public override void OnUpdate(float t)
    {
        item.Value = Mathf.LerpUnclamped(from, to, t);
    }
}</code></pre>
  </div>
</div>

#### Extensions
<div class="flex-container">
  <div class="flex-column">
An example of creating custom extensions to make things easier
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">public static TweenMotionProxy<AnimationObject> Custom(this TweenMotionProxy<AnimationObject> proxy, float value)
    => proxy.Apply(new CustomMotion(motion.Item, value));</code></pre>
  </div>
</div>

#### Apply
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Appling the motion to an AnimationObject animationObject using the extension method</li>
        <li>Appling the motion directly to the tween</li>
    </ol>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Tween tweenWithExtension = new Tween(1f).For(animationObject).Custom(1f);
Tween tweenWithApply = new Tween(1f).Apply(new CustomMotion(animationObject, 1f));</code></pre>
  </div>
</div>