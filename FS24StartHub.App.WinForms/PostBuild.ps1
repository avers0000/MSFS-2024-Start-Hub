param([string]$TargetPath)

$TargetDir = Split-Path $TargetPath
$sign = Join-Path $PSScriptRoot "Build\Scripts\SignBinaries.ps1"

& $sign -TargetPath $TargetPath

$exe = [System.IO.Path]::ChangeExtension($TargetPath, ".exe")
if (Test-Path $exe) {
    & $sign -TargetPath $exe
}

$dst = Join-Path $PSScriptRoot "..\FS24StartHub.Setup\Files"
Remove-Item -Recurse -Force "$dst\*" -ErrorAction SilentlyContinue
Get-ChildItem $TargetDir -File | Where-Object { $_.Extension -ne ".pdb" } | Copy-Item -Destination $dst -Force

$iss = Join-Path $PSScriptRoot "..\FS24StartHub.Setup\Setup.iss"
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" $iss

$setup = Join-Path $PSScriptRoot "..\FS24StartHub.Setup\Output\FS24StartHub.Setup.exe"
& $sign -TargetPath $setup