# Previewer

The FlowEnt Previewer helps you visualise animations(scripted or build in the editor) right in the scene, without hitting play.
This is great when you want to build an animation and want to see step by step the process, or when you want to debug it and see it frame by frame.

To open the previewer go to the FlowEnt menu and open the window from there.

![Menu](~/resources/tools/previewer/menu.png)

Once the window is open select a GameObject that has animations attached and you should see them in the window similar to this screenshot

![Window](~/resources/tools/previewer/window.png)

The previewer will scan all the scripts all the scripts attached to the currently selected GameObject and it displays any FlowEnt animation it finds. It scans for the following
- **Builder Serialised Fields**
- **Fields**
- **<font color="#4387ae">Properties</font>**
- **<font color="#a1a144">Parameterless Method</font>**

Each animation in this list will have controls provided which can be used to play/pause the animation but as well view frame by frame details.

### Focused Mode

The previewer can also be accessed through the button that is available in Unity inspector's on the top-right of the animation. This will enable a focus mode that will only preview the selected animation.
It can be used for animations inside a flow which could ease debugging by previewing animations within without the others playing.

![Focus](~/resources/tools/previewer/focus.png)