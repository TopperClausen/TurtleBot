docker stop turtle
docker rm turtle
docker rmi turtlebot

sudo docker ps

docker build --tag turtlebot .
docker run -d --name turtle turtlebot
