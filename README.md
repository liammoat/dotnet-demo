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

1. Copy and run the executable to WSL:

    ```
    lsb_release -a
    cp /mnt/c/<path-to-project>/bin/Release/net7.0/ubuntu.20.04-x64/publish/DotNetDemo.Console ~/
    ./DotNetDemo.Console ".NET Community"
    ```

## Let's go to the web

1. Create a new .NET Web App:

    ```
    cd .\src\
    dotnet new webapp --name DotNetDemo.Web
    cd ..
    dotnet sln add .\src\DotNetDemo.Web\
    cd .\src\DotNetDemo.Web\
    dotnet add reference ..\DotNetDemo.Core\
    ```

1. Update `Index.cshtml.cs`:

    ```
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using DotNetDemo.Core;

    namespace DotNetDemo.Web.Pages;

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string Greeting;

        public void OnGet()
        {
            Greeting = GreetingService.GetGreeting(".NET Community", "Good afternoon");
        }
    }
    ```

1. Update `Index.cshtml`:

    ```
    <h1 class="display-4">@Model.Greeting</h1>
    ```

## How about Containers?

1. Add `Dockerfile` to the root of `DotNetDemo.Web`:

    ```
    FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
    WORKDIR /app
    EXPOSE 80
    EXPOSE 443

    FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
    WORKDIR /src
    COPY ["src/DotNetDemo.Web/DotNetDemo.Web.csproj", "src/DotNetDemo.Web/"]
    COPY ["src/DotNetDemo.Core/DotNetDemo.Core.csproj", "src/DotNetDemo.Core/"]
    RUN dotnet restore "src/DotNetDemo.Web/DotNetDemo.Web.csproj"
    COPY . .
    WORKDIR "/src/src/DotNetDemo.Web"
    RUN dotnet build "DotNetDemo.Web.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "DotNetDemo.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "DotNetDemo.Web.dll"]
    ```

1. From the solution root run a `docker build`:

    ```
    docker build -t dotnet-demo/web --file .\src\DotNetDemo.Web\Dockerfile .
    ```

2. Run the web app using `docker run`

    ```
    docker run -d -p 8080:80 dotnet-demo/web
    ```
