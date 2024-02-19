#include <stdio.h>
#include <signal.h>
#include <unistd.h>

int main() {
    printf("stop current program %d\n", getpid());
    fprintf(stderr, "stop current program %d\n", getpid());
    sleep(2);
    kill(getpid(), SIGSTOP);
    printf("program should not be continued\n");
    return 0;
}
