# Projekt Management

Dieses Programm dient als Referenz-Projekt. Es stellt kein produktionsfähiges System dar.
Das Programm kann im Development-Modus gestartet werden. Dazu ist eine Docker-Umgebung notwendig.

## Anleitung

Folgende Schritte sind nötig:

1. Downloaden Sie das Projekt und entpacken Sie es.

2. Erstellen Sie eine .env Datei. Als Beispiel nutzen Sie die .env.example Datei.

3. Öffnen Sie im Projektordner eine Konsole und bauen Sie das Docker-Image für das Programm. 
   Folgender Befehl muss dazu ausgeführt werden inklusive dem Punkt:
   docker build -t project_management_api -f ".\ProjectManagement.Api/Dockerfile" .

4. Starten Sie das Projekt.
   Führen Sie folgende Befehle dazu aus:
   docker compose build,
   docker compose up -d

5. Öffnen Sie Ihren Browser und geben Sie folgende URL ein:
   http://localhost:8080/scalar/v1

   Alternativ können Sie auch in Docker-Desktop auf das Portmapping klicken und im Browser nachträglich /scalar/v1 eingeben.

6. Beenden Sie die Ausführung.
   Führen Sie folgenden Befehl dazu aus:
   docker compose down

## Umsetzung

Für die Umsetung wurden folgende Technologien gewählt:

- ASP.NET Core
- Docker
- Postgresql
- Entity Framework Core
- FluentResults
- FluentValidation
- Scalar
- Serilog

## Verwendung

Im Projektmanagement-Tool ist die Grundlage das Projekt. Legen Sie dieses daher zuerst an.
Danach können Sie dem Projekt ein Board zuweisen, Gruppen im Board erstellen und Karten (Aufgaben) erstellen.

Beim Status sind für das Projekt und die Karte folgende Eingaben gültig:

- Open
- InProgress
- Completed

Auf die Groß- und Kleinschreibung muss nicht geachtet werden.