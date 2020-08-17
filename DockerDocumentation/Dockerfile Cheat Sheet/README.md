# Dockerfile Cheat Sheet
A beginner's guide to Dockerfile instructions.

## Choosing a base Image
A typical dockerfile begins with the ```FROM``` instruction. Use it to specify the base Image for subsequent instructions.

    FROM <image-name><:tag><@digest>
Note: ```:tag``` and ```@digest``` are optional, use it to specify a certain version of your image.

Example:

    FROM ubuntu:latest

This command specifies using ```ubuntu``` with the ```latest``` tag as the base image.

## Copying a file or directory into your container
You can use the ```COPY``` instruction to copy a file or directory to a location. **The behaviour differs depending whether it is used on a file or directory.**

### Copying a file
This copies the file specified in ```<file-path>``` into the ```<container-directory>```.

    COPY <file-path> <container-directory>

### Copying a directory
This copies the **contents** of the <directory-path> into the ```<container-directory>```.

    COPY <directory-path> <container-directory>
