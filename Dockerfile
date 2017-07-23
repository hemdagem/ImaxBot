FROM microsoft/dotnet:1.1-sdk
WORKDIR /app

ARG source
# copy and build everything else
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "/app/ImaxBot.Console/out/ImaxBot.Console.dll", "ImaxBot", "$SLACK_TOKEN"]