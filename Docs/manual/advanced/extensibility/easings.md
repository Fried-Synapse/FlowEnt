# Easings

Easings are probably the most important setting inside an animation as it will define how the animation will act. There are many options already available in the library, including using an animation curve, but you can create your own.

#### Create
<div class="flex-container">
  <div class="flex-column">
In the first file, we can see an example of an easing being created by implementing the IEasing interface
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">public class CustomEasing : IEasing
{
    public float GetValue(float t)
        => Mathf.Clamp01(t * 2);
}</code></pre>
  </div>
</div>

#### Apply
<div class="flex-container">
  <div class="flex-column">
Easings can be applied using the <code>SetEasing(IEasing easing)</code> method for tweens
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Tween tween = new Tween(1f).SetEasing(new CustomEasing());</code></pre>
  </div>
</div>