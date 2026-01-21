[Setup]
AppId={{C6ABB831-C2EC-431B-B5D2-1FA95E670D57}}
AppName=MSFS 2024 Start Hub
AppVersion=0.0.0-dev
Publisher=AVErs
VersionInfoCompany=AVErs
DefaultDirName={autopf}\AVErs\FS24StartHub
ArchitecturesInstallIn64BitMode=x64os
OutputBaseFilename=FS24StartHub.Setup
Uninstallable=yes
DisableDirPage=no
CreateAppDir=yes
DisableProgramGroupPage=yes
Compression=lzma2
SolidCompression=yes
CloseApplications=yes
RestartApplications=no

[Tasks]
Name: "startmenuicon"; Description: "Create Start Menu shortcut"
Name: "desktopicon"; Description: "Create Desktop shortcut"

[Files]
Source: "Files\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; Excludes: ".keep"
Source: "Redist\windowsdesktop-runtime-9.0.12-win-x64.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

[Icons]
Name: "{autoprograms}\FS24StartHub"; Filename: "{app}\FS24StartHub.exe"; Tasks: startmenuicon
Name: "{autodesktop}\FS24StartHub"; Filename: "{app}\FS24StartHub.exe"; Tasks: desktopicon

[Run]
Filename: "{tmp}\windowsdesktop-runtime-9.0.12-win-x64.exe"; \
    Parameters: "/install /quiet /norestart"; \
    StatusMsg: "Installing .NET Desktop Runtime..."; \
    Check: NeedsDotNet

[Code]

function HasDesktopRuntime(major: string): Boolean;
var
  version: string;
begin
  Result :=
    RegQueryStringValue(
      HKLM,
      'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedfx\Microsoft.WindowsDesktop.App',
      'Version',
      version
    )
    and (Pos(major + '.', version) = 1);
end;

function NeedsDotNet(): Boolean;
begin
  Result := not (HasDesktopRuntime('8') or HasDesktopRuntime('9'));
end;