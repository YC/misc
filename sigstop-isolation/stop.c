#include <stdio.h>
#include <signal.h>
#include <unistd.h>

int main() {
    printf("stop program %d\n", getpid());
    fprintf(stderr, "stop program %d\n", getpid());
    sleep(2);
    kill(0, SIGSTOP);
    printf("program should not be continued\n");
    return 0;
}
