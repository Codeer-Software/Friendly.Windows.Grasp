Friendly.Windows.Grasp
============================

This library is a layer on top of
Friendly, so you must learn that first.
But it is very easy to learn.

https://github.com/Codeer-Software/Friendly.Windows

## Getting Started
Install Friendly.Windows.Grasp from NuGet

    PM> Install-Package Codeer.Friendly.Windows.Grasp
https://www.nuget.org/packages/Codeer.Friendly.Windows.Grasp/

***
## Features ...
WindowControl is a class for manipulating the Window with Window handle.
It also has a function to identify windows.

### Search top level windows.
* FromZTop
* GetTopLevelWindows
* IdentifyFromWindowText
* IdentifyFromTypeFullName
* IdentifyFromWindowClass
* WaitForIdentifyFromWindowText
* WaitForIdentifyFromTypeFullName
* WaitForIdentifyFromWindowClass
* GetFromWindowText
* GetFromTypeFullName
* GetFromWindowClass
* WaitForNextModal
* WaitForNextWindow
* GetNextWindows

### Search child windows.
* IdentifyFromLogicalTreeIndex
* IdentifyFromVisualTreeIndex
* IdentifyFromZIndex
* IdentifyFromDialogId
* IdentifyFromWindowText
* IdentifyFromBounds
* IdentifyFromTypeFullName
* IdentifyFromWindowClass
* GetFromWindowText
* GetFromBounds
* GetFromTypeFullName
* GetFromWindowClass

### manuplate window.
* IsWindow
* SetWindowText
* GetWindowText
* SetFocus
* SendMessage
* SequentialMessage
* PointToScreen
* Activate
* Close
* WaitForDestroy

### properties.
* AppVar
* App
* Handle
* DialogId
* WindowClassName
* TypeFullName
* ParentWindow
* Size
* AutoRefresh
***
```cs  
//sample  
var process = Process.GetProcessesByName("NativeTarget")[0];  
using (var app = new WindowsAppFriend(process))  
{  
    //Get z top window.
    var testDlg = app.FromZTop();

    //using Friendly.Windows.NativeStandardControls.
    //Get from dialog id.
    //Button
    var button = new NativeButton(testDlg.IdentifyFromDialogId(1004));
    button.EmulateClick();

    //Edit
    var edit = new NativeEdit(testDlg.IdentifyFromDialogId(1020));
    edit.EmulateChangeText("abc");
    
    //Tree
    var tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
    tree.EmulateEdit(tree.Nodes[0], "new text"); 

    //Identify from window text.
    var abcWindow = app.IdentifyFromWindowText("abc");
    abcWindow.Close();
}  
```
### More samples
https://github.com/Codeer-Software/FriendlyBaseTest/tree/master/Test

***
For other GUI types, use the following libraries:

* For Win32/MFC.  
https://www.nuget.org/packages/Codeer.Friendly.Windows.NativeStandardControls/

* For WPF.  
https://www.nuget.org/packages/RM.Friendly.WPFStandardControls/

* For WinForms.  
https://www.nuget.org/packages/Ong.Friendly.FormsStandardControls/  

***
If you use PinInterface, you map control simple.  
https://www.nuget.org/packages/VSHTC.Friendly.PinInterface/



