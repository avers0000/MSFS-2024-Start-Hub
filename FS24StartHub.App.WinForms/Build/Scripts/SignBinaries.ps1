param(
    [string]$TargetPath
)

$pfx = Join-Path $PSScriptRoot "..\Signing\AVErs.pfx"
$pwd = Get-Content (Join-Path $PSScriptRoot "..\Signing\password.txt")

& "C:\Program Files (x86)\Windows Kits\10\bin\10.0.22621.0\x64\signtool.exe" sign /f $pfx /p $pwd /fd sha256 /tr http://timestamp.digicert.com /td sha256 $TargetPath