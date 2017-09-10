![Screenshot](SuperMarioClone.jpg?raw=true "Screenshot")

About
================
Basic implementation of Super Mario World clone in C# using the cross-platform 2D retro graphics engine [Tilengine](https://www.tilengine.org)

**Note**: SuperMarioClone is based on Tilengine v1.14, which is not the most up to date release. Current version 1.15 has some incompatibility that makes this sample unable to run under the new version, please don't update until the incompatibility is fixed.

Install
================
Before running, you must first copy the suitable Tilengine dlls to the project root folder (where the source filea sree located):
* In Windows 32-bit, copy files from lib\x86
* In Windows 64-bit, copy files from lib\x64

Running
================
* Press keyboard left/right or joystick D-Pad to move
* Press Z key or joystick button 1 for running
* Press X key or joystick button 2 for jumping. The longer you press, the higher is the jump
* Press ESC to exit