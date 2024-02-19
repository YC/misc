#!/bin/bash

cc -o stop stop.c
cc -o stop_current stop_current.c
cc -o segfault segfault.c
cc -o normal normal.c

# timeout --preserve-status --kill-after=5s 5s \
#     unshare --net --pid --fork --mount-proc --map-root-user \
#     tini -- ./stop
# echo $?

# timeout --preserve-status --kill-after=5s 5s \
#     unshare --net --pid --fork --map-root-user \
#     tini -- ./stop
# echo $?

# timeout --preserve-status --kill-after=5s 5s \
#     unshare -p --fork --mount-proc --map-root-user \
#     tini -- ./segfault
# echo $?

echo "normal"
set -m
timeout --preserve-status --kill-after=10s 10s ./normal
echo $?
set +m
echo ""

set -m
timeout --preserve-status --kill-after=10s 10s ./stop
echo $?
set +m
echo ""

set -m
timeout --preserve-status --kill-after=10s 10s ./stop_current
echo $?
set +m
echo ""

set -m
timeout --preserve-status --kill-after=10s 10s ./segfault
echo $?
set +m
