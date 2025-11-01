# Wix 5 Stack Overflow Reproduction

## Steps
1. Execute `build.ps1` to build the bundle

2. Execute the bundle:
```powershell
$proc = Start-Process -File TestBundle.exe -Argument '/l Test.log' -PassThru
$proc.ExitCode
```

## Result
- The Exit Code is -1073741571 (Stack Overflow)
- There will be an Application Error event logged in Event Viewer/Windows Logs/Application. e.g.:
```
Faulting application name: TestBundle.exe, version: 1.0.0.0, time stamp: 0x68b0cc75
Faulting module name: KERNEL32.DLL, version: 10.0.19041.5915, time stamp: 0xeee0fc1a
Exception code: 0xc00000fd
Fault offset: 0x0000000000003c8b
Faulting process ID: 0xbf0
Faulting application start time: 0x01dc4ae0388682e6
Faulting application path: <REDACTED>\TestBundle.exe
Faulting module path: C:\Windows\System32\KERNEL32.DLL
Report ID: 51ca53bc-e986-4caa-9dc5-778b76acd09d
Faulting package full name: 
Faulting package-relative application ID: 
```

## How to fix
- Do not create variables which reference itself.
- The variable printing at the bottom of the log is sorted, so it can be used to deduce which variable is broken.
