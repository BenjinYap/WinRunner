# WinRunner
This is a Windows program that lets users register applications in the Windows Registry for use with the Run command.

When you register an application in the `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths` subkey in the Windows Registry, you provide a name and the application path. This allows you to quickly start the application by typing in the name in the Run command (Windows + R). This isn't restricted to executables. This can be done with any file and the computer will use the default program to open the file.

WinRunner makes this process easier by providing a nicer graphical interface for adding, modifying, and deleting applications in this Registry subkey. This program does not alter anything else in the Registry and does not create any files except for a user preferences file in the Documents folder.

Upon starting WinRunner it will search the Registry for any registered applications and populate its list with them. This means that WinRunner does not keep track of any external changes to the Registry.

Do NOT use WinRunner unless you know what you are doing and how modifying the above Registry key impacts your computer. This is NOT meant for basic Windows users.

You can learn more about this process by visiting [Application Registration](https://msdn.microsoft.com/en-us/library/windows/desktop/ee872121(v=vs.85).aspx).
