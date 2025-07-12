# docker pull redis

docker run --name my-redis -p 6379:6379 -d redis

## If you want to persist data:

docker run --name my-redis -p 6379:6379 -v redis-data:/data -d redis

1. name my-redis: Names the container.

2. -p 6379:6379: Maps Redis default port.

3. -d: Runs in detached mode.

# Used Redis publisher subscriber techniques
