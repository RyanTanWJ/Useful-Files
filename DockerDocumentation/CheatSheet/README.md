# Save an image to a tar archive and load it again on a different computer
## Save an image to a tar archive
Use the following command to save a docker image as a tar file on your machine

    docker save <image-name> -o <root-directory>\\<filename>.tar

E.g. This command saves the *docker101tutorial* image as *docker-tut.tar* on *Username*'s desktop
    
    docker save docker101tutorial -o C:\\Users\\Username\\Desktop\\docker-tut.tar
    
## Load an image from a tar archive
Use the following command to load a tar file containing a docker image into docker
    
    docker load -i <root-directory>\\<filename>.tar

### NOTE: Wrap path names with quotation marks, " ", if they contains spaces.

E.g. `docker save docker101tutorial -o "C:\\Users\\User Name\\Desktop\\docker-tut.tar"`