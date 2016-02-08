#!/bin/bash

containerName=northwind
imageName=romixch/npm_gulp

echo "Test if container $containerName is already running..."
if docker ps | grep northwind
then
  docker attach northwind
  exit
fi

echo "Test if there is already a stopped container called $containerName..."
if docker ps -a | grep northwind
then 
  docker start --attach northwind 
  exit
fi

echo "Try to create a container from image $imageName"
currentDir="$(pwd)"
docker run -ti --name $containerName -p 127.0.0.1:9876:9876 -p 127.0.0.1:3000:3000 -v $currentDir:/webdev $imageName /bin/bash /root/startScript