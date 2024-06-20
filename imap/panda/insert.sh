#!/bin/bash
set -e

useradd -m panda && printf "panda\npanda\n" | passwd panda
socat TCP4-LISTEN:143,reuseaddr,fork SYSTEM:'/panda/panda-imap/imapd/imapd' &> /dev/null &

p=$!

sleep 1

cat > commands <<EOL
A1 LOGIN panda panda
A2 CREATE TEST
B1 APPEND TEST {54}
Date: Wed, 12 Jun 2024 22:02:04 +1000
From: test@yc

D1 SELECT TEST
E1 LOGOUT
EOL

unix2dos commands
netcat localhost 143 < commands

kill "$p"
