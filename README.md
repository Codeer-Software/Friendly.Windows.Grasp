Friendly.Windows.Grasp
============================

This library is a layer on top of
Friendly, so you must learn that first.
But it is very easy to learn.

https://github.com/Codeer-Software/Friendly

## Getting Started
Install Friendly.Windows.Grasp from NuGet

    PM> Install-Package Codeer.Friendly.Windows.Grasp
https://www.nuget.org/packages/Codeer.Friendly.Windows.Grasp/

***
## Features ...
The main purpose is to search for window.<br>
To test, you first need to get the target Window.<br>
And sub use case, Win32 level operation can be performed to window.<br>

## Search for window
Use WindowControl.<br>
The top level window can be got using the static method.<br>
And the extension method of WindowsAppFriend is prepared, you can write like this.<br>
```cs  
var app = new WindowsAppFriend(process);

var topwLevelWindows = WindowControl.GetTopLevelWindows(app);
var mainWindow = WindowControl.WaitForIdentifyFromTypeFullName(app, "Target.MainWindow");

//extension method.
var topwLevelWindows2 = app.GetTopLevelWindows();
var mainWindow2 = app.WaitForIdentifyFromTypeFullName("Target.MainWindow");
```

You can also search for child windows.<br>
However, it is better to use field names for WinForms and Visual Tree or Logical Tree for Wpf.<br>
It's a good idea to use this for Win32 windows.<br>

#### Win32
Use Friendly.Windows.NativeStandardControls as the control driver.[See here](https://github.com/Codeer-Software/Friendly.Windows.NativeStandardControls).
```cs  
var mainWindow = app.WaitForIdentifyFromWindowText("Application Title");
var textBox = new NativeEdit(mainWindow.IdentifyFromDialogId(3));
```

#### WinForms
This is a basic feature of Friendly. [See here](https://github.com/Codeer-Software/Friendly).<br>
Use Friendly.FormsStandardControls as the control driver.[See here](https://github.com/ShinichiIshizuka/Ong.Friendly.FormsStandardControls/stargazers).
```cs  
var mainWindow = app.WaitForIdentifyFromWindowText("Target.MainWindow");
FormsTextBox textBox = mainWindow.Dynamic()._textBox;
```

#### WPF
This is a feature of Friendly.WPF Standard Controls. [See here](https://github.com/Roommetro/Friendly.WPFStandardControls).
```cs  
var mainWindow = app.WaitForIdentifyFromTypeFullName("Target.MainWindow");
//get by visual tree.
WPFTextBox textBox = mainWindow.VisualTree().ByBinding("Name").Single().Dynamic();
//Of course, if x:name is attached, it can also be obtained from the field.
WPFTextBox textBox2 = mainWindow.Dynamic()._textBox;
```

The name of the search function is a combination of the following two patterns.
|Behavior||
|----|----|
|Get|Get all windows that meet the specified conditions.|
|Identify|Get When Identify only one by the order condition. Else throw Exception.|
|WaitForIdentify|Wait for identify only one by the order condition. Useful for timing control.|

<br>

|Condition||
|----|----|
|WindowText|Text that can be acquired by GetWindowText of Win32 Api.|
|TypeFullName|.Net type full name|
|WindowClass|Win32 WindowClass|
|ZIndex|ZZindex. Multiple can be specified when the parent and child hierarchy of Window is deep.|
|DialogId|DialogId. Multiple can be specified when the parent and child hierarchy of Window is deep.|

<br>
* I used From instead of By because my English wasn't good at that time. Don't worry about it. 
<br>

### Search top level windows.
* GetTopLevelWindows
* GetFromWindowText
* IdentifyFromWindowText
* WaitForIdentifyFromWindowText
* GetFromTypeFullName
* IdentifyFromTypeFullName
* WaitForIdentifyFromTypeFullName
* GetFromWindowClass
* IdentifyFromWindowClass
* WaitForIdentifyFromWindowClass

### Search child windows.
* IdentifyFromZIndex
* IdentifyFromDialogId
* IdentifyFromWindowText
* IdentifyFromTypeFullName
* IdentifyFromWindowClass
* GetFromWindowText
* GetFromTypeFullName
* GetFromWindowClass
* GetChildren

### manipulate window.
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
//Sample Win32 
var process = Process.GetProcessesByName("NativeTarget")[0];  
using (var app = new WindowsAppFriend(process))  
{  
    //Wait for identify by window text.
    var testDlg = app.WaitForIdentifyFromWindowText("Target Application");

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
}  
```
```cs  
//Sample WinForms
var process = Process.GetProcessesByName("FormsTarget")[0];  
using (var app = new WindowsAppFriend(process))  
{  
    //Wait for identify by type full name.
    var mainForm = app.WaitForIdentifyFromTypeFullName("TargetApp.MainForm");

    //using Friendly.FormsStandardControls.
    //use filed.
    //Button
    FormsButton button = mainForm.Dynamic()._button;
    button.EmulateClick();

    //TextBox
    FormsTextBox textBox = mainForm.Dynamic()._textBox;
    textBox.EmulateChangeText("abc");
    
    //Tree
    FormsTreeView tree = mainForm.Dynamic()._tree;
    tree.GetItem("item1", "item2").EmulateSelect(); 
}  
```

