```shell
# 搜索镜像
docker search redis

# 拉取镜像
docker pull redis

# 查看镜像
docker image ls
docker image inspect redis

# 创建数据卷
docker volume create redisdata

# 查看数据卷
docker volume ls
docker volume inspect redisdata

# 创建容器(同时指定持久化及密码)
docker run -itd --name redis --restart=always -v redisdata:/data -p 6379:6379 redis redis-server --appendonly yes --requirepass password

# 查看容器
docker ps -a
docker container inspect redis

docker ps -a | grep redis
# 输出列名：CONTAINER ID,IMAGE,COMMAND,CREATED,STATUS,PORTS,NAMES

# 进入容器
docker exec -it redis bash
```

> redis在连接时，客户端请勿使用代理，否则无法连接