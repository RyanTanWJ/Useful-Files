# DockerCompose Cheat Sheet

## Basic commands for using docker-compose
You can use DockerCompose to run all containers relevant to your application. With a ***CLI in the directory containing your docker-compose.yml***, use the following command.

  docker-compose up

To run multiple containers of the same service, you can use the ```--scale``` option.

  docker-compose up --scale <service-name>=<number-of-containers>
  
## DockerCompose yml options
