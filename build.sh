#!/bin/sh
mcs myget.cs main.cs -pkg:gtk-sharp-3.0 -pkg:dotnet -r:Mono.Posix.dll -out:kproject-utility

