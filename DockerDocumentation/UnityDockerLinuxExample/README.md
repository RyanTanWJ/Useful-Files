# How to run this example using Unity and Docker
This is a beginner's guide to understanding using Unity and Docker. This guide assumes you already have Docker installed.

The file, ```headless-test.x86_64``` and directory, ```headless-test_Data``` are built from a basic Unity Application that only has a single script. The build configuration is set for Linux and the "Server Build" option enabled. The script prints ```"HeadlessTest has started"``` followed by the CLI parameters entered within the ```dockerfile``` (see ```ENTRYPOINT```).

## Steps to run
1. Clone the repository or download just the UnityDockerLinuxExample directory.
2. Open Windows Powershell and navigate to the directory. You should see something like ```PS <your-file-path>/UnityDockerLinuxExample>``` in your Windows Powershell.
3. Enter the command ```docker-compose up```, you should see something similar to the following output:
```
    test-ubi8-unity | Set current directory to /opt/app-root/app
    test-ubi8-unity | Found path: /opt/app-root/app/root/headless-test.x86_64
    test-ubi8-unity | Mono path[0] = '/opt/app-root/app/root/headless-test_Data/Managed'
    test-ubi8-unity | Mono config path = '/opt/app-root/app/root/headless-test_Data/MonoBleedingEdge/etc'
    test-ubi8-unity | PlayerPrefs - Creating folder: /opt/app-root/.config/unity3d/DefaultCompany
    test-ubi8-unity | PlayerPrefs - Creating folder: /opt/app-root/.config/unity3d/DefaultCompany/Test
    test-ubi8-unity | Initialize engine version: 2018.4.21f1 (fd3915227633)
    test-ubi8-unity | Forcing GfxDevice: Null
    test-ubi8-unity | GfxDevice: creating device client; threaded=0
    test-ubi8-unity | NullGfxDevice:
    test-ubi8-unity |     Version:  NULL 1.0 [1.0]
    test-ubi8-unity |     Renderer: Null Device
    test-ubi8-unity |     Vendor:   Unity Technologies
    test-ubi8-unity | Begin MonoManager ReloadAssembly
    test-ubi8-unity | - Completed reload, in  0.045 seconds
    test-ubi8-unity | UnloadTime: 0.375000 ms
    test-ubi8-unity | HeadlessTest has started
    test-ubi8-unity |
    test-ubi8-unity | (Filename: ./Runtime/Export/Debug.bindings.h Line: 45)
    test-ubi8-unity |
    test-ubi8-unity | CLI Params:
    test-ubi8-unity | ./root/headless-test.x86_64
    test-ubi8-unity | -ab
    test-ubi8-unity | 12312
    test-ubi8-unity | --cdqe
    test-ubi8-unity | v33554ggte
    test-ubi8-unity |
    test-ubi8-unity |
    test-ubi8-unity | (Filename: ./Runtime/Export/Debug.bindings.h Line: 45)
```

## How to specify Command Line Arguments
See ```ENTRYPOINT``` at the end of the ```dockerfile```.
