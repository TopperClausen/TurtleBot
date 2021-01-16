docker stop turtle
docker rm turtle
docke rmi turtlebot

docker build -t turtlebot .
docker run -d --name turtle turtlebot