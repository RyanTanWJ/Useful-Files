# DockerCompose Cheat Sheet

## Basic commands for using docker-compose
You can use DockerCompose to run all containers relevant to your application. With a ***CLI in the directory containing your docker-compose.yml***, use the following command.

    docker-compose up

To run multiple containers of the same service, you can use the ```--scale``` option.

    docker-compose up --scale <service-name>=<number-of-containers>
  
## DockerCompose File Structure
The ```docker-compose.yml``` should begin with a ```version``` key specifying the DockerCompose version in use. Subsequently, use the ```services``` key to specify the services you wish to run.

    version: "3.8"
    services:
      <service-name>:
        image: <image-name>

Using the above docker compose file, a ```<service-name>``` container will be created using the ```<image-name>``` as a base image. The container will receive a default name of:

    <cwd>_<service-name>_1
    
## DockerCompose Options
### container-name
Use this to specify the name of the container created when running ```docker-compose up```.

    version: "3.8"
    services:
      <service-name>:
        image: <image-name>
        container-name: <container-name>
        
The container will then be named, ``<container-name>```, istead of ```<cwd>_<service-name>_1```
