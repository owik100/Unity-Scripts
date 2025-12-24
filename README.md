# Unity scripts

Some useful scripts for Unity Engine. <img src="https://github.com/owik100/Portfolio/blob/gh-pages/images/unity-69-logo-png-transparent.png" width="32" height="32">
<br/>
<br/>
## [SceneEditorManagement](https://github.com/owik100/Unity/blob/master/SceneEditorManagement.cs)  
The script allows you to quickly open scenes additively in the Unity Editor using a dedicated editor window. You can choose whether loaded scenes should be inactive, or whether only Tilemap scenes should remain active after loading.
### Instruction
Tested in Unity 2022.3.45f1 and Unity 6.3.
<br/>
<br/>
Place the script in the *Assets/Scripts/Editor folder*
Create the Editor folder if it does not already exist.

After the script is loaded by the Unity Editor, the settings window will be available from the menu:
*Tools → Scene Editor Management → Open All Scenes Settings Panel*

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

<br/>

## [GenerateCameraColliders](https://github.com/owik100/Unity/blob/master/GenerateCameraColliders.cs)  
Creates colliders on each camera boundary. Resizes together with the camera.
### Instruction
Tested in Unity 5.5.2f1 (64-bit) and Unity 2017.1.1f1 (64-bit).
<br/>
<br/>
Just attach script to camera.
