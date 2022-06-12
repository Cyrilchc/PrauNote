# PrauNote

## Quelques prérequis pour développer

### Dotnet
#### Installer : 
[Installer le sdk dotnet 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

#### Vérifier : 
```
dotnet
```
```
dotnet --version
```
### Outils EntityFramework Core
#### Installer
```
dotnet tool install --global dotnet-ef
```
#### Vérifier
```
dotnet ef
```
```
dotnet ef --version
```
### Migrations et création de la base de données
#### Ajouter une migration :
```
dotnet ef migrations add nomdelamigration
```
#### Appliquer les migrations / Créer la base de données
```
dotnet ef database update
```
### Lancer le projet 
#### Lancer en mode développement avec compilation :
```
cd Api\
dotnet run
```
##### OU compiler puis lancer : 
```
cd Api\
dotnet build
cd bin\Debug\net6.0\
dotnet Api.dll
```