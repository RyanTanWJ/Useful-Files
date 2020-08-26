# How to use Docker to divide the load on a Unity server
This guide aims to outline how you can use docker to provide aspects of your game as microservices so as to offload the work done in each frame to different containers.

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

    FROM registry.access.redhat.com/ubi8/ubi-minimal

    COPY <unity-app-name>.x86_64 ./root
    COPY <unity-app-name>_Data.x86_64 ./root/<unity-app-name>_Data

## Configure your docker-compose file
Create a ```docker-compose.yml``` file, populate it with the following:

    version: "3.8"

    services:
        unity_server:
            build:
                context: .
                dockerfile: ./DockerfileHere/Dockerfile
            image: ubi8_unity_img
            container_name: ubi8-unity-container
            entrypoint: ./root/headless-test.x86_64 -of 41

## Run in Windows Powershell
The last step is simply to execute the command for everything to work in Windows Powershell.

    docker-compose up

## Problems?
Read the [full documentation](https://github.com/RyanTanWJ/Useful-Files/tree/master/DockerDocumentation/ServerClientExample).
