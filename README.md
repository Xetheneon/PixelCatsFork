# PixelCatsFork

This project contains a set of classes and interfaces for managing and displaying a pixel board, including hardware and console display backends, pixel representations, and serial communication helpers.

Below are class diagrams (in Mermaid format) for all major classes and interfaces in the **PixelBoard** project.  
You can paste these into a Markdown viewer that supports Mermaid, or view directly on GitHub with a Mermaid extension.

---

### ArduinoDisplay

```mermaid
classDiagram
    class IDisplay {
        <<interface>>
        +void Draw(IPixel[,])
        +void Draw(ILocatedPixel)
        +void DrawBatch(IEnumerable~ILocatedPixel~)
        +void DisplayInt(int)
        +void DisplayInt(int, bool?)
        +void DisplayInts(int, int)
    }

    class ArduinoDisplay {
        -DisplayHelper dh
        -bool finishedStreaming
        -SerialPortManager SerialPortManager
        -const string streamMode
        +ArduinoDisplay()
        +ArduinoDisplay(sbyte, sbyte, sbyte)
        -void initBoard()
        -void drawToFramerate(object, ElapsedEventArgs)
        +void Draw(IPixel[,])
        +void Draw(ILocatedPixel)
        +void DrawBatch(IEnumerable~ILocatedPixel~)
        +void DisplayInt(int)
        +void DisplayInt(int, bool?)
        +void DisplayInts(int, int)
    }

    class DisplayHelper
    class SerialPortManager

    ArduinoDisplay ..|> IDisplay
    ArduinoDisplay o-- DisplayHelper
    ArduinoDisplay o-- SerialPortManager
```

---

### ArduinoInput

```mermaid
classDiagram
    class ArduinoButtonEventArgs {
        +bool Left
        +bool Right
        +bool Fire
        +ArduinoButtonEventArgs(bool, bool, bool)
    }

    class ArduinoInput {
        -bool LastLeft
        -bool LastRight
        -bool LastFire
        -SerialPortManager SerialPortManager
        -event ButtonEventHandler ButtonPressEvent
        +ArduinoInput(SerialPortManager)
        -void HandleKeys(object, ArduinoButtonEventArgs)
        -void ManageKeyPresses(ButtonEventHandler)
        -void ButtonThread()
        +delegate void ButtonEventHandler(object, ArduinoButtonEventArgs)
    }
    class SerialPortManager

    ArduinoInput o-- SerialPortManager
    ArduinoInput --> ArduinoButtonEventArgs : uses
```

---

### ConsoleDisplay

```mermaid
classDiagram
    class IDisplay {
        <<interface>>
        +void Draw(IPixel[,])
        +void Draw(ILocatedPixel)
        +void DrawBatch(IEnumerable~ILocatedPixel~)
        +void DisplayInt(int)
        +void DisplayInt(int, bool?)
        +void DisplayInts(int, int)
    }

    class ConsoleDisplay {
        -DisplayHelper dh
        -bool refreshing
        +ConsoleDisplay()
        +ConsoleDisplay(sbyte, sbyte, sbyte)
        -void initBoard()
        -void drawToFramerate(object, ElapsedEventArgs)
        +void Draw(IPixel[,])
        +void Draw(ILocatedPixel)
        +void DrawBatch(IEnumerable~ILocatedPixel~)
        +void DisplayInt(int)
        +void DisplayInt(int, bool?)
        +void DisplayInts(int, int)
    }

    class DisplayHelper

    ConsoleDisplay ..|> IDisplay
    ConsoleDisplay o-- DisplayHelper
```

---

### DisplayHelper

```mermaid
classDiagram
    class DisplayHelper {
        -sbyte height
        -sbyte width
        -sbyte framerate
        -object boardLock
        -IPixel[,] lastBoard
        -IPixel[,] currentBoard
        -string lastLCDNumber
        -string currentLCDNumber
        +void SetFramerate(sbyte)
        +void SetSize(sbyte, sbyte)
        +void MakeTimer(ElapsedEventHandler)
        +void ValidateLCDValue(int, int, string)
        +void Draw(IPixel[,])
        +void Draw(ILocatedPixel)
        +void DisplayInt(int)
        +void DisplayInt(int, bool?)
        +void DisplayInts(int, int)
        +void RefreshDisplay(IDisplay)
    }

    class IPixel
    class ILocatedPixel
    class IDisplay

    DisplayHelper --> IPixel
    DisplayHelper --> ILocatedPixel
    DisplayHelper --> IDisplay : uses
```

---

### IArduinoInput

```mermaid
classDiagram
    class IArduinoInput {
        <<interface>>
        +event ButtonEventHandler ButtonPressEvent
        +delegate void ButtonEventHandler(object, ArduinoButtonEventArgs)
    }

    class ArduinoButtonEventArgs

    IArduinoInput --> ArduinoButtonEventArgs
```

---

### IDisplay

```mermaid
classDiagram
    class IDisplay {
        <<interface>>
        +void Draw(IPixel[,])
        +void Draw(ILocatedPixel)
        +void DrawBatch(IEnumerable~ILocatedPixel~)
        +void DisplayInt(int)
        +void DisplayInt(int, bool?)
        +void DisplayInts(int, int)
    }

    class IPixel
    class ILocatedPixel

    IDisplay --> IPixel
    IDisplay --> ILocatedPixel
```

---

### ILocatedPixel

```mermaid
classDiagram
    class ILocatedPixel {
        <<interface>>
        +sbyte Column
        +sbyte Row
    }
    class IPixel

    ILocatedPixel ..|> IPixel
```

---

### IPixel

```mermaid
classDiagram
    class IPixel {
        <<interface>>
        +byte Red
        +byte Green
        +byte Blue
    }
```

---

### LocatedPixel

```mermaid
classDiagram
    class LocatedPixel {
        -sbyte column
        -sbyte row
        +sbyte Column
        +sbyte Row
        +LocatedPixel(byte, byte, byte, sbyte, sbyte)
        +bool Equals(object)
    }
    class Pixel
    class ILocatedPixel

    LocatedPixel ..|> Pixel
    LocatedPixel ..|> ILocatedPixel
```

---

### Pixel

```mermaid
classDiagram
    class Pixel {
        -byte red
        -byte green
        -byte blue
        +byte Red
        +byte Green
        +byte Blue
        +Pixel(byte, byte, byte)
        +bool Equals(object)
    }
    class IPixel

    Pixel ..|> IPixel
```

---

### SerialPortManager

```mermaid
classDiagram
    class SerialPortManager {
        -static SerialPort serialPort
        +SerialPort SerialPort
        +SerialPortManager()
    }
    class SerialPort

    SerialPortManager o-- SerialPort
```
