$runtime = "windowsdesktop-runtime-9.0.12-win-x64.exe"

# Path to the Redist folder inside the Setup project
$redist = Join-Path $PSScriptRoot "..\FS24StartHub.Setup\Redist"
$runtimePath = Join-Path $redist $runtime

if (-not (Test-Path $runtimePath)) {
    Write-Host ""
    Write-Host "ERROR: Required .NET Desktop Runtime installer not found." -ForegroundColor Red
    Write-Host "Expected file:" -ForegroundColor Yellow
    Write-Host "  $runtimePath"
    Write-Host ""
    Write-Host "Download the runtime from:"
    Write-Host "  https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-9.0.12-windows-x64-installer"
    Write-Host ""
    Write-Host "Then place the file into the FS24StartHub.Setup\\Redist folder."
    Write-Host ""
    exit 1
}

Write-Host "Runtime found: $runtime"