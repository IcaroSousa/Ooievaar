FROM mcr.microsoft.com/dotnet/core/runtime:2.1 AS runtime
WORKDIR /app

COPY /docker-entrypoint.sh ./
COPY /Publish/ ./

EXPOSE 5000

RUN \
  chmod a+x /app/docker-entrypoint.sh && \
  chmod 777 *


ENTRYPOINT ["/app/docker-entrypoint.sh"]
CMD [ "dotnet", "/app/Ooievaar.Server.dll" ]