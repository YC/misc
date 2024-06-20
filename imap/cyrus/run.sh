#!/bin/bash

cleanup() {
    docker kill cyrus-test
    docker container rm cyrus-test
}

docker build . -t cyrus-test

cleanup
docker run --name cyrus-test --detach -p 8143:143 cyrus-test

# Wait for startup
sleep 2

netcat localhost 8143 <<EOL
A1 LOGIN test test
A2 SELECT INBOX
A3 UID FETCH * BODY.PEEK[]
E1 LOGOUT
EOL

cleanup
