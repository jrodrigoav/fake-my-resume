FROM node:22-alpine3.20 as frontend
WORKDIR /build
COPY code/frontend/fakemyresume/package.json .
RUN npm install
COPY code/frontend/fakemyresume/ .
RUN npm run ci-build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.20 as backend
WORKDIR /build
COPY code/backend/ .
COPY --from=frontend /build/dist/browser/ /build/FakeMyResume/wwwroot/
RUN cd FakeMyResume && dotnet publish --configuration Release --output publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.20 as final
WORKDIR /app
COPY --from=backend /build/FakeMyResume/publish/ .
COPY --from=backend /build/FakeMyResume/fake-my-resume.sqlite .
EXPOSE 80
ENV TZ=America/Mexico_City
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "FakeMyResume.dll"]