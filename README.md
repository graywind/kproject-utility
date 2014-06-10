kproject-utility
================

A Gtk# utility for fetching the latest ASP.NET vNext snapshots.

build.sh executes mcs with the proper flags and outputs a single binary named kproject-utility.

On first launch, it checks and creates ~/.kproj/installs and ~/.kproj/packages if they do not exist.

XML Feed used: https://www.myget.org/F/aspnetvnext/api/v2/GetUpdates()?packageIds='KRE-mono45-x86'&versions='0.0'&includePrerelease=true&includeAllVersions=true

The download functionality downloads the selected nupkg to packages, unzips to a folder to a subdirectory in ~/.kproj/installs, makes the binaries executable and creates a symlink at ~/.kproj/bin

If you want to quickly switch between versions, add ~/.kproj/bin to your current PATH and hit the download button. If it detects that the install exists already it will just switch the symlink and not download again.

Note: This program executes the unzip command directly, if you receive an error there check that it exists.

![Downloading the nupkg](/screenshot.png?raw=true "Downloading the nupkg")
