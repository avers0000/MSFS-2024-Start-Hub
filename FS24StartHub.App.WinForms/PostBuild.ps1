$src = Join-Path $PSScriptRoot "bin\Debug\net9.0-windows10.0.19041.0"
$dst = Join-Path $PSScriptRoot "..\FS24StartHub.Setup\Files"

Remove-Item -Recurse -Force "$dst\*" -ErrorAction SilentlyContinue

Get-ChildItem $src -File |
    Where-Object { $_.Extension -ne ".pdb" } |
    Copy-Item -Destination $dst -Force

& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" (Join-Path $PSScriptRoot "..\FS24StartHub.Setup\Setup.iss")