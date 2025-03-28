/// <summary>
/// HashGenerator - Ein Kommandozeilenprogramm zur Generierung von Hashwerten
/// 
/// Das Programm nimmt einen Text als Kommandozeilenparameter entgegen und erstellt
/// einen Hashwert daraus. Beide Werte werden in einer Textdatei gespeichert.
/// Die Konfiguration erfolgt über die appsettings.json Datei.
/// 
/// Verwendung:
///     HashGenerator.exe "Zu hashender Text"
/// 
/// Konfiguration (appsettings.json):
/// - Algorithm: Hash-Algorithmus (MD5, SHA1, SHA256, SHA384, SHA512)
/// - OutputDirectory: Ausgabeverzeichnis für die generierten Dateien
/// - OutputFileExtension: Dateiendung der Ausgabedateien
/// - Encoding: Textkodierung für Ein- und Ausgabe
/// - EingabeAnzeigen: Steuert, ob der Eingabetext im Dateinamen erscheint
/// </summary>

using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

// Konfiguration aus appsettings.json laden
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// Konfigurationswerte auslesen mit Standardwerten falls nicht definiert
var hashSettings = config.GetSection("HashSettings");
var algorithm = hashSettings["Algorithm"] ?? "SHA256";
var outputDir = hashSettings["OutputDirectory"] ?? "./output";
var fileExtension = hashSettings["OutputFileExtension"] ?? ".txt";
var encoding = Encoding.GetEncoding(hashSettings["Encoding"] ?? "UTF-8");
var eingabeAnzeigen = bool.Parse(hashSettings["EingabeAnzeigen"] ?? "false");

// Überprüfung der Kommandozeilenparameter
if (args.Length == 0)
{
    Console.WriteLine("Bitte geben Sie einen Text als Parameter an.");
    return 1;
}

var inputText = args[0];

// Sicherstellen, dass das Ausgabeverzeichnis existiert
Directory.CreateDirectory(outputDir);

// Hash-Algorithmus basierend auf Konfiguration auswählen
HashAlgorithm hashAlgorithm = algorithm.ToUpper() switch
{
    "MD5" => MD5.Create(),
    "SHA1" => SHA1.Create(),
    "SHA256" => SHA256.Create(),
    "SHA384" => SHA384.Create(),
    "SHA512" => SHA512.Create(),
    _ => SHA256.Create() // Standardmäßig SHA256 verwenden
};

// Hash-Wert berechnen
var inputBytes = encoding.GetBytes(inputText);
var hashBytes = hashAlgorithm.ComputeHash(inputBytes);
var hashString = Convert.ToHexString(hashBytes);

// Ausgabedateinamen generieren
var filePrefix = eingabeAnzeigen ? inputText : "hash";
var outputPath = Path.Combine(outputDir, $"{filePrefix}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}");

// Eingabetext und Hash in Datei schreiben
await File.WriteAllLinesAsync(outputPath, new[] { inputText, hashString }, encoding);

Console.WriteLine($"Hash wurde erstellt und in {outputPath} gespeichert.");
return 0;
