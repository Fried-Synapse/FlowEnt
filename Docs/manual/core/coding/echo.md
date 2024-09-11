# Echo

Here we have some examples on how to build [Echoes](~/manual/concepts/echo.md) 

#### Basic
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating the echo</li>
        <li>Setting the timeout to 100 seconds</li>
        <li>Setting the stop condition based on a local flag</li>
        <li>Setting a delay of 4 seconds</li>
        <li>Adding an event that is executed before the start of the echo</li>
        <li>Adding an event that is executed after the completion of the echo</li>
        <li>Starting the echo</li>
    </ol>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Echo echo = new Echo()
    .SetTimeout(100f)
    .SetStopCondition((time) => echoShouldStopFlag)
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
        <li>Creating the echo with a timeout of 10s</li>
        <li>Selecting the object to be animated</li>
        <li>Applying the MoveTowards motion</li>
        <li>Starting the echo</li>
    </ol>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Echo echo = new Echo(10f)
    .For(transform)
        .MoveTowards(otherTransform)
    .Start();</code></pre>
  </div>
</div>

#### Character Control
This is an example of the many functionalities that can be achieved with echoes. This is controlling the character using simple input.
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating the echo for the CharacterController</li>
        <li>Applying the Move motion</li>
        <li>Applying the Rotate motion</li>
        <li>Starting the echo </li>
    </ol>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">Echo echo = CharacterController
    .Echo()
        .Move(10f)
        .Rotate(Camera.transform, 5f)
    .Start();</code></pre>
  </div>
</div>