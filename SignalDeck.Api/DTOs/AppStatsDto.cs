namespace SignalDeck.Api.DTOs;

public record AppStatsDto(
    int TotalSignals,
    int ErrorCount, 
    double ErrorRate, 
    string TopSignalName
);