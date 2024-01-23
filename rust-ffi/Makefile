CC=cc
CFLAGS=-O3
RUSTC=rustc
RUST_FLAGS=-O -C opt-level=3 -C panic=abort -C embed-bitcode=no

main: src/main.rs liblibc.rlib libprocess_helper.a
	$(RUSTC) $(RUST_FLAGS) --extern=libc=liblibc.rlib -L native=. -l static=process_helper -o main src/main.rs

libprocess_helper.a: process_helper.o
	ar -rsc libprocess_helper.a process_helper.o

process_helper.o: src/process_helper.c
	$(CC) $(CFLAGS) -c $< -o process_helper.o

vendor/libc-0.2.147/src/lib.rs: | vendor/libc-0.2.147.tar.gz
	tar xf vendor/libc-0.2.147.tar.gz --directory vendor/

liblibc.rlib: vendor/libc-0.2.147/src/lib.rs
	$(RUSTC) $(RUST_FLAGS) --crate-type=lib $<

clean:
	rm -rf target/ *.o *.a *.rlib main vendor/libc-0.2.147
