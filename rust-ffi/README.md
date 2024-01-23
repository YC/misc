# C in Rust FFI
My experiment of compilation and usage of C in Rust.

## Development
```sh
cargo build
```

Then, `binding.rs` can be copied from somewhere within `target/debug/build`.\
This accommodates simple cases of compilation without `cargo`.

## Build for release
With `cargo`:
```sh
cargo build --release
```

Without `cargo`:
```sh
make
```
