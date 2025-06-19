# Vergleich virtueller Arbeitsumgebungen - VR vs. MR

### Im Rahmen meiner Bachelorarbeit an der Hochschule für Technik und Wirtschaft Berlin wurde diese Unity-Anwendung entwickelt. Ihr Zweck besteht in der Gegenüberstellung zweier Arbeitsräume, die in MR und VR umgesetzt wurden.

Die Anwendung wurde für die Meta Quest 3 entwickelt und enthält somit Quest 3 exclusive Features.

## Projektstruktur

Im Folgenden werden die relevantesten Ordner des Projekts erwähnt:

**Assets/** # Hauptordner für Unity-Assets  
**│── Audio/** # Sound-Dateien  
**│── Graphics/** # Sprites & Screenshots  
**│── Materials/** # Importierte und selbst erstellte Materialien  
**│── PlaceablePrefabs/** # GameObject-Vorlagen für das Selbstgestalten des Raums  
**│── Prefabs/** # GameObject-Vorlagen für das Raumdesign  
**│── Scenes/** # Enthält die 3 Szenen: MR_Scene, VR_Scene, MenuScene (.unity)  
**│── Scripts/** # Enthält alle relevanten C#-Skripte  
**│── Shaders/** # Erstellte Shader

## Projekt-Setup

1.  Es wurde die Unity Version **[Unity 2022.3.37f1]** verwendet (https://unity.com/de/releases/editor/whats-new/2022.3.37#installs)
2.  Das **Meta XR All-in-One SDK** wurde in der **Version 71** eingesetzt (https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657#version-current)
3.  Nach dem Herunterladen bzw. Clonen des Projekts sollten alle Dependencies automatisch installiert werden. Je nach Betriebssystem, muss unter File> BuildSettings> die Platform auf Android gewechselt werden (falls nicht schon passiert)
4.  Falls man vom **Project Setup Tool** gefragt wird, welches Handmodell verwendet werden soll -> **OpenXR Hand Skeleton**. Des Weiteren sollten alle der vorgeschlagenen Project Settings die das Tool anbietet, mit Apply/Fix bestätigt werden.
5.  Sind alle 3 Szenen ausgewählt (Menü-Szene als 1. Szene), kann ein Build gemacht werden um die Anwendung anschließend auf der Meta Quest 3 testen zu können. Unter Windows-Betriebssystemen kann die Anwendung auch über Quest Link getestet werden.
6.  Wird die Anwendung gestartet, befindet man sich in der Menü-Szene, über welche man in die beiden Räume gelangen kann.

## Features

- _Steuerung ausschließlich via Handtracking_
- _Trainingsbereich in der Menü-Szene_
- _Handmenü mit Optionen der Raumdekoration in der VR- und MR-Szene_
- _Jeweils ein Building Task in beiden Szenen_
- _Handgesten zum Wechseln gewisser visueller Effekte_

## Steuerung

- _Poke-, Grab-, Snap- und Ray Interaction via Hand_
- _Pinch Pose (Kneifen zwischen Zeigefinger und Daumen) = Eingabebestätigung_

### Raum einrichten

1.  _Auswählen eines Prefabs am Handmenü mit Poke Interaction oder Ray + Pinch Pose_
2.  _Prefab wurde an den Ray übergeben_
3.  _Halten der Pinch Pose zum Positionieren des Prefabs im Raum, (Loslassen = Platzierung)_

### Building Task

**Ziel:** Das Endergebnis mit den vorhandenen Bauteilen, anhand des Vorschau-Bildes nachbauen  
**Option:** Lösung anzeigen lassen per Button-Press

## Autor

**Autor:** Adrian Joos  
**Betreuer:** Herr Prof. Carsten Busch, Jonas Ehrhardt

## Quellen

Zum Entwickeln der Anwendung dienten mir diese Quellen stets zur Hilfe:

- https://www.youtube.com/@ValemTutorials
- https://www.youtube.com/@blackwhalestudio
- https://www.youtube.com/@immersiveinsiders
- https://www.youtube.com/@dilmerv
- https://github.com/oculus-samples/
