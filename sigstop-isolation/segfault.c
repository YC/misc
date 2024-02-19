#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

int main() {
    printf("segfault program %d\n", getpid());
    fprintf(stderr, "segfault program %d\n", getpid());
    int* a;
    *a = 0;
    return 0;
}
