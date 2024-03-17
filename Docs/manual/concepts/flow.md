# Flow

A Flow is a set of animations that can be synchronised and threaded together to create more complex animation. They are very useful for scalability as themselves are an animations which means you can have a flow within a flow. A flow can have multiple queues, which will run in parallel and each queue has a set of animations that are played in sequence.

## 1. Options
[!INCLUDE [animation.options](animation.options.md)]
`Loop Count` -  How many loops should this animation have. `[Default: 1]`

## 2. Events
[!INCLUDE [animation.events](animation.events.md)]

## 3. Queues
Each queue will play play a set of animations, in a sequence(one after the other), and all queues will run in parallel. Here's a visual example for this

#### Example
```mermaid
flowchart TD

A{Start Queue A
No Delay} --> A1(Animation A1) --> A2(Animation A2) --> A3(Animation A3) --> A4(Animation A4)
B{Start Queue B
Delay 3 sec} --> B1(Animation B1) --> B2(Animation B2) --> B3(Animation B3)
C{Start Queue C
Delay 5 sec} --> C1(Animation C1) --> C2(Animation C2) --> C3(Animation C3) --> C4(Animation C4)
```