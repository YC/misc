#!/bin/bash

cleanup() {
    docker kill panda-test
    docker container rm panda-test
}

docker build . -t panda-test

cleanup
docker run --name panda-test --detach -p 8143:143 panda-test

# Wait for startup
sleep 2

netcat localhost 8143 <<EOL
A1 LOGIN panda panda
A2 SELECT TEST
A3 UID FETCH * BODY.PEEK[]
E1 LOGOUT
EOL

cleanup
