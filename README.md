#Overview
WinRunner lets you create various types of shortcuts which can be run using the Windows Run command. This can save precious time if you are a keyboard-heavy user.

Run command shortcuts are created by inserting an entry into the Windows Registry. Shortcuts can actually be created manually by modifying the Registry yourself. WinRunner simply makes the process easier by providing a convenient interface.

WinRunner relies on what is in the Registry to remember the shortcuts that have been created. It is not recommended to modify the Registry keys and values created by this program or you will risk breaking the shortcuts.

You can learn more about this process by visiting the following link: [Application Registration](https://msdn.microsoft.com/en-us/library/windows/desktop/ee872121(v=vs.85).aspx)

##Caution
WinRunner modifies the following Registry key:

`HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths`

Do NOT use WinRunner unless you fully understand and accept the implications of modifying the above Registry key.

WinRunner and its author(s) are NOT responsible or liable for any damages done to your computer when using this program.

#Shortcuts
Every shortcut has one thing in common, the run name. This name is what you have to type into the Run command in order to use the shortcut. The name must be unique across all shortcuts and have certain character restrictions.
There are a few types of shortcuts that WinRunner supports. The Registry itself only supports file shortcuts but WinRunner works around that to support additional types. All files that are created by this program are located in the current user's Documents folder.

##File
This will simply open the file using whatever default program that file type is set to use. This means executables will be executed, text files will be opened with an editor, images will be opened in photo viewers, etc.

##Folder
This will open the specified folder. This is done using a Batch file.

##Batch
This will execute the Batch script that you enter into the box. This is done using a Batch file.
