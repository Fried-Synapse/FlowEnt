# Settings

![Settings](~/resources/advanced/settings.png)

The settings window will hold some general, library wise settings. FlowEnd is pretty self-sufficient and doesn't need a lot of setup. It only has 2 settings and they are both related to debugging

### Debugging
The way these two settings work, is by adding a compilation symbol to the project. This allows animations to track their starting point when an exception happens on update.

`Debugging in Editor` - Enables the debugging flag while in the editor only. Recommended to be left enabled as it doesn't affect live build performance and it gives full details of where the exception within an animation started. `[Default: false]`

`Debugging always` - Enables the debugging flag *all the time*. Recommended to be left disabled as it affects build performance. `[Default: false]`


