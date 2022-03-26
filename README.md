# Unity scripts

Some useful scripts for Unity Engine. <img src="https://github.com/owik100/Portfolio/blob/gh-pages/images/unity-69-logo-png-transparent.png" width="32" height="32">
<br/>
<br/>
## [ResolutionSettings](https://github.com/owik100/Unity/blob/master/ResolutionSettings.cs)  
It helps to create in-game screen resolution and full screen mode options.
### Instruction
Tested in Unity 2017.2.0f3 (64-bit).
<br/>
<br/>
Drag and drop reference to resolutionDropdown and fullScreenToggle.
On Dropdown 'on value changed' set ResolutionSettings -> SetResolution.
On Toggle 'on value changed' set ResolutionSettings -> SetFullScreen.

## [GenerateCameraColliders](https://github.com/owik100/Unity/blob/master/GenerateCameraColliders.cs)  
Creates colliders on each camera boundary. Resizes together with the camera.
### Instruction
Tested in Unity 5.5.2f1 (64-bit) and Unity 2017.1.1f1 (64-bit).
<br/>
<br/>
Just attach script to camera.
