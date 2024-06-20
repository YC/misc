#!/bin/bash
set -e

/usr/sbin/cyrmaster &
p=$!

sleep 1

cat > commands <<EOL
A1 LOGIN test test
A2 CREATE INBOX
B1 APPEND INBOX {54}
Date: Wed, 12 Jun 2024 22:02:04 +1000
From: test@yc

D1 SELECT INBOX
E1 LOGOUT
EOL

unix2dos commands
netcat localhost 143 < commands

kill "$p"
