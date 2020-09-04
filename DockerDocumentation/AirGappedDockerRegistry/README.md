# Creating a Docker registry in an Air Gapped (Offline) Environment
To create your own local Docker Registry to push and pull your images to, you need to run a Docker container using the ```registry``` image from [Docker Hub](https://hub.docker.com/_/registry).

Use ```docker pull registry``` to get the latest registry image.

## Docker Registry Image File
Alternatively, you may use the ```dockerRegistry.tar``` file provided to load the image into your development environment. ```dockerRegistry.tar``` is a compressed file containing the Docker ```registry``` image. The provided ```registry``` version is ```2.7.1```.

After downloading ```dockerRegistry.tar```, use the following command to load the image into docker:

    docker load -i "<file-path>\dockerRegistry.tar"
    
Where ```<file-path>``` is the directory where you saved ```dockerRegistry.tar```.
