# Process isolation

Goal:
- `run.sh` runs program `stop` from `stop.c` with `timeout` command
- `stop.c` will `kill(0, SIGSTOP)`
- `run.sh` should not receive SIGSTOP and hang forever

i.e. We do not want child process to stop the parent script.

## Dump

Before finding `set -m`, looked into creating namespaces with `unshare`.

unshare -p --fork --mount-proc --map-root-user ls

strace\
https://github.com/containers/buildah/issues/1901\
https://github.com/containers/buildah/issues/1901#issuecomment-542546576

unshare -p --fork --user sh

docker run --security-opt seccomp=unconfined --security-opt systempaths=unconfined -it ubuntu /bin/bash

https://stackoverflow.com/questions/56573990/access-to-full-proc-in-docker-container

docker run --security-opt seccomp=/usr/share/containers/seccomp.json -it ubuntu:latest /bin/bash
