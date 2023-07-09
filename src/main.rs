mod binding;

fn main() {
    unsafe {
        let pid = binding::get_pid();
        println!("pid: {}", pid);
    }

    println!("Hello, world!");
}
