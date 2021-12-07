# BetterBroadcasts
Plugin that improves how broadcasts works

# Config:
```yaml
bbc:
  is_enabled: true
  debug_mode: false
  # Plugin is using basegame Broadcast Permission. If set to true, you need to grant bbc.broadcast permission for command to work
  unique_permission: false
  # When you use variables in BetterBroadcast Command they are replaced with provided text
  broadcast_variables:
    '%variable%': this is a test variable
  timed_broadcasts:
  - text: Test Timed Broadcast
    delay: 10
    duration: 10
  repeated_broadcasts:
  - interval: 100
    duration: 10
    text: Test Repeated Broadcast
```

