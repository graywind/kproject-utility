kproject-utility
================

A Gtk# utility for fetching the latest ASP.NET vNext snapshots.

build.sh executes mcs with the proper flags and outputs a single binary 
named kproject-utility.

For testing purposes this points to the updates.xml, if you want to
test against the live API switch the comments on FetchXml_Clicked for 
string url

The download functionality is incomplete and currently points to a Linux
iso for testing.

![First Screenshot](/screenshot.png?raw=true "First Screenshot")
