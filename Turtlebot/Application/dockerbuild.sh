docker stop turtlebot
docker rm turtlebot
docke rmi turtlebot

docker build -t turtlebot .
docker run -d --name turtle turtlebot