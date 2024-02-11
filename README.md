# Weather Forecasting API

Bienvenue dans WeatherApi, un projet ASP.NET Core 8 permettant d'interagir avec une API météo tierce.

## Configuration (sans Docker)

1. Copiez le fichier `.env.example` en tant que `.env` et renseignez les valeurs appropriées pour les clés API météo.

    ```plaintext
    WEATHER_API_URL=
    WEATHER_API_KEY=
    ```

2. Assurez-vous que les variables d'environnement `WEATHER_API_URL` et `WEATHER_API_KEY` sont définies.

### sans Docker

3. Installez les dépendances nécessaires avec la commande :

    ```bash
    dotnet restore
    ```

4. Lancez l'application avec la commande :

    ```bash
    dotnet run
    ```

### avec Docker

5. Construisez et lancez l'application avec Docker en utilisant la commande :

    ```bash
    docker-compose up --build
    ```

Pour arrêter l'application Docker, utilisez la commande :

    ```bash
    docker-compose down
    ```

6. Accédez à Swagger pour explorer les endpoints de l'API :

    [http://localhost:8080/swagger](http://localhost:8080/swagger)


## Endpoints

### Recherche de lieux

Endpoint : `/api/search`

- **Méthode** : GET
- **Paramètres** :
  - `q` (obligatoire) : La requête de recherche.
  - `lang` (facultatif) : La langue de la réponse (par défaut, "en").
- **Exemple** : [http://localhost:5000/api/search?q=Paris](http://localhost:5000/api/search?q=Paris)

### Prévisions météo

Endpoint : `/api/forecast`

- **Méthode** : GET
- **Paramètres** :
  - `q` (obligatoire) : La requête de recherche.
  - `days` (facultatif) : Le nombre de jours de prévisions (par défaut, 3).
  - `lang` (facultatif) : La langue de la réponse (par défaut, "en").
- **Exemple** : [http://localhost:5000/api/forecast?q=Paris](http://localhost:5000/api/forecast?q=Paris)


```bash
docker run --name weatherapi -p 8080:8080 -p 8081:8081 -e WEATHER_API_URL=ssss -e WEATHER_API_KEY=ssss weatherapi
```

```bash
docker run --name weatherapi -p 8080:8080 -p 8081:8081 --env-file .env weatherapi weatherapi
```