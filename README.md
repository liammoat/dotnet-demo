# .NET Demo

## Setup

1. Clone Project

    ```
    git clone https://github.com/liammoat/dotnet-demo.git
    ```

1. Run existing console application

    ```
    cd .\src\DotNetDemo.Console\
    dotnet run -- ".NET Community"
    ```

1. Publish and run a self-contained executable:

    ```
    dotnet publish --self-contained -c Release
    C:\<path-to-project>\bin\Release\net7.0\win-x64\publish\DotNetDemo.Console.exe ".NET Community"

    ```

## Run cross-platform

1. Publish a Ubuntu distro:

    ```
    dotnet publish --runtime ubuntu.20.04-x64 --self-contained -c Release
    ```

2. Copy and run the executable to WSL:

    ```
    cp /mnt/c/<path-to-project>/bin/Release/net7.0/ubuntu.20.04-x64/publish/DotNetDemo.Console ~/
    ./DotNetDemo.Console ".NET Community"
    ```