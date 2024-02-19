# Process isolation

Goal:
- `run.sh` runs program `stop` from `stop.c` with `timeout` command
- `stop.c` will `kill(0, SIGSTOP)`
- `run.sh` should not receive SIGSTOP and hang forever
