# Tween

Here we have some examples on how to build [Tweens](~/manual/concepts/tween.md) 

#### Basic
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating the tween with a time duration of 10s</li>
        <li>Setting the loop type to PingPong(back and forth)</li>
        <li>Setting the loop count to 4</li>
        <li>Setting a delay of 4 seconds</li>
        <li>Adding an event that is executed before the start of the tween</li>
        <li>Adding an event that is executed after the completion of the tween</li>
        <li>Starting the tween</li>
    </ol>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Tween tween = new Tween(10f)
    .SetLoopType(LoopType.PingPong)
    .SetLoopCount(4)
    .SetDelay(4f)
    .OnStarting(() => Debug.Log("Before start."))
    .OnCompleted(() => Debug.Log("After completion!"))
    .Start();</code></pre>
  </div>
</div>

#### Simple Motion
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating the tween with a time duration of 1s</li>
        <li>Selecting the object to be animated</li>
        <li>Applying the Move motion</li>
        <li>Starting the tween </li>
    </ol>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Tween tween = new Tween(1f)
    .For(transform)
        .Move(Vector3.one)
    .Start();</code></pre>
  </div>
</div>

#### Multiple Motions
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating the tween with a time duration of 1s</li>
        <li>Selecting the object to be animated</li>
        <li>Applying the Move motion</li>
        <li>Applying the Rotate motion</li>
        <li>Applying the Scale motion</li>
        <li>Starting the tween</li>
    </ol>
    <blockquote>
        <p><strong>Note:</strong> All 3 motions are applied at the same time, for the same object. They are all part of the same tween</p>
    </blockquote>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Tween tween = new Tween(1f)
    .For(transform)
        .Move(Vector3.one)
        .Rotate(Vector3.up * 360)
        .Scale(Vector3.one * 3)
    .Start();</code></pre>
  </div>
</div>

#### Multiple Objects
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating the tween with a time duration of 1s</li>
        <li>Setting the easing to EaseInSine</li>
        <li>Selecting the first object to be animated</li>
        <li>Applying the Move motion</li>
        <li>Selecting the second object to be animated</li>
        <li>Applying the Move motion</li>
        <li>Selecting the third object to be animated</li>
        <li>Applying the Rotate motion</li>
        <li>Applying the Scale motion</li>
        <li>Starting the tween</li>
    </ol>
    <blockquote>
        <p><strong>Note:</strong> The tween settings are applied to all the motions for all the objects</p>
    </blockquote>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Tween tween = new Tween(1f)
    .SetEasing(Easing.EaseInSine)
    .For(transform1)
        .Move(Vector3.one)
    .For(transform2)
        .Move(Vector3.one * 2)
    .For(transform3)
        .Rotate(Vector3.up * 360)
        .Scale(Vector3.one * 3)
    .Start();</code></pre>
  </div>
</div>

#### Extensions
_In order to apply a tween to an object straight away, you can use the "Tween" extension method._

<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Selecting the object to be animated</li>
        <li>Applying the extension method(available for any object) and setting the time</li>
        <li>Applying the Move motion</li>
        <li>Applying the Rotate motion</li>
        <li>Applying the Scale motion</li>
        <li>Starting the tween</li>
    </ol>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Tween tween = transform
    .Tween(1f)
    .SetTimeScale(2f)
        .Move(Vector3.one)
        .Rotate(Vector3.up * 360)
        .Scale(Vector3.one * 3)
    .Start();</code></pre>
  </div>
</div>

#### Async
<div class="flex-container">
  <div class="flex-column">
        In the first example, a tween is being awaited upon staring it.
        </br>
        In the second example, a tween is started, something else is done, and if the tween is not yet finished, it can be awaited to finish.
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">await new Tween(1f)
    .For(transform)
    .SetSkipFrames(20)
        .Move(Vector3.one)
    .StartAsync();</code></pre>
    <pre><code class="lang-csharp hljs language-csharp">Tween tween = new Tween(1f)
    .For(transform)
    .SetSkipFrames(20)
        .Move(Vector3.one)
    .Start();

await DoSomethingElseAsync();

await tween.AsAsync();</code></pre>
  </div>
</div>
