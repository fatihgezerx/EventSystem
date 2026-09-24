# EventSystem

Type-keyed publish/subscribe event bus for Unity. Zero dependencies, zero allocations.

![EventSystem](ScreenShots/EventSystem.png)

## Overview

EventSystem is a lightweight, code-first event bus. An event is simply a type, usually a small
`readonly struct` declared by the system that raises it. There's no shared enum to edit and no
ScriptableObject: every system brings its own events, so packages that use EventSystem never have to
touch it.

## Features

- Static `EventManager` API: `Register<T>`, `Unregister<T>`, `Invoke<T>`, `HasListeners<T>`, `Clear`
- Events are types: type-safe payloads, and no central list that every package has to add to
- One static channel per event type: `Invoke` is a field read and a delegate call, with no dictionary
  lookup, no boxing, and no allocation for struct events
- `Create > Event System > Event Script` generates a ready-to-edit `readonly struct` event
- Handlers are cleared automatically when entering Play Mode without a domain reload

## Setup

### Requirements

- Unity 2021.3 LTS or newer

### Installation

Either:
- **Package Manager:** `Window > Package Manager > + > Add package from git URL`, and enter
  `https://github.com/fatihgezerx/EventSystem.git`
- **Or** copy the repository into your project's `Assets/`.

It has no dependencies, so it compiles in any project.

## Quick Start

**1. Declare an event.** Use `Create > Event System > Event Script`, or write it by hand:

```csharp
public readonly struct HealthChanged
{
    public readonly int Value;

    public HealthChanged(int value) => Value = value;
}

public readonly struct PlayerDied { }
```

**2. Register a listener**, e.g. in `OnEnable` / `OnDisable`:

```csharp
private void OnEnable() => EventManager.Register<HealthChanged>(OnHealthChanged);
private void OnDisable() => EventManager.Unregister<HealthChanged>(OnHealthChanged);

private void OnHealthChanged(HealthChanged e) => healthBar.value = e.Value;
```

**3. Invoke it where it actually happens:**

```csharp
EventManager.Invoke(new HealthChanged(80));
EventManager.Invoke<PlayerDied>(); // an event with no data
```

Register in `OnEnable` and unregister in `OnDisable`. The EventManager doesn't know about Unity's object
lifecycle, so a listener that forgets to unregister keeps receiving events after it should be gone.

## License

[MIT License](LICENSE)
