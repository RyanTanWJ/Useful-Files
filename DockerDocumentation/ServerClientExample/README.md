# How to use Docker to divide the load on a Unity server
This guide aims to outline how you can use docker to provide aspects of your game as microservices so as to offload the work done in each frame to different containers.

###### When following this guide, please ensure that your dockerfile and docker-compose.yml files are in the same directory as your Unity build.

## Assumptions
This guide assumes you already have the following:
1. Docker is installed on your system.
2. A Unity application that accepts and processes command line arguments for configuration as:
    1. A Server build as a single source of truth
    2. A ServiceClient build that can run any of the independent services provided by your Unity application
    3. A UserClient build that can be used for visualising the server side state

## Overview of the process
1. [Build your Unity Server, ServiceClient and UserClient Applications](#build-your-unity-server-serviceclient-and-userclient-applications)
2. [Set up your dockerfile](#set-up-your-dockerfile)
3. [Configure your ```docker-compose``` file](#configure-your-docker-compose-file)
4. [Run in Windows Powershell](#run-in-windows-powershell)

## Build your Unity Server, ServiceClient and UserClient Applications
My Server and ServiceClients will be running in containers with the ```ubi8``` Operating System. I am using Docker on a ```Windows``` system, so my UserClient will be running on my ```Windows``` physical machine.

#### To build the Server and ServiceClient Application
1. In the Unity Editor, go to:
> File > Build Settings
2. Set **Target Platform** as ```Linux```
3. ***Enable*** **Server Build** or **Headless Mode** (depends on Unity editor version)
4. Click the "Build" button and select a directory to save it in

[**Reminder:** Your ```dockerfile``` and ```docker-compose.yml``` should be located in the same directory where you build this](#when-following-this-guide-please-ensure-that-your-dockerfile-and-docker-composeyml-files-are-in-the-same-directory-as-your-unity-build) 

#### To build the UserClient Application
1. In the Unity Editor, go to:
> File > Build Settings
2. Set **Target Platform** as ```Windows```
3. ***Disable*** **Server Build** or **Headless Mode** (depends on Unity editor version)
4. Click the "Build" button and select a directory to save it in

## Set up your dockerfile
Here you will set up a dockerfile to build you Unity application.

### Create a dockerfile
Create a new file, and call it ```dockerfile```. It should not have any extension.

[**Reminder:** Your ```dockerfile``` and ```docker-compose.yml``` should be located in the same directory where you build this](#when-following-this-guide-please-ensure-that-your-dockerfile-and-docker-composeyml-files-are-in-the-same-directory-as-your-unity-build) 

Your ```dockerfile``` should begin with:

    FROM <registry-path>/<image-name>

In this example, we will be using ```ubi8/ubi-minimal``` image as our base image to host our Unity Server and Service Client(s). So your ```dockerfile should begin with:

    FROM <registry-path>/ubi8/ubi-minimal

Replace ```<registry-path>``` accordingly to point to an image you would prefer to use. For this guide, we will use the default registry provided by redhat, ```registry.access.redhat.com```.

    FROM registry.access.redhat.com/ubi8/ubi-minimal

**Congratulations!** You have just written your first ```dockerfile```. However, right now, the image built from this dockerfile will be no different from the ```ubi8/ubi-minimal``` base image. We do something about that in the next step.

### Add your Unity application Executable and Data Directory to a Container
There is an important distinction between *Images* and *Containers* in docker. *Images* are read-only and cannot be run. What actually happens when you "run" an *image* in docker is that a new *Container* is created with the specified *image* as a base image. The *container* creates a read/write layer on top of the *image* allowing you to "modify" the state of the *image* and eventually save it as a new *image*. The concept is similar to Microsoft Word's Save As feature.

In this guide, we will be adding a Unity application to our container. We will make use of the ```COPY``` instruction to do so.

Within the ```dockerfile```, add the following lines:

    FROM registry.access.redhat.com/ubi8/ubi-minimal

    COPY <unity-app-name>.x86_64 ./root
    COPY <unity-app-name>_Data.x86_64 ./root/<unity-app-name>_Data

## Configure your docker-compose file
Here you will specify a set of instructions to start a UserClient on your physical machine, run containers with the ServiceClient with specific settings. Provided below is an example of a ```docker-compose.yml``` file, followed by the explanations for how to use the keys.

[**Reminder:** Your ```dockerfile``` and ```docker-compose.yml``` should be located in the same directory where you build this](#when-following-this-guide-please-ensure-that-your-dockerfile-and-docker-composeyml-files-are-in-the-same-directory-as-your-unity-build) 

    version: "3.8"

    services:
        unity_server:
            build:
                context: .
                dockerfile: ./DockerfileHere/Dockerfile
            image: ubi8_unity_img
            container_name: ubi8-unity-container
            entrypoint: ./root/headless-test.x86_64 -of 41

```version:``` specifies the ```docker-compose``` [file reference version](https://docs.docker.com/compose/compose-file/).

You can specify all the different services to provide under ```services:```. Do not let the name scare you. The defined services, such as ```unity_server``` are little more than names. Within each service you can configure how your service is built. Each of the defined services will create a new image.

```build:``` specifies the build context, where the instructions in your dockerfile take place. Without ```build```, docker will try to pull the image instead.

If you do not need to specify other options, ```context``` is not necessary. You can simple use:

```build: .```

You can use ```dockerfile:``` to specify a different dockerfile or even one that is not in your build context.

```image:```specifies the name of the image that docker. You can also give it a tag by adding ```:<tag>``` to the end. E.g. ```image: ubi8_unity_img: mytag```

After the image is built, docker will automatically create a container to run the new service images you've specified.

```container-name:``` can be used to set the container's name rather than using one of docker's randomly generated default names.

Lastly, ```entrypoint:``` can be used to define the ```ENTRYPOINT``` for docker and will override the ```ENTRYPOINT``` in the ```dockerfile``` if it exists. You can also add on any additional command line parameters, ```-a```, ```v```, ```--help```, that will be processed by the Unity application in the container.

**NOTE:** If you have already built an image once before, you may notice that your images are not being built again after making changes to your ```docker-compose.yml```. In order to rebuild these images you can either remove the images and their associated containers, or you can run ```docker-compose up --build```

## Run in Windows Powershell
The last step is simply to execute the command for everything to work in Windows Powershell.

    docker-compose up
