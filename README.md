# Projekt Management

Dieses Programm dient als Referenz-Projekt. Es stellt kein produktionsfähiges System dar.
Das Programm kann im Development-Modus gestartet werden. Dazu ist eine Docker-Umgebung und Postman notwendig.
Postman wird für das Abfragen des Access-Token benötigt.

Ich arbeite stetig an dem Projekt. Es können daher Änderungen erfolgen.

## Anleitung

Folgende Schritte sind nötig:

1. Downloaden Sie das Projekt und entpacken Sie es.

2. Öffnen Sie im Projektordner eine Konsole und bauen Sie das Docker-Image für das Programm. 
   Folgender Befehl muss dazu ausgeführt werden inklusive dem Punkt:
   docker build -t project_management_api -f ".\ProjectManagement.Api/Dockerfile" .

3. Starten Sie das Projekt.
   Führen Sie folgende Befehle dazu aus:
   - docker compose build
   - docker compose up -d

4. Richten Sie Keycloak ein. Öffnen Sie dazu Keycloak über das Portmapping in Docker-Desktop und geben Sie die Admin-Anmeldedaten ein.
   Sie finden diese in der Docker-Compose-Datei. Klicken Sie auf die Schaltfläche "Manage realms".
   Klicken Sie dann auf die blaue Schaltfläche "Create realm". Klicken Sie jetzt im Fenster, welches sich geöffnet hat,
   auf die Schaltfläche "Browse". Navigieren Sie jetzt in den Projektordner. Dort finden Sie die JSON-Datei "realm-export.json".
   Wählen Sie diese aus. Die Datei wurde geladen und das JSON wird Ihnen angezeigt. Klicken Sie jetzt
   auf die blaue Schaltfläche "Create". Das Realm wird daraufhin erstellt.
   Auf der linken oberen Seite finden Sie die Anzeige "Current realm". Dort muss jetzt "myrealm" angezeigt werden.
   Ist dies nicht der Fall, klicken Sie auf "Manage realms" und wählen Sie dann "myrealm" aus.
   
5. Erzeugen Sie ein Client-Secret. Wählen Sie dazu das Realm "myrealm" aus. Klicken Sie dann links im Menü auf die Schaltfläche "Clients".
   Suchen Sie nach der Client-ID "project-management" und wählen Sie diese aus. Klicken Sie dann auf den Tab "Credentials". Sie finden dort
   das Feld "Client-Secret". Rechts neben den Feld finden Sie die Schaltfläche "Regenerate". Klicken Sie auf diese. Damit wird ein neues Client-Secret erzeugt.

   **Wichtig:** Wenn Sie diesen Schritt nicht ausführen wird die Authentifizierung fehlschlagen!

   Kopieren Sie sich das Client-Secret. Dieses wird bei der Abfrage des Access-Tokens benötigt.

6. Fragen Sie jetzt das Access-Token über Postman ab. Führen Sie folgende Schritte dazu aus: 
   1. Fügen Sie diese Adresse ein http://localhost:8081/realms/myrealm/protocol/openid-connect/token .
   2. Wählen Sie die HTTP-Methode "POST" aus.
   3. Wählen Sie den Tab "Body" aus.
   4. Wählen Sie das Format "x-www-form-urlencoded" aus.
   5. Geben Sie folgende Daten ein:
      - client_id - project-management
      - client_secret - Tragen Sie hier das kopierte Client-Secret ein.
      - grant_type - client_credentials
   
   Das angefragte Access-Token wird Ihnen im Response angezeigt. Kopieren Sie sich den Inhalt innerhalb der " " vom Access-Token. Das Access-Token wird zur Authentifzierung benötigt.

7. Öffnen Sie Ihren Browser und geben Sie folgende URL ein:
   http://localhost:8080/scalar/v1

   Alternativ können Sie auch in Docker-Desktop auf das Portmapping klicken und im Browser nachträglich /scalar/v1 eingeben.

   Oben finden Sie auf der rechten Seite das Feld "Authentication". Darunter wird das Eingabefeld "Bearer Token" angezeigt. Fügen Sie hier das kopierte Access-Token ein.

   Das Access-Token wird jetzt bei jedem Request mitgeschickt.

8. Beenden Sie die Ausführung.
   Führen Sie folgenden Befehl dazu aus:
   docker compose down

## Umsetzung

Für die Umsetung wurden folgende Technologien gewählt:

- ASP.NET Core
- Docker
- Postgresql
- Entity Framework Core
- Keycloak
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