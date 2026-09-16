# Changelog

## [1.0.0] - 2026-09-16

### Added
- `EventManager`: a static, enum-keyed publish/subscribe event manager (`RegisterEvent`, `UnregisterEvent`, `InvokeEvent`) supporting both parameterless events and events that carry a typed payload.
- `EventTypes`: a plain, hand-edited enum listing every event in the project.
- Built-in payload types: `IntArgs`, `FloatArgs`, `BoolArgs`, `StringArgs`, `GameObjectArgs`, `Vector2Args`, `Vector3Args`, `AudioArgs`.
- `Create > Event System > Args Script` menu item for generating custom, project-specific payload classes from a template.
