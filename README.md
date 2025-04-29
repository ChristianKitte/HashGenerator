# HashGenerator

Ein Kommandozeilenprogramm zur Generierung von Hashwerten aus Texteingaben. Das Programm erstellt für einen übergebenen Text einen Hashwert und speichert beide Werte in einer Textdatei.

**Hinweis:** Dieses Programm wurde mit Hilfe von Windsurf erstellt.

## Über Windsurf

Windsurf ist eine KI-getriebene integrierte Entwicklungsumgebung (IDE), die Entwicklern dabei hilft, effizienter zu programmieren, indem sie intelligente Vorschläge und Automatisierungen bietet.

## Features

- Unterstützung verschiedener Hash-Algorithmen (MD5, SHA1, SHA256, SHA384, SHA512)
- Konfigurierbare Ausgabeverzeichnisse und Dateiendungen
- Flexible Textkodierung
- Optionale Einbindung des Eingabetextes in den Dateinamen
- Selbstständig ausführbar (keine .NET-Installation erforderlich)

## Installation

1. Laden Sie die neueste Version des HashGenerator herunter
2. Entpacken Sie den Inhalt des `publish`-Ordners in ein Verzeichnis Ihrer Wahl
3. Die Anwendung ist sofort einsatzbereit, keine weitere Installation notwendig

## Verwendung

```powershell
HashGenerator.exe "Zu hashender Text"
```

Beispiel:
```powershell
HashGenerator.exe "Hallo Welt"
```

Die Ausgabe erfolgt in eine Textdatei im konfigurierten Ausgabeverzeichnis. Der Dateiname setzt sich zusammen aus:
- Dem Eingabetext (wenn `EingabeAnzeigen: true`) oder "hash"
- Dem aktuellen Datum und der Uhrzeit
- Der konfigurierten Dateiendung

Die erzeugte Datei enthält zwei Zeilen:
1. Den ursprünglichen Eingabetext
2. Den berechneten Hashwert

## Konfiguration

Die Konfiguration erfolgt über die Datei `appsettings.json`. Folgende Einstellungen sind verfügbar:

| Parameter | Beschreibung | Standardwert | Mögliche Werte |
|-----------|-------------|--------------|----------------|
| Algorithm | Der zu verwendende Hash-Algorithmus | SHA256 | MD5, SHA1, SHA256, SHA384, SHA512 |
| OutputDirectory | Verzeichnis für die Ausgabedateien | ./output | Beliebiger gültiger Pfad |
| OutputFileExtension | Dateiendung der Ausgabedateien | .txt | Beliebige Dateiendung (z.B. .hash) |
| Encoding | Textkodierung für Ein- und Ausgabe | UTF-8 | Beliebige gültige Kodierung |
| EingabeAnzeigen | Steuert, ob der Eingabetext im Dateinamen erscheint | false | true, false |

Beispiel `appsettings.json`:
```json
{
  "HashSettings": {
    "Algorithm": "SHA256",
    "OutputDirectory": "./output",
    "OutputFileExtension": ".txt",
    "Encoding": "UTF-8",
    "EingabeAnzeigen": true
  }
}
```

## Fehlerbehebung

1. **Fehler: "Bitte geben Sie einen Text als Parameter an."**
   - Sie haben keinen Text zum Hashen übergeben
   - Lösung: Übergeben Sie den Text in Anführungszeichen als Parameter

2. **Ausgabeverzeichnis existiert nicht**
   - Das Programm erstellt das Verzeichnis automatisch
   - Stellen Sie sicher, dass der Benutzer Schreibrechte hat

3. **Ungültiger Algorithmus in der Konfiguration**
   - Bei ungültigen Algorithmen wird automatisch SHA256 verwendet
   - Überprüfen Sie die Schreibweise in der `appsettings.json`

## Technische Details

- Entwickelt in C# mit .NET 9
- Selbstständig ausführbar (self-contained)
- Verwendet die neuesten Kryptographie-Bibliotheken
- Konfiguration über Microsoft.Extensions.Configuration

## Sicherheitshinweise

- MD5 und SHA1 gelten als kryptographisch unsicher und sollten nur für nicht-sicherheitskritische Anwendungen verwendet werden
- Für sicherheitskritische Anwendungen wird SHA256 oder höher empfohlen
- Hashwerte werden in Großbuchstaben ausgegeben (hexadezimal)
