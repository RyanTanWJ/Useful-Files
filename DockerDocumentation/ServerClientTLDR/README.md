# How to use Docker to divide the load on a Unity server
This guide aims to outline how you can use docker to provide aspects of your game as microservices so as to offload the work done in each frame to different containers.

###### When following this guide, please ensure that your dockerfile and docker-compose.yml files are in the same directory as your Unity build.

## Overview of the process
1. Build your Unity Server, ServiceClient and UserClient Applications
2. [Set up your dockerfile](#set-up-your-dockerfile)
3. [Configure your ```docker-compose.yml``` file](#configure-your-docker-compose-file)
4. [Run in Windows Powershell](#run-in-windows-powershell)

## Set up your dockerfile
Here you will set up a dockerfile to build you Unity application.

### Create a dockerfile
Create a new file, and call it ```dockerfile```. It should not have any extension.

Use a text editor to populate it with:

    FROM <base-image>

    COPY <unity-app-name>.x86_64 ./root
    COPY <unity-app-name>_Data.x86_64 ./root/<unity-app-name>_Data


Replace ```<base-image>``` with the **base image** that you want to use.

Replace ```<unity-app-name>``` with the name of your **linux build**.

[**Reminder:** Your ```dockerfile``` should be located in the same directory where your build is](#when-following-this-guide-please-ensure-that-your-dockerfile-and-docker-composeyml-files-are-in-the-same-directory-as-your-unity-build) 

## Configure your docker-compose file
Create a ```docker-compose.yml``` file, populate it with the following:

    version: "3.8"

    services:
        unity_server:
            build:
                context: .
                dockerfile: <docker-file-path>
            image: <image-name>
            container_name: <container-name>
            entrypoint: ./root/<unity-app-name>.x86_64 <command-options>


Replace ```<docker-file-path>``` with the path to your created **dockerfile**.

Replace ```<image-name>``` with the name you want to give the **created image**.

Replace ```<container-name>``` with the name you want to give the **created container**.

Replace ```<unity-app-name>``` with the name of your **linux build**.

Replace ```<command-options>``` with any **options** that you want to use and have implemented in the Unity program. (e.g. ```-o 41```, ```--help```)

[**Reminder:** Your ```docker-compose.yml``` should be located in the same directory where you build is](#when-following-this-guide-please-ensure-that-your-dockerfile-and-docker-composeyml-files-are-in-the-same-directory-as-your-unity-build) 

## Run in Windows Powershell
The last step is simply to execute the command for everything to work in Windows Powershell.

    docker-compose up

## Problems?
Read the [full documentation](https://github.com/RyanTanWJ/Useful-Files/tree/master/DockerDocumentation/ServerClientExample).
