# API Documentation

This repository does not contain the backend implementation for the leaderboard service. 

## Endpoint Summary

| Method | Route | Consumers |
| --- | --- | --- |
| GET | `/api/generate_code` | `PixelCatsClient/ApiClient.cs` |
| POST | `/api/codes` | `ConsoleTest/Leaderboardclient.cs`, `PixelCatsClient/ApiClient.cs`, `PixelCatsClient/PixelCatsApiClient.cs` |
| GET | `/api/leaderboard?limit={n}` | `PixelCatsClient/ApiClient.cs`, `PixelCatsClient/PixelCatsApiClient.cs` |

## GET /api/generate_code

### Purpose

Fetch a server-generated code value.

### Request Structure

- No request body is used by the client code.
- No query parameters are observed.

### Response Structure

```json
{
  "code": "ABC123"
}
```

### Validation Rules

- The client expects a successful HTTP status code.
- The client expects a JSON object with a non-empty `code` string property.

### Possible Error Responses

- Any non-success HTTP status causes the client to return `null`.
- Invalid JSON causes the client to return `null`.


## POST /api/codes

### Purpose

Submit a score/code payload to the leaderboard service and, in some flows, receive a claim code in the response.

### Observed Request Variants

The repository contains multiple client-side payload shapes for the same route:

1. `ConsoleTest/Leaderboardclient.cs`

```json
{
  "game_code": "VTwLvlyoHGw",
  "score": 123,
  "ts": 1715000000,
  "nonce": "base64urlnonce",
  "sig": "base64urlsig"
}
```

2. `PixelCatsClient/ApiClient.cs` and `PixelCatsClient/PixelCatsApiClient.cs`

```json
{
  "code": "ABC123",
  "score": 123,
  "game_code": "Tetris"
}
```

### Response Structure

Observed response shape for the claim-code minting flow:

```json
{
  "code": "ABC123"
}
```

For the boolean submission flow, only success or failure is observed by the client.

### Validation Rules

- `ConsoleTest/Leaderboardclient.cs` requires a non-blank `gameCode` and a numeric score.
- `PixelCatsClient/PixelCatsApiClient.cs` requires non-empty `code` and `gameCode` values.
- `LeaderboardClient` signs the payload with HMAC-SHA256 using a per-game secret and a Unix timestamp plus nonce.
- `PixelCatsApiClient` can attach an `x-api-key` header when provided.

### Possible Error Responses

- Any non-success HTTP status is treated as a failure by the clients.
- Missing or malformed `code` values in the response cause exceptions in the claim-code flow or a `false`/`null` result in the simplified client flows.


## GET /api/leaderboard?limit={n}

### Purpose

Retrieve leaderboard records, optionally limited to a maximum number of entries.

### Request Structure

- Query parameter: `limit` as an integer.

### Response Structure

Observed response shape as parsed by `PixelCatsClient/ApiClient.cs` and tested in `ConsoleTest.Tests/PixelCatsApiClientTests.cs`:

```json
[
  {
    "id": 1,
    "name": "Alice",
    "score": 100,
    "created_at": "2026-01-01T00:00:00Z",
    "gameName": "Tetris"
  }
]
```

The parsing logic also tolerates `game`, `gameName`, or `gameId` as the game field.

### Validation Rules

- The client expects a successful HTTP status code.
- The client expects a JSON array.
- Each entry should contain the expected score fields if the caller wants to use them.

### Possible Error Responses

- Non-success HTTP responses return an empty array.
- Invalid JSON returns an empty array.
- If the payload is not an array, the client returns an empty array.

