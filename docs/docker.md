## Explication des commandes de Dockerfile

1. Base Image (Image de base):

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
```

Cette ligne spécifie l'image de base à utiliser. Ici, vous utilisez l'image ASP.NET Core 8.0, qui contient l'environnement d'exécution ASP.NET Core.

```dockerfile
WORKDIR /app
```

Définit le répertoire de travail dans le conteneur comme /app. Les commandes suivantes seront exécutées dans ce répertoire.

```dockerfile
EXPOSE 8080
EXPOSE 8081
```

Expose les ports 8080 et 8081 sur le conteneur. Cela n'ouvre pas les ports sur la machine hôte, mais cela permet à d'autres conteneurs de se connecter à ces ports.


2. Build Image (Image de construction):

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
```

Cette ligne spécifie une deuxième image à utiliser pour la construction de l'application. Ici, vous utilisez l'image SDK ASP.NET Core 8.0, qui contient l'environnement de développement nécessaire pour construire votre application.

```dockerfile
ARG BUILD_CONFIGURATION=Release
```

Définit un argument de construction BUILD_CONFIGURATION avec la valeur par défaut "Release". Cela permet de spécifier le mode de build lors de la construction de l'image.

```dockerfile
WORKDIR /src
```

Définit le répertoire de travail dans le conteneur pour la construction comme /src.

```dockerfile
COPY ["WeatherApi/WeatherApi.csproj", "WeatherApi/"]
```

Copie le fichier de projet (WeatherApi.csproj) dans le répertoire de travail.

```dockerfile
RUN dotnet restore "WeatherApi/WeatherApi.csproj"
```

Exécute la commande dotnet restore pour restaurer les dépendances du projet.

```dockerfile
COPY . .
```

Copie le reste du code source dans le répertoire de travail.

```dockerfile
WORKDIR "/src/WeatherApi"
```

Change le répertoire de travail pour celui du projet.

```dockerfile
RUN dotnet build "WeatherApi.csproj" -c $BUILD_CONFIGURATION -o /app/build
```

Exécute la commande dotnet build pour construire l'application dans le répertoire /app/build.


3. Publication (Publish):

```dockerfile
FROM build AS publish
```

Utilise l'image de construction précédente comme base pour la phase de publication.

```dockerfile
ARG BUILD_CONFIGURATION=Release
```

Redéfinit l'argument BUILD_CONFIGURATION pour cette phase.

```dockerfile
RUN dotnet publish "WeatherApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish
```

Exécute la commande dotnet publish pour publier l'application dans le répertoire /app/publish.


4. Image Finale:

```dockerfile
FROM base AS final
```

Utilise l'image de base comme point de départ pour l'image finale.

```dockerfile
WORKDIR /app
```

Définit le répertoire de travail dans le conteneur comme /app.

```dockerfile
COPY --from=publish /app/publish .
```

Copie le résultat de la phase de publication dans le répertoire de travail.

```dockerfile
ENV WEATHER_API_URL=WEATHER_API_URL
ENV WEATHER_API_KEY=WEATHER_API_KEY
```

Définit des variables d'environnement pour les clés API.

```dockerfile
ENTRYPOINT ["dotnet", "WeatherApi.dll"]
```

Définit la commande d'entrée à exécuter lorsque le conteneur démarre. Dans ce cas, cela lance votre application ASP.NET Core.


## Construire l'image Docker et la pousser vers Docker Hub

1. Créez un compte Docker Hub :

Assurez-vous d'avoir un compte sur [Docker Hub](https://hub.docker.com/). Si vous n'en avez pas, créez-en un.

2. Connectez-vous à Docker Hub :

Ouvrez un terminal et utilisez la commande `docker login` pour vous connecter à votre compte Docker Hub.

```bash
docker login
```

3. Construisez l'image Docker :

Assurez-vous d'être dans le répertoire où se trouve votre Dockerfile et exécutez la commande suivante pour construire l'image Docker. Remplacez <USERNAME> par votre nom d'utilisateur Docker Hub.

```bash
docker build -t macktireh/weatherapi:1.0 .
```

Cette commande crée une image avec le tag `1.0`

4. Vérifiez que l'image a été créée avec succès :

```bash
docker images
```

Assurez-vous de voir votre nouvelle image dans la liste.

5. Poussez l'image vers Docker Hub :

Utilisez la commande `docker push` pour pousser votre image vers Docker Hub. Remplacez `<USERNAME>` par votre nom d'utilisateur Docker Hub et `<TAG>` par le tag que vous avez utilisé lors de la construction.

```bash
docker push macktireh/weatherapi:1.0
```

6. Vérifiez l'image sur Docker Hub :

Visitez [Docker Hub](https://hub.docker.com/) dans votre navigateur et assurez-vous que votre image a été téléchargée avec succès.