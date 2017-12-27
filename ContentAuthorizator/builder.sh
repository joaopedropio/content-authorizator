#!/bin/bash

# exit emmediately if a command fails with a non zero exit code(set -e) and to print shell input lines as they are read (set -v)
set -ev

PROJECT="content_authorizator"

CONTAINER=$(docker ps -a --filter "name=$PROJECT" -q);
IMAGE=$(docker images ${PROJECT} -q);
NETWORK=$(docker network ls --filter "name=$localnetwork" -q);
RUNNING=$(docker ps --filter "name=$PROJECT" -q);

if [ ! -z $CONTAINER ]
then
    echo "Removing container..."
    docker rm -f $CONTAINER
else
    echo "No container found."
fi

if [ ! -z $IMAGE ]
then
    echo "Removing image..."
    docker rmi $IMAGE
else
    echo "No image found."
fi

echo "Building the docker image.."
docker build -f Dockerfile -t $PROJECT .

if [ -z $NETWORK ]
then
    echo "No network found..."
    docker network create localnetwork
    echo "Network created."
else
    echo "Network found"
fi

echo "Running container..." 
docker run  --network=localnetwork -p 4000:4000  --name $PROJECT $PROJECT 

if [ ! -z $RUNNING ]
then
    echo "The container is up and runnig!"
    echo "Done!"
fi
