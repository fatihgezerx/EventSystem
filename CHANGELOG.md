# Changelog

## [2.0.0] - 2026-09-25

### Changed
- **Breaking:** events are now keyed by type instead of by an enum. `EventManager.Register<T>`,
  `Unregister<T>` and `Invoke<T>` replace `RegisterEvent`, `UnregisterEvent` and `InvokeEvent`. An event
  is any type, usually a `readonly struct` declared by the system that raises it, so packages that use
  EventSystem never have to edit it.
- Each event type has its own static channel: invoking is a field read and a delegate call, with no
  dictionary lookup, no boxing, and no allocation for struct events.

### Added
- `Invoke<T>()` for events with no data, `HasListeners<T>()`, `Clear<T>()`.
- `Create > Event System > Event Script` generates a `readonly struct` event.
- Handlers are cleared when entering Play Mode without a domain reload.
- `package.json`, so the package can be added from its git URL.

### Removed
- `EventTypes` and the built-in `Args` payload types (`IntArgs`, `BoolArgs`...). Declare your own event
  structs instead.

## [1.0.0] - 2026-09-16

### Added
- `EventManager`: a static, enum-keyed publish/subscribe event manager (`RegisterEvent`, `UnregisterEvent`, `InvokeEvent`) supporting both parameterless events and events that carry a typed payload.
- `EventTypes`: a plain, hand-edited enum listing every event in the project.
- Built-in payload types: `IntArgs`, `FloatArgs`, `BoolArgs`, `StringArgs`, `GameObjectArgs`, `Vector2Args`, `Vector3Args`, `AudioArgs`.
- `Create > Event System > Args Script` menu item for generating custom, project-specific payload classes from a template.
