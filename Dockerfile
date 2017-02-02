FROM microsoft/dotnet:1.1.0-sdk-projectjson
WORKDIR /dotnetapp

COPY . .
RUN ./all.sh

#ENTRYPOINT ["./run.sh"]

