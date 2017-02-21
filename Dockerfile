FROM microsoft/dotnet:1.0.1-sdk-projectjson
WORKDIR /dotnetapp

COPY . .
RUN ./all.sh

#ENTRYPOINT ["./run.sh"]

