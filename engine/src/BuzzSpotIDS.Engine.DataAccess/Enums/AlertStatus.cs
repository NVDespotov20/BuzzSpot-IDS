namespace BuzzSpotIDS.Engine.DataAccess.Enums;

public enum AlertStatus : short
{
    New = 1,
    Acknowledged = 2,
    Resolved = 3,
    FalsePositive = 4
}