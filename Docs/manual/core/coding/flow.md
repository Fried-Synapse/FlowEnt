# Flow

Here we have some examples on how to build [Flows](~/manual/concepts/flow.md) 

#### Basic
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating a new flow</li>
        <li>Queueing a tween for transform 1 to move 10 to the right</li>
        <li>Queueing a tween for transform 1 to move 10 up</li>
        <li>1 second after the flow started adding a tween to move transform 2 to 10 right and 10 up</li>
        <li>Queuing after the previous animation a tween to move transform 2 to 10 all over</li>
        <li>Starting the flow</li>
    </ol>
    <blockquote>
        <p><strong>Note:</strong> The tweens do not need to be started because the flow will control their start</p>
    </blockquote>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">
Flow flow = new Flow()
    .Queue(new Tween(2f).For(transform1).MoveX(10f))
    .Queue(new Tween(3f).For(transform1).MoveY(10f))
    .At(1f, new Tween(2f).For(transform2).MoveTo(new Vector3(10f, 10f, 0f)))
    .Queue(new Tween(3f).For(transform2).MoveTo(new Vector3(10f, 10f, 10f)))
    .Start();
    </code></pre>
  </div>
</div>

#### Deferred
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating a new flow</li>
        <li>Queueing a tween for transform 1 to move 10 to the right</li>
        <li>Queueing a callback to create a tween for transform 1 to move 10 up. The callback is executed only after the previous animation has finished(Time index: 2 sec)</li>
        <li>Adding a callback that is executed at time index 10 sec to create a tween to move transform 2 to 10 right and 10 up</li>
        <li>Starting the flow</li>
    </ol>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">
Flow flow = new Flow()
    .Queue(new Tween(2f).For(transform1).MoveX(10f))
    .QueueDeferred(() => new Tween(2f).For(transform1).MoveY(10f))
    .AtDeferred(10f, () => new Tween(2f).For(transform2).MoveTo(new Vector3(10f, 10f, 0f)))
    .Start();
    </code></pre>
  </div>
</div>

#### Inception
<div class="flex-container">
  <div class="flex-column">
    <ol>
        <li>Creating a new flow as inner flow</li>
        <li>Queueing a tween for transform 1 to move 10 up</li>
        <li>Empty line</li>
        <li>Creating a new flow</li>
        <li>Queueing a tween for transform 1 to move 10 to the right</li>
        <li>Queueing the inner flow</li>
        <li>Starting the flow</li>
    </ol>
    <blockquote>
        <p><strong>Note:</strong> The inner flow does not need to be started as the flow will call its start when its time comes to be played</p>
    </blockquote>
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">
Flow innerFlow = new Flow()
    .Queue(new Tween(2f).For(transform).MoveY(10f));

Flow flow = new Flow()
    .Queue(new Tween(2f).For(transform).MoveX(10f))
    .Queue(innerFlow)
    .Start();
    </code></pre>
  </div>
</div>

#### Awaiters
<div class="flex-container">
  <div class="flex-column">
If waiting for a task is needed in order to continue a flow, a "blocker" can be set to make the flow await till the item is finished. Available options are to await for a task, a flag callback, or custom awaiters can be created!
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">
Task task = Task.Run(() => Task.Delay(2000));

Flow flow = new Flow()
    .Queue(new Tween(1f).For(transform).MoveY(10f))
    .QueueAwaiter(task)
    .Queue(new Tween(1f).For(transform).MoveY(10f))
    .Start();
    </code></pre>
  </div>
</div>

#### Async
<div class="flex-container">
  <div class="flex-column">
Same flow as before, just async.
  </div>
  <div class="flex-column">
    <pre><code class="lang-csharp hljs language-csharp">
Flow flow = await new Flow()
    .Queue(new Tween(2f).For(transform).MoveX(10f))
    .Queue(new Tween(2f).For(transform).MoveY(10f))
    .StartAsync();
    </code></pre>
  </div>
</div>