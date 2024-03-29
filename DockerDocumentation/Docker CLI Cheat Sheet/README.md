# Build a Docker image from a Dockerfile
With a ***CLI in the directory containing your dockerfile***, use the following command:

    docker build . -t <image-name>
    
# Quick-Start: Create a new container with an image as a base and opens a bash terminal (LINUX-SPECIFIC)

    docker run -ti <image-name> /bin/bash
    
```/bin/bash``` opens bash terminal for you to access the virtualised OS. ```-ti``` makes it possible to enter commands and for user interaction.

# Listing Images and Containers
Images are a saved state of an Operating System (OS) at a certain time. Images are read-only and **cannot** be run. Containers use an image as a base. As containers virtualise the running of an OS, a new Image can be created from a container.

## List all Images
Use the following command.

    docker images

###### Sample output:

    REPOSITORY                  TAG                 IMAGE ID            CREATED             SIZE
    a-container                 latest              2d12799ae068        2 days ago          10.4MB
    docker/docker101tutorial    latest              5c257ac7fd00        4 months ago        72.8MB
    tagged-container            latest              5c257ac7fd00        4 months ago        72.8MB

## List all Containers
Use the following command. You can exclude the ```-a``` option to view only running containers.

    docker ps -a

###### Sample output:

    CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS                         PORTS               NAMES
    b171a61e0028        a-container         "/bin/sh -c ./bin/ba…"   3 minutes ago       Exited (0) 9 minutes ago                           ecstatic_chatterjee
    dd55ea5bc7b5        tagged-container    "/bin/sh -c ./bin/ba…"   About an hour ago   Created                                            elated_wilbur

# Save an image to a tar archive and load it again on a different computer
## Save an image to a tar archive
Use the following command to save a docker image as a tar file on your machine

    docker save <image-name> -o <root-directory>\\<filename>.tar

E.g. This command saves the *docker101tutorial* image as *docker-tut.tar* on *Username*'s desktop
    
    docker save docker101tutorial -o C:\\Users\\Username\\Desktop\\docker-tut.tar
    
## Load an image from a tar archive
Use the following command to load a tar file containing a docker image into docker
    
    docker load -i <root-directory>\\<filename>.tar

### NOTE: Docker Redirection
While Docker supports redirection, "<" and ">" for input output, it is recommended to use the commands above to prevent errors like

    Error processing tar file(exit status 1): archive/tar: invalid tar header
    
 See this [Stack Overflow question](https://stackoverflow.com/questions/40622162/docker-load-and-save-archive-tar-invalid-tar-header) for more info.

### NOTE: Wrap path names with quotation marks, " ", if they contains spaces.

E.g. `docker save docker101tutorial -o "C:\\Users\\User Name\\Desktop\\docker-tut.tar"`
